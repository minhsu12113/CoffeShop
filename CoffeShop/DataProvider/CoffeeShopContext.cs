using Business.Model;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(): base(DBHelper.DBSetting.Instance.MainConectString) 
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoffeeShopContext, Migrations.Configuration>());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Billinfo> Billinfo { get; set; }
    }
}
