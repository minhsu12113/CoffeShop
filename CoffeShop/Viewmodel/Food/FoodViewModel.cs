using Business.Model;
using CoffeShop.DataProvider;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.View.Foods;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Food
{
    public class FoodViewModel : BindableBase, ICustomDialog
    {
        #region [Variable]
        private object _dialogContent;
        private bool _isOpenDialog;
        private PagingViewmodel _pagingViewmodel;
        private string _searchFoodContent;
        private bool _isWaitLoadData;


        public bool IsWaitLoadData
        {
            get { return _isWaitLoadData; }
            set { _isWaitLoadData = value; OnPropertyChanged(); }
        }
        public string SearchFoodContent
        {
            get { return _searchFoodContent; }
            set { _searchFoodContent = value; OnPropertyChanged(); }
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
            get { return _isOpenDialog; }
            set { _isOpenDialog = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Collection]
        private List<FoodModel> _foodList;
        private List<CategoryModel> _categoryList;
        private List<Guid> _categoryFilterList;


        public List<Guid> CategoryFilterList
        {
            get { return _categoryFilterList; }
            set { _categoryFilterList = value; OnPropertyChanged(); }
        }
        public List<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }
        public List<FoodModel> FoodList
        {
            get { return _foodList; }
            set { _foodList = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(AddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Edit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Delete); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadFood); } }
        public ICommand CheckedCategoryTagCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, CheckedCategoryTag); } }
        public ICommand UnCheckedCategoryTagCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, UnCheckedCategoryTag); } }
        #endregion

        public FoodViewModel()
        {
            LoadCategoryList();
            SearchFoodContent = string.Empty;
            PagingViewmodel = new PagingViewmodel(LoadFoodListPaging);
            LoadFood();
        }
        public void AddNew()
        {
            OpenDialog(new FoodsAddOrUpdateUC(new FoodsAddOrUpdateViewModel(CloseDialog, LoadFood)));
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
        public async void LoadFood()
        {
            // Gọi hàm LoadFoodListPaging khi TotalCountItem có sự thay đổi
            PagingViewmodel.TotalCountItem = await LoadCountFood();
        }
        public async void LoadFoodListPaging(int pageIndex, int pageSize)
        {
            OpenDialog(new WaitingDialogUc());

            await Task.Delay(500);
            await Task.Run(() => 
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    FoodList = unitOfWork.Foods.GetListPagingWithImage(pageIndex, pageSize, SearchFoodContent, CategoryFilterList).ToList();
                    //IsWaitLoadData = false;
                    //CSGlobal.Instance.MainViewmodel.IsLoadSomeThing = false;
                    CloseDialog();
                }
            });
        }
        public async Task<int> LoadCountFood()
        {
            int count = 0;
            await Task.Delay(500);
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    count = unitOfWork.Foods.Find((f) => f.Name.Contains(SearchFoodContent) && CategoryFilterList.Contains(f.CategoryId)).Count();
                }               
            });
            return count;
        }
        public void Edit(FoodModel foodModel)
        {
            using (var context = new UnitOfWork(new CoffeeShopContext()))
            {
                foodModel.ImageData = context.Images.SingleOrDefault((i) => i.IdParent == foodModel.Id)?.Data;
                OpenDialog(new FoodsAddOrUpdateUC(new FoodsAddOrUpdateViewModel(CloseDialog, LoadFood, foodModel)));
            }
        }
        public void Delete(FoodModel foodModel)
        {
            OpenDialog(new ConfirmUC($@"Bạn có muốn xóa [{foodModel.Name}] không?", () =>
            {
                using (var context = new UnitOfWork(new CoffeeShopContext()))
                {
                    var selectFood = context.Foods.FindById(foodModel.Id);
                    var selectImage = context.Images.SingleOrDefault((i) => i.IdParent == foodModel.Id);
                    if (selectFood != null)
                    {
                        context.Foods.Remove(selectFood);
                        if (selectImage != null)
                            context.Images.Remove(selectImage);

                        var result = context.Completed();
                        if (string.IsNullOrEmpty(result.ErrorMessage))
                        {
                            CloseDialog();
                            LoadFood();
                        }
                    }
                }
            }, CloseDialog));
        }
        public void LoadCategoryList()
        {
            using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
            {
                var catListBase = unitOfWork.Category.GetAll().ToList();
                var catListConvert = new List<CategoryModel>();
                if (catListBase != null)
                {
                    foreach (var category in catListBase)
                    {
                        CategoryModel cloener = category.CloneData<CategoryModel>();
                        catListConvert.Add(cloener);
                    }
                }
                CategoryList = catListConvert;
                CategoryFilterList = catListConvert.Select((i) => i.Id).ToList();
            }
        }
        public void CheckedCategoryTag(CategoryModel category)
        {
            CategoryFilterList.Add(category.Id);
        }
        public void UnCheckedCategoryTag(CategoryModel category)
        {
            CategoryFilterList.Remove(category.Id);
        }
    }
}
