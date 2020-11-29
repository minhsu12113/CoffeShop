using CoffeShop.Viewmodel;
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

namespace CoffeShop.View.Dialog
{
    /// <summary>
    /// Interaction logic for ErrorNotifyDialogUC.xaml
    /// </summary>
    public partial class ErrorNotifyDialogUC : UserControl
    {
        public ErrorNotifyDialogUC(ErrorNotifyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
