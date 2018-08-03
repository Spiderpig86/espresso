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

namespace Espresso.Pages {
    /// <summary>
    /// Interaction logic for SettingsAdvancedPage.xaml
    /// </summary>
    public partial class SettingsAdvancedPage : Page, FormBaseLib.Models.IExtendedPage {

        private const String PAGE_NAME = "Advanced Settings";

        public SettingsAdvancedPage() {
            InitializeComponent();

            this.updatePageContents();
        }

        public void updatePageContents() {
            this.toggleActivateLaunch.IsChecked = UserSettings.ActivateOnStart;
            this.toggleWindowsStart.IsChecked = UserSettings.StartWithWindows;
        }

        private void toggleWindowsStart_Click(object sender, RoutedEventArgs e) {
            UserSettings.StartWithWindows = (bool)this.toggleWindowsStart.IsChecked;
            UserSettings.Save();
        }

        private void toggleActivateLaunch_Click(object sender, RoutedEventArgs e) {
            UserSettings.ActivateOnStart = (bool)this.toggleActivateLaunch.IsChecked;
            UserSettings.Save();
        }

        public override string ToString() {
            return PAGE_NAME;
        }
    }
}
