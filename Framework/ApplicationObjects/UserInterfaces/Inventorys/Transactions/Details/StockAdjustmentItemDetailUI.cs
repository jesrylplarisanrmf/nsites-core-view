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
    public partial class StockAdjustmentItemDetailUI : Form
    {
        Stock loStock;
        Location loLocation;
        
        string[] lRecordData = new string[11];

        string lDetailId;
        string lStockId;
        string lLocationId;
        decimal lQtyIN;
        decimal lQtyOUT;
        string lRemarks;
        string lQtyType;
        string lOperator;


        public StockAdjustmentItemDetailUI(string pQtyType)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = "";
            lQtyType = pQtyType;
            lOperator = "Add";
        }

        public StockAdjustmentItemDetailUI(string pDetailId, string pStockId,
            string pLocationId, decimal pQtyIN, decimal pQtyOUT, string pRemarks, string pQtyType)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            lDetailId = pDetailId;
            lStockId = pStockId;
            lLocationId = pLocationId;
            lQtyIN = pQtyIN;
            lQtyOUT = pQtyOUT;
            lRemarks = pRemarks;
            lQtyType = pQtyType;
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
                txtQtyIN.Text = "0.00";
                txtQtyOUT.Text = "0.00";
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
            txtBalance.Text = string.Format("{0:n}", decimal.Parse(txtOnHand.Text) - decimal.Parse(txtQtyOUT.Text));
        }

        private void StockAdjustmentItemDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboStockDescription.DataSource = loStock.getAllData("ViewAll", "", "");
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

            txtQtyIN.Enabled = false;
            txtQtyOUT.Enabled = false;

            if (lQtyType == "In")
            {
                txtQtyIN.Enabled = true;
                txtQtyIN.Text = "1.00";
                txtQtyOUT.Enabled = false;
                txtQtyOUT.Text = "0.00";
            }
            else if (lQtyType == "Out")
            {
                txtQtyIN.Enabled = false;
                txtQtyIN.Text = "0.00";
                txtQtyOUT.Enabled = true;
                txtQtyOUT.Text = "1.00";
            }
            else if (lQtyType == "In & Out")
            {
                txtQtyIN.Enabled = true;
                txtQtyOUT.Enabled = true;
                txtQtyIN.Text = "0.00";
                txtQtyOUT.Text = "0.00";
            }

            if (lOperator == "Edit")
            {
                cboStockDescription.SelectedValue = lStockId;
                cboLocation.SelectedValue = lLocationId;
                txtQtyIN.Text = string.Format("{0:n}", lQtyIN);
                txtQtyOUT.Text = string.Format("{0:n}", lQtyOUT);
                getQtyOnHand();
                txtRemarks.Text = lRemarks;
            }
            else
            {
                txtOnHand.Text = "0.00";
                txtBalance.Text = "0.00";
            }
        }

        private void cboStockDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loStock.getAllData("",cboStockDescription.SelectedValue.ToString(),"").Rows)
                {
                    txtStockCode.Text = _dr["Code"].ToString();
                    txtUnit.Text = _dr["Unit"].ToString();
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

        private void txtQtyOUT_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQtyOUT.Text = string.Format("{0:n}", decimal.Parse(txtQtyOUT.Text));
            }
            catch
            {
                txtQtyOUT.Text = "0.00";
            }
        }

        private void txtQtyIN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQtyIN.Text) != 0)
                {
                    txtQtyOUT.Enabled = false;
                }
                else
                {
                    txtQtyOUT.Enabled = true;
                }
            }
            catch
            {
                txtQtyOUT.Enabled = true;
            }
            computeTotalQtyOnHand();
        }

        private void txtQtyOUT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQtyOUT.Text) != 0)
                {
                    txtQtyIN.Enabled = false;
                }
                else
                {
                    txtQtyIN.Enabled = true;
                }
            }
            catch
            {
                txtQtyIN.Enabled = true;
            }
            computeTotalQtyOnHand();
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getQtyOnHand();
            computeTotalQtyOnHand();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQtyIN.Text) == 0 && decimal.Parse(txtQtyOUT.Text) == 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                if (decimal.Parse(txtQtyIN.Text) != 0 && decimal.Parse(txtQtyOUT.Text) != 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Only 1 (one) Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
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
                lRecordData[7] = string.Format("{0:n}", decimal.Parse(txtQtyIN.Text));
                lRecordData[8] = string.Format("{0:n}", decimal.Parse(txtQtyOUT.Text));
                lRecordData[9] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData };
                if (lOperator == "Add")
                {
                    lRecordData[10] = "Add";
                    ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[10] = "Edit";
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
