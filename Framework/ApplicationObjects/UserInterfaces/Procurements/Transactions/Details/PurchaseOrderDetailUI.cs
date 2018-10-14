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
    public partial class PurchaseOrderDetailUI : Form
    {
        #region "VARIABLES"
        PurchaseOrder loPurchaseOrder;
        PurchaseOrderDetail loPurchaseOrderDetail;
        PurchaseRequest loPurchaseRequest;
        PurchaseRequestDetail loPurchaseRequestDetail;
        Supplier loSupplier;
        Stock loStock;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        LookUpPurchaseRequestUI loLookUpPurchaseRequest;
        ReportViewerUI loReportViewer;
        string lPurchaseOrderId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public PurchaseOrderDetailUI()
        {
            InitializeComponent();
            loPurchaseOrder = new PurchaseOrder();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loPurchaseRequest = new PurchaseRequest();
            loPurchaseRequestDetail = new PurchaseRequestDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            loLookUpPurchaseRequest = new LookUpPurchaseRequestUI();
            loReportViewer = new ReportViewerUI();
            lPurchaseOrderId = "";
        }
        public PurchaseOrderDetailUI(string pPurchaseOrderId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loPurchaseOrder = new PurchaseOrder();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loPurchaseRequest = new PurchaseRequest();
            loPurchaseRequestDetail = new PurchaseRequestDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loCommon = new Common();
            loLookUpPurchaseRequest = new LookUpPurchaseRequestUI();
            loReportViewer = new ReportViewerUI();
            lPurchaseOrderId = pPurchaseOrderId;
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
                for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                {
                    if (dgvDetailStockInventory.Rows[i].Cells[1].Value.ToString() == pRecordData[1])
                    {
                        MessageBoxUI _mb = new MessageBoxUI("Duplicate", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                        _mb.showDialog();
                        return;
                    }
                }
                
                int n = dgvDetailStockInventory.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailStockInventory.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetailStockInventory.CurrentRow.Selected = false;
                dgvDetailStockInventory.FirstDisplayedScrollingRowIndex = dgvDetailStockInventory.Rows[n].Index;
                dgvDetailStockInventory.Rows[n].Selected = true;

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
                string _operator = dgvDetailStockInventory.CurrentRow.Cells["Status"].Value.ToString();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetailStockInventory.CurrentRow.Cells[i].Value = pRecordData[i];
                }
                if (_operator == "Add")
                {
                    dgvDetailStockInventory.CurrentRow.Cells["Status"].Value = "Add";
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
                decimal _TotalPOQty = 0;
                decimal _TotalQtyIn = 0;
                decimal _TotalQtyVariance = 0;
                decimal _TotalAmount = 0;

                for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                {
                    if (dgvDetailStockInventory.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalPOQty += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["POQty"].Value.ToString());
                            _TotalQtyIn += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyIn"].Value.ToString());
                            _TotalQtyVariance += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                            _TotalAmount += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                        }
                        catch { }
                    }
                }
                lblTotalPOQty.Text = string.Format("{0:n}", _TotalPOQty);
                lblTotalQtyIn.Text = string.Format("{0:n}", _TotalQtyIn);
                lblTotalQtyVariance.Text = string.Format("{0:n}", _TotalQtyVariance);
                txtTotalAmount.Text = string.Format("{0:n}", _TotalAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PurchaseOrderDetailUI_Load(object sender, EventArgs e)
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
                    foreach (DataRow _dr in loPurchaseOrder.getAllData("", lPurchaseOrderId, "").Rows)
                    {
                        txtId.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        try
                        {
                            cboSupplier.SelectedValue = _dr["SupplierId"].ToString();
                        }
                        catch
                        {
                            cboSupplier.Text = "";
                        }
                        cboPurchaseRequest.Text = _dr["P.R. Id"].ToString();
                        txtTerms.Text = _dr["Terms"].ToString();
                        dtpDueDate.Value = GlobalFunctions.ConvertToDate(_dr["Due Date"].ToString());
                        txtInstructions.Text = _dr["Instructions"].ToString();
                        try
                        {
                            cboRequestedBy.SelectedValue = _dr["RequestedBy"].ToString();
                        }
                        catch
                        {
                            cboRequestedBy.Text = "";
                        }
                        dtpDateNeeded.Value = GlobalFunctions.ConvertToDate(_dr["Date Needed"].ToString());
                        lblTotalPOQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total P.O. Qty"].ToString()));
                        lblTotalQtyIn.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty In"].ToString()));
                        lblTotalQtyVariance.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty Variance"].ToString()));
                        txtTotalAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();

                        foreach (DataRow _drDetails in loPurchaseOrderDetail.getPurchaseOrderDetails("",lPurchaseOrderId).Rows)
                        {
                            int i = dgvDetailStockInventory.Rows.Add();
                            dgvDetailStockInventory.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["POQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["P.O. Qty"].ToString()));
                            dgvDetailStockInventory.Rows[i].Cells["QtyIn"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty In"].ToString()));
                            dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Variance"].ToString()));
                            dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Unit Price"].ToString()));

                            dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value = _drDetails["DiscountId"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["Discount"].Value = _drDetails["Discount"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Discount Amount"].ToString()));

                            dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Total Price"].ToString()));
                            dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetailStockInventory.Rows[i].Cells["Status"].Value = "Saved";
                        }
                        computeTotalAmount();
                        cboSupplier.Enabled = false;
                        cboPurchaseRequest.Enabled = false;
                        btnLookUpPurchaseRequest.Enabled = false;
                    }
                }
                else
                {
                    foreach (DataRow _dr in loCommon.getNextTabelSequenceId("PurchaseOrder").Rows)
                    {
                        txtId.Text = _dr[0].ToString();
                    }
                    cboSupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PurchaseOrderDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetailStockInventory.CurrentRow.Cells["Status"].Value.ToString() == "Saved" || dgvDetailStockInventory.CurrentRow.Cells["Status"].Value.ToString() == "Edit")
                {
                    dgvDetailStockInventory.CurrentRow.Cells["Status"].Value = "Delete";
                    dgvDetailStockInventory.CurrentRow.Visible = false;
                }
                else if (dgvDetailStockInventory.CurrentRow.Cells["Status"].Value.ToString() == "Add")
                {
                    if (this.dgvDetailStockInventory.SelectedRows.Count > 0)
                    {
                        dgvDetailStockInventory.Rows.RemoveAt(this.dgvDetailStockInventory.SelectedRows[0].Index);
                    }
                }
                computeTotalAmount();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnDeleteAllStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                {
                    if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Saved" || dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                    {
                        dgvDetailStockInventory.Rows[i].Cells["Status"].Value = "Delete";
                        dgvDetailStockInventory.Rows[i].Visible = false;
                    }
                    else if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Add")
                    {
                        dgvDetailStockInventory.Rows.RemoveAt(this.dgvDetailStockInventory.Rows[i].Index);
                    }
                }
                computeTotalAmount();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAllStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAddStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                ProcurementStockTransactionDetailUI loStockTransactionDetail = new ProcurementStockTransactionDetailUI();
                loStockTransactionDetail.ParentList = this;
                loStockTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAddStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEditStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                ProcurementStockTransactionDetailUI loStockTransactionDetail = new ProcurementStockTransactionDetailUI(dgvDetailStockInventory.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetailStockInventory.CurrentRow.Cells["StockId"].Value.ToString(),
                    dgvDetailStockInventory.CurrentRow.Cells["LocationId"].Value.ToString(),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["POQty"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["QtyIn"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["QtyVariance"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["UnitPrice"].Value.ToString()),
                    dgvDetailStockInventory.CurrentRow.Cells["DiscountId"].Value.ToString(),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["DiscountAmount"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["TotalPrice"].Value.ToString()),
                    dgvDetailStockInventory.CurrentRow.Cells["Remarks"].Value.ToString());
                loStockTransactionDetail.ParentList = this;
                loStockTransactionDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEditStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetailStockInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditStockInventory_Click(null, new EventArgs());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(lblTotalPOQty.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of P.O. Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _SupplierId = "";
                string _Employee = "";
                try
                {
                    _SupplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("You must select a Supplier!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    cboSupplier.Focus();
                    return;
                }
                try
                {
                    _Employee = cboRequestedBy.SelectedValue.ToString();
                }
                catch
                {
                    _Employee = "";
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Purchase Order?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loPurchaseOrder.Id = lPurchaseOrderId;
                    loPurchaseOrder.Date = dtpDate.Value;
                    try
                    {
                        loPurchaseOrder.PRId = cboPurchaseRequest.SelectedValue.ToString();
                    }
                    catch
                    {
                        loPurchaseOrder.PRId = "";
                    }

                    loPurchaseOrder.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loPurchaseOrder.SupplierId = _SupplierId;
                    try
                    {
                        loPurchaseOrder.Terms = int.Parse(txtTerms.Text);
                    }
                    catch
                    {
                        loPurchaseOrder.Terms = 0;
                    }
                    loPurchaseOrder.DueDate = dtpDueDate.Value;
                    loPurchaseOrder.Instructions = GlobalFunctions.replaceChar(txtInstructions.Text);
                    loPurchaseOrder.RequestedBy = _Employee;
                    loPurchaseOrder.DateNeeded = dtpDateNeeded.Value;
                    loPurchaseOrder.TotalPOQty = decimal.Parse(lblTotalPOQty.Text);
                    loPurchaseOrder.TotalQtyIn = decimal.Parse(lblTotalQtyIn.Text);
                    loPurchaseOrder.TotalQtyVariance = decimal.Parse(lblTotalQtyVariance.Text);
                    loPurchaseOrder.TotalAmount = decimal.Parse(txtTotalAmount.Text);
                    loPurchaseOrder.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loPurchaseOrder.UserId = GlobalVariables.UserId;

                    string _PurchaseOrderId = loPurchaseOrder.save(lOperation);
                    if (_PurchaseOrderId != "")
                    {
                        for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                        {
                            if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Add")
                            {
                                try
                                {
                                    loPurchaseOrderDetail.DetailId = dgvDetailStockInventory.Rows[i].Cells["Id"].Value.ToString();
                                }
                                catch
                                {
                                    loPurchaseOrderDetail.DetailId = "";
                                }
                                try
                                {
                                    loPurchaseOrderDetail.PurchaseOrderId = _PurchaseOrderId;
                                    loPurchaseOrderDetail.StockId = dgvDetailStockInventory.Rows[i].Cells["StockId"].Value.ToString();
                                    loPurchaseOrderDetail.LocationId = dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value.ToString();
                                    loPurchaseOrderDetail.POQty = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["POQty"].Value.ToString());
                                    loPurchaseOrderDetail.QtyIn = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyIn"].Value.ToString());
                                    loPurchaseOrderDetail.QtyVariance = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                                    loPurchaseOrderDetail.UnitPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value.ToString());
                                    loPurchaseOrderDetail.DiscountId = dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value.ToString();
                                    loPurchaseOrderDetail.DiscountAmount = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value.ToString());
                                    loPurchaseOrderDetail.TotalPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                                    loPurchaseOrderDetail.Remarks = dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value.ToString();
                                    loPurchaseOrderDetail.UserId = GlobalVariables.UserId;
                                    loPurchaseOrderDetail.save(GlobalVariables.Operation.Add);
                                }
                                catch { }
                            }
                            else if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                            {
                                try
                                {
                                    loPurchaseOrderDetail.DetailId = dgvDetailStockInventory.Rows[i].Cells["Id"].Value.ToString();
                                }
                                catch
                                {
                                    loPurchaseOrderDetail.DetailId = "";
                                }
                                loPurchaseOrderDetail.PurchaseOrderId = _PurchaseOrderId;
                                loPurchaseOrderDetail.StockId = dgvDetailStockInventory.Rows[i].Cells["StockId"].Value.ToString();
                                loPurchaseOrderDetail.LocationId = dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value.ToString();
                                loPurchaseOrderDetail.POQty = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["POQty"].Value.ToString());
                                loPurchaseOrderDetail.QtyIn = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyIn"].Value.ToString());
                                loPurchaseOrderDetail.QtyVariance = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                                loPurchaseOrderDetail.UnitPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value.ToString());
                                loPurchaseOrderDetail.DiscountId = dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value.ToString();
                                loPurchaseOrderDetail.DiscountAmount = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value.ToString());
                                loPurchaseOrderDetail.TotalPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                                loPurchaseOrderDetail.Remarks = dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value.ToString();
                                loPurchaseOrderDetail.UserId = GlobalVariables.UserId;
                                loPurchaseOrderDetail.save(GlobalVariables.Operation.Edit);

                            }
                            else if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                            {
                                loPurchaseOrderDetail.remove(dgvDetailStockInventory.Rows[i].Cells[0].Value.ToString());
                            }
                        }
                        if (txtId.Text == _PurchaseOrderId)
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Purchase Order has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }
                        else
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Purchase Order has been saved successfully! New Purchase Order ID. : " + _PurchaseOrderId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }

                        //previewDetail(_PurchaseOrderId);

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
                cboPurchaseRequest.DataSource = loPurchaseRequest.getPurchaseRequestBySupplier(cboSupplier.SelectedValue.ToString(),"");
                cboPurchaseRequest.DisplayMember = "Id";
                cboPurchaseRequest.ValueMember = "Id";
                cboPurchaseRequest.SelectedIndex = -1;
            }
            catch
            {
                cboPurchaseRequest.DataSource = null;
            }
        }

        private void cboPurchaseRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loPurchaseRequest.getAllData("", cboPurchaseRequest.SelectedValue.ToString(), "").Rows)
                {
                    txtReference.Text = _dr["Reference"].ToString();
                    txtTerms.Text = _dr["Terms"].ToString();
                    dtpDueDate.Value = DateTime.Now.AddDays(double.Parse(txtTerms.Text));
                    cboRequestedBy.Text = _dr["Requested By"].ToString();
                    txtInstructions.Text = _dr["Instructions"].ToString();
                }
                if (lOperation == GlobalVariables.Operation.Add)
                {
                    dgvDetailStockInventory.Rows.Clear();
                    foreach (DataRow _drDetails in loPurchaseRequestDetail.getPurchaseRequestDetails("",cboPurchaseRequest.SelectedValue.ToString()).Rows)
                    {
                        int i = dgvDetailStockInventory.Rows.Add();
                        dgvDetailStockInventory.Rows[i].Cells["Id"].Value = "";
                        dgvDetailStockInventory.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["POQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["QtyIn"].Value = "0.00";
                        dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Unit Price"].ToString()));

                        dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value = "";
                        dgvDetailStockInventory.Rows[i].Cells["Discount"].Value = "";
                        dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value = "0.00";

                        dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Total Price"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Status"].Value = "Add";
                    }
                }
            }
            catch
            {
                dgvDetailStockInventory.Rows.Clear();
            }

            computeTotalAmount();
        }

        private void btnLookUpPurchaseRequest_Click(object sender, EventArgs e)
        {
            try
            {
                string _SupplierId;
                try
                {
                    _SupplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    _SupplierId = "";
                }
                LookUpPurchaseRequestUI loLookUpPurchaseRequest = new LookUpPurchaseRequestUI();
                loLookUpPurchaseRequest.lSupplierId = _SupplierId;
                loLookUpPurchaseRequest.ShowDialog();
                if (loLookUpPurchaseRequest.lFromSelection)
                {
                    cboPurchaseRequest.Text = loLookUpPurchaseRequest.lId;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnLookUpPurchaseRequest_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtTerms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtpDueDate.Value = DateTime.Now.AddDays(int.Parse(txtTerms.Text));
            }
            catch
            {
                dtpDueDate.Value = DateTime.Now;
            }
        }
    }
}
