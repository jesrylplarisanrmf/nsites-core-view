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
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    public partial class FinancialYearOpeningUI : Form
    {
        ChartOfAccount loChartOfAccount;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;

        public FinancialYearOpeningUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void updateData(string[] pRecordData)
        {
            try
            {
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvBalanceForwarded.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void computeTotal()
        {
            try
            {
                decimal _totalDebit = 0;
                decimal _totalCredit = 0;
                for (int i = 0; i < dgvBalanceForwarded.Rows.Count; i++)
                {
                    _totalDebit += decimal.Parse(dgvBalanceForwarded.Rows[i].Cells["Debit"].Value.ToString());
                    _totalCredit += decimal.Parse(dgvBalanceForwarded.Rows[i].Cells["Credit"].Value.ToString());
                }
                txtTotalDebit.Text = string.Format("{0:n}", _totalDebit);
                txtTotalCredit.Text = string.Format("{0:n}", _totalCredit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getSubsidiary(string pAccountId)
        {
            try
            {
                string _Subsidiary = "";
                foreach (DataRow _dr in loChartOfAccount.getAllData("", pAccountId, "").Rows)
                {
                    _Subsidiary = _dr["Subsidiary"].ToString();
                }
                return _Subsidiary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void load()
        {
            // balance forwarded
            try
            {
                dgvBalanceForwarded.Rows.Clear();
                decimal _postClosingDebit;
                decimal _postClosingCredit;
                foreach (DataRow _drBalanceForwarded in loJournalEntryDetail.getBalanceForwardedAccounts(int.Parse(txtFinancialYear.Text) - 1).Rows)
                {
                    if (_drBalanceForwarded["Classification"].ToString() == "4" & _drBalanceForwarded["Classification"].ToString() == "5")
                    {
                        continue;
                    }
                    _postClosingDebit = 0;
                    _postClosingCredit = 0;

                    if (_drBalanceForwarded["ContraAccount"].ToString() == "N")
                    {
                        if (_drBalanceForwarded["Classification"].ToString() == "1")
                        {
                            {
                                decimal _Total = decimal.Parse(_drBalanceForwarded[4].ToString()) - decimal.Parse(_drBalanceForwarded[5].ToString());
                                if (_Total > 0)
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
                        else if (_drBalanceForwarded["Classification"].ToString() == "2" || _drBalanceForwarded["Classification"].ToString() == "3")
                        {
                            decimal _Total = decimal.Parse(_drBalanceForwarded[5].ToString()) - decimal.Parse(_drBalanceForwarded[4].ToString());
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
                        if (_drBalanceForwarded["Classification"].ToString() == "1")
                        {
                            {
                                decimal _Total = decimal.Parse(_drBalanceForwarded[5].ToString()) - decimal.Parse(_drBalanceForwarded[4].ToString());
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
                        else if (_drBalanceForwarded["Classification"].ToString() == "2" || _drBalanceForwarded["Classification"].ToString() == "3")
                        {
                            decimal _Total = decimal.Parse(_drBalanceForwarded[4].ToString()) - decimal.Parse(_drBalanceForwarded[5].ToString());
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
                    if (_postClosingDebit != 0 || _postClosingCredit != 0)
                    {
                        int i = dgvBalanceForwarded.Rows.Add();
                        dgvBalanceForwarded.Rows[i].Cells["Id"].Value = "";
                        dgvBalanceForwarded.Rows[i].Cells["AccountId"].Value = _drBalanceForwarded["AccountId"].ToString();
                        dgvBalanceForwarded.Rows[i].Cells["AccountCode"].Value = _drBalanceForwarded["AccountCode"].ToString();
                        dgvBalanceForwarded.Rows[i].Cells["AccountTitle"].Value = _drBalanceForwarded["AccountTitle"].ToString();
                        dgvBalanceForwarded.Rows[i].Cells["Debit"].Value = string.Format("{0:n}", _postClosingDebit);
                        dgvBalanceForwarded.Rows[i].Cells["Credit"].Value = string.Format("{0:n}", _postClosingCredit);

                        dgvBalanceForwarded.Rows[i].Cells["Subsidiary"].Value = getSubsidiary(_drBalanceForwarded["AccountId"].ToString());
                        dgvBalanceForwarded.Rows[i].Cells["SubsidiaryId"].Value = "";
                        dgvBalanceForwarded.Rows[i].Cells["Description"].Value = "";
                        dgvBalanceForwarded.Rows[i].Cells["Remarks"].Value = "";
                        dgvBalanceForwarded.Rows[i].Cells["Status"].Value = "Add";
                    }
                }
                computeTotal();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmFinancialYearOpening", "Open"))
            {
                return;
            }
            //balance forwarded or beginning balance
            try
            {
                if (txtTotalDebit.Text != txtTotalCredit.Text)
                {
                    MessageBoxUI _mb = new MessageBoxUI("DR and CR must be equal/balance!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
      
                loJournalEntry.JournalEntryId = "";
                loJournalEntry.FinancialYear = int.Parse(txtFinancialYear.Text);
                loJournalEntry.Journal = "GJ";
                loJournalEntry.Form = "JV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = dtpDate.Value;
                loJournalEntry.Explanation = "System : Balance Forwarded";
                loJournalEntry.TotalDebit = decimal.Parse(txtTotalDebit.Text);
                loJournalEntry.TotalCredit = decimal.Parse(txtTotalCredit.Text);
                loJournalEntry.Reference = "";
                loJournalEntry.SupplierId = "";
                loJournalEntry.CustomerId = "";
                loJournalEntry.BegBal = "Y";
                loJournalEntry.Adjustment = "N";
                loJournalEntry.ClosingEntry = "N";
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
                        //balance forwarded
                        for (int i = 0; i < dgvBalanceForwarded.Rows.Count; i++)
                        {
                            loJournalEntryDetail.DetailId = dgvBalanceForwarded.Rows[i].Cells["Id"].Value.ToString();
                            loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                            loJournalEntryDetail.AccountId = dgvBalanceForwarded.Rows[i].Cells["AccountId"].Value.ToString();
                            loJournalEntryDetail.Debit = decimal.Parse(dgvBalanceForwarded.Rows[i].Cells["Debit"].Value.ToString());
                            loJournalEntryDetail.Credit = decimal.Parse(dgvBalanceForwarded.Rows[i].Cells["Credit"].Value.ToString());
                            loJournalEntryDetail.Subsidiary = dgvBalanceForwarded.Rows[i].Cells["Subsidiary"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryId = dgvBalanceForwarded.Rows[i].Cells["SubsidiaryId"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryDescription = dgvBalanceForwarded.Rows[i].Cells["Description"].Value.ToString();
                            loJournalEntryDetail.Remarks = dgvBalanceForwarded.Rows[i].Cells["Remarks"].Value.ToString();
                            loJournalEntryDetail.UserId = GlobalVariables.UserId;
                            loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                MessageBoxUI _mb2 = new MessageBoxUI("Beginning Balance has been successfully created!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                _mb2.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnOpen_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmFinancialYearOpening", "Edit"))
                {
                    return;
                }

                if (dgvBalanceForwarded.Rows.Count > 0)
                {
                    VoucherDetailUI loVoucherDetail = new VoucherDetailUI(
                        dgvBalanceForwarded.CurrentRow.Cells["Id"].Value.ToString(),
                        dgvBalanceForwarded.CurrentRow.Cells["AccountId"].Value.ToString(),
                        decimal.Parse(dgvBalanceForwarded.CurrentRow.Cells["Debit"].Value.ToString()),
                        decimal.Parse(dgvBalanceForwarded.CurrentRow.Cells["Credit"].Value.ToString()),
                        dgvBalanceForwarded.CurrentRow.Cells["Subsidiary"].Value.ToString(),
                        dgvBalanceForwarded.CurrentRow.Cells["SubsidiaryId"].Value.ToString(),
                        dgvBalanceForwarded.CurrentRow.Cells["Description"].Value.ToString(),
                        dgvBalanceForwarded.CurrentRow.Cells["Remarks"].Value.ToString());
                    loVoucherDetail.ParentList = this;
                    loVoucherDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEdit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvBalanceForwarded_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void FinancialYearOpeningUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                txtFinancialYear.Text = GlobalVariables.CurrentFinancialYear;
                dtpDate.Value = DateTime.Parse("01-01-" + GlobalVariables.CurrentFinancialYear);
                load();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "FinancialYearOpeningUI_Load");
                em.ShowDialog();
                return;
            }
        }
    }
}
