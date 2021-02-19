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

namespace JiayiLauncher
{
    public partial class Jiayi : Form
    {
        public DiscordRpcClient client;
        string discordTime = "";

        public Jiayi()
        {
            InitializeComponent();
            InitializeDiscord("In Launcher");
            versionFinderForLabel("Get-AppPackage -name Microsoft.MinecraftUWP | select -expandproperty Version", VersionDisplay);
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

        // Version stuff

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
    }
}
