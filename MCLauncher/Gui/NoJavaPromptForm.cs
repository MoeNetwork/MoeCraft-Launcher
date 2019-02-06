using System;
using System.Diagnostics;
using System.Windows.Forms;
using MCLauncher.Library;

namespace MCLauncher
{
    public partial class NoJavaPromptForm : Form
    {
        public enum NoJavaMessageType { NotFound, TooOld, Incompatible, Other };

        public NoJavaPromptForm(NoJavaMessageType MessageType = NoJavaMessageType.Other)
        {

            InitializeComponent();

            switch(MessageType)
            {
                case NoJavaMessageType.Incompatible:
                    Text = "Java 版本不兼容 - " + Text;
                    break;

                case NoJavaMessageType.NotFound:
                    Text = "Java 未安装 - " + Text;
                    break;

                case NoJavaMessageType.TooOld:
                    Text = "Java 版本过低 - " + Text;
                    break;

            }

            LabelPromptTitle.Text = String.Format(LabelPromptTitle.Text, ApplicationEnvironment.JavaVersionReadable);
            ButtonDownloadJava.Text = String.Format(ButtonDownloadJava.Text, ApplicationEnvironment.JavaVersionReadable);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", ApplicationEnvironment.JavaDownloadURL);
        }
    }
}
