using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Espresso {
    /**
     * Interface for system calls for screen timeout
     */
    public static class NativeWrapper {

        [FlagsAttribute]
        public enum ExecutionState : uint {
            EsAwaymodeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }

        private static uint _previousExecutionState;

        /* Kernel function for setting execution state of sleep thread */
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(ExecutionState esFlags);

        public static uint PreventSleep() {
            uint res = SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
            if (res == 0) {
                MessageBox.Show(Constants.MSG_ERR_SETTING_STATE,
                "Espresso", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _previousExecutionState = res;
            return _previousExecutionState;
        }

        public static uint AllowSleep() {
            uint res = SetThreadExecutionState(ExecutionState.EsContinuous);
            if (res == 0) {
                MessageBox.Show(Constants.MSG_ERR_SETTING_STATE,
                "Espresso", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _previousExecutionState;
        }

        /// <summary>
        ///     Called by shutdown routine
        /// </summary>
        /// <returns></returns>
        public static uint SetPreviousState() {
            return SetThreadExecutionState((ExecutionState)_previousExecutionState);
        }
    }
}
