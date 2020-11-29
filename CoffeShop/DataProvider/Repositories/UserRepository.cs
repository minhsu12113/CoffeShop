using Business.Model;
using CoffeShop.DataProvider.Interfaces;
using CoffeShop.ExtentionCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
   public class UserRepository : Repository<User>,IUserRepository
   {
        public UserRepository(CoffeeShopContext coffeeShopContext) : base(coffeeShopContext) { }
        public CoffeeShopContext CoffeeShopContext
        {
            get { return Context as CoffeeShopContext; }
        }

        public void CheckAndCreateAccountAdmin()
        {
            var checkExitsAc = CoffeeShopContext.User.FirstOrDefault((u) => u.UserName == "admin");
            if (checkExitsAc == null)
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    unitOfWork.User.Add(new User()
                    { 
                        UserName = "admin",
                        PassWord = MyExtention.Base64Encode("admin")
                    });
                    unitOfWork.Completed();
                }
            }            
        }
        public int GetCountByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetListPaging(int pageIndex, int pageSize, string unitName)
        {
            throw new NotImplementedException();
        }
   }
}
