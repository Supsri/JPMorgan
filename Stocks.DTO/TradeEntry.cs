using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace Stocks.DTO
{
    // A class to hold trade information
    public class TradeEntry
    {
        //The stock symbol
        private String stockSymbol;

        //Trade timestamp
        private DateTime timeStamp;

        //quantity of shares
        private int quantity;

        //buy or sell indicator
        private TradeIndicator indicator = TradeIndicator.BUY;

        //trade price
        private double price;
        private string tEST_COMMON_STOCK;
        private TradeIndicator bUY;

        public TradeEntry(string tEST_COMMON_STOCK, DateTime timestamp, int quantity, TradeIndicator indc, double price)
        {
            this.StockSymbol = tEST_COMMON_STOCK;
            this.TimeStamp = timestamp;
            this.quantity = quantity;
            this.Indicator = indc;
            this.price = price;
        }

        //Properties

        public String StockSymbol
        {
            get { return stockSymbol; }

            set { stockSymbol = value; }
        }

        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }

            set
            {
                timeStamp = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public TradeIndicator Indicator
        {
            get
            {
                return indicator;
            }

            set
            {
                indicator = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
    }
}
