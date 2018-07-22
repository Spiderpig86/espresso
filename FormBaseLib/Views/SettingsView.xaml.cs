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

namespace FormBaseLib.Views {
    /// <summary>
    ///     Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsView : Window {

        // Store settings page instances
        private FormBaseLib.Models.IExtendedPage basicSettingsPage;
        private FormBaseLib.Models.IExtendedPage advancedSettingsPage;

        public SettingsView() {
            InitializeComponent();

            // Init pages once
            this.basicSettingsPage = new FormBaseLib.Pages.SettingsBasicPage();
            this.advancedSettingsPage = new FormBaseLib.Pages.SettingsAdvancedPage();

            // Show the initial page
            Frame.Content = this.basicSettingsPage;
        }

        /// <summary>
        ///     Trigger update for page contents of application settings
        /// </summary>
        /// <param name="page">
        ///     The page that we want to update
        /// </param>
        private void updateSettings(FormBaseLib.Models.IExtendedPage page) {
            page.updatePageContents();
        }


        #region "Event Handlers"

        private void BasicSettingsButton_Click(object sender, RoutedEventArgs e) {
            switchPage(this.basicSettingsPage);
        }

        private void AdvancedSettingsButton_Click(object sender, RoutedEventArgs e) {
            switchPage(this.advancedSettingsPage);
        }

        /// <summary>
        ///     Helper function for switching page views
        /// </summary>
        /// <param name="page">
        ///     The page to switch to 
        /// </param>
        private void switchPage(FormBaseLib.Models.IExtendedPage page) {
            Frame.Content = page;
            this.SettingsViewLabel.Content = page.ToString();
        }

        #endregion
    }
}
