using Business.Model;
using CoffeShop.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DataProvider.Repositories
{
    public class ImageRepository : Repository<Images>, IImageRepository
    {
        public ImageRepository(CoffeeShopContext coffeeShopContext) : base(coffeeShopContext) { }
    }
}
