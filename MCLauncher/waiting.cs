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
    public partial class waiting : Form
    {
        public waiting()
        {
            InitializeComponent();
            VerionLabel.Text = Application.ProductVersion;
        }

        private void waiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer", "https://kenvix.com");
            }catch(Exception) { }
        }
    }
}
