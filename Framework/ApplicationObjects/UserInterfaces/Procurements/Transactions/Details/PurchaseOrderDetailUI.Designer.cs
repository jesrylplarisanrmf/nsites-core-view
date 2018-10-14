namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details
{
    partial class PurchaseOrderDetailUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrderDetailUI));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.lblTotalPOQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalQtyIn = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotalQtyVariance = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpDateNeeded = new System.Windows.Forms.DateTimePicker();
            this.btnAddStockInventory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEditStockInventory = new System.Windows.Forms.Button();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.btnDeleteStockInventory = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnDeleteAllStockInventory = new System.Windows.Forms.Button();
            this.btnLookUpPurchaseRequest = new System.Windows.Forms.Button();
            this.dgvDetailStockInventory = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyVariance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.cboPurchaseRequest = new System.Windows.Forms.ComboBox();
            this.cboRequestedBy = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTerms = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblMaturityDate = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailStockInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.lblTotalPOQty);
            this.pnlBody.Controls.Add(this.label8);
            this.pnlBody.Controls.Add(this.lblTotalQtyIn);
            this.pnlBody.Controls.Add(this.label14);
            this.pnlBody.Controls.Add(this.lblTotalQtyVariance);
            this.pnlBody.Controls.Add(this.label12);
            this.pnlBody.Controls.Add(this.dtpDateNeeded);
            this.pnlBody.Controls.Add(this.btnAddStockInventory);
            this.pnlBody.Controls.Add(this.label4);
            this.pnlBody.Controls.Add(this.btnEditStockInventory);
            this.pnlBody.Controls.Add(this.dtpDueDate);
            this.pnlBody.Controls.Add(this.btnDeleteStockInventory);
            this.pnlBody.Controls.Add(this.label16);
            this.pnlBody.Controls.Add(this.btnDeleteAllStockInventory);
            this.pnlBody.Controls.Add(this.btnLookUpPurchaseRequest);
            this.pnlBody.Controls.Add(this.dgvDetailStockInventory);
            this.pnlBody.Controls.Add(this.label15);
            this.pnlBody.Controls.Add(this.cboPurchaseRequest);
            this.pnlBody.Controls.Add(this.cboRequestedBy);
            this.pnlBody.Controls.Add(this.label13);
            this.pnlBody.Controls.Add(this.txtInstructions);
            this.pnlBody.Controls.Add(this.label9);
            this.pnlBody.Controls.Add(this.txtTerms);
            this.pnlBody.Controls.Add(this.label6);
            this.pnlBody.Controls.Add(this.label11);
            this.pnlBody.Controls.Add(this.txtTotalAmount);
            this.pnlBody.Controls.Add(this.cboSupplier);
            this.pnlBody.Controls.Add(this.label10);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.btnSave);
            this.pnlBody.Controls.Add(this.txtId);
            this.pnlBody.Controls.Add(this.txtRemarks);
            this.pnlBody.Controls.Add(this.label7);
            this.pnlBody.Controls.Add(this.txtReference);
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.label5);
            this.pnlBody.Controls.Add(this.dtpDate);
            this.pnlBody.Controls.Add(this.lblMaturityDate);
            this.pnlBody.Location = new System.Drawing.Point(13, 11);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1035, 550);
            this.pnlBody.TabIndex = 14;
            // 
            // lblTotalPOQty
            // 
            this.lblTotalPOQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPOQty.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPOQty.Location = new System.Drawing.Point(877, 121);
            this.lblTotalPOQty.Name = "lblTotalPOQty";
            this.lblTotalPOQty.Size = new System.Drawing.Size(128, 17);
            this.lblTotalPOQty.TabIndex = 239;
            this.lblTotalPOQty.Text = "0.00";
            this.lblTotalPOQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(786, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 17);
            this.label8.TabIndex = 238;
            this.label8.Text = "Total P.O. Qty";
            // 
            // lblTotalQtyIn
            // 
            this.lblTotalQtyIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalQtyIn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQtyIn.Location = new System.Drawing.Point(877, 145);
            this.lblTotalQtyIn.Name = "lblTotalQtyIn";
            this.lblTotalQtyIn.Size = new System.Drawing.Size(128, 17);
            this.lblTotalQtyIn.TabIndex = 237;
            this.lblTotalQtyIn.Text = "0.00";
            this.lblTotalQtyIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(799, 145);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 17);
            this.label14.TabIndex = 236;
            this.label14.Text = "Total Qty In";
            // 
            // lblTotalQtyVariance
            // 
            this.lblTotalQtyVariance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalQtyVariance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQtyVariance.Location = new System.Drawing.Point(877, 169);
            this.lblTotalQtyVariance.Name = "lblTotalQtyVariance";
            this.lblTotalQtyVariance.Size = new System.Drawing.Size(128, 17);
            this.lblTotalQtyVariance.TabIndex = 235;
            this.lblTotalQtyVariance.Text = "0.00";
            this.lblTotalQtyVariance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(760, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 17);
            this.label12.TabIndex = 234;
            this.label12.Text = "Total Qty Variance";
            // 
            // dtpDateNeeded
            // 
            this.dtpDateNeeded.CustomFormat = "MM-dd-yyyy";
            this.dtpDateNeeded.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDateNeeded.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateNeeded.Location = new System.Drawing.Point(502, 121);
            this.dtpDateNeeded.Name = "dtpDateNeeded";
            this.dtpDateNeeded.Size = new System.Drawing.Size(110, 25);
            this.dtpDateNeeded.TabIndex = 229;
            // 
            // btnAddStockInventory
            // 
            this.btnAddStockInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStockInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAddStockInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStockInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddStockInventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddStockInventory.Location = new System.Drawing.Point(653, 204);
            this.btnAddStockInventory.Name = "btnAddStockInventory";
            this.btnAddStockInventory.Size = new System.Drawing.Size(85, 30);
            this.btnAddStockInventory.TabIndex = 5;
            this.btnAddStockInventory.Text = "Add";
            this.btnAddStockInventory.UseVisualStyleBackColor = false;
            this.btnAddStockInventory.Click += new System.EventHandler(this.btnAddStockInventory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 230;
            this.label4.Text = "Date Needed";
            // 
            // btnEditStockInventory
            // 
            this.btnEditStockInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditStockInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEditStockInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStockInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEditStockInventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditStockInventory.Location = new System.Drawing.Point(742, 204);
            this.btnEditStockInventory.Name = "btnEditStockInventory";
            this.btnEditStockInventory.Size = new System.Drawing.Size(85, 30);
            this.btnEditStockInventory.TabIndex = 6;
            this.btnEditStockInventory.Text = "Edit";
            this.btnEditStockInventory.UseVisualStyleBackColor = false;
            this.btnEditStockInventory.Click += new System.EventHandler(this.btnEditStockInventory_Click);
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDueDate.CustomFormat = "MM-dd-yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(880, 90);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(125, 25);
            this.dtpDueDate.TabIndex = 227;
            // 
            // btnDeleteStockInventory
            // 
            this.btnDeleteStockInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteStockInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDeleteStockInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteStockInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDeleteStockInventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteStockInventory.Location = new System.Drawing.Point(831, 204);
            this.btnDeleteStockInventory.Name = "btnDeleteStockInventory";
            this.btnDeleteStockInventory.Size = new System.Drawing.Size(85, 30);
            this.btnDeleteStockInventory.TabIndex = 7;
            this.btnDeleteStockInventory.Text = "Delete";
            this.btnDeleteStockInventory.UseVisualStyleBackColor = false;
            this.btnDeleteStockInventory.Click += new System.EventHandler(this.btnDeleteStockInventory_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(811, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 17);
            this.label16.TabIndex = 228;
            this.label16.Text = "Due Date";
            // 
            // btnDeleteAllStockInventory
            // 
            this.btnDeleteAllStockInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAllStockInventory.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDeleteAllStockInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllStockInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDeleteAllStockInventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllStockInventory.Location = new System.Drawing.Point(920, 204);
            this.btnDeleteAllStockInventory.Name = "btnDeleteAllStockInventory";
            this.btnDeleteAllStockInventory.Size = new System.Drawing.Size(85, 30);
            this.btnDeleteAllStockInventory.TabIndex = 8;
            this.btnDeleteAllStockInventory.Text = "Delete All";
            this.btnDeleteAllStockInventory.UseVisualStyleBackColor = false;
            this.btnDeleteAllStockInventory.Click += new System.EventHandler(this.btnDeleteAllStockInventory_Click);
            // 
            // btnLookUpPurchaseRequest
            // 
            this.btnLookUpPurchaseRequest.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLookUpPurchaseRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLookUpPurchaseRequest.Image = ((System.Drawing.Image)(resources.GetObject("btnLookUpPurchaseRequest.Image")));
            this.btnLookUpPurchaseRequest.Location = new System.Drawing.Point(618, 26);
            this.btnLookUpPurchaseRequest.Name = "btnLookUpPurchaseRequest";
            this.btnLookUpPurchaseRequest.Size = new System.Drawing.Size(37, 25);
            this.btnLookUpPurchaseRequest.TabIndex = 226;
            this.btnLookUpPurchaseRequest.UseVisualStyleBackColor = false;
            this.btnLookUpPurchaseRequest.Click += new System.EventHandler(this.btnLookUpPurchaseRequest_Click);
            // 
            // dgvDetailStockInventory
            // 
            this.dgvDetailStockInventory.AllowUserToAddRows = false;
            this.dgvDetailStockInventory.AllowUserToDeleteRows = false;
            this.dgvDetailStockInventory.AllowUserToResizeColumns = false;
            this.dgvDetailStockInventory.AllowUserToResizeRows = false;
            this.dgvDetailStockInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetailStockInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetailStockInventory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetailStockInventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetailStockInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetailStockInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.StockId,
            this.StockCode,
            this.StockDescription,
            this.Unit,
            this.LocationId,
            this.Location,
            this.POQty,
            this.QtyIn,
            this.QtyVariance,
            this.UnitPrice,
            this.DiscountId,
            this.Discount,
            this.DiscountAmount,
            this.TotalPrice,
            this.Remarks,
            this.Status});
            this.dgvDetailStockInventory.Location = new System.Drawing.Point(28, 240);
            this.dgvDetailStockInventory.MultiSelect = false;
            this.dgvDetailStockInventory.Name = "dgvDetailStockInventory";
            this.dgvDetailStockInventory.ReadOnly = true;
            this.dgvDetailStockInventory.RowHeadersVisible = false;
            this.dgvDetailStockInventory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDetailStockInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetailStockInventory.Size = new System.Drawing.Size(977, 237);
            this.dgvDetailStockInventory.TabIndex = 205;
            this.dgvDetailStockInventory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetailStockInventory_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // StockId
            // 
            this.StockId.HeaderText = "StockId";
            this.StockId.Name = "StockId";
            this.StockId.ReadOnly = true;
            this.StockId.Visible = false;
            // 
            // StockCode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StockCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.StockCode.FillWeight = 68.46558F;
            this.StockCode.HeaderText = "Stock Code";
            this.StockCode.Name = "StockCode";
            this.StockCode.ReadOnly = true;
            // 
            // StockDescription
            // 
            this.StockDescription.FillWeight = 171.164F;
            this.StockDescription.HeaderText = "Stock Description";
            this.StockDescription.Name = "StockDescription";
            this.StockDescription.ReadOnly = true;
            // 
            // Unit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Unit.FillWeight = 47.92591F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // LocationId
            // 
            this.LocationId.HeaderText = "LocationId";
            this.LocationId.Name = "LocationId";
            this.LocationId.ReadOnly = true;
            this.LocationId.Visible = false;
            // 
            // Location
            // 
            this.Location.FillWeight = 68.46558F;
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // POQty
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.POQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.POQty.FillWeight = 68.46558F;
            this.POQty.HeaderText = "P.O. Qty";
            this.POQty.Name = "POQty";
            this.POQty.ReadOnly = true;
            // 
            // QtyIn
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QtyIn.DefaultCellStyle = dataGridViewCellStyle5;
            this.QtyIn.FillWeight = 68.46558F;
            this.QtyIn.HeaderText = "Qty In";
            this.QtyIn.Name = "QtyIn";
            this.QtyIn.ReadOnly = true;
            // 
            // QtyVariance
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.QtyVariance.DefaultCellStyle = dataGridViewCellStyle6;
            this.QtyVariance.FillWeight = 68.46558F;
            this.QtyVariance.HeaderText = "Qty Variance";
            this.QtyVariance.Name = "QtyVariance";
            this.QtyVariance.ReadOnly = true;
            // 
            // UnitPrice
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UnitPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.UnitPrice.FillWeight = 82.1587F;
            this.UnitPrice.HeaderText = "Unit Price";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // DiscountId
            // 
            this.DiscountId.HeaderText = "DiscountId";
            this.DiscountId.Name = "DiscountId";
            this.DiscountId.ReadOnly = true;
            this.DiscountId.Visible = false;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            // 
            // DiscountAmount
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.DiscountAmount.FillWeight = 82.1587F;
            this.DiscountAmount.HeaderText = "Discount Amount";
            this.DiscountAmount.Name = "DiscountAmount";
            this.DiscountAmount.ReadOnly = true;
            // 
            // TotalPrice
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.TotalPrice.FillWeight = 102.6984F;
            this.TotalPrice.HeaderText = "Total Price";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.FillWeight = 102.6984F;
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(374, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 17);
            this.label15.TabIndex = 225;
            this.label15.Text = "Purchase Request Id";
            // 
            // cboPurchaseRequest
            // 
            this.cboPurchaseRequest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPurchaseRequest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPurchaseRequest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPurchaseRequest.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPurchaseRequest.Location = new System.Drawing.Point(502, 27);
            this.cboPurchaseRequest.Name = "cboPurchaseRequest";
            this.cboPurchaseRequest.Size = new System.Drawing.Size(110, 25);
            this.cboPurchaseRequest.TabIndex = 224;
            this.cboPurchaseRequest.SelectedIndexChanged += new System.EventHandler(this.cboPurchaseRequest_SelectedIndexChanged);
            // 
            // cboRequestedBy
            // 
            this.cboRequestedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRequestedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRequestedBy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboRequestedBy.FormattingEnabled = true;
            this.cboRequestedBy.Location = new System.Drawing.Point(502, 89);
            this.cboRequestedBy.Name = "cboRequestedBy";
            this.cboRequestedBy.Size = new System.Drawing.Size(211, 25);
            this.cboRequestedBy.TabIndex = 219;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(374, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 17);
            this.label13.TabIndex = 220;
            this.label13.Text = "Requested By";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtInstructions.Location = new System.Drawing.Point(99, 89);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(249, 57);
            this.txtInstructions.TabIndex = 216;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 17);
            this.label9.TabIndex = 215;
            this.label9.Text = "Instructions";
            // 
            // txtTerms
            // 
            this.txtTerms.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtTerms.Location = new System.Drawing.Point(719, 89);
            this.txtTerms.Name = "txtTerms";
            this.txtTerms.Size = new System.Drawing.Size(47, 25);
            this.txtTerms.TabIndex = 214;
            this.txtTerms.Text = "0";
            this.txtTerms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTerms.TextChanged += new System.EventHandler(this.txtTerms_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(716, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 213;
            this.label6.Text = "Terms";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(778, 486);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 17);
            this.label11.TabIndex = 207;
            this.label11.Text = "Total Amount";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.Location = new System.Drawing.Point(873, 483);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(132, 25);
            this.txtTotalAmount.TabIndex = 206;
            this.txtTotalAmount.TabStop = false;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboSupplier
            // 
            this.cboSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(99, 58);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(249, 25);
            this.cboSupplier.TabIndex = 201;
            this.cboSupplier.SelectedIndexChanged += new System.EventHandler(this.cboSupplier_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 202;
            this.label10.Text = "Supplier";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(853, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 187;
            this.label1.Text = "Id";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(458, 493);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "   &Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtId.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtId.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtId.Location = new System.Drawing.Point(880, 59);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(125, 25);
            this.txtId.TabIndex = 186;
            this.txtId.TabStop = false;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtRemarks.Location = new System.Drawing.Point(99, 152);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(513, 50);
            this.txtRemarks.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 184;
            this.label7.Text = "Remarks";
            // 
            // txtReference
            // 
            this.txtReference.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtReference.Location = new System.Drawing.Point(502, 58);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(153, 25);
            this.txtReference.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 176;
            this.label2.Text = "Reference";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(117)))));
            this.label5.Location = new System.Drawing.Point(832, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 32);
            this.label5.TabIndex = 174;
            this.label5.Text = "Purchase Order";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MM-dd-yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(99, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(117, 25);
            this.dtpDate.TabIndex = 0;
            // 
            // lblMaturityDate
            // 
            this.lblMaturityDate.AutoSize = true;
            this.lblMaturityDate.Location = new System.Drawing.Point(25, 29);
            this.lblMaturityDate.Name = "lblMaturityDate";
            this.lblMaturityDate.Size = new System.Drawing.Size(35, 17);
            this.lblMaturityDate.TabIndex = 170;
            this.lblMaturityDate.Text = "Date";
            // 
            // PurchaseOrderDetailUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(140)))));
            this.ClientSize = new System.Drawing.Size(1060, 572);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PurchaseOrderDetailUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Detail";
            this.Load += new System.EventHandler(this.PurchaseOrderDetailUI_Load);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailStockInventory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnLookUpPurchaseRequest;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboPurchaseRequest;
        private System.Windows.Forms.Button btnAddStockInventory;
        private System.Windows.Forms.Button btnEditStockInventory;
        private System.Windows.Forms.Button btnDeleteStockInventory;
        private System.Windows.Forms.Button btnDeleteAllStockInventory;
        private System.Windows.Forms.DataGridView dgvDetailStockInventory;
        private System.Windows.Forms.ComboBox cboRequestedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTerms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblMaturityDate;
        private System.Windows.Forms.DateTimePicker dtpDateNeeded;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn POQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyVariance;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotalPOQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalQtyIn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalQtyVariance;
    }
}