using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.DTO;
using Microsoft.Extensions.Logging;
using Stocks.DTO.Exceptions;
using System.Numerics;
using System.Linq.Expressions;

namespace Stocks.Repository
{
    public class StockRepository : IStockRepository
    {
        //internal representation for stocks
        private IDictionary<string, Stock> stocks = null;

        //internal representation of trades
        private IList<TradeEntry> trades = null;

        private ILogger<IStockRepository> logger = null;

        public IDictionary<string, Stock> Stocks
        {
            get
            {
                return stocks;
            }

            set
            {
                stocks = value;
            }
        }

        public IList<TradeEntry> Trades
        {
            get
            {
                return trades;
            }

            set
            {
                trades = value;
            }
        }

        public StockRepository()
        {
            Stocks = new Dictionary<string,Stock>();
            Trades = new List<TradeEntry>();
        }

        //Get the associated stock for the input symbol
        public Stock getStockBySymbol(string stockSymbol)
        {
            Stock stock = null;
            bool isValueValid = false;

            if (stockSymbol != null)
                isValueValid = Stocks.TryGetValue(stockSymbol, out stock);

            if (!isValueValid)
            {
                throw new NotFoundException("Cannot find the stock " + stockSymbol + " in the market. Please register the stock first");
            }
        
            return stock;
        }

        public IDictionary<string, Stock> getStocks()
        {
            return Stocks;
        }

        public IList<TradeEntry> getTrades()
        {
            return Trades;
        }

        // record a new trade
        public bool recordTrade(TradeEntry trade)
        {
            bool result = false;
            
            string stockSymbol = trade.StockSymbol;
            Stock associatedStock = getStockBySymbol(stockSymbol);

            if (associatedStock != null)
            {
                associatedStock.TradeEntries.Add(trade);
                result = true;
            }

            return result;
        }

        
        //get trade entries within the given time frame
        public IList<TradeEntry> getTradeEntriesByTime(Stock stock_in, int minutes_in)
        {
            Stock stock = stock_in;

            Func<TradeEntry, bool> inGivenMinutes = t => (t.TimeStamp.Subtract(DateTime.Now).TotalMinutes <= minutes_in);

            IEnumerable<TradeEntry> tradeEntries = stock_in.TradeEntries.Where(inGivenMinutes);

            return tradeEntries.ToList();
        }
            
        /// <summary>
        /// register the incoming stock
        /// </summary>
        /// <param name="stock"></param>
        public void registerStock(Stock stock)
        {
            string errorMessage = "The stock with id "+stock.StockSymbol+"has already been registered";

            if(this.Stocks.ContainsKey(stock.StockSymbol))
            {
                throw new InvalidValueException(errorMessage);
                
            }

            Stocks.Add(stock.StockSymbol,stock);
        }

        /// <summary>
        /// unregister the stock with the specified symbol
        /// </summary>
        /// <param name="stockSymbol"></param>
        public void unregisterStock(string stockSymbol)
        {
            string errorMessage = "The stock with id "+stockSymbol+"has not been registered";

            if (!Stocks.ContainsKey(stockSymbol))
            {
                throw new NotFoundException(errorMessage);
            }

            this.Stocks.Remove(stockSymbol);
        }
    }
}
