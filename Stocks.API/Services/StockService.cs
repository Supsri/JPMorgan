using Microsoft.Extensions.Logging;
using Stocks.DTO;
using Stocks.DTO.Exceptions;
using Stocks.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Stocks.Services
{
    public class StockService : IStockService
    {
        private IStockRepository _repository;

        private ILog _logger;
        

        public StockService(IStockRepository repository_in, ILog logger_in)
        {
            _repository = repository_in;
            _logger = logger_in;
        }

        // Stock repository
        public IStockRepository Repository
        {
            get
            {
                return _repository;
            }
            
        }


        //Calculate stock price for specified trade range
        public double calculateStockPriceForRange(string stockSymbol, int range)
        {
            _logger.Log(TraceEventType.Information," Calculating stock price for the symbol : " + stockSymbol );

            double stockPrice= 0.0;
            double accumulatedShareQuantity = 0.0;
            double accumulatedTradePrice = 0.0;

            Stock stock = _repository.getStockBySymbol(stockSymbol);

            _logger.Log(TraceEventType.Information,"Trades in the original collection : " + stock.TradeEntries.Count);
            IList<TradeEntry> tradeEntries = _repository.getTradeEntriesByTime(stock, range);
            _logger.Log(TraceEventType.Information,"Trades in the filtered collection : " + tradeEntries.Count);

            accumulatedTradePrice = tradeEntries.Where(t=>t.Price > 0).Select(t => t.Price * t.Quantity ).ToList().Sum(c=>c);
            accumulatedShareQuantity = tradeEntries.Sum(t => t.Quantity);

            if (accumulatedShareQuantity > 0)
                stockPrice = accumulatedTradePrice / accumulatedShareQuantity;

            return stockPrice;
            
        }

        // calculate dividend yield
        public double getDividendYield(string symbol, double tickerPrice)
        {
            Stock stock = _repository.getStockBySymbol(symbol);

            verifyPositive(tickerPrice);

            double result = 0.0;

            if (stock.StockType == StockType.COMMON)
                result = stock.LastDividend / tickerPrice;

            else
            {
                result = (stock.FixedDividend * stock.ParValue) / tickerPrice;
            }

            return result;
        }

        // calculate GBCE all share index
        public double getGBCEAllShareIndex()
        {
            if (_repository.getStocks().Count == 0)
            {
                _logger.Log(TraceEventType.Error, "Zero registered stocks found");
                throw new NotFoundException("No stocks registered. Please register stocks first");
            }

            IList<Stock> stocks = _repository.getStocks().Values.ToList();

            double stockPriceAccumulate = 1;

            IList<double> stockListFiltered = stocks.Where(s => s.TickerPrice > 0).Select(s => s.TickerPrice).ToList();

            int stockCount = stockListFiltered.Count;

            stockPriceAccumulate = stockListFiltered.Aggregate((a, b) => a * b);

            double gbce = 0.0;

            gbce = Math.Pow(stockPriceAccumulate, 1/stockCount);

            return stockPriceAccumulate;

        }

        //calculates the p/e ratio of the given stock based on the given price
        public double getPERatio(string symbol, double tickerPrice)
        {
            verifyPositive(tickerPrice);

            double result = tickerPrice / getDividendYield(symbol, tickerPrice);

            return result;
        }

        public void registerStock(Stock stock_in)
        {
            try
            {
                _repository.registerStock(stock_in);
            }

            catch(InvalidValueException ex)
            {
                _logger.Log(TraceEventType.Error, ex.Message);
                throw ex;
            }
        }

        public void unregisterStock(string stock_Id)
        {
            if (!this.Repository.getStocks().ContainsKey(stock_Id))
            {
                String errorMessage = "The stock " + stock_Id + " has not been registerd.";
                throw new NotFoundException(errorMessage);
            }
            this.Repository.unregisterStock(stock_Id);
        }

        private void verifyPositive(double value)
        {
            if (value <= 0)
            {
                _logger.Log(TraceEventType.Error, "Found negative price value");
                throw new InvalidValueException("Found non-positive value: " + value);
            }
        }
    }
}