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
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    public partial class JournalEntrysUI : Form
    {
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        Common loCommon;
        SearchesUI loSearches;
        System.Data.DataTable ldtJournalEntry;
        JournalEntryRpt loJournalEntryRpt;
        ReportViewerUI loReportViewer;
        
        public JournalEntrysUI()
        {
            InitializeComponent();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCommon = new Common();
            ldtJournalEntry = new System.Data.DataTable();
            loJournalEntryRpt = new JournalEntryRpt();
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
                    ldtJournalEntry = loJournalEntry.getAllData("", "ViewAll", "", "");
                }
                catch
                {
                    ldtJournalEntry = null;
                }
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
                dgvDetailList.DataSource = loJournalEntryDetail.getJournalEntryDetails("ViewAll", dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        public void sendEmailToCreator()
        {
            try
            {
                string _form = dgvList.CurrentRow.Cells[5].Value.ToString();
                string _bodyhead = "";
                if (_form == "JV")
                {
                    _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Journal Voucher</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";
                }
                else if (_form == "CV")
                {
                    _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Check Voucher</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";
                }
                else if (_form == "OR")
                {
                    _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Official Receipts</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Payor : " + dgvList.CurrentRow.Cells[13].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";
                }
                else if (_form == "PV")
                {
                    _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Payable Voucher</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";
                }
                else if (_form == "DV")
                {
                    _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                "<h4>Form : Dividend Voucher</h4>" +
                                "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">";
                }
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
                foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails("ViewAll", dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
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
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Refresh"))
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loJournalEntryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loJournalEntryRpt.Database.Tables[1].SetDataSource(ldtJournalEntry);
                    loJournalEntryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loJournalEntryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loJournalEntryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loJournalEntryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loJournalEntryRpt.SetParameterValue("Title", "Journal Entry");
                    loJournalEntryRpt.SetParameterValue("SubTitle", "Journal Entry");
                    loJournalEntryRpt.SetParameterValue("Company", "");
                    loReportViewer.crystalReportViewer.ReportSource = loJournalEntryRpt;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Search"))
                {
                    return;
                }
                
                string _DisplayFields = "SELECT je.JournalEntryId AS Id,je.FinancialYear AS [F.Y.], "+
			            "je.Posted,c.Name AS Company,je.Journal,je.Form, "+
			            "je.VoucherNo AS [Voucher No.], "+
			            "CONVERT(VARCHAR(10),je.DatePrepared,110) AS [Date Prepared], "+
			            "je.Explanation,je.TotalDebit AS [Total Debit],je.TotalCredit AS [Total Credit], "+
			            "je.CheckNo AS [Check No.],s.Name AS Payee,cust.Name AS Payor, "+
			            "je.BegBal AS [Beg. Bal.],je.Adjustment, "+
                        "je.ForCancellation as [For Cancellation],je.Cancel,je.Reason, " +
			            "je.PreparedBy AS [Prepared By], "+
			            "je.CertifiedCorrect AS [Certified Correct],je.Remarks "+
			            "FROM journalentry je "+
			            "LEFT JOIN company c "+
			            "ON je.Company = c.Code "+
			            "LEFT JOIN supplier s "+
			            "ON je.Payee = s.Id "+
                        "LEFT JOIN customer cust " +
			            "ON je.Payor = cust.Id ";
                string _WhereFields = " AND je.[Status] = 'Active' AND je.Company in (Select CompanyCode from usergroupcompany where UserGroupCode = '" + GlobalVariables.UserGroupId + "') ORDER BY je.Posted DESC;";
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSearch_Click");
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
                    this.dgvList.Columns[e.ColumnIndex].Name == "Beg. Bal." ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Adjustment" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Closing Entry" ||
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
                        e.Value = string.Format("{0:n}",decimal.Parse(e.Value.ToString()));
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
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Post"))
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
                            MessageBoxUI _mb1 = new MessageBoxUI("Journal Entry has been successfully posted!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            sendEmailToCreator();
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

        private void tsmSendEmailForPosting_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Request to Check and Post"))
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
                    string _bodyhead = "";
                    if (_form == "JV")
                    {
                        _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Journal Voucher</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    }
                    else if (_form == "CV")
                    {
                        _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Check Voucher</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                    "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    }
                    else if (_form == "OR")
                    {
                        _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Official Receipts</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Payor : " + dgvList.CurrentRow.Cells[13].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    }
                    else if (_form == "PV")
                    {
                        _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Payable Voucher</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    }
                    else if (_form == "DV")
                    {
                        _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                    "<h4>Form : Dividend Voucher</h4>" +
                                    "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                    "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                    "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                    "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                    "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                    "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                    "</br> " +
                                    "<table border=\"1\">";
                    }
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSendEmailForPosting_Click");
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

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, new EventArgs());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmJournalEntry", "Cancel"))
            {
                return;
            }
            try
            {
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

        private void tsmiRequestToCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Request to Cancel"))
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
                            string _form = dgvList.CurrentRow.Cells[5].Value.ToString();
                            string _bodyhead = "";
                            if (_form == "JV")
                            {
                                _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                            "<h3>Reason : " + loReason.lReason + "</h3>" +
                                            "<h4>Form : Journal Voucher</h4>" +
                                            "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                            "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                            "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                            "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                            "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                            "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                            "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[20].Value.ToString() + "</h4>" +
                                            "</br> " +
                                            "<table border=\"1\">";
                            }
                            else if (_form == "CV")
                            {
                                _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                            "<h3>Reason : " + loReason.lReason + "</h3>" +
                                            "<h4>Form : Check Voucher</h4>" +
                                            "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                            "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                            "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                            "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                            "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                            "<h4>Check No. : " + dgvList.CurrentRow.Cells[11].Value.ToString() + "</h4>" +
                                            "<h4>Payee : " + dgvList.CurrentRow.Cells[12].Value.ToString() + "</h4>" +
                                            "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                            "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[20].Value.ToString() + "</h4>" +
                                            "</br> " +
                                            "<table border=\"1\">";
                            }
                            else if (_form == "OR")
                            {
                                _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                            "<h3>Reason : " + loReason.lReason + "</h3>" +
                                            "<h4>Form : Official Receipts</h4>" +
                                            "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                            "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                            "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                            "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                            "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                            "<h4>Payor : " + dgvList.CurrentRow.Cells[13].Value.ToString() + "</h4>" +
                                            "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                            "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[20].Value.ToString() + "</h4>" +
                                            "</br> " +
                                            "<table border=\"1\">";
                            }
                            else if (_form == "PV")
                            {
                                _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                            "<h3>Reason : " + loReason.lReason + "</h3>" +
                                            "<h4>Form : Payable Voucher</h4>" +
                                            "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                            "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                            "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                            "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                            "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                            "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                            "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[20].Value.ToString() + "</h4>" +
                                            "</br> " +
                                            "<table border=\"1\">";
                            }
                            else if (_form == "DV")
                            {
                                _bodyhead = "<h3>Journal Entry Id : " + dgvList.CurrentRow.Cells[0].Value.ToString() + "</h3>" +
                                            "<h3>Reason : " + loReason.lReason + "</h3>" +
                                            "<h4>Form : Dividend Voucher</h4>" +
                                            "<h4>Company : " + dgvList.CurrentRow.Cells[3].Value.ToString() + "</h4>" +
                                            "<h4>Date Prepared : " + dgvList.CurrentRow.Cells[7].Value.ToString() + "</h4>" +
                                            "<h4>Explanation : " + dgvList.CurrentRow.Cells[8].Value.ToString() + "</h4>" +
                                            "<h4>Total Debit : " + dgvList.CurrentRow.Cells[9].Value.ToString() + "</h4>" +
                                            "<h4>Total Credit : " + dgvList.CurrentRow.Cells[10].Value.ToString() + "</h4>" +
                                            "<h4>Prepared By : " + dgvList.CurrentRow.Cells[19].Value.ToString() + "</h4>" +
                                            "<h4>Certified Correct : " + dgvList.CurrentRow.Cells[20].Value.ToString() + "</h4>" +
                                            "</br> " +
                                            "<table border=\"1\">";
                            }
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

        private void chkViewUnposted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "chkViewUnposted_CheckedChanged");
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

        private void JournalEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(JournalEntry);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmJournalEntry");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "JournalEntryUI_Load");
                em.ShowDialog();
                return;
            }
        }
    }
}
