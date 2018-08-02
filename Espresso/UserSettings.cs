using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Espresso.Constants;

namespace Espresso {

    /// <summary>
    ///     Manage application settings
    /// </summary>
    public static class UserSettings {

        /// <summary>
        ///     Default settings
        /// </summary>
        private static Duration _SleepDuration = new Duration(30);
        private static bool _StartWithWindows = false;
        private static bool _ActiviateOnStart = false;

        public static Duration SleepDuration {
            get => _SleepDuration;
            set => _SleepDuration = value;
        }

        public static bool StartWithWindows {
            get => _StartWithWindows;
            set => _StartWithWindows = value;
        }

        public static bool ActivateOnStart {
            get => _ActiviateOnStart;
            set => _ActiviateOnStart = value;
        }

        public static bool TrySetSleepDuration(String duration) {
            int parsedTime;
            if (Int32.TryParse(duration, out parsedTime)) {
               SleepDuration = new Duration(parsedTime);
                return true;
            }

            return false;
        }

        public static bool TrySetStartWithWindows(String val) {
            bool parsedBool;
            if (Boolean.TryParse(val, out parsedBool)) {
                StartWithWindows = parsedBool;
                return true;
            }

            return false;
        }

        public static bool TrySetActivateOnStart(String val) {
            bool parsedBool;
            if (Boolean.TryParse(val, out parsedBool)) {
                ActivateOnStart = parsedBool;
                return true;
            }

            return false;
        }

        #region Settings IO
        /// <summary>
        ///     Parse settings from file
        /// </summary>
        /// <returns>
        ///     Return success status
        /// </returns>
        public static bool ParseSettings() {

            return true;
        }

        public static bool SaveSettings() {
            StringBuilder sb = new StringBuilder();
            sb.Append(APP_DEFAULT_SETTINGS_HEADER);
            sb.AppendLine(nameof(SleepDuration) + "=" + SleepDuration.Time.ToString());
            sb.AppendLine(nameof(StartWithWindows) + "=" + StartWithWindows.ToString());
            sb.Append(nameof(ActivateOnStart) + "= " + ActivateOnStart.ToString());

            String preferenceFilePath = Constants.AppConfigFolder + @"\" + Constants.AppSettingsFile;

            try {
                File.WriteAllText(preferenceFilePath, Constants.APP_DEFAULT_SETTINGS);
            } catch (Exception e) {
                return false;
            }

            return true;
        }
        #endregion
    }
}
