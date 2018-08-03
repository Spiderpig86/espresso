using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Espresso {

    /// <summary>
    ///     Hold app constants
    /// </summary>
    public static class Constants {

        /// <summary>
        ///     Path for config app config folder
        /// </summary>
        public static String AppConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Espresso_Config";
        public static String AppSettingsFile = "espresso_prefs.ini";
        public static String AppSettingsFilePath = AppConfigFolder + "/" + AppSettingsFile;

        /* App string constants */
        public static String MSG_ERR_FILESYS_TITLE = "Filesystem Error";
        public static String MSG_ERR_CREATE_DIR = "Error creating directory at {0}. Please try again";
        public static String MSG_ERR_CREATE_FILE = "Error creating file {0}. Please try again";
        public static String MSG_ERR_SAVE_SETTINGS = "Error saving user settings, please ensure that you have access to " + AppConfigFolder;

        public static String APP_DEFAULT_SETTINGS_HEADER = @"
' Espresso Preferences File
' It is not recommended that you manually edit this file.

[Preferences]";

        public static String APP_DEFAULT_SETTINGS = @"
' Espresso Preferences File
' It is not recommended that you manually edit this file.

[Preferences]

WakeDuration=30
StartWithWindows=false
ActivateOnStart=false
";

        /// <summary>
        ///     Wrapper for time
        /// </summary>
        public class Duration {
            public int Time { get; set; }
            public Duration(int time) { this.Time = time; }
            public String Description {
                get {
                    return Functions.MinutesToDesc(this.Time);
                }
            }

            public override string ToString() {
                // Needed for our blackbox function (infers that toString() gets the description)
                return this.Description;
            }
        }

        private static List<Duration> _durationMins = new List<Duration>() {
            new Duration(-1),

            // Minutes Menu Items
            new Duration(5),
            new Duration(10),
            new Duration(15),
            new Duration(30),
            new Duration(45),

            // Hours Menu Items
            new Duration(60),
            new Duration(120),
            new Duration(180),
            new Duration(480),
            new Duration(720)
        };

        public static List<Duration> DurationMins {
            get {
                return _durationMins;
            }

            set {
                _durationMins = value;
            }
        }

        /**
         * Collection methods
         */
        public static bool AddDuration(Duration dur) {
            if (_durationMins.Contains(dur))
                return false;

            _durationMins.Add(dur);
            return true;
        }
    }
}
