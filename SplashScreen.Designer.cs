namespace JiayiLauncher
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.JiayiLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.SplashTimer = new System.Windows.Forms.Timer(this.components);
            this.SakuraLeaves = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.JiayiLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SakuraLeaves)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.AnimationInterval = 300;
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DragForm = false;
            this.guna2BorderlessForm1.ResizeForm = false;
            // 
            // JiayiLogo
            // 
            this.JiayiLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JiayiLogo.BackColor = System.Drawing.Color.Transparent;
            this.JiayiLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.JiayiLogo.FillColor = System.Drawing.Color.Transparent;
            this.JiayiLogo.Image = ((System.Drawing.Image)(resources.GetObject("JiayiLogo.Image")));
            this.JiayiLogo.Location = new System.Drawing.Point(235, 80);
            this.JiayiLogo.Name = "JiayiLogo";
            this.JiayiLogo.ShadowDecoration.Parent = this.JiayiLogo;
            this.JiayiLogo.Size = new System.Drawing.Size(200, 200);
            this.JiayiLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.JiayiLogo.TabIndex = 1;
            this.JiayiLogo.TabStop = false;
            this.JiayiLogo.UseTransparentBackground = true;
            // 
            // SplashTimer
            // 
            this.SplashTimer.Interval = 2000;
            this.SplashTimer.Tick += new System.EventHandler(this.SplashTimer_Tick);
            // 
            // SakuraLeaves
            // 
            this.SakuraLeaves.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SakuraLeaves.Image = ((System.Drawing.Image)(resources.GetObject("SakuraLeaves.Image")));
            this.SakuraLeaves.Location = new System.Drawing.Point(0, 0);
            this.SakuraLeaves.Name = "SakuraLeaves";
            this.SakuraLeaves.ShadowDecoration.Parent = this.SakuraLeaves;
            this.SakuraLeaves.Size = new System.Drawing.Size(670, 370);
            this.SakuraLeaves.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SakuraLeaves.TabIndex = 0;
            this.SakuraLeaves.TabStop = false;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(670, 370);
            this.Controls.Add(this.JiayiLogo);
            this.Controls.Add(this.SakuraLeaves);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.JiayiLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SakuraLeaves)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2PictureBox JiayiLogo;
        private System.Windows.Forms.Timer SplashTimer;
        private Guna.UI2.WinForms.Guna2PictureBox SakuraLeaves;
    }
}