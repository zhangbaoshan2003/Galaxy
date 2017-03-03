using System;
using System.Data.Entity;
using System.Linq;
using Galaxy.DAL.DataContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Galaxy.Tests
{
    [TestClass]
    public class GalaxyDBTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                Database.SetInitializer(new GalaxyDBInitializer());
                using (var db = new GalaxyDB())
                {
                    var admins = db.StrategyTypes.ToList();
                    admins.ForEach(a =>
                    {
                        Console.WriteLine(a.ID + ":" + a.Name);
                    });
                    Console.WriteLine(admins.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
