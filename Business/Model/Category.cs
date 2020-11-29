using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
   public class Category 
   {
        public Guid Id { get; set; }
        public ICollection<Foods> Foods { get; set; }
        [MaxLength(32)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Note { get; set; }
        public string UserCreated { get; set; }
        public DateTime TimeCreated { get; set; }
        public string UserModified { get; set; }
        public Nullable<DateTime> TimeModified { get; set; }
   }
}
