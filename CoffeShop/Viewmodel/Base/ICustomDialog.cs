using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Viewmodel.Base
{
    public interface ICustomDialog
    {
        /// <summary>
        /// Is A Usercontrol Display
        /// </summary>
        object DialogContent { get; set; }
        /// <summary>
        /// Variable Open and close dialog
        /// </summary>
        bool IsOpenDialog { get; set; }
        /// <summary>
        /// Is Open Dialog
        /// </summary>
        void OpenDialog(object uc = null);
        /// <summary>
        /// Is Close Dialog
        /// </summary>
        void CloseDialog();
    }
}
