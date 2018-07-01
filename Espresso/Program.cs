using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Espresso {
    static class Program {

        /// <summary>
        ///     For displaying accelerator keys constantly
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam"></param>
        /// <param name="fuWinIni"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);

        private const int SPI_SETKEYBOARDCUES = 4107; // 100B
        private const int SPIF_SENDWININICHANGE = 2;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            // Only allow for one instance of this program
            bool hasBeenCreated = false;
            String mutexName = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString(); // Get the GUID of the app

            // Only spawn a single thread
            using (Mutex mutex = new Mutex(false, mutexName, out hasBeenCreated)) {
                // hasBeenCreated will be assigned true after lock is created
                if (!hasBeenCreated) {
                    // Only allow one instance
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 1, 0);

                try {
                    EspressoContext context = new EspressoContext();
                    Application.Run(context);
                } catch (Exception e) {
                    MessageBox.Show(e.Message, "Espresso Startup Error");
                }
            }

        }
    }
}
