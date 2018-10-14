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
    public partial class InventoryTypeDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[5];
        GlobalVariables.Operation lOperation;
        InventoryType loInventoryType;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public InventoryTypeDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loInventoryType = new InventoryType();
        }
        public InventoryTypeDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loInventoryType = new InventoryType();
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
            txtDescription.Clear();
            cboQty.SelectedIndex = -1;
            cboSource.SelectedIndex = -1;
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void InventoryTypeDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtDescription.Text = lRecords[1];
                    cboQty.Text = lRecords[2];
                    cboSource.Text = lRecords[3];
                    txtRemarks.Text = lRecords[4];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "InventoryTypeDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboQty.Text == "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Qty must have a value!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboQty.Focus();
                    return;
                }
                if (cboSource.Text == "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Source must have a value!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboSource.Focus();
                    return;
                }

                loInventoryType.Id = lId;
                loInventoryType.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                loInventoryType.Qty = cboQty.Text;
                loInventoryType.Source = cboSource.Text;
                loInventoryType.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loInventoryType.UserId = GlobalVariables.UserId;

                string _Id = loInventoryType.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Inventory Type has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtDescription.Text;
                    lRecords[2] = cboQty.Text;
                    lRecords[3] = cboSource.Text;
                    lRecords[4] = txtRemarks.Text;
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
    }
}
