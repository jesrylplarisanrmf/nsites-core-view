using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NSites_V.ApplicationObjects.UserInterfaces;

namespace FrameWork
{
    static class ApplicationObjectsStarter
    {
        /// <summary>
        /// The main entry point for the ApplicationObjects.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInUI());
        }
    }
}
