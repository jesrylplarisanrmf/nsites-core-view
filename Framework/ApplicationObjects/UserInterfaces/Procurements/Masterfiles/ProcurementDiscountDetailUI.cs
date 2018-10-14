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
using NSites_V.ApplicationObjects.Classes.Procurements;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Masterfiles
{
    public partial class ProcurementDiscountDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[6];
        GlobalVariables.Operation lOperation;
        ProcurementDiscount loProcurementDiscount;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ProcurementDiscountDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loProcurementDiscount = new ProcurementDiscount();
        }
        public ProcurementDiscountDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loProcurementDiscount = new ProcurementDiscount();
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
            cboType.SelectedIndex = -1;
            txtValue.Text = "0.00";
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loProcurementDiscount.Id = lId;
                loProcurementDiscount.Code = GlobalFunctions.replaceChar(txtCode.Text);
                loProcurementDiscount.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loProcurementDiscount.Type = cboType.Text;
                loProcurementDiscount.Value = decimal.Parse(txtValue.Text);
                loProcurementDiscount.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loProcurementDiscount.UserId = GlobalVariables.UserId;

                string _Id = loProcurementDiscount.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Procurement Discount has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = txtDescription.Text;
                    lRecords[3] = cboType.Text;
                    lRecords[4] = decimal.Parse(txtValue.Text).ToString();
                    lRecords[5] = txtRemarks.Text;
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

        private void txtValue_Leave(object sender, EventArgs e)
        {
            try
            {
                txtValue.Text = string.Format("{0:n}", decimal.Parse(txtValue.Text));
            }
            catch
            {
                txtValue.Text = "0.00";
            }
        }

        private void ProcurementDiscountDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                cboType.SelectedIndex = 0;

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    txtDescription.Text = lRecords[2];
                    cboType.Text = lRecords[3];
                    txtValue.Text = string.Format("{0:n}", decimal.Parse(lRecords[4]));
                    txtRemarks.Text = lRecords[5];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ProcurementDiscountDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }
    }
}
