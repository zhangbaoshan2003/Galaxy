using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galaxy.BAL;

namespace Galaxy.Tests
{
    [TestClass]
    public class BusinessLayerTest
    {
        [TestMethod]
        public void TestFetchProduct()
        {
            ProductDataManager mgr = new ProductDataManager();
            var product = mgr.FetchProduct(6189, new DateTime(2017, 11, 11), "股票");
            Assert.IsTrue(product.Portfolio.Count > 0);
            Console.WriteLine(product.Portfolio.Count);
        }

        [TestMethod]
        public void TestFetchAssetDis()
        {
            ProductDataManager mgr = new ProductDataManager();
            var product = mgr.FetchCurrentProductFundAssetDist(6189, new DateTime(2017, 11, 11));
            Assert.IsTrue(product.Count > 0);
            Console.WriteLine(product.Count);
        }

        [TestMethod]
        public void TestFetchEquitIndustryDist()
        {
            ProductDataManager mgr = new ProductDataManager();
            var model = mgr.FetchProductEquityAssetDist(6189, new DateTime(2017, 11, 11));
            Assert.IsTrue(model.Dictionary.Keys.Count > 0);
            Console.WriteLine(model.Dictionary.Values.Count>0);

            //var totalCount = model.Dictionary.Values.Count;
            //var oneCount = model.Dictionary["工业"].Capacity;
            //Assert.IsTrue(totalCount == oneCount * model.Dictionary.Keys.Count);
        }
    }
}
