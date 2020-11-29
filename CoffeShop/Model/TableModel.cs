using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
   public class TableModel
   {
        public Guid Id { get; set; }      
        public string Name { get; set; }
        public bool IsLock { get; set; }
        public bool Status { get; set; }
        public int Serial { get; set; }
   }
}
