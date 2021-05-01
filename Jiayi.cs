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

        public static string encryptDecrypt(string input)
        {
            char[] key = { 'J', 'I', 'A', 'Y', 'I' }; 
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
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

            // preset theme settings
            if (Properties.Settings.Default.Theme == "Chr7st")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/cubemap_0.png"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.DeepSkyBlue;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Enderman")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/0a3cff39ad4952d622cb5682ff743cda.jpg"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.BlueViolet;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Cloudy")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Screenshot_272.png"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.LightSteelBlue;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Patar")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Webp.net-resizeimage.png"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.SteelBlue;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Eim")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/unknown.jpeg"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.RoyalBlue;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Plural")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/KJ.png"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.DarkRed;
                Properties.Settings.Default.Save();
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
            } else if (Properties.Settings.Default.Theme == "Rilaye")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/image1.jpg"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.Pink;
                Properties.Settings.Default.Save();
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

            } else if (Properties.Settings.Default.Theme == "Morty")
            {
                WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Mo8rty-Jiyai_-_Final.png"));
                Stream stream = request.GetResponse().GetResponseStream();
                Image image = Image.FromStream(stream);
                BackImageCheckBox.Checked = true;
                HomePanel.UseTransparentBackground = true;
                SettingsPanel.UseTransparentBackground = true;
                ThemesPanel.UseTransparentBackground = true;
                UpdatePanel.UseTransparentBackground = true;
                TopPanel.UseTransparentBackground = true;
                this.BackgroundImage = image;

                DarkTheme();

                Properties.Settings.Default.AccentColor = Color.Lime;
                Properties.Settings.Default.Save();
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

            // discord tag
            try
            {
                label12.Text = "  " + client.CurrentUser.Username + "#" + client.CurrentUser.Discriminator.ToString();
            } catch (NullReferenceException)
            {
                label12.Text = "  Failed";
            }

            // rgb settings poggers!
            RGBCheckBox.Checked = Properties.Settings.Default.RGB;
            IntervalTrackBar.Value =  Properties.Settings.Default.RGBInterval;
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
                    {
                        xboxGamertag.Text = "Failed";
                        MessageBox.Show("Xbox info is unavailable at this time. Try again in an hour or so.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        xboxGamertag.Text = "Failed";
                        MessageBox.Show("Couldn't get your Xbox avatar and gamertag. Make sure you're signed in to Xbox Live.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void xboxIconCompleted(object sender, AsyncCompletedEventArgs e)
        {
            xboxIcon.Image = (Image)new Bitmap("C:\\jiayi\\icon.png");
        }

        public void DarkTheme()
        { 
            /*WebRequest requestdark = WebRequest.Create("https://github.com/notcarlton/jiayi/raw/master/Images/LightHomeScreen.png");
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
            xboxGamertag.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(15, 15, 15);
        }

        public void LightTheme()
        {
            // kinda broken and annoying to fit
            /*WebRequest light = WebRequest.Create("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/DarkHomeScreen.png");
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
            xboxGamertag.ForeColor = Color.Black;
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
                string message = "Error";
                string caption = "We couldn't detect your Minecraft version. Make sure you have Minecraft installed.";
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
            CosmeticsPanel.Visible = false;

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
            CosmeticsPanel.Visible = false;

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
            CosmeticsPanel.Visible = false;

            ThemesPanel.Visible = false;

            RPCForBtns("Looking At Newsfeed");

            TopPanel.Text = ("Newsfeed");
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
            CosmeticsPanel.Visible = true;
            CosmeticsPanel.BringToFront();

            ThemesPanel.Visible = false;

            RPCForBtns("In Cosmetics Menu");

            TopPanel.Text = ("Cosmetics");
        }


        // Minimize and Close Btn Functions
        private void MinimizeBtn_MouseHover(object sender, EventArgs e)
        {
            // MinimizeBtn.Checked = true;
            // MinimizeBtn.Location = new Point(828, 7); no
        }

        private void ExitBtn_MouseHover(object sender, EventArgs e)
        {
            // ExitBtn.Checked = true;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            client.Dispose();
            NotifyIcon.Visible = false;
            this.Close();
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            // ExitBtn.Checked = false;
        }

        private void MinimizeBtn_MouseLeave(object sender, EventArgs e)
        {
            // MinimizeBtn.Checked = false;
            // MinimizeBtn.Location = new Point(828, 12);
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
            if (Properties.Settings.Default.DLLPath == "")
            {
                StatusText.Text = "Select a DLL first.";
                return;
            }
            Status.Visible = true;
            StatusText.Visible = true;
            // just gonna leave this here
            //string mcversion = Version.Text.Remove(0, 8);
            //Console.WriteLine(mcversion);
            StatusText.Text = ("Preparing to launch..");
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
            StableSettingBtn.Checked = true;
            ExpirementalSettingBtn.Checked = false;
            Properties.Settings.Default.Branch = "Stable";
            Properties.Settings.Default.Save();
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
                    StatusText.Text = ("Reading settings...");
                    MoreSettings();

                }

                else
                {
                    StatusText.Text = ("Closing launcher...");
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
                StatusText.Text = ("Reading settings...");
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
            //if (RpcIgnBtn.Checked == true) rpc in dll now
            //{
            //    try
            //    {
            //        RPCInGame("IGN:" + xboxName);
            //    }
            //    catch (ArgumentException)
            //    {
            //        string message = "Error";
            //        string caption = "We couldn't get your Gamertag. Try installing the Xbox Console Companion app.";
            //        MessageBoxButtons buttons = MessageBoxButtons.OK;
            //        MessageBox.Show(message, caption, buttons);
            //    }
            //}

            //else
            //{
            //    RPCInGame("");
            //}
            timer1.Start();
            Thread.Sleep(10000);
            InjectDLL(Properties.Settings.Default.DLLPath);
            Thread.Sleep(200);

            Process[] processes = Process.GetProcessesByName("Minecraft.Windows");
            foreach (Process proc in processes)
                if (ProcessPriorityBox.SelectedItem.ToString() == "High")
                {
                    proc.PriorityClass = ProcessPriorityClass.High;
                }

                else if (ProcessPriorityBox.SelectedItem.ToString() == "Medium")
                {
                    proc.PriorityClass = ProcessPriorityClass.Normal;
                }
                else if (ProcessPriorityBox.SelectedItem.ToString() == "Low")
                {
                    proc.PriorityClass = ProcessPriorityClass.BelowNormal;
                }

            Process[] MinecraftIndex = Process.GetProcessesByName("Minecraft.Windows");
            if (MinecraftIndex.Length > 0)
            {
                Process Minecraft = Process.GetProcessesByName("Minecraft.Windows")[0];
                if (ResolutionComboBox.SelectedItem.ToString() == "1920x1080")
                {
                    MoveWindow(Minecraft.MainWindowHandle, 0, 0, 1920, 1080, true);
                }
                else if (ResolutionComboBox.SelectedItem.ToString() == "1280x720")
                {
                    MoveWindow(Minecraft.MainWindowHandle, 0, 0, 1280, 720, true);
                }
                else if (ResolutionComboBox.SelectedItem.ToString() == "1600x900")
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
                Console.WriteLine("injected");
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
                string FeedText1 = webClient.DownloadString("https://raw.githubusercontent.com/notcarlton/jiayi/master/Feed/FeedData1.txt");
                FeedData1.Text = FeedText1;

                string FeedText2 = webClient.DownloadString("https://raw.githubusercontent.com/notcarlton/jiayi/master/Feed/FeedData2.txt");
                FeedData2.Text = FeedText2;

                string FeedText3 = webClient.DownloadString("https://raw.githubusercontent.com/notcarlton/jiayi/master/Feed/FeedData3.txt");
                FeedData3.Text = FeedText3;

                // get and return images for feed 

                WebRequest request1 = WebRequest.Create("https://github.com/notcarlton/jiayi/raw/master/Images/FeedPic1.png");
                Stream stream = request1.GetResponse().GetResponseStream();
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                this.FeedPic1.Image = img;

                WebRequest request2 = WebRequest.Create("https://github.com/notcarlton/jiayi/raw/master/Images/FeedPic2.png");
                Stream stream2 = request2.GetResponse().GetResponseStream();
                System.Drawing.Image img2 = System.Drawing.Image.FromStream(stream2);
                this.FeedPic2.Image = img2;

                WebRequest request3 = WebRequest.Create("https://github.com/notcarlton/jiayi/raw/master/Images/FeedPic3.png");
                Stream stream3 = request3.GetResponse().GetResponseStream();
                System.Drawing.Image img3 = System.Drawing.Image.FromStream(stream3);
                this.FeedPic3.Image = img3;

                // blurb text
                TitleLabel.Text = webClient.DownloadString("https://raw.githubusercontent.com/notcarlton/jiayi/master/Feed/BlurbTitle.txt");
                TextLabel.Text = webClient.DownloadString("https://raw.githubusercontent.com/notcarlton/jiayi/master/Feed/BlurbText.txt");
            }

            catch (ArgumentException)
            {
                string message = "Couldn't download newsfeed data. Please check your internet connection.";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
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
                Status201.Text = "STATUS: Failed to open AppX file.";
            }
        }

        private void VersionBtn_Click(object sender, EventArgs e)
        {
            SelectDLLDialog.ShowDialog();
            Properties.Settings.Default.DLLPath = SelectDLLDialog.FileName;
            Properties.Settings.Default.Save();
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
                Status201.Text = "STATUS: This AppX file already exists.";
            }
            else
            {
                Version201Bar.Visible = true;
                Launch201Btn.Enabled = false;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_201Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_201Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/notcarlton/jiayi/releases/download/1.16.201/Minecraft-1.16.201.2.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.201.2.Appx");
            }
        }

        private void v1_16_201Completed(object sender, AsyncCompletedEventArgs e)
        {
            Launch201Btn.Enabled = true;
            Version201Bar.Visible = false;
            Status201.Text = "STATUS: Successfully installed.";
        }

        private void v1_16_201Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            Version201Bar.Value = e.ProgressPercentage;
            Status201.Text = "STATUS: Currently at " + e.ProgressPercentage.ToString() + "%.";
        }

        private void Install100Btn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C:\\Jiayi\\Versions\\Minecraft-1.16.100.4.Appx"))
            {
                Status100.Text = "STATUS: This AppX file already exists.";
            }
            else
            {
                Launch100Btn.Enabled = false;
                ProgressBar100.Visible = true;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_100Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_100Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/notcarlton/jiayi/releases/download/1.16.100/Minecraft-1.16.100.4.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.100.4.Appx");
            }
        }

        private void v1_16_100Completed(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar100.Visible = false;
            Status100.Text = "STATUS: Successfully installed.";
            Launch100Btn.Enabled = true;
        }

        private void v1_16_100Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar100.Value = e.ProgressPercentage;
            Status100.Text = "STATUS: Currently at " + e.ProgressPercentage.ToString() + "%.";
        }

        private void Install40Btn_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists("C:\\Jiayi\\Versions\\Minecraft-1.16.40.2.Appx"))
            {
                Status40.Text = "STATUS: This AppX file already exists.";
            }
            else
            {
                Launch100Btn.Enabled = false;
                ProgressBar100.Visible = true;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(v1_16_40Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(v1_16_40Changed);
                webClient.DownloadFileAsync(new Uri("https://github.com/notcarlton/jiayi/releases/download/1.16.40/Minecraft-1.16.40.2.Appx"), "C:\\Jiayi\\Versions\\Minecraft-1.16.40.2.Appx");
            }
        }

        private void v1_16_40Completed(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar100.Visible = false;
            Status40.Text = "STATUS: Successfully installed.";
            Launch100Btn.Enabled = true;
        }

        private void v1_16_40Changed(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar100.Value = e.ProgressPercentage;
            Status40.Text = "STATUS: Currently at " + e.ProgressPercentage.ToString() + "%.";
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
                Status40.Text = "STATUS: Failed to open AppX file.";
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
                Status100.Text = "STATUS: Failed to open AppX file.";
            }
        }

        private void VersionPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Status201_Click(object sender, EventArgs e)
        {
            // fook
        }

        private void Install40Btn_Click_1(object sender, EventArgs e)
        {

        }


        // themes (below is a pretty good example of how they work)
        private void Chr7stTheme_Click(object sender, EventArgs e)
        {
            // download image without saving it on the hard drive
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/cubemap_0.png"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            // apply dark or light theme
            DarkTheme();

            // apply custom accent color
            Properties.Settings.Default.AccentColor = Color.DeepSkyBlue;
            Properties.Settings.Default.Save();
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

            // save theme for next launch
            Properties.Settings.Default.Theme = "Chr7st";
            Properties.Settings.Default.Save();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Screenshot_272.png"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.LightSteelBlue;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Cloudy";
            Properties.Settings.Default.Save();

        }

        // im lazy
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = "Default";
            Properties.Settings.Default.AccentColor = Color.Red;
            Properties.Settings.Default.LightDarkMode = "Dark";
            Properties.Settings.Default.BackImagePath = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.RGB = false;
            Properties.Settings.Default.RGBInterval = 50;
            MessageBox.Show("Restart Jiayi to apply your changes.", "Themes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // more themes
        private void EndermanTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/0a3cff39ad4952d622cb5682ff743cda.jpg"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.BlueViolet;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Enderman";
            Properties.Settings.Default.Save();

        }

        private void PatarTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Webp.net-resizeimage.png"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.SteelBlue;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Patar";
            Properties.Settings.Default.Save();

        }

        private void EimTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/unknown.jpeg"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.RoyalBlue;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Eim";
            Properties.Settings.Default.Save();

        }

        private void PluralTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/KJ.png"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.DarkRed;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Plural";
            Properties.Settings.Default.Save();

        }

        private void RilayeTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/image1.jpg"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.Pink;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Rilaye";
            Properties.Settings.Default.Save();
        }

        private void TextureProfiles_Click(object sender, EventArgs e)
        {
            // fuck
        }

        private void MortyTheme_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(new Uri("https://raw.githubusercontent.com/notcarlton/jiayi/master/Images/Mo8rty-Jiyai_-_Final.png"));
            Stream stream = request.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            BackImageCheckBox.Checked = true;
            HomePanel.UseTransparentBackground = true;
            SettingsPanel.UseTransparentBackground = true;
            ThemesPanel.UseTransparentBackground = true;
            UpdatePanel.UseTransparentBackground = true;
            TopPanel.UseTransparentBackground = true;
            this.BackgroundImage = image;

            DarkTheme();

            Properties.Settings.Default.AccentColor = Color.Lime;
            Properties.Settings.Default.Save();
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

            Properties.Settings.Default.Theme = "Morty";
            Properties.Settings.Default.Save();
        }

        private void CreditsTimer_Tick(object sender, EventArgs e)
        {
            // NEVER MIND
        }

        private void IntervalTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Properties.Settings.Default.RGBInterval = IntervalTrackBar.Value;
            Properties.Settings.Default.Save();
        }

        int red = 255;
        int green = 0;
        int blue = 0; // we'll need these

        private void RGBIntervalTimer_Tick(object sender, EventArgs e)
        {
            if (!RGBCheckBox.Checked) { // no check?
                return;
            }
            RGBIntervalTimer.Interval = IntervalTrackBar.Value;

            // the fun stuff
            if (red == 255)
            {
                if (blue != 0)
                {
                    blue -= 5;
                } else
                {
                    green += 5;
                }
            }
            if (green == 255)
            {
                if (red != 0)
                {
                    red -= 5;
                } else
                {
                    blue += 5;
                }
            }
            if (blue == 255)
            {
                if (green != 0)
                {
                    green -= 5;
                }
                else
                {
                    red += 5;
                }
            }

            Color finishedcolor = Color.FromArgb(red, green, blue);
            TopPanel.HoverState.CustomBorderColor = finishedcolor;
            TopPanel.CustomBorderColor = finishedcolor;
            HomeBtn.CheckedState.CustomBorderColor = finishedcolor;
            SettingsBtn.CheckedState.CustomBorderColor = finishedcolor;
            UpdatePanelBtn.CheckedState.CustomBorderColor = finishedcolor;
            CosmeticsBtn.CheckedState.CustomBorderColor = finishedcolor;
            CloseLauncher.CheckedState.CustomBorderColor = finishedcolor;
            HideLauncher.CheckedState.CustomBorderColor = finishedcolor;
            KeepOpen.CheckedState.CustomBorderColor = finishedcolor;
            Version201Bar.FillColor = finishedcolor;
            Install201Btn.BorderColor = finishedcolor;
            LogoLabel.ForeColor = finishedcolor;
            StableSettingBtn.CheckedState.CustomBorderColor = finishedcolor;
            ExpirementalSettingBtn.CheckedState.CustomBorderColor = finishedcolor;
            ThemesButton.CheckedState.CustomBorderColor = finishedcolor;
            ResolutionComboBox.FocusedState.BorderColor = finishedcolor;
            ProcessPriorityBox.FocusedState.BorderColor = finishedcolor;
            LightThemeBtn.CheckedState.CustomBorderColor = finishedcolor;
            DarkThemeBtn.CheckedState.CustomBorderColor = finishedcolor;
            RpcIgnBtn.CheckedState.CustomBorderColor = finishedcolor;
            RpcSrverBtn.CheckedState.CustomBorderColor = finishedcolor;
            IntervalTrackBar.ThumbColor = finishedcolor;

            AccentColorBtn.FillColor = finishedcolor;
        }

        private void RGBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RGB = RGBCheckBox.Checked;
            Properties.Settings.Default.Save();
            IntervalTrackBar.Enabled = RGBCheckBox.Checked;
            if (Properties.Settings.Default.RGB)
            {
                RGBIntervalTimer.Start();
                AccentColorBtn.Enabled = false;
            } else
            {
                RGBIntervalTimer.Stop();
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
                IntervalTrackBar.ThumbColor = Color.FromArgb(25, 24, 26);

                AccentColorBtn.FillColor = Properties.Settings.Default.AccentColor;
                AccentColorBtn.Enabled = true;
            }
        }
    }
}
