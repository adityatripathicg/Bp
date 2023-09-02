using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memory;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace Bp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Memory
        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);

        private async Task PutTaskDelay(int Time)
        {
            await Task.Delay(Time);
        }

        private string sr;
        public long enumerable = new long();
        private int x;
        public Mem MemLib = new Mem();
        private static string string_0;
        private static string string_1;
        private static string string_2;
        private static string string_3;
        private static string string_4;
        private static string string_5;
        private static string string_6;
        private IContainer icontainer_0;

        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        public struct ProcessEntry64
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th64ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th64ModuleID;
            public uint cntThreads;
            public uint th64ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
        [Flags]

        public enum ThreadAccess
        {

            TERMINATE = 1,

            SUSPEND_RESUME = 2,

            GET_CONTEXT = 8,

            SET_CONTEXT = 16,

            SET_INFORMATION = 32,

            QUERY_INFORMATION = 64,

            SET_THREAD_TOKEN = 128,

            IMPERSONATE = 256,

            DIRECT_IMPERSONATION = 512
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(Form1.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        // Token: 0x06000011 RID: 17
        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);

        // Token: 0x06000012 RID: 18
        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);

        // Token: 0x06000013 RID: 19

        int timer = 0;



        #endregion
        #region mouse
        private bool mouseDown;
        private Point lastLocation;
        private int correctCounter;
        private int notWritten;

        private void Bypass_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Bypass_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Bypass_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
        #endregion


        string pathDriver = @"C:\cg.sys";
        private void Form1_Load(object sender, EventArgs e)
        {
            CommandLine("net stop cg");
            CommandLine("sc delete cg");
            File.WriteAllBytes(pathDriver, Properties.Resources.Driver);
            CommandLine("sc create cg binPath=" + pathDriver + " start=demand type=filesys");
            CommandLine("net start cg");
            CommandLine("sc start cg");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                status.Text = "Select Emulator";
            }
            else
            {


                try
                {
                    foreach (Process proc in Process.GetProcessesByName("AppMarket"))
                    {
                        proc.Kill();
                        proc.Kill();
                    }
                    foreach (Process proc in Process.GetProcessesByName("AppMarket.exe"))
                    {
                        proc.Kill();
                        proc.Kill();
                    }
                    foreach (Process proc in Process.GetProcessesByName("AndroidEmulatorEx.exe"))
                    {
                        proc.Kill();
                        proc.Kill();
                    }
                    foreach (Process proc in Process.GetProcessesByName("AndroidEmulatorEx"))
                    {
                        proc.Kill();
                        proc.Kill();
                    }
                }
                catch { }


                if (comboBox1.SelectedIndex == 0)
                {
                    try
                    {
                        string start = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Tencent\MobileGamePC\UI", "InstallPath", "").ToString();
                          Process.Start(Path.Combine(start) + "/AndroidEmulatorEx.exe", "-vm 100");
                        

                        status.Text = "emulator started";
                        status.ForeColor = Color.White;
                    }
                    catch
                    {
                        status.Text = "Please Retry Starting Emulator!!";
                    }

                }
            }



        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/GUD5wkuwJr");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        public void CommandLine(string arg)
        {

            Process process = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + @"\cmd.exe";
            startInfo.Arguments = "/c" + arg;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                GetProcID();
            } 
           
        }

        private async Task<IntPtr> GetProcID()
        {

            var intPtr = IntPtr.Zero;
            uint num = 0U;
            var intPtr2 = CreateToolhelp32Snapshot(2U, 0U);
            if ((int)intPtr2 > 0)
            {
                ProcessEntry32 processEntry = default;
                processEntry.dwSize = (uint)Marshal.SizeOf(processEntry);
                int num2 = Process32First(intPtr2, ref processEntry);
                while (num2 == 1)
                {
                    var intPtr3 = Marshal.AllocHGlobal((int)processEntry.dwSize);
                    Marshal.StructureToPtr(processEntry, intPtr3, true);
                    ProcessEntry32 processEntry2 = (ProcessEntry32)Marshal.PtrToStructure(intPtr3, typeof(ProcessEntry32));
                    Marshal.FreeHGlobal(intPtr3);

                    if (processEntry2.szExeFile.Contains("aow_exe") && processEntry2.cntThreads > num)
                    {
                        num = processEntry2.cntThreads;
                        intPtr = (IntPtr)((long)(unchecked((ulong)processEntry2.th32ProcessID)));
                    }

                    num2 = Process32Next(intPtr2, ref processEntry);
                }
                pid.Text = Convert.ToString(intPtr);
                // check1();
                //startgame();
                //Thread.Sleep(10000);
                ld();

            }

            return intPtr;
        }

        public static void SuspendProcess(int pid)
        {
            Process processById = Process.GetProcessById(pid);
            if (processById.ProcessName == string.Empty)
            {
                return;
            }

            foreach (ProcessThread thread in processById.Threads)
            {
                IntPtr intPtr = Imps.OpenThread(Imps.ThreadAccess.SUSPEND_RESUME, bInheritHandle: false, checked((uint)thread.Id));
                if (!(intPtr == IntPtr.Zero))
                {
                    Imps.SuspendThread(intPtr);
                    Imps.CloseHandle(intPtr);
                }
            }
        }

        public static void ResumeProcess(int pid)
        {
            Process processById = Process.GetProcessById(pid);
            if (processById.ProcessName == string.Empty)
            {
                return;
            }

            foreach (ProcessThread thread in processById.Threads)
            {
                IntPtr intPtr = Imps.OpenThread(Imps.ThreadAccess.SUSPEND_RESUME, bInheritHandle: false, checked((uint)thread.Id));
                if (!(intPtr == IntPtr.Zero))
                {
                    int num = 0;
                    do
                    {
                        num = Imps.ResumeThread(intPtr);
                    }
                    while (num > 0);
                    Imps.CloseHandle(intPtr);
                }
            }
        }


        public async void Rep(string original, string replace)
        {
            try
            {
                SuspendProcess(Convert.ToInt32(pid.Text));
                this.MemLib.OpenProcess(Convert.ToInt32(pid.Text));
                IEnumerable<long> scanmem = await this.MemLib.AoBScan(0L, 140737488355327L, original, true, true);
                long FirstScan = scanmem.FirstOrDefault();
                if (FirstScan == 0)
                {
                    status.Text = "Open Emulator First";
                    ResumeProcess(Convert.ToInt32(pid.Text));
                }
                else
                {
                    status.Text = "Applying...";
                }
                foreach (long num in scanmem)
                {
                    this.MemLib.ChangeProtection(num.ToString("X"), Imps.MemoryProtection.ReadWrite, out Imps.MemoryProtection _);
                    this.MemLib.WriteMemory(num.ToString("X"), "bytes", replace);

                }
                if (FirstScan == 0)
                {
                    status.Text = "Error!";
                    ResumeProcess(Convert.ToInt32(pid.Text));
                }
                else
                {
                    scanmem = (IEnumerable<long>)null;
                    status.Text = "All Done, Enjoy";
                    ResumeProcess(Convert.ToInt32(pid.Text));
                    startgame();
                }
            }
            catch
            {
            }
        }

        public async void check1()
        {
            MemLib.OpenProcess(Convert.ToInt32(pid.Text));
            //SuspendProcess(Convert.ToInt32(PID.Text));
            bool g = false;

            
            string all = "01 2C";
            var enumerable = await MemLib.AoBScan(0000000000000000, 0x00007fffffffffff, all, true, true,"");
            string_0 = "0x" + enumerable.FirstOrDefault<long>().ToString("X");
            foreach (long Libtrsafelong in enumerable)
            {
                g = true;
            }
            if (g == true)
            {
                status.Text = "Bypassing....";
                ld();


            }
            else
            {

                status.Text = "Now Start Game Manually! and wait for done!";
                GetProcID();
            }
        }

        private async void ld()
        {
            bool ok = false;
            MemLib.OpenProcess(Convert.ToInt32(pid.Text));

            #region 11
            string Aob1 = "7F 45 4C 46 01 01 01 00 00 00 00 00 00 00 00 00 03 00 28 00 01 00 00 00 00 00 00 00 34 00 00 00 C0 FA 3D 00 00 02 00 05 34 00 20 00 08 00 28 00 1D 00 1C 00 06 00 00 00 34 00 00 00 34 00 00 00 34 00 00 00 00 01 00 00 00 01 00 00 04 00 00 00 04 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 25 3A 00 00 25 3A 00 05 00 00 00 00 10 00 00 01 00 00 00 BC 31 3A 00 BC 41 3A 00 BC 41 3A 00 B8 C4 03 00 B4 F5 04 00 06 00 00 00 00 10 00 00 02 00 00 00 D8 94 3A 00 D8 A4 3A 00 D8 A4 3A 00 20 01 00 00 20 01 00 00 06 00 00 00 04 00 00 00 04 00 00 00 34 01 00 00 34 01 00 00 34 01 00 00 BC 00 00 00 BC 00 00 00 04 00 00 00 04 00 00 00 51 E5 74 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 06 00 00 00 10 00 00 00 01 00 00 70 00 43 32 00 00 43 32 00 00 43 32 00 28 A8 01 00 28 A8 01 00 04 00 00 00 04 00 00 00 52 E5 74 64 BC 31 3A 00 BC 41 3A 00 BC 41 3A 00 44 6E 00 00 44 6E 00 00 06 00 00 00 04 00 00 00 08 00 00 00 84 00 00 00 01 00 00 00 41 6E 64 72 6F 69 64 00 10 00 00 00 72 32 31 65";
            #endregion //header

            var enumerable = await MemLib.AoBScan(0000000000000000, 0x00007fffffffffff, Aob1, true, true, true, "");
            string_0 = "0x" + enumerable.FirstOrDefault<long>().ToString("X");


            foreach (long Libtrsafelong in enumerable)
            {
                //Rep("01 2C 0A D1 3B 49 02 AC 79 44", "00 00");
                ok = true;
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x4398A).ToString("X"), "bytes", "00 20 70 47 2D E9 00 07 AD F5 82 6D 0A 29 30 D1");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x34E90).ToString("X"), "bytes", "00 20 70 47 15 E1 D9 F8 00 00 D9 F8 04 10 13 F0");
                //to avoid 2 month ban
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x46B74).ToString("X"), "bytes", "00 20 70 47 4D F8 04 BD 82 B0 91 B3 05 46 08 68");//case 34
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x60B5C).ToString("X"), "bytes", "00 20 70 47 4D F8 04 BD 82 B0 00 F2 14 46 04 46");//case 37
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x34814).ToString("X"), "bytes", "00 20 70 47 2D E9 00 07 DC B0 89 46 01 46 CD 48");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x6E51C).ToString("X"), "bytes", "00 20 70 47 4D F8 04 BD A1 B3 05 46 08 78 0C 46");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x6C862).ToString("X"), "bytes", "00 20 70 47 84 B0 0C 46 50 B3 05 46 00 78 38 B3");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x46B74).ToString("X"), "bytes", "00 20 70 47 4D F8 04 BD 82 B0 91 B3 05 46 08 68");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x34F2A).ToString("X"), "bytes", "59 00 00 0F 1C BF 99 F8 00 00 00 28 41 D1 4F F0");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x30D88).ToString("X"), "bytes", "00 00 B0 E3 1E FF 2F E1 E8 FF BC E5 03 C6 8F E2");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x30ECC).ToString("X"), "bytes", "00 00 B0 E3 1E FF 2F E1 10 FF BC E5 03 C6 8F E2");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x31880).ToString("X"), "bytes", "00 20 70 47 01 50 A0 E1 7C 12 91 E5 44 D0 4D E2");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x34814).ToString("X"), "bytes", "00 20 70 47 2D E9 00 07 DC B0 89 46 01 46 CD 48");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x60A34).ToString("X"), "bytes", "00 20 70 47 4D F8 04 8D 04 46 4F F0 FF 30 F1 B3");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x67CFC).ToString("X"), "bytes", "00 20 70 47 4D F8 04 BD 82 B0 14 46 06 46 00 20");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0xAC7DC).ToString("X"), "bytes", "00 20 70 47 08 B0 8D E2 40 D0 4D E2 E0 20 9F E5");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x261818).ToString("X"), "bytes", "00 20 70 47 02 AF 09 4D 4F F0 FF 32 7D 44 10 F8");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x60B38).ToString("X"), "bytes", "00 20 70 47 70 47 51 B1 0A 46 D1 F8 02 10 31 B1");
                MemLib.WriteMemory("0x" + (Libtrsafelong + 0x30A28).ToString("X"), "bytes", "00 20 70 47 7A CA 8C E2 28 F2 BC E5 03 C6 8F E2");

            }
            if (ok)
            {
                status.Text = ("bypass done!");
            }
            else
            {
                status.Text = ("bypass Failed!");
            }
        }


        private void gameloopex()
        {
            var intPtr = IntPtr.Zero;
            uint num = 0U;
            var intPtr2 = CreateToolhelp32Snapshot(2U, 0U);
            if ((int)intPtr2 > 0)
            {
                ProcessEntry32 processEntry = default;
                processEntry.dwSize = (uint)Marshal.SizeOf(processEntry);
                int num2 = Process32First(intPtr2, ref processEntry);
                while (num2 == 1)
                {
                    var intPtr3 = Marshal.AllocHGlobal((int)processEntry.dwSize);
                    Marshal.StructureToPtr(processEntry, intPtr3, true);
                    ProcessEntry32 processEntry2 = (ProcessEntry32)Marshal.PtrToStructure(intPtr3, typeof(ProcessEntry32));
                    Marshal.FreeHGlobal(intPtr3);
                    // AndroidProcess [( Use Aow_exe to run it on gameloop, but must use island tick to prevent offline ban)]




                    if (processEntry2.szExeFile.Contains("AndroidEmulatorEx.exe") && processEntry2.cntThreads > num)
                    {
                        num = processEntry2.cntThreads;
                        intPtr = (IntPtr)(long)(ulong)processEntry2.th32ProcessID;


                    }


                    num2 = Process32Next(intPtr2, ref processEntry);
                }
                pid.Text = Convert.ToString(intPtr);
                patch();
            }

        }

        private void patch()
        {
            status.Text = "patching";
            MemLib.OpenProcess(Convert.ToInt32(pid.Text));
            MemLib.WriteMemory("0x" + (0x00479B75).ToString("X"), "bytes", "C2 08 00", "", null, true);
            status.Text = "patched";
           

        }

        private void startgame()
        {
            if(comboBox2.SelectedIndex == 0) //bgmi
            {
                status.Text = "starting BGMI...";
                CommandLine("adb shell am kill com.pubg.imobile");
                CommandLine("adb shell am force-stop com.pubg.imobile");
                CommandLine("adb shell am start -n com.pubg.imobile/com.epicgames.ue4.SplashActivity filter");
                //Thread.Sleep(3000);
                //CommandLine("adb shell chmod 777 /data/data/com.pubg.imobile/lib");
                //nsystem("adb.exe -s emulator-5554 shell push C:\BYPASS_BGMI.so /System/data/user/0/com.pubg.imobile/lib/BYPASS_BGMI.so");
                //nsystem("adb rm /data/user/0/com.pubg.imobile/lib/BYPASS_BGMI.so");  adb -s emulator-5554 shell chmod *777
                // CommandLine("adb.exe -s emulator-5554 shell push C:\\BYPASS_BGMI.so /System/data/user/0/com.pubg.imobile/lib/BYPASS_BGMI.so");
               //CommandLine("adb shell push C:\\BYPASS.so /storage/emulated/0/Download/BYPASS.so");
                //CommandLine("adb rm /data/user/0/com.pubg.imobile/lib/BYPASS_BGMI.so");
                CommandLine("adb kill-server");
                status.Text = "started!";
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("AppMarket"))
                {
                    proc.Kill();
                    proc.Kill();
                }
                foreach (Process proc in Process.GetProcessesByName("AppMarket.exe"))
                {
                    proc.Kill();
                    proc.Kill();
                }
                foreach (Process proc in Process.GetProcessesByName("AndroidEmulatorEx.exe"))
                {
                    proc.Kill();
                    proc.Kill();
                }
                foreach (Process proc in Process.GetProcessesByName("AndroidEmulatorEx"))
                {
                    proc.Kill();
                    proc.Kill();
                }
            }
            catch { }
            status.Text = ("Killed!");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            startgame();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
