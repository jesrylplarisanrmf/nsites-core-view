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
    public partial class JournalEntryUI : Form
    {
        JournalEntry loJournalEntry;
        JournalEntryRpt loJournalEntryRpt;

        public JournalEntryUI()
        {
            InitializeComponent();
            loJournalEntry = new JournalEntry();
            loJournalEntryRpt = new JournalEntryRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void JournalEntryUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmJournalEntry", "Refresh"))
                {
                    return;
                }

                DataTable _dt = loJournalEntry.getJournalEntryReport(dtpFinancialYear.Value.Year);
                if (_dt.Rows.Count > 0)
                {
                    loJournalEntryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loJournalEntryRpt.Database.Tables[1].SetDataSource(loJournalEntry.getJournalEntryReport(dtpFinancialYear.Value.Year));
                    loJournalEntryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loJournalEntryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loJournalEntryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loJournalEntryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loJournalEntryRpt.SetParameterValue("Title", "Journal Entry");
                    loJournalEntryRpt.SetParameterValue("SubTitle", "Journal Entry");
                    loJournalEntryRpt.SetParameterValue("FinancialYear", dtpFinancialYear.Value.Year.ToString());
                    crystalReportViewer.ReportSource = loJournalEntryRpt;
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("No records found!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
