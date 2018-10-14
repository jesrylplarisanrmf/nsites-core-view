namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    partial class DatabaseBackupRestoreUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseBackupRestoreUI));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBackupDatabase = new System.Windows.Forms.Button();
            this.txtSaveFileTo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBackupServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBackupDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackupUserId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBackupPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBackupPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRestoreDatabase = new System.Windows.Forms.Button();
            this.txtRestoreServer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRestoreDatabase = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRestoreUserId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRestorePort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRestorePassword = new System.Windows.Forms.TextBox();
            this.btnOpenRestoreFile = new System.Windows.Forms.Button();
            this.txtSqlFileFrom = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBackupDatabase);
            this.groupBox1.Controls.Add(this.txtSaveFileTo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtBackupServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBackupDatabase);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBackupUserId);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBackupPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBackupPassword);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 296);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BACKUP DATABASE";
            // 
            // btnBackupDatabase
            // 
            this.btnBackupDatabase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBackupDatabase.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBackupDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnBackupDatabase.Image")));
            this.btnBackupDatabase.Location = new System.Drawing.Point(127, 237);
            this.btnBackupDatabase.Name = "btnBackupDatabase";
            this.btnBackupDatabase.Size = new System.Drawing.Size(101, 40);
            this.btnBackupDatabase.TabIndex = 65;
            this.btnBackupDatabase.Text = "Backup";
            this.btnBackupDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackupDatabase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBackupDatabase.UseVisualStyleBackColor = false;
            this.btnBackupDatabase.Click += new System.EventHandler(this.btnBackupDatabase_Click);
            // 
            // txtSaveFileTo
            // 
            this.txtSaveFileTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaveFileTo.Location = new System.Drawing.Point(118, 42);
            this.txtSaveFileTo.Name = "txtSaveFileTo";
            this.txtSaveFileTo.Size = new System.Drawing.Size(212, 25);
            this.txtSaveFileTo.TabIndex = 76;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 17);
            this.label7.TabIndex = 77;
            this.label7.Text = "Save File (To)";
            // 
            // txtBackupServer
            // 
            this.txtBackupServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupServer.Location = new System.Drawing.Point(118, 73);
            this.txtBackupServer.Name = "txtBackupServer";
            this.txtBackupServer.Size = new System.Drawing.Size(212, 25);
            this.txtBackupServer.TabIndex = 66;
            this.txtBackupServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 67;
            this.label1.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "Database";
            // 
            // txtBackupDatabase
            // 
            this.txtBackupDatabase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupDatabase.Location = new System.Drawing.Point(118, 104);
            this.txtBackupDatabase.Name = "txtBackupDatabase";
            this.txtBackupDatabase.Size = new System.Drawing.Size(212, 25);
            this.txtBackupDatabase.TabIndex = 68;
            this.txtBackupDatabase.Text = "jc_softwares_srt_hardware_prod";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 71;
            this.label4.Text = "User Id";
            // 
            // txtBackupUserId
            // 
            this.txtBackupUserId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupUserId.Location = new System.Drawing.Point(118, 135);
            this.txtBackupUserId.Name = "txtBackupUserId";
            this.txtBackupUserId.Size = new System.Drawing.Size(212, 25);
            this.txtBackupUserId.TabIndex = 70;
            this.txtBackupUserId.Text = "root";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 17);
            this.label6.TabIndex = 75;
            this.label6.Text = "Port";
            // 
            // txtBackupPort
            // 
            this.txtBackupPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupPort.Location = new System.Drawing.Point(118, 197);
            this.txtBackupPort.Name = "txtBackupPort";
            this.txtBackupPort.Size = new System.Drawing.Size(212, 25);
            this.txtBackupPort.TabIndex = 74;
            this.txtBackupPort.Text = "3306";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 73;
            this.label5.Text = "Password";
            // 
            // txtBackupPassword
            // 
            this.txtBackupPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupPassword.Location = new System.Drawing.Point(118, 166);
            this.txtBackupPassword.Name = "txtBackupPassword";
            this.txtBackupPassword.PasswordChar = '*';
            this.txtBackupPassword.Size = new System.Drawing.Size(212, 25);
            this.txtBackupPassword.TabIndex = 72;
            this.txtBackupPassword.Text = "admin";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(474, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 32);
            this.label2.TabIndex = 83;
            this.label2.Text = "Backup / Restore Database";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRestoreDatabase);
            this.groupBox2.Controls.Add(this.txtRestoreServer);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtRestoreDatabase);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtRestoreUserId);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtRestorePort);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtRestorePassword);
            this.groupBox2.Controls.Add(this.btnOpenRestoreFile);
            this.groupBox2.Controls.Add(this.txtSqlFileFrom);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(406, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 296);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RESTORE DATABASE";
            // 
            // btnRestoreDatabase
            // 
            this.btnRestoreDatabase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestoreDatabase.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRestoreDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestoreDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnRestoreDatabase.Image")));
            this.btnRestoreDatabase.Location = new System.Drawing.Point(129, 237);
            this.btnRestoreDatabase.Name = "btnRestoreDatabase";
            this.btnRestoreDatabase.Size = new System.Drawing.Size(101, 40);
            this.btnRestoreDatabase.TabIndex = 61;
            this.btnRestoreDatabase.Text = "Restore";
            this.btnRestoreDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestoreDatabase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestoreDatabase.UseVisualStyleBackColor = false;
            this.btnRestoreDatabase.Click += new System.EventHandler(this.btnRestoreDatabase_Click);
            // 
            // txtRestoreServer
            // 
            this.txtRestoreServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestoreServer.Location = new System.Drawing.Point(120, 73);
            this.txtRestoreServer.Name = "txtRestoreServer";
            this.txtRestoreServer.Size = new System.Drawing.Size(212, 25);
            this.txtRestoreServer.TabIndex = 63;
            this.txtRestoreServer.Text = "localhost";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(29, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 17);
            this.label9.TabIndex = 64;
            this.label9.Text = "Server";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 17);
            this.label12.TabIndex = 66;
            this.label12.Text = "Database";
            // 
            // txtRestoreDatabase
            // 
            this.txtRestoreDatabase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestoreDatabase.Location = new System.Drawing.Point(120, 104);
            this.txtRestoreDatabase.Name = "txtRestoreDatabase";
            this.txtRestoreDatabase.Size = new System.Drawing.Size(212, 25);
            this.txtRestoreDatabase.TabIndex = 65;
            this.txtRestoreDatabase.Text = "jc_softwares_srt_hardware_prod";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(29, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 17);
            this.label13.TabIndex = 68;
            this.label13.Text = "User Id";
            // 
            // txtRestoreUserId
            // 
            this.txtRestoreUserId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestoreUserId.Location = new System.Drawing.Point(120, 135);
            this.txtRestoreUserId.Name = "txtRestoreUserId";
            this.txtRestoreUserId.Size = new System.Drawing.Size(212, 25);
            this.txtRestoreUserId.TabIndex = 67;
            this.txtRestoreUserId.Text = "root";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(29, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 17);
            this.label14.TabIndex = 72;
            this.label14.Text = "Port";
            // 
            // txtRestorePort
            // 
            this.txtRestorePort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestorePort.Location = new System.Drawing.Point(120, 197);
            this.txtRestorePort.Name = "txtRestorePort";
            this.txtRestorePort.Size = new System.Drawing.Size(212, 25);
            this.txtRestorePort.TabIndex = 71;
            this.txtRestorePort.Text = "3306";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(29, 169);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 17);
            this.label15.TabIndex = 70;
            this.label15.Text = "Password";
            // 
            // txtRestorePassword
            // 
            this.txtRestorePassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestorePassword.Location = new System.Drawing.Point(120, 166);
            this.txtRestorePassword.Name = "txtRestorePassword";
            this.txtRestorePassword.PasswordChar = '*';
            this.txtRestorePassword.Size = new System.Drawing.Size(212, 25);
            this.txtRestorePassword.TabIndex = 69;
            this.txtRestorePassword.Text = "admin";
            // 
            // btnOpenRestoreFile
            // 
            this.btnOpenRestoreFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRestoreFile.Image")));
            this.btnOpenRestoreFile.Location = new System.Drawing.Point(302, 41);
            this.btnOpenRestoreFile.Name = "btnOpenRestoreFile";
            this.btnOpenRestoreFile.Size = new System.Drawing.Size(30, 25);
            this.btnOpenRestoreFile.TabIndex = 62;
            this.btnOpenRestoreFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenRestoreFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenRestoreFile.UseVisualStyleBackColor = true;
            this.btnOpenRestoreFile.Click += new System.EventHandler(this.btnOpenRestoreFile_Click);
            // 
            // txtSqlFileFrom
            // 
            this.txtSqlFileFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlFileFrom.Location = new System.Drawing.Point(120, 42);
            this.txtSqlFileFrom.Name = "txtSqlFileFrom";
            this.txtSqlFileFrom.Size = new System.Drawing.Size(176, 25);
            this.txtSqlFileFrom.TabIndex = 59;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 17);
            this.label8.TabIndex = 60;
            this.label8.Text = "Sql File (From)";
            // 
            // DatabaseBackupRestoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 388);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseBackupRestoreUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Backup/Restore";
            this.Load += new System.EventHandler(this.DatabaseBackupRestoreUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBackupDatabase;
        private System.Windows.Forms.TextBox txtSaveFileTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBackupServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBackupDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackupUserId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBackupPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBackupPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRestoreDatabase;
        private System.Windows.Forms.TextBox txtRestoreServer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRestoreDatabase;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRestoreUserId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRestorePort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRestorePassword;
        private System.Windows.Forms.Button btnOpenRestoreFile;
        private System.Windows.Forms.TextBox txtSqlFileFrom;
        private System.Windows.Forms.Label label8;
    }
}