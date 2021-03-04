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
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO.Compression;
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

        public string Folder = "";
        private bool allowVisible;
        public string newestver = "";
        public uint CurrentResolution;
        WebClient wc = new WebClient();
        AboutDialog AboutDialog = new AboutDialog("1.6.4");
        SettingsForm SettingsForm = new SettingsForm();
        LicenseAgreementDialog LicenseAgreementDialog = new LicenseAgreementDialog();
        RegistryKey Settings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner", true);
        RegistryKey Startup = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        public MainForm()
        {
            InitializeComponent();

            // Copyright License
            if (Settings.GetValue("LicenseAccepted") != null)
            {
            }
            else
            {
                Settings.SetValue("LicenseAccepted", "False", RegistryValueKind.String);
            }

            if (Settings.GetValue("LicenseAccepted").ToString() == "False")
            {
                LicenseAgreementDialog.ShowDialog(this);
            }

            // Checked For Updates
            if (Settings.GetValue("CheckedForUpdates") != null)
            {
                if (Settings.GetValue("CheckedForUpdates").ToString() == "False")
                {
                    checkforupdates();
                }
            }
            else
            {
                checkforupdates();
            }

            // Desired Timer Resolution
            if (Settings.GetValue("DesiredTimerResolution") != null)
            {
                SettingsForm.DesiredTimerResolution.Value = Convert.ToInt32(Settings.GetValue("DesiredTimerResolution"));
            }
            else
            {
                SettingsForm.DesiredTimerResolution.Value = 5000;
                Settings.SetValue("DesiredTimerResolution", SettingsForm.DesiredTimerResolution.Value, RegistryValueKind.String);
            }

            // Enable Clearing Of The Standby List
            if (Settings.GetValue("EnableClearingOfTheStandbyList") != null)
            {
            }
            else
            {
                Settings.SetValue("EnableClearingOfTheStandbyList", "1", RegistryValueKind.String);
            }
            if (Settings.GetValue("EnableClearingOfTheStandbyList").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = true;
            }
            else if (Settings.GetValue("EnableClearingOfTheStandbyList").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = false;
            }

            // Enable Custom Timer Resolution
            if (Settings.GetValue("EnableCustomTimerResolution") != null)
            {
            }
            else
            {
                Settings.SetValue("EnableCustomTimerResolution", "1", RegistryValueKind.String);
            }
            if (Settings.GetValue("EnableCustomTimerResolution").ToString() == "1")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = true;
            }
            else if (Settings.GetValue("EnableCustomTimerResolution").ToString() == "0")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = false;
            }

            // Enable Emptying Of The Working Set
            if (Settings.GetValue("EnableEmptyingOfTheWorkingSet") != null)
            {
            }
            else
            {
                Settings.SetValue("EnableEmptyingOfTheWorkingSet", "1", RegistryValueKind.String);
            }
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
                Settings.SetValue("HotkeyToCleanMemory", SettingsForm.HotkeyToCleanMemory.Text, RegistryValueKind.String);
            }

            // Start Memory Cleaner On System Startup
            if (Settings.GetValue("StartMemoryCleanerOnSystemStartup") != null)
            {
            }
            else
            {
                Settings.SetValue("StartMemoryCleanerOnSystemStartup", "0", RegistryValueKind.String);
            }
            if (Settings.GetValue("StartMemoryCleanerOnSystemStartup").ToString() == "1")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = true;
                Startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"", RegistryValueKind.String);
            }
            else if (Settings.GetValue("StartMemoryCleanerOnSystemStartup").ToString() == "0")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = false;
                if (Startup.GetValue("Memory Cleaner") != null)
                {
                    Startup.DeleteValue("Memory Cleaner");
                }
            }

            // Start Minimized
            if (Settings.GetValue("StartMinimized") != null)
            {
            }
            else
            {
                Settings.SetValue("StartMinimized", "0", RegistryValueKind.String);
            }
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
            if (Settings.GetValue("StartTimerResolutionAutomatically") != null)
            {
            }
            else
            {
                Settings.SetValue("StartTimerResolutionAutomatically", "0", RegistryValueKind.String);
            }
            if (Settings.GetValue("StartTimerResolutionAutomatically").ToString() == "1")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = true;
                TimerRes(1);
            }
            else if (Settings.GetValue("StartTimerResolutionAutomatically").ToString() == "0")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = false;
                TimerRes(2);
            }

            // Timer Enabled
            if (Settings.GetValue("TimerEnabled") != null)
            {
            }
            else
            {
                Settings.SetValue("TimerEnabled", "1", RegistryValueKind.String);
            }
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
                Settings.SetValue("TimerPollingInterval", SettingsForm.TimerPollingInterval.Text, RegistryValueKind.String);
            }

            SaveSettings();
        }

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Exit(sender, e);
            UpdateValues();
        }

        private void MenuItemSettings_Click(object sender, EventArgs e)
        {
            SettingsForm.ShowDialog(this);
            UpdateValues();
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutDialog.ShowDialog(this);
            UpdateValues();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            TimerRes(1);
            UpdateValues();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            TimerRes(2);
            UpdateValues();
        }

        private void ButtonCleanMemory_Click(object sender, EventArgs e)
        {
            CleanMemory();
            UpdateValues();
        }

        private void SystemTrayIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            Maximize();
            UpdateValues();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    Minimize();
                    UpdateValues();
                    break;

                case FormWindowState.Maximized:
                    Maximize();
                    UpdateValues();
                    break;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
            UnregisterHotKey(this.Handle, 1);
        }

        public void SaveSettings()
        {
            RegisterHotkey();
            UpdateValues();
            Timer();

            switch (SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked)
            {
                case true:
                    Startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"", RegistryValueKind.String);
                    break;

                case false:
                    if (Startup.GetValue("Memory Cleaner") != null)
                    {
                        Startup.DeleteValue("Memory Cleaner");
                    }
                    break;
            }

            switch (SettingsForm.CheckBoxEnableCustomTimerResolution.Checked)
            {
                case true:
                    ButtonStart.Enabled = true;
                    ButtonStop.Enabled = true;
                    break;

                case false:
                    ButtonStart.Enabled = false;
                    ButtonStop.Enabled = false;
                    break;
            }
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

        private ulong GetMemory(int x)
        {
            MEMORYSTATUSEX statex = new MEMORYSTATUSEX();
            statex.dwLength = 64;
            bool a = GlobalMemoryStatusEx(out statex);

            switch (x)
            {
                case 1:
                    return statex.ullAvailPhys;

                case 2:
                    return statex.ullTotalPhys;

                default:
                    return 0;
            }
        }

        private void TimerRes(int x)
        {
            switch (x)
            {
                case 1:
                    NtSetTimerResolution(Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value), true, ref CurrentResolution);
                    break;

                case 2:
                    NtSetTimerResolution(Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value), false, ref CurrentResolution);
                    break;
            }
        }

        private Timer GetTimerRes()
        {
            var a = new Timer();
            NtQueryTimerResolution(out a.min, out a.max, out a.cur);
            return a;
        }

        private void Minimize()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        private void Maximize()
        {
            this.allowVisible = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void RegisterHotkey()
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

        private void Timer()
        {
            switch (SettingsForm.TimerPollingInterval.Text)
            {
                case "0.5 sec":
                    if (SettingsForm.CheckBoxEnableTimer.Checked == true)
                    {
                        Timer1.Interval = 500;
                    }
                    break;

                case "1 sec":
                    if (SettingsForm.CheckBoxEnableTimer.Checked == true)
                    {
                        Timer1.Interval = 1000;
                    }
                    break;

                case "2 sec":
                    if (SettingsForm.CheckBoxEnableTimer.Checked == true)
                    {
                        Timer1.Interval = 2000;
                    }
                    break;

                case "5 sec":
                    if (SettingsForm.CheckBoxEnableTimer.Checked == true)
                    {
                        Timer1.Interval = 5000;
                    }
                    break;

                case "10 sec":
                    if (SettingsForm.CheckBoxEnableTimer.Checked == true)
                    {
                        Timer1.Interval = 10000;
                    }
                    break;
            }

            switch (SettingsForm.CheckBoxEnableTimer.Checked)
            {
                case true:
                    Timer1.Enabled = true;
                    break;

                case false:
                    Timer1.Enabled = false;
                    break;
            }
        }

        private void Clear(int x)
        {
            switch (x)
            {
                case 1:
                    string processname = "";
                    Process[] allProcesses = Process.GetProcesses();
                    List<string> successProcesses = new List<string>();
                    List<string> failProcesses = new List<string>();
                    for (int i = 0; i < allProcesses.Length; i++)
                    {
                        Process p = new Process();
                        p = allProcesses[i];
                        try
                        {
                            processname = p.ProcessName;
                            EmptyWorkingSet(p.Handle);
                        }
                        catch
                        {
                        }
                    }
                    break;

                case 2:
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
                    break;
            }
        }

        private void CleanMemory()
        {
            if (SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked == true)
            {
                Clear(1);
            }

            if (SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked == true)
            {
                Clear(2);
            }
        }

        private static bool SetIncreasePrivilege(string privilegeName)
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

        private void UpdateValues()
        {
            AvailableMemory.Text = "Available: " + (GetMemory(1) / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetMemory(2) / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().cur / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().max / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().min / 10000.0) + " ms";
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0312)
            {
                CleanMemory();
                UpdateValues();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void SelectFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Folder = fbd.SelectedPath;
            }
            else if (fbd.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Please select a folder where to download the newest version in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SelectFolder();
            }
        }

        private void checkforupdates()
        {
            Settings.SetValue("CheckedForUpdates", "True", RegistryValueKind.String);

            int currentversion = Convert.ToInt32("1.6.3".Replace(".", ""));
            newestver = wc.DownloadString("https://raw.githubusercontent.com/danskee/MemoryCleaner/main/version").Replace("\n", "").Replace("\r", "");
            int newestversion = Convert.ToInt32(newestver.Replace(".", ""));

            if (currentversion < newestversion)
            {
                switch (MessageBox.Show("The version " + newestver + " is available. Download?", "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        DownloadNewestVersion();
                        break;
                }
            }
        }

        private void CheckForUpdates(object sender, EventArgs e)
        {
            checkforupdates();
        }

        private void DownloadNewestVersion()
        {
            SelectFolder();

            try
            {
                wc.DownloadFile("https://github.com/danskee/MemoryCleaner/releases/download/v" + newestver + "/" + "MemoryCleaner-v" + newestver + ".zip", Folder + @"\" + "MemoryCleaner-v" + newestver + ".zip");
                ZipFile.ExtractToDirectory(Folder + @"\" + "MemoryCleaner-v" + newestver + ".zip", Folder);
                File.Delete(Folder + @"\" + "MemoryCleaner-v" + newestver + ".zip");
                MessageBox.Show("Succesfully downloaded the newest version in " + Folder + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Failed to download the newest version. Make sure you have a stable internet connection and that you're not trying to overwrite an already existing file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}