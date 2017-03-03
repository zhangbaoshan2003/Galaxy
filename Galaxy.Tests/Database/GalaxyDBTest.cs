using System;
using System.Data.Entity;
using System.Linq;
using Galaxy.DAL.DataContext;
using Galaxy.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Galaxy.Tests
{
    [TestClass]
    public class GalaxyDBTest
    {
        [TestMethod]
        public void SeedGalaxyDB()
        {
            Database.SetInitializer(new GalaxyDBInitializer());

            using (var db = new GalaxyDB())
            {
                var admins = db.Products.ToList();
                admins.ForEach(a =>
                {
                    Console.WriteLine(a.ID + ":" + a.Name);
                });
                Console.WriteLine(admins.Count);
                Assert.AreEqual(50, admins.Count);
            }
        }
    }
}
