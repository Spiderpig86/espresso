using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Espresso {
    /**
     * Interface for system calls for screen timeout
     */
    public static class NativeWrapper {

        [FlagsAttribute]
        private enum ExecutionState : uint {
            EsAwaymodeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }

        /* Kernel function for setting execution state of sleep thread */
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(ExecutionState esFlags);

        public static uint PreventSleep() {
            return SetThreadExecutionState(ExecutionState.EsContinuous | ExecutionState.EsSystemRequired);
        }

        public static uint AllowSleep() {
            return SetThreadExecutionState(ExecutionState.EsContinuous);
        }
    }
}
