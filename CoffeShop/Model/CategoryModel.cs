using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
    public class CategoryModel : BindableBase
    {
        private Guid _id;
        private string _name;
        private string _note;
        private string _userCreated;
        private DateTime _timeCreated;
        private string _userModified;
        private Nullable<DateTime> _timeModified;
        private bool _isSelected = true;



        public bool IsSelected 
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }
        public Guid Id 
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        [MaxLength(39)]
        public string Name 
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged(); }
        }
        public string UserCreated
        {
            get { return _userCreated; }
            set { _userCreated = value; OnPropertyChanged(); }
        }
        public DateTime TimeCreated
        {
            get { return _timeCreated; }
            set { _timeCreated = value; OnPropertyChanged(); }
        }
        public string UserModified
        {
            get { return _userModified; }
            set { _userModified = value; OnPropertyChanged(); }
        }
        public Nullable<DateTime> TimeModified
        {
            get { return _timeModified; }
            set { _timeModified = value; OnPropertyChanged(); }
        }

        public CategoryModel()
        {
            this.Id = Guid.NewGuid();
            this.Name = String.Empty;
            this.Note = String.Empty;
            this.UserCreated = CSGlobal.Instance.CurrentUser.UserName;
            this.TimeCreated = DateTime.Now;
            this.UserModified = String.Empty;
        }
    }
}
