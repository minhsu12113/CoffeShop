using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeShop.ExtentionCommon;
using CoffeShop.DataProvider;
using Business.Model;

namespace CoffeShop.Viewmodel.Food
{
    public class FoodsAddOrUpdateViewModel : BindableBase
    {
        #region [Variable]
        private FoodModel _foodCurrent;
        private CategoryModel _categoryCurrent;


        public CategoryModel CategoryCurrent
        {
            get { return _categoryCurrent; }
            set { _categoryCurrent = value; OnPropertyChanged(); }
        }
        public FoodModel FoodCurrent
        {
            get { return _foodCurrent; }
            set { _foodCurrent = value; }
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
        public ICommand OpenDialogChooseImageCMD { get { return new CommandHelper(OpenDialogChooseImage); } }
        public ICommand CloseDialogParentCMD { get { return new CommandHelper(CloseDialogParent.Invoke); } }
        public ICommand SaveCMD { get { return new CommandHelper(Save); } }
        #endregion
        #region [Action]
        public Action CloseDialogParent { get; set; }
        public Action ReloadFoodsList { get; set; }
        #endregion

        public FoodsAddOrUpdateViewModel(Action closeDialog, Action reloadFoodsList, FoodModel foodModel = null)
        {
            if (foodModel != null)
                FoodCurrent = foodModel.Clone();
            else
                FoodCurrent = new FoodModel();

            CloseDialogParent = closeDialog;
            ReloadFoodsList = reloadFoodsList;            
            LoadCategoryList();
        }
        public void OpenDialogChooseImage()
        {
            OpenFileDialog openFile = new OpenFileDialog() { Filter = "Image files (*.jpg, *.png) | *.jpg; *.png" };

            if (openFile.ShowDialog() == DialogResult.OK)
                FoodCurrent.ImageData = MyExtention.ConvertImageToBase64(openFile.FileName);
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
            }
        }
        public void Save()
        {
            using (var uniOfWork = new UnitOfWork(new CoffeeShopContext()))
            {
                var selectImage = uniOfWork.Images.SingleOrDefault((i) => i.IdParent == FoodCurrent.Id);
                if (selectImage != null)
                    uniOfWork.Images.Remove(selectImage);

                uniOfWork.Foods.AddOrUpdate(new Foods()
                {
                    Id = FoodCurrent.Id,
                    CategoryId = CategoryCurrent.Id,
                    Name = FoodCurrent.Name,
                    Note = FoodCurrent.Note,
                    Price = FoodCurrent.Price,
                    IsOutOfStock = FoodCurrent.IsOutOfStock
                });
                uniOfWork.Images.AddOrUpdate(new Images() 
                {
                    Id = Guid.NewGuid(), 
                    IdParent = FoodCurrent.Id,
                    Data = FoodCurrent.ImageData 
                });
                var result = uniOfWork.Completed();
                if (string.IsNullOrEmpty(result.ErrorMessage))
                {
                    CloseDialogParent.Invoke();
                    ReloadFoodsList.Invoke();
                }                   
            }
        }
    }
}
