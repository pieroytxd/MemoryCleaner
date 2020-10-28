using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Memory_Cleaner
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Info.Focus();
        }

        private void LinkGitHub_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/danskee");
            Info.Focus();
        }

        private void ButtonTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/danskexd");
            Info.Focus();
        }

        private void ButtonDonate_Click(object sender, EventArgs e)
        {
            Process.Start("https://paypal.me/danskexd");
            Info.Focus();
        }

        private void ButtonDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://pastebin.com/QcUD4MMH");
            Info.Focus();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Info.Focus();
        }
    }
}
