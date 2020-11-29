using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
    public class ImageModel : BindableBase
    {
        private Guid _id;
        private Guid _idParent;
        private String _data;




        public Guid Id 
        {
            get { return _id; }
            set{ _id = value; OnPropertyChanged(); }
        }
        public Guid IdParent
        {
            get { return _idParent; }
            set { _idParent = value; OnPropertyChanged(); }
        }
        public String Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(); }
        }
        public ImageModel()
        {
            this.Id = Guid.NewGuid();
            this.IdParent = Guid.Empty;
            this.Data = String.Empty;
        }
    }
}
