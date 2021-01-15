﻿/*
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
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Memory_Cleaner
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Timer
    {
        public uint min;
        public uint max;
        public uint cur;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SYSTEM_CACHE_INFORMATION_64_BIT
    {
        public long MinimumWorkingSet;
        public long MaximumWorkingSet;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokPriv1Luid
    {
        public int Count;
        public long Luid;
        public int Attr;
    }

    public partial class MainForm : Form
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern int EmptyWorkingSet(IntPtr hwProc);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx(out MEMORYSTATUSEX lpBuffer);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern UInt32 NtSetSystemInformation(int InfoClass, IntPtr Info, int Length);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private bool allowVisible;
        public uint CurrentResolution;
        AboutForm AboutForm = new AboutForm();
        SettingsForm SettingsForm = new SettingsForm();
        LicenseAgreementForm LicenseAgreementForm = new LicenseAgreementForm();
        RegistryKey Settings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings", true);
        RegistryKey Startup = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public MainForm()
        {
            InitializeComponent();

            // Copyright License
            {
                if (Settings.GetValue("LicenseAccepted") != null) { }
                else
                {
                    Settings.SetValue("LicenseAccepted", "False");
                }

                if (Settings.GetValue("LicenseAccepted").ToString() == "False")
                {
                    LicenseAgreementForm.ShowDialog(this);
                }
                else { }
            }

            // Desired Timer Resolution
            if (Settings.GetValue("DesiredTimerResolution") != null) { }
            else
            {
                Settings.SetValue("DesiredTimerResolution", "5000");
            }

            // Enable Clearing Of The Standby List
            if (Settings.GetValue("EnableClearingOfTheStandbyList") != null) { }
            else
            {
                Settings.SetValue("EnableClearingOfTheStandbyList", "1");
            }

            // Enable Custom Timer Resolution
            if (Settings.GetValue("EnableCustomTimerResolution") != null) { }
            else
            {
                Settings.SetValue("EnableCustomTimerResolution", "1");
            }

            // Enable Emptying Of The Working Set
            if (Settings.GetValue("EnableEmptyingOfTheWorkingSet") != null) { }
            else
            {
                Settings.SetValue("EnableEmptyingOfTheWorkingSet", "1");
            }

            // Hotkey Modifier Key
            if (Settings.GetValue("HotkeyModifierKey") != null) { }
            else
            {
                Settings.SetValue("HotkeyModifierKey", "None");
            }

            // Hotkey To Clean Memory
            if (Settings.GetValue("HotkeyToCleanMemory") != null) { }
            else
            {
                Settings.SetValue("HotkeyToCleanMemory", "F10");
            }

            // Start Memory Cleaner On System Startup
            if (Settings.GetValue("StartMemoryCleanerOnSystemStartup") != null) { }
            else
            {
                Settings.SetValue("StartMemoryCleanerOnSystemStartup", "0");
            }

            // Start Minimized
            if (Settings.GetValue("StartMinimized") != null) { }
            else
            {
                Settings.SetValue("StartMinimized", "0");
            }

            // Start Timer Resolution Automatically
            if (Settings.GetValue("StartTimerResolutionAutomatically") != null) { }
            else
            {
                Settings.SetValue("StartTimerResolutionAutomatically", "0");
            }

            // Timer Enabled
            if (Settings.GetValue("TimerEnabled") != null) { }
            else
            {
                Settings.SetValue("TimerEnabled", "1");
            }

            // Timer Polling Interval
            if (Settings.GetValue("TimerPollingInterval") != null) { }
            else
            {
                Settings.SetValue("TimerPollingInterval", "1 sec");
            }

            // Desired Timer Resolution
            if (Settings.GetValue("DesiredTimerResolution") != null)
            {
                SettingsForm.DesiredTimerResolution.Value = Convert.ToInt32(Settings.GetValue("DesiredTimerResolution"));
            }
            else
            {
                SettingsForm.DesiredTimerResolution.Value = 5000;
                Settings.SetValue("DesiredTimerResolution", SettingsForm.DesiredTimerResolution.Value);
            }

            // Enable Clearing Of The Standby List
            if (Settings.GetValue("EnableClearingOfTheStandbyList").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = true;
            }
            else if (Settings.GetValue("EnableClearingOfTheStandbyList").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = false;
            }

            // Enable Custom Timer Resolution
            if (Settings.GetValue("EnableCustomTimerResolution").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = true;
            }
            else if (Settings.GetValue("EnableCustomTimerResolution").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = false;
            }

            // Enable Emptying Of The Working Set
            if (Settings.GetValue("EnableEmptyingOfTheWorkingSet").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked = true;
            }
            else if (Settings.GetValue("EnableEmptyingOfTheWorkingSet").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked = false;
            }

            // Hotkey To Clean Memory
            if (Settings.GetValue("HotkeyToCleanMemory") != null)
            {
                SettingsForm.HotkeyToCleanMemory.Text = Settings.GetValue("HotkeyToCleanMemory").ToString();
            }
            else
            {
                SettingsForm.HotkeyToCleanMemory.Text = "F10";
                Settings.SetValue("HotkeyToCleanMemory", SettingsForm.HotkeyToCleanMemory.Text);
            }

            // Start Memory Cleaner On System Startup
            if (Settings.GetValue("StartMemoryCleanerOnSystemStartup").ToString() == "1")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = true;
                Startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"");
            }
            else if (Settings.GetValue("StartMemoryCleanerOnSystemStartup").ToString() == "0")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = false;
                if (Startup.GetValue("Memory Cleaner") != null)
                {
                    Startup.DeleteValue("Memory Cleaner");
                }
                else { }
            }

            // Start Minimized
            if (Settings.GetValue("StartMinimized").ToString() == "1")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = true;
                Minimize();
            }
            else if (Settings.GetValue("StartMinimized").ToString() == "0")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = false;
                Maximize();
            }

            // Start Timer Resolution Automatically
            if (Settings.GetValue("StartTimerResolutionAutomatically").ToString() == "1")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = true;
                SetTimerRes();
            }
            else if (Settings.GetValue("StartTimerResolutionAutomatically").ToString() == "0")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = false;
                UnSetTimerRes();
            }

            // Timer Enabled
            if (Settings.GetValue("TimerEnabled").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableTimer.Checked = true;
            }
            else if (Settings.GetValue("TimerEnabled").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableTimer.Checked = false;
            }

            // Timer Polling Interval
            if (Settings.GetValue("TimerPollingInterval") != null)
            {
                SettingsForm.TimerPollingInterval.Text = Settings.GetValue("TimerPollingInterval").ToString();
            }
            else
            {
                SettingsForm.TimerPollingInterval.Text = "1 sec";
                Settings.SetValue("TimerPollingInterval", SettingsForm.TimerPollingInterval.Text);
            }

            SaveSettings();
        }

        void MenuItemExit_Click(object sender, EventArgs e)
        {
            Exit(sender, e);
            UpdateValues();
        }

        void MenuItemSettings_Click(object sender, EventArgs e)
        {
            SettingsForm.ShowDialog(this);
            UpdateValues();
        }

        void MenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutForm.ShowDialog(this);
            UpdateValues();
        }

        void ButtonStart_Click(object sender, EventArgs e)
        {
            SetTimerRes();
            UpdateValues();
        }

        void ButtonStop_Click(object sender, EventArgs e)
        {
            UnSetTimerRes();
            UpdateValues();
        }

        void ButtonCleanMemory_Click(object sender, EventArgs e)
        {
            switch (SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked)
            {
                case true:
                    {
                        ClearStandbyList();
                        break;
                    }
                case false:
                    {
                        break;
                    }
            }

            switch (SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked)
            {
                case true:
                    {
                        EmptyWorkingSet();
                        break;
                    }
                case false:
                    {
                        break;
                    }
            }

            UpdateValues();
        }

        void SystemTrayIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            Maximize();
            UpdateValues();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            UpdateValues();
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                Minimize();
                UpdateValues();
            }
            else if (FormWindowState.Maximized == this.WindowState)
            {
                Maximize();
                UpdateValues();
            }
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit(sender, e);
        }

        public void SaveSettings()
        {
            RegisterHotkey();
            UpdateValues();
            Timer();

            switch (SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked)
            {
                case true:
                    {
                        Startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"");
                        break;
                    }
                case false:
                    {
                        if (Startup.GetValue("Memory Cleaner") != null)
                        {
                            Startup.DeleteValue("Memory Cleaner");
                        }
                        else { }
                        break;
                    }
            }

            switch (SettingsForm.CheckBoxEnableCustomTimerResolution.Checked)
            {
                case true:
                    {
                        ButtonStart.Enabled = true;
                        ButtonStop.Enabled = true;
                        break;
                    }
                case false:
                    {
                        ButtonStart.Enabled = false;
                        ButtonStop.Enabled = false;
                        break;
                    }
            }
        }

        void Exit(Object sender, EventArgs e)
        {
            Application.Exit();
            UnregisterHotKey(this.Handle, 1);
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        ulong GetAvailableMemory()
        {
            MEMORYSTATUSEX statex = new MEMORYSTATUSEX();
            statex.dwLength = 64;
            bool a = GlobalMemoryStatusEx(out statex);
            return statex.ullAvailPhys;
        }

        ulong GetTotalMemory()
        {
            MEMORYSTATUSEX statex = new MEMORYSTATUSEX();
            statex.dwLength = 64;
            bool a = GlobalMemoryStatusEx(out statex);
            return statex.ullTotalPhys;
        }

        void SetTimerRes()
        {
            NtSetTimerResolution(Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value), true, ref CurrentResolution);
        }

        void UnSetTimerRes()
        {
            NtSetTimerResolution(Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value), false, ref CurrentResolution);
        }

        Timer GetTimerRes()
        {
            var a = new Timer();
            NtQueryTimerResolution(out a.min, out a.max, out a.cur);
            return a;
        }

        void Minimize()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        void Maximize()
        {
            this.allowVisible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        void RegisterHotkey()
        {
            Keys a;
            Enum.TryParse(SettingsForm.HotkeyToCleanMemory.Text.Replace("Shift + ", "").Replace("Control + ", "").Replace("Alt + ", ""), out a);

            if (SettingsForm.HotkeyToCleanMemory.Text.Contains("Shift"))
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)4, a.GetHashCode());
            }
            else if (SettingsForm.HotkeyToCleanMemory.Text.Contains("Control"))
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)2, a.GetHashCode());
            }
            else if (SettingsForm.HotkeyToCleanMemory.Text.Contains("Alt"))
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)1, a.GetHashCode());
            }
            else
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)0, a.GetHashCode());
            }
        }

        void Timer()
        {
            switch (SettingsForm.TimerPollingInterval.Text)
            {
                case "0.5 sec":
                    {
                        switch (SettingsForm.CheckBoxEnableTimer.Checked)
                        {
                            case true:
                                {
                                    Timer1.Interval = 500;
                                    break;
                                }
                            case false:
                                {
                                    break;
                                }
                        }
                        break;
                    }

                case "1 sec":
                    {
                        switch (SettingsForm.CheckBoxEnableTimer.Checked)
                        {
                            case true:
                                {
                                    Timer1.Interval = 1000;
                                    break;
                                }
                            case false:
                                {
                                    break;
                                }
                        }
                        break;
                    }

                case "2 sec":
                    {
                        switch (SettingsForm.CheckBoxEnableTimer.Checked)
                        {
                            case true:
                                {
                                    Timer1.Interval = 2000;
                                    break;
                                }
                            case false:
                                {
                                    break;
                                }
                        }
                        break;
                    }

                case "5 sec":
                    {
                        switch (SettingsForm.CheckBoxEnableTimer.Checked)
                        {
                            case true:
                                {
                                    Timer1.Interval = 5000;
                                    break;
                                }
                            case false:
                                {
                                    break;
                                }
                        }
                        break;
                    }

                case "10 sec":
                    {
                        switch (SettingsForm.CheckBoxEnableTimer.Checked)
                        {
                            case true:
                                {
                                    Timer1.Interval = 10000;
                                    break;
                                }
                            case false:
                                {
                                    break;
                                }
                        }
                        break;
                    }
            }

            switch (SettingsForm.CheckBoxEnableTimer.Checked)
            {
                case true:
                    {
                        Timer1.Enabled = true;
                        break;
                    }
                case false:
                    {
                        Timer1.Enabled = false;
                        break;
                    }
            }
        }

        void EmptyWorkingSet()
        {
            string ProcessName = string.Empty;
            Process[] allProcesses = Process.GetProcesses();
            List<string> successProcesses = new List<string>();
            List<string> failProcesses = new List<string>();
            for (int i = 0; i < allProcesses.Length; i++)
            {
                Process p = new Process();
                p = allProcesses[i];
                try
                {
                    ProcessName = p.ProcessName;
                    EmptyWorkingSet(p.Handle);
                }
                catch
                {
                }
            }
        }

        void ClearStandbyList()
        {
            try
            {
                if (SetIncreasePrivilege("SeIncreaseQuotaPrivilege"))
                {
                    int SystemInfoLength = Marshal.SizeOf(4);
                    GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    SYSTEM_CACHE_INFORMATION_64_BIT information64Bit = new SYSTEM_CACHE_INFORMATION_64_BIT();
                    information64Bit.MinimumWorkingSet = -1L;
                    information64Bit.MaximumWorkingSet = -1L;
                    SystemInfoLength = Marshal.SizeOf(information64Bit);
                    gcHandle = GCHandle.Alloc(information64Bit, GCHandleType.Pinned);
                    gcHandle.Free();
                }
                if (SetIncreasePrivilege("SeProfileSingleProcessPrivilege"))
                {
                    GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    uint num = NtSetSystemInformation(80, gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(4));
                    if (num != 0)
                    {
                        throw new Exception("NtSetSystemInformation(SYSTEMMEMORYLISTINFORMATION) error: ", new Win32Exception(Marshal.GetLastWin32Error()));
                    }
                    gcHandle.Free();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        static bool SetIncreasePrivilege(string privilegeName)
        {
            using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                TokPriv1Luid newst;
                newst.Count = 1;
                newst.Luid = 0L;
                newst.Attr = 2;
                if (!LookupPrivilegeValue(null, privilegeName, ref newst.Luid))
                {
                    throw new Exception("Error in LookupPrivilegeValue: ", new Win32Exception(Marshal.GetLastWin32Error()));
                }
                int num = AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0;
                if (num == 0)
                {
                    throw new Exception("Error in AdjustTokenPrivileges: ", new Win32Exception(Marshal.GetLastWin32Error()));
                }
                return num != 0;
            }
        }

        void UpdateValues()
        {
            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().cur / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().max / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().min / 10000.0) + " ms";
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                switch (SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked)
                {
                    case true:
                        {
                            ClearStandbyList();
                            break;
                        }
                    case false:
                        {
                            break;
                        }
                }

                switch (SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked)
                {
                    case true:
                        {
                            EmptyWorkingSet();
                            break;
                        }
                    case false:
                        {
                            break;
                        }
                }

                UpdateValues();
            }
        }

        void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateValues();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            KeyEventArgs a = new KeyEventArgs(keyData);
            if (a.KeyCode == Keys.F1)
            {
                AboutForm.ShowDialog(this);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}