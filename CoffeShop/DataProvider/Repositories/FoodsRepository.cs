using Business.Model;
using CoffeShop.DataProvider.Interfaces;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
    public class FoodsRepository : Repository<Foods>, IFoodRepository
    {
        public FoodsRepository(CoffeeShopContext coffeeShopContext) : base(coffeeShopContext) { }
        public CoffeeShopContext CoffeeShopContext
        {
            get { return Context as CoffeeShopContext; }
        }
        public int Count()
        {
            return CoffeeShopContext.Foods.Count();
        }
        public int GetCountByName(string name)
        {
            return CoffeeShopContext.Foods.Where((u) => u.Name.Contains(name)).ToList().Count;
        }
        public IEnumerable<Foods> GetListPaging(int pageIndex, int pageSize, string nameSearch)
        {
            return CoffeeShopContext.Foods
           .Where(t => t.Name.Contains(nameSearch))
           .OrderBy(t => t.Name)
           .Skip((pageIndex - 1) * pageSize)
           .Take(pageSize)
           .ToList();
        }
        public IEnumerable<FoodModel> GetListPagingWithImage(int pageIndex, int pageSize, string nameSearch, List<Guid> fiterList)
        {
            return (from f in CoffeeShopContext.Foods
                   join c in CoffeeShopContext.Category on f.CategoryId equals c.Id
                   join i in CoffeeShopContext.Images on f.Id equals i.IdParent
                   where f.Name.Contains(nameSearch)
                   where fiterList.Contains(f.CategoryId)
                   select new FoodModel
                   {
                       Id = f.Id,
                       Name = f.Name,
                       CategoryName = c.Name,
                       ImageData = i.Data,
                       Note = f.Note,
                       CategoryId = f.CategoryId,
                       IsOutOfStock = f.IsOutOfStock,
                       Price = f.Price,
                       Type = (Enums.ALL_ENUM.TYPE_FOOD)f.Type
                   }).ToList().Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }
    }
}
