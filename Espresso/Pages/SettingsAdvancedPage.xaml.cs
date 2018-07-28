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
        }

        public void updatePageContents() {

        }

        public override string ToString() {
            return PAGE_NAME;
        }
    }
}
