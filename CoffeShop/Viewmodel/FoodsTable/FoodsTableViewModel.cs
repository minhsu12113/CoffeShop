using Business.Model;
using CoffeShop.DataProvider;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.View.Dialog;
using CoffeShop.View.FoodsTable;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.DashBoard
{
    public class FoodsTebleViewModel : BindableBase,ICustomDialog
    {
        #region [Variable]
        private string _nameSearch;
        private PagingViewmodel _pagingViewmodel;
        private object _dialogContent;
        private bool _isOpenDialog;


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
        public PagingViewmodel PagingViewmodel
        {
            get { return _pagingViewmodel; }
            set { _pagingViewmodel = value; OnPropertyChanged(); }
        }
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]
        public ICommand SearchCMD { get { return new CommandHelper(LoadTable); } }
        public ICommand AddOrEditFoodTabelCMD { get { return new CommandHelper<TableModel>((t) => { return t != null; }, AddOrUpdateFoodTabel); } }
        #endregion
        #region [Collection]
        private List<TableModel> _tableList;


        public List<TableModel> TableList
        {
            get { return _tableList; }
            set { _tableList = value; OnPropertyChanged(); }
        }
        #endregion

        public FoodsTebleViewModel()
        {
            NameSearch = String.Empty;
            PagingViewmodel = new PagingViewmodel(SearchTable,18);
            LoadTable();            
        }
        public async void LoadTable()
        {
            PagingViewmodel.TotalCountItem = await LoadTotalCount();            
        }
        public async Task<int> LoadTotalCount()
        {
            int count = 0;
            await Task.Delay(500);
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    count = unitOfWork.Table.GetCountByName(NameSearch);
                }
            });
            return count;
        }
        public async void SearchTable(int pageIndex, int pageSize)
        {
            OpenDialog(new WaitingDialogUc());
            await Task.Delay(500);
            await Task.Run(() => 
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    var listDataFinal = new List<TableModel>();
                    var listData = unitOfWork.Table.GetListPaging(pageIndex, pageSize, NameSearch).ToList();

                    foreach (Table item in listData)
                    {
                        TableModel tableModel = MyExtention.CloneData<TableModel>(item);
                        listDataFinal.Add(tableModel);
                    }
                    TableList = listDataFinal;
                    CloseDialog();
                }
            });           
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
        public void AddOrUpdateFoodTabel(TableModel table)
        {
            OpenDialog(new AddOrUpdateFoodTabelUC());
        }
    }
}
