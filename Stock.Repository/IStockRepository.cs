using Stocks.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Stocks.Repository
{
    public interface IStockRepository
    {
        //register a stock
        void registerStock(Stock stock);

        //unregister a stock
        void unregisterStock(String stockSymbol);

        //Record a trade represented by the incoming object
        bool recordTrade(TradeEntry trade);

        //gets list that contains all trades
        IList<TradeEntry> getTrades();

        //gets stocl represented by the input symbol
        Stock getStockBySymbol(String stockSymbol);

        //Gets list of stocks supported by the application
        IDictionary<String, Stock> getStocks();
                
        //get trade entries for the stock within the specified minutes
        IList<TradeEntry> getTradeEntriesByTime(Stock stock_in, int minutes);
    }
}
    