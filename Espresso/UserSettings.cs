using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
