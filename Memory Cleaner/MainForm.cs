using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace Memory_Cleaner
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Timer
    {
        public uint PeriodMin;
        public uint PeriodMax;
        public uint PeriodCurrent;
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

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
        }

        AboutForm AboutForm = new AboutForm();
        SettingsForm SettingsForm = new SettingsForm();

        private bool allowVisible;

        public MainForm()
        {
            InitializeComponent();

            RegistryKey ctsfr = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            ctsfr.Close();

            RegistryKey dtr1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object dtr2 = dtr1.GetValue("DesiredTimerResolution");
            if (dtr2 != null)
            {
            }
            else
            {
                RegistryKey dtr3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                dtr3.SetValue("DesiredTimerResolution", "5000");
                dtr3.Close();
                dtr1.Close();
            }

            RegistryKey ecotsl1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ecotsl2 = ecotsl1.GetValue("EnableClearingOfTheStandbyList");
            if (ecotsl2 != null)
            {
            }
            else
            {
                RegistryKey ecotsl3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                ecotsl3.SetValue("EnableClearingOfTheStandbyList", "1");
                ecotsl3.Close();
                ecotsl1.Close();
            }

            RegistryKey ectr1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ectr2 = ectr1.GetValue("EnableCustomTimerResolution");
            if (ectr2 != null)
            {
            }
            else
            {
                RegistryKey ectr3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                ectr3.SetValue("EnableCustomTimerResolution", "1");
                ectr3.Close();
                ectr1.Close();
            }

            RegistryKey eeotws1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object eeotws2 = eeotws1.GetValue("EnableEmptyingOfTheWorkingSet");
            if (eeotws2 != null)
            {
            }
            else
            {
                RegistryKey eeotws3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                eeotws3.SetValue("EnableEmptyingOfTheWorkingSet", "1");
                eeotws3.Close();
                eeotws1.Close();
            }

            RegistryKey hmk1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object hmk2 = hmk1.GetValue("HotkeyModifierKey");
            if (hmk2 != null)
            {
            }
            else
            {
                RegistryKey hmk3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                hmk3.SetValue("HotkeyModifierKey", "None");
                hmk3.Close();
                hmk1.Close();
            }

            RegistryKey htcm1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object htcm2 = htcm1.GetValue("HotkeyToCleanMemory");
            if (htcm2 != null)
            {
            }
            else
            {
                RegistryKey htcm3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                htcm3.SetValue("HotkeyToCleanMemory", "F10");
                htcm3.Close();
                htcm1.Close();
            }

            RegistryKey smcoss1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object smcoss2 = smcoss1.GetValue("StartMemoryCleanerOnSystemStartup");
            if (smcoss2 != null)
            {
            }
            else
            {
                RegistryKey smcoss3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                smcoss3.SetValue("StartMemoryCleanerOnSystemStartup", "0");
                smcoss3.Close();
                smcoss1.Close();
            }

            RegistryKey sm1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object sm2 = sm1.GetValue("StartMinimized");
            if (sm2 != null)
            {
            }
            else
            {
                RegistryKey sm3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                sm3.SetValue("StartMinimized", "0");
                sm3.Close();
                sm1.Close();
            }

            RegistryKey stra1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object stra2 = stra1.GetValue("StartTimerResolutionAutomatically");
            if (stra2 != null)
            {
            }
            else
            {
                RegistryKey stra3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                stra3.SetValue("StartTimerResolutionAutomatically", "0");
                stra3.Close();
                stra1.Close();
            }

            RegistryKey te1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object te2 = te1.GetValue("TimerEnabled");
            if (te2 != null)
            {
            }
            else
            {
                RegistryKey te3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                te3.SetValue("TimerEnabled", "1");
                te3.Close();
                te3.Close();
            }

            RegistryKey tpi1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object tpi2 = tpi1.GetValue("TimerPollingInterval");
            if (tpi2 != null)
            {
            }
            else
            {
                RegistryKey tpi3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                tpi3.SetValue("TimerPollingInterval", "1 sec");
                tpi3.Close();
                tpi1.Close();
            }

            RegistryKey ddtr1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ddtr2 = ddtr1.GetValue("DesiredTimerResolution");
            if (ddtr2 != null)
            {
                SettingsForm.DesiredTimerResolution.Value = Convert.ToInt32(ddtr2);
                ddtr1.Close();
            }
            else
            {
                SettingsForm.DesiredTimerResolution.Value = 5000;
                ddtr1.Close();
            }

            RegistryKey ddtr = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            ddtr.SetValue("DesiredTimerResolution", SettingsForm.DesiredTimerResolution.Value);
            ddtr.Close();

            RegistryKey eecotsl1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object eecotsl2 = eecotsl1.GetValue("EnableClearingOfTheStandbyList");
            if (eecotsl2.ToString() == "1")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = true;
                eecotsl1.Close();
            }
            else if (eecotsl2.ToString() == "0")
            {
                SettingsForm.CheckBoxEnableClearingOfTheStandbyList.Checked = false;
                eecotsl1.Close();
            }

            RegistryKey eectr1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object eectr2 = eectr1.GetValue("EnableCustomTimerResolution");
            if (eectr2.ToString() == "1")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = true;
                eectr1.Close();
            }
            else if (eectr2.ToString() == "0")
            {
                SettingsForm.CheckBoxEnableCustomTimerResolution.Checked = false;
                eectr1.Close();
            }

            RegistryKey eeeotws1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object eeeotws2 = eeeotws1.GetValue("EnableEmptyingOfTheWorkingSet");
            if (eeeotws2.ToString() == "1")
            {
                SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked = true;
                eeeotws1.Close();
            }
            else if (eeeotws2.ToString() == "0")
            {
                SettingsForm.CheckBoxEnableEmptyingOfTheWorkingSet.Checked = false;
                eeeotws1.Close();
            }

            RegistryKey hhmk1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object hhmk2 = hhmk1.GetValue("HotkeyModifierKey");
            if (hhmk2 != null)
            {
                SettingsForm.HotkeyModifierKey.Text = hhmk2.ToString();
                hhmk1.Close();
            }
            else
            {
                SettingsForm.HotkeyModifierKey.Text = "None";
                hhmk1.Close();
            }

            RegistryKey hmk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            hmk.SetValue("HotkeyModifierKey", SettingsForm.HotkeyModifierKey.Text);
            hmk.Close();

            RegistryKey hhtcm1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object hhtcm2 = hhtcm1.GetValue("HotkeyToCleanMemory");
            if (hhtcm2 != null)
            {
                SettingsForm.HotkeyToCleanMemory.Text = hhtcm2.ToString();
                hhtcm1.Close();
            }
            else
            {
                SettingsForm.HotkeyToCleanMemory.Text = "F10";
                hhtcm1.Close();
            }

            RegistryKey htcm = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            htcm.SetValue("HotkeyToCleanMemory", SettingsForm.HotkeyToCleanMemory.Text);
            htcm.Close();

            RegistryKey ssmcoss1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ssmcoss2 = ssmcoss1.GetValue("StartMemoryCleanerOnSystemStartup");
            if (ssmcoss2.ToString() == "1")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = true;
                ssmcoss1.Close();
                RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"");
            }
            else if (ssmcoss2.ToString() == "0")
            {
                SettingsForm.CheckBoxStartMemoryCleanerOnSystemStartup.Checked = false;
                ssmcoss1.Close();
                RegistryKey a1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                Object a = a1.GetValue("Memory Cleaner");
                if (a != null)
                {
                    RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    startup.DeleteValue("Memory Cleaner");
                }
                else
                {
                }
            }

            RegistryKey ssm1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ssm2 = ssm1.GetValue("StartMinimized");
            if ((ssm2.ToString()) == "1")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = true;
                ssm1.Close();
                Minimize();
            }
            else if ((ssm2.ToString()) == "0")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = false;
                ssm1.Close();
                Maximize();
            }

            RegistryKey sstra1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object sstra2 = sstra1.GetValue("StartTimerResolutionAutomatically");
            if (sstra2.ToString() == "1")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = true;
                sstra1.Close();
                SetTimerRes();
            }
            else if (sstra2.ToString() == "0")
            {
                SettingsForm.CheckBoxStartTimerResolutionAutomatically.Checked = false;
                sstra1.Close();
                UnSetTimerRes();
            }

            RegistryKey tte1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object tte2 = tte1.GetValue("TimerEnabled");
            if (tte2.ToString() == "1")
            {
                SettingsForm.CheckBoxEnableTimer.Checked = true;
                tte1.Close();
            }
            else if (tte2.ToString() == "0")
            {
                SettingsForm.CheckBoxEnableTimer.Checked = false;
                tte1.Close();
            }

            RegistryKey ttpi1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object ttpi2 = ttpi1.GetValue("TimerPollingInterval");
            if (ttpi2 != null)
            {
                SettingsForm.TimerPollingInterval.Text = ttpi2.ToString();
                ttpi1.Close();
            }
            else
            {
                SettingsForm.TimerPollingInterval.Text = "1 sec";
                ttpi1.Close();
            }

            RegistryKey ttpi = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            ttpi.SetValue("TimerPollingInterval", SettingsForm.TimerPollingInterval.Text);
            ttpi.Close();

            SaveSettings();
        }

        // Buttons ------------------------------------------------------------------------------------ //

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
            AboutForm.ShowDialog(this);
            UpdateValues();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            SetTimerRes();
            UpdateValues();
            CurrentTimerRes.Focus();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            UnSetTimerRes();
            UpdateValues();
            CurrentTimerRes.Focus();
        }

        private void ButtonCleanMemory_Click(object sender, EventArgs e)
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
            CurrentTimerRes.Focus();
        }

        private void SystemTrayIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            Maximize();
            UpdateValues();
        }

        // Functions ------------------------------------------------------------------------------------ //

        public void MainForm_Load(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void MainForm_Resize(object sender, EventArgs e)
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit(sender, e);
            UpdateValues();
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
                        RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                        startup.SetValue("Memory Cleaner", "\"" + Application.ExecutablePath + "\"");
                        break;
                    }
                case false:
                    {
                        RegistryKey a1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                        Object a = a1.GetValue("Memory Cleaner");
                        if (a != null)
                        {
                            RegistryKey startup = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                            startup.DeleteValue("Memory Cleaner");
                        }
                        else
                        {
                        }
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

            CurrentTimerRes.Focus();
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

        private void Exit(Object sender, EventArgs e)
        {
            Application.Exit();
            UnregisterHotKey(this.Handle, 1);
        }

        public ulong GetAvailableMemory()
        {
            MEMORYSTATUSEX statex = new MEMORYSTATUSEX();
            statex.dwLength = 64;
            bool a = GlobalMemoryStatusEx(out statex);
            return statex.ullAvailPhys;
        }

        public ulong GetTotalMemory()
        {
            MEMORYSTATUSEX statex = new MEMORYSTATUSEX();
            statex.dwLength = 64;
            bool a = GlobalMemoryStatusEx(out statex);
            return statex.ullTotalPhys;
        }

        private void SetTimerRes()
        {
            uint DesiredResolution = (Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value));
            bool SetResolution = true;
            uint CurrentResolution = 156250;
            NtSetTimerResolution(DesiredResolution, SetResolution, ref CurrentResolution);
        }

        private void UnSetTimerRes()
        {
            uint DesiredResolution = (Convert.ToUInt32(SettingsForm.DesiredTimerResolution.Value));
            bool SetResolution = false;
            uint CurrentResolution = 156250;
            NtSetTimerResolution(DesiredResolution, SetResolution, ref CurrentResolution);
        }

        private Timer GetTimerRes()
        {
            var a = new Timer();
            var result = NtQueryTimerResolution(out a.PeriodMin, out a.PeriodMax, out a.PeriodCurrent);
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

        public void RegisterHotkey()
        {
            switch (SettingsForm.HotkeyToCleanMemory.Text)
            {
                case "F1":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F1.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F1.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F1.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F1.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F2":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F2.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F2.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F2.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F2.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F3":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F3.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F3.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F3.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F3.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F4":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F4.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F4.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F4.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F4.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F5":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F5.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F5.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F5.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F5.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F6":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F6.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F6.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F6.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F6.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F7":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F7.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F7.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F7.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F7.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F8":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F8.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F8.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F8.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F8.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F9":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F9.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F9.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F9.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F9.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F10":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F10.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F10.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F10.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F10.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "F11":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F11.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F11.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F11.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F11.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "Home":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.Home.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.Home.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.Home.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.Home.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "Insert":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.Insert.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.Insert.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.Insert.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.Insert.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "PageUp":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.PageUp.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.PageUp.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.PageUp.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.PageUp.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "PageDown":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.PageDown.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.PageDown.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.PageDown.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.PageDown.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }

                case "CapsLock":
                    {
                        switch (SettingsForm.HotkeyModifierKey.Text)
                        {
                            case "None":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.CapsLock.GetHashCode());
                                    break;
                                }
                            case "Alt":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.CapsLock.GetHashCode());
                                    break;
                                }
                            case "Ctrl":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.CapsLock.GetHashCode());
                                    break;
                                }
                            case "Shift":
                                {
                                    UnregisterHotKey(this.Handle, 1);
                                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.CapsLock.GetHashCode());
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        public void Timer()
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

        private void EmptyWorkingSet()
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

        private void ClearStandbyList()
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
            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateValues();
        }
    }
}