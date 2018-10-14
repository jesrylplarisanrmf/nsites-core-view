using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
//using Microsoft.Office.Interop.Excel;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    public partial class CashDisbursementJournalUI : Form
    {
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        CashDisbursementDetail loCashDisbursementDetail;
        CheckDetail loCheckDetail;
        PurchaseOrder loPurchaseOrder;
        Common loCommon;
        SearchesUI loSearches;
        System.Data.DataTable ldtJournalEntry;
        CashDisbursementJournalRpt loCashDisbursementJournalRpt;
        CashDisbursementVoucherRpt loCashDisbursementVoucherRpt;
        PrintCheckRpt loPrintCheckRpt;
        ReportViewerUI loReportViewer;

        public CashDisbursementJournalUI()
        {
            InitializeComponent();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashDisbursementDetail = new CashDisbursementDetail();
            loCheckDetail = new CheckDetail();
            loPurchaseOrder = new PurchaseOrder();
            loCommon = new Common();
            ldtJournalEntry = new System.Data.DataTable();
            loCashDisbursementJournalRpt = new CashDisbursementJournalRpt();
            loCashDisbursementVoucherRpt = new CashDisbursementVoucherRpt();
            loPrintCheckRpt = new PrintCheckRpt();
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
                ldtJournalEntry = loJournalEntry.getAllData("CDJ", "ViewAll", "", "");
                GlobalFunctions.refreshGrid(ref dgvList, ldtJournalEntry);
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
                try
                {
                    dgvDetailList.DataSource = loJournalEntryDetail.getJournalEntryDetails("ViewAll", dgvList.CurrentRow.Cells[0].Value.ToString());
                }
                catch
                {
                    dgvDetailList.DataSource = null;
                }
                try
                {
                    dgvDetailDisbursement.DataSource = loCashDisbursementDetail.getCashDisbursementDetails(dgvList.CurrentRow.Cells[0].Value.ToString());
                }
                catch
                {
                    dgvDetailDisbursement.DataSource = null;
                }
                try
                {
                    dgvCheckDetail.DataSource = loCheckDetail.getCheckDetails(dgvList.CurrentRow.Cells[0].Value.ToString());
                }
                catch
                {
                    dgvCheckDetail.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvList.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvList.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvList.CurrentRow.Selected = false;
                dgvList.FirstDisplayedScrollingRowIndex = dgvList.Rows[n].Index;
                dgvList.Rows[n].Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateData(string[] pRecordData)
        {
            try
            {
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvList.CurrentRow.Cells[i].Value = pRecordData[i];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void sendEmailToCreator()
        {
            try
            {
                string _form = dgvList.CurrentRow.Cells[5].Value.ToString();
                string _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Check/Cash Voucher</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[15].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";

                string _bodyDetailHeader = "<tr>" +
                                                "<th>Account Code</th>" +//1
                                                "<th>Account Title</th>" +//2
                                                "<th>Debit</th>" +//3
                                                "<th>Credit</th>" +//4
                                                "<th>Subsidiary</th>" +//5
                                                "<th>Description</th>" +//6
                                                "<th>Remarks</th>" +//7
                                           "</tr>";
                string _bodycontent = "";
                foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails("ViewAll",dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    _bodycontent += "<tr>" +
                                        "<td>" + _drbody[1].ToString() + "</td>" +
                                        "<td>" + _drbody[2].ToString() + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[3].ToString())) + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[4].ToString())) + "</td>" +
                                        "<td align=\"center\">" + _drbody[5].ToString() + "</td>" +
                                        "<td>" + _drbody[6].ToString() + "</td>" +
                                        "<td align=\"center\">" + _drbody[7].ToString() + "</td>" +
                                    "</tr>";
                }

                try
                {
                    /*
                    //send to email address
                    foreach (DataRow _dr1 in loJournalEntry.getPreparedByEmailAddress(dgvList.CurrentRow.Cells["Prepared By"].Value.ToString()).Rows)
                    {
                        if (_dr1[0].ToString() != "")
                        {
                            GlobalFunctions.SendEmail(_dr1[0].ToString(), "", "Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + " is already posted.", _bodyhead + _bodyDetailHeader + _bodycontent);
                        }
                    }*/
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

        private void previewDetail(string pJournalEntryId)
        {
            try
            {
                foreach (DataRow _dr in loJournalEntry.getAllData("", "", pJournalEntryId, "").Rows)
                {
                    loCashDisbursementVoucherRpt.Subreports["SubCashDisbursementJERpt.rpt"].SetDataSource(loJournalEntryDetail.getJournalEntryDetails("ViewAll", pJournalEntryId));
                    loCashDisbursementVoucherRpt.Subreports["SubCashDisbursementRpt.rpt"].SetDataSource(loCashDisbursementDetail.getCashDisbursementDetails(pJournalEntryId));
                    loCashDisbursementVoucherRpt.Subreports["SubCashDisbursementCheckRpt.rpt"].SetDataSource(loCheckDetail.getCheckDetails(pJournalEntryId));
                    loCashDisbursementVoucherRpt.Database.Tables[0].SetDataSource(GlobalVariables.DTCompanyLogo);
                    loCashDisbursementVoucherRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loCashDisbursementVoucherRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loCashDisbursementVoucherRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loCashDisbursementVoucherRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loCashDisbursementVoucherRpt.SetParameterValue("Title", "Cash Disbursement Voucher");
                    loCashDisbursementVoucherRpt.SetParameterValue("SubTitle", "Cash Disbursement Voucher");
                    loCashDisbursementVoucherRpt.SetParameterValue("JEId", _dr["Id"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("FY", _dr["F.Y."].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("VoucherNo", _dr["Voucher No."].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("DatePrepared", _dr["Date Prepared"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("Supplier", _dr["Supplier"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("Explanation", _dr["Explanation"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("PreparedBy", _dr["Prepared By"].ToString());
                    loCashDisbursementVoucherRpt.SetParameterValue("PostedBy", _dr["Posted By"].ToString());
                    loReportViewer.crystalReportViewer.ReportSource = loCashDisbursementVoucherRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Refresh"))
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Create"))
                {
                    return;
                }
                CashDisbursementVoucherUI loCheckVoucher = new CashDisbursementVoucherUI();
                loCheckVoucher.ParentList = this;
                loCheckVoucher.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Update"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update a POSTED Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    CashDisbursementVoucherUI loCheckVoucher = new CashDisbursementVoucherUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loCheckVoucher.ParentList = this;
                    loCheckVoucher.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Remove"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove a POSTED Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Journal Entry record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        try
                        {
                            if (loJournalEntry.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Journal Entry record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            mb.ShowDialog();
                            return;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Search"))
                {
                    return;
                }
                string _DisplayFields = "SELECT je.JournalEntryId AS Id,je.FinancialYear AS `F.Y.`, "+
							"je.Posted,je.Cancel,je.Journal,je.Form,je.VoucherNo AS `Voucher No.`, "+
							"DATE_FORMAT(je.DatePrepared,'%m-%d-%Y') AS `Date Prepared`,je.Explanation, "+
							"je.TotalDebit AS `Total Debit`,je.TotalCredit AS `Total Credit`, "+
							"je.Reference, s.Name AS Supplier, "+
							"preby.Username AS `Prepared By`, "+
							"posby.Username AS `Posted By`,DATE_FORMAT(je.DatePosted,'%m-%d-%Y') AS `Date Posted`, "+
							"canby.Username AS `Cancelled By`,je.CancelledReason AS `Cancelled Reason`, "+
							"DATE_FORMAT(je.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`,je.Remarks "+
							"FROM journalentry je "+
							"LEFT JOIN supplier s "+
							"ON je.SupplierId = s.Id "+
							"LEFT JOIN `user` preby "+
							"ON je.PreparedBy = preby.Id "+
							"LEFT JOIN `user` posby "+
							"ON je.PostedBy = posby.Id "+
                            "LEFT JOIN `user` canby " +
							"ON je.CancelledBy = canby.Id ";
                string _WhereFields = " AND je.`Status` = 'Active' AND je.Journal = 'CDJ' AND "+
							"je.FinancialYear = "+GlobalVariables.CurrentFinancialYear+" ORDER BY je.JournalEntryId DESC;";
                loSearches.lAlias = "je.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtJournalEntry = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtJournalEntry);

                    viewDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void CheckDisbursementJournalUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(JournalEntry);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmCashDisbursementJournal");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CashDisbursementJournalUI_Load");
                em.ShowDialog();
                return;
            }

        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "F.Y." ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Journal" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Form" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Voucher No." ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Check No." ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "For Cancellation")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Debit" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Total Credit")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Posted")
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

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void dgvDetailList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Account Code")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Debit" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Credit")
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

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Post"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Journal Entry is already POSTED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot POST a Journal Entry you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue posting this Journal Entry record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loJournalEntry.post(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            decimal _paymentAmount = 0;
                            foreach (DataRow _drList in loCashDisbursementDetail.getCashDisbursementDetails(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                            {
                                decimal _runningBalance = 0;
                                _paymentAmount = decimal.Parse(_drList["Payment Amount"].ToString());

                                foreach (DataRow _drPO in loPurchaseOrder.getAllData("", _drList["P.O. Id"].ToString(), "").Rows)
                                {
                                    _runningBalance = decimal.Parse(_drPO["Running Balance"].ToString());
                                    if (_paymentAmount >= _runningBalance)
                                    {
                                        loPurchaseOrder.updatePORunningBalance(_drList["P.O. Id"].ToString(), 0);
                                    }
                                    else
                                    {
                                        loPurchaseOrder.updatePORunningBalance(_drList["P.O. Id"].ToString(), (_runningBalance - _paymentAmount));
                                    }
                                }
                            }

                            MessageBoxUI _mb1 = new MessageBoxUI("Journal Entry record has been successfully posted!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
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

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "cboCompany_SelectedIndexChanged");
                em.ShowDialog();
                return;
            }
        }

        private void tsmViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvList, ldtJournalEntry);
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCreate_Click(null, new EventArgs());
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnPost_Click(null, new EventArgs());
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Cancel"))
                {
                    return;
                }
                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel an UNPOSTED Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Journal Entry is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Journal Entry you preprared/posted!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Journal Entry record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        AccountingCancelReasonUI loReason = new AccountingCancelReasonUI();
                        loReason.ShowDialog();
                        if (loReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loJournalEntry.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loReason.lReason))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Journal Entry record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, new EventArgs());
        }

        private void tsmiRequestToCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCheckDisbursementJournal", "Request to Cancel"))
                {
                    return;
                }
                
                string pCompanyCode = "";
                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    pCompanyCode = _drStatus["Company"].ToString();
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel an unposted Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Journal Entry is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                AccountingCancelReasonUI loReason = new AccountingCancelReasonUI();
                loReason.ShowDialog();
                if (loReason.lReason != "")
                {
                    /*
                    if (loJournalEntry.forCancellation(dgvList.CurrentRow.Cells[0].Value.ToString()))
                    {
                        foreach (DataRow _dr in loJournalEntry.getAccountantEmailAddressForCancel(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                        {
                            string _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                        "<h3>Reason : " + loReason.lReason + "</h3>" +
                                        "<h4>Form : Check Voucher</h4>" +
                                        "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                        "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                        "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                        "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                        "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                        "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                        "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                        "<h4>Prepared By : " + dgvList.CurrentRow.Cells[15].Value.ToString() + "</h4>" +
                                        "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[16].Value.ToString() + "</h4>" +
                                        "</br> " +
                                        "<table border=\"1\">";

                            string _bodyDetailHeader = "<tr>" +
                                                            "<th>Account Code</th>" +//1
                                                            "<th>Account Title</th>" +//2
                                                            "<th>Debit</th>" +//3
                                                            "<th>Credit</th>" +//4
                                                            "<th>Subsidiary</th>" +//5
                                                            "<th>Description</th>" +//6
                                                            "<th>Remarks</th>" +//7
                                                       "</tr>";
                            string _bodycontent = "";
                            foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                            {
                                _bodycontent += "<tr>" +
                                                    "<td>" + _drbody[1].ToString() + "</td>" +
                                                    "<td>" + _drbody[2].ToString() + "</td>" +
                                                    "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[3].ToString())) + "</td>" +
                                                    "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[4].ToString())) + "</td>" +
                                                    "<td align=\"center\">" + _drbody[5].ToString() + "</td>" +
                                                    "<td>" + _drbody[6].ToString() + "</td>" +
                                                    "<td align=\"center\">" + _drbody[7].ToString() + "</td>" +
                                                "</tr>";
                            }

                            try
                            {
                                if (_dr[1].ToString() != "")
                                {
                                    string[] emailAdd = _dr[1].ToString().Split(',');
                                    for (int i = 0; i < emailAdd.Length; i++)
                                    {
                                        foreach (DataRow _dr1 in loJournalEntry.getCheckAccountantEmailAddress(emailAdd[i], pCompanyCode).Rows)
                                        {
                                            if (int.Parse(_dr1[0].ToString()) > 0)
                                            {
                                                GlobalFunctions.SendEmail(emailAdd[i], "", "Request to Cancel (J.E. Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + ")", _bodyhead + _bodyDetailHeader + _bodycontent);
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiRequestToCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiPrintCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Print Check"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Posted"].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot print a check of an unposted Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["Cancel"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Journal Entry is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                foreach (DataRow _dr in loCheckDetail.getCheckDetail(dgvCheckDetail.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    loPrintCheckRpt.SetParameterValue("Date", _dr["Check Date"].ToString());
                    loPrintCheckRpt.SetParameterValue("Payee", "***" + _dr["Payee"].ToString().ToUpper() + "***");
                    loPrintCheckRpt.SetParameterValue("AmountInFigures", "***" + string.Format("{0:n}", decimal.Parse(_dr["Check Amount"].ToString()))+"***");
                    loPrintCheckRpt.SetParameterValue("AmountInWords", "***" + GlobalFunctions.NumberToCurrencyText(decimal.Parse(_dr["Check Amount"].ToString()), 0).ToUpper()+"***");
                    loReportViewer.crystalReportViewer.ReportSource = loPrintCheckRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiPrintCheck_Click");
                em.ShowDialog();
                return;
            }
        }
      
        private void tsmRequestToCheckAndPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCheckDisbursementJournal", "Request to Check and Post"))
                {
                    return;
                }
                
                string pCompanyCode = "";
                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    pCompanyCode = _drStatus["Company"].ToString();
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot repost a posted Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                /*
                foreach (DataRow _dr in loJournalEntry.getAccountantEmailAddress(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    string _form = dgvList.CurrentRow.Cells[5].Value.ToString();
                    string _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Check/Cash Voucher</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                    "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[15].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    
                    string _bodyDetailHeader = "<tr>" +
                                                    "<th>Account Code</th>" +//1
                                                    "<th>Account Title</th>" +//2
                                                    "<th>Debit</th>" +//3
                                                    "<th>Credit</th>" +//4
                                                    "<th>Subsidiary</th>" +//5
                                                    "<th>Description</th>" +//6
                                                    "<th>Remarks</th>" +//7
                                               "</tr>";
                    string _bodycontent = "";
                    foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                    {
                        _bodycontent += "<tr>" +
                                            "<td>" + _drbody[1].ToString() + "</td>" +
                                            "<td>" + _drbody[2].ToString() + "</td>" +
                                            "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[3].ToString())) + "</td>" +
                                            "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[4].ToString())) + "</td>" +
                                            "<td align=\"center\">" + _drbody[5].ToString() + "</td>" +
                                            "<td>" + _drbody[6].ToString() + "</td>" +
                                            "<td align=\"center\">" + _drbody[7].ToString() + "</td>" +
                                        "</tr>";
                    }

                    try
                    {
                        if (_dr[1].ToString() != "")
                        {
                            string[] emailAdd = _dr[1].ToString().Split(',');
                            for (int i = 0; i < emailAdd.Length; i++)
                            {
                                foreach (DataRow _dr1 in loJournalEntry.getCheckAccountantEmailAddress(emailAdd[i], pCompanyCode).Rows)
                                {
                                    if (int.Parse(_dr1[0].ToString()) > 0)
                                    {
                                        GlobalFunctions.SendEmail(emailAdd[i], "", "Request to Check and Post (J.E. Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + ")", _bodyhead + _bodyDetailHeader + _bodycontent);
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }*/
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmRequestToCheckAndPost_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_KeyUp");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetailDisbursement_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvDetailDisbursement.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvDetailDisbursement.Columns[e.ColumnIndex].Name == "P.O. Id")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailDisbursement.Columns[e.ColumnIndex].Name == "Amount Due" ||
                    this.dgvDetailDisbursement.Columns[e.ColumnIndex].Name == "Payment Amount" ||
                    this.dgvDetailDisbursement.Columns[e.ColumnIndex].Name == "Balance")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvDetailDisbursement_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvCheckDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvCheckDetail.Columns[e.ColumnIndex].Name == "Id" ||
                    this.dgvCheckDetail.Columns[e.ColumnIndex].Name == "Check No.")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvCheckDetail.Columns[e.ColumnIndex].Name == "Check Amount")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvCheckDetail_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPreviewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Preview Detail"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loJournalEntry.getJournalEntryStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only APPROVED Journal Entry can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Journal Entry!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPreviewDetail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvCheckDetail_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    System.Drawing.Point pt = dgvCheckDetail.PointToScreen(e.Location);
                    cmsCheckFunction.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvCheckDetail_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmCashDisbursementJournal", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loCashDisbursementJournalRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loCashDisbursementJournalRpt.Database.Tables[1].SetDataSource(ldtJournalEntry);
                    loCashDisbursementJournalRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loCashDisbursementJournalRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loCashDisbursementJournalRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loCashDisbursementJournalRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loCashDisbursementJournalRpt.SetParameterValue("Title", "Cash Disbursement Journal");
                    loCashDisbursementJournalRpt.SetParameterValue("SubTitle", "Cash Disbursement Journal");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loCashDisbursementJournalRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loCashDisbursementJournalRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loCashDisbursementJournalRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loCashDisbursementJournalRpt;
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
    }
}
