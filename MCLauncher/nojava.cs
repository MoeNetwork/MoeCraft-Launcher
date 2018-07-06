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
    public partial class nojava : Form
    {
        public enum NoJavaMessageType { NotFound, TooOld, Incompatible, Other };

        public nojava(NoJavaMessageType MessageType = NoJavaMessageType.Other)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "http://www.oracle.com/technetwork/cn/java/javase/downloads/jre10-downloads-4417026.html?ssSourceSiteId=otncn");
        }
    }
}
