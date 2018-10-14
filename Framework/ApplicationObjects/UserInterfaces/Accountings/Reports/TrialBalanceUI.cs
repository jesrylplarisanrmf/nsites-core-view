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
    public partial class TrialBalanceUI : Form
    {
        ChartOfAccount loChartOfAccount;
        JournalEntryDetail loJournalEntryDetail;
        TrialBalanceRpt loTrialBalanceRpt;

        public TrialBalanceUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loJournalEntryDetail = new JournalEntryDetail();
            loTrialBalanceRpt = new TrialBalanceRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void refresh()
        {
            try
            {
                string _AsOf = GlobalFunctions.GetDate(dtpFinancialYear.Value.Year.ToString(), cboMonth.Text);
                loTrialBalanceRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loTrialBalanceRpt.Database.Tables[1].SetDataSource(loJournalEntryDetail.getTrialBalance(dtpFinancialYear.Value.Year, _AsOf));
                loTrialBalanceRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loTrialBalanceRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loTrialBalanceRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loTrialBalanceRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loTrialBalanceRpt.SetParameterValue("Title", "Trial Balance");
                loTrialBalanceRpt.SetParameterValue("SubTitle", "Trial Balance");
                loTrialBalanceRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", DateTime.Parse(_AsOf)));
                crystalReportViewer.ReportSource = loTrialBalanceRpt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void TrialBalanceUI_Load(object sender, EventArgs e)
        {
            cboMonth.Text = string.Format("{0:MMMM}", DateTime.Now);
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

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmTrialBalance", "Preview"))
                {
                    return;
                }
                refresh();
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
