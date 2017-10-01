using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;

namespace MCLauncher
{
    static class Program
    {
        public static string path = Application.StartupPath + "\\";
        public static string launcher = path + "Launcher.jar";
        public static IList args = (IList)Environment.GetCommandLineArgs();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            if (args.Contains("/nojava") || args.Contains("-nojava"))
            {
                Application.Run(new nojava());
                Environment.Exit(0);
            }
            if (!File.Exists(launcher))
            {
                if(File.Exists(path + "启动器.jar"))
                {
                    launcher = path + "启动器.jar";
                }
                else
                {
                    launcher = "";
                    var df = Directory.GetFiles(path);
                    foreach(string la in df)
                    {
                        var ff = new FileInfo(la);
                        if(ff.Name.Substring(0,4).ToLower() == "hmcl")
                        {
                            launcher = la;
                            break;
                        }
                    }
                    if(string.IsNullOrEmpty(launcher))
                    {
                        error("找不到启动器文件\r\nLauncher.jar 或 启动器.jar 或 HMCL*.jar\r\n\r\n请打开更新器更新 MoeCraft");
                    }
                }
            }
            else
            {
                try
                {
                    java("java.exe");
                }
                catch (Exception)
                {
                    try
                    {
                        RegistryKey regRoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                        Version javaver     = new Version(regRoot.GetValue("CurrentVersion").ToString());
                        if(!args.Contains("/allowj7") && !args.Contains("-allowj7") && javaver <= new Version("1.7"))
                        {
                            Application.Run(new nojava());
                            Environment.Exit(4);
                        }
                        RegistryKey reg     = regRoot.OpenSubKey((regRoot.GetSubKeyNames())[regRoot.GetSubKeyNames().Length - 1]);
                        string javahome     = reg.GetValue("JavaHome").ToString();
                        string path         = javahome + @"\bin\java.exe";
                        java(path);
                    }
                    catch (Exception ex)
                    {
                        Application.Run(new nojava());
                        Environment.Exit(3);
                    }
                }
            }
        }

        private static void java(string v)
        {
            var ps = new Process();
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.Arguments = "-jar \"" + launcher + "\" ";
            ps.StartInfo.FileName = v;
            Application.Run(new logger(ps));
        }

        public static void error(string msg, string title = "错误 - MoeCraft Launcher for Windows")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
