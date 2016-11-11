using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Owin;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Stocks.Services;
using Stocks.Repository;
using Stocks.DTO;
using System.Net.Http;
using Stocks;
using StocksAPI.Controllers;

[assembly: OwinStartup("WebAPI",typeof(Stocks.Services.StockService))]

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            var loggerFactory = new LoggerFactory();

            //builder.RegisterAssemblyTypes(typeof(Stock).Assembly);
            //builder.RegisterType<Dictionary<string, Stock>>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterInstance<ILog>(new LoggerAdapter(loggerFactory.CreateLogger("Stocks")));
            builder.RegisterType<StockRepository>().AsImplementedInterfaces().WithProperty("Stocks",new Dictionary<String,Stock>()).WithProperty("Trades", new List<TradeEntry>()).InstancePerLifetimeScope();
           
            builder.RegisterType<StockService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        
            builder.RegisterType<HttpClient>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //container.Resolve<IDictionary<string,Stock>>();
            
            var t2 = container.Resolve<IStockRepository>();
            var t1 = container.Resolve<IStockService>(); 

            var t3 = container.Resolve<GBCEController>();

            app.UseAutofacMiddleware(container);
           // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }

