namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    partial class VoucherDetailUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoucherDetailUI));
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.btnLookupSubsidiary = new System.Windows.Forms.Button();
            this.cboSubsidiary = new System.Windows.Forms.ComboBox();
            this.lblSubsidiary = new System.Windows.Forms.Label();
            this.txtSubsidiaryId = new System.Windows.Forms.TextBox();
            this.lblSubsidiaryTitle = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboChartOfAccount = new System.Windows.Forms.ComboBox();
            this.btnGetAccountCode = new System.Windows.Forms.Button();
            this.txtDebit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(181, 256);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "  &OK";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.btnSave);
            this.pnlBody.Controls.Add(this.btnLookupSubsidiary);
            this.pnlBody.Controls.Add(this.cboSubsidiary);
            this.pnlBody.Controls.Add(this.lblSubsidiary);
            this.pnlBody.Controls.Add(this.txtSubsidiaryId);
            this.pnlBody.Controls.Add(this.lblSubsidiaryTitle);
            this.pnlBody.Controls.Add(this.txtCredit);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.cboChartOfAccount);
            this.pnlBody.Controls.Add(this.btnGetAccountCode);
            this.pnlBody.Controls.Add(this.txtDebit);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.txtRemarks);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Location = new System.Drawing.Point(12, 11);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(485, 319);
            this.pnlBody.TabIndex = 6;
            // 
            // btnLookupSubsidiary
            // 
            this.btnLookupSubsidiary.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLookupSubsidiary.Enabled = false;
            this.btnLookupSubsidiary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLookupSubsidiary.Image = ((System.Drawing.Image)(resources.GetObject("btnLookupSubsidiary.Image")));
            this.btnLookupSubsidiary.Location = new System.Drawing.Point(428, 146);
            this.btnLookupSubsidiary.Name = "btnLookupSubsidiary";
            this.btnLookupSubsidiary.Size = new System.Drawing.Size(25, 25);
            this.btnLookupSubsidiary.TabIndex = 4;
            this.btnLookupSubsidiary.UseVisualStyleBackColor = false;
            this.btnLookupSubsidiary.Click += new System.EventHandler(this.btnLookupSubsidiary_Click);
            // 
            // cboSubsidiary
            // 
            this.cboSubsidiary.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSubsidiary.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSubsidiary.Enabled = false;
            this.cboSubsidiary.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSubsidiary.Location = new System.Drawing.Point(111, 147);
            this.cboSubsidiary.Name = "cboSubsidiary";
            this.cboSubsidiary.Size = new System.Drawing.Size(311, 25);
            this.cboSubsidiary.TabIndex = 3;
            // 
            // lblSubsidiary
            // 
            this.lblSubsidiary.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubsidiary.Location = new System.Drawing.Point(111, 124);
            this.lblSubsidiary.Name = "lblSubsidiary";
            this.lblSubsidiary.Size = new System.Drawing.Size(155, 20);
            this.lblSubsidiary.TabIndex = 208;
            // 
            // txtSubsidiaryId
            // 
            this.txtSubsidiaryId.Location = new System.Drawing.Point(97, 147);
            this.txtSubsidiaryId.Name = "txtSubsidiaryId";
            this.txtSubsidiaryId.ReadOnly = true;
            this.txtSubsidiaryId.Size = new System.Drawing.Size(11, 25);
            this.txtSubsidiaryId.TabIndex = 207;
            this.txtSubsidiaryId.Visible = false;
            // 
            // lblSubsidiaryTitle
            // 
            this.lblSubsidiaryTitle.AutoSize = true;
            this.lblSubsidiaryTitle.Location = new System.Drawing.Point(20, 150);
            this.lblSubsidiaryTitle.Name = "lblSubsidiaryTitle";
            this.lblSubsidiaryTitle.Size = new System.Drawing.Size(68, 17);
            this.lblSubsidiaryTitle.TabIndex = 204;
            this.lblSubsidiaryTitle.Text = "Subsidiary";
            // 
            // txtCredit
            // 
            this.txtCredit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCredit.Location = new System.Drawing.Point(281, 85);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(141, 25);
            this.txtCredit.TabIndex = 2;
            this.txtCredit.Text = "0.00";
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCredit.TextChanged += new System.EventHandler(this.txtCredit_TextChanged);
            this.txtCredit.Leave += new System.EventHandler(this.txtCredit_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 203;
            this.label2.Text = "Credit";
            // 
            // cboChartOfAccount
            // 
            this.cboChartOfAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboChartOfAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboChartOfAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboChartOfAccount.Location = new System.Drawing.Point(111, 27);
            this.cboChartOfAccount.Name = "cboChartOfAccount";
            this.cboChartOfAccount.Size = new System.Drawing.Size(311, 25);
            this.cboChartOfAccount.TabIndex = 0;
            this.cboChartOfAccount.SelectedIndexChanged += new System.EventHandler(this.cboChartOfAccount_SelectedIndexChanged);
            // 
            // btnGetAccountCode
            // 
            this.btnGetAccountCode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetAccountCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetAccountCode.Image = ((System.Drawing.Image)(resources.GetObject("btnGetAccountCode.Image")));
            this.btnGetAccountCode.Location = new System.Drawing.Point(428, 27);
            this.btnGetAccountCode.Name = "btnGetAccountCode";
            this.btnGetAccountCode.Size = new System.Drawing.Size(25, 25);
            this.btnGetAccountCode.TabIndex = 1;
            this.btnGetAccountCode.UseVisualStyleBackColor = false;
            this.btnGetAccountCode.Click += new System.EventHandler(this.btnGetAccountCode_Click);
            // 
            // txtDebit
            // 
            this.txtDebit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtDebit.Location = new System.Drawing.Point(111, 85);
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Size = new System.Drawing.Size(141, 25);
            this.txtDebit.TabIndex = 1;
            this.txtDebit.Text = "0.00";
            this.txtDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDebit.TextChanged += new System.EventHandler(this.txtDebit_TextChanged);
            this.txtDebit.Leave += new System.EventHandler(this.txtDebit_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Debit";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtRemarks.Location = new System.Drawing.Point(111, 178);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(342, 50);
            this.txtRemarks.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Remarks";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Title";
            // 
            // VoucherDetailUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(509, 342);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VoucherDetailUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Voucher Detail";
            this.Load += new System.EventHandler(this.PlacementDetailUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDebit;
        private System.Windows.Forms.Button btnGetAccountCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboChartOfAccount;
        private System.Windows.Forms.TextBox txtCredit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubsidiary;
        private System.Windows.Forms.TextBox txtSubsidiaryId;
        private System.Windows.Forms.Label lblSubsidiaryTitle;
        private System.Windows.Forms.ComboBox cboSubsidiary;
        private System.Windows.Forms.Button btnLookupSubsidiary;
    }
}