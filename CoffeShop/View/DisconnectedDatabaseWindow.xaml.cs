using CoffeShop.Utility;
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
using System.Windows.Shapes;

namespace CoffeShop.View
{
    /// <summary>
    /// Interaction logic for DisconnectedDatabaseWindow.xaml
    /// </summary>
    public partial class DisconnectedDatabaseWindow : Window
    {
        public DisconnectedDatabaseWindow()
        {
            InitializeComponent();
            CSGlobal.Instance.DisconnectedDatabaseWindow = this;
        }
    }
}
