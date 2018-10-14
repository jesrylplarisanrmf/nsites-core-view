using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Inventorys;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class CashDisbursementVoucherUI : Form
    {
        #region "VARIABLES"
        Supplier loSupplier;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        CashDisbursementDetail loCashDisbursementDetail;
        CheckDetail loCheckDetail;
        ChartOfAccount loChartOfAccount;
        PurchaseOrder loPurchaseOrder;
        GlobalVariables.Operation lOperation;
        string lJournalEntryId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CashDisbursementVoucherUI()
        {
            InitializeComponent();
            loSupplier = new Supplier();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashDisbursementDetail = new CashDisbursementDetail();
            loCheckDetail = new CheckDetail();
            loChartOfAccount = new ChartOfAccount();
            loPurchaseOrder = new PurchaseOrder();
            lOperation = GlobalVariables.Operation.Add;
            lJournalEntryId = "";
        }
        public CashDisbursementVoucherUI(string pJournalEntryId)
        {
            InitializeComponent();
            loSupplier = new Supplier();
            lOperation = GlobalVariables.Operation.Edit;
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashDisbursementDetail = new CashDisbursementDetail();
            loCheckDetail = new CheckDetail();
            loChartOfAccount = new ChartOfAccount();
            loPurchaseOrder = new PurchaseOrder();
            lJournalEntryId = pJournalEntryId;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void clear()
        {

        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvDetail.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetail.CurrentRow.Selected = false;
                dgvDetail.FirstDisplayedScrollingRowIndex = dgvDetail.Rows[n].Index;
                dgvDetail.Rows[n].Selected = true;

                computeTotal();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void updateData(string[] pRecordData)
        {
            try
            {
                string _operator = dgvDetail.CurrentRow.Cells["Status"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetail.CurrentRow.Cells["Status"].Value = "Add";
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
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        _totalDebit += decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                        _totalCredit += decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                    }
                }
                txtTotalDebit.Text = string.Format("{0:n}", _totalDebit);
                txtTotalCredit.Text = string.Format("{0:n}", _totalCredit);

                //disbursement
                decimal _TotalPaymentAmount = 0;
                for (int i = 0; i < dgvDetailDisbursement.Rows.Count; i++)
                {
                    if (dgvDetailDisbursement.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalPaymentAmount += decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementPaymentAmount"].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                txtTotalPaymentAmount.Text = string.Format("{0:n}", _TotalPaymentAmount);

                //check details
                decimal _TotalCheckAmount = 0;
                for (int i = 0; i < dgvCheckDetails.Rows.Count; i++)
                {
                    if (dgvCheckDetails.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalCheckAmount += decimal.Parse(dgvCheckDetails.Rows[i].Cells["CheckAmount"].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                txtTotalCheckAmount.Text = string.Format("{0:n}", _TotalCheckAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void sendEmailForPosting(string pJournalEntryId,string pCompanyCode, string pCompany, string pDatePrepared, string pExplanation, decimal pTotalDebit,
            decimal pTotalCredit,string pCheckNo, string pPayee, string pPreparedBy)
        {
            try
            {
                string _bodyhead = "<h3>Journal Entry Id : " + pJournalEntryId + "</h3>" +
                                "<h4>Form : Check/Cash Voucher</h4>" +
                                "<h4>Company : " + pCompany + "</h4>" +
                                "<h4>Date Prepared : " + pDatePrepared + "</h4>" +
                                "<h4>Explanation : " + pExplanation + "</h4>" +
                                "<h4>Total Debit : " + string.Format("{0:n}", pTotalDebit) + "</h4>" +
                                "<h4>Total Credit : " + string.Format("{0:n}", pTotalCredit) + "</h4>" +
                                "<h4>Check No. : " + pCheckNo + "</h4>" +
                                "<h4>Payee : " + pPayee + "</h4>" +
                                "<h4>Prepared By : " + pPreparedBy + "</h4>" +
                                "</br> " +
                                "<table border=\"1\">" +
                                "<tr>" +
                                    "<th>Account Code</th>" +//1
                                    "<th>Account Title</th>" +//2
                                    "<th>Debit</th>" +//3
                                    "<th>Credit</th>" +//4
                                    "<th>Subsidiary</th>" +//5
                                    "<th>Description</th>" +//7
                                    "<th>Remarks</th>" +//8
                                "</tr>";
                string _bodycontent = "";
                foreach (DataRow _drbody in loJournalEntryDetail.getJournalEntryDetails("ViewAll", pJournalEntryId).Rows)
                {
                    _bodycontent += "<tr>" +
                                        "<td align=\"center\">" + _drbody[1].ToString() + "</td>" +
                                        "<td>" + _drbody[2].ToString() + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[3].ToString())) + "</td>" +
                                        "<td align=\"right\">" + string.Format("{0:n}", decimal.Parse(_drbody[4].ToString())) + "</td>" +
                                        "<td align=\"center\">" + _drbody[5].ToString() + "</td>" +
                                        "<td>" + _drbody[6].ToString() + "</td>" +
                                        "<td align=\"center\">" + _drbody[7].ToString() + "</td>" +
                                    "</tr>";
                }
                /*
                //get preparer email address
                string _preparerEmailAddress = "";
                foreach (DataRow _dr in loJournalEntry.getAllData("","",pJournalEntryId,"").Rows)
                {
                    foreach (DataRow _dr1 in loJournalEntry.getPreparedByEmailAddress(_dr["Prepared By"].ToString()).Rows)
                    {
                        _preparerEmailAddress = _dr1[0].ToString();
                    }
                }

                foreach (DataRow _dr in loJournalEntry.getAccountantEmailAddress(pJournalEntryId).Rows)
                {
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
                                        GlobalFunctions.SendEmail(emailAdd[i], _preparerEmailAddress, "Request to Check and Post (J.E. Id : " + pJournalEntryId + ")", _bodyhead + _bodycontent);
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
                throw ex;
            }
        }

        public void addDataDisbursement(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                for (int i = 0; i < dgvDetailDisbursement.Rows.Count; i++)
                {
                    if (dgvDetailDisbursement.Rows[i].Cells[1].Value.ToString() == pRecordData[1])
                    {
                        if (!pFromProcess)
                        {
                            MessageBoxUI _mb = new MessageBoxUI("Duplicate", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb.showDialog();
                            return;
                        }
                    }
                }

                int n = dgvDetailDisbursement.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailDisbursement.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetailDisbursement.CurrentRow.Selected = false;
                dgvDetailDisbursement.FirstDisplayedScrollingRowIndex = dgvDetailDisbursement.Rows[n].Index;
                dgvDetailDisbursement.Rows[n].Selected = true;

                if (!pFromProcess)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateDataDisbursement(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                string _operator = dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailDisbursement.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value = "Add";
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addDataCheck(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                for (int i = 0; i < dgvCheckDetails.Rows.Count; i++)
                {
                    if (dgvCheckDetails.Rows[i].Cells[1].Value.ToString() == pRecordData[1] &&
                        dgvCheckDetails.Rows[i].Cells[3].Value.ToString() == pRecordData[3])
                    {
                        if (!pFromProcess)
                        {
                            MessageBoxUI _mb = new MessageBoxUI("Duplicate", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb.showDialog();
                            return;
                        }
                    }
                }

                int n = dgvCheckDetails.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvCheckDetails.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvCheckDetails.CurrentRow.Selected = false;
                dgvCheckDetails.FirstDisplayedScrollingRowIndex = dgvCheckDetails.Rows[n].Index;
                dgvCheckDetails.Rows[n].Selected = true;

                if (!pFromProcess)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateDataCheck(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                string _operator = dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvCheckDetails.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value = "Add";
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion "END OF METHODS"

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetail.Rows.Count == 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Journal Entry must a record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                if (dgvDetailDisbursement.Rows.Count == 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Disbursement must a record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                if (dgvCheckDetails.Rows.Count == 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Check Details must a record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                
                if (decimal.Parse(txtTotalDebit.Text) != decimal.Parse(txtTotalCredit.Text))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Totals of Debit and Credit must be equal!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                if (decimal.Parse(txtTotalDebit.Text) == 0 && decimal.Parse(txtTotalCredit.Text) == 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Totals of Debit and Credit must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                if (dtpDatePrepared.Value.Year != int.Parse(GlobalVariables.CurrentFinancialYear))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Date Prepared must be within Current Financial Year!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
                /*
                if (txtCheckNo.Text == "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Check No. must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    txtCheckNo.Focus();
                    return;
                }
                
                if (decimal.Parse(txtTotalPaymentAmount.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals Payment Amount must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                */

                //check if subsidiary has value
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString() != "" && dgvDetail.Rows[i].Cells["SubsidiaryId"].Value.ToString() == "")
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Subsidiary " + dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString() + " must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb1.showDialog();
                        return;
                    }
                }

                //check if bank in check detail has value
                for (int i = 0; i < dgvCheckDetails.Rows.Count; i++)
                {
                    if (dgvCheckDetails.Rows[i].Cells[1].Value.ToString() == "" || dgvCheckDetails.Rows[i].Cells[2].Value.ToString() == "" || dgvCheckDetails.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Bank and Check No. must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb1.showDialog();
                        return;
                    }
                }

                if (decimal.Parse(lblJournalEntryAmount.Text) != decimal.Parse(lblDisbursementsAmount.Text))
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Total Amount of Accounts and Disbursements must be equal!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }

                //check if cash in bank is the same with check amount
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString() == "Bank")
                    {
                        if ((decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString()) + decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString())) != decimal.Parse(lblCheckAmount.Text))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Cash in Bank and Check Amount must be equal!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.showDialog();
                            return;
                        }
                    }
                }

                loJournalEntry.JournalEntryId = lJournalEntryId;
                loJournalEntry.FinancialYear = int.Parse(txtFinancialYear.Text);
                loJournalEntry.Journal = "CDJ";
                loJournalEntry.Form = "CV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = dtpDatePrepared.Value;
                loJournalEntry.Explanation = GlobalFunctions.replaceChar(txtExplanation.Text);
                loJournalEntry.TotalDebit = decimal.Parse(txtTotalDebit.Text);
                loJournalEntry.TotalCredit = decimal.Parse(txtTotalCredit.Text);
                loJournalEntry.Reference = txtReference.Text;
                try
                {
                    loJournalEntry.SupplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("Supplier must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboSupplier.Focus();
                    return;
                }
                loJournalEntry.CustomerId = "";
                loJournalEntry.BegBal = "N";
                loJournalEntry.Adjustment = "N";
                loJournalEntry.ClosingEntry = "N";
                loJournalEntry.PreparedBy = GlobalVariables.Username;
                loJournalEntry.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loJournalEntry.SOId = "0";
                loJournalEntry.POId = "0";
                loJournalEntry.UserId = GlobalVariables.UserId;

                string _JournalEntryId = loJournalEntry.save(lOperation);
                if (_JournalEntryId != "")
                {
                    for (int i = 0; i < dgvDetail.Rows.Count; i++)
                    {
                        if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                        {
                            loJournalEntryDetail.DetailId = dgvDetail.Rows[i].Cells["Id"].Value.ToString();
                            loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                            loJournalEntryDetail.AccountId = dgvDetail.Rows[i].Cells["AccountId"].Value.ToString();
                            loJournalEntryDetail.Debit = decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                            loJournalEntryDetail.Credit = decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                            loJournalEntryDetail.Subsidiary = dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryId = dgvDetail.Rows[i].Cells["SubsidiaryId"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryDescription = dgvDetail.Rows[i].Cells["Description"].Value.ToString();
                            loJournalEntryDetail.Remarks = dgvDetail.Rows[i].Cells["Remarks"].Value.ToString();
                            loJournalEntryDetail.UserId = GlobalVariables.UserId;
                            loJournalEntryDetail.save(GlobalVariables.Operation.Add);
                        }
                        else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                        {
                            loJournalEntryDetail.DetailId = dgvDetail.Rows[i].Cells["Id"].Value.ToString();
                            loJournalEntryDetail.JournalEntryId = _JournalEntryId;
                            loJournalEntryDetail.AccountId = dgvDetail.Rows[i].Cells["AccountId"].Value.ToString();
                            loJournalEntryDetail.Debit = decimal.Parse(dgvDetail.Rows[i].Cells["Debit"].Value.ToString());
                            loJournalEntryDetail.Credit = decimal.Parse(dgvDetail.Rows[i].Cells["Credit"].Value.ToString());
                            loJournalEntryDetail.Subsidiary = dgvDetail.Rows[i].Cells["Subsidiary"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryId = dgvDetail.Rows[i].Cells["SubsidiaryId"].Value.ToString();
                            loJournalEntryDetail.SubsidiaryDescription = dgvDetail.Rows[i].Cells["Description"].Value.ToString();
                            loJournalEntryDetail.Remarks = dgvDetail.Rows[i].Cells["Remarks"].Value.ToString();
                            loJournalEntryDetail.UserId = GlobalVariables.UserId;
                            loJournalEntryDetail.save(GlobalVariables.Operation.Edit);
                        }
                        else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                        {
                            loJournalEntryDetail.remove(dgvDetail.Rows[i].Cells[0].Value.ToString());
                        }
                    }

                    //insert disbursement detail
                    for (int i = 0; i < dgvDetailDisbursement.Rows.Count; i++)
                    {
                        if (dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Add")
                        {
                            try
                            {
                                loCashDisbursementDetail.DetailId = dgvDetailDisbursement.Rows[i].Cells["DisbursementId"].Value.ToString();
                            }
                            catch
                            {
                                loCashDisbursementDetail.DetailId = "";
                            }
                            try
                            {
                                loCashDisbursementDetail.JournalEntryId = _JournalEntryId;
                                loCashDisbursementDetail.PurchaseOrderId = dgvDetailDisbursement.Rows[i].Cells["DisbursementPOId"].Value.ToString();
                                loCashDisbursementDetail.AmountDue = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementAmountDue"].Value.ToString());
                                loCashDisbursementDetail.PaymentAmount = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementPaymentAmount"].Value.ToString());
                                loCashDisbursementDetail.Balance = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementBalance"].Value.ToString());
                                loCashDisbursementDetail.Remarks = dgvDetailDisbursement.Rows[i].Cells["DisbursementRemarks"].Value.ToString();
                                loCashDisbursementDetail.UserId = GlobalVariables.UserId;
                                loCashDisbursementDetail.save(GlobalVariables.Operation.Add);
                            }
                            catch { }
                        }
                        else if (dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Edit")
                        {
                            try
                            {
                                loCashDisbursementDetail.DetailId = dgvDetailDisbursement.Rows[i].Cells["DisbursementId"].Value.ToString();
                            }
                            catch
                            {
                                loCashDisbursementDetail.DetailId = "";
                            }
                            try
                            {
                                loCashDisbursementDetail.JournalEntryId = _JournalEntryId;
                                loCashDisbursementDetail.PurchaseOrderId = dgvDetailDisbursement.Rows[i].Cells["DisbursementPOId"].Value.ToString();
                                loCashDisbursementDetail.AmountDue = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementAmountDue"].Value.ToString());
                                loCashDisbursementDetail.PaymentAmount = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementPaymentAmount"].Value.ToString());
                                loCashDisbursementDetail.Balance = decimal.Parse(dgvDetailDisbursement.Rows[i].Cells["DisbursementBalance"].Value.ToString());
                                loCashDisbursementDetail.Remarks = dgvDetailDisbursement.Rows[i].Cells["DisbursementRemarks"].Value.ToString();
                                loCashDisbursementDetail.UserId = GlobalVariables.UserId;
                                loCashDisbursementDetail.save(GlobalVariables.Operation.Edit);
                            }
                            catch { }
                        }
                        else if (dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Delete")
                        {
                            loCashDisbursementDetail.remove(dgvDetailDisbursement.Rows[i].Cells[0].Value.ToString());
                        }
                    }

                    //insert check detail
                    for (int i = 0; i < dgvCheckDetails.Rows.Count; i++)
                    {
                        if (dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Add")
                        {
                            try
                            {
                                loCheckDetail.DetailId = dgvCheckDetails.Rows[i].Cells["CheckId"].Value.ToString();
                            }
                            catch
                            {
                                loCheckDetail.DetailId = "";
                            }
                            try
                            {
                                loCheckDetail.JournalEntryId = _JournalEntryId;
                                loCheckDetail.BankId = dgvCheckDetails.Rows[i].Cells["CheckBankId"].Value.ToString();
                                loCheckDetail.CheckNo = dgvCheckDetails.Rows[i].Cells["CheckNo"].Value.ToString();
                                loCheckDetail.CheckAmount = decimal.Parse(dgvCheckDetails.Rows[i].Cells["CheckAmount"].Value.ToString());
                                loCheckDetail.CheckDate = DateTime.Parse(dgvCheckDetails.Rows[i].Cells["CheckDate"].Value.ToString());
                                loCheckDetail.Remarks = dgvCheckDetails.Rows[i].Cells["CheckRemarks"].Value.ToString();
                                loCheckDetail.UserId = GlobalVariables.UserId;
                                loCheckDetail.save(GlobalVariables.Operation.Add);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else if (dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Edit")
                        {
                            try
                            {
                                loCheckDetail.DetailId = dgvCheckDetails.Rows[i].Cells["CheckId"].Value.ToString();
                            }
                            catch
                            {
                                loCheckDetail.DetailId = "";
                            }
                            try
                            {
                                loCheckDetail.JournalEntryId = _JournalEntryId;
                                loCheckDetail.BankId = dgvCheckDetails.Rows[i].Cells["CheckBankId"].Value.ToString();
                                loCheckDetail.CheckNo = dgvCheckDetails.Rows[i].Cells["CheckNo"].Value.ToString();
                                loCheckDetail.CheckAmount = decimal.Parse(dgvCheckDetails.Rows[i].Cells["CheckAmount"].Value.ToString());
                                loCheckDetail.CheckDate = DateTime.Parse(dgvCheckDetails.Rows[i].Cells["CheckDate"].Value.ToString());
                                loCheckDetail.Remarks = dgvCheckDetails.Rows[i].Cells["CheckRemarks"].Value.ToString();
                                loCheckDetail.UserId = GlobalVariables.UserId;
                                loCheckDetail.save(GlobalVariables.Operation.Edit);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else if (dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Delete")
                        {
                            try
                            {
                                loCheckDetail.remove(dgvCheckDetails.Rows[i].Cells[0].Value.ToString());
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    MessageBoxUI _mb = new MessageBoxUI("Journal Entry has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();

                    try
                    {
                        if (lOperation == GlobalVariables.Operation.Edit)
                        {
                            //sendEmailForPosting(_JournalEntryId, cboCompany.SelectedValue.ToString(), cboCompany.Text, string.Format("{0:MM-dd-yyyy}", dtpDatePrepared.Value), txtExplanation.Text, decimal.Parse(txtTotalDebit.Text), decimal.Parse(txtTotalCredit.Text), GlobalVariables.Userfullname);
                        }
                    }
                    catch
                    {
                        MessageBoxUI _mb1 = new MessageBoxUI("Failure to send email", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb1.showDialog();
                    }
                    
                    ParentList.GetType().GetMethod("refresh").Invoke(ParentList, null);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                VoucherDetailUI loVoucherDetail = new VoucherDetailUI();
                loVoucherDetail.ParentList = this;
                loVoucherDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAdd_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Saved" || dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Edit")
                {
                    dgvDetail.CurrentRow.Cells["Status"].Value = "Delete";
                    dgvDetail.CurrentRow.Visible = false;
                }
                else if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Add")
                {
                    if (this.dgvDetail.SelectedRows.Count > 0)
                    {
                        dgvDetail.Rows.RemoveAt(this.dgvDetail.SelectedRows[0].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDelete_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Saved" || dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                    {
                        dgvDetail.Rows[i].Cells["Status"].Value = "Delete";
                        dgvDetail.Rows[i].Visible = false;
                    }
                    else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                    {
                        dgvDetail.Rows.RemoveAt(this.dgvDetail.Rows[i].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAll_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                VoucherDetailUI loVoucherDetail = new VoucherDetailUI(
                    dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["AccountId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Debit"].Value.ToString()), 
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Credit"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["Subsidiary"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["SubsidiaryId"].Value.ToString(), 
                    dgvDetail.CurrentRow.Cells["Description"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loVoucherDetail.ParentList = this;
                loVoucherDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEdit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAddDisbursement_Click(object sender, EventArgs e)
        {
            try
            {
                string _supplierId = "";
                try
                {
                    _supplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Supplier!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                CashDisbursementTransactionDetailUI loCashDisbursementTransactionDetail = new CashDisbursementTransactionDetailUI(_supplierId);
                loCashDisbursementTransactionDetail.ParentList = this;
                loCashDisbursementTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAddDisbursement_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEditDisbursement_Click(object sender, EventArgs e)
        {
            try
            {
                string _supplierId = "";
                try
                {
                    _supplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Supplier!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            
                CashDisbursementTransactionDetailUI loCashDisbursementTransactionDetail = new CashDisbursementTransactionDetailUI(_supplierId,
                    dgvDetailDisbursement.CurrentRow.Cells[0].Value.ToString(),
                    dgvDetailDisbursement.CurrentRow.Cells[1].Value.ToString(),
                    decimal.Parse(dgvDetailDisbursement.CurrentRow.Cells[2].Value.ToString()),
                    decimal.Parse(dgvDetailDisbursement.CurrentRow.Cells[3].Value.ToString()),
                    decimal.Parse(dgvDetailDisbursement.CurrentRow.Cells[4].Value.ToString()),
                    dgvDetailDisbursement.CurrentRow.Cells[5].Value.ToString());
                loCashDisbursementTransactionDetail.ParentList = this;
                loCashDisbursementTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEditDisbursement_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteDisbursement_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value.ToString() == "Saved" ||
                    dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value.ToString() == "Edit")
                {
                    dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value = "Delete";
                    dgvDetailDisbursement.CurrentRow.Visible = false;
                }
                else if (dgvDetailDisbursement.CurrentRow.Cells["DisbursementStatus"].Value.ToString() == "Add")
                {
                    if (this.dgvDetailDisbursement.SelectedRows.Count > 0)
                    {
                        dgvDetailDisbursement.Rows.RemoveAt(this.dgvDetailDisbursement.SelectedRows[0].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteDisbursement_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAllDisbursement_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetailDisbursement.Rows.Count; i++)
                {
                    if (dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Saved" ||
                        dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Edit")
                    {
                        dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value = "Delete";
                        dgvDetailDisbursement.Rows[i].Visible = false;
                    }
                    else if (dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value.ToString() == "Add")
                    {
                        dgvDetailDisbursement.Rows.RemoveAt(this.dgvDetailDisbursement.Rows[i].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAllDisbursement_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAddCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDetailUI loCheckDetail = new CheckDetailUI();
                loCheckDetail.ParentList = this;
                loCheckDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAddCheckDetails_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEditCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDetailUI loCheckDetail = new CheckDetailUI(dgvCheckDetails.CurrentRow.Cells["CheckId"].Value.ToString(),
                        dgvCheckDetails.CurrentRow.Cells["CheckBankId"].Value.ToString(),
                        dgvCheckDetails.CurrentRow.Cells["CheckNo"].Value.ToString(),
                        decimal.Parse(dgvCheckDetails.CurrentRow.Cells["CheckAmount"].Value.ToString()),
                        DateTime.Parse(dgvCheckDetails.CurrentRow.Cells["CheckDate"].Value.ToString()),
                        dgvCheckDetails.CurrentRow.Cells["CheckRemarks"].Value.ToString());
                loCheckDetail.ParentList = this;
                loCheckDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEditCheckDetails_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value.ToString() == "Saved" ||
                    dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value.ToString() == "Edit")
                {
                    dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value = "Delete";
                    dgvCheckDetails.CurrentRow.Visible = false;
                }
                else if (dgvCheckDetails.CurrentRow.Cells["CheckStatus"].Value.ToString() == "Add")
                {
                    if (this.dgvCheckDetails.SelectedRows.Count > 0)
                    {
                        dgvCheckDetails.Rows.RemoveAt(this.dgvCheckDetails.SelectedRows[0].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteCheckDetails_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAllCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvCheckDetails.Rows.Count; i++)
                {
                    if (dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Saved" ||
                        dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Edit")
                    {
                        dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value = "Delete";
                        dgvCheckDetails.Rows[i].Visible = false;
                    }
                    else if (dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value.ToString() == "Add")
                    {
                        dgvCheckDetails.Rows.RemoveAt(this.dgvCheckDetails.Rows[i].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAllCheckDetails_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetail.Rows.Clear();
                dgvDetailDisbursement.Rows.Clear();
                dgvCheckDetails.Rows.Clear();

                string _supplierId = "";
                try
                {
                    _supplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Supplier!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                //insert debit account
                string[] lRecordDebitData = new string[11];
                lRecordDebitData[0] = ""; //detail id
                lRecordDebitData[1] = GlobalVariables.CDDebitAccount;//Account Id
                lRecordDebitData[2] = "";//Account Code
                lRecordDebitData[3] = "";
                foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.CDDebitAccount, "").Rows)
                {
                    lRecordDebitData[2] = _dr["Code"].ToString(); //Account title
                    lRecordDebitData[3] = _dr["Account Title"].ToString(); //Account title
                }
                lRecordDebitData[4] = string.Format("{0:n}", decimal.Parse(txtDisbursement.Text)); //debit
                lRecordDebitData[5] = "0.00"; //credit
                lRecordDebitData[6] = "Supplier"; //subsidiary
                lRecordDebitData[7] = _supplierId; //subsidiary id/code
                lRecordDebitData[8] = cboSupplier.Text;//subsidiary description
                lRecordDebitData[9] = ""; //remarks
                lRecordDebitData[10] = "Add";
                addData(lRecordDebitData);

                //insert credit account
                string[] lRecordCreditData = new string[11];
                lRecordCreditData[0] = ""; //detail id
                lRecordCreditData[1] = GlobalVariables.CDCreditAccount; //Account Id
                lRecordCreditData[2] = ""; // Account Code
                lRecordCreditData[3] = ""; //Account Title
                foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.CDCreditAccount, "").Rows)
                {
                    lRecordCreditData[2] = _dr["Code"].ToString(); // Account Code
                    lRecordCreditData[3] = _dr["Account Title"].ToString(); //Account Title
                }
                lRecordCreditData[4] = "0.00"; //debit
                lRecordCreditData[5] = string.Format("{0:n}", decimal.Parse(txtDisbursement.Text)); //credit
                lRecordCreditData[6] = "Bank"; //subsidiary
                lRecordCreditData[7] = ""; //subsidiary id/code
                lRecordCreditData[8] = ""; //subsidiary description
                lRecordCreditData[9] = ""; //remarks
                lRecordCreditData[10] = "Add";
                addData(lRecordCreditData);

                //insert payment receive
                decimal _paymentbalance = decimal.Parse(txtDisbursement.Text);

                foreach (DataRow _dr in loPurchaseOrder.getCashDisbursementPOBySupplier(_supplierId, "").Rows)
                {
                    if (_paymentbalance != 0)
                    {
                        if (decimal.Parse(_dr["Running Balance"].ToString()) == _paymentbalance)
                        {
                            string[] lRecordPaymentData = new string[7];
                            lRecordPaymentData[0] = ""; //detail
                            lRecordPaymentData[1] = _dr["Id"].ToString(); // sales order id
                            lRecordPaymentData[2] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString())); // amount due
                            lRecordPaymentData[3] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString())); // payment amount
                            lRecordPaymentData[4] = "0.00"; // balance
                            lRecordPaymentData[5] = ""; // remarks
                            lRecordPaymentData[6] = "Add";
                            addDataDisbursement(lRecordPaymentData, true);

                            _paymentbalance = decimal.Parse(_dr["Running Balance"].ToString()) - _paymentbalance;
                        }
                        else if (decimal.Parse(_dr["Running Balance"].ToString()) > _paymentbalance)
                        {
                            string[] lRecordPaymentData = new string[7];
                            lRecordPaymentData[0] = ""; //detail
                            lRecordPaymentData[1] = _dr["Id"].ToString(); // sales order id
                            lRecordPaymentData[2] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString())); // amount due
                            lRecordPaymentData[3] = string.Format("{0:n}", _paymentbalance); // payment amount
                            lRecordPaymentData[4] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString()) - _paymentbalance); // balance
                            lRecordPaymentData[5] = ""; // remarks
                            lRecordPaymentData[6] = "Add";
                            addDataDisbursement(lRecordPaymentData, true);

                            _paymentbalance = 0;
                        }
                        else if (decimal.Parse(_dr["Running Balance"].ToString()) < _paymentbalance)
                        {
                            string[] lRecordPaymentData = new string[7];
                            lRecordPaymentData[0] = ""; //detail
                            lRecordPaymentData[1] = _dr["Id"].ToString(); // sales order id
                            lRecordPaymentData[2] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString())); // amount due
                            lRecordPaymentData[3] = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString())); // payment amount
                            lRecordPaymentData[4] = string.Format("{0:n}", 0); // balance
                            lRecordPaymentData[5] = ""; // remarks
                            lRecordPaymentData[6] = "Add";
                            addDataDisbursement(lRecordPaymentData, true);

                            _paymentbalance = _paymentbalance - decimal.Parse(_dr["Running Balance"].ToString());
                        }
                    }
                }
                if (_paymentbalance > 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("OVER PAYMENT! " + string.Format("{0:n}", _paymentbalance), GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                //insert chech details
                string[] lRecordCheckData = new string[8];
                lRecordCheckData[0] = ""; //detail id
                lRecordCheckData[1] = ""; //bank Id
                lRecordCheckData[2] = ""; // bank name
                lRecordCheckData[3] = ""; //check no.
                lRecordCheckData[4] = string.Format("{0:n}", decimal.Parse(txtDisbursement.Text)); //check amount
                lRecordCheckData[5] = string.Format("{0:MM-dd-yyyy}", DateTime.Now); //check date
                lRecordCheckData[6] = ""; // remarks
                lRecordCheckData[7] = "Add"; //status
                addDataCheck(lRecordCheckData, true);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnProcess_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtDisbursement_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDisbursement.Text = string.Format("{0:n}", decimal.Parse(txtDisbursement.Text));
            }
            catch
            {
                txtDisbursement.Text = "0.00";
            }
        }

        private void CashDisbursementVoucherUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                txtFinancialYear.Text = GlobalVariables.CurrentFinancialYear;

                cboSupplier.DataSource = loSupplier.getAllData("ViewAll", "", "");
                cboSupplier.ValueMember = "Id";
                cboSupplier.DisplayMember = "Name";
                cboSupplier.SelectedIndex = -1;

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loJournalEntry.getAllData("", "", lJournalEntryId, "").Rows)
                    {
                        txtFinancialYear.Text = _dr["F.Y."].ToString();
                        dtpDatePrepared.Value = GlobalFunctions.ConvertToDate(_dr["Date Prepared"].ToString());
                        txtExplanation.Text = _dr["Explanation"].ToString();
                        txtTotalDebit.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Debit"].ToString()));
                        txtTotalCredit.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Credit"].ToString()));
                        txtReference.Text = _dr["Reference"].ToString();
                        cboSupplier.Text = _dr["Supplier"].ToString();
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        foreach (DataRow _drDetails in loJournalEntryDetail.getJournalEntryDetails("", lJournalEntryId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["AccountId"].Value = _drDetails["AccountId"].ToString();
                            dgvDetail.Rows[i].Cells["AccountCode"].Value = _drDetails["Account Code"].ToString();
                            dgvDetail.Rows[i].Cells["AccountTitle"].Value = _drDetails["Account Title"].ToString();
                            dgvDetail.Rows[i].Cells["Debit"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Debit"].ToString()));
                            dgvDetail.Rows[i].Cells["Credit"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Credit"].ToString()));
                            dgvDetail.Rows[i].Cells["Subsidiary"].Value = _drDetails["Subsidiary"].ToString();
                            dgvDetail.Rows[i].Cells["SubsidiaryId"].Value = _drDetails["SubsidiaryId"].ToString();
                            dgvDetail.Rows[i].Cells["Description"].Value = _drDetails["Subsidiary Description"].ToString();
                            dgvDetail.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetail.Rows[i].Cells["Status"].Value = "Saved";
                        }

                        //get disbursement
                        foreach (DataRow _drDetails in loCashDisbursementDetail.getCashDisbursementDetails(lJournalEntryId).Rows)
                        {
                            int i = dgvDetailDisbursement.Rows.Add();
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementId"].Value = _drDetails["Id"].ToString();
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementPOId"].Value = _drDetails["P.O. Id"].ToString();
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementAmountDue"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Amount Due"].ToString()));
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementPaymentAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Payment Amount"].ToString()));
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementBalance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Balance"].ToString()));
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementRemarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetailDisbursement.Rows[i].Cells["DisbursementStatus"].Value = "Saved";
                        }

                        //get check details
                        foreach (DataRow _drDetails in loCheckDetail.getCheckDetails(lJournalEntryId).Rows)
                        {
                            int i = dgvCheckDetails.Rows.Add();
                            dgvCheckDetails.Rows[i].Cells["CheckId"].Value = _drDetails["Id"].ToString();//detail
                            dgvCheckDetails.Rows[i].Cells["CheckBankId"].Value = _drDetails["BankId"].ToString();//bankcode
                            dgvCheckDetails.Rows[i].Cells["CheckBankName"].Value = _drDetails["Bank"].ToString();//bankname
                            dgvCheckDetails.Rows[i].Cells["CheckNo"].Value = _drDetails["Check No."].ToString();//checkno
                            dgvCheckDetails.Rows[i].Cells["CheckAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Check Amount"].ToString()));//check amount
                            dgvCheckDetails.Rows[i].Cells["CheckDate"].Value = string.Format("{0:MM-dd-yyyy}", DateTime.Parse(_drDetails["Check Date"].ToString()));//checkdate
                            dgvCheckDetails.Rows[i].Cells["CheckRemarks"].Value = _drDetails["Remarks"].ToString();
                            dgvCheckDetails.Rows[i].Cells["CheckStatus"].Value = "Saved";
                        }

                        computeTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CashDisbursementVoucherUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetailDisbursement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditDisbursement_Click(null, new EventArgs());
        }

        private void dgvCheckDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditCheckDetails_Click(null, new EventArgs());
        }

        private void pnlBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTotalDebit_TextChanged(object sender, EventArgs e)
        {
            lblJournalEntryAmount.Text = String.Format("{0:n}",decimal.Parse(txtTotalDebit.Text));
        }

        private void txtTotalPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            lblDisbursementsAmount.Text = String.Format("{0:n}", decimal.Parse(txtTotalPaymentAmount.Text));
        }

        private void txtTotalCheckAmount_TextChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Text = String.Format("{0:n}", decimal.Parse(txtTotalCheckAmount.Text));
        }
    }
}
