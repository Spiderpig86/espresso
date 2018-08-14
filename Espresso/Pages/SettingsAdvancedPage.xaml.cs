using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            
            var shell = new SysShell();
            var shortcut = GetShortcutPath(shell);
            var executable = Assembly.GetExecutingAssembly()
                                        .GetName().CodeBase;
            if (UserSettings.StartWithWindows) {
                // create shortcut in startup items folder in start menu
                shell.CreateShortcut(shortcut, executable);
            } else {
                // remove shortcut if it exists
                File.Delete(shortcut);
            }

            UserSettings.Save();
        }

        static string GetShortcutPath(SysShell shell = null) {
            if (shell == null) {
                shell = new SysShell();
            }
            var startup = shell.GetSpecialFolder("Startup");
            return Path.Combine(startup, "Espresso.lnk");
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
