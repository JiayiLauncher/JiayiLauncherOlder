using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiayiLauncher
{
    public partial class Jiayi : Form
    {
        public Jiayi()
        {
            InitializeComponent();
        }

        // All Side Panel Functions 

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = true;
            SettingsBtn.Checked = false;
            UpdateBtn.Checked = false;
            CosmeticsBtn.Checked = false;

            TopPanel.Text = ("Home");
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            SettingsBtn.Checked = true;
            UpdateBtn.Checked = false;
            CosmeticsBtn.Checked = false;

            TopPanel.Text = ("Settings");
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            SettingsBtn.Checked = false;
            UpdateBtn.Checked = true;
            CosmeticsBtn.Checked = false;

            TopPanel.Text = ("Updates");
        }

        private void CosmeticsBtn_Click(object sender, EventArgs e)
        {
            HomeBtn.Checked = false;
            SettingsBtn.Checked = false;
            UpdateBtn.Checked = false;
            CosmeticsBtn.Checked = true;

            TopPanel.Text = ("Cosmetics");
        }

        // All home screen functions


    }
}
