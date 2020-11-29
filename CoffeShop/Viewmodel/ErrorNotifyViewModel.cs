using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel
{
    public class ErrorNotifyViewModel : BindableBase
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
        public Action CallBackClose { get; set; }
        public ICommand CallBackCloseCMD { get { return new CommandHelper(CallBackClose); } }

        public ErrorNotifyViewModel(string content,Action callBackClose)
        {
            Content = content;
            CallBackClose = callBackClose;
        }
    }
}
