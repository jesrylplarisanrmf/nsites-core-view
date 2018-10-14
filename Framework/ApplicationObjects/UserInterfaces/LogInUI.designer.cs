namespace NSites_V.ApplicationObjects.UserInterfaces
{
    partial class LogInUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInUI));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbRemember = new System.Windows.Forms.CheckBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblVersionNo = new System.Windows.Forms.Label();
            this.pctBannerImage = new System.Windows.Forms.PictureBox();
            this.pnlCompanyDetails = new System.Windows.Forms.Panel();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblCompanyAddress = new System.Windows.Forms.Label();
            this.lblLicenseTo = new System.Windows.Forms.Label();
            this.pnlApplicationName = new System.Windows.Forms.Panel();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.lblDeveloperName = new System.Windows.Forms.Label();
            this.cboConnection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBannerImage)).BeginInit();
            this.pnlCompanyDetails.SuspendLayout();
            this.pnlApplicationName.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbRemember
            // 
            this.chbRemember.AutoSize = true;
            this.chbRemember.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chbRemember.Location = new System.Drawing.Point(342, 191);
            this.chbRemember.Name = "chbRemember";
            this.chbRemember.Size = new System.Drawing.Size(172, 21);
            this.chbRemember.TabIndex = 3;
            this.chbRemember.Text = "Remember my Password";
            this.chbRemember.UseVisualStyleBackColor = true;
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogIn.Location = new System.Drawing.Point(342, 217);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(180, 41);
            this.btnLogIn.TabIndex = 4;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.Control;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtUsername.Location = new System.Drawing.Point(342, 128);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(180, 25);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TabStop = false;
            this.txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(265, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Control;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassword.Location = new System.Drawing.Point(342, 159);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(180, 25);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(265, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Username";
            // 
            // lblVersionNo
            // 
            this.lblVersionNo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNo.ForeColor = System.Drawing.Color.Maroon;
            this.lblVersionNo.Location = new System.Drawing.Point(353, 64);
            this.lblVersionNo.Name = "lblVersionNo";
            this.lblVersionNo.Size = new System.Drawing.Size(169, 19);
            this.lblVersionNo.TabIndex = 38;
            this.lblVersionNo.Tag = "9-16-2015";
            this.lblVersionNo.Text = "Version 1.16";
            this.lblVersionNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pctBannerImage
            // 
            this.pctBannerImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctBannerImage.Location = new System.Drawing.Point(14, 12);
            this.pctBannerImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pctBannerImage.Name = "pctBannerImage";
            this.pctBannerImage.Size = new System.Drawing.Size(230, 100);
            this.pctBannerImage.TabIndex = 41;
            this.pctBannerImage.TabStop = false;
            // 
            // pnlCompanyDetails
            // 
            this.pnlCompanyDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.pnlCompanyDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCompanyDetails.Controls.Add(this.lblCompanyName);
            this.pnlCompanyDetails.Controls.Add(this.lblCompanyAddress);
            this.pnlCompanyDetails.Location = new System.Drawing.Point(17, 147);
            this.pnlCompanyDetails.Name = "pnlCompanyDetails";
            this.pnlCompanyDetails.Size = new System.Drawing.Size(227, 112);
            this.pnlCompanyDetails.TabIndex = 47;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(7, 8);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(215, 18);
            this.lblCompanyName.TabIndex = 30;
            // 
            // lblCompanyAddress
            // 
            this.lblCompanyAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyAddress.Location = new System.Drawing.Point(7, 27);
            this.lblCompanyAddress.Name = "lblCompanyAddress";
            this.lblCompanyAddress.Size = new System.Drawing.Size(215, 71);
            this.lblCompanyAddress.TabIndex = 31;
            // 
            // lblLicenseTo
            // 
            this.lblLicenseTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.lblLicenseTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseTo.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblLicenseTo.Location = new System.Drawing.Point(17, 127);
            this.lblLicenseTo.Name = "lblLicenseTo";
            this.lblLicenseTo.Size = new System.Drawing.Size(227, 26);
            this.lblLicenseTo.TabIndex = 46;
            this.lblLicenseTo.Text = "License to :";
            // 
            // pnlApplicationName
            // 
            this.pnlApplicationName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.pnlApplicationName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlApplicationName.Controls.Add(this.lblApplicationName);
            this.pnlApplicationName.Controls.Add(this.lblDeveloperName);
            this.pnlApplicationName.Location = new System.Drawing.Point(256, 12);
            this.pnlApplicationName.Name = "pnlApplicationName";
            this.pnlApplicationName.Size = new System.Drawing.Size(266, 47);
            this.pnlApplicationName.TabIndex = 49;
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.BackColor = System.Drawing.Color.Transparent;
            this.lblApplicationName.Font = new System.Drawing.Font("Segoe UI", 7.75F);
            this.lblApplicationName.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblApplicationName.Location = new System.Drawing.Point(-1, 24);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(266, 18);
            this.lblApplicationName.TabIndex = 21;
            this.lblApplicationName.Text = "CORE Applications";
            this.lblApplicationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeveloperName
            // 
            this.lblDeveloperName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeveloperName.Font = new System.Drawing.Font("Segoe UI", 13.25F, System.Drawing.FontStyle.Bold);
            this.lblDeveloperName.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblDeveloperName.Location = new System.Drawing.Point(-1, 1);
            this.lblDeveloperName.Name = "lblDeveloperName";
            this.lblDeveloperName.Size = new System.Drawing.Size(266, 33);
            this.lblDeveloperName.TabIndex = 35;
            this.lblDeveloperName.Text = "NSites Business Applications";
            this.lblDeveloperName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cboConnection
            // 
            this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboConnection.FormattingEnabled = true;
            this.cboConnection.Items.AddRange(new object[] {
            "Production Server",
            "Test Server",
            "Development Server"});
            this.cboConnection.Location = new System.Drawing.Point(342, 96);
            this.cboConnection.Name = "cboConnection";
            this.cboConnection.Size = new System.Drawing.Size(180, 25);
            this.cboConnection.TabIndex = 0;
            this.cboConnection.TabStop = false;
            this.cboConnection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboConnection_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(265, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 51;
            this.label2.Text = "Connection";
            // 
            // LogInUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(539, 275);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboConnection);
            this.Controls.Add(this.pnlApplicationName);
            this.Controls.Add(this.pnlCompanyDetails);
            this.Controls.Add(this.lblLicenseTo);
            this.Controls.Add(this.pctBannerImage);
            this.Controls.Add(this.lblVersionNo);
            this.Controls.Add(this.chbRemember);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogInUI_FormClosed);
            this.Load += new System.EventHandler(this.LogInUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctBannerImage)).EndInit();
            this.pnlCompanyDetails.ResumeLayout(false);
            this.pnlApplicationName.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbRemember;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVersionNo;
        private System.Windows.Forms.PictureBox pctBannerImage;
        private System.Windows.Forms.Panel pnlCompanyDetails;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblCompanyAddress;
        private System.Windows.Forms.Label lblLicenseTo;
        private System.Windows.Forms.Panel pnlApplicationName;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblDeveloperName;
        private System.Windows.Forms.ComboBox cboConnection;
        private System.Windows.Forms.Label label2;
    }
}