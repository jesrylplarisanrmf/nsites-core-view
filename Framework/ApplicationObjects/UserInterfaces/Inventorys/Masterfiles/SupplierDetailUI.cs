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


namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles
{
    public partial class SupplierDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[10];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Supplier loSupplier;
        Bank loBank;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SupplierDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loSupplier = new Supplier();
            loBank = new Bank();
        }
        public SupplierDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loSupplier = new Supplier();
            loBank = new Bank();
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
            txtName.Clear();
            txtAddress.Clear();
            txtTIN.Clear();
            txtContactPerson.Clear();
            txtContactNo.Clear();
            txtEmailAddress.Clear();
            txtBankName.Clear();
            txtBankAccountNo.Clear();
            txtRemarks.Clear();
            txtName.Focus();
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
                loSupplier.Id = lId;
                loSupplier.Name = GlobalFunctions.replaceChar(txtName.Text);
                loSupplier.Address = GlobalFunctions.replaceChar(txtAddress.Text);
                loSupplier.TIN = txtTIN.Text;
                loSupplier.ContactPerson = txtContactPerson.Text;
                loSupplier.ContactNo = txtContactNo.Text;
                loSupplier.EmailAddress = txtEmailAddress.Text;
                loSupplier.BankName = txtBankName.Text;
                loSupplier.BankAccountNo = txtBankAccountNo.Text;
                loSupplier.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loSupplier.UserId = GlobalVariables.UserId;

                string _Id = loSupplier.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Supplier has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtName.Text;
                    lRecords[2] = txtAddress.Text;
                    lRecords[3] = txtTIN.Text;
                    lRecords[4] = txtContactPerson.Text;
                    lRecords[5] = txtContactNo.Text;
                    lRecords[6] = txtEmailAddress.Text;
                    lRecords[7] = txtBankName.Text;
                    lRecords[8] = txtBankAccountNo.Text;
                    lRecords[9] = txtRemarks.Text;
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

        private void SupplierDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtName.Text = lRecords[1];
                    txtAddress.Text = lRecords[2];
                    txtTIN.Text = lRecords[3];
                    txtContactPerson.Text = lRecords[4];
                    txtContactNo.Text = lRecords[5];
                    txtEmailAddress.Text = lRecords[6];
                    txtBankName.Text = lRecords[7];
                    txtBankAccountNo.Text = lRecords[8];
                    txtRemarks.Text = lRecords[9];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "SupplierDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }
    }
}
