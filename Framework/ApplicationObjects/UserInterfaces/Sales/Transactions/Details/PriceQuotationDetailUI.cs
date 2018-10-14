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
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details
{
    public partial class PriceQuotationDetailUI : Form
    {
        #region "VARIABLES"
        PriceQuotation loPriceQuotation;
        PriceQuotationDetail loPriceQuotationDetail;
        Customer loCustomer;
        SalesPerson loSalesPerson;
        Stock loStock;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        //StockReceivingDetailRpt loStockReceivingDetailRpt;
        ReportViewerUI loReportViewer;
        string lPriceQuotationId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public PriceQuotationDetailUI()
        {
            InitializeComponent();
            loPriceQuotation = new PriceQuotation();
            loPriceQuotationDetail = new PriceQuotationDetail();
            loCustomer = new Customer();
            loSalesPerson = new SalesPerson();
            loStock = new Stock();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lPriceQuotationId = "";
        }
        public PriceQuotationDetailUI(string pPriceQuotationId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loPriceQuotation = new PriceQuotation();
            loPriceQuotationDetail = new PriceQuotationDetail();
            loCustomer = new Customer();
            loSalesPerson = new SalesPerson();
            loStock = new Stock();
            loCommon = new Common();
            //loStockReceivingDetailRpt = new StockReceivingDetailRpt();
            loReportViewer = new ReportViewerUI();
            lPriceQuotationId = pPriceQuotationId;
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

        private void PriceQuotationDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboCustomer.DataSource = loCustomer.getAllData("ViewAll", "", "");
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "Id";
                cboCustomer.SelectedIndex = -1;

                try
                {
                    cboSalesPerson.DataSource = loSalesPerson.getAllData("ViewAll","","");
                    cboSalesPerson.DisplayMember = "Name";
                    cboSalesPerson.ValueMember = "Id";
                    cboSalesPerson.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loPriceQuotation.getAllData("", lPriceQuotationId, "").Rows)
                    {
                        txtId.Text = _dr["Id"].ToString();
                        dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                        txtReference.Text = _dr["Reference"].ToString();
                        cboCustomer.Text = _dr["Customer"].ToString();
                        txtTerms.Text = _dr["Terms"].ToString();
                        txtInstructions.Text = _dr["Instructions"].ToString();
                        dtpValidUntil.Value = GlobalFunctions.ConvertToDate(_dr["Valid Until"].ToString());
                        cboSalesPerson.Text = _dr["Sales Person"].ToString();
                        dtpShipDate.Value = GlobalFunctions.ConvertToDate(_dr["Ship Date"].ToString());
                        txtShipVia.Text = _dr["Ship Via"].ToString();
                        txtTotalQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty"].ToString()));
                        txtTotalAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString()));
                        txtRemarks.Text = _dr["Remarks"].ToString();
                        foreach (DataRow _drDetails in loPriceQuotationDetail.getPriceQuotationDetails("",lPriceQuotationId).Rows)
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

                            dgvDetail.Rows[i].Cells["DiscountId"].Value = _drDetails["DiscountId"].ToString();
                            dgvDetail.Rows[i].Cells["Discount"].Value = _drDetails["Discount"].ToString();
                            dgvDetail.Rows[i].Cells["DiscountAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Discount Amount"].ToString()));

                            dgvDetail.Rows[i].Cells["TotalPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Total Price"].ToString()));
                            dgvDetail.Rows[i].Cells["Remarks"].Value = _drDetails["Remarks"].ToString();
                            dgvDetail.Rows[i].Cells["Status"].Value = "Saved";
                        }
                        computeTotalAmount();
                    }
                }
                else
                {
                    foreach (DataRow _dr in loCommon.getNextTabelSequenceId("PriceQuotation").Rows)
                    {
                        txtId.Text = _dr[0].ToString();
                    }
                    cboCustomer.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "PriceQuotationDetailUI_Load");
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
                PQStockTransactionDetailUI loPQStockTransactionDetail = new PQStockTransactionDetailUI();
                loPQStockTransactionDetail.ParentList = this;
                loPQStockTransactionDetail.ShowDialog();
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
                PQStockTransactionDetailUI loPQStockTransactionDetail = new PQStockTransactionDetailUI(dgvDetail.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["StockId"].Value.ToString(),
                    dgvDetail.CurrentRow.Cells["LocationId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["Qty"].Value.ToString()),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["UnitPrice"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["DiscountId"].Value.ToString(),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["DiscountAmount"].Value.ToString()),
                    decimal.Parse(dgvDetail.CurrentRow.Cells["TotalPrice"].Value.ToString()),
                    dgvDetail.CurrentRow.Cells["Remarks"].Value.ToString());
                loPQStockTransactionDetail.ParentList = this;
                loPQStockTransactionDetail.ShowDialog();
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
                string _CustomerId = "";
                string _SalesPerson = "";
                try
                {
                    _CustomerId = cboCustomer.SelectedValue.ToString();
                }
                catch
                {
                    _CustomerId = "";
                }
                try
                {
                    _SalesPerson = cboSalesPerson.SelectedValue.ToString();
                }
                catch
                {
                    _SalesPerson = "";
                }
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Price Quotation?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loPriceQuotation.Id = lPriceQuotationId;
                    loPriceQuotation.Date = dtpDate.Value;
                    loPriceQuotation.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loPriceQuotation.CustomerId = _CustomerId;
                    try
                    {
                        loPriceQuotation.Terms = int.Parse(txtTerms.Text);
                    }
                    catch
                    {
                        loPriceQuotation.Terms = 0;
                    }
                    loPriceQuotation.Instructions = GlobalFunctions.replaceChar(txtInstructions.Text);
                    loPriceQuotation.ValidUntil = dtpValidUntil.Value;
                    loPriceQuotation.SalesPerson = _SalesPerson;
                    loPriceQuotation.ShipDate = dtpShipDate.Value;
                    loPriceQuotation.ShipVia = GlobalFunctions.replaceChar(txtShipVia.Text);
                    loPriceQuotation.TotalQty = decimal.Parse(txtTotalQty.Text);
                    loPriceQuotation.TotalAmount = decimal.Parse(txtTotalAmount.Text);
                    loPriceQuotation.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loPriceQuotation.UserId = GlobalVariables.UserId;

                    string _PriceQuotationId = loPriceQuotation.save(lOperation);
                    if (_PriceQuotationId != "")
                    {
                        for (int i = 0; i < dgvDetail.Rows.Count; i++)
                        {
                            if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Add")
                            {
                                try
                                {
                                    loPriceQuotationDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    loPriceQuotationDetail.DetailId = "";
                                }
                                try
                                {
                                    loPriceQuotationDetail.PriceQuotationId = _PriceQuotationId;
                                    loPriceQuotationDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                    loPriceQuotationDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                    loPriceQuotationDetail.Qty = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                    loPriceQuotationDetail.UnitPrice = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                    loPriceQuotationDetail.DiscountId = dgvDetail.Rows[i].Cells[9].Value.ToString();
                                    loPriceQuotationDetail.DiscountAmount = decimal.Parse(dgvDetail.Rows[i].Cells[11].Value.ToString());
                                    loPriceQuotationDetail.TotalPrice = decimal.Parse(dgvDetail.Rows[i].Cells[12].Value.ToString());
                                    loPriceQuotationDetail.Remarks = dgvDetail.Rows[i].Cells[13].Value.ToString();
                                    loPriceQuotationDetail.UserId = GlobalVariables.UserId;
                                    loPriceQuotationDetail.save(GlobalVariables.Operation.Add);
                                }
                                catch { }
                            }
                            else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                            {
                                try
                                {
                                    loPriceQuotationDetail.DetailId = dgvDetail.Rows[i].Cells[0].Value.ToString();
                                }
                                catch
                                {
                                    loPriceQuotationDetail.DetailId = "";
                                }
                                loPriceQuotationDetail.PriceQuotationId = _PriceQuotationId;
                                loPriceQuotationDetail.StockId = dgvDetail.Rows[i].Cells[1].Value.ToString();
                                loPriceQuotationDetail.LocationId = dgvDetail.Rows[i].Cells[5].Value.ToString();
                                loPriceQuotationDetail.Qty = decimal.Parse(dgvDetail.Rows[i].Cells[7].Value.ToString());
                                loPriceQuotationDetail.UnitPrice = decimal.Parse(dgvDetail.Rows[i].Cells[8].Value.ToString());
                                loPriceQuotationDetail.DiscountId = dgvDetail.Rows[i].Cells[9].Value.ToString();
                                loPriceQuotationDetail.DiscountAmount = decimal.Parse(dgvDetail.Rows[i].Cells[11].Value.ToString());
                                loPriceQuotationDetail.TotalPrice = decimal.Parse(dgvDetail.Rows[i].Cells[12].Value.ToString());
                                loPriceQuotationDetail.Remarks = dgvDetail.Rows[i].Cells[13].Value.ToString();
                                loPriceQuotationDetail.UserId = GlobalVariables.UserId;
                                loPriceQuotationDetail.save(GlobalVariables.Operation.Edit);

                            }
                            else if (dgvDetail.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                            {
                                loPriceQuotationDetail.remove(dgvDetail.Rows[i].Cells[0].Value.ToString());
                            }
                        }
                        if (txtId.Text == _PriceQuotationId)
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Price Quotation has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }
                        else
                        {
                            MessageBoxUI _mb2 = new MessageBoxUI("Price Quotation has been saved successfully! New Price Quotation ID. : " + _PriceQuotationId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                            _mb2.showDialog();
                        }

                        //previewDetail(_PriceQuotationId);

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

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataRow _dr in loCustomer.getAllData("",cboCustomer.SelectedValue.ToString(),"").Rows)
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
