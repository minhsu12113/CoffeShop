using CoffeShop.DataProvider;
using CoffeShop.DBHelper;
using CoffeShop.Internationalization;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CoffeShop.Viewmodel
{
    public class DisconnectedDatabaseViewmodel : BindableBase, ICustomDialog
    {
        #region [Variable]
        private object _selectedSeverName;
        private List<string> _listServerName;
        private string _messConnected;
        private bool _isValidConnect;
        private bool _isOpenDialog;
        private object _dialogContent;
        private Brush _colorMess;
        private WindowState _stateWindow;
        private string _currentServerName;




        public WindowState StateWindow
        {
            get { return _stateWindow; }
            set { _stateWindow = value; OnPropertyChanged(); }
        }
        public Brush ColorMess
        {
            get { return _colorMess; }
            set { _colorMess = value; OnPropertyChanged(); }
        }
        public bool IsValidConnect
        {
            get { return _isValidConnect; }
            set { _isValidConnect = value; OnPropertyChanged(); }
        }
        public string MessConnected
        {
            get { return _messConnected; }
            set { _messConnected = value; OnPropertyChanged(); }
        }
        public object SelectedSeverName
        {
            get { return _selectedSeverName; }
            set { _selectedSeverName = value; OnPropertyChanged(); CheckConnectSQLServer(value.ToString()); }
        }
        public List<string> ListServerName
        {
            get { return _listServerName; }
            set { _listServerName = value; OnPropertyChanged(); }
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
        #region [Command]
        public ICommand DragMoveWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, DragMoveWindow); } }
        public ICommand ChangeLanguageToVietNameseCMD { get { return new CommandHelper(ChangeLanguageToVietNamese); } }
        public ICommand ChangeLanguageToVietEnglishCMD { get { return new CommandHelper(ChangeLanguageToVietEnglish); } }
        public ICommand MinimizedWindowCMD { get { return new CommandHelper(MinimizedWindow); } }
        public ICommand CloseWindowCMD { get { return new CommandHelper(CloseWindow); } }
        public ICommand SaveServerNameCMD { get { return new CommandHelper(SaveServerName); } }
        public ICommand LoadedWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, LoadedWindow); } }
        #endregion

        public void LoadedWindow(Window window)
        {
            GetListServerName();
        }
        public DisconnectedDatabaseViewmodel()
        {
            StringResources.ApplyLanguage(Enums.ALL_ENUM.LANGUAGE.VN);
            StateWindow = WindowState.Normal;
            GetListServerName();
        }
        private async void GetListServerName()
        {
            OpenDialog(new WaitingDialogUc());

            await Task.Run(() =>
            {
                var listServerName = new List<string>();
                while (listServerName.Count == 0)
                {                    
                    SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                    DataTable table = instance.GetDataSources();
                    string ServerName = Environment.MachineName;
                    foreach (DataRow row in table.Rows)
                    {
                        string server = ServerName + "\\" + row["InstanceName"].ToString();
                        listServerName.Add(server);
                    }
                    App.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (listServerName.Count > 0)
                        {
                            ListServerName = listServerName;
                            CloseDialog();
                        }
                    }));
                }
            });
        }
        public void CheckConnectSQLServer(string serverName)
        {
            if (!string.IsNullOrEmpty(serverName))
            {
                OpenDialog(new WaitingDialogUc());
                Task.Run(() =>
                {
                    IsValidConnect = DBSetting.Instance.CheckConnectMasterDB(serverName);
                    App.Current.Dispatcher?.Invoke(() =>
                    {
                        if (IsValidConnect)
                        {
                            MessConnected = string.Format(StringResources.Find("CONNECTED_TO_SERVER_SUCCES"), serverName);
                            ColorMess = new SolidColorBrush(Colors.Blue);
                            _currentServerName = serverName;
                            DBSetting.Instance.MainConectString = DBSetting.Instance.BuidConnectString(_currentServerName);
                            DBSetting.Instance.WriteConfig(_currentServerName);
                        }
                        else
                        {
                            MessConnected = string.Format(StringResources.Find("CONNECTED_TO_SERVER_ERROR"), serverName);
                            ColorMess = new SolidColorBrush(Colors.Red);
                        }
                        CloseDialog();
                    });
                });
            }
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
        public void DragMoveWindow(Window windown)
        {
            windown.DragMove();
        }
        public void ChangeLanguageToVietNamese()
        {
            StringResources.ApplyLanguage(Enums.ALL_ENUM.LANGUAGE.VN);
        }
        public void ChangeLanguageToVietEnglish()
        {
            StringResources.ApplyLanguage(Enums.ALL_ENUM.LANGUAGE.EN);
        }
        public void MinimizedWindow()
        {
            StateWindow = WindowState.Minimized;
        }
        public void CloseWindow()
        {
            App.Current.Shutdown();
        }
        public async void SaveServerName()
        {
            await Task.Run(() =>
            {
                using (var context = new CoffeeShopContext())
                {
                    context.Database.CreateIfNotExists();
                    App.Current.Dispatcher.BeginInvoke((Action)(() => 
                    {
                        var selectDataContext = CSGlobal.Instance.InitializeWindow.DataContext as InitViewmodel;
                        if (selectDataContext != null)
                        {
                            selectDataContext.Loaded();
                        }
                    }));
                }
            });           
            CSGlobal.Instance.DisconnectedDatabaseWindow.Hide();
            CSGlobal.Instance.InitializeWindow.Show();
        }
    }
}
