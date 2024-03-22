using System;
using System.Windows.Forms;

namespace System_For_AnalyzingProject
{
    internal static class Program
    {
        /// <summary> Главная точка входа для приложени </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
        }
    }
}