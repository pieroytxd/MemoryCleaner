using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Memory_Cleaner
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void DesiredTimerResolution_ValueChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("DesiredTimerRes", DesiredTimerResolution.Value);
            key1.Close();
        }

        private void HotkeyToCleanMemory_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("Hotkey", HotkeyToCleanMemory.Text);
            key1.Close();
        }

        private void HotkeyModifierKey_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("HotkeyModifier", HotkeyModifierKey.Text);
            key1.Close();
        }

        private void TimerEnabled_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("TimerEnabled", TimerEnabled.Text);
            key1.Close();
        }

        private void TimerPollingInterval_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("TimerPollingInterval", TimerPollingInterval.Text);
            key1.Close();
        }

        private void CheckBoxStartAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartAutomatically.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("AutoStart", "1");
                key1.Close();
            }
            else if (CheckBoxStartAutomatically.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("AutoStart", "0");
                key1.Close();
            }
        }

        private void CheckBoxStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartMinimized.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("Hide", "1");
                key1.Close();
            }
            else if (CheckBoxStartMinimized.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("Hide", "0");
                key1.Close();
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("DesiredTimerRes", "5000");
            key1.Close();
            DesiredTimerResolution.Value = 5000;

            RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key2.SetValue("Hotkey", "F10");
            key2.Close();
            HotkeyToCleanMemory.Text = "F10";

            RegistryKey key3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key3.SetValue("HotkeyModifier", "None");
            key3.Close();
            HotkeyModifierKey.Text = "None";

            RegistryKey key4 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key4.SetValue("TimerEnabled", "True");
            key4.Close();
            TimerEnabled.Text = "True";

            RegistryKey key5 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key5.SetValue("TimerPollingInterval", "1 sec");
            key5.Close();
            TimerPollingInterval.Text = "1 sec";

            RegistryKey key6 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key6.SetValue("AutoStart", "0");
            key6.Close();
            CheckBoxStartAutomatically.Checked = false;

            RegistryKey key7 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key7.SetValue("Hide", "0");
            key7.Close();
            CheckBoxStartMinimized.Checked = false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm MainForm = (MainForm)Application.OpenForms["MainForm"];
            MainForm.SaveSettings();
        }
    }
}
