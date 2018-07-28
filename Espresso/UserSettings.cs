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
    public class UserSettings {

        /// <summary>
        ///     Default settings
        /// </summary>
        private Duration _SleepDuration = new Duration(30);
        private bool _StartWithWindows = false;
        private bool _ActiviateOnStart = false;

        public Duration SleepDuration {
            get => _SleepDuration;
            set => _SleepDuration = value;
        }

        public bool StartWithWindows {
            get => _StartWithWindows;
            set => _StartWithWindows = value;
        }

        public bool ActivateOnStart {
            get => _ActiviateOnStart;
            set => _ActiviateOnStart = value;
        }

        public bool TrySetSleepDuration(String duration) {
            int parsedTime;
            if (Int32.TryParse(duration, out parsedTime)) {
                this.SleepDuration = new Duration(parsedTime);
                return true;
            }

            return false;
        }

        public bool TrySetStartWithWindows(String val) {
            bool parsedBool;
            if (Boolean.TryParse(val, out parsedBool)) {
                this.StartWithWindows = parsedBool;
                return true;
            }

            return false;
        }

        public bool TrySetActivateOnStart(String val) {
            bool parsedBool;
            if (Boolean.TryParse(val, out parsedBool)) {
                this.ActivateOnStart = parsedBool;
                return true;
            }

            return false;
        }
    }
}
