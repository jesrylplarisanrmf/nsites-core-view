using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports.TransactionsRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions
{
    public partial class StockTransferInUI : Form
    {
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        SalesOrderDetail loSalesOrderDetail;
        Common loCommon;
        SearchesUI loSearches;
        StockTransferInRpt loStockTransferInRpt;
        StockTransferInDetailRpt loStockTransferInDetailRpt;
        System.Data.DataTable ldtInventory;
        
        ReportViewerUI loReportViewer;

        public StockTransferInUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loSalesOrderDetail = new SalesOrderDetail();
            loCommon = new Common();
            ldtInventory = new System.Data.DataTable();
            loStockTransferInRpt = new StockTransferInRpt();
            loStockTransferInDetailRpt = new StockTransferInDetailRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void refresh(string pType)
        {
            try
            {
                try
                {
                    ldtInventory = loInventory.getAllData(pType, "ViewAll", "", "");
                }
                catch
                {
                    ldtInventory = null;
                }
                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtInventory);

                viewDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void viewDetails()
        {
            try
            {
                dgvDetailList.DataSource = loInventoryDetail.getInventoryDetails("ViewAll", dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void previewInventoryDetail(string pInventoryId)
        {
            try
            {
                foreach (DataRow _dr in loInventory.getAllData("Stock Transfer In", "", pInventoryId, "").Rows)
                {
                    loStockTransferInDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loStockTransferInDetailRpt.Database.Tables[1].SetDataSource(loInventoryDetail.getInventoryDetails("ViewAll", pInventoryId));
                    loStockTransferInDetailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loStockTransferInDetailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loStockTransferInDetailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loStockTransferInDetailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loStockTransferInDetailRpt.SetParameterValue("Title", "Stock Transfer In");
                    loStockTransferInDetailRpt.SetParameterValue("SubTitle", "Stock Transfer In");
                    loStockTransferInDetailRpt.SetParameterValue("STId", _dr["Id"].ToString());
                    loStockTransferInDetailRpt.SetParameterValue("Date", _dr["Date"].ToString());
                    loStockTransferInDetailRpt.SetParameterValue("PreparedBy", _dr["Prepared By"].ToString());
                    loStockTransferInDetailRpt.SetParameterValue("STOutId", _dr["S.T. Out Id"].ToString());
                    loStockTransferInDetailRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loStockTransferInDetailRpt.SetParameterValue("FinalizedBy", _dr["Finalized By"].ToString());
                    loReportViewer.crystalReportViewer.ReportSource = loStockTransferInDetailRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StockTransferInUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(Inventory);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmStockTransferIn");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockTransferInUI_Load");
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
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Refresh"))
                {
                    return;
                }
                refresh("Stock Transfer In");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "Reference" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "S.T. Out Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty In")
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

        private void dgvDetailList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Stock Code" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Unit")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty In")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvDetailList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Create"))
                {
                    return;
                }
                StockTransferInDetailUI loStockTransferInDetail = new StockTransferInDetailUI();
                loStockTransferInDetail.ParentList = this;
                loStockTransferInDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCreate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Update"))
                {
                    return;
                }
                foreach (DataRow _drStatus in loInventory.getInventoryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update a FINALIZED Stock Inventory!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    StockTransferInDetailUI loStockTransferInDetail = new StockTransferInDetailUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loStockTransferInDetail.ParentList = this;
                    loStockTransferInDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Remove"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loInventory.getInventoryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove a FINALIZED Stock Inventory!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Stock Inventory record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loInventory.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Stock Inventory record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            refresh("Stock Transfer In");
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRemove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Finalize"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loInventory.getInventoryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stock Inventory is already FINALIZED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot FINALIZE a Stock Inventory you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue finalizing this Stock Inventory record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loInventory.final(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Stock Inventory record has been successfully finalized!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewInventoryDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                            //sendEmailToCreator();
                            refresh("Stock Transfer In");
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFinalize_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Cancel"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loInventory.getInventoryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stock Inventory must be finalized to be cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stock Inventory is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Stock Inventory you preprared/finalized!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Stock Inventory record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        InventoryCancelReasonUI loInventoryCancelReason = new InventoryCancelReasonUI();
                        loInventoryCancelReason.ShowDialog();
                        if (loInventoryCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loInventory.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loInventoryCancelReason.lReason))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Stock Inventory record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh("Stock Transfer In");
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Search"))
                {
                    return;
                }

                string _DisplayFields = "SELECT i.Id AS Id,DATE_FORMAT(i.`Date`,'%m-%d-%Y') AS `Date`, "+
					"i.Final,i.Cancel,i.STOutId AS `S.T. Out Id`, "+
					"i.Reference,i.Reference,lf.`Description` AS `Location From`,lt.`Description` AS `Location To`, "+
					"i.TotalQtyIn AS `Total Qty In`, "+
					"preby.Username AS `Prepared By`,finby.Username AS `Finalized By`, "+
					"DATE_FORMAT(i.DateFinalized,'%m-%d-%Y') AS `Date Finalized`, "+
					"canby.Username AS `Cancelled By`,i.CancelledReason AS `Cancelled Reason`, "+
					"DATE_FORMAT(i.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`,i.Remarks "+
					"FROM inventory i "+
					"LEFT JOIN location lf "+
					"ON i.FromLocationId = lf.Id "+
					"LEFT JOIN location lt "+
					"ON i.ToLocationId = lt.Id "+
					"LEFT JOIN `user` preby "+
					"ON i.PreparedBy = preby.Id "+
					"LEFT JOIN `user` finby "+
					"ON i.FinalizedBy = finby.Id "+
                    "LEFT JOIN `user` canby " +
					"ON i.CancelledBy = canby.Id ";
                string _WhereFields = " AND i.`Status` = 'Active' AND i.`Type` = 'Stock Transfer In' ORDER BY i.Id DESC;";
                loSearches.lAlias = "i.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtInventory = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtInventory);

                    viewDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSearch_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loStockTransferInRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loStockTransferInRpt.Database.Tables[1].SetDataSource(ldtInventory);
                    loStockTransferInRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loStockTransferInRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loStockTransferInRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loStockTransferInRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loStockTransferInRpt.SetParameterValue("Title", "Stock Transfer In");
                    loStockTransferInRpt.SetParameterValue("SubTitle", "Stock Transfer In");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loStockTransferInRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loStockTransferInRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loStockTransferInRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loStockTransferInRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void dgvList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    System.Drawing.Point pt = dgvList.PointToScreen(e.Location);
                    cmsFunction.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellDoubleClick");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiPreviewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStockTransferIn", "Preview Detail"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loInventory.getInventoryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only FINALIZED Inventory can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Inventory!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewInventoryDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiPreviewDetail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvList, ldtInventory);
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void tsmiCreate_Click(object sender, EventArgs e)
        {
            btnCreate_Click(null, new EventArgs());
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tsmiFinalize_Click(object sender, EventArgs e)
        {
            btnFinalize_Click(null, new EventArgs());
        }

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, new EventArgs());
        }

        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tsmiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }
    }
}
