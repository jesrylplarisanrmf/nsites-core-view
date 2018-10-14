using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports.ReportsRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports
{
    public partial class ReorderLevelUI : Form
    {
        Stock loStock;
        ReorderLevelRpt loReorderLevelRpt;
        ReorderLevelByGroupRpt loReorderLevelByGroupRpt;

        public ReorderLevelUI()
        {
            InitializeComponent();
            loStock = new Stock();
            loReorderLevelRpt = new ReorderLevelRpt();
            loReorderLevelByGroupRpt = new ReorderLevelByGroupRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void ReorderLevelUI_Load(object sender, EventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmReorderLevel", "Refresh"))
                {
                    return;
                }

                try
                {
                    loReorderLevelRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loReorderLevelRpt.Database.Tables[1].SetDataSource(loStock.getReorderLevel());
                    loReorderLevelRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loReorderLevelRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loReorderLevelRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loReorderLevelRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loReorderLevelRpt.SetParameterValue("Title", "Reorder Level");
                    loReorderLevelRpt.SetParameterValue("SubTitle", "Reorder Level");
                    crvAllStocks.ReportSource = loReorderLevelRpt;
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                try
                {
                    loReorderLevelByGroupRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loReorderLevelByGroupRpt.Database.Tables[1].SetDataSource(loStock.getReorderLevel());
                    loReorderLevelByGroupRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loReorderLevelByGroupRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loReorderLevelByGroupRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loReorderLevelByGroupRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loReorderLevelByGroupRpt.SetParameterValue("Title", "Reorder Level by Group");
                    loReorderLevelByGroupRpt.SetParameterValue("SubTitle", "Reorder Level by Group");
                    crvByGroup.ReportSource = loReorderLevelByGroupRpt;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
