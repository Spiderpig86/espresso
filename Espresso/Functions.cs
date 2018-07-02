using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
