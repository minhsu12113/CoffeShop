using Business.Model;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Interfaces
{
   public interface IFoodRepository : IRepository<Foods>
   {
        IEnumerable<Foods> GetListPaging(int pageIndex, int pageSize, string nameSearch);
        IEnumerable<FoodModel> GetListPagingWithImage(int pageIndex, int pageSize, string nameSearch, List<Guid> filterList);
        int GetCountByName(string name);
        int Count();
   }
}
