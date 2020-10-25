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
        }

        private void LinkGitHub_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/danskee");
        }

        private void ButtonTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/danskexd");
        }

        private void ButtonGitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/danskee/memorycleaner");
        }

        private void ButtonDonate_Click(object sender, EventArgs e)
        {
            Process.Start("https://paypal.me/danskexd");
        }

        private void ButtonDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://pastebin.com/QcUD4MMH");
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
