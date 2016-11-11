using Stocks;
using Stocks.DTO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace StocksAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api")]
    public class StockController : ApiController
    {
        private IStockService _stockService;

        public StockController(IStockService service_in)
        {
            _stockService = service_in;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("stock/{stockId}/{range}/stockPrice", Name = "PriceForMinutesRange")]
        public IHttpActionResult Get(string stockId, int range)
        {
            try
            {
                var stockPriceForRange = _stockService.calculateStockPriceForRange(stockId, range);

                return Ok(stockPriceForRange);
            }

            catch (NotFoundException ex)
            {
                return NotFound();
            }

            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("stock/{stockId}/{tickerPrice}/dividendYield", Name = "DividendYieldForPrice")]
        public IHttpActionResult GetDividendYield(string stockId, double tickerPrice)
        {
            try
            {
                var dividendYield = _stockService.getDividendYield(stockId, tickerPrice);
                return Ok(dividendYield);
            }

            catch(NotFoundException ex)
            {
                return NotFound();
            }

            catch(InvalidValueException ex)
            {
                return BadRequest();
            }
        }
        
        [System.Web.Http.Route("stock")]
        public IHttpActionResult Post([FromBody]Stocks.DTO.Stock stock_in)
        {
            
                if (stock_in == null)
                {
                    return BadRequest();
                }

                try
                {
                    _stockService.registerStock(stock_in);
                    return Ok();
                }

                catch(InvalidValueException)
                {
                    return BadRequest();
                }
                
                catch(Exception)
                {
                    return InternalServerError();
                }
                
            
        }
    }
}
