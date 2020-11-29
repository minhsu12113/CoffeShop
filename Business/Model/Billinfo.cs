using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
   public class Billinfo
   {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid FoodsId { get; set; }
        public Bill Bill { get; set; }       
        public Foods Foods { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
   }
}
