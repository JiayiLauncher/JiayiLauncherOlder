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
            StatusText.Text = ("STATUS: Finding previous versions...");
            StatusText.Location = new Point(-4, 405);

            if (File.Exists(Path.Combine(path, dllfile)))
            {
                File.Delete(Path.Combine(path, dllfile));
                StatusText.Text = ("STATUS: Removing previous version...");
                StatusText.Location = new Point(-4, 405);
            }

            else
            {
                StatusText.Text = ("STATUS: No previous versions found.");
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
            StatusText.Text = ("STATUS: Preparing to update...");
            Thread.Sleep(500);
            client = new System.Net.WebClient(); 
            client.DownloadFileCompleted += client_DownloadFileCompleted;
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            StatusText.Text = ("STATUS: Updating Jiayi...");
            Thread.Sleep(500);

            client.DownloadFileAsync(new Uri(""), Path.Combine(path, dllfile));
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StatusText.Text = ("STATUS: Jiayi has been updated.");
            Thread.Sleep(800);
            this.Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) // NEW
        {
            DownloadProgress.Value = e.ProgressPercentage;

            StatusText.Text = "STATUS: Currently at " + DownloadProgress.Value.ToString() + "%.";
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
                client.Dispose();
        }
    }
}
