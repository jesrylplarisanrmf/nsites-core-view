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

using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class StockReceivingDetailUI : Form
    {
        #region "VARIABLES"
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        Supplier loSupplier;
        Stock loStock;
        PurchaseOrder loPurchaseOrder;
        PurchaseOrderDetail loPurchaseOrderDetail;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        //StockReceivingDetailRpt loStockReceivingDetailRpt;
        ReportViewerUI loReportViewer;
        string lInventoryId;
        string lPurchaseOrderId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockReceivingDetailUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loPurchaseOrder = new PurchaseOrder();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = "";
            lPurchaseOrderId = "";
        }
        public StockReceivingDetailUI(string pInventoryId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loSupplier = new Supplier();
            loStock = new Stock();
            loPurchaseOrder = new PurchaseOrder();
            loPurchaseOrderDetail = new PurchaseOrderDetail();
            loCommon = new Common();
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = pInventoryId;
            lPurchaseOrderId = "";
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
                int n = dgvDetail.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvDetail.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvDetail.CurrentRow.Selected = false;
                dgvDetail.FirstDisplayedScrollingRowIndex = dgvDetail.Rows[n].Index;
                dgvDetail.Rows[n].Selected = true;

                computeTotalQty();
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
                computeTotalQty();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void computeTotalQty()
        {
            try
            {
                decimal _TotalPOQty = 0;
                decimal _TotalQtyIN = 0;
                decimal _TotalVariance = 0;

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalPOQty += decimal.Parse(dgvDetail.Rows[i].Cells["POQty"].Value.ToString());
                            _TotalQtyIN += decimal.Parse(dgvDetail.Rows[i].Cells["QtyIN"].Value.ToString());
                            _TotalVariance += decimal.Parse(dgvDetail.Rows[i].Cells["Variance"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalPOQty.Text = string.Format("{0:n}", _TotalPOQty);
                txtTotalQtyIn.Text = string.Format("{0:n}", _TotalQtyIN);
                txtTotalVariance.Text = string.Format("{0:n}", _TotalVariance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StockReceivingDetailUI_Load(object sender, EventArgs e)
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
                    foreach (DataRow _dr in loInventory.getAllData("", "", lInventoryId, "").Rows)
                    {
                        txtId.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        lPurchaseOrderId = _dr["P.O. Id"].ToString();
                        cboSupplier.Text = _dr["Supplier"].ToString();
                        cboPurchaseOrder.Text = _dr["P.O. Id"].ToString();
                        txtTotalPOQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total P.O. Qty"].ToString()));
                        txtTotalQtyIn.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty In"].ToString()));
                        txtTotalVariance.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Variance"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        foreach (DataRow _drDetails in loInventoryDetail.getInventoryDetails("",lInventoryId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["PODetailId"].Value = _drDetails["PODetailId"].ToString();
                            dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                            dgvDetail.Rows[i].Cells["POQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["P.O. Qty"].ToString()));
                            dgvDetail.Rows[i].Cells["QtyIn"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty In"].ToString()));
                            dgvDetail.Rows[i].Cells["Variance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Variance"].ToString()));
                            dgvDetail.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetail.Rows[i].Cells["Status"].Value = "Saved";
                        }
                        computeTotalQty();
                    }
                }
                else
                {
                    foreach (DataRow _dr in loCommon.getNextTabelSequenceId("Inventory").Rows)
                    {
                        txtId.Text = _dr[0].ToString();
                    }
                    cboSupplier.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockReceivingDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtTotalQtyIn.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _SupplierId = "";
                try
                {
                    _SupplierId = cboSupplier.SelectedValue.ToString();
                }
                catch
                {
                    _SupplierId = "";
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Stock Receiving?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loInventory.Id = lInventoryId;
                    loInventory.Date = dtpDate.Value;
                    loInventory.Type = "Stock Receiving";
                    try
                    {
                        loInventory.POId = cboPurchaseOrder.Text;
                    }
                    catch
                    {
                        loInventory.POId = "";
                    }
                    loInventory.SOId = "";
                    loInventory.STInId = "";
                    loInventory.STOutId = "";
                    loInventory.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loInventory.CustomerId = "0";
                    loInventory.SupplierId = _SupplierId;
                    loInventory.TotalPOQty = decimal.Parse(txtTotalPOQty.Text);
                    loInventory.TotalQtyIn = decimal.Parse(txtTotalQtyIn.Text);
                    loInventory.TotalSOQty = 0;
                    loInventory.TotalQtyOut = 0;
                    loInventory.TotalVariance = decimal.Parse(txtTotalVariance.Text);
                    loInventory.FromLocationId = "";
                    loInventory.ToLocationId = "";
                    loInventory.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loInventory.UserId = GlobalVariables.UserId;

                    try
                    {
                        string _InventoryId = loInventory.save(lOperation);
                        if (_InventoryId != "")
                        {
                            for (int i = 0; i < dgvDetail.Rows.Count; i++)
                            {
                                if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                                {
                                    try
                                    {
                                        loInventoryDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        loInventoryDetail.DetailId = "";
                                    }
                                    try
                                    {
                                        loInventoryDetail.InventoryId = _InventoryId;
                                        loInventoryDetail.PODetailId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.SODetailId = "0";
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[2].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[6].Value.ToString();
                                        loInventoryDetail.POQty = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                        loInventoryDetail.QtyIn = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = 0;
                                        loInventoryDetail.Variance = decimal.Parse(dgvDetail.Rows[i].Cells[10].Value.ToString());
                                        loInventoryDetail.Remarks = dgvDetail.Rows[i].Cells[11].Value.ToString();
                                        loInventoryDetail.UserId = GlobalVariables.UserId;
                                        loInventoryDetail.save(GlobalVariables.Operation.Add);
                                    }
                                    catch { }
                                }
                                else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                                {
                                    try
                                    {
                                        loInventoryDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                    }
                                    catch
                                    {
                                        loInventoryDetail.DetailId = "";
                                    }
                                    try
                                    {
                                        loInventoryDetail.InventoryId = _InventoryId;
                                        loInventoryDetail.PODetailId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.SODetailId = "0";
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[2].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[6].Value.ToString();
                                        loInventoryDetail.POQty = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                        loInventoryDetail.QtyIn = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = 0;
                                        loInventoryDetail.Variance = decimal.Parse(dgvDetail.Rows[i].Cells[10].Value.ToString());
                                        loInventoryDetail.Remarks = dgvDetail.Rows[i].Cells[11].Value.ToString();
                                        loInventoryDetail.UserId = GlobalVariables.UserId;
                                        loInventoryDetail.save(GlobalVariables.Operation.Edit);
                                    }
                                    catch { }
                                }
                                else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                                {
                                    loInventoryDetail.remove(dgvDetail.Rows[i].Cells[0].Value.ToString());
                                }
                            }
                            if (txtId.Text == _InventoryId)
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Receiving has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }
                            else
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Receiving has been saved successfully! New Stock Receiving Id : " + _InventoryId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }

                            //previewDetail(_InventoryId);

                            object[] _params = { "Stock Receiving" };
                            ParentList.GetType().GetMethod("refresh").Invoke(ParentList, _params);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Unclosed quotation mark after the character string"))
                        {
                            MessageBoxUI _mb3 = new MessageBoxUI("Do not use this character( ' ).", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb3.showDialog();
                            return;
                        }
                        else
                        {
                            MessageBoxUI _mb3 = new MessageBoxUI(ex.Message, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            _mb3.showDialog();
                            return;
                        }
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
                cboPurchaseOrder.DataSource = null;
                cboPurchaseOrder.DataSource = loPurchaseOrder.getPurchaseOrderBySupplier(cboSupplier.SelectedValue.ToString(), "");
                cboPurchaseOrder.DisplayMember = "Id";
                cboPurchaseOrder.ValueMember = "Id";
                cboPurchaseOrder.SelectedIndex = -1;
            }
            catch
            {
                cboPurchaseOrder.DataSource = null;
            }
        }

        private void cboPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _POId = "";
                try
                {
                    _POId = cboPurchaseOrder.SelectedValue.ToString();
                }
                catch { }
                if (_POId != "")
                {
                    foreach (DataRow _dr in loPurchaseOrder.getAllData("", _POId, "").Rows)
                    {
                        txtReference.Text = _dr["Reference"].ToString();
                    }
                    if (lOperation == GlobalVariables.Operation.Add)
                    {
                        dgvDetail.Rows.Clear();
                        foreach (DataRow _drDetails in loPurchaseOrderDetail.getPurchaseOrderDetails("",_POId).Rows)
                        {
                            if (decimal.Parse(_drDetails["Qty Variance"].ToString()) > 0)
                            {
                                int i = dgvDetail.Rows.Add();
                                dgvDetail.Rows[i].Cells["Id"].Value = "";
                                dgvDetail.Rows[i].Cells["PODetailId"].Value = _drDetails["Id"].ToString();
                                dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                                dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                                dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                                dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                                dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                                dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                                dgvDetail.Rows[i].Cells["POQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Variance"].ToString()));
                                dgvDetail.Rows[i].Cells["QtyIn"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Variance"].ToString()));

                                dgvDetail.Rows[i].Cells["Variance"].Value = "0.00";
                                dgvDetail.Rows[i].Cells["Remarks"].Value = "";
                                dgvDetail.Rows[i].Cells["Status"].Value = "Add";
                            }
                        }
                    }
                }
            }
            catch
            {
                dgvDetail.Rows.Clear();
            }

            computeTotalQty();
        }

        private void btnLookUpPurchaseOrder_Click(object sender, EventArgs e)
        {
            string _supplierId;
            try
            {
                _supplierId = cboSupplier.SelectedValue.ToString();
            }
            catch
            {
                _supplierId = "";
            }
            LookUpPurchaseOrderUI loLookUpPurchaseOrder = new LookUpPurchaseOrderUI();
            loLookUpPurchaseOrder.lSupplierId = _supplierId;
            loLookUpPurchaseOrder.ShowDialog();
            if (loLookUpPurchaseOrder.lFromSelection)
            {
                cboPurchaseOrder.Text = loLookUpPurchaseOrder.lId;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StockReceivingItemDetailUI loStockReceivingItemDetailUI = new StockReceivingItemDetailUI(
                dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["PODetailId"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                decimal.Parse(dgvDetail.CurrentRow.Cells["POQty"].Value.ToString()),
                decimal.Parse(dgvDetail.CurrentRow.Cells["QtyIn"].Value.ToString()),
                decimal.Parse(dgvDetail.CurrentRow.Cells["Variance"].Value.ToString()),
                dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loStockReceivingItemDetailUI.ParentList = this;
                loStockReceivingItemDetailUI.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEdit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                StockReceivingItemDetailUI loStockReceivingItemDetailUI = new StockReceivingItemDetailUI();
                loStockReceivingItemDetailUI.ParentList = this;
                loStockReceivingItemDetailUI.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAdd_Click");
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
                computeTotalQty();
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
                computeTotalQty();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnDeleteAll_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }
    }
}
