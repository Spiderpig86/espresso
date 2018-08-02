using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using Espresso;

namespace Espresso.Pages {
    /// <summary>
    /// Interaction logic for SettingsBasicPage.xaml
    /// </summary>
    public partial class SettingsBasicPage : Page, FormBaseLib.Models.IExtendedPage, INotifyPropertyChanged {

        private const String PAGE_NAME = "Settings";
        private ObservableCollection<Constants.Duration> _DurationCollection;

        public ObservableCollection<Constants.Duration> DurationCollection {
            get => this._DurationCollection;
            set {
                this._DurationCollection = value;
                OnPropertyChanged("DurationCollection");
            }
        }

        public SettingsBasicPage() {
            InitializeComponent();
            this.DurationCollection = new ObservableCollection<Constants.Duration>(Constants.DurationMins); // Initialize the app durations
            this.DataContext = this; // Expose fields for property binding
            MessageBox.Show(this.comboDuration.Items.Count.ToString());
        }

        public void updatePageContents() {

        }

        #region PropertyListener
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName) {

            PropertyChangedEventHandler handler = this.PropertyChanged; // Get a reference to the current handler
            if (handler != null) {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e); // Call the handler
            }
        }
        #endregion

        public override string ToString() {
            return PAGE_NAME;
        }
    }
}
