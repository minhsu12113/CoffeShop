using CoffeShop.DataProvider.Interfaces;
using CoffeShop.DataProvider.Repositories;
using CoffeShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoffeeShopContext _context;
        public UnitOfWork(CoffeeShopContext context)
        {
            _context = context;
            Table = new TableRepository(_context);
            User = new UserRepository(_context);
            Category = new CategoryRepository(_context);
            Foods = new FoodsRepository(_context);
            Images = new ImageRepository(_context);
        }
        public ResultHelper Completed()
        {
            ResultHelper resultHelper = new ResultHelper();

            try
            {
                resultHelper.AffectRow = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    resultHelper.ErrorMessage = ex.InnerException.Message;
                resultHelper.ErrorMessage = ex.Message;
            }
            return resultHelper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public ITableRepository Table { get; }
        public IUserRepository User { get; }
        public ICategoryRepository Category { get; }
        public IFoodRepository Foods { get; }
        public IImageRepository Images { get; }
    }
}
