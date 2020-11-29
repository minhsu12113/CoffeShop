using Business.Model;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Interfaces
{
    public interface ITableRepository : IRepository<Table>
    {
        IEnumerable<Table> GetListPaging(int pageIndex, int pageSize, string nameSearch);
        int GetCountByName(string name);
        int Count();
    }
}
