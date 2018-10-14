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
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions.Details
{
    public partial class StockTransferOutDetailUI : Form
    {
        #region "VARIABLES"
        Inventory loInventory;
        InventoryDetail loInventoryDetail;
        Location loLocation;
        Common loCommon;
        
        GlobalVariables.Operation lOperation;
        //StockReceivingDetailRpt loStockReceivingDetailRpt;
        ReportViewerUI loReportViewer;
        string lInventoryId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockTransferOutDetailUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            loCommon = new Common();
            //loInventoryType = new InventoryType();
            //loSupplier = new Supplier();
            //loCustomer = new Customer();
            //loStock = new Stock();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = "";
        }
        public StockTransferOutDetailUI(string pInventoryId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            loCommon = new Common();
            //loInventoryType = new InventoryType();
            //loSupplier = new Supplier();
            //loCustomer = new Customer();
            //loStock = new Stock();
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = pInventoryId;
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
                decimal _TotalQtyOUT = 0;

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalQtyOUT += decimal.Parse(dgvDetail.Rows[i].Cells["QtyOut"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalQtyOUT.Text = string.Format("{0:n}", _TotalQtyOUT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StockTransferOutDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboToLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                cboToLocation.DisplayMember = "Description";
                cboToLocation.ValueMember = "Id";
                cboToLocation.SelectedIndex = -1;
                
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loInventory.getAllData("", "", lInventoryId, "").Rows)
                    {
                        txtStockTransferOutNo.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        cboToLocation.Text = _dr["Location To"].ToString();
                        txtTotalQtyOUT.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty OuT"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();

                        foreach (DataRow _drDetails in loInventoryDetail.getInventoryDetails("",lInventoryId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                            dgvDetail.Rows[i].Cells["QtyOut"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Out"].ToString()));
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
                        txtStockTransferOutNo.Text = _dr[0].ToString();
                    }
                 }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockTransferOutDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                StockTransferOutItemDetailUI loStockTransferOutItemDetail = new StockTransferOutItemDetailUI();
                loStockTransferOutItemDetail.ParentList = this;
                loStockTransferOutItemDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAdd_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StockTransferOutItemDetailUI loStockTransferOutItemDetail = new StockTransferOutItemDetailUI(
                    dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["QtyOut"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loStockTransferOutItemDetail.ParentList = this;
                loStockTransferOutItemDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnEdit_Click");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                if (decimal.Parse(txtTotalQtyOUT.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }

                string _ToLocationId = "";
                try
                {
                    _ToLocationId = cboToLocation.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("You must select a Location To!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    cboToLocation.Focus();
                    return;
                }
                if (_ToLocationId == GlobalVariables.CurrentLocationId)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Location To must not the same with Current Location!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Stock Transfer Out?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loInventory.Id = lInventoryId;
                    loInventory.Date = dtpDate.Value;
                    loInventory.Type = "Stock Transfer Out";
                    loInventory.POId = "";
                    loInventory.SOId = "";
                    loInventory.STInId = "";
                    loInventory.STOutId = "";
                    loInventory.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loInventory.CustomerId = "0";
                    loInventory.SupplierId = "0";
                    loInventory.TotalPOQty = 0;
                    loInventory.TotalQtyIn = 0;
                    loInventory.TotalSOQty = 0;
                    loInventory.TotalQtyOut = decimal.Parse(txtTotalQtyOUT.Text);
                    loInventory.TotalVariance = 0;
                    loInventory.FromLocationId = GlobalVariables.CurrentLocationId;
                    loInventory.ToLocationId = _ToLocationId;
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
                                        loInventoryDetail.SODetailId = "0";
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                        loInventoryDetail.POQty = 0;
                                        loInventoryDetail.QtyIn = 0;
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                        loInventoryDetail.Variance = 0;
                                        loInventoryDetail.Remarks = dgvDetail.Rows[i].Cells[8].Value.ToString();
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
                                        loInventoryDetail.SODetailId = "0";
                                        loInventoryDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                        loInventoryDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                        loInventoryDetail.POQty = 0;
                                        loInventoryDetail.QtyIn = 0;
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                        loInventoryDetail.Variance = 0;
                                        loInventoryDetail.Remarks = dgvDetail.Rows[i].Cells[8].Value.ToString();
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
                            if (txtStockTransferOutNo.Text == _InventoryId)
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Transfer Out has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }
                            else
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Transfer Out has been saved successfully! New Stock Transfer Out Id : " + _InventoryId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }

                            //previewDetail(_InventoryId);

                            object[] _params = { "Stock Transfer Out" };
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
    }
}
