
namespace JiayiLauncher
{
    partial class Jiayi
    {
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
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.BtnPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.HomeBtn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.SettingsBtn = new Guna.UI2.WinForms.Guna2Button();
            this.UpdateBtn = new Guna.UI2.WinForms.Guna2Button();
            this.CosmeticsBtn = new Guna.UI2.WinForms.Guna2Button();
            this.TopPanel = new Guna.UI2.WinForms.Guna2Button();
            this.BtnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.BorderRadius = 40;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DragForm = false;
            // 
            // BtnPanel
            // 
            this.BtnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.BtnPanel.Controls.Add(this.CosmeticsBtn);
            this.BtnPanel.Controls.Add(this.UpdateBtn);
            this.BtnPanel.Controls.Add(this.SettingsBtn);
            this.BtnPanel.Controls.Add(this.guna2PictureBox1);
            this.BtnPanel.Controls.Add(this.HomeBtn);
            this.BtnPanel.Location = new System.Drawing.Point(-1, -1);
            this.BtnPanel.Name = "BtnPanel";
            this.BtnPanel.ShadowDecoration.Parent = this.BtnPanel;
            this.BtnPanel.Size = new System.Drawing.Size(177, 542);
            this.BtnPanel.TabIndex = 0;
            // 
            // HomeBtn
            // 
            this.HomeBtn.Animated = true;
            this.HomeBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.HomeBtn.BorderThickness = 5;
            this.HomeBtn.Checked = true;
            this.HomeBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(32)))));
            this.HomeBtn.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.HomeBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.HomeBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("HomeBtn.CheckedState.Image")));
            this.HomeBtn.CheckedState.Parent = this.HomeBtn;
            this.HomeBtn.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.HomeBtn.CustomBorderThickness = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.HomeBtn.CustomImages.Parent = this.HomeBtn;
            this.HomeBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.HomeBtn.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomeBtn.ForeColor = System.Drawing.Color.White;
            this.HomeBtn.HoverState.Parent = this.HomeBtn;
            this.HomeBtn.Image = ((System.Drawing.Image)(resources.GetObject("HomeBtn.Image")));
            this.HomeBtn.ImageOffset = new System.Drawing.Point(-5, -3);
            this.HomeBtn.ImageSize = new System.Drawing.Size(26, 26);
            this.HomeBtn.Location = new System.Drawing.Point(0, 148);
            this.HomeBtn.Name = "HomeBtn";
            this.HomeBtn.PressedDepth = 10;
            this.HomeBtn.ShadowDecoration.Parent = this.HomeBtn;
            this.HomeBtn.Size = new System.Drawing.Size(177, 56);
            this.HomeBtn.TabIndex = 0;
            this.HomeBtn.Text = "Home";
            this.HomeBtn.TextOffset = new System.Drawing.Point(-2, 0);
            this.HomeBtn.Click += new System.EventHandler(this.HomeBtn_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.Location = new System.Drawing.Point(2, 3);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(185, 139);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.Animated = true;
            this.SettingsBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.SettingsBtn.BorderThickness = 5;
            this.SettingsBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(32)))));
            this.SettingsBtn.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.SettingsBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.SettingsBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("SettingsBtn.CheckedState.Image")));
            this.SettingsBtn.CheckedState.Parent = this.SettingsBtn;
            this.SettingsBtn.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.SettingsBtn.CustomBorderThickness = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.SettingsBtn.CustomImages.Parent = this.SettingsBtn;
            this.SettingsBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.SettingsBtn.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsBtn.ForeColor = System.Drawing.Color.White;
            this.SettingsBtn.HoverState.Parent = this.SettingsBtn;
            this.SettingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("SettingsBtn.Image")));
            this.SettingsBtn.ImageOffset = new System.Drawing.Point(-1, 0);
            this.SettingsBtn.ImageSize = new System.Drawing.Size(26, 26);
            this.SettingsBtn.Location = new System.Drawing.Point(0, 210);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.PressedDepth = 10;
            this.SettingsBtn.ShadowDecoration.Parent = this.SettingsBtn;
            this.SettingsBtn.Size = new System.Drawing.Size(177, 56);
            this.SettingsBtn.TabIndex = 2;
            this.SettingsBtn.Text = "Settings";
            this.SettingsBtn.TextOffset = new System.Drawing.Point(1, 0);
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Animated = true;
            this.UpdateBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.UpdateBtn.BorderThickness = 5;
            this.UpdateBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(32)))));
            this.UpdateBtn.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.UpdateBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.UpdateBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("UpdateBtn.CheckedState.Image")));
            this.UpdateBtn.CheckedState.Parent = this.UpdateBtn;
            this.UpdateBtn.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.UpdateBtn.CustomBorderThickness = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.UpdateBtn.CustomImages.Parent = this.UpdateBtn;
            this.UpdateBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.UpdateBtn.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.HoverState.Parent = this.UpdateBtn;
            this.UpdateBtn.Image = ((System.Drawing.Image)(resources.GetObject("UpdateBtn.Image")));
            this.UpdateBtn.ImageOffset = new System.Drawing.Point(-2, -3);
            this.UpdateBtn.ImageSize = new System.Drawing.Size(26, 26);
            this.UpdateBtn.Location = new System.Drawing.Point(0, 272);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.PressedDepth = 10;
            this.UpdateBtn.ShadowDecoration.Parent = this.UpdateBtn;
            this.UpdateBtn.Size = new System.Drawing.Size(177, 56);
            this.UpdateBtn.TabIndex = 3;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.TextOffset = new System.Drawing.Point(-1, 0);
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // CosmeticsBtn
            // 
            this.CosmeticsBtn.Animated = true;
            this.CosmeticsBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.CosmeticsBtn.BorderThickness = 5;
            this.CosmeticsBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(35)))), ((int)(((byte)(32)))));
            this.CosmeticsBtn.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.CosmeticsBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.CosmeticsBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("CosmeticsBtn.CheckedState.Image")));
            this.CosmeticsBtn.CheckedState.Parent = this.CosmeticsBtn;
            this.CosmeticsBtn.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(24)))), ((int)(((byte)(26)))));
            this.CosmeticsBtn.CustomBorderThickness = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.CosmeticsBtn.CustomImages.Parent = this.CosmeticsBtn;
            this.CosmeticsBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.CosmeticsBtn.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CosmeticsBtn.ForeColor = System.Drawing.Color.White;
            this.CosmeticsBtn.HoverState.Parent = this.CosmeticsBtn;
            this.CosmeticsBtn.Image = ((System.Drawing.Image)(resources.GetObject("CosmeticsBtn.Image")));
            this.CosmeticsBtn.ImageOffset = new System.Drawing.Point(3, -3);
            this.CosmeticsBtn.ImageSize = new System.Drawing.Size(26, 26);
            this.CosmeticsBtn.Location = new System.Drawing.Point(0, 334);
            this.CosmeticsBtn.Name = "CosmeticsBtn";
            this.CosmeticsBtn.PressedDepth = 10;
            this.CosmeticsBtn.ShadowDecoration.Parent = this.CosmeticsBtn;
            this.CosmeticsBtn.Size = new System.Drawing.Size(177, 56);
            this.CosmeticsBtn.TabIndex = 4;
            this.CosmeticsBtn.Text = "Cosmetics";
            this.CosmeticsBtn.TextOffset = new System.Drawing.Point(5, 0);
            this.CosmeticsBtn.Click += new System.EventHandler(this.CosmeticsBtn_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TopPanel.CheckedState.Parent = this.TopPanel;
            this.TopPanel.CustomBorderColor = System.Drawing.Color.Red;
            this.TopPanel.CustomBorderThickness = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.TopPanel.CustomImages.Parent = this.TopPanel;
            this.TopPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TopPanel.Font = new System.Drawing.Font("Raleway Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopPanel.ForeColor = System.Drawing.Color.White;
            this.TopPanel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TopPanel.HoverState.Parent = this.TopPanel;
            this.TopPanel.Location = new System.Drawing.Point(170, -1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.PressedDepth = 0;
            this.TopPanel.ShadowDecoration.Parent = this.TopPanel;
            this.TopPanel.Size = new System.Drawing.Size(729, 98);
            this.TopPanel.TabIndex = 1;
            this.TopPanel.Text = "Home";
            // 
            // Jiayi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.BtnPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Jiayi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jiayi";
            this.BtnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel BtnPanel;
        private Guna.UI2.WinForms.Guna2Button CosmeticsBtn;
        private Guna.UI2.WinForms.Guna2Button UpdateBtn;
        private Guna.UI2.WinForms.Guna2Button SettingsBtn;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Button HomeBtn;
        private Guna.UI2.WinForms.Guna2Button TopPanel;
    }
}

