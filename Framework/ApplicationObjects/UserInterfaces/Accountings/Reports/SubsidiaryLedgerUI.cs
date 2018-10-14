using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports
{
    public partial class SubsidiaryLedgerUI : Form
    {
        JournalEntryDetail loJournalEntryDetail;
        ChartOfAccount loChartOfAccount;
        DataTable ldtJournalEntryDetail;
        SubsidiaryLedgerRpt loSubsidiaryLedgerRpt;
        ReportViewerUI loReportViewer;

        public SubsidiaryLedgerUI()
        {
            InitializeComponent();
            loJournalEntryDetail = new JournalEntryDetail();
            loChartOfAccount = new ChartOfAccount();
            ldtJournalEntryDetail = new DataTable();
            loSubsidiaryLedgerRpt = new SubsidiaryLedgerRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void getAccountTitle()
        {
            try
            {
                dgvAccount.Rows.Clear();
                foreach (DataRow _dr in loJournalEntryDetail.getSubsidiaryLedgerAccounts(dtpFinancialYear.Value.Year).Rows)
                {
                    int i = dgvAccount.Rows.Add();
                    dgvAccount.Rows[i].Cells[0].Value = _dr[0].ToString();
                    dgvAccount.Rows[i].Cells[1].Value = _dr[1].ToString();
                    dgvAccount.Rows[i].Cells[2].Value = _dr[2].ToString();
                    dgvAccount.Rows[i].Cells[3].Value = _dr[3].ToString();
                }
                getSubsidiary();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void getSubsidiary()
        {
            try
            {
                dgvSubsidiary.DataSource = loJournalEntryDetail.getSubsidiaries(dgvAccount.CurrentRow.Cells[0].Value.ToString(), dgvAccount.CurrentRow.Cells[3].Value.ToString(), dtpFinancialYear.Value.Year);
                viewLedger();
            }
            catch
            {
                dgvSubsidiary.DataSource = null;
            }
        }

        private void viewLedger()
        {
            try
            {
                loSubsidiaryLedgerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loSubsidiaryLedgerRpt.Database.Tables[1].SetDataSource(loJournalEntryDetail.getSubsidiaryLedgerDetails(dgvAccount.CurrentRow.Cells[0].Value.ToString(),dgvSubsidiary.CurrentRow.Cells[0].Value.ToString(),dgvAccount.CurrentRow.Cells[3].Value.ToString(), dtpFinancialYear.Value.Year));
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loSubsidiaryLedgerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loSubsidiaryLedgerRpt.SetParameterValue("Title", "Subsidiary Ledger");
                loSubsidiaryLedgerRpt.SetParameterValue("SubTitle", "Subsidiary Ledger");
                loSubsidiaryLedgerRpt.SetParameterValue("Account", dgvAccount.CurrentRow.Cells[1].Value.ToString() + " - " + dgvAccount.CurrentRow.Cells[2].Value.ToString());
                try
                {
                    loSubsidiaryLedgerRpt.SetParameterValue("Subsidiary", dgvSubsidiary.CurrentRow.Cells[1].Value.ToString() + "\n" + dgvSubsidiary.CurrentRow.Cells[2].Value.ToString());
                }
                catch
                {
                    loSubsidiaryLedgerRpt.SetParameterValue("Subsidiary", dgvSubsidiary.CurrentRow.Cells[1].Value.ToString());
                }
                loSubsidiaryLedgerRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", DateTime.Now));
                crvSL.ReportSource = loSubsidiaryLedgerRpt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void viewLedgerByDate()
        {
            try
            {
                loSubsidiaryLedgerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loSubsidiaryLedgerRpt.Database.Tables[1].SetDataSource(loJournalEntryDetail.getSubsidiaryLedgerDetailsByDate(dgvAccount.CurrentRow.Cells[0].Value.ToString(), dgvSubsidiary.CurrentRow.Cells[0].Value.ToString(), dgvAccount.CurrentRow.Cells[3].Value.ToString(), dtpFinancialYear.Value.Year, dtpDate.Value));
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loSubsidiaryLedgerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loSubsidiaryLedgerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loSubsidiaryLedgerRpt.SetParameterValue("Title", "Subsidiary Ledger");
                loSubsidiaryLedgerRpt.SetParameterValue("SubTitle", "Subsidiary Ledger");
                loSubsidiaryLedgerRpt.SetParameterValue("Account", dgvAccount.CurrentRow.Cells[0].Value.ToString() + " - " + dgvAccount.CurrentRow.Cells[1].Value.ToString());
                try
                {
                    loSubsidiaryLedgerRpt.SetParameterValue("Subsidiary", dgvSubsidiary.CurrentRow.Cells[1].Value.ToString() + " - " + dgvSubsidiary.CurrentRow.Cells[2].Value.ToString());
                }
                catch
                {
                    loSubsidiaryLedgerRpt.SetParameterValue("Subsidiary", dgvSubsidiary.CurrentRow.Cells[1].Value.ToString());
                }
                loSubsidiaryLedgerRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", dtpDate.Value));
                crvSLByDate.ReportSource = loSubsidiaryLedgerRpt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void SubsidiaryLedgerUI_Load(object sender, EventArgs e)
        {

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
                if (!GlobalFunctions.checkRights("tsmSubsidiaryLedger", "Refresh"))
                {
                    return;
                }
                getAccountTitle();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                getSubsidiary();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvAccount_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvSubsidiary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                viewLedger();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvSubsidiary_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvSubsidiary_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvSubsidiary.Columns[e.ColumnIndex].Name == "Code")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvSubsidiary.Columns[e.ColumnIndex].Name == "No. of Shares")
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvSubsidiary_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDate.Value.Year == dtpFinancialYear.Value.Year)
                {
                    viewLedgerByDate();
                }
                else
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Year must be the same with the Financial Year!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    dtpDate.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnView_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
