using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Sales;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class CashReceiptTransactionDetailUI : Form
    {
        SalesOrder loSalesOrder;

        string[] lRecordData = new string[7];
        string lCustomerId;

        string lDetailId;
        string lSalesOrderId;
        decimal lAmountDue;
        decimal lPaymentAmount;
        decimal lBalance;
        string lRemarks;
        string lOperator;
        
        public CashReceiptTransactionDetailUI(string pCustomerId)
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            lCustomerId = pCustomerId;
            lDetailId = "";
            lOperator = "Add";
        }

        public CashReceiptTransactionDetailUI(string pCustomerId, string pDetailId, string pSalesOrderId, decimal pAmountDue, decimal pPaymentAmount,
            decimal pBalance, string pRemarks)
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            lCustomerId = pCustomerId;
            lDetailId = pDetailId;
            lSalesOrderId = pSalesOrderId;
            lAmountDue = pAmountDue;
            lPaymentAmount = pPaymentAmount;
            lBalance = pBalance;
            lRemarks = pRemarks;
            lOperator = "Edit";
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void clear()
        {
            try
            {
                cboSalesOrder.SelectedIndex = -1;
                txtAmountDue.Text = "0.00";
                txtPaymentAmount.Text = "0.00";
                txtBalance.Text = "0.00";
                txtRemarks.Clear();
                cboSalesOrder.Focus();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void computeBalance()
        {
            try
            {
                txtBalance.Text = string.Format("{0:n}", decimal.Parse(txtAmountDue.Text) - decimal.Parse(txtPaymentAmount.Text));
            }
            catch
            {
                txtBalance.Text = "0.00";
            }
        }
        
        private void ReceiptTransactionDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboSalesOrder.DataSource = loSalesOrder.getCashReceiptSOByCustomer(lCustomerId, "");
                    cboSalesOrder.DisplayMember = "Id";
                    cboSalesOrder.ValueMember = "Id";
                    cboSalesOrder.SelectedIndex = -1;
                }
                catch { }

                if (lOperator == "Edit")
                {
                    cboSalesOrder.SelectedValue = lSalesOrderId;
                    cboSalesOrder.Enabled = true;
                    txtAmountDue.Text = string.Format("{0:n}", lAmountDue);
                    txtPaymentAmount.Text = string.Format("{0:n}", lPaymentAmount);
                    txtBalance.Text = string.Format("{0:n}", lBalance);
                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    cboSalesOrder.SelectedIndex = -1;
                    txtAmountDue.Text = "0.00";
                    txtPaymentAmount.Text = "0.00";
                    txtBalance.Text = "0.00";
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtPaymentAmount.Text) == 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Payment Amount must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                lRecordData[0] = lDetailId;
                try
                {
                    lRecordData[1] = cboSalesOrder.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Sales Order Id!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    cboSalesOrder.Focus();
                    return;
                }
                lRecordData[2] = string.Format("{0:n}", decimal.Parse(txtAmountDue.Text));
                lRecordData[3] = string.Format("{0:n}", decimal.Parse(txtPaymentAmount.Text));
                lRecordData[4] = string.Format("{0:n}", decimal.Parse(txtBalance.Text));
                lRecordData[5] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData, false };
                if (lOperator == "Add")
                {
                    lRecordData[6] = "Add";
                    ParentList.GetType().GetMethod("addDataReceipt").Invoke(ParentList, _params);
                    //MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    //_mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[6] = "Edit";
                    ParentList.GetType().GetMethod("updateDataReceipt").Invoke(ParentList, _params);
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

        private void txtPaymentAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtPaymentAmount.Text = string.Format("{0:n}", decimal.Parse(txtPaymentAmount.Text));
            }
            catch
            {
                txtPaymentAmount.Text = "0.00";
            }
        }

        private void cboSalesOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _SOId = "";
                try
                {
                    _SOId = cboSalesOrder.SelectedValue.ToString();
                }
                catch { }
                if (_SOId != "")
                {
                    foreach (DataRow _dr in loSalesOrder.getAllData("", _SOId, "").Rows)
                    {
                        txtAmountDue.Text = string.Format("{0:n}", decimal.Parse(_dr["Running Balance"].ToString()));
                        computeBalance();
                    }
                } 
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "cboSalesOrder_SelectedIndexChanged");
                em.ShowDialog();
                return;
            }
        }

        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                computeBalance();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtPaymentAmount_TextChanged");
                em.ShowDialog();
                return;
            }
        }

        private void btnLookUpSalesOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
