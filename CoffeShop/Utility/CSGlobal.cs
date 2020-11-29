using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.View;
using CoffeShop.Viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CoffeShop.Utility
{
    public class CSGlobal
    {
        #region [Implementations Singelton]
        private CSGlobal() 
        {
            
        }
        private static CSGlobal _instance;
        private static object _lock = new object();


        public static CSGlobal Instance 
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        return _instance = new CSGlobal();
                    return _instance;
                }
            }
        }
        #endregion

        public UserModel CurrentUser { get; set; }
        public double CurrentWidthScreen { get; set; }
        public double CurrentHeightScreen { get; set; }
        public MainWindow MainWindow { get; set; }
        public DisconnectedDatabaseWindow DisconnectedDatabaseWindow { get; set; }
        public MainViewmodel MainViewmodel { get; set; }
        public LoginWindow LoginWindow { get; set; }
        public InitializeWindow InitializeWindow { get; set; }
        public BitmapImage NoImage()
        {
            var uri = new Uri("pack://application:,,,/CommonResources;component/Images/nothumb370x300.png");
            return new BitmapImage(uri);
        }
        public void ExitApp()
        {
            App.Current.Shutdown();
        }
    }
}
