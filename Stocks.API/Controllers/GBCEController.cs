using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Stocks;
using Stocks.DTO.Exceptions;
using Stocks.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StocksAPI.Controllers
{
    public class GBCEController : ApiController
    {
        private ILog _logger;
        private IStockService _service;
        

        public GBCEController(ILog logger_in, IStockService service_in)
        {
            _logger = logger_in;
            _service = service_in;
        }

        public IHttpActionResult Get()
        {
            try
            {
                double result = _service.getGBCEAllShareIndex();
                return Ok(result);
            }

            catch(NotFoundException)
            {
                return NotFound();
            }

            catch(Exception)
            {
                return InternalServerError();
            }
        }
    }
}
