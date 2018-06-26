using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormBaseLib.Models {
    public class AboutViewModel : ModelBase {

        public AboutViewModel() {
            // TODO: Get year for copyright
        }

        private System.Windows.Media.ImageSource _icon;

        public System.Windows.Media.ImageSource Icon {
            get {
                return _icon;
            }
            set {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }

    }
}
