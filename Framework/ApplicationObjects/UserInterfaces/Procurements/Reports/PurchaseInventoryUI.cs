using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports.ReportRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports
{
    public partial class PurchaseInventoryUI : Form
    {
        PurchaseOrderDetail loPurchaseOrderDetail;
        PurchaseInventoryRpt loPurchaseInventoryRpt;
        PurchaseInventoryBySupplierRpt loPurchaseInventoryBySupplierRpt;
        PurchaseInventoryByCategoryRpt loPurchaseInventoryByCategoryRpt;

        public PurchaseInventoryUI()
        {
            InitializeComponent();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loPurchaseInventoryRpt = new PurchaseInventoryRpt();
            loPurchaseInventoryBySupplierRpt = new PurchaseInventoryBySupplierRpt();
            loPurchaseInventoryByCategoryRpt = new PurchaseInventoryByCategoryRpt();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void PurchaseInventoryUI_Load(object sender, EventArgs e)
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
                if (!GlobalFunctions.checkRights("tsmPurchaseInventory", "Preview"))
                {
                    return;
                }

                try
                {
                    DataTable _dtByDate = loPurchaseOrderDetail.getPurchaseInventory(dtpFromDate.Value, dtpToDate.Value);
                    if (_dtByDate.Rows.Count > 0)
                    {
                        loPurchaseInventoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loPurchaseInventoryRpt.Database.Tables[1].SetDataSource(_dtByDate);
                        loPurchaseInventoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loPurchaseInventoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loPurchaseInventoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loPurchaseInventoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loPurchaseInventoryRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loPurchaseInventoryRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loPurchaseInventoryRpt.SetParameterValue("Title", "Purchase Inventory - By Date");
                        loPurchaseInventoryRpt.SetParameterValue("SubTitle", "Purchase Inventory - By Date");
                        crvByDate.ReportSource = loPurchaseInventoryRpt;
                    }

                    DataTable _dtBy = loPurchaseOrderDetail.getPurchaseInventoryBy(dtpFromDate.Value, dtpToDate.Value);

                    if (_dtBy.Rows.Count > 0)
                    {
                        loPurchaseInventoryBySupplierRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loPurchaseInventoryBySupplierRpt.Database.Tables[1].SetDataSource(_dtBy);
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("Title", "Sales Inventory - By Supplier");
                        loPurchaseInventoryBySupplierRpt.SetParameterValue("SubTitle", "Sales Inventory - By Supplier");
                        crvBySupplier.ReportSource = loPurchaseInventoryBySupplierRpt;
                    }
                    
                    if (_dtBy.Rows.Count > 0)
                    {
                        loPurchaseInventoryByCategoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                        loPurchaseInventoryByCategoryRpt.Database.Tables[1].SetDataSource(_dtBy);
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("DateFrom", string.Format("{0:MM-dd-yyyy}", dtpFromDate.Value));
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("DateTo", string.Format("{0:MM-dd-yyyy}", dtpToDate.Value));
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("Title", "Purchase Inventory - By Category");
                        loPurchaseInventoryByCategoryRpt.SetParameterValue("SubTitle", "Purchase Inventory - By Category");
                        crvByCategory.ReportSource = loPurchaseInventoryByCategoryRpt;
                    }
                }
                catch (Exception ex)
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
