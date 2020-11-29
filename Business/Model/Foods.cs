using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
   public class Foods
   {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public bool IsOutOfStock { get; set; }
        public double Price { get; set; }
        public string Note { get; set; }
        public int Type { get; set; }
   }
}
