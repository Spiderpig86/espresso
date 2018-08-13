using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Espresso.Views {
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : Window {
        public AboutView() {
            InitializeComponent();
        }

        private void ReportLabel_MouseUp(object sender, MouseButtonEventArgs e) {
            Process.Start("https://github.com/Spiderpig86/espresso/issues");
        }

        private void SourceLabel_MouseUp(object sender, MouseButtonEventArgs e) {
            Process.Start("https://github.com/Spiderpig86/espresso");
        }
    }
}
