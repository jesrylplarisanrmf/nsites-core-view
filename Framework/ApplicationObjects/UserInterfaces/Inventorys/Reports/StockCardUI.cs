using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports.ReportsRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports
{
    public partial class StockCardUI : Form
    {
        #region "VARIABLES"
        Stock loStock;
        InventoryDetail loInventoryDetail;
        Location loLocation;
        DataTable ldtStock;
        DataTable ldtList;
        StockCardRpt loStockCardRpt;
        ReportViewerUI loReportViewer;
        #endregion "END OF VARIABLES"

        public StockCardUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            ldtStock = new DataTable();
            ldtList = new DataTable();
            loStockCardRpt = new StockCardRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void StockCardUI_Load(object sender, EventArgs e)
        {
            try
            {
                cboLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                cboLocation.DisplayMember = "Description";
                cboLocation.ValueMember = "Id";
                cboLocation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockCardUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockCard", "Refresh"))
                {
                    return;
                }

                try
                {
                    ldtStock = loInventoryDetail.getStockInventoryList(cboLocation.SelectedValue.ToString(), "");
                }
                catch { }
                GlobalFunctions.refreshGrid(ref dgvStockList, ldtStock);
                getDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvStockList.Columns[e.ColumnIndex].Name == "Stock Code" || this.dgvStockList.Columns[e.ColumnIndex].Name == "Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvStockList.Columns[e.ColumnIndex].Name == "Qty on Hand")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvStockList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvStockList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                getDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvStockList_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void getDetails()
        {
            try
            {
                dgvList.DataSource = null;
                ldtList = loStock.getStockCard(dtpFromDate.Value, dtpToDate.Value, dgvStockList.CurrentRow.Cells[0].Value.ToString(), cboLocation.SelectedValue.ToString());
                dgvList.DataSource = ldtList;
            }
            catch { }
        }

        private void txtSearchStock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvList.DataSource = null;
                ldtList = null;
                ldtStock = loInventoryDetail.getStockInventoryList(cboLocation.SelectedValue.ToString(), txtSearchStock.Text);
                GlobalFunctions.refreshGrid(ref dgvStockList, ldtStock);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearchStock_TextChanged");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockCard", "Preview"))
                {
                    return;
                }

                decimal _qtyTotalIn = 0;
                decimal _qtyTotalOut = 0;
                decimal _qtyBegBal = 0;

                foreach (DataRow _dr in loStock.getStockCardBegBal(dtpFromDate.Value, dgvStockList.CurrentRow.Cells[0].Value.ToString(), cboLocation.SelectedValue.ToString()).Rows)
                {
                    _qtyTotalIn += decimal.Parse(_dr[0].ToString());
                    _qtyTotalOut += decimal.Parse(_dr[1].ToString());
                    _qtyBegBal += decimal.Parse(_dr[2].ToString());
                }

                loStockCardRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loStockCardRpt.Database.Tables[1].SetDataSource(ldtList);
                loStockCardRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loStockCardRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loStockCardRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loStockCardRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loStockCardRpt.SetParameterValue("Title", "Stock Card");
                loStockCardRpt.SetParameterValue("SubTitle", "Stock Card");
                loStockCardRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                loStockCardRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                loStockCardRpt.SetParameterValue("Stock", dgvStockList.CurrentRow.Cells[1].Value.ToString());
                loStockCardRpt.SetParameterValue("Location", cboLocation.Text);
                loStockCardRpt.SetParameterValue("QtyTotalIn", string.Format("{0:n}", _qtyTotalIn));
                loStockCardRpt.SetParameterValue("QtyTotalOut", string.Format("{0:n}", _qtyTotalOut));
                loStockCardRpt.SetParameterValue("QtyBegBal", string.Format("{0:n}", _qtyBegBal));
                loReportViewer.crystalReportViewer.ReportSource = loStockCardRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Qty In" || this.dgvList.Columns[e.ColumnIndex].Name == "P.O. Qty" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Balance" || this.dgvList.Columns[e.ColumnIndex].Name == "S.O. Qty" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Qty Out" || this.dgvList.Columns[e.ColumnIndex].Name == "Variance")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }
    }
}
