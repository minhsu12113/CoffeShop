using CoffeShop.DataProvider;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Management;
using CoffeShop.DBHelper;
using System.ComponentModel;
using CoffeShop.Internationalization;
using static CoffeShop.Enums.ALL_ENUM;
using CoffeShop.Model.UI;
using System.Windows.Media;
using CoffeShop.View.Category;
using CoffeShop.View.Home;
using CoffeShop.View.Foods;
using CoffeShop.View.Statistics;
using CoffeShop.View.User;
using CoffeShop.View.Setting;
using CoffeShop.View.Dialog;
using Business.Model;
using CoffeShop.Model;
using CoffeShop.ExtentionCommon;
using System.Threading;
using CoffeShop.View.FoodsTable;

namespace CoffeShop.Viewmodel
{
    public class MainViewmodel : BindableBase, ICustomDialog
    {
        #region [Vareable]
        private object _currentView;
        private WindowState _stateWindow;
        private object _dialogContent;
        private bool _isOpenDialog;
        private bool _isLoadSomeThing;



        public bool IsLoadSomeThing
        {
            get => _isLoadSomeThing;
            set { _isLoadSomeThing = value; OnPropertyChanged(); }
        }
        public object DialogContent
        {
            get => _dialogContent;
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog
        {
            get => _isOpenDialog;
            set { _isOpenDialog = value; OnPropertyChanged(); }
        }
        public WindowState StateWindow
        {
            get { return _stateWindow; }
            set { _stateWindow = value; OnPropertyChanged(); }
        }
        public object CurrenView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]        
        public ICommand NavigateToViewCMD { get { return new CommandHelper<ItemNavigate>((v) => { return v != null; }, NavigateToView); } }
        public ICommand DragmoveWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, DragmoveWindow); } }
        public ICommand MinimizedWindowCMD { get { return new CommandHelper(MinimizedWindow); } }
        public ICommand ShutdownAppCMD { get { return new CommandHelper(ShutdownApp); } }
        #endregion

        public MainViewmodel()
        {
            StringResources.ApplyLanguage(Enums.ALL_ENUM.LANGUAGE.VN);
            CurrenView = new HomeUC();
            StateWindow = WindowState.Normal;
            GenerationTabel();
        }
        public void NavigateToView(ItemNavigate itemNavigate)
        {
            HandleItemNavigateWhenChangeView(itemNavigate);

            switch (itemNavigate.TypeView)
            {
                case TYPE_VIEW.HOME:
                    CurrenView = new HomeUC();
                    break;
                case TYPE_VIEW.DASHBOARD:
                    CurrenView = new FoodsTableUC();
                    break;
                case TYPE_VIEW.FOODS:
                    CurrenView = new FoodsUC();
                    break;
                case TYPE_VIEW.CATEGORY:
                    CurrenView = new CategoryUC();
                    break;
                case TYPE_VIEW.STATISTICS:
                    CurrenView = new StatisticsUC();
                    break;
                case TYPE_VIEW.USER:
                    CurrenView = new UserUC();
                    break;
                case TYPE_VIEW.SETTING:
                    CurrenView = new SettingsUC();
                    break;
                default:
                    break;
            }
        }
        public void HandleItemNavigateWhenChangeView(ItemNavigate itemNavigate)
        {
            if (itemNavigate != null)
            {
                ItemNavigate.ListItemNavigate.ForEach((i) => { i.BackgoundItem = new SolidColorBrush(Colors.Transparent); i.ForegroundItem = new SolidColorBrush(Colors.White); i.StatePointer = Visibility.Collapsed; });
                itemNavigate.ForegroundItem = new SolidColorBrush(Color.FromRgb(51, 38, 174));
                itemNavigate.BackgoundItem = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                itemNavigate.StatePointer = Visibility.Visible;
            }
        }
        public void DragmoveWindow(Window window)
        {
            window.DragMove();
        }
        public void MinimizedWindow()
        {
            StateWindow = WindowState.Minimized;
        }
        public void ShutdownApp()
        {
            OpenDialog(new ConfirmUC("Bạn có muốn đóng ứng dụng không?", () => { App.Current.Shutdown(); }, CloseDialog));
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
        public async void GenerationTabel()
        {
            await Task.Run(() =>
            {
                var listTabel = new List<Table>();
                for (int i = 1; i < 51; i++)
                {
                    Table table = new Table()
                    {
                        Id = Guid.NewGuid(),
                        Name = $@"Bàn {i}",
                        Serial = i

                    };
                    listTabel.Add(table);
                }
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    int checkCountTable = unitOfWork.Table.Count();
                    if (checkCountTable <= 0)
                    {
                        unitOfWork.Table.AddRange(listTabel);
                        unitOfWork.Completed();
                    }
                }
            });
        }
    }
}
