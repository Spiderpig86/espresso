using FormBaseLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Espresso {

    /// <summary>
    ///     Tray icon that displays in the taskbar
    /// </summary>
    public class TrayView {

        // Hidden window for running code on GUI thread
        private System.Windows.Window _hiddenWindow;

        private NotifyIcon _notifyIcon; // The tray display
        private IContainer _components; // For grouping components

        // FORMS
        private FormBaseLib.Views.AboutView _aboutView;
        private FormBaseLib.Models.AboutViewModel _aboutViewModel;

        // TOOLSTRIP MENU ITEMS
        private MenuItem _aboutItem;
        private MenuItem _durationItem;
        private MenuItem _exitItem;
        private MenuItem _settingsItem;
        private MenuItem _toggleItem;

        private bool isTimeoutDisabled = false; // Flag to indicate if the user has toggled timeouts. If true, screen will not sleep

        public TrayView() {

            _components = new Container();
            _notifyIcon = new NotifyIcon(_components) {
                ContextMenu = new ContextMenu(),
                Icon = Espresso.Properties.Resources.NotReadyIcon,
                Text = "Espresso (not running)",
                Visible = true,
            };

            // Hook events
            _notifyIcon.ContextMenu.Popup += ContextMenu_Opening;

            // Init models
            _aboutViewModel = new FormBaseLib.Models.AboutViewModel();

            _hiddenWindow = new System.Windows.Window();
            _hiddenWindow.Hide();
        }

        System.Windows.Media.ImageSource AppIcon {
            get {
                System.Drawing.Icon icon = Properties.Resources.espresso_100_ico;
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
        }

        /// <summary>
        ///     Construct toolstrip menu item with handler
        /// </summary>
        /// <param name="displayText">
        ///     Tex to display
        /// </param>
        /// <param name="tooltipText">
        ///     Hover tooltip text
        /// </param>
        /// <param name="eventHandler">
        ///     Event callback
        /// </param>
        /// <returns>
        ///     Constructed toolstrip menu item
        /// </returns>
        private MenuItem buildMenuItem(String displayText, String tooltipText, EventHandler eventHandler) {
            MenuItem item = new MenuItem(displayText);
            if (eventHandler != null) {
                item.Click += eventHandler;
            }

            //item.Text = tooltipText;
            return item;
        }

        private MenuItem buildMenuItemFromCollection<T>(String displayText, EventHandler eventHandler, IList<T> collection) {
            MenuItem item = buildMenuItem(displayText, "", null);
            item.MenuItems.AddRange(collection.Select(ele => buildMenuItem(ele.ToString(), "", eventHandler)).ToArray());

            return item;
        }

        private void displayStatusMessage(String text) {
            _hiddenWindow.Dispatcher.Invoke(delegate {
                _notifyIcon.BalloonTipText = isTimeoutDisabled ? "Screen Timeout Disabled" : "Screen Timeout Enabled";
                // The timeout is ignored on recent Windows
                _notifyIcon.ShowBalloonTip(3000);
            });
        }

        /**
         * EVENT HANDLERS
         */
        /// <summary>
        ///     Populate context menu on each open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Opening(Object sender, EventArgs e) {

            // Populate list if empty
            if (_notifyIcon.ContextMenu.MenuItems.Count == 0) {
                _aboutItem = buildMenuItem("&About...", "About the app", aboutItem_Click);
                _durationItem = buildMenuItemFromCollection("Select Duration...", null, Constants.DurationMins);
                _exitItem = buildMenuItem("E&xit", "Exits System Tray App", exitItem_Click);
                _settingsItem = buildMenuItem("Se&ttings...", "Open app settings", settingsItem_Click);
                _toggleItem = buildMenuItem(isTimeoutDisabled ? "Disable &Sleep" : "Enable &Sleep", "Toggle Screen Sleep", toggleItem_Click);

                _notifyIcon.ContextMenu.MenuItems.AddRange(new MenuItem[]{
                    _aboutItem,
                    _toggleItem,
                    _exitItem
                });
            }
        }

        private void aboutItem_Click(object sender, EventArgs e) {
            if (_aboutView == null) {
                _aboutView = new FormBaseLib.Views.AboutView();
                _aboutView.DataContext = _aboutViewModel;
                _aboutView.Closing += ((sdr, args) => _aboutView = null); // Destroy on close
                _aboutView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

                _aboutViewModel.AddAppInfo("name", Assembly.GetExecutingAssembly().GetName().Name);
                _aboutViewModel.AddAppInfo("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                _aboutView.Icon = AppIcon;

                _aboutView.Show();
            } else
                _aboutView.Activate();

            _aboutView.Icon = AppIcon;
        }

        private void exitItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void settingsItem_Click(object sender, EventArgs e) {

        }

        private void toggleItem_Click(object sender, EventArgs e) {
            if (isTimeoutDisabled) {
                this.isTimeoutDisabled = false; // Enable timeout again
                NativeWrapper.AllowSleep();
            } else {
                this.isTimeoutDisabled = true;
                NativeWrapper.PreventSleep();
            }
        }

    }

}
