using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Inventorys;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class LookUpStockTransferOutUI : Form
    {
        Inventory loInventory;
        public string lId;
        public bool lFromSelection;

        public LookUpStockTransferOutUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            lFromSelection = false;
        }

        private void displayResult(string pSearchString)
        {
            try
            {
                dgvList.DataSource = null;
                dgvList.DataSource = loInventory.getStockTransferOut(GlobalVariables.CurrentLocationId,pSearchString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LookUpStockTransferOutUI_Load(object sender, EventArgs e)
        {
            try
            {
                displayResult("");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LookUpStockTransferOutUI_Load");
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
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "Reference" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "S.T. In Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty Out")
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
