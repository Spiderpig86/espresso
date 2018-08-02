using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FormBaseLib.Models {

    /// <summary>
    ///     Extended wrapper for Page
    /// </summary>
    public interface IExtendedPage {

        /// <summary>
        ///     Used for triggering refreshes when the page is shown, such as reloading settings
        /// </summary>
        void updatePageContents();

    }
}
