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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            SplashTimer.Start();
        }

        bool CanClose = false;

        private void SplashTimer_Tick(object sender, EventArgs e)
        {
            if (!CanClose)
            {
                CanClose = true;
            } else
            {
                //Jiayi jiayi = new Jiayi();
                //jiayi.Show();
                Close();
            }
        }
    }
}
