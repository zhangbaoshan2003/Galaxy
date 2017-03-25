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
            var product = mgr.FetchProduct(6189, new DateTime(2017, 11, 11));
            Assert.IsTrue(product.Portfolio.Count > 0);
            Console.WriteLine(product.Portfolio.Count);
        }
    }
}
