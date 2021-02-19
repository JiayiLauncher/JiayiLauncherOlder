using System;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Http;
using DiscordRPC;
using System.Linq;
using System.Xml;
using System.Drawing;
using System.Management.Automation;
using System.IO.Compression;

namespace JiayiLauncher
{
    
    public partial class Jiayi : Form
    {
        public DiscordRpcClient client;
        string discordTime = "";

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public Jiayi()
        {
            InitializeComponent();
            InitializeDiscord("In Launcher");
            versionFinderForLabel("Get-AppPackage -name Microsoft.MinecraftUWP | select -expandproperty Version", VersionDisplay);
            Directory.CreateDirectory(@"c:\Jiayi");
        }

        private void Jiayi_Load(object sender, EventArgs e)
        {

        }

        // RPC Functions

        public void InitializeDiscord(string status)
        {
            int TimestampStart = 0;
            int TimestampEnd = 0;
            dynamic DateTimestampEnd = null;

            if (discordTime != "" && Int32.TryParse(discordTime, out TimestampEnd))
                DateTimestampEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampEnd);

            client = new DiscordRpcClient("812363424071548970");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = status,

                Assets = new Assets()
                {

                    LargeImageKey = "logonewdiscord",
                    LargeImageText = "Jiayi Launcher",
                },
                Timestamps = new Timestamps()
                {
                    Start = discordTime != "" && Int32.TryParse(discordTime, out TimestampStart) ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampStart) : DateTime.UtcNow,
                    End = DateTimestampEnd
                }

            });
        }

        public void RPCForBtns(string status)  // made seperate rpc so that it doesnt initiate a new rpc client
        {
            int TimestampStart = 0;
            int TimestampEnd = 0;
            dynamic DateTimestampEnd = null;

            if (discordTime != "" && Int32.TryParse(discordTime, out TimestampEnd))
                DateTimestampEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampEnd);

            client.SetPresence(new RichPresence()
            {
                Details = status,

                Assets = new Assets()
                {

                    LargeImageKey = "logonewdiscord",
                    LargeImageText = "Jiayi Launcher",
                },
                Timestamps = new Timestamps()
                {
                    Start = discordTime != "" && Int32.TryParse(discordTime, out TimestampStart) ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampStart) : DateTime.UtcNow,
                    End = DateTimestampEnd
                }

            });
        }

        public void RPCInGame(string status)  // made seperate rpc so that it doesnt initiate a new rpc client
        {
            int TimestampStart = 0;
            int TimestampEnd = 0;
            dynamic DateTimestampEnd = null;

            if (discordTime != "" && Int32.TryParse(discordTime, out TimestampEnd))
                DateTimestampEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampEnd);

            client.SetPresence(new RichPresence()
            {
                Details = status,
                State = "on " + VersionDisplay.Text,

                Assets = new Assets()
                {

                    LargeImageKey = "logonewdiscord",
                    LargeImageText = "Jiayi Launcher",
                    SmallImageKey = "minecraft",
                    SmallImageText = "Minecraft Bedrock Edition"
                },
                Timestamps = new Timestamps()
                {
                    Start = discordTime != "" && Int32.TryParse(discordTime, out TimestampStart) ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampStart) : DateTime.UtcNow,
                    End = DateTimestampEnd
                }

            });
        }

        // Version stuff

        private void Version_Click(object sender, EventArgs e)
        {

        }

        public static void versionFinderForLabel(string script, Label version)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);
                powerShell.AddCommand("Out-String");
                Collection<PSObject> PSOutput = powerShell.Invoke();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject pSObject in PSOutput)
                    stringBuilder.AppendLine(pSObject.ToString());
                version.Text = stringBuilder.ToString();
            }
        }

        // All Side Panel Functions 

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = true;
            HomePanel.Visible = true;

            SettingsBtn.Checked = false;
            SettingsPanel.Visible = false;

            UpdateBtn.Checked = false;


            CosmeticsBtn.Checked = false;


            RPCForBtns("In Launcher");

            TopPanel.Text = ("Home");
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = true;
            SettingsPanel.Visible = true;

            UpdateBtn.Checked = false;


            CosmeticsBtn.Checked = false;


            RPCForBtns("Configuring Settings");

            SettingsPanel.Visible = true;
            TopPanel.Text = ("Settings");
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = false;
            SettingsPanel.Visible = false;

            UpdateBtn.Checked = true;


            CosmeticsBtn.Checked = false;


            RPCForBtns("In Launcher");

            TopPanel.Text = ("Updates");
        }

        private void CosmeticsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = false;
            SettingsPanel.Visible = false;

            UpdateBtn.Checked = false;


            CosmeticsBtn.Checked = true;
            

            RPCForBtns("In Cosmetics Menu");

            TopPanel.Text = ("Cosmetics");
        }

        // Minimize and Close Btn Functions
        private void MinimizeBtn_MouseHover(object sender, EventArgs e)
        {
            MinimizeBtn.Checked = true;
            MinimizeBtn.Location = new Point(792, 7);
        }

        private void ExitBtn_MouseHover(object sender, EventArgs e)
        {
            ExitBtn.Checked = true;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.Checked = false;
        }

        private void MinimizeBtn_MouseLeave(object sender, EventArgs e)
        {
            MinimizeBtn.Checked = false;
            MinimizeBtn.Location = new Point(792, 12);
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // All home screen functions

        private void LaunchBtn_MouseHover(object sender, EventArgs e) // hover enlarge effect
        {
            LaunchBtn.Size = new Size(351, 78);
            LaunchBtn.Location = new Point(175, 317);
        }

        private void LaunchBtn_MouseLeave(object sender, EventArgs e) // undo enlarge effect when mouse stops hovering on btn
        {
            LaunchBtn.Size = new Size(333, 74);
            LaunchBtn.Location = new Point(185, 317);
        }

        private void LaunchBtn_Click(object sender, EventArgs e)
        {
            Settings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Minecraft.Windows");
            if (pname.Length == 0)
            {
                if (HomeBtn.Checked == true)
                {
                    RPCForBtns("In Launcher");
                    timer1.Stop();
                }

                else if (SettingsBtn.Checked == true)
                {
                    RPCForBtns("Configuring Settings");
                    timer1.Stop();
                }

                else if (UpdateBtn.Checked == true)
                {
                    RPCForBtns("In Launcher");
                    timer1.Stop();
                }

                else if (CosmeticsBtn.Checked == true)
                {
                    RPCForBtns("In Cosmetics Menu");
                    timer1.Stop();
                }
            }
                
            else
            {
            }
                
        }

        private void VersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // here, a base for version switching

            //WebClient Downloader = new WebClient();
            //if (VersionComboBox.SelectedItem.ToString() == "1.16.40")
            //{
            //    Downloader.DownloadFile(new Uri("https://github.com/xarson/JiayiLauncher/releases/download/1.16.40/Minecraft-1.16.40.2.Appx"), 
            //        @"c:\Jiayi\Minecraft-1.16.40.2.zip");
            //    Directory.CreateDirectory(@"c:\Jiayi\Minecraft-1.16.40.2");
            //    ZipFile.ExtractToDirectory(@"c:\Jiayi\Minecraft-1.16.40.2.zip", @"c:\Jiayi\Minecraft-1.16.40.2");
            //}
        }

        // ALL settings functions

        private void Settings()
        {
            Process.Start("minecraft://");
            if (CloseLauncher.Checked == true)
            {
                Process[] pname = Process.GetProcessesByName("Minecraft.Windows");
                if (pname.Length == 0)
                {
                    InjectDLL();
                }

                else
                {
                    this.Close();
                }

            }

            if (HideLauncher.Checked == true)
            {
                NotifyIcon.Visible = true;
                InjectDLL();
            }

            if (KeepOpen.Checked == true)
            {
                InjectDLL();
            }
        }

        private void CloseLauncher_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = true;
            HideLauncher.Checked = false;
            KeepOpen.Checked = false;
        }

        private void HideLauncher_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = false;
            HideLauncher.Checked = true;
            KeepOpen.Checked = false;
        }

        private void KeepOpen_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = false;
            HideLauncher.Checked = false;
            KeepOpen.Checked = true;
        }

        //Load Settings At Launch

        public void InjectDLL()
        {
            RPCInGame("Playing Minecraft");
            timer1.Start();


            Thread.Sleep(200);

            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");
            foreach (Process proc in processes)
            if (ProcessPriorityBox.SelectedItem == "High")
                {
                    proc.PriorityClass = ProcessPriorityClass.RealTime;
                }

            else if (ProcessPriorityBox.SelectedItem == "Medium")
                {
                    proc.PriorityClass = ProcessPriorityClass.AboveNormal;
                }
            else if (ProcessPriorityBox.SelectedItem == "Low")
                {
                    proc.PriorityClass = ProcessPriorityClass.BelowNormal;
                }

            Process[] MinecraftIndex = Process.GetProcessesByName("Minecraft.Windows");
            if (MinecraftIndex.Length > 0)
            {
                Process Minecraft = Process.GetProcessesByName("Minecraft.Windows")[0];
                if (ResolutionComboBox.SelectedItem == "1920x1080")
                {
                    MoveWindow(Minecraft.MainWindowHandle, 0, 0, 1920, 1080, true);
                }
                else if (ResolutionComboBox.SelectedItem == "1280x720")
                {
                    MoveWindow(Minecraft.MainWindowHandle, 0, 0, 1280, 720, true);
                }
            }
        }
    }
}
