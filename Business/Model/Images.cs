using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model
{
    public class Images
    {
        public Guid Id { get; set; }
        public Guid IdParent { get; set; }
        public string Data { get; set; }
    }
}
