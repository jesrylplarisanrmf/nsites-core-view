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
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Sales;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class CashReceiptVoucherUI : Form
    {
        #region "VARIABLES"
        Customer loCustomer;
        JournalEntry loJournalEntry;
        JournalEntryDetail loJournalEntryDetail;
        CashReceiptDetail loCashReceiptDetail;
        ChartOfAccount loChartOfAccount;
        SalesOrder loSalesOrder;
        GlobalVariables.Operation lOperation;
        string lJournalEntryId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CashReceiptVoucherUI()
        {
            InitializeComponent();
            loCustomer = new Customer();
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashReceiptDetail = new CashReceiptDetail();
            loChartOfAccount = new ChartOfAccount();
            loSalesOrder = new SalesOrder();
            lOperation = GlobalVariables.Operation.Add;
            lJournalEntryId = "";
        }
        public CashReceiptVoucherUI(string pJournalEntryId)
        {
            InitializeComponent();
            loCustomer = new Customer();
            lOperation = GlobalVariables.Operation.Edit;
            loJournalEntry = new JournalEntry();
            loJournalEntryDetail = new JournalEntryDetail();
            loCashReceiptDetail = new CashReceiptDetail();
            loChartOfAccount = new ChartOfAccount();
            loSalesOrder = new SalesOrder();
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

                //payment receipt
                decimal _TotalPaymentAmount = 0;
                for (int i = 0; i < dgvDetailPaymentReceipt.Rows.Count; i++)
                {
                    if (dgvDetailPaymentReceipt.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalPaymentAmount += decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptPaymentAmount"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalPaymentAmount.Text = string.Format("{0:n}", _TotalPaymentAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void sendEmailForPosting(string pJournalEntryId, string pCompanyCode,string pCompany, string pDatePrepared, string pExplanation, decimal pTotalDebit,
            decimal pTotalCredit, string pPayor, string pPreparedBy)
        {
            try
            {
                string _bodyhead = "<h3>Journal Entry Id : " + pJournalEntryId + "</h3>" +
                                "<h4>Form : Official Receipt </h4>" +
                                "<h4>Company : " + pCompany + "</h4>" +
                                "<h4>Date Prepared : " + pDatePrepared + "</h4>" +
                                "<h4>Explanation : " + pExplanation + "</h4>" +
                                "<h4>Total Debit : " + string.Format("{0:n}", pTotalDebit) + "</h4>" +
                                "<h4>Total Credit : " + string.Format("{0:n}", pTotalCredit) + "</h4>" +
                                "<h4>Payor : " + pPayor + "</h4>" +
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

        public void addDataReceipt(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                for (int i = 0; i < dgvDetailPaymentReceipt.Rows.Count; i++)
                {
                    if (dgvDetailPaymentReceipt.Rows[i].Cells[1].Value.ToString() == pRecordData[1])
                    {
                        if(!pFromProcess)
                        {
                            MessageBoxUI _mb = new MessageBoxUI("Duplicate", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb.showDialog();
                            return;
                        }
                    }
                }

                int n = dgvDetailPaymentReceipt.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailPaymentReceipt.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetailPaymentReceipt.CurrentRow.Selected = false;
                dgvDetailPaymentReceipt.FirstDisplayedScrollingRowIndex = dgvDetailPaymentReceipt.Rows[n].Index;
                dgvDetailPaymentReceipt.Rows[n].Selected = true;

                if(!pFromProcess)
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

        public void updateDataReceipt(string[] pRecordData, bool pFromProcess)
        {
            try
            {
                string _operator = dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailPaymentReceipt.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value = "Add";
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
                if (dgvDetailPaymentReceipt.Rows.Count == 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Payment Receipt must a record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
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
                if (decimal.Parse(lblJournalEntryAmount.Text) != decimal.Parse(lblReceiptsAmount.Text))
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Amount is not equal!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                loJournalEntry.JournalEntryId = lJournalEntryId;
                loJournalEntry.FinancialYear = int.Parse(txtFinancialYear.Text);
                loJournalEntry.Journal = "CRJ";
                loJournalEntry.Form = "RV";
                loJournalEntry.VoucherNo = "";
                loJournalEntry.DatePrepared = dtpDatePrepared.Value;
                loJournalEntry.Explanation = GlobalFunctions.replaceChar(txtExplanation.Text);
                loJournalEntry.TotalDebit = decimal.Parse(txtTotalDebit.Text);
                loJournalEntry.TotalCredit = decimal.Parse(txtTotalCredit.Text);
                loJournalEntry.Reference = txtReference.Text;
                loJournalEntry.SupplierId = "";
                try
                {
                    loJournalEntry.CustomerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("Customer must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboCustomer.Focus();
                    return;
                }
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
                    //insert journal entry detail
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
                    //insert payment receipt detail
                    for (int i = 0; i < dgvDetailPaymentReceipt.Rows.Count; i++)
                    {
                        if (dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Add")
                        {
                            try
                            {
                                loCashReceiptDetail.DetailId = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptId"].Value.ToString();
                            }
                            catch
                            {
                                loCashReceiptDetail.DetailId = "";
                            }
                            try
                            {
                                loCashReceiptDetail.JournalEntryId = _JournalEntryId;
                                loCashReceiptDetail.SalesOrderId = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptSOId"].Value.ToString();
                                loCashReceiptDetail.AmountDue = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptAmountDue"].Value.ToString());
                                loCashReceiptDetail.PaymentAmount = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptPaymentAmount"].Value.ToString());
                                loCashReceiptDetail.Balance = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptBalance"].Value.ToString());
                                loCashReceiptDetail.Remarks = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptRemarks"].Value.ToString();
                                loCashReceiptDetail.UserId = GlobalVariables.UserId;
                                loCashReceiptDetail.save(GlobalVariables.Operation.Add);
                            }
                            catch { }
                        }
                        else if (dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Edit")
                        {
                            try
                            {
                                loCashReceiptDetail.DetailId = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptId"].Value.ToString();
                            }
                            catch
                            {
                                loCashReceiptDetail.DetailId = "";
                            }
                            loCashReceiptDetail.JournalEntryId = _JournalEntryId;
                            loCashReceiptDetail.SalesOrderId = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptSOId"].Value.ToString();
                            loCashReceiptDetail.AmountDue = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptAmountDue"].Value.ToString());
                            loCashReceiptDetail.PaymentAmount = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptPaymentAmount"].Value.ToString());
                            loCashReceiptDetail.Balance = decimal.Parse(dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptBalance"].Value.ToString());
                            loCashReceiptDetail.Remarks = dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptRemarks"].Value.ToString();
                            loCashReceiptDetail.UserId = GlobalVariables.UserId;
                            loCashReceiptDetail.save(GlobalVariables.Operation.Edit);

                        }
                        else if (dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Delete")
                        {
                            loCashReceiptDetail.remove(dgvDetailPaymentReceipt.Rows[i].Cells[0].Value.ToString());
                        }
                    }

                    MessageBoxUI _mb = new MessageBoxUI("Journal Entry has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    try
                    {
                        if (lOperation == GlobalVariables.Operation.Edit)
                        {
                            //sendEmailForPosting(_JournalEntryId, cboCompany.SelectedValue.ToString(), cboCompany.Text, string.Format("{0:MM-dd-yyyy}", dtpDatePrepared.Value), txtExplanation.Text, decimal.Parse(txtTotalDebit.Text), decimal.Parse(txtTotalCredit.Text), cboPayor.Text, GlobalVariables.Userfullname);
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

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void dgvDetailPaymentReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditPaymentReceipt_Click(null, new EventArgs());
        }

        private void btnAddPaymentReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                string _customerId = "";
                try
                {
                    _customerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Customer!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                CashReceiptTransactionDetailUI loReceiptTransactionDetail = new CashReceiptTransactionDetailUI(_customerId);
                loReceiptTransactionDetail.ParentList = this;
                loReceiptTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAddPaymentReceipt_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEditPaymentReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                string _customerId = "";
                try
                {
                    _customerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Customer!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                CashReceiptTransactionDetailUI loReceiptTransactionDetail = new CashReceiptTransactionDetailUI(_customerId,
                        dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptId"].Value.ToString(),
                        dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptSOId"].Value.ToString(),
                        decimal.Parse(dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptAmountDue"].Value.ToString()),
                        decimal.Parse(dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptPaymentAmount"].Value.ToString()),
                        decimal.Parse(dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptBalance"].Value.ToString()),
                        dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptRemarks"].Value.ToString());
                loReceiptTransactionDetail.ParentList = this;
                loReceiptTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEditPaymentReceipt_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeletePaymentReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value.ToString() == "Saved" ||
                    dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value.ToString() == "Edit")
                {
                    dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value = "Delete";
                    dgvDetailPaymentReceipt.CurrentRow.Visible = false;
                }
                else if (dgvDetailPaymentReceipt.CurrentRow.Cells["ReceiptStatus"].Value.ToString() == "Add")
                {
                    if (this.dgvDetailPaymentReceipt.SelectedRows.Count > 0)
                    {
                        dgvDetailPaymentReceipt.Rows.RemoveAt(this.dgvDetailPaymentReceipt.SelectedRows[0].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeletePaymentReceipt_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAllPaymentReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetailPaymentReceipt.Rows.Count; i++)
                {
                    if (dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Saved" ||
                        dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Edit")
                    {
                        dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value = "Delete";
                        dgvDetailPaymentReceipt.Rows[i].Visible = false;
                    }
                    else if (dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value.ToString() == "Add")
                    {
                        dgvDetailPaymentReceipt.Rows.RemoveAt(this.dgvDetail.Rows[i].Index);
                    }
                }
                computeTotal();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAllPaymentReceipt_Click");
                em.ShowDialog();
                return;
            }
        }

        private void CashReceiptVoucherUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                txtFinancialYear.Text = GlobalVariables.CurrentFinancialYear;

                cboCustomer.DataSource = loCustomer.getAllData("ViewAll", "", "");
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "Id";
                cboCustomer.SelectedIndex = -1;

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
                        try
                        {
                            cboCustomer.SelectedValue = _dr["CustomerId"].ToString();
                        }
                        catch
                        {
                            cboCustomer.SelectedIndex = -1;
                        }
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        //get journal entry
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
                        //get payment receipt
                        foreach (DataRow _drDetails in loCashReceiptDetail.getCashReceiptDetails(lJournalEntryId).Rows)
                        {
                            int i = dgvDetailPaymentReceipt.Rows.Add();
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptId"].Value = _drDetails["Id"].ToString();
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptSOId"].Value = _drDetails["S.O. Id"].ToString();
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptAmountDue"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Amount Due"].ToString()));
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptPaymentAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Payment Amount"].ToString()));
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptBalance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Balance"].ToString()));
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptRemarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetailPaymentReceipt.Rows[i].Cells["ReceiptStatus"].Value = "Saved";
                        }
                        computeTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CashReceiptVoucherUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetail.Rows.Clear();
                dgvDetailPaymentReceipt.Rows.Clear();
                string _customerId = "";
                try
                {
                    _customerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Customer!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }

                //insert debit account
                string[] lRecordDebitData = new string[11];
                lRecordDebitData[0] = ""; //detail id
                lRecordDebitData[1] = GlobalVariables.CRDebitAccount; //coa id
                lRecordDebitData[2] = "";
                lRecordDebitData[3] = "";
                foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.CRDebitAccount, "").Rows)
                {
                    lRecordDebitData[2] = _dr["Code"].ToString(); //coa code
                    lRecordDebitData[3] = _dr["Account Title"].ToString(); //coa title
                }
                lRecordDebitData[4] = string.Format("{0:n}", decimal.Parse(txtPaymentReceipt.Text)); //debit
                lRecordDebitData[5] = "0.00"; //credit
                lRecordDebitData[6] = "Bank"; //subsidiary
                lRecordDebitData[7] = ""; //subsidiary id/code
                lRecordDebitData[8] = ""; //subsidiary description
                lRecordDebitData[9] = ""; //remarks
                lRecordDebitData[10] = "Add";
                addData(lRecordDebitData);

                //insert credit account
                string[] lRecordCreditData = new string[11];
                lRecordCreditData[0] = ""; //detail id
                lRecordCreditData[1] = GlobalVariables.CRCreditAccount; //coa id
                lRecordCreditData[2] = "";
                lRecordCreditData[3] = "";
                foreach (DataRow _dr in loChartOfAccount.getAllData("", GlobalVariables.CRCreditAccount, "").Rows)
                {
                    lRecordCreditData[2] = _dr["Code"].ToString(); //coa code
                    lRecordCreditData[3] = _dr["Account Title"].ToString(); //coa title
                }
                lRecordCreditData[4] = "0.00"; //debit
                lRecordCreditData[5] = string.Format("{0:n}", decimal.Parse(txtPaymentReceipt.Text)); //credit
                lRecordCreditData[6] = "Customer"; //subsidiary
                lRecordCreditData[7] = _customerId; //subsidiary id/code
                lRecordCreditData[8] = cboCustomer.Text; //subsidiary description
                lRecordCreditData[9] = ""; //remarks
                lRecordCreditData[10] = "Add";
                addData(lRecordCreditData);

                //insert payment receive
                decimal _paymentbalance = decimal.Parse(txtPaymentReceipt.Text);

                foreach (DataRow _dr in loSalesOrder.getCashReceiptSOByCustomer(_customerId, "").Rows)
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
                            addDataReceipt(lRecordPaymentData, true);

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
                            addDataReceipt(lRecordPaymentData, true);

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
                            addDataReceipt(lRecordPaymentData, true);

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
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnProcess_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtPaymentReceipt_Leave(object sender, EventArgs e)
        {
            try
            {
                txtPaymentReceipt.Text = string.Format("{0:n}", decimal.Parse(txtPaymentReceipt.Text));
            }
            catch
            {
                txtPaymentReceipt.Text = "0.00";
            }
        }

        private void txtTotalDebit_TextChanged(object sender, EventArgs e)
        {
            lblJournalEntryAmount.Text = String.Format("{0:n}", decimal.Parse(txtTotalDebit.Text));
        }

        private void txtTotalPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            lblReceiptsAmount.Text = String.Format("{0:n}", decimal.Parse(txtTotalPaymentAmount.Text));
        }
    }
}
