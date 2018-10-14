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
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports
{
    public partial class CheckIssuanceUI : Form
    {
        CheckDetail loCheckDetail;
        CheckIssuanceRpt loCheckIssuanceRpt;
        
        public CheckIssuanceUI()
        {
            InitializeComponent();
            loCheckDetail = new CheckDetail();
            loCheckIssuanceRpt = new CheckIssuanceRpt();
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
                DataTable _dtIssuance = loCheckDetail.getCheckIssuance(dtpStartDate.Value, dtpEndDate.Value);
                if (_dtIssuance.Rows.Count > 0)
                {
                    loCheckIssuanceRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loCheckIssuanceRpt.Database.Tables[1].SetDataSource(_dtIssuance);
                    loCheckIssuanceRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loCheckIssuanceRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loCheckIssuanceRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loCheckIssuanceRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loCheckIssuanceRpt.SetParameterValue("Title", "Check Issuance");
                    loCheckIssuanceRpt.SetParameterValue("SubTitle", "Check Issuance");
                    loCheckIssuanceRpt.SetParameterValue("StartDate", String.Format("{0:yyyy-MM-dd}", dtpStartDate.Value));
                    loCheckIssuanceRpt.SetParameterValue("EndDate", String.Format("{0:yyyy-MM-dd}", dtpEndDate.Value));
                    crvList.ReportSource = loCheckIssuanceRpt;
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
        }

        private void CheckIssuanceUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmCheckIssuance", "Preview"))
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
