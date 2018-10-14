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
    public partial class StockTransferInDetailUI : Form
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
        public StockTransferInDetailUI()
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lInventoryId = "";
        }
        public StockTransferInDetailUI(string pInventoryId)
        {
            InitializeComponent();
            loInventory = new Inventory();
            loInventoryDetail = new InventoryDetail();
            loLocation = new Location();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Edit;
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
                decimal _TotalQtyIn = 0;

                for (int i = 0; i < dgvDetail.Rows.Count; i++)
                {
                    if (dgvDetail.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalQtyIn += decimal.Parse(dgvDetail.Rows[i].Cells["QtyIn"].Value.ToString());
                        }
                        catch { }
                    }
                }
                txtTotalQtyIN.Text = string.Format("{0:n}", _TotalQtyIn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void StockTransferInDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                try
                {
                    cboStockTransferOut.DataSource = loInventory.getStockTransferOut(GlobalVariables.CurrentLocationId,"");
                    cboStockTransferOut.ValueMember = "Id";
                    cboStockTransferOut.DisplayMember = "Id";
                    cboStockTransferOut.SelectedIndex = -1;
                }
                catch
                { }

                cboFromLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                cboFromLocation.DisplayMember = "Description";
                cboFromLocation.ValueMember = "Id";
                cboFromLocation.SelectedIndex = -1;

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loInventory.getAllData("", "", lInventoryId, "").Rows)
                    {
                        txtStockTransferOutNo.Text = _dr["Id"].ToString();
                        cboStockTransferOut.SelectedValue = _dr["S.T. Out Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        cboFromLocation.Text = _dr["Location From"].ToString();
                        txtTotalQtyIN.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty IN"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();

                        foreach (DataRow _drDetails in loInventoryDetail.getInventoryDetails("", lInventoryId).Rows)
                        {
                            int i = dgvDetail.Rows.Add();
                            dgvDetail.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                            dgvDetail.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                            dgvDetail.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                            dgvDetail.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                            dgvDetail.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                            dgvDetail.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                            dgvDetail.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                            dgvDetail.Rows[i].Cells["QtyIn"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty In"].ToString()));
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "StockTransferInDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StockTransferInItemDetailUI loStockTransferInItemDetail = new StockTransferInItemDetailUI();
            loStockTransferInItemDetail.ParentList = this;
            loStockTransferInItemDetail.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                StockTransferInItemDetailUI loStockTransferInItemDetail = new StockTransferInItemDetailUI(
                 dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                 dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                 dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                 decimal.Parse(dgvDetail.CurrentRow.Cells["QtyIn"].Value.ToString()),
                 dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loStockTransferInItemDetail.ParentList = this;
                loStockTransferInItemDetail.ShowDialog();
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

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            dgvDetail.Rows.Clear();
            computeTotalQty();
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit_Click(null, new EventArgs());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtTotalQtyIN.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
                }
                string _stockTransferOutId = "";
                try
                {
                    _stockTransferOutId = cboStockTransferOut.SelectedValue.ToString();
                }
                catch
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("You must select Stock Transfer Out Id!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    cboStockTransferOut.Focus();
                    return;
                }

                string _FromLocationId = "";
                try
                {
                    _FromLocationId = cboFromLocation.SelectedValue.ToString();
                }
                catch
                {
                    _FromLocationId = "";
                }

                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Stock Transfer In?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loInventory.Id = lInventoryId;
                    loInventory.Date = dtpDate.Value;
                    loInventory.Type = "Stock Transfer In";
                    loInventory.POId = "";
                    loInventory.SOId = "";
                    loInventory.STInId = "";
                    loInventory.STOutId = _stockTransferOutId;
                    loInventory.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loInventory.CustomerId = "0";
                    loInventory.SupplierId = "0";
                    loInventory.TotalPOQty = 0;
                    loInventory.TotalQtyIn = decimal.Parse(txtTotalQtyIN.Text);
                    loInventory.TotalSOQty = 0;
                    loInventory.TotalQtyOut = 0;
                    loInventory.TotalVariance = 0;
                    loInventory.FromLocationId = _FromLocationId;
                    loInventory.ToLocationId = GlobalVariables.CurrentLocationId;
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
                                        loInventoryDetail.QtyIn = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = 0;
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
                                        loInventoryDetail.QtyIn = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                        loInventoryDetail.SOQty = 0;
                                        loInventoryDetail.QtyOut = 0;
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
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Transfer In has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }
                            else
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Stock Transfer In has been saved successfully! New Stock Transfer In Id : " + _InventoryId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }

                            //previewDetail(_InventoryId);

                            object[] _params = { "Stock Transfer In" };
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

        private void btnLookUpStockTransferOutId_Click(object sender, EventArgs e)
        {
            LookUpStockTransferOutUI loLookUpStockTransferOut = new LookUpStockTransferOutUI();
            loLookUpStockTransferOut.ShowDialog();
            if (loLookUpStockTransferOut.lFromSelection)
            {
                cboStockTransferOut.Text = loLookUpStockTransferOut.lId;
            }
        }

        private void cboStockTransferOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lOperation == GlobalVariables.Operation.Add)
                {
                    foreach (DataRow _dr in loInventory.getAllData("", "", cboStockTransferOut.SelectedValue.ToString(), "").Rows)
                    {
                        txtReference.Text = _dr["Reference"].ToString();
                        cboFromLocation.Text = _dr["Location From"].ToString();
                    }
                    
                    dgvDetail.Rows.Clear();
                    foreach (DataRow _drDetails in loInventoryDetail.getInventoryDetails("", cboStockTransferOut.SelectedValue.ToString()).Rows)
                    {
                        int i = dgvDetail.Rows.Add();
                        dgvDetail.Rows[i].Cells[0].Value = _drDetails["Id"].ToString();
                        dgvDetail.Rows[i].Cells[1].Value = _drDetails["StockId"].ToString();
                        dgvDetail.Rows[i].Cells[2].Value = _drDetails["Stock Code"].ToString();
                        dgvDetail.Rows[i].Cells[3].Value = _drDetails["Stock Description"].ToString();
                        dgvDetail.Rows[i].Cells[4].Value = _drDetails["Unit"].ToString();
                        dgvDetail.Rows[i].Cells[5].Value = GlobalVariables.CurrentLocationId;
                        foreach (DataRow _dr in loLocation.getAllData("", GlobalVariables.CurrentLocationId, "").Rows)
                        {
                            dgvDetail.Rows[i].Cells[6].Value = _dr["Description"].ToString();
                        }
                        dgvDetail.Rows[i].Cells[7].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Out"].ToString()));
                        dgvDetail.Rows[i].Cells[8].Value = _drDetails["Remarks"].ToString();
                        dgvDetail.Rows[i].Cells[9].Value = "Add";
                    }
                }
            }
            catch
            {
                dgvDetail.Rows.Clear();
            }

            computeTotalQty();
        }
    }
}
