using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MCLauncher
{
    static class Program
    {
        public static string path = Application.StartupPath + "\\";
        public static string launcher = path + "Launcher.jar";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
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
                        RegistryKey reg     = regRoot.OpenSubKey((regRoot.GetSubKeyNames())[regRoot.GetSubKeyNames().Length - 1]);
                        string javahome     = reg.GetValue("JavaHome").ToString();
                        string path         = javahome + @"\bin\java.exe";
                        if (string.IsNullOrEmpty(javahome) || !File.Exists(path))
                        {
                            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JAVA_HOME")))
                            {
                                path         = Environment.GetEnvironmentVariable("JAVA_HOME") + @"\bin\java.exe";
                                if (!File.Exists(path))
                                {
                                    path = "java.exe";
                                }
                            }
                        }
                        java(path);
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("你的电脑可能没有安装 Java，你可以从以下地址获取 Java：\r\nhttps://cdn.moecraft.net/jre/index.html\r\n你必须安装 Java 才能运行 MoeCraft，现在打开该地址吗？\r\n\r\n" + ex.Message, "Java 错误 - MoeCraft Launcher for Windows", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            try
                            {
                                Process.Start("https://cdn.moecraft.net/jre/index.html");
                            }
                            catch (Exception pex)
                            {
                                error(pex.Message);
                            }
                        }
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
