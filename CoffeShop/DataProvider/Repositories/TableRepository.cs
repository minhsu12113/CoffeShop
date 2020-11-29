using CoffeShop.DataProvider.Interfaces;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Business.Model;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(CoffeeShopContext coffeeShopContext ): base(coffeeShopContext) { }
        public CoffeeShopContext CoffeeShopContext
        {
            get { return Context as CoffeeShopContext; }
        }
        public int Count()
        {
            return CoffeeShopContext.Table.Count();
        }
        public int GetCountByName(string name)
        {
            return CoffeeShopContext.Table.Where((u) => u.Name.Contains(name)).ToList().Count;
        }
        public IEnumerable<Table> GetListPaging(int pageIndex, int pageSize, string nameSearch)
        {
            return CoffeeShopContext.Table
            .Where(t => t.Name.Contains(nameSearch))
            .OrderBy(t => t.Serial)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }
    }
}
