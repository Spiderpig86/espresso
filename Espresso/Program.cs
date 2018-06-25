using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Espresso {
    static class Program {
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
                    //return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

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
