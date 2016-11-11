using Stocks.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StocksAPI.Controllers
{
    public class TradeController : ApiController
    {
        public bool AddRecord(String symbol, DateTime timestamp, int quantity, TradeIndicator indicator, double price)
        {
            return true;
        }
    }
}
