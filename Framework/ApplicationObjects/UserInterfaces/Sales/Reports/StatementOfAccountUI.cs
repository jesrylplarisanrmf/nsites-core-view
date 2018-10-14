using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports
{
    public partial class StatementOfAccountUI : Form
    {
        SalesOrder loSalesOrder;
        Customer loCustomer;
        StatementOfAccountRpt loStatementOfAccountRpt;
        
        public StatementOfAccountUI()
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            loCustomer = new Customer();
            loStatementOfAccountRpt = new StatementOfAccountRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void refresh(string pCustomerId)
        {
            try
            {
                DataTable _dtSOA = loSalesOrder.getStatementOfAccount(pCustomerId);
                if (_dtSOA.Rows.Count > 0)
                {
                    loStatementOfAccountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loStatementOfAccountRpt.Database.Tables[1].SetDataSource(_dtSOA);
                    loStatementOfAccountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loStatementOfAccountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loStatementOfAccountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loStatementOfAccountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loStatementOfAccountRpt.SetParameterValue("Title", "Statement of Account");
                    loStatementOfAccountRpt.SetParameterValue("SubTitle", "Statement of Account");
                    loStatementOfAccountRpt.SetParameterValue("CustomerName", cboCustomer.Text);
                    crvList.ReportSource = loStatementOfAccountRpt;
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("No records found!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void StatementOfAccountUI_Load(object sender, EventArgs e)
        {
            try
            {
                cboCustomer.DataSource = loCustomer.getAllData("ViewAll", "", "");
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "Id";
                cboCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StatementOfAccountUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClose_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmStatementOfAccount", "Preview"))
                {
                    return;
                }

                string _customerId = "";
                try
                {
                    _customerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI mb = new MessageBoxUI("You must select a correct Customer!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.showDialog();
                    cboCustomer.Focus();
                    return;
                }

                refresh(_customerId);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
