using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       public ITableRepository Table { get; }
       public IUserRepository User { get; }
       public ICategoryRepository Category { get; }
       public IFoodRepository Foods { get; }
       public IImageRepository Images { get; }      
       public ResultHelper Completed();
    }
}
