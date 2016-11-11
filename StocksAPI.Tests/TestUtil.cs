using Stocks.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StocksAPI.Tests
{
    public class TestUtil
    {
        /** The symbol of the stock. */
        public static readonly String PREFIX_STOCK_SYMBOL = "test_stock_";
	
	/** The symbol of the common stock. */
	public static readonly String TEST_COMMON_STOCK = PREFIX_STOCK_SYMBOL+"common";
	
	/** The symbol of the preferred stock. */
	public static readonly String TEST_PREFERRED_STOCK = PREFIX_STOCK_SYMBOL+"preferred";

	
	    public static double getRandomDouble()
        {
            var random = new Random();
            return  random.NextDouble() * 100000;
        }

        
        public static Stock getDefaultCommonStock()
        {
            Stock s =  new Stock();
            s.StockType = StockType.COMMON;
            s.LastDividend = 0.0;
            s.ParValue = 0.0;
            s.TickerPrice = 0.0;
            s.StockSymbol = TEST_COMMON_STOCK;
            return s;
        }

        
        public static Stock getDefaultPreferredStock()
        {
            Stock s = new Stock();
            s.StockType = StockType.PREFERRED;
            s.LastDividend = 0.0;
            s.ParValue = 0.0;
            s.TickerPrice = 0.0;
            s.StockSymbol = TEST_PREFERRED_STOCK;
            return s;
        }

    }
}