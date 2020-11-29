using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
   public class Bill
   {
        public Guid Id { get; set; }        
        public Guid TableId { get; set; }
        public Table Table { get; set; }
        public int Count { get; set; }
        public double TotalPrice { get; set; }
        public DateTime TimePayMent { get; set; }
        public ICollection<Billinfo> Billinfo { get; set; }
   }
}
