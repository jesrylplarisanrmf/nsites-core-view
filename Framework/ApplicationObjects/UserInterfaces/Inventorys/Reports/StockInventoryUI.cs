using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports.ReportsRpt;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports
{
    public partial class StockInventoryUI : Form
    {
        StockInventoryRpt loStockInventoryRpt;
        StockInventoryByGroupRpt loStockInventoryByGroupRpt;
        StockInventoryByLocationRpt loStockInventoryByLocationRpt;
        StockInventoryByLocationByGroupRpt loStockInventoryByLocationByGroupRpt;
        InventoryDetail loInventoryDetail;
        Location loLocation;
        ReportViewerUI loReportViewer;
        DataTable ldtAllStocks;
        DataTable ldtStocksByLocation;
        
        public StockInventoryUI()
        {
            InitializeComponent();
            loStockInventoryRpt = new StockInventoryRpt();
            loStockInventoryByGroupRpt = new StockInventoryByGroupRpt();
            loStockInventoryByLocationRpt = new StockInventoryByLocationRpt();
            loStockInventoryByLocationByGroupRpt = new StockInventoryByLocationByGroupRpt();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            loReportViewer = new ReportViewerUI();
            ldtAllStocks = new DataTable();
            ldtStocksByLocation = new DataTable();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void StockInventoryUI_Load(object sender, EventArgs e)
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockInventoryUI_Load");
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

        private void dgvListAllStocks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvListAllStocks.Columns[e.ColumnIndex].Name == "Stock Code")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvListAllStocks.Columns[e.ColumnIndex].Name == "Qty on Hand")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvListAllStocks_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreviewAllStocks_Click(object sender, EventArgs e)
        {
            try
            {
                loStockInventoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loStockInventoryRpt.Database.Tables[1].SetDataSource(ldtAllStocks);
                loStockInventoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loStockInventoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loStockInventoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loStockInventoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loStockInventoryRpt.SetParameterValue("Title", "Stock Inventory");
                loStockInventoryRpt.SetParameterValue("SubTitle", "Stock Inventory");
                loReportViewer.crystalReportViewer.ReportSource = loStockInventoryRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreviewAllStocks_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockInventory", "Refresh"))
                {
                    return;
                }
                try
                {
                    txtSearch.Clear();
                    dgvListAllStocks.DataSource = null;
                    ldtAllStocks = loInventoryDetail.getStockInventory("");
                    dgvListAllStocks.DataSource = ldtAllStocks;
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                try
                {
                    txtSearchByLocation.Clear();
                    dgvListByLocation.DataSource = null;
                    ldtStocksByLocation = loInventoryDetail.getStockInventoryByLocation(cboLocation.SelectedValue.ToString(), "");
                    dgvListByLocation.DataSource = ldtStocksByLocation;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListAllStocks.DataSource = null;
                ldtAllStocks = loInventoryDetail.getStockInventory(txtSearch.Text);
                dgvListAllStocks.DataSource = ldtAllStocks;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearch_TextChanged");
                em.ShowDialog();
                return;
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSearchByLocation.Clear();
                dgvListByLocation.DataSource = null;
                ldtStocksByLocation = loInventoryDetail.getStockInventoryByLocation(cboLocation.SelectedValue.ToString(), "");
                dgvListByLocation.DataSource = ldtStocksByLocation;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "cboLocation_SelectedIndexChanged");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearchByLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvListAllStocks.DataSource = null;
                ldtStocksByLocation = loInventoryDetail.getStockInventoryByLocation(cboLocation.SelectedValue.ToString(), txtSearchByLocation.Text);
                dgvListByLocation.DataSource = ldtStocksByLocation;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearchByLocation_TextChanged");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreviewByLocation_Click(object sender, EventArgs e)
        {
            try
            {
                loStockInventoryByLocationRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loStockInventoryByLocationRpt.Database.Tables[1].SetDataSource(ldtStocksByLocation);
                loStockInventoryByLocationRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loStockInventoryByLocationRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loStockInventoryByLocationRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loStockInventoryByLocationRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loStockInventoryByLocationRpt.SetParameterValue("Title", "Stock Inventory by Location");
                loStockInventoryByLocationRpt.SetParameterValue("SubTitle", "Stock Inventory by Location");
                loStockInventoryByLocationRpt.SetParameterValue("Location", cboLocation.Text);
                loReportViewer.crystalReportViewer.ReportSource = loStockInventoryByLocationRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreviewByLocation_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreviewAllStocksByGroup_Click(object sender, EventArgs e)
        {
            try
            {
                loStockInventoryByGroupRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loStockInventoryByGroupRpt.Database.Tables[1].SetDataSource(ldtAllStocks);
                loStockInventoryByGroupRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loStockInventoryByGroupRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loStockInventoryByGroupRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loStockInventoryByGroupRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loStockInventoryByGroupRpt.SetParameterValue("Title", "Stock Inventory");
                loStockInventoryByGroupRpt.SetParameterValue("SubTitle", "Stock Inventory");
                loReportViewer.crystalReportViewer.ReportSource = loStockInventoryByGroupRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreviewAllStocksByGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreviewByGroupByLocation_Click(object sender, EventArgs e)
        {
            try
            {
                loStockInventoryByLocationByGroupRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loStockInventoryByLocationByGroupRpt.Database.Tables[1].SetDataSource(ldtAllStocks);
                loStockInventoryByLocationByGroupRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loStockInventoryByLocationByGroupRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loStockInventoryByLocationByGroupRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loStockInventoryByLocationByGroupRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loStockInventoryByLocationByGroupRpt.SetParameterValue("Title", "Stock Inventory by Location");
                loStockInventoryByLocationByGroupRpt.SetParameterValue("SubTitle", "Stock Inventory by Location");
                loStockInventoryByLocationByGroupRpt.SetParameterValue("Location", cboLocation.Text);
                loReportViewer.crystalReportViewer.ReportSource = loStockInventoryByLocationByGroupRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreviewByGroupByLocation_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvListByLocation_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvListByLocation.Columns[e.ColumnIndex].Name == "Stock Code")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvListByLocation.Columns[e.ColumnIndex].Name == "Qty on Hand")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvListByLocation_CellFormatting");
                em.ShowDialog();
                return;
            }
        }
    }
}
