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
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports.TransactionRpt;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions
{
    public partial class PriceQuotationUI : Form
    {
        PriceQuotation loPriceQuotation;
        PriceQuotationDetail loPriceQuotationDetail;
        Common loCommon;
        SearchesUI loSearches;
        PriceQuotationRpt loPriceQuotationRpt;
        PriceQuotationDetailRpt loPriceQuotationDetailRpt;
        System.Data.DataTable ldtPriceQuotation;
        ReportViewerUI loReportViewer;

        public PriceQuotationUI()
        {
            InitializeComponent();
            loPriceQuotation = new PriceQuotation();
            loPriceQuotationDetail = new PriceQuotationDetail();
            loCommon = new Common();
            ldtPriceQuotation = new System.Data.DataTable();
            loPriceQuotationRpt = new PriceQuotationRpt();
            loPriceQuotationDetailRpt = new PriceQuotationDetailRpt();
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
                    ldtPriceQuotation = loPriceQuotation.getAllData("ViewAll", "", "");
                }
                catch
                {
                    ldtPriceQuotation = null;
                }
                loSearches.lQuery = "";
                GlobalFunctions.refreshGrid(ref dgvList, ldtPriceQuotation);
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
                dgvDetailList.DataSource = loPriceQuotationDetail.getPriceQuotationDetails("ViewAll",dgvList.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                dgvDetailList.DataSource = null;
            }
        }

        private void previewPriceQuotationDetail(string pPriceQuotationId)
        {
            try
            {
                foreach (DataRow _dr in loPriceQuotation.getAllData("", pPriceQuotationId, "").Rows)
                {
                    loPriceQuotationDetailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPriceQuotationDetailRpt.Database.Tables[1].SetDataSource(loPriceQuotationDetail.getPriceQuotationDetails("ViewAll",pPriceQuotationId));
                    loPriceQuotationDetailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPriceQuotationDetailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPriceQuotationDetailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPriceQuotationDetailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPriceQuotationDetailRpt.SetParameterValue("Title", "Price Quotation");
                    loPriceQuotationDetailRpt.SetParameterValue("SubTitle", "Price Quotation");
                    loPriceQuotationDetailRpt.SetParameterValue("PQId", _dr["Id"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("Date", _dr["Date"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("SalesPerson", _dr["Sales Person"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("Reference", _dr["Reference"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("ValidUntil", _dr["Valid Until"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("Customer", _dr["Customer"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("Instructions", _dr["Instructions"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("Terms", _dr["Terms"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("ShipDate", _dr["Ship Date"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("ShipVia", _dr["Ship Via"].ToString());
                    loPriceQuotationDetailRpt.SetParameterValue("TotalQty", string.Format("{0:n}", decimal.Parse(_dr["Total Qty"].ToString())));
                    loPriceQuotationDetailRpt.SetParameterValue("TotalAmount", string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString())));
                    loReportViewer.crystalReportViewer.ReportSource = loPriceQuotationDetailRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PriceQuotationUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(PriceQuotation);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmPriceQuotation");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PriceQuotationUI_Load");
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Refresh"))
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
                if (this.dgvList.Columns[e.ColumnIndex].Name == "Id" || this.dgvList.Columns[e.ColumnIndex].Name == "Terms" ||
                    this.dgvList.Columns[e.ColumnIndex].Name == "Reference" || this.dgvList.Columns[e.ColumnIndex].Name == "S.O. Id")
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Create"))
                {
                    return;
                }
                PriceQuotationDetailUI loPriceQuotationDetail = new PriceQuotationDetailUI();
                loPriceQuotationDetail.ParentList = this;
                loPriceQuotationDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Update"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPriceQuotation.getPriceQuotationStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot update an APPROVED Price Quotation!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }
                if (dgvList.Rows.Count > 0)
                {
                    PriceQuotationDetailUI loPriceQuotationDetail = new PriceQuotationDetailUI(dgvList.CurrentRow.Cells[0].Value.ToString());
                    loPriceQuotationDetail.ParentList = this;
                    loPriceQuotationDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Remove"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPriceQuotation.getPriceQuotationStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot remove an APPROVED Price Quotation!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                }

                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this Price Quotation record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPriceQuotation.remove(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Price Quotation record has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Approved"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPriceQuotation.getPriceQuotationStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Price Quotation is already APPROVED!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot APPROVE a Price Quotation you preprared!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue approving this Price Quotation record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loPriceQuotation.approve(dgvList.CurrentRow.Cells[0].Value.ToString()))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("Price Quotation record has been successfully approved!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            previewPriceQuotationDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Cancel"))
                {
                    return;
                }

                foreach (DataRow _drStatus in loPriceQuotation.getPriceQuotationStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Price Quotation must be APPROVED to be cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Price Quotation is already cancelled!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    if (_drStatus["SOId"].ToString() != "")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot cancel a Price Quotation with S.O. Id!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    /*
                    if (_drStatus[1].ToString() == GlobalVariables.Username || _drStatus[4].ToString() == GlobalVariables.Username)
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot CANCEL a Price Quotation you preprared/approved!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    */
                }
                if (dgvList.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue cancelling this Price Quotation record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        SalesCancelReasonUI loSalesCancelReason = new SalesCancelReasonUI();
                        loSalesCancelReason.ShowDialog();
                        if (loSalesCancelReason.lReason == "")
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("You must have a reason in cancelling entry!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (loPriceQuotation.cancel(dgvList.CurrentRow.Cells[0].Value.ToString(), loSalesCancelReason.lReason))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI("Price Quotation record has been successfully cancelled!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Search"))
                {
                    return;
                }
                
                string _DisplayFields = "SELECT pq.Id AS Id,DATE_FORMAT(pq.`Date`,'%m-%d-%Y') AS `Date`, "+
		            "pq.Approve,pq.Cancel,pq.SOId AS `S.O. Id`,pq.Reference, "+
		            "c.Name AS Customer,pq.Terms,pq.Instructions, "+
		            "DATE_FORMAT(pq.ValidUntil,'%m-%d-%Y') AS `Valid Until`, "+
		            "sp.Name AS `Sales Person`,DATE_FORMAT(pq.ShipDate,'%m-%d-%Y') AS `Ship Date`, "+
		            "pq.ShipVia AS `Ship Via`,pq.TotalQty AS `Total Qty`,pq.TotalAmount AS `Total Amount`, "+
		            "preby.Username AS `Prepared By`,appby.Username AS `Approved By`, "+
		            "DATE_FORMAT(pq.DateApproved,'%m-%d-%Y') AS `Date Approved`, "+
		            "canby.Username AS `Cancelled By`,pq.CancelledReason AS `Cancelled Reason`, "+
		            "DATE_FORMAT(pq.DateCancelled,'%m-%d-%Y') AS `Date Cancelled`,pq.Remarks "+
		            "FROM pricequotation pq "+
		            "LEFT JOIN customer c "+
		            "ON pq.CustomerId = c.Id "+
		            "LEFT JOIN salesperson sp "+
		            "ON pq.SalesPersonId = sp.Id "+
		            "LEFT JOIN `user` preby "+
		            "ON pq.PreparedBy = preby.Id "+
		            "LEFT JOIN `user` appby "+
		            "ON pq.ApprovedBy = appby.Id "+
                    "LEFT JOIN `user` canby " +
		            "ON pq.CancelledBy = canby.Id ";
                string _WhereFields = " AND pq.`Status` = 'Active' ORDER BY pq.Id DESC;";
                loSearches.lAlias = "pq.";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtPriceQuotation = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvList, ldtPriceQuotation);

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
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Preview"))
                {
                    return;
                }
                if (dgvList.Rows.Count > 0)
                {
                    loPriceQuotationRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loPriceQuotationRpt.Database.Tables[1].SetDataSource(ldtPriceQuotation);
                    loPriceQuotationRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loPriceQuotationRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loPriceQuotationRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loPriceQuotationRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loPriceQuotationRpt.SetParameterValue("Title", "Price Quotation");
                    loPriceQuotationRpt.SetParameterValue("SubTitle", "Price Quotation");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loPriceQuotationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loPriceQuotationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loPriceQuotationRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loPriceQuotationRpt;
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
                GlobalFunctions.refreshAll(ref dgvList, ldtPriceQuotation);
                viewDetails();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
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

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void tsmPreviewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmPriceQuotation", "Preview Detail"))
                {
                    return;
                }
                foreach (DataRow _drStatus in loPriceQuotation.getPriceQuotationStatus(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                {
                    if (_drStatus[0].ToString() == "N")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("Only APPROVED Price Quotation can be previewed!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else if (_drStatus[2].ToString() == "Y")
                    {
                        MessageBoxUI _mbStatus = new MessageBoxUI("You cannot preview a cancelled Price Quotation!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        _mbStatus.ShowDialog();
                        return;
                    }
                    else
                    {
                        previewPriceQuotationDetail(dgvList.CurrentRow.Cells[0].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPreviewDetail_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
