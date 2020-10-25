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

            RegistryKey a = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            a.Close();

            RegistryKey key1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k1 = key1.GetValue("DesiredTimerRes");
            if (k1 != null)
            {
            }
            else
            {
                RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key2.SetValue("DesiredTimerRes", "5000");
                key2.Close();
                key1.Close();
            }

            RegistryKey key3 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k3 = key3.GetValue("Hotkey");
            if (k3 != null)
            {
            }
            else
            {
                RegistryKey key4 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key4.SetValue("Hotkey", "F10");
                key4.Close();
                key3.Close();
            }

            RegistryKey key5 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k5 = key5.GetValue("HotkeyModifier");
            if (k5 != null)
            {
            }
            else
            {
                RegistryKey key6 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key6.SetValue("HotkeyModifier", "None");
                key6.Close();
                key5.Close();
            }

            RegistryKey key7 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k7 = key7.GetValue("AutoStart");
            if (k7 != null)
            {
            }
            else
            {
                RegistryKey key8 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key8.SetValue("AutoStart", "0");
                key8.Close();
                key7.Close();
            }

            RegistryKey key9 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k9 = key9.GetValue("Hide");
            if (k9 != null)
            {
            }
            else
            {
                RegistryKey key10 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key10.SetValue("Hide", "0");
                key10.Close();
                key9.Close();
            }

            RegistryKey key11 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k11 = key11.GetValue("TimerPollingInterval");
            if (k11 != null)
            {
            }
            else
            {
                RegistryKey key12 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key12.SetValue("TimerPollingInterval", "1 sec");
                key12.Close();
                key11.Close();
            }

            RegistryKey key13 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k13 = key13.GetValue("TimerEnabled");
            if (k13 != null)
            {
            }
            else
            {
                RegistryKey key14 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key14.SetValue("TimerEnabled", "True");
                key14.Close();
                key13.Close();
            }

            RegistryKey key15 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k15 = key15.GetValue("DesiredTimerRes");
            if (k15 != null)
            {
                SettingsForm.DesiredTimerResolution.Value = Convert.ToInt32(k15);
                key15.Close();
            }
            else
            {
                SettingsForm.DesiredTimerResolution.Value = 5000;
                key15.Close();
            }

            RegistryKey key16 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key16.SetValue("DesiredTimerRes", SettingsForm.DesiredTimerResolution.Value);
            key16.Close();

            RegistryKey key17 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k17 = key17.GetValue("Hotkey");
            if (k17 != null)
            {
                SettingsForm.HotkeyToCleanMemory.Text = (k17.ToString());
                key17.Close();
            }
            else
            {
                SettingsForm.HotkeyToCleanMemory.Text = "F10";
                key17.Close();
            }

            RegistryKey key18 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key18.SetValue("Hotkey", SettingsForm.HotkeyToCleanMemory.Text);
            key18.Close();

            RegistryKey key19 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k19 = key19.GetValue("HotkeyModifier");
            if (k19 != null)
            {
                SettingsForm.HotkeyModifierKey.Text = (k19.ToString());
                key19.Close();
            }
            else
            {
                SettingsForm.HotkeyModifierKey.Text = "None";
                key19.Close();
            }

            RegistryKey key20 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key20.SetValue("HotkeyModifier", SettingsForm.HotkeyModifierKey.Text);
            key20.Close();

            RegistryKey key21 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k21 = key21.GetValue("AutoStart");
            if ((k21.ToString()) == "0")
            {
                SettingsForm.CheckBoxStartAutomatically.Checked = false;
                key21.Close();
                UnSetTimerRes();
            }
            else if ((k21.ToString()) == "1")
            {
                SettingsForm.CheckBoxStartAutomatically.Checked = true;
                key21.Close();
                SetTimerRes();
            }

            RegistryKey key22 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k22 = key22.GetValue("Hide");
            if ((k22.ToString()) == "0")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = false;
                key22.Close();
                Maximize();
            }
            else if ((k22.ToString()) == "1")
            {
                SettingsForm.CheckBoxStartMinimized.Checked = true;
                key22.Close();
                Minimize();
            }

            RegistryKey key23 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k23 = key23.GetValue("TimerPollingInterval");
            if (k23 != null)
            {
                SettingsForm.TimerPollingInterval.Text = (k23.ToString());
                key23.Close();
            }
            else
            {
                SettingsForm.TimerPollingInterval.Text = "1 sec";
                key23.Close();
            }

            RegistryKey key24 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key24.SetValue("TimerPollingInterval", SettingsForm.TimerPollingInterval.Text);
            key24.Close();

            RegistryKey key25 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k25 = key25.GetValue("TimerEnabled");
            if (k25 != null)
            {
                SettingsForm.TimerEnabled.Text = (k25.ToString());
                key25.Close();
            }
            else
            {
                SettingsForm.TimerEnabled.Text = "True";
                key25.Close();
            }

            RegistryKey key26 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key26.SetValue("TimerEnabled", SettingsForm.TimerEnabled.Text);
            key26.Close();

            HotkeyCheck();
            TimerCheck();
        }

        // Buttons ------------------------------------------------------------------------------------ //

        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void MenuItemSettings_Click(object sender, EventArgs e)
        {
            SettingsForm.ShowDialog(this);

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutForm.ShowDialog(this);

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            SetTimerRes();

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            UnSetTimerRes();

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void ButtonCleanMemory_Click(object sender, EventArgs e)
        {
            ClearStandbyList();
            EmptyWorkingSet();

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void SystemTrayIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            Maximize();
            HotkeyCheck();

            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        // Functions ------------------------------------------------------------------------------------ //

        public void MainForm_Load(object sender, EventArgs e)
        {
            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                Minimize();
                HotkeyCheck();
            }
            else
            {
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit();
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

        private void Exit()
        {
            Application.Exit();
            UnregisterHotKey(this.Handle, 1);
        }

        public void SaveSettings()
        {
            HotkeyCheck();
            TimerCheck();
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

        public void HotkeyCheck()
        {
            string HK = SettingsForm.HotkeyToCleanMemory.Text;
            string HKM = SettingsForm.HotkeyModifierKey.Text;

            if (HK == "F1")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F1.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F1.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F1.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F1.GetHashCode());
                }
            }

            if (HK == "F2")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F2.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F2.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F2.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F2.GetHashCode());
                }
            }

            if (HK == "F3")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F3.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F3.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F3.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F3.GetHashCode());
                }
            }

            if (HK == "F4")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F4.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F4.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F4.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F4.GetHashCode());
                }
            }

            if (HK == "F5")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F5.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F5.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F5.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F5.GetHashCode());
                }
            }

            if (HK == "F6")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F6.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F6.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F6.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F6.GetHashCode());
                }
            }

            if (HK == "F7")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F7.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F7.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F7.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F7.GetHashCode());
                }
            }

            if (HK == "F8")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F8.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F8.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F8.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F8.GetHashCode());
                }
            }

            if (HK == "F9")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F9.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F9.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F9.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F9.GetHashCode());
                }
            }

            if (HK == "F10")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F10.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F10.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F10.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F10.GetHashCode());
                }
            }

            if (HK == "F11")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F11.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.F11.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.F11.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.F11.GetHashCode());
                }
            }

            if (HK == "Home")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.Home.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.Home.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.Home.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.Home.GetHashCode());
                }
            }

            if (HK == "Insert")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.Insert.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.Insert.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.Insert.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.Insert.GetHashCode());
                }
            }

            if (HK == "PageUp")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.PageUp.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.PageUp.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.PageUp.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.PageUp.GetHashCode());
                }
            }

            if (HK == "PageDown")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.PageDown.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.PageDown.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.PageDown.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.PageDown.GetHashCode());
                }
            }

            if (HK == "CapsLock")
            {
                if (HKM == "None")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.CapsLock.GetHashCode());
                }
                else if (HKM == "Alt")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Alt, Keys.CapsLock.GetHashCode());
                }
                else if (HKM == "Ctrl")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Ctrl, Keys.CapsLock.GetHashCode());
                }
                else if (HKM == "Shift")
                {
                    UnregisterHotKey(this.Handle, 1);
                    RegisterHotKey(this.Handle, 1, (int)KeyModifier.Shift, Keys.CapsLock.GetHashCode());
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

        public void TimerCheck()
        {
            string T = SettingsForm.TimerPollingInterval.Text;
            string TE = SettingsForm.TimerEnabled.Text;

            if (Timer.Enabled == true)
            {
                if (T == "0.5 sec")
                {
                    Timer.Interval = 500;
                }
                else if (T == "1 sec")
                {
                    Timer.Interval = 1000;
                }
                else if (T == "2 sec")
                {
                    Timer.Interval = 2000;
                }
                else if (T == "5 sec")
                {
                    Timer.Interval = 5000;
                }
                else if (T == "10 sec")
                {
                    Timer.Interval = 10000;
                }
            }
            else if (Timer.Enabled == false)
            {
            }
            else
            {
            }

            if (TE == "True")
            {
                Timer.Enabled = true;
            }
            else if (TE == "False")
            {
                Timer.Enabled = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                ClearStandbyList();
                EmptyWorkingSet();
            }
        }

        // Timer ------------------------------------------------------------------------------------ //

        void TimerTick(object sender, EventArgs e)
        {
            AvailableMemory.Text = "Available: " + (GetAvailableMemory() / 1048576) + " MB";
            TotalMemory.Text = "Total: " + (GetTotalMemory() / 1048576) + " MB";
            CurrentTimerRes.Text = "Current: " + (GetTimerRes().PeriodCurrent / 10000.0) + " ms";
            MaxTimerRes.Text = "Max: " + (GetTimerRes().PeriodMax / 10000.0) + " ms";
            MinTimerRes.Text = "Min: " + (GetTimerRes().PeriodMin / 10000.0) + " ms";
        }
    }
}