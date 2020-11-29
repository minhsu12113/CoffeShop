using CoffeShop.Viewmodel.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Utility
{
    public class SizeManager : BindableBase
    {
        #region [Implementations Singelton]
        private SizeManager()
        {
            // Resolution Change
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }
        private static object _lock = new object();
        private static SizeManager _instance;
        public static SizeManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SizeManager();
                        _instance.Init();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region [Fonsize Common]
        private double _fontSize34;
        private double _fontSize32;
        private double _fontSize24;
        private double _fontSize18;
        private double _fontSize16;
        private double _fontSize14;





        public double FontSize24
        {
            get { return _fontSize24; }
            set { _fontSize24 = value; OnPropertyChanged(); }
        }
        public double FontSize34
        {
            get { return _fontSize34; }
            set { _fontSize34 = value; OnPropertyChanged(); }
        }
        public double FontSize32
        {
            get { return _fontSize32; }
            set { _fontSize32 = value; OnPropertyChanged(); }
        }
        public double FontSize14
        {
            get { return _fontSize14; }
            set { _fontSize14 = value; OnPropertyChanged(); }
        }
        public double FontSize16
        {
            get { return _fontSize16; }
            set { _fontSize16 = value; OnPropertyChanged(); }
        }
        public double FontSize18
        {
            get { return _fontSize18; }
            set { _fontSize18 = value; OnPropertyChanged(); }
        }
        #endregion

        #region [ConfirmUC]
        private double _widthConfirmUC;
        private double _heightConfirmUC;
        private double _heightBtnYesConfirmUC;
        


        public double WidthConfirmUC 
        {
            get { return _widthConfirmUC; }
            set { _widthConfirmUC = value; OnPropertyChanged(); }
        }
        public double HeightConfirmUC 
        {
            get { return _heightConfirmUC; }
            set { _heightConfirmUC = value; OnPropertyChanged(); }
        }
        public double HeightBtnYesConfirmUC 
        {
            get { return _heightBtnYesConfirmUC; }
            set { _heightBtnYesConfirmUC = value; OnPropertyChanged(); }
        }
        #endregion

        #region [Init Window]
        private double _widthInitWindow;
        private double _heightInitWindow;


        public double WidthInitWindow 
        {
            get => _widthInitWindow;
            set { _widthInitWindow = value; OnPropertyChanged(); }
        }
        public double HeightInitMainWindow 
        {
            get => _heightInitWindow;
            set { _heightInitWindow = value; OnPropertyChanged(); }
        }
        #endregion

        #region [DisconnectedDatabase Window]
        private double _widthDisconnectedDatabaseWindow;
        private double _heightDisconnectedDatabaseWindow;

        public double WidthDisconnectedDatabaseWindow
        {
            get { return _widthDisconnectedDatabaseWindow;  }
            set { _widthDisconnectedDatabaseWindow = value; OnPropertyChanged(); }
        }
        public double HeightDisconnectedDatabaseWindow
        {
            get { return _heightDisconnectedDatabaseWindow; }
            set { _heightDisconnectedDatabaseWindow = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Login Window]
        private double _widthLoginWindow;
        private double _heightLoginWindow;

        public double WidthLoginWindow
        {
            get { return _widthLoginWindow; }
            set { _widthLoginWindow = value; OnPropertyChanged(); }
        }
        public double HeightLoginWindow
        {
            get { return _heightLoginWindow; }
            set { _heightLoginWindow = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Main Window]
        private double _widthMainWindow;
        private double _heightMainWindow;
        private double _heightIconNavigate;
        private double _widthIconNavigate;
        private double _heightBtnNavigate;




        public double HeightIconNavigate
        {
            get { return _heightIconNavigate; }
            set { _heightIconNavigate = value; OnPropertyChanged(); }
        }
        public double WidthIconNavigate
        {
            get { return _widthIconNavigate; }
            set { _widthIconNavigate = value; OnPropertyChanged(); }
        }
        public double WidthMainWindow
        {
            get { return _widthMainWindow; }
            set { _widthMainWindow = value; OnPropertyChanged(); }
        }
        public double HeightMainWindow
        {
            get { return _heightMainWindow; }
            set { _heightMainWindow = value; OnPropertyChanged(); }
        }
        public double HeightBtnNavigate
        {
            get { return _heightBtnNavigate; }
            set { _heightBtnNavigate = value; OnPropertyChanged(); }
        }

        #endregion

        #region [WarningUC]
        private double _widthWarningUC;
        private double _heightWarningUC;



        public double HeightWarningUC
        {
            get { return _heightWarningUC; }
            set { _heightWarningUC = value; OnPropertyChanged(); }
        }
        public double WidthWarningUC
        {
            get { return _widthWarningUC; }
            set { _widthWarningUC = value; OnPropertyChanged(); }
        }
        #endregion

        #region [ErrorNotifyDialogUC]
        private double _heightErrorNotifyDialogUC;
        private double _widthErrorNotifyDialogUC;
        private double _widthBtnErrorNotifyDialogUC;





        public double HeightErrorNotifyDialogUC
        {
            get { return _heightErrorNotifyDialogUC; }
            set { _heightErrorNotifyDialogUC = value; OnPropertyChanged(); }
        }
        public double WidthErrorNotifyDialogUC
        {
            get { return _widthErrorNotifyDialogUC; }
            set { _widthErrorNotifyDialogUC = value; OnPropertyChanged(); }
        }
        public double WidthBtnErrorNotifyDialogUC
        {
            get { return _widthBtnErrorNotifyDialogUC; }
            set { _widthBtnErrorNotifyDialogUC = value; OnPropertyChanged(); }
        }
        #endregion

        public void Init()
        {
            // Claculator On 1920x1080
            CSGlobal.Instance.CurrentHeightScreen = System.Windows.SystemParameters.PrimaryScreenHeight;
            CSGlobal.Instance.CurrentWidthScreen = System.Windows.SystemParameters.PrimaryScreenWidth;
            
            FontSize34 = CSGlobal.Instance.CurrentWidthScreen / 56.47058823529412;
            FontSize32 = CSGlobal.Instance.CurrentWidthScreen / 60;
            FontSize24 = CSGlobal.Instance.CurrentWidthScreen / 80;
            FontSize18 = CSGlobal.Instance.CurrentWidthScreen / 106.6666666666667;
            FontSize16 = CSGlobal.Instance.CurrentWidthScreen / 120;
            FontSize14 = CSGlobal.Instance.CurrentWidthScreen / 137.1428571428571;

            HeightErrorNotifyDialogUC = CSGlobal.Instance.CurrentHeightScreen / 3.724137931034483;
            WidthErrorNotifyDialogUC = CSGlobal.Instance.CurrentWidthScreen / 3.918367346938776;
            WidthBtnErrorNotifyDialogUC = CSGlobal.Instance.CurrentWidthScreen / 24.30379746835443;


            HeightWarningUC = CSGlobal.Instance.CurrentHeightScreen / 3.724137931034483;
            WidthWarningUC = CSGlobal.Instance.CurrentWidthScreen / 4.923076923076923;


            HeightInitMainWindow = CSGlobal.Instance.CurrentHeightScreen / 1.6875;
            WidthInitWindow = CSGlobal.Instance.CurrentWidthScreen / 2.865671641791045;

            HeightLoginWindow = CSGlobal.Instance.CurrentHeightScreen / 1.5;
            WidthLoginWindow = CSGlobal.Instance.CurrentWidthScreen / 2.021052631578947;

            HeightMainWindow = CSGlobal.Instance.CurrentHeightScreen / 1.101379310344828;
            WidthMainWindow = CSGlobal.Instance.CurrentWidthScreen / 1.181294964028777;
            HeightIconNavigate = CSGlobal.Instance.CurrentHeightScreen / 37.24137931034483;
            WidthIconNavigate = CSGlobal.Instance.CurrentWidthScreen / 66.20689655172414;
            HeightBtnNavigate = CSGlobal.Instance.CurrentHeightScreen / 22.04081632653061;

            HeightDisconnectedDatabaseWindow = CSGlobal.Instance.CurrentHeightScreen / 1.830508474576271;
            WidthDisconnectedDatabaseWindow = CSGlobal.Instance.CurrentWidthScreen / 4.923076923076923;

            WidthConfirmUC = WidthMainWindow / 3.307692307692308;
            HeightConfirmUC = HeightMainWindow / 3.545454545454545;
            HeightBtnYesConfirmUC = HeightConfirmUC / 5.945945945945946;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
             Init();
        }
    }
}
