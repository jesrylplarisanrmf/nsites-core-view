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
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports
{
    public partial class GeneralLedgerUI : Form
    {
        JournalEntryDetail loJournalEntryDetail;
        ChartOfAccount loChartOfAccount;
        DataTable ldtJournalEntryDetail;
        GeneralLedgerRpt loGeneralLedgerRpt;

        public GeneralLedgerUI()
        {
            InitializeComponent();
            loJournalEntryDetail = new JournalEntryDetail();
            loChartOfAccount = new ChartOfAccount();
            ldtJournalEntryDetail = new DataTable();
            loGeneralLedgerRpt = new GeneralLedgerRpt();
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
                foreach (DataRow _dr in loJournalEntryDetail.getGeneralLedgerAccounts(dtpFinancialYear.Value.Year).Rows)
                {
                    int i = dgvAccount.Rows.Add();
                    dgvAccount.Rows[i].Cells[0].Value = _dr[0].ToString();
                    dgvAccount.Rows[i].Cells[1].Value = _dr[1].ToString();
                    dgvAccount.Rows[i].Cells[2].Value = _dr[2].ToString();
                }
                viewLedger();
            }
            catch { }
        }

        private void viewLedger()
        {
            try
            {
                loGeneralLedgerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loGeneralLedgerRpt.Database.Tables[1].SetDataSource(loJournalEntryDetail.getGeneralLedgerDetails(dgvAccount.CurrentRow.Cells[0].Value.ToString(), dtpFinancialYear.Value.Year));
                loGeneralLedgerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loGeneralLedgerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loGeneralLedgerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loGeneralLedgerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loGeneralLedgerRpt.SetParameterValue("Title", "General Ledger");
                loGeneralLedgerRpt.SetParameterValue("SubTitle", "General Ledger");
                loGeneralLedgerRpt.SetParameterValue("Account", dgvAccount.CurrentRow.Cells[1].Value.ToString() + " - " + dgvAccount.CurrentRow.Cells[2].Value.ToString());
                loGeneralLedgerRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", DateTime.Now));
                crvGL.ReportSource = loGeneralLedgerRpt;
            }
            catch { }
        }

        private void viewLedgerByDate()
        {
            try
            {
                loGeneralLedgerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loGeneralLedgerRpt.Database.Tables[1].SetDataSource(loJournalEntryDetail.getGeneralLedgerDetailsByDate(dgvAccount.CurrentRow.Cells[0].Value.ToString(), dtpFinancialYear.Value.Year, dtpDate.Value));
                loGeneralLedgerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loGeneralLedgerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loGeneralLedgerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loGeneralLedgerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loGeneralLedgerRpt.SetParameterValue("Title", "General Ledger");
                loGeneralLedgerRpt.SetParameterValue("SubTitle", "General Ledger");
                loGeneralLedgerRpt.SetParameterValue("Account", dgvAccount.CurrentRow.Cells[1].Value.ToString() + " - " + dgvAccount.CurrentRow.Cells[2].Value.ToString());
                loGeneralLedgerRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", dtpDate.Value));
                crvGLByDate.ReportSource = loGeneralLedgerRpt;
            }
            catch { }
        }

        private void PayrollReportUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmGeneralLedger", "Refresh"))
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

        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void dgvInvestmentList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvAccount.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvAccount.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvAccount.Columns[e.ColumnIndex].Name == "Code")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvInvestmentList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            viewLedger();
        }

        private void btnView_Click(object sender, EventArgs e)
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
    }
}
