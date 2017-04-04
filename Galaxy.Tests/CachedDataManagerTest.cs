using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Galaxy.BAL;

namespace Galaxy.Tests
{
    [TestClass]
    public class CachedDataManagerTest
    {
        [TestMethod]
        public void TestFetchProductNetValueDistViewModelWithCache()
        {
            CachedDataManager manager = new CachedDataManager();

            Stopwatch sp = new Stopwatch();
            sp.Start();
            var result = manager.FetchProductNetValueDistViewModel(6189);
            sp.Stop();
            Assert.IsNotNull(result);
            var timeElapsedWithoutCache = sp.Elapsed.TotalSeconds;

            sp = new Stopwatch();
            sp.Start();
            result = manager.FetchProductNetValueDistViewModel(6189);
            sp.Stop();
            Assert.IsNotNull(result);
            var timeElapsedWtihCache = sp.Elapsed.TotalSeconds;

            Assert.IsTrue(timeElapsedWithoutCache > timeElapsedWtihCache * 2);

        }

        [TestMethod]
        public void TestFetchProductViewModelWithCache()
        {
            CachedDataManager manager = new CachedDataManager();

            Stopwatch sp = new Stopwatch();
            sp.Start();
            var result = manager.FetchProduct(6189,new DateTime(2017,1,1),"股票");
            sp.Stop();
            Assert.IsNotNull(result);
            var timeElapsedWithoutCache = sp.Elapsed.TotalSeconds;

            sp = new Stopwatch();
            sp.Start();
            result = manager.FetchProduct(6189, new DateTime(2017, 1, 1), "股票");
            sp.Stop();
            Assert.IsNotNull(result);
            var timeElapsedWtihCache = sp.Elapsed.TotalSeconds;

            Assert.IsTrue(timeElapsedWithoutCache > timeElapsedWtihCache * 2);

        }
    }
}
