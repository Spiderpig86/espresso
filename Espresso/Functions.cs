using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Espresso {
    /// <summary>
    ///     Helper functions
    /// </summary>
    public static class Functions {

        public static String MinutesToDesc(int minutes) {
            if (minutes == -1)
                return "Constant";

            int mins = minutes % 60;
            if (mins != 0)
                return String.Format("{0} minutes", mins);
            else {
                // Note: Expecting whole hours here
                int hours = minutes / 60;
                return String.Format("{0} " + ((hours > 1) ? "hours" : "hour"), hours);
            }
        }

        public static int ToMinutes(int minutes) {
            return minutes * 60 * 1000;
        }

        #region Startup Functions

        /// <summary>
        ///     Initializes app directory for app settings and other files
        /// </summary>
        /// <returns>
        ///     If the files were created successfully
        /// </returns>
        public static Boolean CreateAppDirs() {

            if (!Directory.Exists(Constants.AppConfigFolder)) {
                try {
                    Directory.CreateDirectory(Constants.AppConfigFolder);
                } catch (Exception e) {
                    MessageBox.Show(Constants.MSG_ERR_FILESYS_TITLE, String.Format(Constants.MSG_ERR_CREATE_DIR, Constants.AppConfigFolder));
                    return false;
                }
            }

            if (!File.Exists(Constants.AppConfigFolder + @"\" + Constants.AppSettingsFile)) {

            }

            return true;
        }
        #endregion
    }

}
