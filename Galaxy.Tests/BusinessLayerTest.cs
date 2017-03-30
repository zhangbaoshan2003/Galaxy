﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Galaxy.BAL;

namespace Galaxy.Tests
{
    [TestClass]
    public class BusinessLayerTest
    {
        private int productId = 6189;
        [TestMethod]
        public void TestFetchProductFundAssetDist()
        {
            ProductDataManager mgr = new ProductDataManager();
            var product = mgr.FetchProductFundAssetDist(productId, new DateTime(2017, 11, 11));
            Assert.IsTrue(product.Dictionary.Count > 0.0);
            Assert.IsTrue(product.Dictionary["股票"].Count> 0);
        }

        [TestMethod]
        public void TestFetchProduct()
        {
            ProductDataManager mgr = new ProductDataManager();
            var product = mgr.FetchProduct(productId, new DateTime(2017, 11, 11), "股票");
            Assert.IsTrue(product.TotalAssetMarketValue>0.0);
            Assert.IsTrue(product.Portfolio.Count > 0);
            Console.WriteLine(product.Portfolio.Count);
        }

        [TestMethod]
        public void TestFetchAssetDis()
        {
            ProductDataManager mgr = new ProductDataManager();
            var product = mgr.FetchCurrentProductFundAssetDist(productId, new DateTime(2017, 11, 11));
            Assert.IsTrue(product.Count > 0);
            Console.WriteLine(product.Count);
        }

        [TestMethod]
        public void TestFetchEquitIndustryDist()
        {
            ProductDataManager mgr = new ProductDataManager();
            var model = mgr.FetchProductEquityAssetDist(productId, new DateTime(2017, 11, 11));
            Assert.IsTrue(model.Dictionary.Keys.Count > 0);
            Console.WriteLine(model.Dictionary.Values.Count>0);
        }

        [TestMethod]
        public void TestFetchProductNetValueDistViewModel()
        {
            ProductDataManager mgr = new ProductDataManager();
            var model = mgr.FetchProductNetValueDistViewModel(productId);
            Assert.IsTrue(model.Dictionary.Keys.Count > 0);
            Console.WriteLine(model.Dictionary.Values.Count > 0);
        }
    }
}
