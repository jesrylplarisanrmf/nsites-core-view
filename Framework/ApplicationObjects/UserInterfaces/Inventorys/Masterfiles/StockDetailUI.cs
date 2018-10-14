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
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Inventorys;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles
{
    public partial class StockDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[13];
        GlobalVariables.Operation lOperation;
        Stock loStock;
        Category loCategory;
        Unit loUnit;
        
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loStock = new Stock();
            loCategory = new Category();
            loUnit = new Unit();
        }
        public StockDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loStock = new Stock();
            loCategory = new Category();
            loUnit = new Unit();
            lRecords = pRecords;
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
            lId = "";
            txtCode.Clear();
            txtDescription.Clear();
            cboCategory.SelectedIndex = -1;
            cboUnit.SelectedIndex = -1;
            txtUnitCost.Text = "0.00";
            txtBasePrice.Text = "0.00";
            txtUnitPrice.Text = "0.00";
            txtReorderLevel.Text = "0.00";
            chkActive.Checked = true;
            chkSaleable.Checked = true;
            chkNonInventory.Checked = false;
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void txtUnitCost_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitCost.Text = string.Format("{0:n}", decimal.Parse(txtUnitCost.Text)).ToString();
            }
            catch
            {
                txtUnitCost.Text = "0.00";
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text)).ToString();
            }
            catch
            {
                txtUnitPrice.Text = "0.00";
            }
        }

        private void txtReorderLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                txtReorderLevel.Text = string.Format("{0:n}", decimal.Parse(txtReorderLevel.Text)).ToString();
            }
            catch
            {
                txtReorderLevel.Text = "0.00";
            }
        }

        private void StockDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboCategory.DataSource = loCategory.getAllData("ViewAll", "", "");
                    cboCategory.DisplayMember = "Description";
                    cboCategory.ValueMember = "Id";
                    cboCategory.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboUnit.DataSource = loUnit.getAllData("ViewAll", "", "");
                    cboUnit.DisplayMember = "Description";
                    cboUnit.ValueMember = "Id";
                    cboUnit.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtDescription.Text = lRecords[2];
                    cboCategory.Text = lRecords[3];
                    cboUnit.Text = lRecords[4];
                    txtUnitCost.Text = string.Format("{0:n}", decimal.Parse(lRecords[5]));
                    txtBasePrice.Text = string.Format("{0:n}", decimal.Parse(lRecords[6]));
                    txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(lRecords[7]));
                    txtReorderLevel.Text = string.Format("{0:n}", decimal.Parse(lRecords[8]));
                    chkActive.Checked = lRecords[9] == "Y" ? true : false;
                    chkSaleable.Checked = lRecords[10] == "Y" ? true : false;
                    chkNonInventory.Checked = lRecords[11] == "Y" ? true : false;
                    txtRemarks.Text = lRecords[12];
                }
                else
                {
                    chkActive.Checked = true;
                    chkSaleable.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lOperation == GlobalVariables.Operation.Add)
                {
                    if (loStock.getStocksByCode(txtCode.Text).Rows.Count > 0)
                    {
                        MessageBoxUI _mb = new MessageBoxUI("Stock Code already exist!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb.showDialog();
                        txtCode.Focus();
                        return;
                    }
                }
                else if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loStock.getStocksByCode(txtCode.Text).Rows)
                    {
                        if (_dr["Id"].ToString() != lId)
                        {
                            MessageBoxUI _mb = new MessageBoxUI("Stock Code already exist!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb.showDialog();
                            txtCode.Focus();
                            return;
                        }
                    }
                }

                loStock.Id = lId;
                loStock.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loStock.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                try
                {
                    loStock.CategoryId = cboCategory.SelectedValue.ToString();
                }
                catch
                {
                    loStock.CategoryId = "";
                }
                try
                {
                    loStock.UnitId = cboUnit.SelectedValue.ToString();
                }
                catch
                {
                    loStock.UnitId = "";
                }
                loStock.UnitCost = decimal.Parse(txtUnitCost.Text);
                loStock.BasePrice = decimal.Parse(txtBasePrice.Text);
                loStock.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                loStock.ReorderLevel = decimal.Parse(txtReorderLevel.Text);
                loStock.Active = chkActive.Checked ? "Y" : "N";
                loStock.Saleable = chkSaleable.Checked ? "Y" : "N";
                loStock.NonInventory = chkNonInventory.Checked ? "Y" : "N";
                loStock.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loStock.UserId = GlobalVariables.UserId;

                string _Id = loStock.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Stock has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = cboCategory.Text;
                    lRecords[4] = cboUnit.Text;
                    lRecords[5] = decimal.Parse(txtUnitCost.Text).ToString();
                    lRecords[6] = decimal.Parse(txtBasePrice.Text).ToString();
                    lRecords[7] = decimal.Parse(txtUnitPrice.Text).ToString();
                    lRecords[8] = decimal.Parse(txtReorderLevel.Text).ToString();
                    lRecords[9] = chkActive.Checked ? "Y" : "N";
                    lRecords[10] = chkSaleable.Checked ? "Y" : "N";
                    lRecords[11] = chkNonInventory.Checked ? "Y" : "N";
                    lRecords[12] = txtRemarks.Text;
                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                    }
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtBasePrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBasePrice.Text = string.Format("{0:n}", decimal.Parse(txtBasePrice.Text)).ToString();
            }
            catch
            {
                txtBasePrice.Text = "0.00";
            }
        }
    }
}
