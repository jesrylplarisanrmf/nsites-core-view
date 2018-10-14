using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Accountings;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions.Details
{
    public partial class CheckDetailUI : Form
    {
        Bank loBank;

        string[] lRecordData = new string[8];

        string lDetailId;
        string lBankId;
        string lCheckNo;
        decimal lCheckAmount;
        DateTime lCheckDate;
        string lRemarks;
        string lOperator;
        
        public CheckDetailUI()
        {
            InitializeComponent();
            loBank = new Bank();
            lDetailId = "";
            lOperator = "Add";
        }

        public CheckDetailUI(string pDetailId, string pBankId, string pCheckNo, decimal pCheckAmount,
            DateTime pCheckDate, string pRemarks)
        {
            InitializeComponent();
            loBank = new Bank();
            lDetailId = pDetailId;
            lBankId = pBankId;
            lCheckNo = pCheckNo;
            lCheckAmount = pCheckAmount;
            lCheckDate = pCheckDate;
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
                cboBank.SelectedIndex = -1;
                txtCheckNo.Text = "";
                txtCheckAmount.Text = "0.00";
                dtpCheckDate.Value = DateTime.Now;
                txtRemarks.Clear();
                cboBank.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboBank.DataSource = loBank.getAllData("ViewAll", "", "");
                cboBank.DisplayMember = "Description";
                cboBank.ValueMember = "Id";
                cboBank.SelectedIndex = -1;

                if (lOperator == "Edit")
                {
                    try
                    {
                        cboBank.SelectedValue = lBankId;
                    }
                    catch { }
                    cboBank.Enabled = true;
                    txtCheckNo.Text = lCheckNo;
                    txtCheckAmount.Text = string.Format("{0:n}", lCheckAmount);
                    dtpCheckDate.Value = lCheckDate;
                    txtRemarks.Text = lRemarks;
                }
                else
                {
                    cboBank.SelectedIndex = -1;
                    txtCheckNo.Text = "";
                    txtCheckAmount.Text = "0.00";
                    dtpCheckDate.Value = DateTime.Now;
                    txtRemarks.Clear();
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtCheckAmount.Text) == 0)
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("Check Amount must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    return;
                }

                lRecordData[0] = lDetailId;
                try
                {
                    lRecordData[1] = cboBank.SelectedValue.ToString();
                    lRecordData[2] = cboBank.Text;
                }
                catch
                {
                    MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Bank!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mbStatus.ShowDialog();
                    cboBank.Focus();
                    return;
                }
                lRecordData[3] = txtCheckNo.Text;
                lRecordData[4] = string.Format("{0:n}", decimal.Parse(txtCheckAmount.Text));
                lRecordData[5] = string.Format("{0:MM-dd-yyyy}", dtpCheckDate.Value);
                lRecordData[6] = GlobalFunctions.replaceChar(txtRemarks.Text);

                object[] _params = { lRecordData, false };
                if (lOperator == "Add")
                {
                    lRecordData[7] = "Add";
                    ParentList.GetType().GetMethod("addDataCheck").Invoke(ParentList, _params);
                    //MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    //_mbStatus.ShowDialog();
                    clear();
                }
                else if (lOperator == "Edit")
                {
                    lRecordData[7] = "Edit";
                    ParentList.GetType().GetMethod("updateDataCheck").Invoke(ParentList, _params);
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

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCheckAmount.Text = string.Format("{0:n}", decimal.Parse(txtCheckAmount.Text));
            }
            catch
            {
                txtCheckAmount.Text = "0.00";
            }
        }
    }
}
