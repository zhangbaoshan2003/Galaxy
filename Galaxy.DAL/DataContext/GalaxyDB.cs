using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.DAL.Model;

namespace Galaxy.DAL.DataContext
{
    public class GalaxyDB : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<ProductType> ProductTypes{ get; set; }

        public DbSet<StrategyType> StrategyTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<SecurityInfo> Securitys { get; set; }

        public DbSet<InducstryInfo> InducstryList{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public GalaxyDB()
            : base("GalaxyDBServer")
        {
        }
    }
}
