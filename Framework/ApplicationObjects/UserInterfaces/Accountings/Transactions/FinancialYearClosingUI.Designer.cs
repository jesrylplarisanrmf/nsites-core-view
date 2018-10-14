namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    partial class FinancialYearClosingUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinancialYearClosingUI));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.txtTotalDebit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalCredit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblMaturityDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvIncomeEntry = new System.Windows.Forms.DataGridView();
            this.IncomeAccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeAccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeAccountTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvExpenseEntry = new System.Windows.Forms.DataGridView();
            this.ExpenseAccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseAccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseAccountTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenseCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvRetainedEarningEntry = new System.Windows.Forms.DataGridView();
            this.REAccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REAccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REAccountTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFinancialYear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeEntry)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseEntry)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetainedEarningEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(259, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 40);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = " &Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.btnClose);
            this.pnlBody.Controls.Add(this.txtTotalDebit);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.txtTotalCredit);
            this.pnlBody.Controls.Add(this.label5);
            this.pnlBody.Controls.Add(this.dtpDate);
            this.pnlBody.Controls.Add(this.lblMaturityDate);
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.tabControl1);
            this.pnlBody.Controls.Add(this.txtFinancialYear);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Location = new System.Drawing.Point(12, 12);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(619, 423);
            this.pnlBody.TabIndex = 8;
            // 
            // txtTotalDebit
            // 
            this.txtTotalDebit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDebit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDebit.Location = new System.Drawing.Point(214, 327);
            this.txtTotalDebit.Name = "txtTotalDebit";
            this.txtTotalDebit.Size = new System.Drawing.Size(133, 25);
            this.txtTotalDebit.TabIndex = 228;
            this.txtTotalDebit.Text = "0.00";
            this.txtTotalDebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(135, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 227;
            this.label4.Text = "Total Debit";
            // 
            // txtTotalCredit
            // 
            this.txtTotalCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalCredit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCredit.Location = new System.Drawing.Point(452, 327);
            this.txtTotalCredit.Name = "txtTotalCredit";
            this.txtTotalCredit.Size = new System.Drawing.Size(133, 25);
            this.txtTotalCredit.TabIndex = 226;
            this.txtTotalCredit.Text = "0.00";
            this.txtTotalCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(369, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 225;
            this.label5.Text = "Total Credit";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MM-dd-yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(66, 54);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 25);
            this.dtpDate.TabIndex = 217;
            // 
            // lblMaturityDate
            // 
            this.lblMaturityDate.AutoSize = true;
            this.lblMaturityDate.Location = new System.Drawing.Point(25, 58);
            this.lblMaturityDate.Name = "lblMaturityDate";
            this.lblMaturityDate.Size = new System.Drawing.Size(35, 17);
            this.lblMaturityDate.TabIndex = 216;
            this.lblMaturityDate.Text = "Date";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.label3.Location = new System.Drawing.Point(349, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 32);
            this.label3.TabIndex = 215;
            this.label3.Text = "Financial Year Closing";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(25, 90);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(567, 231);
            this.tabControl1.TabIndex = 214;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvIncomeEntry);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(559, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Income Entry";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvIncomeEntry
            // 
            this.dgvIncomeEntry.AllowUserToAddRows = false;
            this.dgvIncomeEntry.AllowUserToDeleteRows = false;
            this.dgvIncomeEntry.AllowUserToResizeColumns = false;
            this.dgvIncomeEntry.AllowUserToResizeRows = false;
            this.dgvIncomeEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIncomeEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncomeEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIncomeEntry.ColumnHeadersHeight = 25;
            this.dgvIncomeEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvIncomeEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IncomeAccountId,
            this.IncomeAccountCode,
            this.IncomeAccountTitle,
            this.IncomeDebit,
            this.IncomeCredit});
            this.dgvIncomeEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncomeEntry.Location = new System.Drawing.Point(3, 3);
            this.dgvIncomeEntry.MultiSelect = false;
            this.dgvIncomeEntry.Name = "dgvIncomeEntry";
            this.dgvIncomeEntry.ReadOnly = true;
            this.dgvIncomeEntry.RowHeadersVisible = false;
            this.dgvIncomeEntry.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvIncomeEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncomeEntry.Size = new System.Drawing.Size(553, 195);
            this.dgvIncomeEntry.TabIndex = 208;
            // 
            // IncomeAccountId
            // 
            this.IncomeAccountId.HeaderText = "AccountId";
            this.IncomeAccountId.Name = "IncomeAccountId";
            this.IncomeAccountId.ReadOnly = true;
            this.IncomeAccountId.Visible = false;
            this.IncomeAccountId.Width = 90;
            // 
            // IncomeAccountCode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IncomeAccountCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.IncomeAccountCode.HeaderText = "Account Code";
            this.IncomeAccountCode.Name = "IncomeAccountCode";
            this.IncomeAccountCode.ReadOnly = true;
            this.IncomeAccountCode.Width = 114;
            // 
            // IncomeAccountTitle
            // 
            this.IncomeAccountTitle.HeaderText = "Account Title";
            this.IncomeAccountTitle.Name = "IncomeAccountTitle";
            this.IncomeAccountTitle.ReadOnly = true;
            this.IncomeAccountTitle.Width = 107;
            // 
            // IncomeDebit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IncomeDebit.DefaultCellStyle = dataGridViewCellStyle3;
            this.IncomeDebit.HeaderText = "Debit";
            this.IncomeDebit.Name = "IncomeDebit";
            this.IncomeDebit.ReadOnly = true;
            this.IncomeDebit.Width = 64;
            // 
            // IncomeCredit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IncomeCredit.DefaultCellStyle = dataGridViewCellStyle4;
            this.IncomeCredit.HeaderText = "Credit";
            this.IncomeCredit.Name = "IncomeCredit";
            this.IncomeCredit.ReadOnly = true;
            this.IncomeCredit.Width = 68;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvExpenseEntry);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(559, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Expense Entry";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvExpenseEntry
            // 
            this.dgvExpenseEntry.AllowUserToAddRows = false;
            this.dgvExpenseEntry.AllowUserToDeleteRows = false;
            this.dgvExpenseEntry.AllowUserToResizeColumns = false;
            this.dgvExpenseEntry.AllowUserToResizeRows = false;
            this.dgvExpenseEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvExpenseEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpenseEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvExpenseEntry.ColumnHeadersHeight = 25;
            this.dgvExpenseEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvExpenseEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExpenseAccountId,
            this.ExpenseAccountCode,
            this.ExpenseAccountTitle,
            this.ExpenseDebit,
            this.ExpenseCredit});
            this.dgvExpenseEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpenseEntry.Location = new System.Drawing.Point(3, 3);
            this.dgvExpenseEntry.MultiSelect = false;
            this.dgvExpenseEntry.Name = "dgvExpenseEntry";
            this.dgvExpenseEntry.ReadOnly = true;
            this.dgvExpenseEntry.RowHeadersVisible = false;
            this.dgvExpenseEntry.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvExpenseEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenseEntry.Size = new System.Drawing.Size(553, 195);
            this.dgvExpenseEntry.TabIndex = 209;
            // 
            // ExpenseAccountId
            // 
            this.ExpenseAccountId.HeaderText = "ExpenseAccountId";
            this.ExpenseAccountId.Name = "ExpenseAccountId";
            this.ExpenseAccountId.ReadOnly = true;
            this.ExpenseAccountId.Visible = false;
            this.ExpenseAccountId.Width = 138;
            // 
            // ExpenseAccountCode
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ExpenseAccountCode.DefaultCellStyle = dataGridViewCellStyle6;
            this.ExpenseAccountCode.HeaderText = "Account Code";
            this.ExpenseAccountCode.Name = "ExpenseAccountCode";
            this.ExpenseAccountCode.ReadOnly = true;
            this.ExpenseAccountCode.Width = 114;
            // 
            // ExpenseAccountTitle
            // 
            this.ExpenseAccountTitle.HeaderText = "Account Title";
            this.ExpenseAccountTitle.Name = "ExpenseAccountTitle";
            this.ExpenseAccountTitle.ReadOnly = true;
            this.ExpenseAccountTitle.Width = 107;
            // 
            // ExpenseDebit
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ExpenseDebit.DefaultCellStyle = dataGridViewCellStyle7;
            this.ExpenseDebit.HeaderText = "Debit";
            this.ExpenseDebit.Name = "ExpenseDebit";
            this.ExpenseDebit.ReadOnly = true;
            this.ExpenseDebit.Width = 64;
            // 
            // ExpenseCredit
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ExpenseCredit.DefaultCellStyle = dataGridViewCellStyle8;
            this.ExpenseCredit.HeaderText = "Credit";
            this.ExpenseCredit.Name = "ExpenseCredit";
            this.ExpenseCredit.ReadOnly = true;
            this.ExpenseCredit.Width = 68;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvRetainedEarningEntry);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(559, 201);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Retained Earnings Entry";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvRetainedEarningEntry
            // 
            this.dgvRetainedEarningEntry.AllowUserToAddRows = false;
            this.dgvRetainedEarningEntry.AllowUserToDeleteRows = false;
            this.dgvRetainedEarningEntry.AllowUserToResizeColumns = false;
            this.dgvRetainedEarningEntry.AllowUserToResizeRows = false;
            this.dgvRetainedEarningEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRetainedEarningEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRetainedEarningEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRetainedEarningEntry.ColumnHeadersHeight = 25;
            this.dgvRetainedEarningEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRetainedEarningEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.REAccountId,
            this.REAccountCode,
            this.REAccountTitle,
            this.REDebit,
            this.RECredit});
            this.dgvRetainedEarningEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRetainedEarningEntry.Location = new System.Drawing.Point(3, 3);
            this.dgvRetainedEarningEntry.MultiSelect = false;
            this.dgvRetainedEarningEntry.Name = "dgvRetainedEarningEntry";
            this.dgvRetainedEarningEntry.ReadOnly = true;
            this.dgvRetainedEarningEntry.RowHeadersVisible = false;
            this.dgvRetainedEarningEntry.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRetainedEarningEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRetainedEarningEntry.Size = new System.Drawing.Size(553, 195);
            this.dgvRetainedEarningEntry.TabIndex = 210;
            // 
            // REAccountId
            // 
            this.REAccountId.HeaderText = "REAccountId";
            this.REAccountId.Name = "REAccountId";
            this.REAccountId.ReadOnly = true;
            this.REAccountId.Visible = false;
            this.REAccountId.Width = 105;
            // 
            // REAccountCode
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.REAccountCode.DefaultCellStyle = dataGridViewCellStyle10;
            this.REAccountCode.HeaderText = "Account Code";
            this.REAccountCode.Name = "REAccountCode";
            this.REAccountCode.ReadOnly = true;
            this.REAccountCode.Width = 114;
            // 
            // REAccountTitle
            // 
            this.REAccountTitle.HeaderText = "Account Title";
            this.REAccountTitle.Name = "REAccountTitle";
            this.REAccountTitle.ReadOnly = true;
            this.REAccountTitle.Width = 107;
            // 
            // REDebit
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.REDebit.DefaultCellStyle = dataGridViewCellStyle11;
            this.REDebit.HeaderText = "Debit";
            this.REDebit.Name = "REDebit";
            this.REDebit.ReadOnly = true;
            this.REDebit.Width = 64;
            // 
            // RECredit
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RECredit.DefaultCellStyle = dataGridViewCellStyle12;
            this.RECredit.HeaderText = "Credit";
            this.RECredit.Name = "RECredit";
            this.RECredit.ReadOnly = true;
            this.RECredit.Width = 68;
            // 
            // txtFinancialYear
            // 
            this.txtFinancialYear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtFinancialYear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinancialYear.Location = new System.Drawing.Point(66, 23);
            this.txtFinancialYear.Name = "txtFinancialYear";
            this.txtFinancialYear.ReadOnly = true;
            this.txtFinancialYear.Size = new System.Drawing.Size(110, 25);
            this.txtFinancialYear.TabIndex = 213;
            this.txtFinancialYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "F.Y.";
            // 
            // FinancialYearClosingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(643, 447);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FinancialYearClosingUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Year Closing";
            this.Load += new System.EventHandler(this.FinancialYearClosingUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeEntry)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseEntry)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetainedEarningEntry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtFinancialYear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvIncomeEntry;
        private System.Windows.Forms.DataGridView dgvExpenseEntry;
        private System.Windows.Forms.DataGridView dgvRetainedEarningEntry;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblMaturityDate;
        private System.Windows.Forms.TextBox txtTotalDebit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeAccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeAccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeAccountTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseAccountTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenseCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn REAccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn REAccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn REAccountTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn REDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECredit;
    }
}