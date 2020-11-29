using CoffeShop.Internationalization;
using CoffeShop.Viewmodel.Base;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static CoffeShop.Enums.ALL_ENUM;

namespace CoffeShop.Model.UI
{
    public class ItemNavigate : BindableBase
    {
        private string _content;
        private PackIconKind _icon;
        private Brush _backGround;
        private TYPE_VIEW _typeView;
        private Brush _foregroundItem;
        private Visibility _statePointer;






        public Visibility StatePointer 
        {
            get { return _statePointer; }
            set { _statePointer = value; OnPropertyChanged(); }
        }
        public Brush ForegroundItem
        {
            get { return _foregroundItem; }
            set { _foregroundItem = value; OnPropertyChanged(); }
        }
        public TYPE_VIEW TypeView
        {
            get { return _typeView; }
            set { _typeView = value; OnPropertyChanged(); }
        }
        public Brush BackgoundItem
        {
            get { return _backGround; }
            set { _backGround = value; OnPropertyChanged(); }
        }
        public PackIconKind Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }

        public static List<ItemNavigate> ListItemNavigate = new List<ItemNavigate>()
        {
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.HOME,
                    Content = StringResources.Find("HOME"),
                    Icon = PackIconKind.Home,
                    BackgoundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(51, 38, 174)),
                    StatePointer = Visibility.Visible
                },
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.DASHBOARD,
                    Content = StringResources.Find("DASHBOARD"),
                    Icon = PackIconKind.TabletDashboard,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                },                
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.CATEGORY,
                    Content = StringResources.Find("CATEGORY"),
                    Icon = PackIconKind.ShapeOutline,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                },
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.FOODS,
                    Content = StringResources.Find("FOODS"),
                    Icon = PackIconKind.Food,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                },
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.STATISTICS,
                    Content = StringResources.Find("STATISTICS"),
                    Icon = PackIconKind.ChartBoxOutline,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                },
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.USER,
                    Content = StringResources.Find("USER"),
                    Icon = PackIconKind.AccountBoxOutline,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                },
                new ItemNavigate()
                {
                    TypeView = TYPE_VIEW.SETTING,
                    Content = StringResources.Find("SETTING"),
                    Icon = PackIconKind.Settings,
                    BackgoundItem = new SolidColorBrush(Colors.Transparent),
                    ForegroundItem = new SolidColorBrush(Color.FromRgb(255,255,255)),
                    StatePointer = Visibility.Collapsed
                }
        };
    }
}
