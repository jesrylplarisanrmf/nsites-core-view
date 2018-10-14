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
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions
{
    public partial class PurchaseOrderUI : Form
    {
        PurchaseOrder loPurchaseOrder;
        PurchaseOrderDetail loPurchaseOrderDetail;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        Common loCommon;
        SearchesUI loSearches;
        PurchaseOrderRpt loPurchaseOrderRpt;
        PurchaseOrderDetailRpt loPurchaseOrderDetailRpt;
        System.Data.DataTable ldtPurchaseOrder;
        ReportViewerUI loReportViewer;

        public PurchaseOrderUI()
        {
            InitializeComponent();
            loPurchaseOrder = new PurchaseOrder();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCommon = new Common();
            ldtPurchaseOrder = new System.Data.DataTable();
            loPurchaseOrderRpt = new PurchaseOrderRpt();
            loPurchaseOrderDetailRpt = new PurchaseOrderDetailRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void refresh()
        {
            try
            {
                try
                {
                    ldtPurchaseOrder = loPurchaseOrder.getAllData("ViewAll", "", "");
                }
                catch
                {
                    ldtPurchaseOrder = null;
                }

                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtPurchaseOrder);

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
                dgvDetailList.DataSource = loPurchaseOrderDetail.getPurchaseOrderDetails("ViewAll",dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void insertJournalEntry(string pPOId, decimal pTotalAmount, string pSupplierId, string pSupplierName, string pExplanation)
        {
            try
            {
                loJournalEntry.JournalEntryId = "";
                loJournalEntry.FinancialYear = int.Parse(GlobalVariables.CurrentFinancialYear);
                loJournalEntry.Journal = "PJ";
                loJournalEntry.Form = "PV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = DateTime.Now;
                loJournalEntry.Explanation = pExplanation;
                loJournalEntry.TotalDebit = pTotalAmount;
                loJournalEntry.TotalCredit = pTotalAmount;
                loJournalEntry.Reference = "";
                loJournalEntry.SupplierId = pSupplierId;
                loJournalEntry.CustomerId = "";
                loJournalEntry.BegBal = "N";
                loJournalEntry.Adjustment = "N";
                loJournalEntry.ClosingEntry = "N";
                loJournalEntry.PreparedBy = GlobalVariables.Username;
                loJournalEntry.Remarks = "";
                loJournalEntry.SOId = "0";
                loJournalEntry.POId = pPOId;
                loJournalEntry.UserId = GlobalVariables.UserId;

                try
                {
                    string _JournalEntryId = loJournalEntry.save(GlobalVariables.Operation.Add);
                    if (_JournalEntryId != "")
                    {
                        //add debit
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.PODebitAccount;
                        loJournalEntryDetail.Debit = pTotalAmount;
                        loJournalEntryDetail.Credit = 0;
                        loJournalEntryDetail.Subsidiary = "";
                        loJournalEntryDetail.SubsidiaryId = "";
                        loJournalEntryDetail.SubsidiaryDescription = "";
                        loJournalEntryDetail.Remarks = "";
                        loJournalEntryDetail.UserId = GlobalVariables.UserId;
                        loJournalEntryDetail.save(GlobalVariables.Operation.Add);

                        //add create
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.POCreditAccount;
                        loJournalEntryDetail.Debit = 0;
                        loJournalEntryDetail.Credit = pTotalAmount;
                        loJournalEntryDetail.Subsidiary = "Supplier";
                        loJournalEntryDetail.SubsidiaryId = pSupplierId;
                        loJournalEntryDetail.SubsidiaryDescription = pSupplierName;
                        loJournalEntryDetail.Remarks = "";
                        loJournalEntryDetail.UserId = GlobalVariables.UserId;
                        loJournalEntryDetail.save(GlobalVariables.Operation.Add);

                        //post Journal Entry
                        loJournalEntry.post(_JournalEntryId);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void previewPurchaseOrderDetail(string pPurchaseOrderId)
        {
            try
            {
                foreach (DataRow _dr in loPurchaseOrder.getAllData("", pPurchaseOrderId, "").Rows)
                {
                    //loPurchaseOrderDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPurchaseOrderDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPurchaseOrderDetailRpt.Database.Tables[1].SetDataSource(loPurchaseOrderDetail.getPurchaseOrderDetails("ViewAll",pPurchaseOrderId));
                    loPurchaseOrderDetailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPurchaseOrderDetailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPurchaseOrderDetailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPurchaseOrderDetailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPurchaseOrderDetailRpt.SetParameterValue("Title", "Purchase Order");
                    loPurchaseOrderDetailRpt.SetParameterValue("SubTitle", "Purchase Order");
                    loPurchaseOrderDetailRpt.SetParameterValue("POId", _dr["Id"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("Date", _dr["Date"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("PreparedBy", _dr["Prepared By"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("DueDate", _dr["Due Date"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("Supplier", _dr["Supplier"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("Instructions", _dr["Instructions"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("Terms", _dr["Terms"].ToString());
                    loPurchaseOrderDetailRpt.SetParameterValue("TotalQty", string.Format("{0:n}",decimal.Parse(_dr["Total P.O. Qty"].ToString())));
                    loPurchaseOrderDetailRpt.SetParameterValue("TotalAmount", string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString())));
                    loPurchaseOrderDetailRpt.SetParameterValue("FinalizedBy", _dr["Finalized By"].ToString());
                    loReportViewer.crystalReportViewer.ReportSource = loPurchaseOrderDetailRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PurchaseOrderUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(PurchaseOrder);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmPurchaseOrder");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PurchaseOrderUI_Load");
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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Refresh"))
                {
                    return;
                }
                refresh();
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
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Reference" || this.dgvList.Columns[e.ColumnIndex].Name == "P.R. Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total P.O. Qty" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Running Balance" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty In" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty Variance")
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
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Post")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (e.Value.ToString() == "N")
                        {
                            e.CellStyle.BackColor = Color.Blue;
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
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty In" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Unit Price" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "P.O. Qty" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty Variance" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Discount Amount" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Price")
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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Create"))
                {
                    return;
                }
                PurchaseOrderDetailUI loPurchaseOrderDetail = new PurchaseOrderDetailUI();
                loPurchaseOrderDetail.ParentList = this;
                loPurchaseOrderDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Update"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update a FINALIZED Purchase Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    PurchaseOrderDetailUI loPurchaseOrderDetail = new PurchaseOrderDetailUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loPurchaseOrderDetail.ParentList = this;
                    loPurchaseOrderDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUpdate_Click");
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Remove"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove a FINALIZED Purchase Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Purchase Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPurchaseOrder.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Purchase Order record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            refresh();
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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Finalize"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Order is already FINALIZED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot FINALIZE a Purchase Order you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                    /*
                    if (_drStatus["SRId"].ToString() == "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stocks Receiving must precedes finalization!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue finalizing this Purchase Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPurchaseOrder.finalize(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Purchase Order record has been successfully finalized!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewPurchaseOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                            //sendEmailToCreator();
                            refresh();
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
            if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Cancel"))
            {
                return;
            }
            try
            {
                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Order must be FINALIZED to be cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["Cancel"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Order is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["Post"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel a POSTED Purchase Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Purchase Order you preprared/finalized!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Purchase Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        ProcurementCancelReasonUI loProcurementCancelReason = new ProcurementCancelReasonUI();
                        loProcurementCancelReason.ShowDialog();
                        if (loProcurementCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loPurchaseOrder.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loProcurementCancelReason.lReason))
                            {
                                foreach (DataRow _drPO in loJournalEntry.getJournalEntryByPOId(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                                {
                                    loJournalEntry.cancel(_drPO[0].ToString(), loProcurementCancelReason.lReason);
                                }

                                MessageBoxUI _mb1 = new MessageBoxUI("Purchase Order record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Search"))
                {
                    return;
                }

                string _DisplayFields = "SELECT po.Id AS Id,DATE_FORMAT(po.`Date`,'%m-%d-%Y') AS `Date`, "+
		            "po.Final,po.Cancel,po.Post,po.PRId AS `P.R. Id`, "+
		            "po.Reference,s.Name AS Supplier,po.Terms, "+
		            "DATE_FORMAT(po.DueDate,'%m-%d-%Y') AS `Due Date`,po.Instructions, "+
		            "CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ', e.Lastname) AS `Requested By`, "+
		            "DATE_FORMAT(po.DateNeeded,'%m-%d-%Y') AS `Date Needed`, "+
		            "po.TotalPOQty AS `Total P.O. Qty`,po.TotalQtyIn AS `Total Qty In`,po.TotalQtyVariance AS `Total Qty Variance`, "+
		            "po.TotalAmount AS `Total Amount`,po.RunningBalance AS `Running Balance`, "+
		            "preby.Username AS `Prepared By`,finby.Username AS `Finalized By`, "+
		            "DATE_FORMAT(po.DateFinalized,'%m-%d-%Y') AS `Date Finalized`, "+
		            "canby.Username AS `Cancelled By`,po.CancelledReason AS `Cancelled Reason`, "+
		            "DATE_FORMAT(po.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`,posby.Username AS `PostedBy`, "+
		            "DATE_FORMAT(po.DatePosted,'%m-%d-%Y') AS `Date Posted`,po.Remarks "+
		            "FROM purchaseorder po "+
		            "LEFT JOIN supplier s "+
		            "ON po.SupplierId = s.Id "+
		            "LEFT JOIN employee e "+
		            "ON po.RequestedBy = e.Id "+
		            "LEFT JOIN `user` preby "+
		            "ON po.PreparedBy = preby.Id "+
		            "LEFT JOIN `user` finby "+
		            "ON po.FinalizedBy = finby.Id "+
		            "LEFT JOIN `user` canby "+
		            "ON po.CancelledBy = canby.Id "+
                    "LEFT JOIN `user` posby " +
		            "ON po.PostedBy = posby.Id ";
                string _WhereFields = " AND po.`Status` = 'Active' ORDER BY po.Id DESC;";
                loSearches.lAlias = "po.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtPurchaseOrder = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtPurchaseOrder);

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
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loPurchaseOrderRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPurchaseOrderRpt.Database.Tables[1].SetDataSource(ldtPurchaseOrder);
                    loPurchaseOrderRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPurchaseOrderRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPurchaseOrderRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPurchaseOrderRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPurchaseOrderRpt.SetParameterValue("Title", "Purchase Order");
                    loPurchaseOrderRpt.SetParameterValue("SubTitle", "Purchase Order");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loPurchaseOrderRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loPurchaseOrderRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loPurchaseOrderRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loPurchaseOrderRpt;
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvList, ldtPurchaseOrder);
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

        private void tsmiPreviewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Preview Detail"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only FINALIZED Purchase Order can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Purchase Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewPurchaseOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
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

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseOrder", "Post"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPurchaseOrder.getPurchaseOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Order must be FINALIZED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus["Cancel"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot post a CANCELLED Purchase Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus["Post"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Order is already POSTED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (decimal.Parse(_drStatus["TotalQtyIn"].ToString()) <= 0)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Total Qty IN must be greater than or equal to zero(0)!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot FINALIZE a Purchase Order you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                    /*
                    if (_drStatus["SRId"].ToString() == "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stocks Receiving must precedes finalization!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue posting this Purchase Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPurchaseOrder.post(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            foreach (DataRow _drPO in loPurchaseOrder.getAllData("", dgvList.CurrentRow.Cells[0].Value.ToString(), "").Rows)
                            {
                                insertJournalEntry(dgvList.CurrentRow.Cells[0].Value.ToString(), decimal.Parse(_drPO["Total Amount"].ToString()),
                                    _drPO["SupplierId"].ToString(), _drPO["Supplier"].ToString(), "Purchases from Purchase Order (POId:" + _drPO["Id"].ToString() + ").");
                            }
                            MessageBoxUI _mb1 = new MessageBoxUI("Purchase Order record has been successfully posted!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            //previewPurchaseOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                            //sendEmailToCreator();
                            refresh();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPost_Click");
                em.ShowDialog();
                return;
            }
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
