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
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details
{
    public partial class PurchaseRequestDetailUI : Form
    {
        #region "VARIABLES"
        PurchaseRequest loPurchaseRequest;
        PurchaseRequestDetail loPurchaseRequestDetail;
        Supplier loSupplier;
        Stock loStock;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        //StockReceivingDetailRpt loStockReceivingDetailRpt;
        ReportViewerUI loReportViewer;
        string lPurchaseRequestId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public PurchaseRequestDetailUI()
        {
            InitializeComponent();
            loPurchaseRequest = new PurchaseRequest();
            loPurchaseRequestDetail = new PurchaseRequestDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lPurchaseRequestId = "";
        }
        public PurchaseRequestDetailUI(string pPurchaseRequestId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loPurchaseRequest = new PurchaseRequest();
            loPurchaseRequestDetail = new PurchaseRequestDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loCommon = new Common();
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lPurchaseRequestId = pPurchaseRequestId;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        public void addData(string[] pRecordData)
        {
            try
            {
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells[1].Value.ToString() == pRecordData[1])
                    {
                        MessageBoxUI _mb = new MessageBoxUI("Duplicate", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb.showDialog();
                        return;
                    }
                }
                
                int n = dgvDetail.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetail.CurrentRow.Selected = false;
                dgvDetail.FirstDisplayedScrollingRowIndex = dgvDetail.Rows[n].Index;
                dgvDetail.Rows[n].Selected = true;

                MessageBoxUI _mb1 = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                _mb1.showDialog();

                computeTotalAmount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateData(string[] pRecordData)
        {
            try
            {
                string _operator = dgvDetail.CurrentRow.Cells["Status"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetail.CurrentRow.Cells["Status"].Value = "Add";
                }
                computeTotalAmount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void computeTotalAmount()
        {
            try
            {
                decimal _TotalQty = 0;
                decimal _TotalAmount = 0;

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalQty += decimal.Parse(dgvDetail.Rows[i].Cells["Qty"].Value.ToString());
                            _TotalAmount += decimal.Parse(dgvDetail.Rows[i].Cells["TotalPrice"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalQty.Text = string.Format("{0:n}", _TotalQty);
                txtTotalAmount.Text = string.Format("{0:n}", _TotalAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PurchaseRequestDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

                try
                {
                    cboSupplier.DataSource = loSupplier.getAllData("ViewAll", "", "");
                    cboSupplier.DisplayMember = "Name";
                    cboSupplier.ValueMember = "Id";
                    cboSupplier.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loPurchaseRequest.getAllData("", lPurchaseRequestId, "").Rows)
                    {
                        txtId.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        cboSupplier.Text = _dr["Supplier"].ToString();
                        txtTerms.Text = _dr["Terms"].ToString();
                        txtInstructions.Text = _dr["Instructions"].ToString();
                        dtpDateNeeded.Value = GlobalFunctions.ConvertToDate(_dr["Date Needed"].ToString());
                        cboRequestedBy.Text = _dr["Requested By"].ToString();
                        txtTotalQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty"].ToString()));
                        txtTotalAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        foreach (DataRow _drDetails in loPurchaseRequestDetail.getPurchaseRequestDetails("",lPurchaseRequestId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();

                            dgvDetail.Rows[i].Cells["Qty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty"].ToString()));
                            dgvDetail.Rows[i].Cells["UnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Unit Price"].ToString()));
                            dgvDetail.Rows[i].Cells["TotalPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Total Price"].ToString()));

                            dgvDetail.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetail.Rows[i].Cells["Status"].Value = "Saved";
                        }
                        computeTotalAmount();
                    }
                }
                else
                {
                    foreach (DataRow _dr in loCommon.getNextTabelSequenceId("PurchaseRequest").Rows)
                    {
                        txtId.Text = _dr[0].ToString();
                    }
                    cboRequestedBy.SelectedIndex = -1;
                    cboSupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PurchaseRequestDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Saved" || dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Edit")
                {
                    dgvDetail.CurrentRow.Cells["Status"].Value = "Delete";
                    dgvDetail.CurrentRow.Visible = false;
                }
                else if (dgvDetail.CurrentRow.Cells["Status"].Value.ToString() == "Add")
                {
                    if (this.dgvDetail.SelectedRows.Count > 0)
                    {
                        dgvDetail.Rows.RemoveAt(this.dgvDetail.SelectedRows[0].Index);
                    }
                }
                computeTotalAmount();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDelete_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Saved" || dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                    {
                        dgvDetail.Rows[i].Cells["Status"].Value = "Delete";
                        dgvDetail.Rows[i].Visible = false;
                    }
                    else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                    {
                        dgvDetail.Rows.RemoveAt(this.dgvDetail.Rows[i].Index);
                    }
                }
                computeTotalAmount();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAll_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseRequestStockDetailUI loPurchaseRequestStockDetail = new PurchaseRequestStockDetailUI();
                loPurchaseRequestStockDetail.ParentList = this;
                loPurchaseRequestStockDetail.ShowDialog();
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseRequestStockDetailUI loPurchaseRequestStockDetail = new PurchaseRequestStockDetailUI(
                    dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Qty"].Value.ToString()),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["UnitPrice"].Value.ToString()),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["TotalPrice"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loPurchaseRequestStockDetail.ParentList = this;
                loPurchaseRequestStockDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEdit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtTotalQty.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _SupplierId = "";
                string _RequestedBy = "";
                try
                {
                    _SupplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    _SupplierId = "";
                }
                try
                {
                    _RequestedBy = cboRequestedBy.SelectedValue.ToString();
                }
                catch
                {
                    _RequestedBy = "";
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Purchase Request?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loPurchaseRequest.Id = lPurchaseRequestId;
                    loPurchaseRequest.Date = dtpDate.Value;
                    loPurchaseRequest.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loPurchaseRequest.SupplierId = _SupplierId;
                    try
                    {
                        loPurchaseRequest.Terms = int.Parse(txtTerms.Text);
                    }
                    catch
                    {
                        loPurchaseRequest.Terms = 0;
                    }
                    loPurchaseRequest.Instructions = GlobalFunctions.replaceChar(txtInstructions.Text);
                    loPurchaseRequest.RequestedBy = _RequestedBy;
                    loPurchaseRequest.DateNeeded = dtpDateNeeded.Value;
                    loPurchaseRequest.TotalQty = decimal.Parse(txtTotalQty.Text);
                    loPurchaseRequest.TotalAmount = decimal.Parse(txtTotalAmount.Text);
                    loPurchaseRequest.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loPurchaseRequest.UserId = GlobalVariables.UserId;

                    string _PurchaseRequestId = loPurchaseRequest.save(lOperation);
                    if (_PurchaseRequestId != "")
                    {
                        for (int i = 0; i < dgvDetail.Rows.Count; i++)
                        {
                            if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                            {
                                try
                                {
                                    loPurchaseRequestDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    loPurchaseRequestDetail.DetailId = "";
                                }
                                try
                                {
                                    loPurchaseRequestDetail.PurchaseRequestId = _PurchaseRequestId;
                                    loPurchaseRequestDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                    loPurchaseRequestDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                    loPurchaseRequestDetail.Qty = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                    loPurchaseRequestDetail.UnitPrice = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                    loPurchaseRequestDetail.TotalPrice = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
                                    loPurchaseRequestDetail.Remarks = dgvDetail.Rows[i].Cells[10].Value.ToString();
                                    loPurchaseRequestDetail.UserId = GlobalVariables.UserId;
                                    loPurchaseRequestDetail.save(GlobalVariables.Operation.Add);
                                }
                                catch { }
                            }
                            else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                            {
                                try
                                {
                                    loPurchaseRequestDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    loPurchaseRequestDetail.DetailId = "";
                                }
                                loPurchaseRequestDetail.PurchaseRequestId = _PurchaseRequestId;
                                loPurchaseRequestDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                loPurchaseRequestDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                loPurchaseRequestDetail.Qty = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                loPurchaseRequestDetail.UnitPrice = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                loPurchaseRequestDetail.TotalPrice = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
                                loPurchaseRequestDetail.Remarks = dgvDetail.Rows[i].Cells[10].Value.ToString();
                                loPurchaseRequestDetail.UserId = GlobalVariables.UserId;
                                loPurchaseRequestDetail.save(GlobalVariables.Operation.Edit);

                            }
                            else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                            {
                                loPurchaseRequestDetail.remove(dgvDetail.Rows[i].Cells[0].Value.ToString());
                            }
                        }
                        if (txtId.Text == _PurchaseRequestId)
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Purchase Request has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }
                        else
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Purchase Request has been saved successfully! New Purchase Request ID. : " + _PurchaseRequestId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }

                        //previewDetail(_PurchaseRequestId);

                        object[] _params = { };
                        ParentList.GetType().GetMethod("refresh").Invoke(ParentList, _params);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataRow _dr in loSupplier.getAllData("",cboSupplier.SelectedValue.ToString(),"").Rows)
                {
                    txtAddress.Text = _dr["Address"].ToString();
                    txtContactPerson.Text = _dr["Contact Person"].ToString();
                }
            }
            catch
            {
                txtAddress.Clear();
                txtContactPerson.Clear();
            }
        }
    }
}
