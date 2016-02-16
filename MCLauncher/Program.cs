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
                error("找不到启动器文件：" + launcher + "\r\n请打开更新器更新 MoeCraft");
            }
            else
            {
                try
                {
                    RegistryKey regRoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                    RegistryKey reg     = regRoot.OpenSubKey((regRoot.GetSubKeyNames())[regRoot.GetSubKeyNames().Length - 1]);
                    string javahome     = reg.GetValue("JavaHome").ToString();
                    string path         = javahome + @"\bin\javaw.exe";
                    if (javahome == null || !File.Exists(path))
                    {
                        path = "javaw.exe";
                    }
                    Process.Start(path, "-jar \"" + launcher + "\" ");
                }
                catch(Exception ex)
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

        public static void error(string msg, string title = "错误 - MoeCraft Launcher for Windows")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
