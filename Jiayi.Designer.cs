
namespace Jiayi_Launcher
{
    partial class Jiayi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Jiayi));
            this.guna2BorderlessJiayi = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.ButtonPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.SettingsScreenBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.HomeScreenBtn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessJiayi
            // 
            this.guna2BorderlessJiayi.ContainerControl = this;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.ButtonPanel.Controls.Add(this.SettingsScreenBtn);
            this.ButtonPanel.Controls.Add(this.HomeScreenBtn);
            this.ButtonPanel.Location = new System.Drawing.Point(870, -2);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.ShadowDecoration.Parent = this.ButtonPanel;
            this.ButtonPanel.Size = new System.Drawing.Size(74, 583);
            this.ButtonPanel.TabIndex = 0;
            this.ButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonPanel_Paint);
            this.ButtonPanel.MouseLeave += new System.EventHandler(this.ButtonPanel_MouseLeave);
            this.ButtonPanel.MouseHover += new System.EventHandler(this.ButtonPanel_MouseHover);
            // 
            // SettingsScreenBtn
            // 
            this.SettingsScreenBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("SettingsScreenBtn.CheckedState.Image")));
            this.SettingsScreenBtn.CheckedState.ImageSize = new System.Drawing.Size(50, 50);
            this.SettingsScreenBtn.CheckedState.Parent = this.SettingsScreenBtn;
            this.SettingsScreenBtn.HoverState.ImageSize = new System.Drawing.Size(53, 53);
            this.SettingsScreenBtn.HoverState.Parent = this.SettingsScreenBtn;
            this.SettingsScreenBtn.Image = ((System.Drawing.Image)(resources.GetObject("SettingsScreenBtn.Image")));
            this.SettingsScreenBtn.ImageRotate = 0F;
            this.SettingsScreenBtn.ImageSize = new System.Drawing.Size(50, 50);
            this.SettingsScreenBtn.IndicateFocus = true;
            this.SettingsScreenBtn.Location = new System.Drawing.Point(5, 224);
            this.SettingsScreenBtn.Name = "SettingsScreenBtn";
            this.SettingsScreenBtn.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.SettingsScreenBtn.PressedState.Parent = this.SettingsScreenBtn;
            this.SettingsScreenBtn.Size = new System.Drawing.Size(64, 54);
            this.SettingsScreenBtn.TabIndex = 1;
            this.SettingsScreenBtn.Click += new System.EventHandler(this.SettingsScreenBtn_Click);
            this.SettingsScreenBtn.MouseLeave += new System.EventHandler(this.SettingsScreenBtn_MouseLeave);
            this.SettingsScreenBtn.MouseHover += new System.EventHandler(this.SettingsScreenBtn_MouseHover);
            // 
            // HomeScreenBtn
            // 
            this.HomeScreenBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("HomeScreenBtn.CheckedState.Image")));
            this.HomeScreenBtn.CheckedState.ImageSize = new System.Drawing.Size(50, 50);
            this.HomeScreenBtn.CheckedState.Parent = this.HomeScreenBtn;
            this.HomeScreenBtn.HoverState.ImageSize = new System.Drawing.Size(53, 53);
            this.HomeScreenBtn.HoverState.Parent = this.HomeScreenBtn;
            this.HomeScreenBtn.Image = ((System.Drawing.Image)(resources.GetObject("HomeScreenBtn.Image")));
            this.HomeScreenBtn.ImageRotate = 0F;
            this.HomeScreenBtn.ImageSize = new System.Drawing.Size(50, 50);
            this.HomeScreenBtn.IndicateFocus = true;
            this.HomeScreenBtn.Location = new System.Drawing.Point(5, 102);
            this.HomeScreenBtn.Name = "HomeScreenBtn";
            this.HomeScreenBtn.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.HomeScreenBtn.PressedState.Parent = this.HomeScreenBtn;
            this.HomeScreenBtn.Size = new System.Drawing.Size(64, 54);
            this.HomeScreenBtn.TabIndex = 0;
            this.HomeScreenBtn.Click += new System.EventHandler(this.HomeScreenBtn_Click);
            this.HomeScreenBtn.MouseLeave += new System.EventHandler(this.HomeScreenBtn_MouseLeave);
            this.HomeScreenBtn.MouseHover += new System.EventHandler(this.HomeScreenBtn_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Jiayi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(21)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(944, 580);
            this.Controls.Add(this.ButtonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Jiayi";
            this.Text = "Jiayi Launcher";
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessJiayi;
        private Guna.UI2.WinForms.Guna2Panel ButtonPanel;
        private Guna.UI2.WinForms.Guna2ImageButton HomeScreenBtn;
        private Guna.UI2.WinForms.Guna2ImageButton SettingsScreenBtn;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.Timer timer1;
    }
}

