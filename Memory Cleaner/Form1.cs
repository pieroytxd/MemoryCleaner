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
using System.Net.Http.Headers;

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
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
        }

        public Form1()
        {
            InitializeComponent();
            InitializeRAMCounter();

            comboBox1.Items.Add("None");
            comboBox1.Items.Add("Alt");
            comboBox1.Items.Add("Ctrl");
            comboBox1.Items.Add("Shift");

            comboBox2.Items.Add("F1");
            comboBox2.Items.Add("F2");
            comboBox2.Items.Add("F3");
            comboBox2.Items.Add("F4");
            comboBox2.Items.Add("F5");
            comboBox2.Items.Add("F6");
            comboBox2.Items.Add("F7");
            comboBox2.Items.Add("F8");
            comboBox2.Items.Add("F9");
            comboBox2.Items.Add("F10");
            comboBox2.Items.Add("F11");
            comboBox2.Items.Add("Home");
            comboBox2.Items.Add("Insert");
            comboBox2.Items.Add("PageUp");
            comboBox2.Items.Add("PageDown");
            comboBox2.Items.Add("CapsLock");

            comboBox3.Items.Add("0.5s");
            comboBox3.Items.Add("1s");
            comboBox3.Items.Add("2s");
            comboBox3.Items.Add("5s");
            comboBox3.Items.Add("10s");

            comboBox4.Items.Add("True");
            comboBox4.Items.Add("False");

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

            RegistryKey key18 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object i = key18.GetValue("HotkeyModifier");
            if (i != null)
            {
            }
            else
            {
                RegistryKey key19 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key19.SetValue("HotkeyModifier", "None");
                key19.Close();
                key18.Close();
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

            RegistryKey key22 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object l = key22.GetValue("TimerPollingInterval");
            if (l != null)
            {
            }
            else
            {
                RegistryKey key23 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key23.SetValue("TimerPollingInterval", "1s");
                key23.Close();
                key22.Close();
            }

            RegistryKey key24 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object m = key24.GetValue("TimerEnabled");
            if (m != null)
            {
            }
            else
            {
                RegistryKey key25 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
                key25.SetValue("TimerEnabled", "True");
                key25.Close();
                key24.Close();
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
                comboBox2.Text = (f.ToString());
                key12.Close();
            }
            else
            {
                comboBox2.Text = "F10";
                key12.Close();
            }

            RegistryKey key13 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key13.SetValue("Hotkey", comboBox2.Text);
            key13.Close();

            RegistryKey key14 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object g = key14.GetValue("HotkeyModifier");
            if (g != null)
            {
                comboBox1.Text = (g.ToString());
                key14.Close();
            }
            else
            {
                comboBox1.Text = "None";
                key14.Close();
            }

            RegistryKey key15 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key15.SetValue("HotkeyModifier", comboBox1.Text);
            key15.Close();

            RegistryKey key16 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object h = key16.GetValue("AutoStart");
            if ((h.ToString()) == "1")
            {
                checkBox1.Checked = true;
                key16.Close();
                NtSetTimerResolution();
            }
            else if ((h.ToString()) == "0")
            {
                checkBox1.Checked = false;
                key16.Close();
                NtUnSetTimerResolution();
            }

            RegistryKey key17 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object j = key17.GetValue("Hide");
            if ((j.ToString()) == "1")
            {
                checkBox2.Checked = true;
                key17.Close();
                this.FormBorderStyle = FormBorderStyle.None;
                this.ShowInTaskbar = false;
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
            else if ((j.ToString()) == "0")
            {
                checkBox2.Checked = false;
                key17.Close();
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ShowInTaskbar = true;
                this.Show();
            }

            RegistryKey key20 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object k = key20.GetValue("TimerPollingInterval");
            if (k != null)
            {
                comboBox3.Text = (k.ToString());
                key20.Close();
            }
            else
            {
                comboBox3.Text = "1s";
                key20.Close();
            }

            RegistryKey key21 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key21.SetValue("TimerPollingInterval", comboBox3.Text);
            key21.Close();

            RegistryKey key26 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            Object n = key26.GetValue("TimerEnabled");
            if (n != null)
            {
                comboBox4.Text = (n.ToString());
                key26.Close();
            }
            else
            {
                comboBox3.Text = "True";
                key26.Close();
            }

            RegistryKey key27 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key27.SetValue("TimerEnabled", comboBox4.Text);
            key27.Close();

            HotkeyCheck();
            TimerCheck();
            textBox2.Text = Convert.ToInt32(ramC.NextValue()).ToString() + " MB";
            textBox5.Text = (NtQueryTimerResolution().PeriodCurrent / 10000.0) + " ms";
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
            string HK = comboBox2.Text;
            string HKM = comboBox1.Text;

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

        private void TimerCheck()
        {
            string T = comboBox3.Text;
            string TE = comboBox4.Text;

            if (timer1.Enabled == true)
            {
                if (T == "0.5s")
                {
                    timer1.Interval = 500;
                }
                else if (T == "1s")
                {
                    timer1.Interval = 1000;
                }
                else if (T == "2s")
                {
                    timer1.Interval = 2000;
                }
                else if (T == "5s")
                {
                    timer1.Interval = 5000;
                }
                else if (T == "10s")
                {
                    timer1.Interval = 10000;
                }
            }
            else if (timer1.Enabled == false)
            {
            }

            if (TE == "True")
            {
                timer1.Enabled = true;
            }
            else if (TE == "False")
            {
                timer1.Enabled = false;
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = true;
            this.Show();
            HotkeyCheck();
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
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            HotkeyCheck();
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
            key2.SetValue("Hotkey", comboBox2.Text);
            key2.Close();

            RegistryKey key7 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key7.SetValue("HotkeyModifier", comboBox1.Text);
            key7.Close();

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

            RegistryKey key8 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key8.SetValue("TimerPollingInterval", comboBox3.Text);
            key8.Close();

            RegistryKey key9 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Memory Cleaner\Settings");
            key9.SetValue("TimerEnabled", comboBox4.Text);
            key9.Close();

            HotkeyCheck();
            TimerCheck();
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
            textBox2.Text = Convert.ToInt32(ramC.NextValue()).ToString() + " MB";
            textBox5.Text = (NtQueryTimerResolution().PeriodCurrent / 10000.0) + " ms";
        }
    }
}