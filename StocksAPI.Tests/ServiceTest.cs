using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks.Services;
using Moq;
using Stocks.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stocks.DTO;
using Stocks.DTO.Exceptions;

namespace StocksAPI.Tests
{
   [TestClass]
    public class ServiceTest
    {
        private StockService stockService;

        private TradeService tradeService;
        /**
         * Runs before every test.
         */
         [TestInitialize]
        public void setUp()
        {
           //Mock<IStockRepository> rep = new Mock<IStockRepository>();
           // rep.Setup(r=>r.getStocks()).Returns(new Mock<IDictionary<string,Stock>>().Object);
            this.stockService = new StockService(new StockRepository(),new Mock<ILog>().Object);
            this.tradeService = new TradeService(stockService, new Mock<ILog>().Object);
        }

        [TestMethod]
        public void testRegisterAndUnregisterStock()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            Stock stock_out;
            this.stockService.Repository.getStocks().TryGetValue(TestUtil.TEST_COMMON_STOCK, out stock_out);
            Assert.AreEqual(stock, stock_out );
            this.stockService.unregisterStock(TestUtil.TEST_COMMON_STOCK);
            Assert.IsTrue((this.stockService.Repository.getStocks().Count) == 0);
        }

    
    [TestMethod]
    [ExpectedException(typeof(InvalidValueException))]
	public void testRegisterStockDuplicated()
        {
           Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            this.stockService.registerStock(stock);
        }


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void testUnregisterStockNonExist()
        {
            this.stockService.unregisterStock(TestUtil.TEST_COMMON_STOCK);
        }


        [TestMethod]
        public void testGetDividendYield_CommonStock()
        {

            Stock commonStock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(commonStock);
            commonStock.LastDividend = 23;
            double result = this.stockService.getDividendYield(TestUtil.TEST_COMMON_STOCK, 130);
            Assert.AreEqual(0.177, Math.Round(result,3));
        }

        [TestMethod]
        public void testGetDividendYield_PreferredStock()
        {
            Stock preferredStock = TestUtil.getDefaultPreferredStock();
            this.stockService.registerStock(preferredStock);
            preferredStock.FixedDividend = 0.02;
            preferredStock.ParValue = 100;
            double result = this.stockService.getDividendYield(TestUtil.TEST_PREFERRED_STOCK, 130);
            Assert.AreEqual(0.015, Math.Round(result,3));

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void testGetDividendYield_InvalidPrice()
        {
            Stock commonStock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(commonStock);
            commonStock.LastDividend = 23;
            this.stockService.getDividendYield(TestUtil.TEST_COMMON_STOCK, 0);

        }

        [TestMethod]
        public void testGetPERatio()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            stock.LastDividend = 23;
            double result = this.stockService.getPERatio(TestUtil.TEST_COMMON_STOCK, 60);
            Assert.AreEqual(157, Math.Round(result));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void testGetPERatio_InvalidPrice()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            stock.LastDividend = 23;
            this.stockService.getPERatio(TestUtil.TEST_COMMON_STOCK, 0);
        }

       

        [TestMethod]
        public void testRecordTrade()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            DateTime timestamp = new DateTime();
            int quantity = 1000;
            double price = 1250;
            this.tradeService.AddTrade(new TradeEntry(TestUtil.TEST_COMMON_STOCK, timestamp, quantity, TradeIndicator.BUY, price));
            TradeEntry record = stock.TradeEntries.First();
            Assert.AreEqual(TestUtil.TEST_COMMON_STOCK, record.StockSymbol);
            Assert.AreEqual(timestamp, record.TimeStamp);
            Assert.AreEqual(quantity, record.Quantity);
            Assert.AreEqual(price, record.Price);
            Assert.AreEqual(TradeIndicator.BUY, record.Indicator);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void testRecordTrade_InvalidPrice()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            int quantity = 1000;
            int price = -1;
            this.tradeService.AddTrade(new TradeEntry(TestUtil.TEST_COMMON_STOCK, DateTime.Now, quantity, TradeIndicator.BUY, price));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void testRecordTrade_InvalidQuantity()
        {
            Stock stock = TestUtil.getDefaultCommonStock();
            this.stockService.registerStock(stock);
            int quantity = 0;
            int price = 100;
            this.tradeService.AddTrade(new TradeEntry(TestUtil.TEST_COMMON_STOCK, DateTime.Now, quantity, TradeIndicator.BUY, price));
        }

 

        [TestMethod]
    public void testGBCEAllShareIndex()
        {
            Stock commonStock1 = new Stock();
            commonStock1.StockType = StockType.COMMON;
            commonStock1.LastDividend = 0.0;
            commonStock1.ParValue = 0.0;
            commonStock1.TickerPrice = 1234;
            commonStock1.StockSymbol = TestUtil.TEST_COMMON_STOCK + 1;
            
            Stock commonStock2 = new Stock();
            commonStock2.StockType = StockType.COMMON;
            commonStock2.LastDividend = 0.0;
            commonStock2.ParValue = 0.0;
            commonStock2.TickerPrice = 7057;
            commonStock2.StockSymbol = TestUtil.TEST_COMMON_STOCK + 2;
           
            Stock commonStock3 = new Stock();
            commonStock3.StockType = StockType.COMMON;
            commonStock3.LastDividend = 0.0;
            commonStock3.ParValue = 0.0;
            commonStock3.TickerPrice = 0;
            commonStock3.StockSymbol = TestUtil.TEST_COMMON_STOCK + 3;
            Stock preferStock1 = new Stock();
            preferStock1.StockType = StockType.PREFERRED;
            preferStock1.LastDividend = 0.0;
            preferStock1.ParValue = 0.0;
            preferStock1.TickerPrice = 583;
            preferStock1.StockSymbol = TestUtil.TEST_PREFERRED_STOCK + 1;
            Stock preferStock2 = new Stock();
            preferStock2.StockType = StockType.PREFERRED;
            preferStock2.LastDividend = 0.0;
            preferStock2.ParValue = 0.0;
            preferStock2.TickerPrice = 0;
            preferStock2.StockSymbol = TestUtil.TEST_PREFERRED_STOCK + 2;
            this.stockService.registerStock(commonStock1);
            this.stockService.registerStock(commonStock2);
            this.stockService.registerStock(commonStock3);
            this.stockService.registerStock(preferStock1);
            this.stockService.registerStock(preferStock2);
            Assert.AreEqual(5076961054, this.stockService.getGBCEAllShareIndex());
        }

       
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void testGBCEAllShareIndex_NoStock()
        {
            Assert.AreEqual(0, this.stockService.getGBCEAllShareIndex());
        }

    }
}
