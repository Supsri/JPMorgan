using Stocks.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Services
{
    public interface ITradeService
    {
        bool AddTrade(TradeEntry tradeEntry);

        IList<TradeEntry> RetrieveTrades();

        IList<TradeEntry> FilterTradeEntriesByTime(Stock stock_in,int minutes_in);
    }
}
