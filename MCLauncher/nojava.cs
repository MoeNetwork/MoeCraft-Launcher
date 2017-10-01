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
        public nojava()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html");
        }
    }
}
