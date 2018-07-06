using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace MCLauncher
{
    static class Program
    {
        const string MinJavaVer = "1.8";

        public static string path = Application.StartupPath + "\\";
        public static string launcher = path + "Launcher.jar";
        public static IList args = (IList)Environment.GetCommandLineArgs();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (args.Contains("/updateHMCLConfig") || args.Contains("-updateHMCLConfig"))
            {
                if(File.Exists("hmcl.json") && File.Exists("updater/paths/mcjar.txt"))
                {
                    try
                    {
                        string json = File.ReadAllText("hmcl.json");
                        Match target = Regex.Match(json, @"""name"": ""MoeCraft-Auto"",([\s\S]*?)""gameDir", RegexOptions.IgnoreCase);
                        string replacement = Regex.Replace(target.Value, @"""selectedMinecraftVersion"": ""(.*?)""", @"""selectedMinecraftVersion"": """ + File.ReadAllText("updater/paths/mcjar.txt") + @"""", RegexOptions.IgnoreCase);
                        string result = json.Substring(0, target.Index) + replacement + json.Substring(target.Index + target.Length);
                        File.WriteAllText("hmcl.json", result);
                    }
                    catch(Exception ex)
                    {
                        error(ex.ToString(), "更新启动器配置文件失败");
                        Environment.Exit(1);
                    }
                    Environment.Exit(0); //DONE
                }
                else
                {
                    Environment.Exit(3); //HMCL JSON NOT EXISTS
                }
            }
            Application.EnableVisualStyles();
            Thread WaitingForm = new Thread(() =>
            {
                Application.Run(new waiting());
            });
            WaitingForm.Start();
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
                        error("找不到启动器文件\r\nLauncher.jar 或 启动器.jar 或 HMCL*.jar\r\n\r\n请打开 MoeCraft Toolbox 更新 MoeCraft");
                        Environment.Exit(4);
                    }
                }
            }
            try
            {
                Version JavaVer1 = null;
                Version JavaVer2 = null;
                Version JavaTrueVer = null;
                string JavaTrueRegPath = null;
                try
                {
                    RegistryKey JavaRegRoot1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                    if (JavaRegRoot1 != null)
                    {
                        var JavaVerKey1 = JavaRegRoot1.GetValue("CurrentVersion");
                        if (JavaVerKey1 != null)
                        {
                            JavaVer1 = new Version(JavaVerKey1.ToString());
                        }
                    }
                }
                catch (Exception) { }
                try
                {
                    RegistryKey JavaRegRoot2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\JRE");
                    if (JavaRegRoot2 != null)
                    {
                        var JavaVerKey2 = JavaRegRoot2.GetValue("CurrentVersion");
                        if (JavaVerKey2 != null)
                        {
                            JavaVer2 = new Version(JavaVerKey2.ToString());
                        }
                    }
                }
                catch (Exception) { }
                if (JavaVer1 != null && (JavaVer2 == null || JavaVer1 >= JavaVer2))
                {
                    JavaTrueVer = JavaVer1;
                    JavaTrueRegPath = "Java Runtime Environment";
                }
                else if (JavaVer2 != null && (JavaVer1 == null || JavaVer1 <= JavaVer2))
                {
                    JavaTrueVer = JavaVer2;
                    JavaTrueRegPath = "JRE";
                }
                if (JavaTrueVer == null)
                {
                    Application.Run(new nojava(nojava.NoJavaMessageType.NotFound));
                    Environment.Exit(3);
                }
                if (!args.Contains("/allowold") && !args.Contains("-allowold") && JavaTrueVer < new Version(MinJavaVer))
                {
                    Application.Run(new nojava(nojava.NoJavaMessageType.TooOld));
                    Environment.Exit(4);
                }
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\JavaSoft\" + JavaTrueRegPath).OpenSubKey(JavaTrueVer.ToString());
                string javahome = reg.GetValue("JavaHome").ToString();
                string path = javahome + @"\bin\javaw.exe";
                java(path);
            }
            catch (Exception ex)
            {
                Application.Run(new nojava(nojava.NoJavaMessageType.NotFound));
                Environment.Exit(3);
            }
        }

        private static void java(string v)
        {
            try
            {
                var ps = new Process();
                ps.StartInfo.UseShellExecute = false;
                ps.StartInfo.Arguments = "-jar \"" + launcher + "\" ";
                ps.StartInfo.FileName = v;
                ps.StartInfo.RedirectStandardOutput = true;
                ps.Start();
                ps.WaitForInputIdle();
                Thread.Sleep(1111);
                Environment.Exit(0);
            } catch(Exception ex)
            {
                error("启动 Java 失败\r\n" + ex.ToString());
            }
        }

        public static void error(string msg, string title = "错误 - MoeCraft Launcher for Windows")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
