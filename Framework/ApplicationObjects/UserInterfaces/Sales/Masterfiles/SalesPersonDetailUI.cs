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
using NSites_V.ApplicationObjects.Classes.Sales;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Masterfiles
{
    public partial class SalesPersonDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[6];
        GlobalVariables.Operation lOperation;
        SalesPerson loSalesPerson;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SalesPersonDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loSalesPerson = new SalesPerson();
        }
        public SalesPersonDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loSalesPerson = new SalesPerson();
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
            txtContactNo.Clear();
            txtEmailAddress.Clear();
            txtRemarks.Clear();
            txtName.Focus();
        }
        #endregion "END OF METHODS"

        private void SalesPersonDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtName.Text = lRecords[1];
                    txtAddress.Text = lRecords[2];
                    txtContactNo.Text = lRecords[3];
                    txtEmailAddress.Text = lRecords[4];
                    txtRemarks.Text = lRecords[5];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "SalesPersonDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loSalesPerson.Id = lId;
                loSalesPerson.Name = GlobalFunctions.replaceChar(txtName.Text);
                loSalesPerson.Address = GlobalFunctions.replaceChar(txtAddress.Text);
                loSalesPerson.ContactNo = GlobalFunctions.replaceChar(txtContactNo.Text);
                loSalesPerson.EmailAddress = GlobalFunctions.replaceChar(txtEmailAddress.Text);
                loSalesPerson.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loSalesPerson.UserId = GlobalVariables.UserId;

                string _Id = loSalesPerson.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Sales Person has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtName.Text;
                    lRecords[2] = txtAddress.Text;
                    lRecords[3] = txtContactNo.Text;
                    lRecords[4] = txtEmailAddress.Text;
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
    }
}
