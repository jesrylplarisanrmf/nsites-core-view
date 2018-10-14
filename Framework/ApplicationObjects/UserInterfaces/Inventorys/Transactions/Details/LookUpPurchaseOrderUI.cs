using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Procurements;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class LookUpPurchaseOrderUI : Form
    {
        PurchaseOrder loPurchaseOrder;
        public string lId;
        public string lSupplierId;
        public bool lFromSelection;

        public LookUpPurchaseOrderUI()
        {
            InitializeComponent();
            loPurchaseOrder = new PurchaseOrder();
            lFromSelection = false;
        }

        private void displayResult(string pSearchString)
        {
            try
            {
                dgvList.DataSource = null;
                dgvList.DataSource = loPurchaseOrder.getPurchaseOrderBySupplier(lSupplierId, pSearchString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LookUpPurchaseOrderUI_Load(object sender, EventArgs e)
        {
            try
            {
                displayResult("");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LookUpPurchaseOrderUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvList.Rows.Count > 0)
            {
                lId = dgvList.CurrentRow.Cells[0].Value.ToString();
                lFromSelection = true;
            }
            else
            {
                lId = "";
                lFromSelection = false;
            }
            this.Close();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Reference" || this.dgvList.Columns[e.ColumnIndex].Name == "P.R. Id" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "S.R. Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Running Balance")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Final")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (e.Value.ToString() == "N")
                        {
                            e.CellStyle.BackColor = Color.Green;
                        }
                    }
                }
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Cancel")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (e.Value.ToString() == "Y")
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                displayResult(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearch_TextChanged");
                em.ShowDialog();
                return;
            }
        }
    }
}
