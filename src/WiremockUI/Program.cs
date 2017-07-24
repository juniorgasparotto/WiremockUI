using System;
using System.Threading;
using System.Windows.Forms;

namespace WiremockUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Add handler to handle the exception raised by main threads
            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            //// Add handler to handle the exception raised by additional threads
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMaster());

            // Stop the application and all the threads in suspended state.
            Environment.Exit(-1);
        }

        static void Application_ThreadException
            (object sender, System.Threading.ThreadExceptionEventArgs e)
        {// All exceptions thrown by the main thread are handled over this method

            ShowExceptionDetails(e.Exception);
        }

        static void CurrentDomain_UnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {// All exceptions thrown by additional threads are handled in this method

            ShowExceptionDetails(e.ExceptionObject as Exception);

            // Suspend the current thread for now to stop the exception from throwing.
            Thread.CurrentThread.Suspend();
        }

        static void ShowExceptionDetails(Exception Ex)
        {
            // Do logging of exception details
            MessageBox.Show(Ex.Message, Ex.TargetSite.ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Environment.Exit(-1);
        }
    }
}
