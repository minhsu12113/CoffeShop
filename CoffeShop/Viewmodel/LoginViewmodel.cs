using Business.Model;
using CoffeShop.DataProvider;
using CoffeShop.ExtentionCommon;
using CoffeShop.Internationalization;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Schema;

namespace CoffeShop.Viewmodel
{
    public class LoginViewmodel : BindableBase, ICustomDialog
    {
        #region [Variable]
        private object _dialogContent;
        private bool _isOpendialog;
        private User _currentUser;
        private WindowState _windowState;






        public WindowState StateWindow
        {
            get { return _windowState; }
            set { _windowState = value; OnPropertyChanged(); }
        }
        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }
        public object DialogContent
        {
            get => _dialogContent;
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog 
        { 
            get => _isOpendialog;
            set { _isOpendialog = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]
        public ICommand DragMoveWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, DragMoveWindow); } }
        public ICommand LoginCMD { get { return new CommandHelper(Login); } }
        public ICommand MiniMizedWindowCMD { get { return new CommandHelper(MiniMizedWindow); } }
        public ICommand CloseWindowCMD { get { return new CommandHelper(CloseWindow); } }
        #endregion


        public LoginViewmodel()
        {
            CurrentUser = new User();
        }
        public void CloseDialog()
        {
            IsOpenDialog = false;
        }
        public void OpenDialog(object uc = null)
        {
            if (uc != null)
                DialogContent = uc;
            IsOpenDialog = true;
        }
        public void OpenDialog(int timeSecondAutoClose, object uc = null)
        {
            if (uc != null)
            {
                if (timeSecondAutoClose > 0)
                    Task.Delay(TimeSpan.FromSeconds(timeSecondAutoClose)).ContinueWith((t, _) => CloseDialog(), null, TaskScheduler.FromCurrentSynchronizationContext());
                DialogContent = uc;
            }               
            IsOpenDialog = true;
        }
        public void DragMoveWindow(Window window)
        {
            window.DragMove();
        }
        public void Login()
        {
            if (!string.IsNullOrWhiteSpace(CurrentUser.UserName) && !string.IsNullOrWhiteSpace(CurrentUser.UserName))
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    string passDecode = MyExtention.Base64Encode(CurrentUser.PassWord);
                    var exitsAcc = unitOfWork.User.SingleOrDefault((u) => u.UserName == CurrentUser.UserName && u.PassWord == passDecode);
                    if (exitsAcc != null)
                    {
                        CSGlobal.Instance.LoginWindow.Hide();
                        CSGlobal.Instance.MainWindow.Show();
                        CSGlobal.Instance.CurrentUser = MyExtention.CloneData<UserModel>(CurrentUser);
                    }
                    else
                    {
                        OpenDialog(2, new WarningUC(StringResources.Find("ERROR_LOGIN")));
                    }
                }
            }
            else
            {
                OpenDialog(2, new WarningUC(StringResources.Find("ERROR_LOGIN")));
            }
        }
        public void MiniMizedWindow()
        {
            StateWindow = WindowState.Minimized;
        }
        public void CloseWindow()
        {
            OpenDialog(new ConfirmUC("Bạn có muốn đóng ứng dụng không?", () => { App.Current.Shutdown(); }, CloseDialog));
        }
    }
}
