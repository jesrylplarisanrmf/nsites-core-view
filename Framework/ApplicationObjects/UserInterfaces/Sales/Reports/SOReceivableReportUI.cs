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
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports
{
    public partial class SOReceivableReportUI : Form
    {
        SalesOrder loSalesOrder;
        SOReceivableRpt loAccountReceivableRpt;
        SOReceivableByDueDateRpt loAccountReceivableByDueDateRpt;
        OverDueAccountsReceivableRpt loOverDueAccountsReceivableRpt;
        
        public SOReceivableReportUI()
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            loAccountReceivableRpt = new SOReceivableRpt();
            loAccountReceivableByDueDateRpt = new SOReceivableByDueDateRpt();
            loOverDueAccountsReceivableRpt = new OverDueAccountsReceivableRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void refresh()
        {
            try
            {
                DataTable _dtReceivable = loSalesOrder.getAccountReceivables();
                if (_dtReceivable.Rows.Count > 0)
                {
                    loAccountReceivableRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loAccountReceivableRpt.Database.Tables[1].SetDataSource(_dtReceivable);
                    loAccountReceivableRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loAccountReceivableRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loAccountReceivableRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loAccountReceivableRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loAccountReceivableRpt.SetParameterValue("Title", "All Receivables");
                    loAccountReceivableRpt.SetParameterValue("SubTitle", "All Receivables");
                    crvAllReceivableAccounts.ReportSource = loAccountReceivableRpt;
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("No records found!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                DataTable _dtDueDate = loSalesOrder.getAccountReceivables();
                if (_dtDueDate.Rows.Count > 0)
                {
                    loAccountReceivableByDueDateRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loAccountReceivableByDueDateRpt.Database.Tables[1].SetDataSource(_dtDueDate);
                    loAccountReceivableByDueDateRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loAccountReceivableByDueDateRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loAccountReceivableByDueDateRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loAccountReceivableByDueDateRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loAccountReceivableByDueDateRpt.SetParameterValue("Title", "Receivables by Due Date");
                    loAccountReceivableByDueDateRpt.SetParameterValue("SubTitle", "Receivables by Due Date");
                    crvReceivablesByDueDate.ReportSource = loAccountReceivableByDueDateRpt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                DataTable _dtOverdueAccounts = loSalesOrder.getAccountReceivablesOverdue();
                if (_dtOverdueAccounts.Rows.Count > 0)
                {
                    loOverDueAccountsReceivableRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loOverDueAccountsReceivableRpt.Database.Tables[1].SetDataSource(_dtOverdueAccounts);
                    loOverDueAccountsReceivableRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loOverDueAccountsReceivableRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loOverDueAccountsReceivableRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loOverDueAccountsReceivableRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loOverDueAccountsReceivableRpt.SetParameterValue("Title", "Overdue Receivables");
                    loOverDueAccountsReceivableRpt.SetParameterValue("SubTitle", "Overdue Receivable");
                    crvOverdueAccounts.ReportSource = loOverDueAccountsReceivableRpt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AccountReceivableReportUI_Load(object sender, EventArgs e)
        {

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
                if (!GlobalFunctions.checkRights("tsmSOReceivableReport", "Preview"))
                {
                    return;
                }
                refresh();
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
