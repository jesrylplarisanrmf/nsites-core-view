using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Accountings;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class VoucherDetailUI : Form
    {
        ChartOfAccount loChartOfAccount;
        Bank loBank;
        Customer loCustomer;
        Supplier loSupplier;
        SalesPerson loSalesPerson;
        Equipment loEquipment;
        Building loBuilding;
        LookUpAccountUI loLookUp;
        string[] lRecordData = new string[11];

        string lDetailId;
        string lAccountId;
        decimal lDebit;
        decimal lCredit;
        string lSubsidiary;
        string lSubsidiaryId;
        string lDescription;
        string lRemarks;
        string lOperator;

        public VoucherDetailUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loBank = new Bank();
            loCustomer = new Customer();
            loSupplier = new Supplier();
            loSalesPerson = new SalesPerson();
            loEquipment = new Equipment();
            loBuilding = new Building();
            loLookUp = new LookUpAccountUI();
            lDetailId = "";
            lAccountId = "";
            lDebit = 0;
            lCredit = 0;
            lSubsidiary = "";
            lSubsidiaryId = "";
            lDescription = "";
            lRemarks = "";
            lOperator = "Add";
        }

        public VoucherDetailUI(string pDetailId, string pAccountId,decimal pDebit,decimal pCredit,string pSubsidiary,string pSubsidiaryId,string pDescription,string pRemarks)
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loBank = new Bank();
            loCustomer = new Customer();
            loSupplier = new Supplier();
            loSalesPerson = new SalesPerson();
            loEquipment = new Equipment();
            loBuilding = new Building();
            loLookUp = new LookUpAccountUI();
            lDetailId = pDetailId;
            lAccountId = pAccountId;
            lDebit = pDebit;
            lCredit = pCredit;
            lSubsidiary = pSubsidiary;
            lSubsidiaryId = pSubsidiaryId;
            lDescription = pDescription;
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
                cboChartOfAccount.SelectedIndex = -1;
                cboChartOfAccount.Text = "";
                txtDebit.Text = "0.00";
                txtCredit.Text = "0.00";
                lblSubsidiary.Text = "";
                txtSubsidiaryId.Clear();
                cboSubsidiary.DataSource = null;
                cboSubsidiary.SelectedIndex = -1;
                cboSubsidiary.Enabled = false;
                btnLookupSubsidiary.Enabled = false;
                lblSubsidiary.Visible = false;
                txtRemarks.Clear();
                cboChartOfAccount.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getSubsidiary()
        {
            try
            {
                foreach (DataRow _dr in loChartOfAccount.getAllData("", cboChartOfAccount.SelectedValue.ToString(), "").Rows)
                {
                    if (_dr["Subsidiary"].ToString() != "")
                    {
                        lblSubsidiary.Text = _dr["Subsidiary"].ToString();
                        cboSubsidiary.Enabled = true;
                        btnLookupSubsidiary.Enabled = true;
                        if (lblSubsidiary.Text == "Bank")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loBank.getAllData("ViewAll", "", "");
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Description";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                        else if (lblSubsidiary.Text == "Customer")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loCustomer.getAllData("ViewAll", "", "");
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Name";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                        else if (lblSubsidiary.Text == "Supplier")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loSupplier.getAllData("ViewAll", "", "");
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Name";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                        else if (lblSubsidiary.Text == "Sales Person")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loSalesPerson.getSalesPersonNames();
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Name";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                        else if (lblSubsidiary.Text == "Equipment")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loEquipment.getAllData("ViewAll", "", "");
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Description";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                        else if (lblSubsidiary.Text == "Building")
                        {
                            cboSubsidiary.DataSource = null;
                            cboSubsidiary.DataSource = loBuilding.getAllData("ViewAll", "", "");
                            cboSubsidiary.ValueMember = "Id";
                            cboSubsidiary.DisplayMember = "Description";
                            cboSubsidiary.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        lblSubsidiary.Text = "";
                        cboSubsidiary.Enabled = false;
                        btnLookupSubsidiary.Enabled = false;
                        cboSubsidiary.DataSource = null;
                    }
                }
            }
            catch { }
            /*
            catch 
            {
                lblSubsidiary.Text = "";
                cboSubsidiary.Enabled = false;
                btnLookupSubsidiary.Enabled = false;
                cboSubsidiary.DataSource = null;
            }
            */
        }

        private void PlacementDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboChartOfAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboChartOfAccount.DisplayMember = "Account Title";
                cboChartOfAccount.ValueMember = "Id";
                cboChartOfAccount.SelectedIndex = -1;

                if (lOperator == "Edit")
                {
                    cboChartOfAccount.TabStop = false;
                    btnGetAccountCode.TabStop = false;
                    cboChartOfAccount.SelectedValue = lAccountId;
                    getSubsidiary();
                    txtDebit.Text = string.Format("{0:n}", lDebit);
                    txtCredit.Text = string.Format("{0:n}", lCredit);
                    lblSubsidiary.Text = lSubsidiary;
                    cboSubsidiary.SelectedValue = lSubsidiaryId;
                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    txtDebit.Text = "0.00";
                    txtCredit.Text = "0.00";
                    txtRemarks.Clear();
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDebit.Text == "0.00" && txtCredit.Text == "0.00")
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must input an amount on Debit or Credit!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                lRecordData[0] = lDetailId;
                try
                {
                    lRecordData[1] = cboChartOfAccount.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Account Title!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    cboChartOfAccount.Focus();
                    return;
                }
                lRecordData[2] = "";//chart of account code
                try
                {
                    foreach (DataRow _dr in loChartOfAccount.getAllData("", cboChartOfAccount.SelectedValue.ToString(), "").Rows)
                    {
                        lRecordData[2] = _dr["Code"].ToString();//chart of account code
                    }
                }
                catch
                {
                    lRecordData[2] = "";
                }
                lRecordData[3] = cboChartOfAccount.Text;
                lRecordData[4] = string.Format("{0:n}", decimal.Parse(txtDebit.Text));
                lRecordData[5] = string.Format("{0:n}", decimal.Parse(txtCredit.Text));
                lRecordData[6] = lblSubsidiary.Text;
                if (lblSubsidiary.Text != "")
                {
                    try
                    {
                        lRecordData[7] = cboSubsidiary.SelectedValue.ToString();
                        lRecordData[8] = cboSubsidiary.Text;
                    }
                    catch
                    {
                        if (cboSubsidiary.Items.Count > 0)
                        {
                            MessageBoxUI _mbError = new MessageBoxUI("You must select a correct " + lblSubsidiary.Text + "!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mbError.ShowDialog();
                            cboSubsidiary.Focus();
                            return;
                        }
                        else
                        {
                            lRecordData[7] = "";
                            lRecordData[8] = "";
                        }
                    }
                }
                else
                {
                    lRecordData[7] = "";
                    lRecordData[8] = "";
                }
                lRecordData[9] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData };
                if (lOperator == "Add")
                {
                    lRecordData[10] = "Add";
                    ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                    MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[10] = "Edit";
                    ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                    Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtDebit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDebit.Text = string.Format("{0:n}", decimal.Parse(txtDebit.Text));
                //getSubsidiary();
            }
            catch
            {
                txtDebit.Text = "0.00";
            }
            if (txtDebit.Text != "0.00")
            {
                txtCredit.Text = "0.00";
            }
        }

        private void txtCredit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCredit.Text = string.Format("{0:n}", decimal.Parse(txtCredit.Text));
                //getSubsidiary();
            }
            catch
            {
                txtCredit.Text = "0.00";
            }
            if (txtCredit.Text != "0.00")
            {
                txtDebit.Text = "0.00";
            }
        }

        private void btnGetAccountCode_Click(object sender, EventArgs e)
        {
            loLookUp.ShowDialog();
            if (loLookUp.lFromSelection)
            {
                cboChartOfAccount.SelectedValue = loLookUp.lAccountCode;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboChartOfAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSubsidiary();
        }

        private void btnLookupSubsidiary_Click(object sender, EventArgs e)
        {

        }

        private void txtDebit_TextChanged(object sender, EventArgs e)
        {
            if (decimal.Parse(txtDebit.Text) != 0)
            {
                txtCredit.Enabled = false;
            }
            else
            {
                txtCredit.Enabled = true;
            }
        }

        private void txtCredit_TextChanged(object sender, EventArgs e)
        {
            if (decimal.Parse(txtCredit.Text) != 0)
            {
                txtDebit.Enabled = false;
            }
            else
            {
                txtDebit.Enabled = true;
            }
        }
    }
}
