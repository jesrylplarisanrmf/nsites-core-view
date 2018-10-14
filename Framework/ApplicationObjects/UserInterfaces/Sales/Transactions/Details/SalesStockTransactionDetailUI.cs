using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Sales;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details
{
    public partial class SalesStockTransactionDetailUI : Form
    {
        Stock loStock;
        Location loLocation;
        SalesDiscount loSalesDiscount;

        string[] lRecordData = new string[17];

        string lDetailId;
        string lStockId;
        string lLocationId;
        decimal lSOQty;
        decimal lQtyOut;
        decimal lQtyVariance;
        decimal lUnitPrice;
        string lDiscountId;
        decimal lDiscountAmount;
        decimal lTotalPrice;
        string lRemarks;
        string lOperator;
        
        public SalesStockTransactionDetailUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            loSalesDiscount = new SalesDiscount();
            lDetailId = "";
            lOperator = "Add";
        }

        public SalesStockTransactionDetailUI(string pDetailId, string pStockId,
            string pLocationId, decimal pSOQty, decimal pQtyOut, decimal pQtyVariance, decimal pUnitPrice,
            string pDiscountId, decimal pDiscountAmount, decimal pTotalPrice, string pRemarks)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            loSalesDiscount = new SalesDiscount(); 
            lDetailId = pDetailId;
            lStockId = pStockId;
            lLocationId = pLocationId;
            lSOQty = pSOQty;
            lQtyOut = pQtyOut;
            lQtyVariance = pQtyVariance;
            lUnitPrice = pUnitPrice;
            lDiscountId = pDiscountId;
            lDiscountAmount = pDiscountAmount;
            lTotalPrice = pTotalPrice;
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
            cboStockDescription.SelectedIndex = -1;
            cboStockDescription.Text = "";
            txtStockCode.Clear();
            txtUnit.Clear();
            cboLocation.SelectedValue = GlobalVariables.CurrentLocationId;
            txtSOQty.Text = "1.00";
            txtQtyOut.Text = "0.00";
            txtQtyVariance.Text = "1.00";
            txtQtyOnHand.Text = "0.00";
            txtUnitPrice.Text = "0.00";
            cboDiscount.SelectedIndex = -1;
            txtDiscountAmount.Text = "0.00";
            txtTotalPrice.Text = "0.00";
            txtRemarks.Clear();
            cboStockDescription.Focus();
        }

        private void computeTotalPrice()
        {
            try
            {
                //compute discount
                decimal _totalPrice = 0;
                decimal _qty = 0;
                decimal _discountAmount = 0;
                if (decimal.Parse(txtSOQty.Text) != 0)
                {
                    _qty = decimal.Parse(txtSOQty.Text);
                }
                else
                {
                    _qty = 0;
                }

                _totalPrice = _qty * decimal.Parse(txtUnitPrice.Text);

                try
                {
                    foreach (DataRow _dr in loSalesDiscount.getAllData("", cboDiscount.SelectedValue.ToString(), "").Rows)
                    {
                        if (_dr["Type"].ToString() == "Percentage")
                        {
                            _discountAmount = _totalPrice * (decimal.Parse(_dr["Value"].ToString())) / 100;
                            txtDiscountAmount.Text = string.Format("{0:n}", _discountAmount);
                        }
                        else
                        {
                            _discountAmount = decimal.Parse(txtDiscountAmount.Text);
                        }
                    }
                }
                catch
                {
                    _discountAmount = 0;
                }
                
                txtTotalPrice.Text = string.Format("{0:n}", _totalPrice - _discountAmount);
            }
            catch
            {
                txtTotalPrice.Text = "0.00";
            }
        }

        private void getQtyOnHand()
        {
            try
            {
                txtQtyOnHand.Text = "0.00";
                foreach (DataRow _dr in loStock.getStockQtyOnHand(cboLocation.SelectedValue.ToString(), cboStockDescription.SelectedValue.ToString()).Rows)
                {
                    txtQtyOnHand.Text = string.Format("{0:n}", decimal.Parse(_dr[0].ToString()));
                }
            }
            catch
            {
                txtQtyOnHand.Text = "0.00";
            }
        }

        private void SalesStockTransactionDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                try
                {
                    cboStockDescription.DataSource = loStock.getSaleableStocks();
                    cboStockDescription.DisplayMember = "Description";
                    cboStockDescription.ValueMember = "Id";
                    cboStockDescription.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                    cboLocation.DisplayMember = "Description";
                    cboLocation.ValueMember = "Id";
                    cboLocation.SelectedIndex = 0;
                }
                catch { }

                cboLocation.SelectedValue = GlobalVariables.CurrentLocationId;

                txtDiscountAmount.Enabled = false;
                try
                {
                    cboDiscount.DataSource = loSalesDiscount.getAllData("ViewAll", "", "");
                    cboDiscount.DisplayMember = "Description";
                    cboDiscount.ValueMember = "Id";
                    cboDiscount.SelectedIndex = -1;
                }
                catch { }

                if (lOperator == "Edit")
                {
                    cboStockDescription.SelectedValue = lStockId;
                    cboLocation.SelectedValue = lLocationId;
                    txtSOQty.Text = string.Format("{0:n}", lSOQty);
                    txtQtyOut.Text = string.Format("{0:n}", lQtyOut);
                    txtQtyVariance.Text = string.Format("{0:n}", lQtyVariance);
                    txtUnitPrice.Text = string.Format("{0:n}", lUnitPrice);
                    txtDiscountAmount.Text = string.Format("{0:n}", lDiscountAmount);
                    txtTotalPrice.Text = string.Format("{0:n}", lTotalPrice);
                    try
                    {
                        cboDiscount.SelectedValue = lDiscountId;
                    }
                    catch
                    {
                        cboDiscount.SelectedIndex = -1;
                    }

                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    txtSOQty.Text = "1.00";
                    txtQtyOnHand.Text = "0.00";
                }
            }
            catch { }
        }

        private void cboStockDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loStock.getAllData("",cboStockDescription.SelectedValue.ToString(),"").Rows)
                {
                    txtStockCode.Text = _dr["Code"].ToString();
                    txtUnit.Text = _dr["Unit"].ToString();
                    txtBasePrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Base Price"].ToString()));
                    txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    computeTotalPrice();
                    getQtyOnHand();
                }
            }
            catch
            { }
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                txtSOQty.Text = string.Format("{0:n}", decimal.Parse(txtSOQty.Text));
            }
            catch
            {
                txtSOQty.Text = "0.00";
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            computeTotalPrice();
            try
            {
                txtQtyVariance.Text = string.Format("{0:n}", decimal.Parse(txtSOQty.Text) - decimal.Parse(txtQtyOut.Text));
            }
            catch
            {
                txtQtyVariance.Text = "0.00";
            }
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getQtyOnHand();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtUnitPrice.Text) < decimal.Parse(txtBasePrice.Text))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Unit Price must not be lesser than Base Price!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    txtUnitPrice.Focus();
                    return;
                }

                if (decimal.Parse(txtTotalPrice.Text) <= 0)
                {
                    MessageBoxUI _mb = new MessageBoxUI("Total Price must not be lesser than or equal to Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    txtUnitPrice.Focus();
                    return;
                }

                if (decimal.Parse(txtSOQty.Text) == 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                lRecordData[0] = lDetailId;
                try
                {
                    lRecordData[1] = cboStockDescription.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Stock!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    cboStockDescription.Focus();
                    return;
                }
                lRecordData[2] = txtStockCode.Text;//stock code
                lRecordData[3] = cboStockDescription.Text;
                lRecordData[4] = txtUnit.Text;
                
                try
                {
                    lRecordData[5] = cboLocation.SelectedValue.ToString();
                }
                catch
                {
                    lRecordData[5] = "";
                }
                lRecordData[6] = cboLocation.Text;
                lRecordData[7] = string.Format("{0:n}", decimal.Parse(txtSOQty.Text));
                lRecordData[8] = string.Format("{0:n}", decimal.Parse(txtQtyOut.Text));
                lRecordData[9] = string.Format("{0:n}", decimal.Parse(txtQtyVariance.Text));
                lRecordData[10] = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
                try
                {
                    lRecordData[11] = cboDiscount.SelectedValue.ToString();
                    lRecordData[12] = cboDiscount.Text;
                }
                catch
                {
                    lRecordData[11] = "";
                    lRecordData[12] = "";
                }

                lRecordData[13] = string.Format("{0:n}", decimal.Parse(txtDiscountAmount.Text));
                lRecordData[14] = string.Format("{0:n}", decimal.Parse(txtTotalPrice.Text));
                lRecordData[15] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData };
                if (lOperator == "Add")
                {
                    lRecordData[16] = "Add";
                    ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                    //MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    //_mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[16] = "Edit";
                    ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
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

        private void cboDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDiscount.SelectedIndex >= 0)
            {
                txtDiscountAmount.Enabled = true;
            }
            else
            {
                txtDiscountAmount.Enabled = false;
            }
            try
            {
                foreach (DataRow _dr in loSalesDiscount.getAllData("", cboDiscount.SelectedValue.ToString(), "").Rows)
                {
                    if (_dr["Type"].ToString() == "Percentage")
                    {
                        txtDiscountAmount.Enabled = false;
                    }
                    else
                    {
                        txtDiscountAmount.Enabled = true;
                        txtDiscountAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Value"].ToString()));
                    }
                }
            }
            catch
            { }
            computeTotalPrice();
        }

        private void txtDiscountAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDiscountAmount.Text = string.Format("{0:n}", decimal.Parse(txtDiscountAmount.Text));
            }
            catch
            {
                txtDiscountAmount.Text = "0.00";
            }
        }

        private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                computeTotalPrice();
            }
            catch { }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            computeTotalPrice();
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtUnitPrice.Text = "0.00";
            }
        }

        private void btnCancelDiscount_Click(object sender, EventArgs e)
        {
            cboDiscount.SelectedIndex = -1;
            txtDiscountAmount.Enabled = false;
            txtDiscountAmount.Text = "0.00";
            computeTotalPrice();
        }
    }
}
