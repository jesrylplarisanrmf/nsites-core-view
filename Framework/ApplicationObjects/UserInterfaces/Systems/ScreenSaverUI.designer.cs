namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    partial class ScreenSaverUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaverUI));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pctCurrentPicture = new System.Windows.Forms.PictureBox();
            this.pctPreviousPicture = new System.Windows.Forms.PictureBox();
            this.btnNoPicture = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreviousPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current Picture";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(450, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Previous Image";
            // 
            // pctCurrentPicture
            // 
            this.pctCurrentPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pctCurrentPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctCurrentPicture.Location = new System.Drawing.Point(12, 33);
            this.pctCurrentPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pctCurrentPicture.Name = "pctCurrentPicture";
            this.pctCurrentPicture.Size = new System.Drawing.Size(432, 295);
            this.pctCurrentPicture.TabIndex = 10;
            this.pctCurrentPicture.TabStop = false;
            // 
            // pctPreviousPicture
            // 
            this.pctPreviousPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pctPreviousPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctPreviousPicture.Location = new System.Drawing.Point(450, 33);
            this.pctPreviousPicture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pctPreviousPicture.Name = "pctPreviousPicture";
            this.pctPreviousPicture.Size = new System.Drawing.Size(433, 295);
            this.pctPreviousPicture.TabIndex = 9;
            this.pctPreviousPicture.TabStop = false;
            // 
            // btnNoPicture
            // 
            this.btnNoPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNoPicture.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNoPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoPicture.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNoPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnNoPicture.Image")));
            this.btnNoPicture.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNoPicture.Location = new System.Drawing.Point(225, 335);
            this.btnNoPicture.Name = "btnNoPicture";
            this.btnNoPicture.Size = new System.Drawing.Size(113, 45);
            this.btnNoPicture.TabIndex = 22;
            this.btnNoPicture.Text = " &No Picture";
            this.btnNoPicture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNoPicture.UseVisualStyleBackColor = false;
            this.btnNoPicture.Click += new System.EventHandler(this.btnNoPicture_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFind.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.Location = new System.Drawing.Point(344, 335);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(100, 45);
            this.btnFind.TabIndex = 21;
            this.btnFind.Text = " &Find";
            this.btnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnCancel.Location = new System.Drawing.Point(784, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 45);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = " &Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(679, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 45);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = " &Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ScreenSaverUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(895, 393);
            this.Controls.Add(this.btnNoPicture);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pctCurrentPicture);
            this.Controls.Add(this.pctPreviousPicture);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScreenSaverUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Saver";
            this.Load += new System.EventHandler(this.ScreenSaverUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreviousPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pctCurrentPicture;
        private System.Windows.Forms.PictureBox pctPreviousPicture;
        private System.Windows.Forms.Button btnNoPicture;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}