namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports
{
    partial class StockInventoryUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockInventoryUI));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnPreviewAllStocksByGroup = new System.Windows.Forms.Button();
            this.btnPreviewAllStocks = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvListAllStocks = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvListByLocation = new System.Windows.Forms.DataGridView();
            this.btnPreviewByGroupByLocation = new System.Windows.Forms.Button();
            this.btnPreviewByLocation = new System.Windows.Forms.Button();
            this.txtSearchByLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListAllStocks)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListByLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(11, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 49);
            this.panel1.TabIndex = 114;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.Location = new System.Drawing.Point(816, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(101, 40);
            this.btnRefresh.TabIndex = 71;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.label3.Location = new System.Drawing.Point(6, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 40);
            this.label3.TabIndex = 57;
            this.label3.Text = "Stock Inventory";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(920, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 40);
            this.btnClose.TabIndex = 53;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 195;
            this.label1.Text = "Location";
            // 
            // cboLocation
            // 
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(69, 12);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(222, 25);
            this.cboLocation.TabIndex = 194;
            this.cboLocation.SelectedIndexChanged += new System.EventHandler(this.cboLocation_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(11, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 461);
            this.tabControl1.TabIndex = 116;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnPreviewAllStocksByGroup);
            this.tabPage1.Controls.Add(this.btnPreviewAllStocks);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dgvListAllStocks);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(956, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ALL STOCKS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnPreviewAllStocksByGroup
            // 
            this.btnPreviewAllStocksByGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewAllStocksByGroup.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreviewAllStocksByGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewAllStocksByGroup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPreviewAllStocksByGroup.Location = new System.Drawing.Point(817, 9);
            this.btnPreviewAllStocksByGroup.Name = "btnPreviewAllStocksByGroup";
            this.btnPreviewAllStocksByGroup.Size = new System.Drawing.Size(133, 28);
            this.btnPreviewAllStocksByGroup.TabIndex = 81;
            this.btnPreviewAllStocksByGroup.Text = "Preview by Group";
            this.btnPreviewAllStocksByGroup.UseVisualStyleBackColor = false;
            this.btnPreviewAllStocksByGroup.Click += new System.EventHandler(this.btnPreviewAllStocksByGroup_Click);
            // 
            // btnPreviewAllStocks
            // 
            this.btnPreviewAllStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewAllStocks.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreviewAllStocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewAllStocks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPreviewAllStocks.Location = new System.Drawing.Point(726, 9);
            this.btnPreviewAllStocks.Name = "btnPreviewAllStocks";
            this.btnPreviewAllStocks.Size = new System.Drawing.Size(85, 28);
            this.btnPreviewAllStocks.TabIndex = 80;
            this.btnPreviewAllStocks.Text = "Preview";
            this.btnPreviewAllStocks.UseVisualStyleBackColor = false;
            this.btnPreviewAllStocks.Click += new System.EventHandler(this.btnPreviewAllStocks_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSearch.Location = new System.Drawing.Point(59, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(207, 25);
            this.txtSearch.TabIndex = 79;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 78;
            this.label2.Text = "Search";
            // 
            // dgvListAllStocks
            // 
            this.dgvListAllStocks.AllowUserToAddRows = false;
            this.dgvListAllStocks.AllowUserToDeleteRows = false;
            this.dgvListAllStocks.AllowUserToResizeRows = false;
            this.dgvListAllStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListAllStocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvListAllStocks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListAllStocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListAllStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvListAllStocks.Location = new System.Drawing.Point(0, 43);
            this.dgvListAllStocks.MultiSelect = false;
            this.dgvListAllStocks.Name = "dgvListAllStocks";
            this.dgvListAllStocks.ReadOnly = true;
            this.dgvListAllStocks.RowHeadersVisible = false;
            this.dgvListAllStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListAllStocks.Size = new System.Drawing.Size(956, 388);
            this.dgvListAllStocks.TabIndex = 77;
            this.dgvListAllStocks.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvListAllStocks_CellFormatting);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvListByLocation);
            this.tabPage3.Controls.Add(this.btnPreviewByGroupByLocation);
            this.tabPage3.Controls.Add(this.btnPreviewByLocation);
            this.tabPage3.Controls.Add(this.txtSearchByLocation);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.cboLocation);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(956, 431);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "BY LOCATION";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvListByLocation
            // 
            this.dgvListByLocation.AllowUserToAddRows = false;
            this.dgvListByLocation.AllowUserToDeleteRows = false;
            this.dgvListByLocation.AllowUserToResizeRows = false;
            this.dgvListByLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListByLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvListByLocation.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListByLocation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListByLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvListByLocation.Location = new System.Drawing.Point(0, 43);
            this.dgvListByLocation.MultiSelect = false;
            this.dgvListByLocation.Name = "dgvListByLocation";
            this.dgvListByLocation.ReadOnly = true;
            this.dgvListByLocation.RowHeadersVisible = false;
            this.dgvListByLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListByLocation.Size = new System.Drawing.Size(956, 388);
            this.dgvListByLocation.TabIndex = 200;
            this.dgvListByLocation.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvListByLocation_CellFormatting);
            // 
            // btnPreviewByGroupByLocation
            // 
            this.btnPreviewByGroupByLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewByGroupByLocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreviewByGroupByLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewByGroupByLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPreviewByGroupByLocation.Location = new System.Drawing.Point(817, 9);
            this.btnPreviewByGroupByLocation.Name = "btnPreviewByGroupByLocation";
            this.btnPreviewByGroupByLocation.Size = new System.Drawing.Size(133, 28);
            this.btnPreviewByGroupByLocation.TabIndex = 199;
            this.btnPreviewByGroupByLocation.Text = "Preview by Group";
            this.btnPreviewByGroupByLocation.UseVisualStyleBackColor = false;
            this.btnPreviewByGroupByLocation.Click += new System.EventHandler(this.btnPreviewByGroupByLocation_Click);
            // 
            // btnPreviewByLocation
            // 
            this.btnPreviewByLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewByLocation.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPreviewByLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewByLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPreviewByLocation.Location = new System.Drawing.Point(726, 9);
            this.btnPreviewByLocation.Name = "btnPreviewByLocation";
            this.btnPreviewByLocation.Size = new System.Drawing.Size(85, 28);
            this.btnPreviewByLocation.TabIndex = 198;
            this.btnPreviewByLocation.Text = "Preview";
            this.btnPreviewByLocation.UseVisualStyleBackColor = false;
            this.btnPreviewByLocation.Click += new System.EventHandler(this.btnPreviewByLocation_Click);
            // 
            // txtSearchByLocation
            // 
            this.txtSearchByLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSearchByLocation.Location = new System.Drawing.Point(369, 12);
            this.txtSearchByLocation.Name = "txtSearchByLocation";
            this.txtSearchByLocation.Size = new System.Drawing.Size(207, 25);
            this.txtSearchByLocation.TabIndex = 197;
            this.txtSearchByLocation.TextChanged += new System.EventHandler(this.txtSearchByLocation_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(316, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 196;
            this.label4.Text = "Search";
            // 
            // StockInventoryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 535);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StockInventoryUI";
            this.Text = "Stock Inventory";
            this.Load += new System.EventHandler(this.StockInventoryUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListAllStocks)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListByLocation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnPreviewAllStocksByGroup;
        private System.Windows.Forms.Button btnPreviewAllStocks;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvListAllStocks;
        private System.Windows.Forms.Button btnPreviewByGroupByLocation;
        private System.Windows.Forms.Button btnPreviewByLocation;
        private System.Windows.Forms.TextBox txtSearchByLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvListByLocation;
    }
}