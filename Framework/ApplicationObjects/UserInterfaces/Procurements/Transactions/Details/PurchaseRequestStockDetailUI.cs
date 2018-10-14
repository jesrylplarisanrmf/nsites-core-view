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

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details
{
    public partial class PurchaseRequestStockDetailUI : Form
    {
        Stock loStock;
        Location loLocation;

        string[] lRecordData = new string[12];

        string lDetailId;
        string lStockId;
        string lLocationId;
        decimal lQty;
        decimal lUnitPrice;
        decimal lTotalPrice;
        string lRemarks;
        string lOperator;
        
        public PurchaseRequestStockDetailUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = "";
            lOperator = "Add";
        }

        public PurchaseRequestStockDetailUI(string pDetailId, string pStockId, 
            string pLocationId, decimal pQty, decimal pUnitPrice, decimal pTotalPrice, string pRemarks)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = pDetailId;
            lStockId = pStockId;
            lLocationId = pLocationId;
            lQty = pQty;
            lUnitPrice = pUnitPrice;
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
            try
            {
                cboStockDescription.SelectedIndex = -1;
                cboStockDescription.Text = "";
                txtStockCode.Clear();
                txtUnit.Clear();
                cboLocation.SelectedIndex = 0;
                txtQty.Text = "1.00";
                txtQtyOnHand.Text = "0.00";
                txtUnitPrice.Text = "0.00";
                txtTotalPrice.Text = "0.00";
                txtRemarks.Clear();
                cboStockDescription.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void computeTotalPrice()
        {
            try
            {
                //compute discount
                decimal _totalPrice = 0;
                decimal _qty = 0;
                if (decimal.Parse(txtQty.Text) != 0)
                {
                    _qty = decimal.Parse(txtQty.Text);
                }
                else
                {
                    _qty = 0;
                }

                _totalPrice = _qty * decimal.Parse(txtUnitPrice.Text);

                txtTotalPrice.Text = string.Format("{0:n}", _totalPrice);
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

        private void PurchaseRequestStockDetailUI_Load(object sender, EventArgs e)
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
                //MessageBox.Show(lStockId+"-"+lLocationId+"-"+lQty+"-"+lUnitPrice+"-"+lTotalPrice);

                if (lOperator == "Edit")
                {
                    cboStockDescription.SelectedValue = lStockId;
                    cboLocation.SelectedValue = lLocationId;
                    txtQty.Text = string.Format("{0:n}", lQty);
                    txtUnitPrice.Text = string.Format("{0:n}", lUnitPrice);
                    txtTotalPrice.Text = string.Format("{0:n}", lTotalPrice);
                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    txtQty.Text = "1.00";
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
                txtQty.Text = string.Format("{0:n}", decimal.Parse(txtQty.Text));
            }
            catch
            {
                txtQty.Text = "0.00";
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            computeTotalPrice();
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getQtyOnHand();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQty.Text) == 0)
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
                lRecordData[2] = txtStockCode.Text;
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
                lRecordData[7] = string.Format("{0:n}", decimal.Parse(txtQty.Text));
                lRecordData[8] = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
                lRecordData[9] = string.Format("{0:n}", decimal.Parse(txtTotalPrice.Text));
                lRecordData[10] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData };
                if (lOperator == "Add")
                {
                    lRecordData[11] = "Add";
                    ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                    //MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    //_mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[11] = "Edit";
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

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            computeTotalPrice();
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQty.Text = string.Format("{0:n}", decimal.Parse(txtQty.Text));
            }
            catch
            {
                txtQty.Text = "0.00";
            }
        }
    }
}
