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
    public partial class CustomerDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[11];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        Customer loCustomer;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CustomerDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loCustomer = new Customer();
        }
        public CustomerDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loCustomer = new Customer();
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
            txtCreditLimit.Text = "0.00";
            txtContactPerson.Clear();
            txtContactNo.Clear();
            txtEmailAddress.Clear();
            txtRemarks.Clear();
            txtName.Focus();
        }
        #endregion "END OF METHODS"

        private void CustomerDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtName.Text = lRecords[1];
                    chkDefault.Checked = lRecords[2] == "Y" ? true : false;
                    txtAddress.Text = lRecords[3];
                    try
                    {
                        dtpBirthdate.Value = GlobalFunctions.ConvertToDate(lRecords[4]);
                    }
                    catch { }
                    txtTIN.Text = lRecords[5];
                    txtCreditLimit.Text = string.Format("{0:n}", decimal.Parse(lRecords[6]));
                    txtContactPerson.Text = lRecords[7];
                    txtContactNo.Text = lRecords[8];
                    txtEmailAddress.Text = lRecords[9];
                    txtRemarks.Text = lRecords[10];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CustomerDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loCustomer.Id = lId;
                loCustomer.Name = GlobalFunctions.replaceChar(txtName.Text);
                loCustomer.Default = chkDefault.Checked ? "Y" : "N";
                loCustomer.Address = GlobalFunctions.replaceChar(txtAddress.Text);
                loCustomer.Birthdate = dtpBirthdate.Value;
                loCustomer.TIN = txtTIN.Text;
                loCustomer.CreditLimit = decimal.Parse(txtCreditLimit.Text);
                loCustomer.ContactPerson = txtContactPerson.Text;
                loCustomer.ContactNo = txtContactNo.Text;
                loCustomer.EmailAddress = txtEmailAddress.Text;
                loCustomer.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loCustomer.UserId = GlobalVariables.UserId;

                string _Id = loCustomer.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Customer has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtName.Text;
                    lRecords[2] = chkDefault.Checked ? "Y" : "N";
                    lRecords[3] = txtAddress.Text;
                    lRecords[4] = string.Format("{0:MM-dd-yyyy}", dtpBirthdate.Value);
                    lRecords[5] = txtTIN.Text;
                    lRecords[6] = string.Format("{0:n}", decimal.Parse(txtCreditLimit.Text));
                    lRecords[7] = txtContactPerson.Text;
                    lRecords[8] = txtContactNo.Text;
                    lRecords[9] = txtEmailAddress.Text;
                    lRecords[10] = txtRemarks.Text;
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

        private void txtCreditLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCreditLimit.Text = string.Format("{0:n}", decimal.Parse(txtCreditLimit.Text));
            }
            catch
            {
                txtCreditLimit.Text = "0.00";
            }
        }
    }
}
    