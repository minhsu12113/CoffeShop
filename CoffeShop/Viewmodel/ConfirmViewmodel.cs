using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeShop.Viewmodel
{
   public class ConfirmViewmodel : BindableBase
   {
        #region [Action]
        public Action OkActionVoid { get; set; }
        public Action<object> OkActionPara { get; set; }
        public Action CalcelAction { get; set; }
        #endregion
        #region [Variable Bind]
        private Visibility _stateBtnOkVoid;
        private Visibility _stateBtnOkPara;
        private string _content;



        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
        public Visibility StateBtnOkVoid
        {
            get { return _stateBtnOkVoid; }
            set { _stateBtnOkVoid = value; OnPropertyChanged(); }
        }
        public Visibility StateBtnOkPara
        {
            get { return _stateBtnOkPara; }
            set { _stateBtnOkPara = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]
        public ICommand OkActionVoidCMD { get { return new CommandHelper(OkActionVoid); } }
        public ICommand OkActionParaCMD { get { return new CommandHelper<object>((p) => { return p != null; }, OkActionPara); } }
        public ICommand CalcelActionCMD { get { return new CommandHelper(CalcelAction); } }
        #endregion
        public ConfirmViewmodel(string content, Action callBackOk, Action callBackCacel)
        {
            Content = content;
            OkActionVoid = callBackOk.Invoke;
            CalcelAction = callBackCacel.Invoke;


            _stateBtnOkVoid = Visibility.Visible;
            _stateBtnOkPara = Visibility.Collapsed;
        }
        public ConfirmViewmodel(string content, Action<object> callBackOk, Action callBackCacel)
        {
            Content = content;
            OkActionPara = callBackOk.Invoke;
            CalcelAction = callBackCacel.Invoke;

            _stateBtnOkPara = Visibility.Visible;
            _stateBtnOkVoid = Visibility.Collapsed;
        }
   }
}
