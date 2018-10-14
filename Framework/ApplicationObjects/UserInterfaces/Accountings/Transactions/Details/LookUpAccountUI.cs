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
    public partial class LookUpAccountUI : Form
    {
        ChartOfAccount loChartOfAccount;
        MainAccount loMainAccount;
        Classification loClassification;
        SubClassification loSubClassification;
        public string lAccountCode;
        public string lAccountTitle;
        public bool lFromSelection;
        
        public LookUpAccountUI()
        {
            InitializeComponent();
            loChartOfAccount = new ChartOfAccount();
            loMainAccount = new MainAccount();
            loClassification = new Classification();
            loSubClassification = new SubClassification();
            lFromSelection = false;
        }

        private void displayResult()
        {
            try
            {
                dgvChartOfAccounts.DataSource = null;
                if (txtSearch.Text != "")
                {
                    dgvChartOfAccounts.DataSource = loChartOfAccount.getAllData("", txtSearch.Text, "");
                }
                else
                {
                    dgvChartOfAccounts.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LookUpUI_Load(object sender, EventArgs e)
        {
            try
            {
                lFromSelection = false;
                lAccountCode = "";
                lAccountTitle = "";
                /*
                cboMainAccount.DataSource = loMainAccount.getAllData("ViewAll", "");
                cboMainAccount.DisplayMember = "Description";
                cboMainAccount.ValueMember = "Code";
                cboMainAccount.SelectedIndex = -1;

                cboClassification.DataSource = loClassification.getAllData("ViewAll", "");
                cboClassification.DisplayMember = "Description";
                cboClassification.ValueMember = "Code";
                cboClassification.SelectedIndex = -1;

                cboSubClassification.DataSource = loSubClassification.getAllData("ViewAll", "");
                cboSubClassification.DisplayMember = "Description";
                cboSubClassification.ValueMember = "Code";
                cboSubClassification.SelectedIndex = -1;
                */
                displayResult();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LookUpUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                displayResult();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearch_TextChanged");
                em.ShowDialog();
                return;
            }
        }

        private void dgvChartOfAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvChartOfAccounts.Columns[e.ColumnIndex].Name == "Code" || this.dgvChartOfAccounts.Columns[e.ColumnIndex].Name == "Contra Account")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvInvestments_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvChartOfAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChartOfAccounts.Rows.Count > 0)
            {
                lAccountCode = dgvChartOfAccounts.CurrentRow.Cells[0].Value.ToString();
                lAccountTitle = dgvChartOfAccounts.CurrentRow.Cells[1].Value.ToString();
                lFromSelection = true;
            }
            else
            {
                lAccountCode = "";
                lAccountTitle = "";
                lFromSelection = false;
            }
            this.Close();
        }
    }
}
