using System;
using System.Windows.Forms;
using NarlonLib.Log;

namespace TaleofMonsters
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            NLog.SetRemote("127.0.0.1", 5501);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}