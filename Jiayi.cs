using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jiayi_Launcher
{
    public partial class Jiayi : Form
    {

        int panelwidth;
        bool Hidden;

        public Jiayi()
        {
            InitializeComponent();
            int panelwidth = ButtonPanel.Width;
            bool Hidden = false;
        }

        // Starting Point of all ButtonPanel functions

        private void ButtonPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ButtonPanel_MouseHover(object sender, EventArgs e)
        {
            timer1.Start();
            
        }

        private void ButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            Hidden = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                ButtonPanel.Width = panelwidth + 10; 
                if(ButtonPanel.Width >= panelwidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }

            else
            {
                ButtonPanel.Width = ButtonPanel.Width - 10;
                if(ButtonPanel.Width <= panelwidth)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        // Ending Point of all ButtonPanel functions



        // Starting Point Of All HomeScreenBtn Functions

        private void HomeScreenBtn_Click(object sender, EventArgs e)
        {
            HomeScreenBtn.Checked = true;
            SettingsScreenBtn.Checked = false;

        }

        private void HomeScreenBtn_MouseHover(object sender, EventArgs e)
        {
            ButtonPanel_MouseHover(sender, e);
        }

        private void HomeScreenBtn_MouseLeave(object sender, EventArgs e)
        {
            ButtonPanel_MouseLeave(sender, e);
        }

        // Ending Point Of All HomeScreenBtn Functions



        // Starting Point Of All SettingsScreenBtn Functions

        private void SettingsScreenBtn_Click(object sender, EventArgs e)
        {
            HomeScreenBtn.Checked = false;
            SettingsScreenBtn.Checked = true; ;
        }

        private void SettingsScreenBtn_MouseHover(object sender, EventArgs e)
        {
            ButtonPanel_MouseHover(sender, e);
        }

        private void SettingsScreenBtn_MouseLeave(object sender, EventArgs e)
        {
            ButtonPanel_MouseLeave(sender, e);
        }

        // Ending Point Of All SettingsScreenBtn Functions

    }
}
