using CoffeShop.DataProvider;
using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Business.Model;
using CoffeShop.ExtentionCommon;
using System.Windows.Input;
using CoffeShop.View.Categorys;
using CoffeShop.Viewmodel.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.Internationalization;

namespace CoffeShop.Viewmodel.Categorys
{
   public class CategoryViewModel : BindableBase, ICustomDialog
   {
        #region [Varable]
        private object _dialogContent;
        private bool _isOpendialog;
        private PagingViewmodel _pagingViewmodel;
        private string _nameSearch;





        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); }
        }
        public PagingViewmodel PagingViewmodel
        {
            get { return _pagingViewmodel; }
            set { _pagingViewmodel = value; OnPropertyChanged(); }
        }
        public object DialogContent 
        {
            get { return _dialogContent; }
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog 
        {
            get { return _isOpendialog; }
            set { _isOpendialog = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Collection]
        private List<CategoryModel> _categoryList;


        public List<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }


        #endregion
        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(AddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, Edit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, Delete); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadCategory); } }
        #endregion
        public CategoryViewModel()
        {
            NameSearch = String.Empty;
            PagingViewmodel = new PagingViewmodel(SearchCategory);
            PagingViewmodel.TotalCountItem = LoadTotalCount();
            LoadCategory();
        }
       
        public int LoadTotalCount()
        {
            using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                return unitOfWork.Category.Count();
        }
        public void LoadCategory()
        {
            PagingViewmodel.TotalCountItem = LoadTotalCount();
        }        
        public void SearchCategory(int pageIndex, int pageSize)
        {
            using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
            {
                var listDataFinal = new List<CategoryModel>();
                var listData = unitOfWork.Category.GetListPaging(pageIndex, pageSize, NameSearch).ToList();

                foreach (Category item in listData)
                {
                    CategoryModel catModel = MyExtention.CloneData<CategoryModel>(item);
                    listDataFinal.Add(catModel);
                }
                CategoryList = listDataFinal;
            }
        }       
        public void AddNew()
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(LoadCategory, CloseDialog)));           
        }
        public void Edit(CategoryModel category)
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(LoadCategory, CloseDialog,category)));
        }
        public void Delete(CategoryModel category)
        {
            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), category.Name);
            OpenDialog(new ConfirmUC(question,
                () =>
                {
                    using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                    {
                        var cat = unitOfWork.Category.FindById(category.Id);
                        unitOfWork.Category.Remove(cat);
                        var result = unitOfWork.Completed();
                        if (string.IsNullOrEmpty(result.ErrorMessage))
                        {
                            LoadCategory();
                            CloseDialog();
                        }                            
                    }                   

                }, CloseDialog));
            
        }
        public void OpenDialog(object uc = null)
        {
            if (uc != null)
                DialogContent = uc;
            IsOpenDialog = true;
        }
        public void CloseDialog()
        {
            IsOpenDialog = false;
        }
   }
}
