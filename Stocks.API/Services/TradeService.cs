using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stocks.DTO;
using Stocks.Repository;
using Microsoft.Extensions.Logging;
using Stocks.DTO.Exceptions;
using System.Diagnostics;

namespace Stocks.Services
{
    public class TradeService : ITradeService
    {
        private IStockService _stockService;
        private ILog _logger;

        public TradeService( IStockService stockService_in,ILog logger_in)
        {
            _logger = logger_in;
            _stockService = stockService_in;
        }

       

        public bool AddTrade(TradeEntry tradeEntry)
        {
            bool result = false;
           
            verifyPositive(tradeEntry.Price);
            verifyPositive(tradeEntry.Quantity);

            if(tradeEntry != null)
            {
                result = _stockService.Repository.recordTrade(tradeEntry);
            }

            return result;
        }

        private void verifyPositive(double value)
        {
            if (value <= 0)
            {
                _logger.Log(TraceEventType.Error,"Found negative price value");
                throw new InvalidValueException("Found non-positive value: " + value);
            }
        }

        public IList<TradeEntry> FilterTradeEntriesByTime(Stock stock_in, int minutes_in)
        {
            throw new NotImplementedException();
        }

        public IList<TradeEntry> RetrieveTrades()
        {
            throw new NotImplementedException();
        }
    }
}