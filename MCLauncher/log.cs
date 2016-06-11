using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCLauncher
{
    public partial class logger : Form
    {
        public Process px;

        public logger(Process ps)
        {
            InitializeComponent();
            ps.StartInfo.RedirectStandardOutput = true;
            ps.Exited += Ps_Exited;
            ps.Start();
            ps.BeginOutputReadLine();
            px = ps;
            ps.OutputDataReceived += new DataReceivedEventHandler((object drsender, DataReceivedEventArgs dre) =>
            {
                log(dre.Data);
            });
        }

        private void Ps_Exited(object sender, EventArgs e)
        {
            Text += " - 游戏已退出";
        }

        public void error(string msg, string title = "错误")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void 清空LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text.Text = "";
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text.SelectAll();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public delegate void logInvoke(string v);
        public void log(string v)
        {
            if (text.InvokeRequired)
            {
                logInvoke li = new logInvoke(log);
                Invoke(li, v);
            }
            else
            {
                text.AppendText(v + "\r\n");
            }
        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            text.ScrollToCaret();
        }

        private void logger_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                px.Kill();
            }
            catch (Exception) { }
        }

        private void 停止SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                px.Kill();
            }
            catch (Exception) { }
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("MoeCraft Launcher for Windows V2.0\r\n作者：Kenvix\r\nhttp://zhizhe8.net","关于",MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    Process.Start("http://zhizhe8.net");
                }
                catch (Exception) { }
            }
        }
    }
}
