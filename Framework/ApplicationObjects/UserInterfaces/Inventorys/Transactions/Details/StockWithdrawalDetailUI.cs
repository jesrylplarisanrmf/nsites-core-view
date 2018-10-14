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
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class StockWithdrawalDetailUI : Form
    {
        #region "VARIABLES"
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        Customer loCustomer;
        Stock loStock;
        SalesOrder loSalesOrder;
        SalesOrderDetail loSalesOrderDetail;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        //StockReceivingDetailRpt loStockReceivingDetailRpt;
        ReportViewerUI loReportViewer;
        string lInventoryId;
        string lSalesOrderId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockWithdrawalDetailUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loCustomer = new Customer();
            loStock = new Stock();
            loSalesOrder = new SalesOrder();
            loSalesOrderDetail = new SalesOrderDetail();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = "";
            lSalesOrderId = "";
        }
        public StockWithdrawalDetailUI(string pInventoryId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loCustomer = new Customer();
            loStock = new Stock();
            loSalesOrder = new SalesOrder();
            loSalesOrderDetail = new SalesOrderDetail();
            loCommon = new Common();
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = pInventoryId;
            lSalesOrderId = "";
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
                decimal _TotalSOQty = 0;
                decimal _TotalQtyOUT = 0;
                decimal _TotalVariance = 0;

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalSOQty += decimal.Parse(dgvDetail.Rows[i].Cells["SOQty"].Value.ToString());
                            _TotalQtyOUT += decimal.Parse(dgvDetail.Rows[i].Cells["QtyOut"].Value.ToString());
                            _TotalVariance += decimal.Parse(dgvDetail.Rows[i].Cells["Variance"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalSOQty.Text = string.Format("{0:n}", _TotalSOQty);
                txtTotalQtyOut.Text = string.Format("{0:n}", _TotalQtyOUT);
                txtTotalVariance.Text = string.Format("{0:n}", _TotalVariance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StockWithdrawalDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                try
                {
                    cboCustomer.DataSource = loCustomer.getAllData("ViewAll", "", "");
                    cboCustomer.DisplayMember = "Name";
                    cboCustomer.ValueMember = "Id";
                    cboCustomer.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loInventory.getAllData("", "", lInventoryId, "").Rows)
                    {
                        txtId.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        lSalesOrderId = _dr["S.O. Id"].ToString();
                        cboCustomer.Text = _dr["Customer"].ToString();
                        cboSalesOrder.Text = _dr["S.O. Id"].ToString();
                        txtTotalSOQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total S.O. Qty"].ToString()));
                        txtTotalQtyOut.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty Out"].ToString()));
                        txtTotalVariance.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Variance"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        foreach (DataRow _drDetails in loInventoryDetail.getInventoryDetails("",lInventoryId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["SODetailId"].Value = _drDetails["SODetailId"].ToString();
                            dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                            dgvDetail.Rows[i].Cells["SOQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["S.O. Qty"].ToString()));
                            dgvDetail.Rows[i].Cells["QtyOut"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Out"].ToString()));
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
                    cboCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockWithdrawalDetailUI_Load");
                em.ShowDialog();
                return;
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtTotalQtyOut.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _CustomerId = "";
                try
                {
                    _CustomerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    _CustomerId = "";
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Stock Withdrawal?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loInventory.Id = lInventoryId;
                    loInventory.Date = dtpDate.Value;
                    loInventory.Type = "Stock Withdrawal";
                    loInventory.POId = "";
                    try
                    {
                        loInventory.SOId = cboSalesOrder.Text;
                    }
                    catch
                    {
                        loInventory.SOId = "";
                    }
                    loInventory.STInId = "";
                    loInventory.STOutId = "";
                    loInventory.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loInventory.CustomerId = _CustomerId;
                    loInventory.SupplierId = "0";
                    loInventory.TotalPOQty = 0;
                    loInventory.TotalQtyIn = 0;
                    loInventory.TotalSOQty = decimal.Parse(txtTotalSOQty.Text);
                    loInventory.TotalQtyOut = decimal.Parse(txtTotalQtyOut.Text);
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
                                        loInventoryDetail.PODetailId = "0";
                                        loInventoryDetail.SODetailId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[2].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[6].Value.ToString();
                                        loInventoryDetail.POQty = 0;
                                        loInventoryDetail.QtyIn = 0;
                                        loInventoryDetail.SOQty = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                        loInventoryDetail.QtyOut = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
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
                                        loInventoryDetail.PODetailId = "0";
                                        loInventoryDetail.SODetailId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[2].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[6].Value.ToString();
                                        loInventoryDetail.POQty = 0;
                                        loInventoryDetail.QtyIn = 0;
                                        loInventoryDetail.SOQty = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                        loInventoryDetail.QtyOut = decimal.Parse(dgvDetail.Rows[i].Cells[9].Value.ToString());
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
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Withdrawal has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }
                            else
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Withdrawal has been saved successfully! New Stock Withdrawal Id : " + _InventoryId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }

                            //previewDetail(_InventoryId);

                            object[] _params = { "Stock Withdrawal" };
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

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboSalesOrder.DataSource = null;
                cboSalesOrder.DataSource = loSalesOrder.getSalesOrderByCustomer(cboCustomer.SelectedValue.ToString(),"");
                cboSalesOrder.DisplayMember = "Id";
                cboSalesOrder.ValueMember = "Id";
                cboSalesOrder.SelectedIndex = -1;
            }
            catch
            {
                cboSalesOrder.DataSource = null;
            }
        }

        private void cboSalesOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _SOId = "";
                try
                {
                    _SOId = cboSalesOrder.SelectedValue.ToString();
                }
                catch { }
                if (_SOId != "")
                {
                    foreach (DataRow _dr in loSalesOrder.getAllData("", _SOId, "").Rows)
                    {
                        txtReference.Text = _dr["Reference"].ToString();
                    }
                    if (lOperation == GlobalVariables.Operation.Add)
                    {
                        dgvDetail.Rows.Clear();
                        foreach (DataRow _drDetails in loSalesOrderDetail.getSalesOrderDetails("",_SOId).Rows)
                        {
                            if (decimal.Parse(_drDetails["Qty Variance"].ToString()) > 0)
                            {
                                int i = dgvDetail.Rows.Add();
                                dgvDetail.Rows[i].Cells["Id"].Value = "";
                                dgvDetail.Rows[i].Cells["SODetailId"].Value = _drDetails["Id"].ToString();
                                dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                                dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                                dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                                dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                                dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                                dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                                dgvDetail.Rows[i].Cells["SOQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Variance"].ToString()));
                                dgvDetail.Rows[i].Cells["QtyOut"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Variance"].ToString()));

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

        private void btnLookUpSalesOrder_Click(object sender, EventArgs e)
        {
            string _customerId;
            try
            {
                _customerId = cboCustomer.SelectedValue.ToString();
            }
            catch
            {
                _customerId = "";
            }

            LookUpSalesOrderUI loLookUpSalesOrder = new LookUpSalesOrderUI();
            loLookUpSalesOrder.lCustomerId = _customerId;
            loLookUpSalesOrder.ShowDialog();
            if (loLookUpSalesOrder.lFromSelection)
            {
                cboSalesOrder.Text = loLookUpSalesOrder.lId;
            }
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StockWithdrawalItemDetailUI loStockWithdrawalItemDetailUI = new StockWithdrawalItemDetailUI(
                dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["SODetailId"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                decimal.Parse(dgvDetail.CurrentRow.Cells["SOQty"].Value.ToString()),
                decimal.Parse(dgvDetail.CurrentRow.Cells["QtyOut"].Value.ToString()),
                decimal.Parse(dgvDetail.CurrentRow.Cells["Variance"].Value.ToString()),
                dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loStockWithdrawalItemDetailUI.ParentList = this;
                loStockWithdrawalItemDetailUI.ShowDialog();
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
                StockWithdrawalItemDetailUI loStockWithdrawalItemDetailUI = new StockWithdrawalItemDetailUI();
                loStockWithdrawalItemDetailUI.ParentList = this;
                loStockWithdrawalItemDetailUI.ShowDialog();
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
    }
}
