using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace JiayiLauncher
{
    public partial class UpdateForm : Form
    {

        public UpdateForm()
        {
            InitializeComponent();
        }

        void DeleteOld()
        {
            StatusText.Text = ("STATUS: Looking For Previous Versions");
            StatusText.Location = new Point(-4, 405);

            if (File.Exists(Path.Combine(path, dllfile)))
            {
                File.Delete(Path.Combine(path, dllfile));
                StatusText.Text = ("STATUS: Deleting Previous Version");
                StatusText.Location = new Point(-4, 405);
            }

            else
            {
                StatusText.Text = ("STATUS: Unable To Locate Previous Version, Now Installing");
                StatusText.Location = new Point(-4, 405);
            }
        }

        string path = System.IO.Directory.GetCurrentDirectory().ToString();
        string dllfile = "JiayiClient.dll";
        WebClient client;

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            DeleteOld();
            Thread.Sleep(500);
            StatusText.Text = ("STATUS: Preparing Installation");
            Thread.Sleep(500);
            client = new System.Net.WebClient(); 
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            StatusText.Text = ("STATUS: Installation Has Started");
            Thread.Sleep(500);

            client.DownloadFileAsync(new Uri(""), path);
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StatusText.Text = ("STATUS: Succefully Installed Jiayi");
            Thread.Sleep(800);
            this.Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) // NEW
        {
            DownloadProgress.Value = e.ProgressPercentage;

           if (DownloadProgress.Value == 25)
            {
                StatusText.Text = ("STATUS: JIAYI: 25%");
                Thread.Sleep(500);
                if (DownloadProgress.Value == 50)
                {
                    StatusText.Text = ("STATUS: JIAYI: 50%");
                    Thread.Sleep(500);
                    if (DownloadProgress.Value == 75)
                    {
                        StatusText.Text = ("STATUS: JIAYI: 75%");
                        Thread.Sleep(500);
                    }
                }
            }
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
                client.Dispose();
        }
    }
}
