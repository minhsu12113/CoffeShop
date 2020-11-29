using CoffeShop.DataProvider;
using CoffeShop.DBHelper;
using CoffeShop.Enums;
using CoffeShop.Internationalization;
using CoffeShop.Utility;
using CoffeShop.View;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeShop.Viewmodel
{
    public class InitViewmodel : BindableBase
    {
        public InitViewmodel()
        {
            StringResources.ApplyLanguage(ALL_ENUM.LANGUAGE.VN);
            new DisconnectedDatabaseWindow();
            Loaded();
        }
        public async void Loaded()
        {
           await Task.Run(() => 
           {
               try
               {
                   Thread.Sleep(3000);
                   string connectString = DBSetting.Instance.LoadConfig();
                   bool isConnectDB = DBSetting.Instance.CheckConnectMainDB(connectString);

                   if (isConnectDB)
                   {
                       DBSetting.Instance.MainConectString = connectString;
                       CreateAccountAdmin();
                       App.Current.Dispatcher.BeginInvoke((Action)(() =>
                       {
                           new MainWindow();
                           new LoginWindow();
                           CSGlobal.Instance.LoginWindow.Show();
                           CSGlobal.Instance.InitializeWindow.Hide();
                       }));
                   }
                   else
                   {
                       App.Current.Dispatcher.BeginInvoke((Action)(() =>
                       {
                           CSGlobal.Instance.DisconnectedDatabaseWindow.Show();
                           CSGlobal.Instance.InitializeWindow.Hide();
                       }));
                   }
               }
               catch (Exception ex)
               {


               }
           });
        }
        public async void CreateAccountAdmin()
        {
            await Task.Run(() =>
            {
                using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                {
                    unitOfWork.User.CheckAndCreateAccountAdmin();
                }
            });
        }        
    }
}
