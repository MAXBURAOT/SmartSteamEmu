using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace SSELauncher
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "{89bc1474-43dc-4462-8551-8f1dc280d36d}");

        public static CAppList AppList;

        [STAThread]
        private static void Main()
        {
            AppList = new CAppList(AppDomain.CurrentDomain.BaseDirectory);
            AppList.Load();

            CApp cApp = null;

            List<string> extraArgList = new List<string>();

            try
            {
                string[] commandLineArgs = Environment.GetCommandLineArgs();

                int i = 1;
                while (i < commandLineArgs.Length)
                {
                    if (commandLineArgs[i] == "-appid" && ++i < commandLineArgs.Length)
                    {
                        int AppID = Int32.Parse(commandLineArgs[i]);

                        cApp = AppList.GetItems().Find(x => x.AppId == AppID);

                        if (cApp == null)
                        {
                            MessageBox.Show(AppID.ToString(), "Unable to find appid");
                            break;
                        }
                    }
                    else if (cApp != null)
                    {
                        extraArgList.Add(commandLineArgs[i]);
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed parsing parameter");
            }

            if (cApp != null)
            {
                string args = "";

                foreach (var arg in extraArgList)
                {
                    args += " " + arg;
                }

                FrmMain.WriteIniAndLaunch(cApp, AppList.GetConfig(), args);

                return;
            }


            if (Program.mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());

                AppList.Save();
                mutex.ReleaseMutex();

                return;
            }

            NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
