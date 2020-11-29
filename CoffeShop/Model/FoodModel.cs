using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static CoffeShop.Enums.ALL_ENUM;

namespace CoffeShop.Model
{
   public class FoodModel : BindableBase
   {
        private Guid _id;
        private Guid _idCategory;
        private String _name;
        private bool _isOutOfStock;
        private double _price;
        private String _note;
        private TYPE_FOOD _type;
        private String _catName;
        private String _imageData;



        public Guid Id 
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public Guid CategoryId
        {
            get { return _idCategory; }
            set { _idCategory = value; OnPropertyChanged(); }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public bool IsOutOfStock
        {
            get { return _isOutOfStock; }
            set { _isOutOfStock = value; OnPropertyChanged(); }
        }
        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }
        public String Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged(); }
        }
        public TYPE_FOOD Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        public String CategoryName
        {
            get { return _catName; }
            set { _catName = value; OnPropertyChanged(); }
        }
        public String ImageData
        {
            get { return _imageData; }
            set { _imageData = value; OnPropertyChanged(); }
        }

        public FoodModel()
        {
            this.Id = Guid.NewGuid();
            this.CategoryId = Guid.Empty;
            this.Name = String.Empty;
            this.Note = String.Empty;
            this.CategoryName = String.Empty;
        }
   }
}
