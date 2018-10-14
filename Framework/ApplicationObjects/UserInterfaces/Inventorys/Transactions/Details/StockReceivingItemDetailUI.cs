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

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class StockReceivingItemDetailUI : Form
    {
        Stock loStock;
        Location loLocation;

        string[] lRecordData = new string[13];

        string lDetailId;
        string lPODetailId;
        string lStockId;
        string lLocationId;
        decimal lPOQty;
        decimal lQtyIN;
        decimal lVariance;
        string lRemarks;
        string lOperator;
        
        public StockReceivingItemDetailUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = "";
            lPODetailId = "0";
            lOperator = "Add";
        }

        public StockReceivingItemDetailUI(string pDetailId, string pPODetailId, string pStockId,
            string pLocationId, decimal pPOQty, decimal pQtyIN, decimal pVariance, string pRemarks)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = pDetailId;
            lPODetailId = pPODetailId;
            lStockId = pStockId;
            lLocationId = pLocationId;
            lPOQty = pPOQty;
            lQtyIN = pQtyIN;
            lVariance = pVariance;
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
                cboLocation.SelectedValue = GlobalVariables.CurrentLocationId;
                txtPOQty.Text = "0.00";
                txtQtyIN.Text = "1.00";
                txtVariance.Text = "0.00";
                txtOnHand.Text = "0.00";
                txtBalance.Text = "0.00";
                txtRemarks.Clear();
                cboStockDescription.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getQtyOnHand()
        {
            try
            {
                foreach (DataRow _dr in loStock.getStockQtyOnHand(cboLocation.SelectedValue.ToString(), cboStockDescription.SelectedValue.ToString()).Rows)
                {
                    txtOnHand.Text = string.Format("{0:n}", decimal.Parse(_dr[0].ToString()));
                }
            }
            catch
            {
                txtOnHand.Text = "0.00";
            }
            computeTotalQtyOnHand();
        }

        private void computeTotalQtyOnHand()
        {
            txtBalance.Text = string.Format("{0:n}", decimal.Parse(txtOnHand.Text) - decimal.Parse(txtQtyIN.Text));
        }

        private void StockReceivingItemDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboStockDescription.DataSource = loStock.getAllData("ViewAll", "", "");
                cboStockDescription.DisplayMember = "Description";
                cboStockDescription.ValueMember = "Id";
                cboStockDescription.SelectedIndex = -1;

                cboLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                cboLocation.DisplayMember = "Description";
                cboLocation.ValueMember = "Id";
                cboLocation.SelectedIndex = 0;

                cboLocation.SelectedValue = GlobalVariables.CurrentLocationId;

                if (lOperator == "Edit")
                {
                    cboStockDescription.SelectedValue = lStockId;
                    cboLocation.SelectedValue = lLocationId;
                    txtPOQty.Text = string.Format("{0:n}", lPOQty);
                    txtQtyIN.Text = string.Format("{0:n}", lQtyIN);
                    txtVariance.Text = string.Format("{0:n}", lVariance);
                    getQtyOnHand();
                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    txtQtyIN.Text = "1.00";
                    txtOnHand.Text = "0.00";
                    txtBalance.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockReceivingItemDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void cboStockDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loStock.getAllData("", cboStockDescription.SelectedValue.ToString(), "").Rows)
                {
                    txtStockCode.Text = _dr["Code"].ToString();
                    txtUnit.Text = _dr["Unit"].ToString();
                    //txtSOQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    getQtyOnHand();
                }
            }
            catch
            { }
        }

        private void txtQtyIN_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQtyIN.Text = string.Format("{0:n}", decimal.Parse(txtQtyIN.Text));
            }
            catch
            {
                txtQtyIN.Text = "0.00";
            }
        }

        private void txtQtyIN_TextChanged(object sender, EventArgs e)
        {
            computeTotalQtyOnHand();
            try
            {
                txtVariance.Text = string.Format("{0:n}", decimal.Parse(txtPOQty.Text) - decimal.Parse(txtQtyIN.Text));
            }
            catch
            {
                txtVariance.Text = "0.00";
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
                if (decimal.Parse(txtQtyIN.Text) == 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                lRecordData[0] = lDetailId;
                lRecordData[1] = lPODetailId;
                try
                {
                    lRecordData[2] = cboStockDescription.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Stock!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    cboStockDescription.Focus();
                    return;
                }
                lRecordData[3] = txtStockCode.Text;
                lRecordData[4] = cboStockDescription.Text;
                lRecordData[5] = txtUnit.Text;
                try
                {
                    lRecordData[6] = cboLocation.SelectedValue.ToString();
                }
                catch
                {
                    lRecordData[6] = "";
                }
                lRecordData[7] = cboLocation.Text;
                lRecordData[8] = string.Format("{0:n}", decimal.Parse(txtPOQty.Text));
                lRecordData[9] = string.Format("{0:n}", decimal.Parse(txtQtyIN.Text));
                lRecordData[10] = string.Format("{0:n}", decimal.Parse(txtVariance.Text));
                lRecordData[11] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData };
                if (lOperator == "Add")
                {
                    lRecordData[12] = "Add";
                    ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                    MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[12] = "Edit";
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
        
    }
}
