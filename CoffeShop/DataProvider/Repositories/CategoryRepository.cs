using Business.Model;
using CoffeShop.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CoffeeShopContext coffeeShopContext) : base(coffeeShopContext) { }
        public CoffeeShopContext CoffeeShopContext
        {
            get { return Context as CoffeeShopContext; }
        }
        public int Count()
        {
            return CoffeeShopContext.Category.Count();
        }
        public int GetCountByName(string name)
        {
            return CoffeeShopContext.Category.Where((u) => u.Name.Contains(name)).ToList().Count;
        }
        public IEnumerable<Category> GetListPaging(int pageIndex, int pageSize, string nameSearch)
        {
            return CoffeeShopContext.Category
            .Where(t => t.Name.Contains(nameSearch))
            .OrderBy(t => t.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }
    }
}
