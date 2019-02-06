using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MCLauncher
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
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
