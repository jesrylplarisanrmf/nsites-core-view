using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports.TransactionRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions
{
    public partial class PurchaseRequestUI : Form
    {
        PurchaseRequest loPurchaseRequest;
        PurchaseRequestDetail loPurchaseRequestDetail;
        Common loCommon;
        SearchesUI loSearches;
        PurchaseRequestRpt loPurchaseRequestRpt;
        PurchaseRequestDetailRpt loPurchaseRequestDetailRpt;
        System.Data.DataTable ldtPurchaseRequest;
        
        ReportViewerUI loReportViewer;

        public PurchaseRequestUI()
        {
            InitializeComponent();
            loPurchaseRequest = new PurchaseRequest();
            loPurchaseRequestDetail = new PurchaseRequestDetail();
            loCommon = new Common();
            ldtPurchaseRequest = new System.Data.DataTable();
            loPurchaseRequestRpt = new PurchaseRequestRpt();
            loPurchaseRequestDetailRpt = new PurchaseRequestDetailRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void refresh()
        {
            try
            {
                try
                {
                    ldtPurchaseRequest = loPurchaseRequest.getAllData("ViewAll", "", "");
                }
                catch
                {
                    ldtPurchaseRequest = null;
                }
                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtPurchaseRequest);
                viewDetails();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void viewDetails()
        {
            try
            {
                dgvDetailList.DataSource = loPurchaseRequestDetail.getPurchaseRequestDetails("ViewAll",dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void previewPurchaseRequestDetail(string pPurchaseRequestId)
        {
            try
            {
                foreach (DataRow _dr in loPurchaseRequest.getAllData("", pPurchaseRequestId, "").Rows)
                {
                    loPurchaseRequestDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPurchaseRequestDetailRpt.Database.Tables[1].SetDataSource(loPurchaseRequestDetail.getPurchaseRequestDetails("ViewAll",pPurchaseRequestId));
                    loPurchaseRequestDetailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPurchaseRequestDetailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPurchaseRequestDetailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPurchaseRequestDetailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPurchaseRequestDetailRpt.SetParameterValue("Title", "Purchase Request");
                    loPurchaseRequestDetailRpt.SetParameterValue("SubTitle", "Purchase Request");
                    loPurchaseRequestDetailRpt.SetParameterValue("PRId", _dr["Id"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("Date", _dr["Date"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("RequestedBy", _dr["Requested By"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("DateNeeded", _dr["Date Needed"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("Supplier", _dr["Supplier"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("Instructions", _dr["Instructions"].ToString());
                    loPurchaseRequestDetailRpt.SetParameterValue("Terms", _dr["Terms"].ToString());
                    loReportViewer.crystalReportViewer.ReportSource = loPurchaseRequestDetailRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PurchaseRequestUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(PurchaseRequest);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmPurchaseRequest");
                loSearches.lQuery = "";
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PurchaseRequestUI_Load");
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Refresh"))
                {
                    return;
                }
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "P.O. Id" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Reference" || this.dgvList.Columns[e.ColumnIndex].Name == "Terms")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Total Qty" || this.dgvList.Columns[e.ColumnIndex].Name == "Total Amount")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                else if (this.dgvList.Columns[e.ColumnIndex].Name == "Approve")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (e.Value.ToString() == "N")
                        {
                            e.CellStyle.BackColor = Color.Green;
                        }
                    }
                }
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Cancel")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        if (e.Value.ToString() == "Y")
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetailList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Id" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Stock Code" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Unit")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvDetailList.Columns[e.ColumnIndex].Name == "Qty" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Unit Price" ||
                    this.dgvDetailList.Columns[e.ColumnIndex].Name == "Discount Amount" || this.dgvDetailList.Columns[e.ColumnIndex].Name == "Total Price")
                {
                    if (e.Value != null)
                    {
                        e.Value = string.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvDetailList_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Create"))
                {
                    return;
                }
                PurchaseRequestDetailUI loPurchaseRequestDetail = new PurchaseRequestDetailUI();
                loPurchaseRequestDetail.ParentList = this;
                loPurchaseRequestDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCreate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Update"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseRequest.getPurchaseRequestStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update an APPROVED Purchase Request!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    PurchaseRequestDetailUI loPurchaseRequestDetail = new PurchaseRequestDetailUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loPurchaseRequestDetail.ParentList = this;
                    loPurchaseRequestDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Remove"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseRequest.getPurchaseRequestStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove an APPROVED Purchase Request!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Purchase Request record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPurchaseRequest.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Purchase Request record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            refresh();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRemove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Approve"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseRequest.getPurchaseRequestStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus["Approve"].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Request is already APPROVED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot APPROVE a Purchase Request you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue approving this Purchase Request record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPurchaseRequest.approve(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Purchase Request record has been successfully approved!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewPurchaseRequestDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                            //sendEmailToCreator();
                            refresh();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnApprove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Cancel"))
                {
                    return;
                }
                
                foreach (DataRow _drStatus in loPurchaseRequest.getPurchaseRequestStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Request must be APPROVED to be cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Purchase Request is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["POId"].ToString() != "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel a Purchase Request with P.O. Id!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Purchase Request you preprared/approved!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Purchase Request record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        ProcurementCancelReasonUI loProcurementCancelReason = new ProcurementCancelReasonUI();
                        loProcurementCancelReason.ShowDialog();
                        if (loProcurementCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loPurchaseRequest.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loProcurementCancelReason.lReason))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Purchase Request record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh();
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Search"))
                {
                    return;
                }

                string _DisplayFields = "SELECT pr.Id AS Id,DATE_FORMAT(pr.`Date`,'%m-%d-%Y') AS `Date`, "+
		            "pr.Approve,pr.Cancel,pr.POId AS `P.O. Id`,pr.Reference,s.Name AS Supplier,pr.Terms,pr.Instructions, "+
		            "CONCAT(e.Firstname,' ',SUBSTRING(e.Middlename, 1, 1),'. ', e.Lastname) AS `Requested BY`, "+
		            "DATE_FORMAT(pr.`DateNeeded`,'%m-%d-%Y') AS `Date Needed`, "+
		            "pr.TotalQty AS `Total Qty`,pr.TotalAmount AS `Total Amount`,preby.Username AS `Prepared BY`, "+
		            "appby.Username AS `Approved BY`,DATE_FORMAT(pr.DateApproved,'%m-%d-%Y') AS `Date Approved`, "+
		            "canby.Username AS `Cancelled BY`,pr.CancelledReason AS `Cancelled Reason`, "+
		            "DATE_FORMAT(pr.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`, "+
		            "pr.Remarks "+
		            "FROM purchaserequest pr "+
		            "LEFT JOIN supplier s "+
		            "ON pr.SupplierId = s.Id "+
		            "LEFT JOIN employee e "+
		            "ON pr.RequestedBy = e.Id "+
		            "LEFT JOIN `user` preby "+
		            "ON pr.PreparedBy = preby.Id "+
		            "LEFT JOIN `user` appby "+
		            "ON pr.ApprovedBy = appby.Id "+
                    "LEFT JOIN `user` canby " +
		            "ON pr.CancelledBy = canby.Id ";
                string _WhereFields = " AND pr.`Status` = 'Active' ORDER BY pr.Id DESC;";
                loSearches.lAlias = "pr.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtPurchaseRequest = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtPurchaseRequest);

                    viewDetails();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSearch_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loPurchaseRequestRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPurchaseRequestRpt.Database.Tables[1].SetDataSource(ldtPurchaseRequest);
                    loPurchaseRequestRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPurchaseRequestRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPurchaseRequestRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPurchaseRequestRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPurchaseRequestRpt.SetParameterValue("Title", "Purchase Request");
                    loPurchaseRequestRpt.SetParameterValue("SubTitle", "Purchase Request");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loPurchaseRequestRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loPurchaseRequestRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loPurchaseRequestRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loPurchaseRequestRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    System.Drawing.Point pt = dgvList.PointToScreen(e.Location);
                    cmsFunction.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvList, ldtPurchaseRequest);
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvList_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void tsmiPreviewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPurchaseRequest", "Preview Detail"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPurchaseRequest.getPurchaseRequestStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only APPROVED Purchase Request can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Purchase Request!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewPurchaseRequestDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiPreviewDetail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void tsmiCreate_Click(object sender, EventArgs e)
        {
            btnCreate_Click(null, new EventArgs());
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tsmiApprove_Click(object sender, EventArgs e)
        {
            btnApprove_Click(null, new EventArgs());
        }

        private void tsmiCancel_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, new EventArgs());
        }

        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tsmiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }
    }
}
