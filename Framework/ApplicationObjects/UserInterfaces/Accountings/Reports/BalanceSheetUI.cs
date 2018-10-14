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
    public partial class BalanceSheetUI : Form
    {
        ChartOfAccount loChartOfAccount;
        JournalEntryDetail loJournalEntryDetail;
        BalanceSheetRpt loBalanceSheetRpt;
        DataTable ldtAssetSubClassification;
        DataTable ldtAssetMainAccount;
        ReportViewerUI loReportViewer;
        DataTable AssetTable;
        DataTable LiabilityTable;
        DataTable EquityTable;
        decimal lTotalLiability;
        decimal lTotalEquity;
        
        public BalanceSheetUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loJournalEntryDetail = new JournalEntryDetail();
            loBalanceSheetRpt = new BalanceSheetRpt();
            ldtAssetSubClassification = new DataTable();
            ldtAssetMainAccount = new DataTable();
            loReportViewer = new ReportViewerUI();
            lTotalLiability = 0;
            lTotalEquity = 0;

            //Datatables
            AssetTable = new DataTable();
            AssetTable.Columns.Add("SubClass", typeof(string));
            AssetTable.Columns.Add("MainAccount", typeof(string));
            AssetTable.Columns.Add("Total", typeof(decimal));

            LiabilityTable = new DataTable();
            LiabilityTable.Columns.Add("SubClass", typeof(string));
            LiabilityTable.Columns.Add("MainAccount", typeof(string));
            LiabilityTable.Columns.Add("Total", typeof(decimal));

            EquityTable = new DataTable();
            EquityTable.Columns.Add("SubClass", typeof(string));
            EquityTable.Columns.Add("MainAccount", typeof(string));
            EquityTable.Columns.Add("Total", typeof(decimal));
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
                lTotalLiability = 0;
                lTotalEquity = 0;

                string _AsOf = GlobalFunctions.GetDate(dtpFinancialYear.Value.Year.ToString(), cboMonth.Text);

                AssetTable.Rows.Clear();

                //get asset
                foreach (DataRow _drSubClass in loJournalEntryDetail.getBalanceSheetSubClassifications(GlobalVariables.AssetClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccounts(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        AssetTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), decimal.Parse(_drMain[1].ToString()) - decimal.Parse(_drMain[2].ToString()));
                    }
                }

                LiabilityTable.Rows.Clear();

                foreach (DataRow _drSubClass in loJournalEntryDetail.getBalanceSheetSubClassifications(GlobalVariables.LiabilityClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccounts(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        LiabilityTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), decimal.Parse(_drMain[2].ToString()) - decimal.Parse(_drMain[1].ToString()));
                    }
                }
                foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.LiabilityClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    lTotalLiability = (decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString()));
                }

                EquityTable.Rows.Clear();
                decimal _totalRetainedEarnings = 0;
                decimal _totalincome = 0;
                decimal _totalexpense = 0;
                foreach (DataRow _drSubClass in loJournalEntryDetail.getBalanceSheetSubClassifications(GlobalVariables.EquityClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccounts(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        EquityTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), decimal.Parse(_drMain[2].ToString()) - decimal.Parse(_drMain[1].ToString()));
                        lTotalEquity += decimal.Parse(_drMain[2].ToString()) - decimal.Parse(_drMain[1].ToString());
                    }
                    //get net income
                    foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.IncomeClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        _totalincome = decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString());
                    }

                    foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.ExpensesClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        _totalexpense = decimal.Parse(_drClass[1].ToString()) - decimal.Parse(_drClass[2].ToString());
                    }
                    foreach (DataRow _drMain in loJournalEntryDetail.getBalanceSheetMainAccountsForRetainedEarnings(_drSubClass[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                    {
                        _totalRetainedEarnings = decimal.Parse(_drMain[2].ToString()) - decimal.Parse(_drMain[1].ToString());
                        _totalRetainedEarnings += (_totalincome - _totalexpense);
                        EquityTable.Rows.Add(_drSubClass[0].ToString(), _drMain[0].ToString(), _totalRetainedEarnings);
                        lTotalEquity += _totalRetainedEarnings;
                    }
                }

                loBalanceSheetRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loBalanceSheetRpt.Subreports["AssetRpt.rpt"].SetDataSource(AssetTable);
                loBalanceSheetRpt.Subreports["LiabilityRpt.rpt"].SetDataSource(LiabilityTable);
                loBalanceSheetRpt.Subreports["EquityRpt.rpt"].SetDataSource(EquityTable);
                loBalanceSheetRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loBalanceSheetRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loBalanceSheetRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loBalanceSheetRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", DateTime.Parse(_AsOf)));
                loBalanceSheetRpt.SetParameterValue("TotalLiabilityAndEquity", string.Format("{0:n}", lTotalLiability + lTotalEquity));
                loBalanceSheetRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loBalanceSheetRpt.SetParameterValue("Title", "Balance Sheet");
                loBalanceSheetRpt.SetParameterValue("SubTitle", "Balance Sheet");
                crystalReportViewer.ReportSource = loBalanceSheetRpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*
            foreach (DataRow _drClass in loChartOfAccount.getBalanceSheetClassifications(lCompanyCode, "3", dtpFinancialYear.Value.Year, _AsOf).Rows)
            {
                //lTotalLiabilityAndEquity = lTotalLiabilityAndEquity + (decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString()) + _totalRetainedEarnings);
                lTotalEquity = (decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString()));
            }
            */
        }

        private void StatementOfFinancialPositionUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmBalanceSheet", "Preview"))
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
