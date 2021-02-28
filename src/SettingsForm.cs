/*
   Memory Cleaner

   Copyright (C) 2021 Danske

   This file is part of Memory Cleaner

   Memory Cleaner is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   Memory Cleaner is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with Memory Cleaner.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Memory_Cleaner
{
    public partial class SettingsForm : Form
    {
        RegistryKey Settings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner", true);

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void HotkeyToCleanMemory_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                if (e.KeyCode.ToString() == "ShiftKey")
                {
                }
                else
                {
                    HotkeyToCleanMemory.Text = "Shift + " + e.KeyCode.ToString();
                }
            }
            else if (Control.ModifierKeys == Keys.Control)
            {
                if (e.KeyCode.ToString() == "ControlKey")
                {
                }
                else
                {
                    HotkeyToCleanMemory.Text = "Control + " + e.KeyCode.ToString();
                }
            }
            else if (Control.ModifierKeys == Keys.Alt)
            {
                if (e.KeyCode.ToString() == "Menu")
                {
                }
                else
                {
                    HotkeyToCleanMemory.Text = "Alt + " + e.KeyCode.ToString();
                }
            }
            else
            {
                HotkeyToCleanMemory.Text = e.KeyCode.ToString();
            }

            Settings.SetValue("HotkeyToCleanMemory", HotkeyToCleanMemory.Text, RegistryValueKind.String);
        }

        private void CheckBoxEnableClearingOfTheStandbyList_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableClearingOfTheStandbyList.Checked == true)
            {
                Settings.SetValue("EnableClearingOfTheStandbyList", "1", RegistryValueKind.String);
            }
            else if (CheckBoxEnableClearingOfTheStandbyList.Checked == false)
            {
                Settings.SetValue("EnableClearingOfTheStandbyList", "0", RegistryValueKind.String);
            }
        }

        private void CheckBoxEnableEmptyingOfTheWorkingSet_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableEmptyingOfTheWorkingSet.Checked == true)
            {
                Settings.SetValue("EnableEmptyingOfTheWorkingSet", "1", RegistryValueKind.String);
            }
            else if (CheckBoxEnableEmptyingOfTheWorkingSet.Checked == false)
            {
                Settings.SetValue("EnableEmptyingOfTheWorkingSet", "0", RegistryValueKind.String);
            }
        }

        private void DesiredTimerResolution_ValueChanged(object sender, EventArgs e)
        {
            Settings.SetValue("DesiredTimerResolution", DesiredTimerResolution.Value, RegistryValueKind.String);
        }

        private void CheckBoxEnableCustomTimerResolution_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableCustomTimerResolution.Checked == true)
            {
                Settings.SetValue("EnableCustomTimerResolution", "1", RegistryValueKind.String);
            }
            else if (CheckBoxEnableCustomTimerResolution.Checked == false)
            {
                Settings.SetValue("EnableCustomTimerResolution", "0", RegistryValueKind.String);
            }
        }

        private void TimerPollingInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SetValue("TimerPollingInterval", TimerPollingInterval.Text, RegistryValueKind.String);
        }

        private void CheckBoxEnableTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxEnableTimer.Checked == true)
            {
                Settings.SetValue("TimerEnabled", "1", RegistryValueKind.String);
            }
            else if (CheckBoxEnableTimer.Checked == false)
            {
                Settings.SetValue("TimerEnabled", "0", RegistryValueKind.String);
            }
        }

        private void CheckBoxStartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartMinimized.Checked == true)
            {
                Settings.SetValue("StartMinimized", "1", RegistryValueKind.String);
            }
            else if (CheckBoxStartMinimized.Checked == false)
            {
                Settings.SetValue("StartMinimized", "0", RegistryValueKind.String);
            }
        }

        private void CheckBoxStartTimerResolutionAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartTimerResolutionAutomatically.Checked == true)
            {
                Settings.SetValue("StartTimerResolutionAutomatically", "1", RegistryValueKind.String);
            }
            else if (CheckBoxStartTimerResolutionAutomatically.Checked == false)
            {
                Settings.SetValue("StartTimerResolutionAutomatically", "0", RegistryValueKind.String);
            }
        }

        private void CheckBoxStartMemoryCleanerOnSystemStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartMemoryCleanerOnSystemStartup.Checked == true)
            {
                Settings.SetValue("StartMemoryCleanerOnSystemStartup", "1", RegistryValueKind.String);
            }
            else if (CheckBoxStartMemoryCleanerOnSystemStartup.Checked == false)
            {
                Settings.SetValue("StartMemoryCleanerOnSystemStartup", "0", RegistryValueKind.String);
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to reset all of the settings?", "Memory Cleaner", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                case DialogResult.Yes:
                    {
                        Settings.SetValue("DesiredTimerResolution", "5000", RegistryValueKind.String);
                        DesiredTimerResolution.Value = 5000;

                        Settings.SetValue("EnableClearingOfTheStandbyList", "1", RegistryValueKind.String);
                        CheckBoxEnableClearingOfTheStandbyList.Checked = true;

                        Settings.SetValue("EnableCustomTimerResolution", "1", RegistryValueKind.String);
                        CheckBoxEnableTimer.Checked = true;

                        Settings.SetValue("EnableEmptyingOfTheWorkingSet", "1", RegistryValueKind.String);
                        CheckBoxEnableEmptyingOfTheWorkingSet.Checked = true;

                        Settings.SetValue("HotkeyToCleanMemory", "F10", RegistryValueKind.String);
                        HotkeyToCleanMemory.Text = "F10";

                        Settings.SetValue("StartMemoryCleanerOnSystemStartup", "0", RegistryValueKind.String);
                        CheckBoxStartMemoryCleanerOnSystemStartup.Checked = false;

                        Settings.SetValue("StartMinimized", "0", RegistryValueKind.String);
                        CheckBoxStartMinimized.Checked = false;

                        Settings.SetValue("StartTimerResolutionAutomatically", "0", RegistryValueKind.String);
                        CheckBoxStartTimerResolutionAutomatically.Checked = false;

                        Settings.SetValue("TimerEnabled", "1", RegistryValueKind.String);
                        CheckBoxEnableTimer.Checked = true;

                        Settings.SetValue("TimerPollingInterval", "1 sec", RegistryValueKind.String);
                        TimerPollingInterval.Text = "1 sec";

                        break;
                    }
            }
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            KeyEventArgs a = new KeyEventArgs(keyData);
            if (a.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
