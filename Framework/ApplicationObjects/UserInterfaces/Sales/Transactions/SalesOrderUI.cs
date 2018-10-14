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
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions
{
    public partial class SalesOrderUI : Form
    {
        SalesOrder loSalesOrder;
        SalesOrderDetail loSalesOrderDetail;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        Common loCommon;
        SearchesUI loSearches;
        SalesOrderRpt loSalesOrderRpt;
        SalesOrderDetailRpt loSalesOrderDetailRpt;
        System.Data.DataTable ldtSalesOrder;
        ReportViewerUI loReportViewer;

        public SalesOrderUI()
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            loSalesOrderDetail = new SalesOrderDetail();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCommon = new Common();
            ldtSalesOrder = new System.Data.DataTable();
            loSalesOrderRpt = new SalesOrderRpt();
            loSalesOrderDetailRpt = new SalesOrderDetailRpt();
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
                    ldtSalesOrder = loSalesOrder.getAllData("ViewAll", "", "");
                }
                catch
                {
                    ldtSalesOrder = null;
                }
                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtSalesOrder);

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
                dgvDetailList.DataSource = loSalesOrderDetail.getSalesOrderDetails("ViewAll",dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void insertJournalEntry(string pSOId,decimal pTotalAmount, string pCustomerId,string pCustomerName, string pExplanation)
        {
            try
            {
                loJournalEntry.JournalEntryId = "";
                loJournalEntry.FinancialYear = int.Parse(GlobalVariables.CurrentFinancialYear);
                loJournalEntry.Journal = "SJ";
                loJournalEntry.Form = "SV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = DateTime.Now;
                loJournalEntry.Explanation = pExplanation;
                loJournalEntry.TotalDebit = pTotalAmount;
                loJournalEntry.TotalCredit = pTotalAmount;
                loJournalEntry.Reference = "";
                loJournalEntry.SupplierId = "";
                loJournalEntry.CustomerId = pCustomerId;
                loJournalEntry.BegBal = "N";
                loJournalEntry.Adjustment = "N";
                loJournalEntry.ClosingEntry = "N";
                loJournalEntry.PreparedBy = GlobalVariables.Username;
                loJournalEntry.Remarks = "";
                loJournalEntry.SOId = pSOId;
                loJournalEntry.POId = "0";
                loJournalEntry.UserId = GlobalVariables.UserId;

                try
                {
                    string _JournalEntryId = loJournalEntry.save(GlobalVariables.Operation.Add);
                    if (_JournalEntryId != "")
                    {
                        //add debit
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.SODebitAccount;
                        loJournalEntryDetail.Debit = pTotalAmount;
                        loJournalEntryDetail.Credit = 0;
                        loJournalEntryDetail.Subsidiary = "Customer";
                        loJournalEntryDetail.SubsidiaryId = pCustomerId;
                        loJournalEntryDetail.SubsidiaryDescription = pCustomerName;
                        loJournalEntryDetail.Remarks = "";
                        loJournalEntryDetail.UserId = GlobalVariables.UserId;
                        loJournalEntryDetail.save(GlobalVariables.Operation.Add);

                        //add create
                        loJournalEntryDetail.DetailId = "";
                        loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                        loJournalEntryDetail.AccountId = GlobalVariables.SOCreditAccount;
                        loJournalEntryDetail.Debit = 0;
                        loJournalEntryDetail.Credit = pTotalAmount;
                        loJournalEntryDetail.Subsidiary = "";
                        loJournalEntryDetail.SubsidiaryId = "";
                        loJournalEntryDetail.SubsidiaryDescription = "";
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

        private void previewSalesOrderDetail(string pSalesOrderId)
        {
            try
            {
                foreach (DataRow _dr in loSalesOrder.getAllData("", pSalesOrderId, "").Rows)
                {
                    loSalesOrderDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loSalesOrderDetailRpt.Database.Tables[1].SetDataSource(loSalesOrderDetail.getSalesOrderDetails("ViewAll",pSalesOrderId));
                    loSalesOrderDetailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loSalesOrderDetailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loSalesOrderDetailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loSalesOrderDetailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loSalesOrderDetailRpt.SetParameterValue("Title", "Sales Order");
                    loSalesOrderDetailRpt.SetParameterValue("SubTitle", "Sales Order");
                    loSalesOrderDetailRpt.SetParameterValue("SOId", _dr["Id"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("Date", _dr["Date"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("PreparedBy", _dr["Prepared By"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("DueDate", _dr["Due Date"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("Customer", _dr["Customer"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("Instructions", _dr["Instructions"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("Terms", _dr["Terms"].ToString());
                    loSalesOrderDetailRpt.SetParameterValue("TotalQty", string.Format("{0:n}",decimal.Parse(_dr["Total S.O. Qty"].ToString())));
                    loSalesOrderDetailRpt.SetParameterValue("TotalAmount", string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString())));
                    loSalesOrderDetailRpt.SetParameterValue("FinalizedBy", _dr["Finalized By"].ToString());
                    loReportViewer.crystalReportViewer.ReportSource = loSalesOrderDetailRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SalesOrderUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(SalesOrder);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmSalesOrder");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "SalesOrderUI_Load");
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Refresh"))
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
                    this.dgvList.Columns[e.ColumnIndex].Name == "Reference" || this.dgvList.Columns[e.ColumnIndex].Name == "P.Q. Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total S.O. Qty" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Running Balance" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty Out" ||
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
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty Out" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Unit Price" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "S.O. Qty" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty Variance" || 
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Create"))
                {
                    return;
                }
                SalesOrderDetailUI loSalesOrderDetail = new SalesOrderDetailUI();
                loSalesOrderDetail.ParentList = this;
                loSalesOrderDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Update"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update a FINALIZED Sales Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    SalesOrderDetailUI loSalesOrderDetail = new SalesOrderDetailUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loSalesOrderDetail.ParentList = this;
                    loSalesOrderDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Remove"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove a FINALIZED Sales Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Sales Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loSalesOrder.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Sales Order record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Finalize"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Sales Order is already FINALIZED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot FINALIZE a Sales Order you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                    /*
                    if (_drStatus["SWId"].ToString() == "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stocks Withdrawal must precedes finalization!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue finalizing this Sales Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loSalesOrder.finalize(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            /*
                            foreach (DataRow _drSO in loSalesOrder.getAllData("", dgvList.CurrentRow.Cells[0].Value.ToString(), "").Rows)
                            {
                                insertJournalEntry(dgvList.CurrentRow.Cells[0].Value.ToString(), decimal.Parse(_drSO["Total Amount"].ToString()),
                                    _drSO["CustomerId"].ToString(), _drSO["Customer"].ToString(), "Sales from Sales Order (SOId:" + _drSO["Id"].ToString() + ").");
                            }
                            */
                            MessageBoxUI _mb1 = new MessageBoxUI("Sales Order record has been successfully finalized!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewSalesOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
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
            try
            {
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Cancel"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Sales Order must be FINALIZED to be cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["Cancel"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Sales Order is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["Post"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel a POSTED Sales Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Sales Order you preprared/finalized!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Sales Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        SalesCancelReasonUI loSalesCancelReason = new SalesCancelReasonUI();
                        loSalesCancelReason.ShowDialog();
                        if (loSalesCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loSalesOrder.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loSalesCancelReason.lReason))
                            {
                                foreach (DataRow _drSO in loJournalEntry.getJournalEntryBySOId(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                                {
                                    loJournalEntry.cancel(_drSO[0].ToString(), loSalesCancelReason.lReason);
                                }

                                MessageBoxUI _mb1 = new MessageBoxUI("Sales Order record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Search"))
                {
                    return;
                }

                string _DisplayFields = "SELECT so.Id AS Id,DATE_FORMAT(so.`Date`,'%m-%d-%Y') AS `Date`, "+
				"so.Final,so.Cancel,so.Post,so.PQId AS `P.Q. Id`, "+
				"so.Reference,c.Name AS Customer,so.Terms, "+
				"DATE_FORMAT(so.`DueDate`,'%m-%d-%Y') AS `Due Date`, "+
				"so.Instructions,sp.Name AS `Sales Person`, "+
				"so.TotalSOQty AS `Total S.O. Qty`,so.TotalQtyOut AS `Total Qty Out`, "+
				"so.TotalQtyVariance AS `Total Qty Variance`, "+
				"so.TotalAmount AS `Total Amount`,so.RunningBalance AS `Running Balance`, "+
				"preby.Username AS `Prepared By`,finby.Username AS `Finalized By`, "+
				"DATE_FORMAT(so.DateFinalized,'%m-%d-%Y') AS `Date Finalized`, "+
				"canby.Username AS `Cancelled By`,so.CancelledReason AS `Cancelled Reason`, "+
                "DATE_FORMAT(so.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`, " +
				"posby.Username AS `Posted By`,DATE_FORMAT(so.DatePosted,'%m-%d-%Y') AS `Date Posted`, "+
				"so.Remarks "+
				"FROM salesorder so "+
				"LEFT JOIN customer c "+
				"ON so.CustomerId = c.Id "+
				"LEFT JOIN salesperson sp "+
				"ON so.SalesPersonId = sp.Id "+
				"LEFT JOIN `user` preby "+
				"ON so.PreparedBy = preby.Id "+
				"LEFT JOIN `user` finby "+
				"ON so.FinalizedBy = finby.Id "+
				"LEFT JOIN `user` canby "+
				"ON so.CancelledBy = canby.Id "+
				"LEFT JOIN `user` posby "+
				"ON so.PostedBy = posby.Id ";
                string _WhereFields = " AND so.`Status` = 'Active' ORDER BY so.Id DESC;";
                loSearches.lAlias = "so.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtSalesOrder = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtSalesOrder);

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
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loSalesOrderRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loSalesOrderRpt.Database.Tables[1].SetDataSource(ldtSalesOrder);
                    loSalesOrderRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loSalesOrderRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loSalesOrderRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loSalesOrderRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loSalesOrderRpt.SetParameterValue("Title", "Sales Order");
                    loSalesOrderRpt.SetParameterValue("SubTitle", "Sales Order");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loSalesOrderRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loSalesOrderRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loSalesOrderRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loSalesOrderRpt;
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
                GlobalFunctions.refreshAll(ref dgvList, ldtSalesOrder);
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
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

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Post"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Final"].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Sales Order must be FINALIZED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus["Cancel"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot post a CANCELLED Sales Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus["Post"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Sales Order is already POSTED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (decimal.Parse(_drStatus["TotalQtyOut"].ToString()) <= 0)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Total Qty OUT must be greater than or equal to zero(0)!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot FINALIZE a Sales Order you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                    /*
                    if (_drStatus["SWId"].ToString() == "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Stocks Withdrawal must precedes finalization!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue posting this Sales Order record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loSalesOrder.post(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            foreach (DataRow _drSO in loSalesOrder.getAllData("", dgvList.CurrentRow.Cells[0].Value.ToString(), "").Rows)
                            {
                                insertJournalEntry(dgvList.CurrentRow.Cells[0].Value.ToString(), decimal.Parse(_drSO["Total Amount"].ToString()),
                                    _drSO["CustomerId"].ToString(), _drSO["Customer"].ToString(), "Sales from Sales Order (SOId:" + _drSO["Id"].ToString() + ").");
                            }
           
                            MessageBoxUI _mb1 = new MessageBoxUI("Sales Order record has been successfully posted!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            //previewSalesOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
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

        private void tsmiPreviewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmSalesOrder", "Preview Details"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loSalesOrder.getSalesOrderStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only APPROVED Sales Order can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Sales Order!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewSalesOrderDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiPreviewBilling_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
