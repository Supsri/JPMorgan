using Stocks.DTO;
using Stocks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public interface IStockService
    {
        //calculates the dividend yield
        double getDividendYield(String symbol, double tickerPrice);

        //calculate p/e ratio of a given stock based on given price
        double getPERatio(String symbol, double tickerPrice);

        //Calculates the GBCE all share Index value based on prices of all stocks
        double getGBCEAllShareIndex();

        // calculate stock price considering trades within given range
        double calculateStockPriceForRange(String stockSymbol, int range);

        //register the specified stock with the details
        void registerStock(Stock stock_in);

        //unregister the stock with the specified id
        void unregisterStock(string stock_Id);

        IStockRepository Repository { get; }

    }
}
