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
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports
{
    public partial class POPayableReportUI : Form
    {
        PurchaseOrder loPurchaseOrder;
        POPayableRpt loAccountPayableRpt;
        POPayableByDueDateRpt loAccountPayableByDueDateRpt;
        OverDueAccountsPayableRpt loOverDueAccountsRpt;
        
        public POPayableReportUI()
        {
            InitializeComponent();
            loPurchaseOrder = new PurchaseOrder();
            loAccountPayableRpt = new POPayableRpt();
            loAccountPayableByDueDateRpt = new POPayableByDueDateRpt();
            loOverDueAccountsRpt = new OverDueAccountsPayableRpt();
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
                DataTable _dtPayable = loPurchaseOrder.getAccountPayables();
                if (_dtPayable.Rows.Count > 0)
                {
                    loAccountPayableRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loAccountPayableRpt.Database.Tables[1].SetDataSource(_dtPayable);
                    loAccountPayableRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loAccountPayableRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loAccountPayableRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loAccountPayableRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loAccountPayableRpt.SetParameterValue("Title", "All Payables");
                    loAccountPayableRpt.SetParameterValue("SubTitle", "All Payables");
                    crvAllPaybleAccounts.ReportSource = loAccountPayableRpt;
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
            
            try
            {
                DataTable _dtDueDate = loPurchaseOrder.getAccountPayables();
                if (_dtDueDate.Rows.Count > 0)
                {
                    loAccountPayableByDueDateRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loAccountPayableByDueDateRpt.Database.Tables[1].SetDataSource(_dtDueDate);
                    loAccountPayableByDueDateRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loAccountPayableByDueDateRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loAccountPayableByDueDateRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loAccountPayableByDueDateRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loAccountPayableByDueDateRpt.SetParameterValue("Title", "Payables by Due Date");
                    loAccountPayableByDueDateRpt.SetParameterValue("SubTitle", "Payables by Due Date");
                    crvPayablesByDueDate.ReportSource = loAccountPayableByDueDateRpt;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            try
            {
                DataTable _dtOverDueAccounts = loPurchaseOrder.getAccountPayablesOverdue();
                if (_dtOverDueAccounts.Rows.Count > 0)
                {
                    loOverDueAccountsRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loOverDueAccountsRpt.Database.Tables[1].SetDataSource(_dtOverDueAccounts);
                    loOverDueAccountsRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loOverDueAccountsRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loOverDueAccountsRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loOverDueAccountsRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loOverDueAccountsRpt.SetParameterValue("Title", "Overdue Payables");
                    loOverDueAccountsRpt.SetParameterValue("SubTitle", "Overdue Payables");
                    crvOverdueAccounts.ReportSource = loOverDueAccountsRpt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AccountPayableReportUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmPOPayableReport", "Preview"))
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
