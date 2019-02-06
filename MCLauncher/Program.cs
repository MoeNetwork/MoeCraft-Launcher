using MCLauncher.Library;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MCLauncher
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Thread progressThread = new Thread(() => Application.Run(new WaitingForm()));
                progressThread.Start();

                var javaResult = new JavaFinder(ApplicationEnvironment.TargetJavaVersion).FindJava();

                if (javaResult == null || !javaResult.IsSuccess)
                {
                    progressThread.Abort();
                    ShowNoJavaPromptForm(NoJavaPromptForm.NoJavaMessageType.NotFound);
                }

                if (javaResult.IsCompat)
                {
                    if (MessageBox.Show(
                            "检测到您的计算机是 64 位计算机，但只安装了 32 位版本的 Java.\r\n继续启动 MoeCraft 可能导致不可预料的故障，我们推荐您安装 64 位的 Java\r\n您想了解如何安装 64 位的 Java 吗？",
                            "Java 版本不兼容", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        progressThread.Abort();
                        ShowNoJavaPromptForm(NoJavaPromptForm.NoJavaMessageType.Incompatible);
                    }
                }

                string jarResult;

                if (args.Length >= 1 && File.Exists(args[0]))
                {
                    jarResult = args[0];
                }
                else
                {
                    jarResult = new JarFinder(ApplicationEnvironment.SearchFileOrder).find();
                }

                if (jarResult == null)
                {
                    Error("未找到符合条件的 jar，请确保你已经正确解压/安装了MoeCraft");
                    Environment.Exit(3);
                }

                try
                {
                    var process = RunJava(javaResult.JavaExePath, jarResult);
                    process.WaitForInputIdle();
                    Thread.Sleep(958);

                    if (process.HasExited && process.ExitCode != 0)
                    {
                        Error(string.Format("检测到 Jar {0} 没有正确启动\r\nJar 退出码: {1}\r\n这可能是由于你没有正确解压/安装 MoeCraft 导致的，请尝试重新安装 MoeCraft.\r\n\r\n若仍有问题，请在本目录打开命令提示符，输入以下命令查看错误消息 (都在一行):\r\njava -jar {2}\r\n\r\n其他参考信息: 所用 Java 路径: {3}", jarResult, process.ExitCode.ToString(), jarResult, javaResult.JavaExePath));

                        Environment.Exit(10);
                    }

                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Error(string.Format("启动 Java 失败：\r\nJar: {0}\r\nJava: {1}\r\n{2}", jarResult, javaResult.JavaExePath, ex));
                    Environment.Exit(5);
                }

            }
            catch (Exception ex)
            {
                Error("操作失败：\r\n" + ex);
                Environment.Exit(1);
            }
        }

        private static void ShowNoJavaPromptForm(NoJavaPromptForm.NoJavaMessageType messageType)
        {
            Application.Run(new NoJavaPromptForm(messageType));
            Environment.Exit(2);
        }

        private static Process RunJava(string javaPath, string jarPath)
        {
            var ps = new Process();
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.Arguments = "-jar \"" + Path.GetFullPath(jarPath) + "\" ";
            ps.StartInfo.FileName = Path.GetFullPath(javaPath);
            ps.Start();

            return ps;
        }

        private static void Error(string msg, MessageBoxIcon icon = MessageBoxIcon.Error, string title = "MoeCraft Launcher for Windows")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }

    }
}
