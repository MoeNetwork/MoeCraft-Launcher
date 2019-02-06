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

                RunJavaAndWait(javaResult.Path, jarResult);

            }
            catch (Exception ex)
            {
                Error("操作失败：\r\n" + ex);
                Environment.Exit(4);
            }

            Thread.Sleep(500);
            Environment.Exit(0);
        }

        private static void ShowNoJavaPromptForm(NoJavaPromptForm.NoJavaMessageType messageType)
        {
            Application.Run(new NoJavaPromptForm(messageType));
            Environment.Exit(2);
        }

        private static void RunJavaAndWait(string javaPath, string jarPath)
        {
            var ps = new Process();
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.Arguments = "-jar \"" + Path.GetFullPath(jarPath) + "\" ";
            ps.StartInfo.FileName = Path.GetFullPath(javaPath);
            ps.Start();

            ps.WaitForInputIdle();
        }

        private static void Error(string msg, MessageBoxIcon icon = MessageBoxIcon.Error, string title = "MoeCraft Launcher for Windows")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }

    }
}
