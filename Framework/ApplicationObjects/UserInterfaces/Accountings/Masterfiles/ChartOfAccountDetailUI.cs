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

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Masterfiles
{
    public partial class ChartOfAccountDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[10];
        LookUpValueUI loLookupValue;
        GlobalVariables.Operation lOperation;
        ChartOfAccount loChartOfAccount;
        MainAccount loMainAccount;
        Classification loClassification;
        SubClassification loSubClassification;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ChartOfAccountDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loLookupValue = new LookUpValueUI();
            loChartOfAccount = new ChartOfAccount();
            loMainAccount = new MainAccount();
            loClassification = new Classification();
            loSubClassification = new SubClassification();
        }
        public ChartOfAccountDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loLookupValue = new LookUpValueUI();
            loChartOfAccount = new ChartOfAccount();
            loMainAccount = new MainAccount();
            loClassification = new Classification();
            loSubClassification = new SubClassification();
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
            cboClassification.SelectedIndex = -1;
            cboSubClassification.SelectedIndex = -1;
            cboMainAccount.SelectedIndex = -1;
            txtAccountTitle.Clear();
            chkContraAccount.Checked = false;
            cboTypeOfAccount.SelectedIndex = -1;
            cboSubsidiary.SelectedIndex = -1;
            txtRemarks.Clear();
            txtCode.Focus();
        }
        #endregion "END OF METHODS"

        private void ChartOfAccountDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboMainAccount.DataSource = loMainAccount.getAllData("ViewAll", "", "");
                    cboMainAccount.DisplayMember = "Description";
                    cboMainAccount.ValueMember = "Id";
                    cboMainAccount.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboClassification.DataSource = loClassification.getAllData("ViewAll", "", "");
                    cboClassification.DisplayMember = "Description";
                    cboClassification.ValueMember = "Id";
                    cboClassification.SelectedIndex = -1;
                }
                catch { }
                try
                {
                    cboSubClassification.DataSource = loSubClassification.getAllData("ViewAll", "", "");
                    cboSubClassification.DisplayMember = "Description";
                    cboSubClassification.ValueMember = "Id";
                    cboSubClassification.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtCode.Text = lRecords[1];
                    txtCode.ReadOnly = true;
                    txtCode.BackColor = SystemColors.Control;
                    txtCode.TabStop = false;
                    cboClassification.Text = lRecords[2];
                    cboSubClassification.Text = lRecords[3];
                    cboMainAccount.Text = lRecords[4];
                    txtAccountTitle.Text = lRecords[5];
                    cboTypeOfAccount.Text = lRecords[6];
                    cboSubsidiary.Text = lRecords[7];
                    chkContraAccount.Checked = lRecords[8].ToString() == "Y" ? true : false;
                    txtRemarks.Text = lRecords[9];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ChartOfAccountDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loChartOfAccount.Id = lId;
                loChartOfAccount.Code = txtCode.Text;
                try
                {
                    loChartOfAccount.ClassificationId = cboClassification.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Classification!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboClassification.Focus();
                    return;
                }
                try
                {
                    loChartOfAccount.SubClassificationId = cboSubClassification.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Sub Classification!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboSubClassification.Focus();
                    return;
                }
                try
                {
                    loChartOfAccount.MainAccountId = cboMainAccount.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb = new MessageBoxUI("You must select a Main Account!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    cboMainAccount.Focus();
                    return;
                }
                loChartOfAccount.AccountTitle = GlobalFunctions.replaceChar(txtAccountTitle.Text);
                try
                {
                    loChartOfAccount.TypeOfAccount = cboTypeOfAccount.Text;
                }
                catch
                {
                    loChartOfAccount.TypeOfAccount = "";
                }
                try
                {
                    loChartOfAccount.Subsidiary = cboSubsidiary.Text;
                }
                catch
                {
                    loChartOfAccount.Subsidiary = "";
                }
                loChartOfAccount.ContraAccount = (chkContraAccount.Checked ? "Y" : "N");
                loChartOfAccount.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loChartOfAccount.UserId = GlobalVariables.UserId;

                string _Id = loChartOfAccount.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Chart of Account has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtCode.Text;
                    lRecords[2] = cboClassification.Text;
                    lRecords[3] = cboSubClassification.Text;
                    lRecords[4] = cboMainAccount.Text;
                    lRecords[5] = txtAccountTitle.Text;
                    lRecords[6] = cboTypeOfAccount.Text;
                    lRecords[7] = cboSubsidiary.Text;
                    lRecords[8] = chkContraAccount.Checked ? "Y" : "N";
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
    }
}
