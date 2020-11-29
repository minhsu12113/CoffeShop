using Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void CheckAndCreateAccountAdmin();
        IEnumerable<User> GetListPaging(int pageIndex, int pageSize, string nameSearch);
        int GetCountByName(string name);
    }
}
