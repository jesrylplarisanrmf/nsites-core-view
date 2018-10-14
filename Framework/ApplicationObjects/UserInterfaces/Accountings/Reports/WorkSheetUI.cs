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
    public partial class WorkSheetUI : Form
    {
        ChartOfAccount loChartOfAccount;
        JournalEntryDetail loJournalEntryDetail;
        DataTable WorkSheetTable;
        WorkSheetRpt loWorkSheetRpt;
        
        public WorkSheetUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loJournalEntryDetail = new JournalEntryDetail();
            loWorkSheetRpt = new WorkSheetRpt();
            //Datatables
            WorkSheetTable = new DataTable();
            WorkSheetTable.Columns.Add("AccountCode", typeof(string));
            WorkSheetTable.Columns.Add("AccountTitle", typeof(string));
            WorkSheetTable.Columns.Add("BegBalDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("BegBalCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("TrialBalanceDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("TrialBalanceCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("AdjustmentDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("AdjustmentCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("AdjustedTrialBalanceDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("AdjustedTrialBalanceCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("BalanceSheetDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("BalanceSheetCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("IncomeStatementDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("IncomeStatementCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("ClosingEntryDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("ClosingEntryCredit", typeof(decimal));
            WorkSheetTable.Columns.Add("PostClosingDebit", typeof(decimal));
            WorkSheetTable.Columns.Add("PostClosingCredit", typeof(decimal));
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
            WorkSheetTable.Rows.Clear();
            decimal _begbalDebit, _begbalCredit, _trialbalanceDebit, _trialbalanceCredit, _adjustmentDebit, _adjustmentCredit,
                _adjustedtrialbalaceDebit, _adjustedtrialbalanceCredit, _balancesheetDebit, _balancesheetCredit,
                _incomestatementDebit, _incomestatementCredit,_closingEntryDebit,_closingEntryCredit,
                _postClosingDebit,_postClosingCredit, lTotalIncome, lTotalExpense,lTotalNetLossBalanceSheet,lTotalNetLossIncomeStatement;
            lTotalIncome = 0;
            lTotalExpense = 0;
            lTotalNetLossBalanceSheet = 0;
            lTotalNetLossIncomeStatement = 0;
            decimal _totalBalanceSheetDebit = 0;
            decimal _totalBalanceSheetCredit = 0;
            decimal _totalIncomeStatementDebit = 0;
            decimal _totalIncomeStatementCredit = 0;
            string _AsOf = GlobalFunctions.GetDate(dtpFinancialYear.Value.Year.ToString(), cboMonth.Text);
            try
            {
                foreach (DataRow _drAccount in loJournalEntryDetail.getWorkSheetAccounts(dtpFinancialYear.Value.Year, _AsOf).Rows)
                {
                    _begbalDebit = 0;
                    _begbalCredit = 0;
                    _trialbalanceDebit = 0;
                    _trialbalanceCredit = 0;
                    _adjustmentDebit = 0;
                    _adjustmentCredit = 0;
                    _adjustedtrialbalaceDebit = 0;
                    _adjustedtrialbalanceCredit = 0;
                    _balancesheetDebit = 0;
                    _balancesheetCredit = 0;
                    _incomestatementDebit = 0;
                    _incomestatementCredit = 0;
                    _closingEntryDebit = 0;
                    _closingEntryCredit = 0;
                    _postClosingDebit = 0;
                    _postClosingCredit = 0;
                    if (_drAccount[0].ToString() != GlobalVariables.IncomeAndExpenseSummaryCode)
                    {
                        //get beginning balance
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetBeginningBalance(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            _begbalDebit = decimal.Parse(_dr[0].ToString());
                            _begbalCredit = decimal.Parse(_dr[1].ToString());
                        }
                        //get trial balance
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetTrialBalance(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            _trialbalanceDebit = decimal.Parse(_dr[0].ToString());
                            _trialbalanceCredit = decimal.Parse(_dr[1].ToString());
                        }
                        //get adjustment
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetAdjustment(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            _adjustmentDebit = decimal.Parse(_dr[0].ToString());
                            _adjustmentCredit = decimal.Parse(_dr[1].ToString());
                        }
                        //adjust trial balance
                        _adjustedtrialbalaceDebit = _begbalDebit + _trialbalanceDebit + _adjustmentDebit;
                        _adjustedtrialbalanceCredit = _begbalCredit + _trialbalanceCredit + _adjustmentCredit;
                        //get balance sheet
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetBalanceSheet(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            if (_drAccount["ContraAccount"].ToString() == "N")
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    {
                                        decimal _Total = decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                        if (_Total >= 0)
                                        {
                                            _balancesheetDebit = _Total;
                                            _balancesheetCredit = 0;

                                            _totalBalanceSheetDebit +=_Total;
                                        }
                                        else
                                        {
                                            _balancesheetDebit = 0;
                                            _balancesheetCredit = _Total * -1;

                                            _totalBalanceSheetCredit += (_Total * -1);
                                        }
                                    }
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                    if (_Total >= 0)
                                    {
                                        _balancesheetDebit = 0;
                                        _balancesheetCredit = _Total;

                                        _totalBalanceSheetCredit += _Total;
                                    }
                                    else
                                    {
                                        _balancesheetDebit = _Total * -1;
                                        _balancesheetCredit = 0;

                                        _totalBalanceSheetDebit += (_Total * -1);
                                    }

                                    lTotalNetLossBalanceSheet += decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                }
                                else
                                {
                                    _balancesheetDebit = 0;
                                    _balancesheetCredit = 0;
                                    lTotalNetLossBalanceSheet += 0;
                                }
                            }
                            else
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    {
                                        decimal _Total = decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                        if (_Total >= 0)
                                        {
                                            _balancesheetDebit = 0;
                                            _balancesheetCredit = _Total;

                                            _totalBalanceSheetCredit += _Total;
                                        }
                                        else
                                        {
                                            _balancesheetDebit = _Total * -1;
                                            _balancesheetCredit = 0;

                                            _totalBalanceSheetDebit += (_Total * -1);
                                        }

                                        lTotalNetLossBalanceSheet += decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                    }
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                    if (_Total >= 0)
                                    {
                                        _balancesheetDebit = _Total;
                                        _balancesheetCredit = 0;

                                        _totalBalanceSheetDebit += _Total;
                                    }
                                    else
                                    {
                                        _balancesheetDebit = 0;
                                        _balancesheetCredit = _Total * -1;

                                        _totalBalanceSheetCredit += (_Total*-1);
                                    }
                                }
                                else
                                {
                                    _balancesheetDebit = 0;
                                    _balancesheetCredit = 0;
                                    lTotalNetLossBalanceSheet += 0;
                                }
                            }
                        }
                        //get income statement
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetIncomeStatement(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            if (_drAccount["ContraAccount"].ToString() == "N")
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                    if (_Total >= 0)
                                    {
                                        _incomestatementDebit = _Total;
                                        _incomestatementCredit = 0;

                                        _totalIncomeStatementDebit += _Total;
                                    }
                                    else
                                    {
                                        _incomestatementDebit = 0;
                                        _incomestatementCredit = _Total * -1;

                                        _totalIncomeStatementCredit += (_Total * -1);
                                    }
                                    lTotalNetLossIncomeStatement += decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                    if (_Total >= 0)
                                    {
                                        _incomestatementDebit = 0;
                                        _incomestatementCredit = _Total;

                                        _totalIncomeStatementCredit += _Total;
                                    }
                                    else
                                    {
                                        _incomestatementDebit = _Total * -1;
                                        _incomestatementCredit = 0;

                                        _totalIncomeStatementDebit += (_Total * -1);
                                    }
                                }
                                else
                                {
                                    _incomestatementDebit = 0;
                                    _incomestatementCredit = 0;
                                    lTotalNetLossIncomeStatement += 0;
                                }
                            }
                            else
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[1].ToString()) - decimal.Parse(_dr[0].ToString());
                                    if (_Total >= 0)
                                    {
                                        _incomestatementDebit = 0;
                                        _incomestatementCredit = _Total;

                                        _totalIncomeStatementCredit += _Total;
                                    }
                                    else
                                    {
                                        _incomestatementDebit = _Total * -1;
                                        _incomestatementCredit = 0;

                                        _totalIncomeStatementDebit += (_Total * -1);
                                    }
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                    if (_Total >= 0)
                                    {
                                        _incomestatementDebit = _Total;
                                        _incomestatementCredit = 0;

                                        _totalIncomeStatementDebit += _Total;
                                    }
                                    else
                                    {
                                        _incomestatementDebit = 0;
                                        _incomestatementCredit = _Total * -1;

                                        _totalIncomeStatementCredit += (_Total * -1);
                                    }
                                    lTotalNetLossIncomeStatement += decimal.Parse(_dr[0].ToString()) - decimal.Parse(_dr[1].ToString());
                                }
                                else
                                {
                                    _incomestatementDebit = 0;
                                    _incomestatementCredit = 0;
                                    lTotalNetLossIncomeStatement += 0;
                                }
                            }
                        }

                        //get closing entry
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetClosingEntry(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            _closingEntryDebit = decimal.Parse(_dr[0].ToString());
                            _closingEntryCredit = decimal.Parse(_dr[1].ToString());
                        }

                        //get net income
                        foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.IncomeClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            lTotalIncome = decimal.Parse(_drClass[2].ToString()) - decimal.Parse(_drClass[1].ToString());
                        }

                        foreach (DataRow _drClass in loJournalEntryDetail.getBalanceSheetClassifications(GlobalVariables.ExpensesClassificationCode, dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            lTotalExpense = decimal.Parse(_drClass[1].ToString()) - decimal.Parse(_drClass[2].ToString());
                        }

                        //get post-closing
                        foreach (DataRow _drAmount in loJournalEntryDetail.getAccountBeginningBalance(dtpFinancialYear.Value.Year, _drAccount[0].ToString()).Rows)
                        {
                            if (_drAccount["ContraAccount"].ToString() == "N")
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    {
                                        decimal _Total = decimal.Parse(_drAmount[1].ToString()) - decimal.Parse(_drAmount[2].ToString());
                                        if(_Total >= 0)
                                        {
                                            _postClosingDebit = _Total;
                                            _postClosingCredit = 0;
                                        }
                                        else
                                        {
                                            _postClosingDebit = 0;
                                            _postClosingCredit = _Total * -1;
                                        }
                                    }
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_drAmount[2].ToString()) - decimal.Parse(_drAmount[1].ToString());
                                    if (_Total >= 0)
                                    {
                                        _postClosingDebit = 0;
                                        _postClosingCredit = _Total;
                                    }
                                    else
                                    {
                                        _postClosingDebit = _Total * -1;
                                        _postClosingCredit = 0;
                                    }
                                }
                                else
                                {
                                    _postClosingDebit = 0;
                                    _postClosingCredit = 0;
                                }
                            }
                            else
                            {
                                if (_drAccount["Classification"].ToString() == GlobalVariables.AssetClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.ExpensesClassificationCode)
                                {
                                    {
                                        decimal _Total = decimal.Parse(_drAmount[2].ToString()) - decimal.Parse(_drAmount[1].ToString());
                                        if (_Total >= 0)
                                        {
                                            _postClosingDebit = 0;
                                            _postClosingCredit = _Total;
                                        }
                                        else
                                        {
                                            _postClosingDebit = _Total * -1;
                                            _postClosingCredit = 0;
                                        }
                                    }
                                }
                                else if (_drAccount["Classification"].ToString() == GlobalVariables.LiabilityClassificationCode || _drAccount["Classification"].ToString() == GlobalVariables.EquityClassificationCode ||
                                    _drAccount["Classification"].ToString() == GlobalVariables.IncomeClassificationCode)
                                {
                                    decimal _Total = decimal.Parse(_drAmount[1].ToString()) - decimal.Parse(_drAmount[2].ToString());
                                    if (_Total >= 0)
                                    {
                                        _postClosingDebit = _Total;
                                        _postClosingCredit = 0;
                                    }
                                    else
                                    {
                                        _postClosingDebit = 0;
                                        _postClosingCredit = _Total * -1;
                                    }
                                }
                                else
                                {
                                    _postClosingDebit = 0;
                                    _postClosingCredit = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        //get closing entry
                        foreach (DataRow _dr in loJournalEntryDetail.getWorkSheetClosingEntry(_drAccount[0].ToString(), dtpFinancialYear.Value.Year, _AsOf).Rows)
                        {
                            _closingEntryDebit = decimal.Parse(_dr[0].ToString());
                            _closingEntryCredit = decimal.Parse(_dr[1].ToString());
                        }

                        //get post-closing
                        foreach (DataRow _drAmount in loJournalEntryDetail.getAccountBeginningBalance(dtpFinancialYear.Value.Year, _drAccount[0].ToString()).Rows)
                        {
                            if (decimal.Parse(_drAmount[1].ToString()) == decimal.Parse(_drAmount[2].ToString()))
                            {
                                _postClosingDebit = 0;
                                _postClosingCredit = 0;
                            }
                            else
                            {
                                _postClosingDebit = decimal.Parse(_drAmount[1].ToString());
                                _postClosingCredit = decimal.Parse(_drAmount[2].ToString());
                            }
                        }
                    }

                    WorkSheetTable.Rows.Add(_drAccount[0].ToString(), _drAccount[1].ToString(), _begbalDebit, _begbalCredit, _trialbalanceDebit, _trialbalanceCredit,
                        _adjustmentDebit, _adjustmentCredit, _adjustedtrialbalaceDebit, _adjustedtrialbalanceCredit, _balancesheetDebit,
                        _balancesheetCredit, _incomestatementDebit, _incomestatementCredit,_closingEntryDebit,_closingEntryCredit,_postClosingDebit,_postClosingCredit);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                loWorkSheetRpt.SetDataSource(WorkSheetTable);
                loWorkSheetRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loWorkSheetRpt.SetParameterValue("PeriodCovered", "As of " + string.Format("{0:MMMM dd, yyyy}", DateTime.Parse(_AsOf)));
                loWorkSheetRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loWorkSheetRpt.SetParameterValue("SummaryOfIncomeAndExpense", string.Format("{0:n}", lTotalIncome - lTotalExpense));

                loWorkSheetRpt.SetParameterValue("TotalNetLossIncomeStatement", string.Format("{0:n}", lTotalNetLossIncomeStatement + (lTotalIncome - lTotalExpense)));

                if ((lTotalIncome - lTotalExpense) >= 0)
                {
                    //for balance sheet
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossBalanceDebit", "");
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossBalanceCredit", string.Format("{0:n}", lTotalIncome - lTotalExpense));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossBalanceSheetDebit", string.Format("{0:n}", _totalBalanceSheetDebit));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossBalanceSheetCredit", string.Format("{0:n}", _totalBalanceSheetCredit + (lTotalIncome - lTotalExpense)));

                    //for income statement
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossIncomeDebit", string.Format("{0:n}", lTotalIncome - lTotalExpense));
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossIncomeCredit", "");
                    loWorkSheetRpt.SetParameterValue("TotalNetLossIncomeStatementDebit", string.Format("{0:n}", _totalIncomeStatementDebit + (lTotalIncome - lTotalExpense)));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossIncomeStatementCredit", string.Format("{0:n}", _totalIncomeStatementCredit));
                }
                else
                {
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossBalanceDebit", string.Format("{0:n}", (lTotalIncome - lTotalExpense) * -1));
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossBalanceCredit", "");
                    loWorkSheetRpt.SetParameterValue("TotalNetLossBalanceSheetDebit", string.Format("{0:n}", _totalBalanceSheetDebit + ((lTotalIncome - lTotalExpense) * -1)));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossBalanceSheetCredit", string.Format("{0:n}", _totalBalanceSheetCredit));

                    //for income statement
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossIncomeDebit", "");
                    loWorkSheetRpt.SetParameterValue("NetIncomeLossIncomeCredit", string.Format("{0:n}", (lTotalIncome - lTotalExpense) * -1));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossIncomeStatementDebit", string.Format("{0:n}", _totalIncomeStatementDebit));
                    loWorkSheetRpt.SetParameterValue("TotalNetLossIncomeStatementCredit", string.Format("{0:n}", _totalIncomeStatementCredit + ((lTotalIncome - lTotalExpense) * -1)));
                }

                loWorkSheetRpt.SetParameterValue("SubTitle", "Work Sheet");
                crystalReportViewer.ReportSource = loWorkSheetRpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WorkSheetUI_Load(object sender, EventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmWorkSheet", "Refresh"))
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
    }
}
