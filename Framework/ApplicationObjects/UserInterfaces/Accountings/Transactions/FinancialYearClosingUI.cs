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
using NSites_V.ApplicationObjects.Classes.Accountings;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    public partial class FinancialYearClosingUI : Form
    {
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        ChartOfAccount loChartOfAccount;
        
        public FinancialYearClosingUI()
        {
            InitializeComponent();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loChartOfAccount = new ChartOfAccount();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void load()
        {
            try
            {
                decimal _totalEarnings = 0;
                decimal _totalExpenses = 0;
                decimal _overAllTotalDebit = 0;
                decimal _overAllTotalCredit = 0;
                //income
                try
                {
                    dgvIncomeEntry.Rows.Clear();
                    decimal _totaldebit = 0;
                    decimal _totalcredit = 0;
                    foreach (DataRow _drIncomeStatement in loJournalEntryDetail.getIncomeStatementForClosingEntry(int.Parse(txtFinancialYear.Text), GlobalVariables.IncomeClassificationCode).Rows)
                    {
                        decimal _total = decimal.Parse(_drIncomeStatement["Credit"].ToString()) - decimal.Parse(_drIncomeStatement["Debit"].ToString());
                        if (_total >= 0)
                        {
                            _totaldebit += _total;
                            _totalcredit += 0;

                            int i = dgvIncomeEntry.Rows.Add();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountId"].Value = _drIncomeStatement["Account Id"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountCode"].Value = _drIncomeStatement["Account Code"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountTitle"].Value = _drIncomeStatement["Account Title"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeDebit"].Value = string.Format("{0:n}", _total);
                            dgvIncomeEntry.Rows[i].Cells["IncomeCredit"].Value = string.Format("{0:n}", 0);
                            _overAllTotalDebit += _total;
                            _overAllTotalCredit += 0;
                        }
                        else
                        {
                            _totaldebit += 0;
                            _totalcredit += (_total * -1);

                            int i = dgvIncomeEntry.Rows.Add();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountId"].Value = _drIncomeStatement["Account Id"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountCode"].Value = _drIncomeStatement["Account Code"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeAccountTitle"].Value = _drIncomeStatement["Account Title"].ToString();
                            dgvIncomeEntry.Rows[i].Cells["IncomeDebit"].Value = string.Format("{0:n}", 0);
                            dgvIncomeEntry.Rows[i].Cells["IncomeCredit"].Value = string.Format("{0:n}", _total * -1);
                            _overAllTotalDebit += 0;
                            _overAllTotalCredit += (_total * -1);
                        }
                    }

                    int j = dgvIncomeEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.IncomeAndExpenseSummaryCode, "").Rows)
                    {
                        dgvIncomeEntry.Rows[j].Cells["IncomeAccountId"].Value = _dr["Id"].ToString();
                        dgvIncomeEntry.Rows[j].Cells["IncomeAccountCode"].Value = _dr["Code"].ToString();
                        dgvIncomeEntry.Rows[j].Cells["IncomeAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvIncomeEntry.Rows[j].Cells["IncomeDebit"].Value = string.Format("{0:n}", _totalcredit);
                        dgvIncomeEntry.Rows[j].Cells["IncomeCredit"].Value = string.Format("{0:n}", _totaldebit);
                        _overAllTotalDebit += _totalcredit;
                        _overAllTotalCredit += _totaldebit;
                        _totalEarnings = _totaldebit - _totalcredit;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //expenses
                try
                {
                    dgvExpenseEntry.Rows.Clear();
                    decimal _totaldebit = 0;
                    decimal _totalcredit = 0;
                    foreach (DataRow _drIncomeStatement in loJournalEntryDetail.getIncomeStatementForClosingEntry(int.Parse(txtFinancialYear.Text), GlobalVariables.ExpensesClassificationCode).Rows)
                    {
                        decimal _total = decimal.Parse(_drIncomeStatement["Debit"].ToString()) - decimal.Parse(_drIncomeStatement["Credit"].ToString());
                        if (_total >= 0)
                        {
                            _totaldebit += 0;
                            _totalcredit += _total;

                            int i = dgvExpenseEntry.Rows.Add();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountId"].Value = _drIncomeStatement["Account Id"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountCode"].Value = _drIncomeStatement["Account Code"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountTitle"].Value = _drIncomeStatement["Account Title"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseDebit"].Value = string.Format("{0:n}", 0);
                            dgvExpenseEntry.Rows[i].Cells["ExpenseCredit"].Value = string.Format("{0:n}", _total);
                            _overAllTotalDebit += 0;
                            _overAllTotalCredit += _total;
                        }
                        else
                        {
                            _totaldebit += (_total * -1);
                            _totalcredit += 0;

                            int i = dgvExpenseEntry.Rows.Add();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountId"].Value = _drIncomeStatement["Account Id"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountCode"].Value = _drIncomeStatement["Account Code"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseAccountTitle"].Value = _drIncomeStatement["Account Title"].ToString();
                            dgvExpenseEntry.Rows[i].Cells["ExpenseDebit"].Value = string.Format("{0:n}", (_total * -1));
                            dgvExpenseEntry.Rows[i].Cells["ExpenseCredit"].Value = string.Format("{0:n}", 0);
                            _overAllTotalDebit += (_total * -1);
                            _overAllTotalCredit += 0;
                        }
                    }

                    int j = dgvExpenseEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.IncomeAndExpenseSummaryCode, "").Rows)
                    {
                        dgvExpenseEntry.Rows[j].Cells["ExpenseAccountId"].Value = _dr["Id"].ToString();
                        dgvExpenseEntry.Rows[j].Cells["ExpenseAccountCode"].Value = _dr["Code"].ToString();
                        dgvExpenseEntry.Rows[j].Cells["ExpenseAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvExpenseEntry.Rows[j].Cells["ExpenseDebit"].Value = string.Format("{0:n}", _totalcredit);
                        dgvExpenseEntry.Rows[j].Cells["ExpenseCredit"].Value = string.Format("{0:n}", _totaldebit);
                        _overAllTotalDebit += _totalcredit;
                        _overAllTotalCredit += _totaldebit;
                        _totalExpenses = _totalcredit - _totaldebit;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                // retained earnings
                if (_totalEarnings >= _totalExpenses)
                {
                    dgvRetainedEarningEntry.Rows.Clear();
                    int k = dgvRetainedEarningEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.RetainedEarningsCode, "").Rows)
                    {
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountId"].Value = _dr["Id"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountCode"].Value = _dr["Code"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REDebit"].Value = string.Format("{0:n}", 0);
                        dgvRetainedEarningEntry.Rows[k].Cells["RECredit"].Value = string.Format("{0:n}", _totalEarnings - _totalExpenses);
                        _overAllTotalDebit += 0;
                        _overAllTotalCredit += _totalEarnings - _totalExpenses;
                    }

                    int l = dgvRetainedEarningEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.IncomeAndExpenseSummaryCode, "").Rows)
                    {
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountId"].Value = _dr["Id"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountCode"].Value = _dr["Code"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvRetainedEarningEntry.Rows[l].Cells["REDebit"].Value = string.Format("{0:n}", _totalEarnings - _totalExpenses);
                        dgvRetainedEarningEntry.Rows[l].Cells["RECredit"].Value = string.Format("{0:n}", 0);
                        _overAllTotalDebit += _totalEarnings - _totalExpenses;
                        _overAllTotalCredit += 0;
                    }
                }
                else
                {
                    dgvRetainedEarningEntry.Rows.Clear();
                    int k = dgvRetainedEarningEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.RetainedEarningsCode, "").Rows)
                    {
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountId"].Value = _dr["Id"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountCode"].Value = _dr["Code"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REDebit"].Value = string.Format("{0:n}", (_totalEarnings - _totalExpenses) * -1);
                        dgvRetainedEarningEntry.Rows[k].Cells["RECredit"].Value = string.Format("{0:n}", 0);
                        _overAllTotalDebit += (_totalEarnings - _totalExpenses) * -1;
                        _overAllTotalCredit += 0;
                    }

                    int l = dgvRetainedEarningEntry.Rows.Add();
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.IncomeAndExpenseSummaryCode, "").Rows)
                    {
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountId"].Value = _dr["Id"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountCode"].Value = _dr["Code"].ToString();
                        dgvRetainedEarningEntry.Rows[k].Cells["REAccountTitle"].Value = _dr["Account Title"].ToString();
                        dgvRetainedEarningEntry.Rows[l].Cells["REDebit"].Value = string.Format("{0:n}", 0);
                        dgvRetainedEarningEntry.Rows[l].Cells["RECredit"].Value = string.Format("{0:n}", (_totalEarnings - _totalExpenses) * -1);
                        _overAllTotalDebit += 0;
                        _overAllTotalCredit += (_totalEarnings - _totalExpenses) * -1;
                    }
                }

                txtTotalDebit.Text = string.Format("{0:n}", _overAllTotalDebit);
                txtTotalCredit.Text = string.Format("{0:n}", _overAllTotalCredit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FinancialYearClosingUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                txtFinancialYear.Text = GlobalVariables.CurrentFinancialYear;
                dtpDate.Value = DateTime.Parse("12-31-" + GlobalVariables.CurrentFinancialYear);
                load();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "FinancialYearClosingUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmFinancialYearClosing", "Close"))
                {
                    return;
                }

                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb1 = new MessageBoxUI("Are sure you want to continue closing the Financial Year?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb1.ShowDialog();
                _dr = _mb1.Operation;
                if (_dr == DialogResult.Yes)
                {
                    //closing entry
                    try
                    {
                        loJournalEntry.JournalEntryId = "";
                        loJournalEntry.FinancialYear = int.Parse(txtFinancialYear.Text);
                        loJournalEntry.Journal = "GJ";
                        loJournalEntry.Form = "JV";
                        loJournalEntry.VoucherNo = "";
                        loJournalEntry.DatePrepared = dtpDate.Value;
                        loJournalEntry.Explanation = "System : Closing Entry";
                        loJournalEntry.TotalDebit = decimal.Parse(txtTotalDebit.Text);
                        loJournalEntry.TotalCredit = decimal.Parse(txtTotalCredit.Text);
                        loJournalEntry.Reference = "";
                        loJournalEntry.SupplierId = "";
                        loJournalEntry.CustomerId = "";
                        loJournalEntry.BegBal = "N";
                        loJournalEntry.Adjustment = "N";
                        loJournalEntry.ClosingEntry = "Y";
                        loJournalEntry.PreparedBy = GlobalVariables.Username;
                        loJournalEntry.Remarks = "";
                        loJournalEntry.SOId = "0";
                        loJournalEntry.POId = "0";
                        loJournalEntry.UserId = GlobalVariables.UserId;

                        try
                        {
                            string _JournalEntryId = loJournalEntry.save(GlobalVariables.Operation.Add);
                            if (_JournalEntryId != "")
                            {
                                //income
                                for (int i = 0; i < dgvIncomeEntry.Rows.Count; i++)
                                {
                                    loJournalEntryDetail.DetailId = "";
                                    loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                                    loJournalEntryDetail.AccountId = dgvIncomeEntry.Rows[i].Cells["IncomeAccountId"].Value.ToString();
                                    loJournalEntryDetail.Debit = decimal.Parse(dgvIncomeEntry.Rows[i].Cells["IncomeDebit"].Value.ToString());
                                    loJournalEntryDetail.Credit = decimal.Parse(dgvIncomeEntry.Rows[i].Cells["IncomeCredit"].Value.ToString());
                                    loJournalEntryDetail.Subsidiary = "";
                                    loJournalEntryDetail.SubsidiaryId = "";
                                    loJournalEntryDetail.SubsidiaryDescription = "";
                                    loJournalEntryDetail.Remarks = "";
                                    loJournalEntryDetail.UserId = GlobalVariables.UserId;
                                    loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                                }
                                //expense
                                for (int i = 0; i < dgvExpenseEntry.Rows.Count; i++)
                                {
                                    loJournalEntryDetail.DetailId = "";
                                    loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                                    loJournalEntryDetail.AccountId = dgvExpenseEntry.Rows[i].Cells["ExpenseAccountId"].Value.ToString();
                                    loJournalEntryDetail.Debit = decimal.Parse(dgvExpenseEntry.Rows[i].Cells["ExpenseDebit"].Value.ToString());
                                    loJournalEntryDetail.Credit = decimal.Parse(dgvExpenseEntry.Rows[i].Cells["ExpenseCredit"].Value.ToString());
                                    loJournalEntryDetail.Subsidiary = "";
                                    loJournalEntryDetail.SubsidiaryId = "";
                                    loJournalEntryDetail.SubsidiaryDescription = "";
                                    loJournalEntryDetail.Remarks = "";
                                    loJournalEntryDetail.UserId = GlobalVariables.UserId;
                                    loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                                }
                                //retained earning
                                for (int i = 0; i < dgvRetainedEarningEntry.Rows.Count; i++)
                                {
                                    loJournalEntryDetail.DetailId = "";
                                    loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                                    loJournalEntryDetail.AccountId = dgvRetainedEarningEntry.Rows[i].Cells["REAccountId"].Value.ToString();
                                    loJournalEntryDetail.Debit = decimal.Parse(dgvRetainedEarningEntry.Rows[i].Cells["REDebit"].Value.ToString());
                                    loJournalEntryDetail.Credit = decimal.Parse(dgvRetainedEarningEntry.Rows[i].Cells["RECredit"].Value.ToString());
                                    loJournalEntryDetail.Subsidiary = "";
                                    loJournalEntryDetail.SubsidiaryId = "";
                                    loJournalEntryDetail.SubsidiaryDescription = "";
                                    loJournalEntryDetail.Remarks = "";
                                    loJournalEntryDetail.UserId = GlobalVariables.UserId;
                                    loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        //loJournalEntry.closeFinancialYear(int.Parse(txtFinancialYear.Text)+1);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    MessageBoxUI _mb2 = new MessageBoxUI("Financial Year : " + txtFinancialYear.Text + " has been successfully closed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb2.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
