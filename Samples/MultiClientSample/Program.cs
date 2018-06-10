using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiClientSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            inf = new InstancesForm();
            Application.Run(inf);
        }
        public static InstancesForm inf;
    }
}
