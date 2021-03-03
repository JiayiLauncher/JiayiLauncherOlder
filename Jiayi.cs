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
using Windows.Management.Deployment;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Management.Core;

namespace JiayiLauncher
{

    public partial class Jiayi : Form
    {
        public DiscordRpcClient client;
        string discordTime = "";
        private string xboxName;
        private string xboxIconLink;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public Jiayi()
        {
            InitializeComponent();
            InitializeDiscord();
            versionFinderForLabel("Get-AppPackage -name Microsoft.MinecraftUWP | select -expandproperty Version", VersionDisplay);
            Directory.CreateDirectory(@"c:\Jiayi");
            XboxInfo();
        }

        private void Jiayi_Load(object sender, EventArgs e)
        {
            NewsfeedLoader(); // gets data for newsfeed

            // launch settings
            if (Properties.Settings.Default.AfterLaunch == "Hide")
            {
                HideLauncher.Checked = true;
            }
            else if (Properties.Settings.Default.AfterLaunch == "Close")
            {
                CloseLauncher.Checked = true;
            }
            else if (Properties.Settings.Default.AfterLaunch == "KeepOpen")
            {
                KeepOpen.Checked = true;
            }

            // resolution settings
            ResolutionComboBox.SelectedItem = Properties.Settings.Default.Resolution;

            // process priority settings
            ProcessPriorityBox.SelectedItem = Properties.Settings.Default.Priority;

            // branch settings
            if (Properties.Settings.Default.Branch == "Stable")
            {
                StableSettingBtn.Checked = true;
            }
            else if (Properties.Settings.Default.Branch == "Experimental")
            {
                ExpirementalSettingBtn.Checked = true;
            }

            // color settings (warning: sucks)
            TopPanel.HoverState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            TopPanel.CustomBorderColor = Properties.Settings.Default.AccentColor;
            HomeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            SettingsBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            UpdatePanelBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            CosmeticsBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            CloseLauncher.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            LogoLabel.ForeColor = Properties.Settings.Default.AccentColor;
            HideLauncher.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            KeepOpen.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            StableSettingBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ExpirementalSettingBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ThemesButton.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ResolutionComboBox.FocusedState.BorderColor = Properties.Settings.Default.AccentColor;
            ProcessPriorityBox.FocusedState.BorderColor = Properties.Settings.Default.AccentColor;
            LightThemeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            DarkThemeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            xboxGamertag.ForeColor = Properties.Settings.Default.AccentColor;
            RpcIgnBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            RpcSrverBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;

            AccentColorBtn.FillColor = Properties.Settings.Default.AccentColor;

            if (Properties.Settings.Default.LightDarkMode == "Light")
            {
                LightThemeBtn.Checked = true;
                LightTheme();
            }
            else
            {
                DarkThemeBtn.Checked = true;
                DarkTheme();
            }

            // bg image settings
            try
            {
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                this.BackgroundImage = Image.FromFile(Properties.Settings.Default.BackImagePath);
            }
            catch (ArgumentException)
            {
                BackImageCheckBox.Checked = false;
                // shit i guess...
            }

            VersionDisplay.BackColor = Color.Transparent;

            // rpc settings
            if (Properties.Settings.Default.RpcMode == "Server")
            {
                RpcSrverBtn.Checked = true;
            }
            else
            {
                RpcIgnBtn.Checked = true;
            }
        }

        public void XboxInfo()
        {
            string localappdata = Environment.GetEnvironmentVariable("LocalAppData");

            if (System.IO.File.Exists("C:\\jiayi\\XboxLiveGamer.xml.txt"))
                System.IO.File.Delete("C:\\jiayi\\XboxLiveGamer.xml.txt");
            if (System.IO.File.Exists(localappdata + "\\Packages\\Microsoft.XboxApp_8wekyb3d8bbwe\\LocalState\\XboxLiveGamer.xml"))
            {
                try
                {
                    PowerShell.Create().AddCommand("Copy-Item").AddParameter("Path", (object)(localappdata + "\\Packages\\Microsoft.XboxApp_8wekyb3d8bbwe\\LocalState\\XboxLiveGamer.xml")).AddParameter("Destination", (object)"C:\\jiayi\\XboxLiveGamer.xml.txt").Invoke();
                    foreach (string readAllLine in System.IO.File.ReadAllLines("C:\\jiayi\\XboxLiveGamer.xml.txt"))
                    {
                        if (readAllLine.Contains("Gamertag"))
                            this.xboxName = readAllLine;
                        else if (readAllLine.Contains("DisplayPic"))
                            this.xboxIconLink = readAllLine;
                    }
                    this.xboxName = this.xboxName.Replace("\"Gamertag\"", "");
                    this.xboxName = this.xboxName.Replace("\"", "");
                    this.xboxName = this.xboxName.Replace(": ", "");
                    this.xboxName = this.xboxName.Replace(",", "");
                    xboxGamertag.Text = xboxName;
                    this.xboxIconLink = this.xboxIconLink.Replace("\"DisplayPic\"", "");
                    this.xboxIconLink = this.xboxIconLink.Replace("\"", "");
                    this.xboxIconLink = this.xboxIconLink.Replace(": ", "");
                    this.xboxIconLink = this.xboxIconLink.Replace(",", "");
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.xboxIconCompleted);
                    webClient.DownloadFileAsync(new Uri(this.xboxIconLink), "C:\\jiayi\\icon.png");
                }

                catch (ArgumentException)
                {
                    if (System.IO.File.Exists(localappdata + "\\Packages\\Microsoft.XboxApp_8wekyb3d8bbwe\\LocalState\\XboxLiveGamer.xml"))
                        xboxGamertag.Text = "TRY IN A HOUR";
                    else
                        xboxGamertag.Text = "InstallXboxCompanionApp";
                }
            }
        }

        private void xboxIconCompleted(object sender, AsyncCompletedEventArgs e)
        {
            xboxIcon.Image = (Image)new Bitmap("C:\\jiayi\\icon.png");
        }

        public void DarkTheme()
        { 
            /*WebRequest requestdark = WebRequest.Create("https://github.com/xarson/jiayi/raw/master/Images/LightHomeScreen.png");
            Stream stream = requestdark.GetResponse().GetResponseStream();
            System.Drawing.Image imgdark = System.Drawing.Image.FromStream(stream);
            this.HomePic.Image = imgdark;
            */

            BtnPanel.BackColor = Color.FromArgb(25, 24, 26);
            TopPanel.ForeColor = Color.FromArgb(255, 255, 255);
            HomeBtn.BackColor = Color.FromArgb(25, 24, 26);
            HomeBtn.CustomBorderColor = Color.FromArgb(25, 24, 26);
            HomeBtn.BorderColor = Color.FromArgb(25, 24, 26);
            VersionPanel.BackColor = Color.FromArgb(15, 15, 15);
            HomeBtn.FillColor = Color.FromArgb(25, 24, 26);
            HomeBtn.CheckedState.FillColor = Color.FromArgb(23, 23, 23);
            HomeBtn.CheckedState.BorderColor = Color.FromArgb(34, 35, 32);
            HomeBtn.ForeColor = Color.FromArgb(255, 255, 255);
            SettingsBtn.BackColor = Color.FromArgb(25, 24, 26);
            SettingsBtn.CustomBorderColor = Color.FromArgb(25, 24, 26);
            SettingsBtn.BorderColor = Color.FromArgb(25, 24, 26);
            SettingsBtn.CheckedState.FillColor = Color.FromArgb(23, 23, 23);
            SettingsBtn.FillColor = Color.FromArgb(25, 24, 26);
            SettingsBtn.ForeColor = Color.FromArgb(255, 255, 255);
            SettingsBtn.CheckedState.BorderColor = Color.FromArgb(34, 35, 32);
            UpdatePanelBtn.BackColor = Color.FromArgb(25, 24, 26);
            UpdatePanelBtn.CustomBorderColor = Color.FromArgb(25, 24, 26);
            UpdatePanelBtn.BorderColor = Color.FromArgb(25, 24, 26);
            UpdatePanelBtn.CheckedState.FillColor = Color.FromArgb(23, 23, 23);
            UpdatePanelBtn.ForeColor = Color.FromArgb(255, 255, 255);
            UpdatePanelBtn.FillColor = Color.FromArgb(25, 24, 26);
            UpdatePanelBtn.CheckedState.BorderColor = Color.FromArgb(34, 35, 32);
            CosmeticsBtn.BackColor = Color.FromArgb(25, 24, 26);
            CosmeticsBtn.CustomBorderColor = Color.FromArgb(25, 24, 26);
            CosmeticsBtn.FillColor = Color.FromArgb(25, 24, 26);
            CosmeticsBtn.ForeColor = Color.FromArgb(255, 255, 255);
            CosmeticsBtn.BorderColor = Color.FromArgb(25, 24, 26);
            CosmeticsBtn.CheckedState.FillColor = Color.FromArgb(23, 23, 23);
            CosmeticsBtn.CheckedState.BorderColor = Color.FromArgb(34, 35, 32);
            this.BackColor = Color.FromArgb(15, 15, 15);
        }

        public void LightTheme()
        {
            // kinda broken and annoying to fit
            /*WebRequest light = WebRequest.Create("https://raw.githubusercontent.com/xarson/jiayi/master/Images/DarkHomeScreen.png");
            Stream stream = light.GetResponse().GetResponseStream();
            System.Drawing.Image imglight = System.Drawing.Image.FromStream(stream);
            this.HomePic.Image = imglight;
            */
            
            BtnPanel.BackColor = Color.FromArgb(232, 232, 232);
            TopPanel.ForeColor = Color.FromArgb(15, 15, 15);
            HomeBtn.BackColor = Color.FromArgb(232, 232, 232);
            HomeBtn.CustomBorderColor = Color.FromArgb(232, 232, 232);
            HomeBtn.BorderColor = Color.FromArgb(232, 232, 232);
            HomeBtn.FillColor = Color.FromArgb(232, 232, 232);
            HomeBtn.CheckedState.FillColor = Color.FromArgb(150, 150, 150);
            HomeBtn.CheckedState.BorderColor = Color.FromArgb(190, 190, 190);
            HomeBtn.ForeColor = Color.FromArgb(15, 15, 15);
            VersionPanel.BackColor = Color.FromArgb(232, 232, 232);
            SettingsBtn.BackColor = Color.FromArgb(232, 232, 232);
            SettingsBtn.CustomBorderColor = Color.FromArgb(232, 232, 232);
            SettingsBtn.BorderColor = Color.FromArgb(232, 232, 232);
            SettingsBtn.CheckedState.FillColor = Color.FromArgb(150, 150, 150);
            SettingsBtn.FillColor = Color.FromArgb(232, 232, 232);
            SettingsBtn.ForeColor = Color.FromArgb(15, 15, 15);
            SettingsBtn.CheckedState.BorderColor = Color.FromArgb(190, 190, 190);
            UpdatePanelBtn.BackColor = Color.FromArgb(232, 232, 232);
            UpdatePanelBtn.CustomBorderColor = Color.FromArgb(232, 232, 232);
            UpdatePanelBtn.BorderColor = Color.FromArgb(232, 232, 232);
            UpdatePanelBtn.CheckedState.FillColor = Color.FromArgb(150, 150, 150);
            UpdatePanelBtn.ForeColor = Color.FromArgb(15, 15, 15);
            UpdatePanelBtn.FillColor = Color.FromArgb(232, 232, 232);
            UpdatePanelBtn.CheckedState.BorderColor = Color.FromArgb(190, 190, 190);
            CosmeticsBtn.BackColor = Color.FromArgb(232, 232, 232);
            CosmeticsBtn.CustomBorderColor = Color.FromArgb(232, 232, 232);
            CosmeticsBtn.FillColor = Color.FromArgb(232, 232, 232);
            CosmeticsBtn.ForeColor = Color.FromArgb(15, 15, 15);
            CosmeticsBtn.BorderColor = Color.FromArgb(232, 232, 232);
            CosmeticsBtn.CheckedState.FillColor = Color.FromArgb(150, 150, 150);
            CosmeticsBtn.CheckedState.BorderColor = Color.FromArgb(190, 190, 190);
            this.BackColor = Color.FromArgb(170, 170, 170);
        }

        // RPC Functions

        public void InitializeDiscord()
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
                Details = "In Launcher",

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
            try
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

            catch (ArgumentException)
            {
                string message = "Is Minecraft Bedrock Installed?";
                string caption = "Error Detected While Finding Version Info";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }

        }


        // All Side Panel Functions 

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = true;
            HomePanel.Visible = true;
            HomePanel.BringToFront();
            VersionPanel.Visible = false;

            SettingsBtn.Checked = false;
            SettingsPanel.Visible = false;

            UpdatePanelBtn.Checked = false;
            UpdatePanel.Visible = false;

            CosmeticsBtn.Checked = false;

            ThemesPanel.Visible = false;

            RPCForBtns("In Launcher");

            TopPanel.Text = ("Home");
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = true;
            SettingsPanel.Visible = true;
            SettingsPanel.BringToFront();
            VersionPanel.Visible = false;

            UpdatePanelBtn.Checked = false;
            UpdatePanel.Visible = false;

            CosmeticsBtn.Checked = false;

            ThemesPanel.Visible = false;

            RPCForBtns("Configuring Settings");

            SettingsPanel.Visible = true;
            TopPanel.Text = ("Settings");
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = false;
            VersionPanel.Visible = false;
            SettingsPanel.Visible = false;

            UpdatePanelBtn.Checked = true;
            UpdatePanel.Visible = true;
            UpdatePanel.BringToFront();

            CosmeticsBtn.Checked = false;

            ThemesPanel.Visible = false;

            RPCForBtns("Looking At Newsfeed");

            TopPanel.Text = ("Updates");
        }

        private void CosmeticsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            HomePanel.Visible = false;

            SettingsBtn.Checked = false;
            SettingsPanel.Visible = false;
            VersionPanel.Visible = false;

            UpdatePanelBtn.Checked = false;
            UpdatePanel.Visible = false;

            CosmeticsBtn.Checked = true;

            ThemesPanel.Visible = false;

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
            client.Dispose();
            NotifyIcon.Visible = false;
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

        private void StatusText_Click(object sender, EventArgs e)
        {

        }

        private void LaunchBtn_MouseHover(object sender, EventArgs e) // hover enlarge effect
        {
            // no
            //LaunchBtn.Size = new Size(351, 78);
            //LaunchBtn.Location = new Point(175, 317);
        }

        private void LaunchBtn_MouseLeave(object sender, EventArgs e) // undo enlarge effect when mouse stops hovering on btn
        {
            //LaunchBtn.Size = new Size(333, 74);
            //LaunchBtn.Location = new Point(185, 317);
        }

        private void LaunchBtn_Click(object sender, EventArgs e)
        {
            Status.Visible = true;
            StatusText.Visible = true;
            // just gonna leave this here
            //string mcversion = Version.Text.Remove(0, 8);
            //Console.WriteLine(mcversion);
            StatusText.Text = ("Preparing Injection Process..");
            Settings();
        }


        //version changer function


        // all settings functions

        private void SettingsPanel_Click(object sender, EventArgs e)
        {

        }

        private void ExpirementalSettingBtn_Click(object sender, EventArgs e)
        {
            StableSettingBtn.Checked = false;
            ExpirementalSettingBtn.Checked = true;
            Properties.Settings.Default.Branch = "Experimental";
            Properties.Settings.Default.Save();
        }

        private void StableSettingBtn_Click(object sender, EventArgs e)
        {
            StableSettingBtn.Checked = false;
            ExpirementalSettingBtn.Checked = true;
            /*Properties.Settings.Default.Branch = "Stable";
            Properties.Settings.Default.Save();
            */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Minecraft.Windows");
            if (pname.Length == 0)
            {
                Status.Visible = false;
                StatusText.Visible = false;

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

                else if (UpdatePanelBtn.Checked == true)
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

        private void Settings()
        {
            Process.Start("minecraft://");
            if (CloseLauncher.Checked == true)
            {
                Process[] pname = Process.GetProcessesByName("Minecraft.Windows");
                if (pname.Length == 0)
                {
                    StatusText.Text = ("Preparing Settings");
                    MoreSettings();

                }

                else
                {
                    StatusText.Text = ("Finalizing Process Before Closing...");
                    MoreSettings();
                    Thread.Sleep(500);

                    this.Close();
                }

            }

            if (HideLauncher.Checked == true)
            {
                if (NotifyIcon.Visible == false)
                    NotifyIcon.Visible = true;
                else
                    NotifyIcon.Visible = true;
                Status.Visible = false;
                this.Hide();
                MoreSettings();
            }

            if (KeepOpen.Checked == true)
            {
                StatusText.Text = ("Preparing Settings");
                MoreSettings();
                Status.Visible = false;
            }
        }

        private void CloseLauncher_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = true;
            HideLauncher.Checked = false;
            KeepOpen.Checked = false;
            Properties.Settings.Default.AfterLaunch = "Close";
            Properties.Settings.Default.Save();
        }

        private void HideLauncher_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = false;
            HideLauncher.Checked = true;
            KeepOpen.Checked = false;
            Properties.Settings.Default.AfterLaunch = "Hide";
            Properties.Settings.Default.Save();
        }

        private void KeepOpen_Click(object sender, EventArgs e)
        {
            CloseLauncher.Checked = false;
            HideLauncher.Checked = false;
            KeepOpen.Checked = true;
            Properties.Settings.Default.AfterLaunch = "KeepOpen";
            Properties.Settings.Default.Save();
        }

        public void MoreSettings()
        {
            if (RpcIgnBtn.Checked == true)
            {
                try
                {
                    RPCInGame("IGN:" + xboxName);
                }
                catch (ArgumentException)
                {
                    string message = "Trouble Finding Username";
                    string caption = "Error Detected While Finding User, Try Installing Xbox Companion App";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
            }

            else
            {
                RPCInGame("");
            }
            timer1.Start();

            //InjectDLL(Directory.GetCurrentDirectory().ToString() + "/JiayiClient.dll");
            Thread.Sleep(200);

            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");
            foreach (Process proc in processes)
                if (ProcessPriorityBox.SelectedItem == "High")
                {
                    proc.PriorityClass = ProcessPriorityClass.High;
                }

                else if (ProcessPriorityBox.SelectedItem == "Medium")
                {
                    proc.PriorityClass = ProcessPriorityClass.Normal;
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
                else if (ResolutionComboBox.SelectedItem == "1600x900")
                {
                    MoveWindow(Minecraft.MainWindowHandle, 0, 0, 1600, 900, true);
                }
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NotifyIcon.Visible = false;
            this.Show();
        }



        //Inject DLL into game

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        public static void InjectDLL(string DLLPath)
        {
            Process[] targetProcessIndex = Process.GetProcessesByName("Minecraft.Windows");
            if (targetProcessIndex.Length > 0)
            {
                applyAppPackages(DLLPath);

                Process targetProcess = Process.GetProcessesByName("Minecraft.Windows")[0];

                IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, targetProcess.Id);

                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);

                UIntPtr bytesWritten;
                WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(DLLPath), (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
                CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

            }
        }

        public static void applyAppPackages(string DLLPath)
        {
            FileInfo InfoFile = new FileInfo(DLLPath);
            FileSecurity fSecurity = InfoFile.GetAccessControl();
            fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"), FileSystemRights.FullControl, InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            InfoFile.SetAccessControl(fSecurity);
        }

        // Update Panel Functions

        private void UpdateBtn_Click_1(object sender, EventArgs e)
        {
            UpdateForm updateForm = new UpdateForm();
            updateForm.Show();
        }

        private void UpdateBtn_MouseHover(object sender, EventArgs e)
        {
            // this sucks, default animations on top
            //UpdateBtn.Size = new Size(352, 74);
            //UpdateBtn.Location = new Point(175, 317);
        } // location 175, 322

        private void UpdateBtn_MouseLeave(object sender, EventArgs e)
        {
            //UpdateBtn.Size = new Size(333, 74);
            //UpdateBtn.Location = new Point(185, 321);
        } //333, 74 

        private void UpdatePanel_Paint(object sender, PaintEventArgs e)
        {
            //oops accidently clicked
        }

        public void NewsfeedLoader()
        {


            // get and returns text data
            try
            {
                WebClient webClient = new WebClient();
                string FeedText1 = webClient.DownloadString("https://raw.githubusercontent.com/xarson/jiayi/master/Feed/FeedData1.txt");
                FeedData1.Text = FeedText1;

                string FeedText2 = webClient.DownloadString("https://raw.githubusercontent.com/xarson/jiayi/master/Feed/FeedData2.txt");
                FeedData2.Text = FeedText2;

                string FeedText3 = webClient.DownloadString("https://raw.githubusercontent.com/xarson/jiayi/master/Feed/FeetData3.txt");
                FeedData3.Text = FeedText3;

                // get and return images for feed 

                WebRequest request1 = WebRequest.Create("https://github.com/xarson/jiayi/raw/master/Images/FeedPic1.png");
                Stream stream = request1.GetResponse().GetResponseStream();
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                this.FeedPic1.Image = img;

                WebRequest request2 = WebRequest.Create("https://github.com/xarson/jiayi/raw/master/Images/FeedPic2.png");
                Stream stream2 = request2.GetResponse().GetResponseStream();
                System.Drawing.Image img2 = System.Drawing.Image.FromStream(stream2);
                this.FeedPic2.Image = img2;

                WebRequest request3 = WebRequest.Create("https://github.com/xarson/jiayi/raw/master/Images/FeedPic3.png");
                Stream stream3 = request3.GetResponse().GetResponseStream();
                System.Drawing.Image img3 = System.Drawing.Image.FromStream(stream3);
                this.FeedPic3.Image = img3;
            }

            catch (ArgumentException)
            {
                string message = "Make Sure You Are Connected To The Internet!";
                string caption = "Error Detected in Newsfeed Loader";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }

        private void UpdatePanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ResolutionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Resolution = ResolutionComboBox.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void ProcessPriorityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Priority = ProcessPriorityBox.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void FeedData1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TopPanel_Click(object sender, EventArgs e)
        {

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        private void ThemesButton_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = false;
            ThemesPanel.Visible = true;
        }

        private void AccentColorBtn_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Properties.Settings.Default.AccentColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
            xboxGamertag.ForeColor = Properties.Settings.Default.AccentColor;
            TopPanel.HoverState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            TopPanel.CustomBorderColor = Properties.Settings.Default.AccentColor;
            HomeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            SettingsBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            UpdatePanelBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            CosmeticsBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            CloseLauncher.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            HideLauncher.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            KeepOpen.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            Version201Bar.FillColor = Properties.Settings.Default.AccentColor;
            Install201Btn.BorderColor = Properties.Settings.Default.AccentColor;
            LogoLabel.ForeColor = Properties.Settings.Default.AccentColor;
            StableSettingBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ExpirementalSettingBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ThemesButton.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            ResolutionComboBox.FocusedState.BorderColor = Properties.Settings.Default.AccentColor;
            ProcessPriorityBox.FocusedState.BorderColor = Properties.Settings.Default.AccentColor;
            LightThemeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            DarkThemeBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            RpcIgnBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;
            RpcSrverBtn.CheckedState.CustomBorderColor = Properties.Settings.Default.AccentColor;

            AccentColorBtn.FillColor = Properties.Settings.Default.AccentColor;
        }

        private void BackToSettings_Click(object sender, EventArgs e)
        {
            SettingsPanel.Visible = true;
            ThemesPanel.Visible = false;
        }

        private void LightThemeBtn_Click(object sender, EventArgs e)
        {
            LightThemeBtn.Checked = true;
            LightTheme();
            DarkThemeBtn.Checked = false;
            Properties.Settings.Default.LightDarkMode = "Light";
            Properties.Settings.Default.Save();
        }

        private void DarkThemeBtn_Click(object sender, EventArgs e)
        {
            DarkTheme();
            LightThemeBtn.Checked = false;
            DarkThemeBtn.Checked = true;
            Properties.Settings.Default.LightDarkMode = "Dark";
            Properties.Settings.Default.Save();
        }

        private void BackImageSelectButton_Click(object sender, EventArgs e)
        {
            SelectImageDialog.ShowDialog();

            if (SelectImageDialog.FileName == null)
            {
                return;
            }

            Properties.Settings.Default.BackImagePath = SelectImageDialog.FileName;
            Properties.Settings.Default.Save();

            try
            {
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = Image.FromFile(Properties.Settings.Default.BackImagePath);
            }
            catch (ArgumentException)
            {
                BackImageCheckBox.Checked = false;
                // IM SUCH A BAD PROGRAMMER OMG
            }
        }

        private void BackImageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            BackImageSelectButton.Enabled = BackImageCheckBox.Checked;
            if (!BackImageCheckBox.Checked)
            {
                this.BackgroundImage = null;
                Properties.Settings.Default.BackImagePath = null;
                Properties.Settings.Default.Save();
            }
        }

        // launch the version selected
        private void Launch201Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("C:\\Jiayi\\Versions\\Minecraft-1.16.201.2.Appx");
            }
            catch (Exception)
            {
                Status201.Text = "STATUS: Version Installer Could Not Be Found!";
            }
        }

        private void VersionBtn_Click(object sender, EventArgs e)
        {
            VersionPanel.Visible = true;
            VersionPanel.BringToFront();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            RpcSrverBtn.Checked = false;
            RpcIgnBtn.Checked = true;
            Properties.Settings.Default.RpcMode = "IGN";
            Properties.Settings.Default.Save();
        }

        private void RpcSrverBtn_Click(object sender, EventArgs e)
        {
            RpcSrverBtn.Checked = true;
            RpcIgnBtn.Checked = false;
            Properties.Settings.Default.RpcMode = "Server";
            Properties.Settings.Default.Save();
        }

        // version changer
        private void Install201Btn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C:\\Jiayi\\Versions\\Minecraft-1.16.201.2.Appx"))
            {
                Status201.Text = "STATUS: Version Installer Already Exists!";
            }
            else
            {
                Version201Bar.Visible = true;
                Launch201Btn.Enabled = false;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_201Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_201Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/xarson/jiayi/releases/download/1.16.201/Minecraft-1.16.201.2.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.201.2.Appx");
            }
        }

        private void v1_16_201Completed(object sender, AsyncCompletedEventArgs e)
        {
            Launch201Btn.Enabled = true;
            Version201Bar.Visible = false;
            Status201.Text = "STATUS: Succefully Installed";
        }

        private void v1_16_201Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            Version201Bar.Value = e.ProgressPercentage;
            Status201.Text = "STATUS: " + e.ProgressPercentage.ToString() + "%";
        }

        private void Install100Btn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C:\\Jiayi\\Versions\\Minecraft-1.16.100.4.Appx"))
            {
                Status100.Text = "STATUS: Version Installer Already Exists!";
            }
            else
            {
                Launch100Btn.Enabled = false;
                ProgressBar100.Visible = true;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_100Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_100Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/xarson/jiayi/releases/download/1.16.100/Minecraft-1.16.100.4.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.100.4.Appx");
            }
        }

        private void v1_16_100Completed(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar100.Visible = false;
            Status100.Text = "STATUS: Succefully Installed";
            Launch100Btn.Enabled = true;
        }

        private void v1_16_100Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar100.Value = e.ProgressPercentage;
            Status100.Text = "STATUS: " + e.ProgressPercentage.ToString() + "%";
        }

        private void Install40Btn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C:\\Jiayi\\Versions\\Minecraft-1.16.40.2.Appx"))
            {
                Status40.Text = "STATUS: Version Installer Already Exists!";
            }
            else
            {
                Launch100Btn.Enabled = false;
                ProgressBar100.Visible = true;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_40Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_40Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/xarson/jiayi/releases/download/1.16.40/Minecraft-1.16.40.2.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.40.2.Appx");
            }
        }

        private void v1_16_40Completed(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar100.Visible = false;
            Status40.Text = "STATUS: Succefully Installed";
            Launch100Btn.Enabled = true;
        }

        private void v1_16_40Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar100.Value = e.ProgressPercentage;
            Status40.Text = "STATUS: " + e.ProgressPercentage.ToString() + "%";
        }

        private void GoBackBtn_Click(object sender, EventArgs e)
        {
            VersionPanel.Visible = false;
            HomePanel.Visible = true;
        }

        private void Launch40Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("C:\\Jiayi\\Versions\\Minecraft-1.16.40.2.Appx");
            }
            catch (Exception)
            {
                Status40.Text = "STATUS: Version Installer Could Not Be Found!";
            }
        }

        private void Launch100Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("C:\\Jiayi\\Versions\\Minecraft-1.16.100.4.Appx");
            }
            catch (Exception)
            {
                Status100.Text = "STATUS: Version Installer Could Not Be Found!";
            }
        }
    }
}
