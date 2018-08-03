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
        private static Duration _WakeDuration = new Duration(30);
        private static bool _StartWithWindows = false;
        private static bool _ActiviateOnStart = false;

        public static Duration WakeDuration {
            get => _WakeDuration;
            set {
                _WakeDuration = value;
            }

        }

        public static bool StartWithWindows {
            get => _StartWithWindows;
            set {
                _StartWithWindows = value;
            }
        }

        public static bool ActivateOnStart {
            get => _ActiviateOnStart;
            set {
                _ActiviateOnStart = value;
            }
        }

        public static int TryParseInt(String duration, Int32 defaultVal) {
            int parsedInt = defaultVal;
            Int32.TryParse(duration, out parsedInt);

            return parsedInt;
        }

        public static bool TryParseBool(String val, bool defaultVal) {
            bool parsedBool = defaultVal;
            Boolean.TryParse(val, out parsedBool);

            return parsedBool;
        }

        #region Settings IO
        /// <summary>
        ///     Parse settings from file
        /// </summary>
        /// <returns>
        ///     Return success status
        /// </returns>
        public static bool Load() {
            if (!File.Exists(Constants.AppSettingsFilePath))
                return false;
        
            try {
                using (StreamReader sr = new StreamReader(Constants.AppSettingsFilePath)) {
                    while (sr.Peek() >= 0) {
                        ExamineLine(sr.ReadLine());
                    }
                }
            } catch (Exception e) {
                return false;
            }

            return true;
        }

        public static void ExamineLine(String preference) {
            if (preference.StartsWith(nameof(WakeDuration))) {
                WakeDuration = new Duration(TryParseInt(preference.Split('=')[1], WakeDuration.Time));
            } else if (preference.StartsWith(nameof(StartWithWindows))) {
                StartWithWindows = TryParseBool(preference.Split('=')[1], StartWithWindows);
            }  else if (preference.StartsWith(nameof(ActivateOnStart))) {
                ActivateOnStart = TryParseBool(preference.Split('=')[1], ActivateOnStart);
            }
        }

        public static bool Save() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(APP_DEFAULT_SETTINGS_HEADER);
            sb.AppendLine(nameof(WakeDuration) + "=" + WakeDuration.Time.ToString());
            sb.AppendLine(nameof(StartWithWindows) + "=" + StartWithWindows.ToString());
            sb.AppendLine(nameof(ActivateOnStart) + "=" + ActivateOnStart.ToString());

            String preferenceFilePath = Constants.AppSettingsFilePath;

            try {
                File.WriteAllText(preferenceFilePath, sb.ToString());
            } catch (Exception e) {
                MessageBox.Show(Constants.MSG_ERR_SAVE_SETTINGS);
                return false;
            }

            return true;
        }
        #endregion
    }
}
