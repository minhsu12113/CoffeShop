using Business.Model;
using CoffeShop.DataProvider;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Categorys
{
    public class CateroryAddOrUpdateViewModel : BindableBase
    {
        #region [Variable]
        private CategoryModel _currentCategory;
        private int _maxLengthName;
        public bool _isEdit { get; set; }


        public int MaxLengthName
        {
            get { return _maxLengthName; }
            set { _maxLengthName = value; OnPropertyChanged(); }
        }
        public CategoryModel CurrentCategory
        {
            get { return _currentCategory; }
            set { _currentCategory = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Action]
        public Action ReLoadList { get; set; }
        public Action CloseDialogParent { get; set; }
        #endregion
        #region [Command]
        public ICommand CloseDialogCMD { get { return new CommandHelper(CloseDialogParent); } }
        public ICommand SaveCMD { get { return new CommandHelper(Save); } }
        #endregion

        public CateroryAddOrUpdateViewModel(Action reLoadListCat, Action closeDialog, CategoryModel category = null)
        {
            if (category == null) // Add new
            {
                CurrentCategory = new CategoryModel(); 
            }                
            else  // Edit
            {
                CurrentCategory = category.Clone();
                _isEdit = true;
            }
               

            MaxLengthName = CurrentCategory.GetAttributeFrom<MaxLengthAttribute>(nameof(CurrentCategory.Name)).Length;
            ReLoadList = reLoadListCat;
            CloseDialogParent = closeDialog;
        }
        public void Save()
        {
            using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
            {                
                var cat = MyExtention.CloneData<Category>(CurrentCategory);
                if (_isEdit)
                {
                    cat.TimeModified = DateTime.Now;
                    cat.UserModified = CSGlobal.Instance.CurrentUser.UserName;
                }

                unitOfWork.Category.AddOrUpdate(cat);
                var result = unitOfWork.Completed();
                if (string.IsNullOrEmpty(result.ErrorMessage))
                {
                    CloseDialogParent();
                    ReLoadList();
                }
                else
                {
                    CloseDialogParent();
                    var selectDatacontext = CSGlobal.Instance.MainWindow.DataContext as MainViewmodel;
                    if (selectDatacontext != null)
                    {
                        selectDatacontext.OpenDialog(new ErrorNotifyDialogUC(new ErrorNotifyViewModel(result.ErrorMessage, selectDatacontext.CloseDialog))); ;
                    }
                }
            }
        }
    }
}
