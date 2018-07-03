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
        private Timer _sleepTimer; // Keep track of duration of no sleep
        private Constants.Duration _selectedDuration; // Current selected duration
        private uint? oldState = null;

        // FORMS
        private FormBaseLib.Views.AboutView _aboutView;
        private FormBaseLib.Models.AboutViewModel _aboutViewModel;

        // TOOLSTRIP MENU ITEMS
        private MenuItem _aboutItem;
        private MenuItem _durationItem;
        private MenuItem _exitItem;
        private MenuItem _settingsItem;
        private MenuItem _toggleItem;

        private bool _isTimeoutDisabled = false; // Flag to indicate if the user has toggled timeouts. If true, screen will not sleep

        public bool IsTimeoutDisabled {
            get {
                return _isTimeoutDisabled;
            }
            set {
                _isTimeoutDisabled = value;
                _toggleItem.Checked = value;
                if (_isTimeoutDisabled) {
                    activate(this._selectedDuration);
                } else {
                    deactivate();
                }

            }
        }

        public TrayView() {

            _components = new Container();
            _notifyIcon = new NotifyIcon(_components) {
                ContextMenu = new ContextMenu(),
                Icon = Espresso.Properties.Resources.espresso_off,
                Text = "Espresso (not running)",
                Visible = true,
            };
            _sleepTimer = new Timer(_components);
            _selectedDuration = Constants.DurationMins.ElementAt(0); // Set to indefinite by default

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
        private MenuItem buildMenuItem(String displayText, String tooltipText, EventHandler eventHandler, Object tag = null) {
            MenuItem item = new MenuItem(displayText);
            if (eventHandler != null) {
                item.Click += eventHandler;
            }

            //item.Text = tooltipText;
            item.Tag = tag;
            return item;
        }

        private MenuItem buildMenuItemFromCollection<T>(String displayText, EventHandler eventHandler, IList<T> collection) {
            MenuItem item = buildMenuItem(displayText, "", null);
            item.MenuItems.AddRange(collection.Select(ele => buildMenuItem(ele.ToString(), "", eventHandler, ele)).ToArray());

            return item;
        }

        private void displayStatusMessage(String text) {
            _hiddenWindow.Dispatcher.Invoke(delegate {
                _notifyIcon.BalloonTipText = IsTimeoutDisabled ? "Screen Timeout Disabled" : "Screen Timeout Enabled";
                // The timeout is ignored on recent Windows
                _notifyIcon.ShowBalloonTip(3000);
            });
        }

        private void activate(Constants.Duration duration) {
            if (duration.Time <= 0) return;

            this._sleepTimer.Enabled = true;
            this._sleepTimer.Interval = Functions.ToMinutes(duration.Time);
            this._sleepTimer.Start();
            this.oldState = NativeWrapper.PreventSleep();

            if (this.oldState == 0) {
                MessageBox.Show("Error");
                Application.Exit();
            }

            // Update icon and text
            this._notifyIcon.Icon = Properties.Resources.espresso_on;
            this._notifyIcon.Text = "Sleep Disabled until: ";
        }

        private void deactivate() {
            this._sleepTimer.Enabled = false;
            this._sleepTimer.Stop();

            // Make sure that the thread was activated before
            if (oldState.HasValue) {
                NativeWrapper.AllowSleep();
            }

            // Update icon and text
            this._notifyIcon.Icon = Properties.Resources.espresso_off;
            this._notifyIcon.Text = "Espresso (off)";
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
                _durationItem = buildMenuItemFromCollection("Select Duration...", durationItem_Click, Constants.DurationMins);
                _exitItem = buildMenuItem("E&xit", "Exits System Tray App", exitItem_Click);
                _settingsItem = buildMenuItem("Se&ttings...", "Open app settings", settingsItem_Click);
                _toggleItem = buildMenuItem(IsTimeoutDisabled ? "Disable &Sleep" : "Enable &Sleep", "Toggle Screen Sleep", toggleItem_Click);

                _notifyIcon.ContextMenu.MenuItems.AddRange(new MenuItem[]{
                    _durationItem,
                    new MenuItem("-"),
                    _aboutItem,
                    _toggleItem,
                    _settingsItem,
                    _exitItem
                });

                // Select current duration
                foreach (MenuItem item in _durationItem.MenuItems)
                    if (((Constants.Duration) item.Tag).Time == _selectedDuration.Time) {
                        item.Checked = true;
                    } else {
                        item.Checked = false;
                    }
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
            IsTimeoutDisabled = !IsTimeoutDisabled; // Update properties
        }

        private void durationItem_Click(object sender, EventArgs e) {
            Constants.Duration duration = (Constants.Duration) ((MenuItem) sender).Tag;
            this._sleepTimer.Stop();
            this._sleepTimer.Interval = Functions.ToMinutes(duration.Time);
            this._sleepTimer.Start();

        }

        /// <summary>
        ///     Run once after timer reaches specified duration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e) {
            deactivate();
        }
    }

}
