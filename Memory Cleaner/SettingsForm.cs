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
            LabelHotkeyToCleanMemory.Focus();
        }

        private void HotkeyToCleanMemory_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("HotkeyToCleanMemory", HotkeyToCleanMemory.Text);
            key1.Close();
        }

        private void HotkeyModifierKey_TextChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("HotkeyModifierKey", HotkeyModifierKey.Text);
            key1.Close();
        }

        private void CheckBoxEnableClearingOfTheStandbyList_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableClearingOfTheStandbyList.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableClearingOfTheStandbyList", "1");
                key1.Close();
            }
            else if (CheckBoxEnableClearingOfTheStandbyList.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableClearingOfTheStandbyList", "0");
                key1.Close();
            }
        }

        private void CheckBoxEnableEmptyingOfTheWorkingSet_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableEmptyingOfTheWorkingSet.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableEmptyingOfTheWorkingSet", "1");
                key1.Close();
            }
            else if (CheckBoxEnableEmptyingOfTheWorkingSet.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableEmptyingOfTheWorkingSet", "0");
                key1.Close();
            }
        }

        private void DesiredTimerResolution_ValueChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("DesiredTimerResolution", DesiredTimerResolution.Value);
            key1.Close();
        }

        private void CheckBoxEnableCustomTimerResolution_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableCustomTimerResolution.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableCustomTimerResolution", "1");
                key1.Close();
            }
            else if (CheckBoxEnableCustomTimerResolution.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("EnableCustomTimerResolution", "0");
                key1.Close();
            }
        }

        private void TimerPollingInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("TimerPollingInterval", TimerPollingInterval.Text);
            key1.Close();
        }

        private void CheckBoxEnableTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableTimer.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("TimerEnabled", "1");
                key1.Close();
            }
            else if (CheckBoxEnableTimer.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("TimerEnabled", "0");
                key1.Close();
            }
        }

        private void CheckBoxStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartMinimized.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartMinimized", "1");
                key1.Close();
            }
            else if (CheckBoxStartMinimized.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartMinimized", "0");
                key1.Close();
            }
        }

        private void CheckBoxStartTimerResolutionAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartTimerResolutionAutomatically.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartTimerResolutionAutomatically", "1");
                key1.Close();
            }
            else if (CheckBoxStartTimerResolutionAutomatically.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartTimerResolutionAutomatically", "0");
                key1.Close();
            }
        }

        private void CheckBoxStartMemoryCleanerOnSystemStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartMemoryCleanerOnSystemStartup.Checked == true)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartMemoryCleanerOnSystemStartup", "1");
                key1.Close();
            }
            else if (CheckBoxStartMemoryCleanerOnSystemStartup.Checked == false)
            {
                RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key1.SetValue("StartMemoryCleanerOnSystemStartup", "0");
                key1.Close();
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset all of the settings?", "Memory Cleaner", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (result)
            {
                case DialogResult.Yes:
                    {
                        RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key1.SetValue("DesiredTimerResolution", "5000");
                        key1.Close();
                        DesiredTimerResolution.Value = 5000;

                        RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key2.SetValue("EnableClearingOfTheStandbyList", "1");
                        key2.Close();
                        CheckBoxEnableClearingOfTheStandbyList.Checked = true;

                        RegistryKey key3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key3.SetValue("EnableCustomTimerResolution", "1");
                        key3.Close();
                        CheckBoxEnableTimer.Checked = true;

                        RegistryKey key4 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key4.SetValue("EnableEmptyingOfTheWorkingSet", "1");
                        key4.Close();
                        CheckBoxEnableEmptyingOfTheWorkingSet.Checked = true;

                        RegistryKey key5 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key5.SetValue("HotkeyModifierKey", "None");
                        key5.Close();
                        HotkeyModifierKey.Text = "None";

                        RegistryKey key6 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key6.SetValue("HotkeyToCleanMemory", "F10");
                        key6.Close();
                        HotkeyToCleanMemory.Text = "F10";

                        RegistryKey key7 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key7.SetValue("StartMemoryCleanerOnSystemStartup", "0");
                        key7.Close();
                        CheckBoxStartMemoryCleanerOnSystemStartup.Checked = false;

                        RegistryKey key8 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key8.SetValue("StartMinimized", "0");
                        key8.Close();
                        CheckBoxStartMinimized.Checked = false;

                        RegistryKey key9 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key9.SetValue("StartTimerResolutionAutomatically", "0");
                        key9.Close();
                        CheckBoxStartTimerResolutionAutomatically.Checked = false;

                        RegistryKey key10 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key10.SetValue("TimerEnabled", "1");
                        key10.Close();
                        CheckBoxEnableTimer.Checked = true;

                        RegistryKey key11 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                        key11.SetValue("TimerPollingInterval", "1 sec");
                        key11.Close();
                        TimerPollingInterval.Text = "1 sec";

                        break;
                    }

                case DialogResult.No:
                    {
                        break;
                    }
            }

            LabelHotkeyToCleanMemory.Focus();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            LabelHotkeyToCleanMemory.Focus();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm MainForm = (MainForm)Application.OpenForms["MainForm"];
            MainForm.SaveSettings();
        }
    }
}
