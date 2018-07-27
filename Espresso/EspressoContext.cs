using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Espresso {

    /// <summary>
    ///     Keeps track of app state and persistence
    /// </summary>
    public class EspressoContext : ApplicationContext {

        private TrayView _trayView;

        public EspressoContext() {
            _trayView = new TrayView();
        }
    }
}
