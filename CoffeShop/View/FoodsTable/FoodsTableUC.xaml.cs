using CoffeShop.Viewmodel.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeShop.View.FoodsTable
{
    /// <summary>
    /// Interaction logic for FoodsTableUC1.xaml
    /// </summary>
    public partial class FoodsTableUC : UserControl
    {
        public FoodsTableUC()
        {
            InitializeComponent();
            DataContext = new FoodsTebleViewModel();
        }
    }
}
