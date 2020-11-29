using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
   public class Table
   {
        public Guid Id { get; set; }
        public int Serial { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public string Name { get; set; }
        public bool IsLock { get; set; }
        public bool Status { get; set; }
   }
}
 