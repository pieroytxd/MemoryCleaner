/* Credits:
 * https://stackoverflow.com/questions/750574/how-to-get-memory-available-or-used-in-c-sharp
 * https://github.com/tebjan/TimerTool/blob/master/Sources/WinApiCalls.cs
 * https://www.fluxbytes.com/csharp/how-to-register-a-global-hotkey-for-your-application-in-c/
 * https://gallery.technet.microsoft.com/scriptcenter/c-PowerShell-wrapper-6465e028
 * https://csharp.developreference.com/article/24067983/Clear+the+windows+7+standby+memory+programmatically
 */

using System;
using System.IO;
using System.Management;
using System.Timers;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing.Text;

namespace Memory_Cleaner
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Timer
    {
        public uint PeriodMin;
        public uint PeriodMax;
        public uint PeriodCurrent;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct SYSTEM_CACHE_INFORMATION_64_BIT
    {
        public long CurrentSize;
        public long PeakSize;
        public long PageFaultCount;
        public long MinimumWorkingSet;
        public long MaximumWorkingSet;
        public long Unused1;
        public long Unused2;
        public long Unused3;
        public long Unused4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokPriv1Luid
    {
        public int Count;
        public long Luid;
        public int Attr;
    }

    public partial class Form1 : Form
    {
        private PerformanceCounter ramC;

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern UInt32 NtSetSystemInformation(int InfoClass, IntPtr Info, int Length);

        [DllImport("psapi.dll", SetLastError = true)]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
        }

        public Form1()
        {
            InitializeComponent();
            InitializeRAMCounter();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.Close();

            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object a = key2.GetValue("DesiredTimerRes");
            if (a != null)
            {
            }
            else
            {
                RegistryKey key3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key3.SetValue("DesiredTimerRes", "5000");
                key3.Close();
                key2.Close();
            }

            RegistryKey key4 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object b = key4.GetValue("Hotkey");
            if (b != null)
            {
            }
            else
            {
                RegistryKey key5 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key5.SetValue("Hotkey", "F10");
                key5.Close();
                key4.Close();
            }

            RegistryKey key6 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object c = key6.GetValue("AutoStart");
            if (c != null)
            {
            }
            else
            {
                RegistryKey key7 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key7.SetValue("AutoStart", "0");
                key7.Close();
                key6.Close();
            }

            RegistryKey key8 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object d = key8.GetValue("Hide");
            if (d != null)
            {
            }
            else
            {
                RegistryKey key9 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key9.SetValue("Hide", "0");
                key9.Close();
                key8.Close();
            }

            RegistryKey key10 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object e = key10.GetValue("DesiredTimerRes");
            if (e != null)
            {
                textBox7.Text = (e.ToString());
                key10.Close();
            }
            else
            {
                textBox7.Text = "5000";
                key10.Close();
            }

            RegistryKey key11 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key11.SetValue("DesiredTimerRes", textBox7.Text);
            key11.Close();

            RegistryKey key12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object f = key12.GetValue("Hotkey");
            if (f != null)
            {
                textBox9.Text = (f.ToString());
                key12.Close();
            }
            else
            {
                textBox9.Text = "F10";
                key12.Close();
            }

            RegistryKey key13 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key13.SetValue("Hotkey", textBox9.Text);
            key13.Close();

            RegistryKey key14 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object g = key14.GetValue("AutoStart");
            if ((g.ToString()) == "1")
            {
                checkBox1.Checked = true;
                key14.Close();
                NtSetTimerResolution();
            }
            else if ((g.ToString()) == "0")
            {
                checkBox1.Checked = false;
                key14.Close();
                NtUnSetTimerResolution();
            }

            RegistryKey key15 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object h = key15.GetValue("Hide");
            if ((h.ToString()) == "1")
            {
                checkBox2.Checked = true;
                key15.Close();
                this.WindowState = FormWindowState.Minimized;
            }
            else if ((h.ToString()) == "0")
            {
                checkBox2.Checked = false;
                key15.Close();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void NtSetTimerResolution()
        {
            uint DesiredResolution = (Convert.ToUInt32(textBox7.Text));
            bool SetResolution = true;
            uint CurrentResolution = 156250;
            NtSetTimerResolution(DesiredResolution, SetResolution, ref CurrentResolution);
        }

        private void NtUnSetTimerResolution()
        {
            uint DesiredResolution = 156250;
            bool SetResolution = false;
            uint CurrentResolution = 156250;
            NtSetTimerResolution(DesiredResolution, SetResolution, ref CurrentResolution);
        }

        public static Timer NtQueryTimerResolution()
        {
            var a = new Timer();
            var result = NtQueryTimerResolution(out a.PeriodMin, out a.PeriodMax, out a.PeriodCurrent);
            return a;
        }

        private void HotkeyCheck()
        {
            string HK = textBox9.Text;

            if (HK == "F1")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F1.GetHashCode());
            }
            else if (HK == "F2")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F2.GetHashCode());
            }
            else if (HK == "F3")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F3.GetHashCode());
            }
            else if (HK == "F4")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F4.GetHashCode());
            }
            else if (HK == "F5")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F5.GetHashCode());
            }
            else if (HK == "F6")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F6.GetHashCode());
            }
            else if (HK == "F7")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F7.GetHashCode());
            }
            else if (HK == "F8")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F8.GetHashCode());
            }
            else if (HK == "F9")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F9.GetHashCode());
            }
            else if (HK == "F10")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F10.GetHashCode());
            }
            else if (HK == "F11")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F11.GetHashCode());
            }
            else if (HK == "F12")
            {
                UnregisterHotKey(this.Handle, 1);
                RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F12.GetHashCode());
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
                    if (NtSetSystemInformation(80, gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(4)) != 0)
                    {
                        throw new Exception("NtSetSystemInformation: ", new Win32Exception(Marshal.GetLastWin32Error()));
                    }
                    gcHandle.Free();
                }
                if (SetIncreasePrivilege("SeProfileSingleProcessPrivilege"))
                {
                    GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    if (NtSetSystemInformation(0x0050, gcHandle.AddrOfPinnedObject(), Marshal.SizeOf(4)) != 0)
                    {
                        throw new Exception("NtSetSystemInformation: ", new Win32Exception(Marshal.GetLastWin32Error()));
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
                int num = AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0;
                return num != 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HotkeyCheck();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            NtSetTimerResolution();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            NtUnSetTimerResolution();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ButtonCleanupsystemmemory_Click(object sender, EventArgs e)
        {
            ClearStandbyList();
            EmptyWorkingSet();
        }

        private void ButtonSaveSettings_Click(object sender, EventArgs e)
        {
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key1.SetValue("DesiredTimerRes", textBox7.Text);
            key1.Close();

            RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key2.SetValue("Hotkey", textBox9.Text);
            key2.Close();

            if (checkBox1.Checked == true)
            {
                RegistryKey key3 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key3.SetValue("AutoStart", "1");
                key3.Close();
            }
            else if (checkBox1.Checked == false)
            {
                RegistryKey key4 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key4.SetValue("AutoStart", "0");
                key4.Close();
            }

            if (checkBox2.Checked == true)
            {
                RegistryKey key5 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key5.SetValue("Hide", "1");
                key5.Close();
            }
            else if (checkBox2.Checked == false)
            {
                RegistryKey key6 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key6.SetValue("Hide", "0");
                key6.Close();
            }

            HotkeyCheck();
        }

        private void ButtonTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/danskexd");
        }

        private void ButtonGitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/danskee");
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
            UnregisterHotKey(this.Handle, 1);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, 1);
        }

        private void InitializeRAMCounter()
        {
            ramC = new PerformanceCounter("Memory", "Available MBytes", true);
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

        void Timer1Tick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.ShowInTaskbar = false;
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ShowInTaskbar = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            textBox2.Text = Convert.ToInt32(ramC.NextValue()).ToString() + " MB";
            textBox5.Text = (NtQueryTimerResolution().PeriodCurrent / 10000.0) + " ms";
        }
    }
}