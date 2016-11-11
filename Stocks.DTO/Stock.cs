using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.DTO
{
    public class Stock
    {
        private String stockSymbol;

        private StockType stockType;

        private double lastDividend;

        private double fixedDividend;

        private double parValue;

        private double tickerPrice;

        private IList<TradeEntry> tradeEntries;

        public Stock()
        {
            tradeEntries = new List<TradeEntry>();
        }

        public string StockSymbol
        {
            get
            {
                return stockSymbol;
            }

            set
            {
                stockSymbol = value;
            }
        }

        public StockType StockType
        {
            get
            {
                return stockType;
            }

            set
            {
                stockType = value;
            }
        }

        public double LastDividend
        {
            get
            {
                return lastDividend;
            }

            set
            {
                lastDividend = value;
            }
        }

        public double FixedDividend
        {
            get
            {
                return fixedDividend;
            }

            set
            {
                fixedDividend = value;
            }
        }

        public double ParValue
        {
            get
            {
                return parValue;
            }

            set
            {
                parValue = value;
            }
        }

        public double TickerPrice
        {
            get
            {
                return tickerPrice;
            }

            set
            {
                tickerPrice = value;
            }
        }

        public IList<TradeEntry> TradeEntries
        {
            get
            {
                return tradeEntries;
            }

            set
            {
                tradeEntries = value;
            }
        }
    }
}
