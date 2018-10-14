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
    public partial class IncomeStatementUI : Form
    {
        ChartOfAccount loChartOfAccount;
        JournalEntryDetail loJournalEntryDetail;
        IncomeStatementRpt loIncomeStatementRpt;
        DataTable IncomeTable;
        DataTable ExpenseTable;
        decimal lTotalIncome;
        decimal lTotalExpense;
        
        public IncomeStatementUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loJournalEntryDetail = new JournalEntryDetail();
            loIncomeStatementRpt = new IncomeStatementRpt();
            lTotalIncome = 0;
            lTotalExpense = 0;

            //Datatables
            IncomeTable = new DataTable();
            IncomeTable.Columns.Add("SubClass", typeof(string));
            IncomeTable.Columns.Add("MainAccount", typeof(string));
            IncomeTable.Columns.Add("Total", typeof(decimal));

            ExpenseTable = new DataTable();
            ExpenseTable.Columns.Add("SubClass", typeof(string));
            ExpenseTable.Columns.Add("MainAccount", typeof(string));
            ExpenseTable.Columns.Add("Total", typeof(decimal));
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

                IncomeTable.Rows.Clear();

                foreach (DataRow _drSubClass in loJournalEntryDetail.getBalanceSheetSubClassifications(GlobalVariables.IncomeClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccounts(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        IncomeTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), decimal.Parse(_drMain[2].ToString()) - decimal.Parse(_drMain[1].ToString()));
                    }
                }
                foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.IncomeClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    lTotalIncome = decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString());
                }

                ExpenseTable.Rows.Clear();

                foreach (DataRow _drSubClass in loJournalEntryDetail.getBalanceSheetSubClassifications(GlobalVariables.ExpensesClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccounts(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        ExpenseTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), decimal.Parse(_drMain[1].ToString()) - decimal.Parse(_drMain[2].ToString()));
                    }
                }
                foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.ExpensesClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    lTotalExpense = decimal.Parse(_drClass[1].ToString()) - decimal.Parse(_drClass[2].ToString());
                }

                loIncomeStatementRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loIncomeStatementRpt.Subreports["IncomeRpt.rpt"].SetDataSource(IncomeTable);
                loIncomeStatementRpt.Subreports["ExpenseRpt.rpt"].SetDataSource(ExpenseTable);
                loIncomeStatementRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loIncomeStatementRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loIncomeStatementRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loIncomeStatementRpt.SetParameterValue("PeriodCovered", "For The Year Ended " + string.Format("{0:MMMM dd, yyyy}", DateTime.Parse(_AsOf)));
                loIncomeStatementRpt.SetParameterValue("NetIncome", string.Format("{0:n}", lTotalIncome - lTotalExpense));
                loIncomeStatementRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loIncomeStatementRpt.SetParameterValue("Title", "Income Statement");
                loIncomeStatementRpt.SetParameterValue("SubTitle", "Income Statement");
                crystalReportViewer.ReportSource = loIncomeStatementRpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StatementOfComprehensiveIncomeUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmIncomeStatement", "Preview"))
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
