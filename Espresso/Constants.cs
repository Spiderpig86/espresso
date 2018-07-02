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

        private static IList<int> _durationMins = new List<int>() {
            -1, 5, 10, 15, 30, 60, 180, 360
        };

        public static IList<int> DurationMins {
            get {
                return _durationMins;
            }

            // TODO: Set
        }
    }
}
