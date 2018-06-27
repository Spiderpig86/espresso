using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormBaseLib.Models {
    public class AboutViewModel : ModelBase {

        public AboutViewModel() {
            // Get program info
            _versionInfo = new System.Collections.ObjectModel.ObservableCollection<AppInfoWrapper>();
        }

        public void AddAppInfo(string name, string val) {
            foreach (var item in VersionInfo) {
                if (item.Name == name) {
                    item.Value = val;
                    OnPropertyChanged("VersionInfo");
                    return;
                }
            }

            AppInfoWrapper info = new AppInfoWrapper();
            info.Name = name;
            info.Value = val;
            VersionInfo.Add(info);
        }

        #region Properties

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

        private System.Collections.ObjectModel.ObservableCollection<AppInfoWrapper> _versionInfo;

        public System.Collections.ObjectModel.ObservableCollection<AppInfoWrapper> VersionInfo {
            get {
                return _versionInfo;
            }
            set {
                _versionInfo = value;
                OnPropertyChanged("VersionInfo");
            }
        }

        public String AppName {
            get {
                return Array.Find(VersionInfo.ToArray(), entry => entry.Name == "name").Value;
            }
        }

        public String AppVersion {
            get {
                return Array.Find(VersionInfo.ToArray(), entry => entry.Name == "version").Value;
            }
        }

        public String Copyright {
            get {
                return "Copyright  " + new DateTime().Year + " Stanley Lim";
            }
        }

        #endregion
    }
}
