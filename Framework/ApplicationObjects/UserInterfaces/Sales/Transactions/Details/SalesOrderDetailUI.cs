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

using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions.Details;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions.Details
{
    public partial class SalesOrderDetailUI : Form
    {
        #region "VARIABLES"
        SalesOrder loSalesOrder;
        SalesOrderDetail loSalesOrderDetail;
        PriceQuotation loPriceQuotation;
        PriceQuotationDetail loPriceQuotationDetail;
        Customer loCustomer;
        SalesPerson loSalesPerson;
        Stock loStock;
        Common loCommon;
        GlobalVariables.Operation lOperation;
        LookUpPriceQuotationUI loLookUpPriceQuotation;
        ReportViewerUI loReportViewer;
        string lSalesOrderId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SalesOrderDetailUI()
        {
            InitializeComponent();
            loSalesOrder = new SalesOrder();
            loSalesOrderDetail = new SalesOrderDetail();
            loPriceQuotation = new PriceQuotation();
            loPriceQuotationDetail = new PriceQuotationDetail();
            loCustomer = new Customer();
            loSalesPerson = new SalesPerson();
            loStock = new Stock();
            loCommon = new Common();
            lOperation = GlobalVariables.Operation.Add;
            loLookUpPriceQuotation = new LookUpPriceQuotationUI();
            loReportViewer = new ReportViewerUI();
            lSalesOrderId = "";
        }
        public SalesOrderDetailUI(string pSalesOrderId)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loSalesOrder = new SalesOrder();
            loSalesOrderDetail = new SalesOrderDetail();
            loPriceQuotation = new PriceQuotation();
            loPriceQuotationDetail = new PriceQuotationDetail();
            loCustomer = new Customer();
            loSalesPerson = new SalesPerson();
            loStock = new Stock();
            loCommon = new Common();
            loLookUpPriceQuotation = new LookUpPriceQuotationUI();
            loReportViewer = new ReportViewerUI();
            lSalesOrderId = pSalesOrderId;
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
                decimal _TotalSOQty = 0;
                decimal _TotalQtyOut = 0;
                decimal _TotalQtyVariance = 0;
                decimal _TotalAmount = 0;

                for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                {
                    if (dgvDetailStockInventory.Rows[i].Visible == true)
                    {
                        try
                        {
                            _TotalSOQty += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["SOQty"].Value.ToString());
                            _TotalQtyOut += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyOut"].Value.ToString());
                            _TotalQtyVariance += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                            _TotalAmount += decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                        }
                        catch { }
                    }
                }
                lblTotalSOQty.Text = string.Format("{0:n}", _TotalSOQty);
                lblTotalQtyOut.Text = string.Format("{0:n}", _TotalQtyOut);
                lblTotalQtyVariance.Text = string.Format("{0:n}", _TotalQtyVariance);
                txtTotalAmount.Text = string.Format("{0:n}", _TotalAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SalesOrderDetailUI_Load(object sender, EventArgs e)
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

            try
            {
                cboSalesPerson.DataSource = loSalesPerson.getSalesPersonNames();
                cboSalesPerson.DisplayMember = "Name";
                cboSalesPerson.ValueMember = "Id";
                cboSalesPerson.SelectedIndex = -1;
            }
            catch { }

            if (lOperation == GlobalVariables.Operation.Edit)
            {
                foreach (DataRow _dr in loSalesOrder.getAllData("", lSalesOrderId, "").Rows)
                {
                    txtId.Text = _dr["Id"].ToString();
                    dtpDate.Value = GlobalFunctions.ConvertToDate(_dr["Date"].ToString());
                    txtReference.Text = _dr["Reference"].ToString();
                    cboCustomer.Text = _dr["Customer"].ToString();
                    
                    cboPriceQuotation.Text = _dr["P.Q. Id"].ToString();
                    txtTerms.Text = _dr["Terms"].ToString();
                    dtpDueDate.Value = GlobalFunctions.ConvertToDate(_dr["Due Date"].ToString());
                    txtInstructions.Text = _dr["Instructions"].ToString();
                    try
                    {
                        cboSalesPerson.SelectedValue = _dr["SalesPerson"].ToString();
                    }
                    catch
                    {
                        cboSalesPerson.Text = "";
                    }
                    lblTotalSOQty.Text = string.Format("{0:n}", decimal.Parse(_dr["Total S.O. Qty"].ToString()));
                    lblTotalQtyOut.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty Out"].ToString()));
                    lblTotalQtyVariance.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Qty Variance"].ToString()));
                    txtTotalAmount.Text = string.Format("{0:n}", decimal.Parse(_dr["Total Amount"].ToString()));
                    txtRemarks.Text = _dr["Remarks"].ToString();

                    foreach (DataRow _drDetails in loSalesOrderDetail.getSalesOrderDetails("",lSalesOrderId).Rows)
                    {
                        int i = dgvDetailStockInventory.Rows.Add();
                        dgvDetailStockInventory.Rows[i].Cells["Id"].Value = _drDetails["Id"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["SOQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["S.O. Qty"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["QtyOut"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty Out"].ToString()));
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
                    cboCustomer.Enabled = false;
                    cboPriceQuotation.Enabled = false;
                    btnLookUpPriceQuotation.Enabled = false;
                }
            }
            else
            {
                foreach (DataRow _dr in loCommon.getNextTabelSequenceId("SalesOrder").Rows)
                {
                    txtId.Text = _dr[0].ToString();
                }
                
                cboCustomer.Focus();
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
                dgvDetailStockInventory.Rows.Clear();
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
                SalesStockTransactionDetailUI loSalesStockTransactionDetail = new SalesStockTransactionDetailUI();
                loSalesStockTransactionDetail.ParentList = this;
                loSalesStockTransactionDetail.ShowDialog();
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
                SalesStockTransactionDetailUI loSalesStockTransactionDetail = new SalesStockTransactionDetailUI(dgvDetailStockInventory.CurrentRow.Cells["Id"].Value.ToString(),
                    dgvDetailStockInventory.CurrentRow.Cells["StockId"].Value.ToString(),
                    dgvDetailStockInventory.CurrentRow.Cells["LocationId"].Value.ToString(),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["SOQty"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["QtyOut"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["QtyVariance"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["UnitPrice"].Value.ToString()),
                    dgvDetailStockInventory.CurrentRow.Cells["DiscountId"].Value.ToString(),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["DiscountAmount"].Value.ToString()),
                    decimal.Parse(dgvDetailStockInventory.CurrentRow.Cells["TotalPrice"].Value.ToString()),
                    dgvDetailStockInventory.CurrentRow.Cells["Remarks"].Value.ToString());
                loSalesStockTransactionDetail.ParentList = this;
                loSalesStockTransactionDetail.ShowDialog();
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
                if (decimal.Parse(lblTotalSOQty.Text) == 0)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Totals of S.O. Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
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
                    MessageBoxUI _mb1 = new MessageBoxUI("You must select a Customer!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    cboCustomer.Focus();
                    return;
                }
                //check credit limit
                decimal _CreditLimit = 0;
                decimal _TotalRunningBalance = 0;
                foreach (DataRow _drCL in loCustomer.getCustomerCreditLimit(_CustomerId).Rows)
                {
                    _CreditLimit = decimal.Parse(_drCL["CreditLimit"].ToString());
                }
                foreach (DataRow _drRB in loSalesOrder.getTotalRunningBalance(_CustomerId).Rows)
                {
                    _TotalRunningBalance = decimal.Parse(_drRB["RunningBalance"].ToString());
                }
                if ((decimal.Parse(txtTotalAmount.Text) + _TotalRunningBalance) > _CreditLimit)
                {
                    MessageBoxUI _mb1 = new MessageBoxUI("Total Amount + Running Balance exceeds Credit Limit of "+string.Format("{0:n}",_CreditLimit)+"!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb1.showDialog();
                    return;
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
                MessageBoxUI _mb = new MessageBoxUI("Continue saving this Sales Order?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loSalesOrder.Id = lSalesOrderId;
                    loSalesOrder.Date = dtpDate.Value;
                    try
                    {
                        loSalesOrder.PQId = cboPriceQuotation.SelectedValue.ToString();
                    }
                    catch
                    {
                        loSalesOrder.PQId = "";
                    }

                    loSalesOrder.Reference = GlobalFunctions.replaceChar(txtReference.Text);
                    loSalesOrder.CustomerId = _CustomerId;
                    try
                    {
                        loSalesOrder.Terms = int.Parse(txtTerms.Text);
                    }
                    catch
                    {
                        loSalesOrder.Terms = 0;
                    }
                    loSalesOrder.DueDate = dtpDueDate.Value;
                    loSalesOrder.Instructions = GlobalFunctions.replaceChar(txtInstructions.Text);
                    loSalesOrder.SalesPersonId = _SalesPerson;
                    loSalesOrder.TotalSOQty = decimal.Parse(lblTotalSOQty.Text);
                    loSalesOrder.TotalQtyOut = decimal.Parse(lblTotalQtyOut.Text);
                    loSalesOrder.TotalQtyVariance = decimal.Parse(lblTotalQtyVariance.Text);
                    loSalesOrder.TotalAmount = decimal.Parse(txtTotalAmount.Text);
                    loSalesOrder.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                    loSalesOrder.UserId = GlobalVariables.UserId;
                    try
                    {
                        string _SalesOrderId = loSalesOrder.save(lOperation);
                        if (_SalesOrderId != "")
                        {
                            for (int i = 0; i < dgvDetailStockInventory.Rows.Count; i++)
                            {
                                if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Add")
                                {
                                    try
                                    {
                                        loSalesOrderDetail.DetailId = dgvDetailStockInventory.Rows[i].Cells["Id"].Value.ToString();
                                    }
                                    catch
                                    {
                                        loSalesOrderDetail.DetailId = "";
                                    }
                                    try
                                    {
                                        loSalesOrderDetail.SalesOrderId = _SalesOrderId;
                                        loSalesOrderDetail.StockId = dgvDetailStockInventory.Rows[i].Cells["StockId"].Value.ToString();
                                        loSalesOrderDetail.LocationId = dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value.ToString();
                                        loSalesOrderDetail.SOQty = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["SOQty"].Value.ToString());
                                        loSalesOrderDetail.QtyOut = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyOut"].Value.ToString());
                                        loSalesOrderDetail.QtyVariance = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                                        loSalesOrderDetail.UnitPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value.ToString());
                                        loSalesOrderDetail.DiscountId = dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value.ToString();
                                        loSalesOrderDetail.DiscountAmount = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value.ToString());
                                        loSalesOrderDetail.TotalPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                                        loSalesOrderDetail.Remarks = dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value.ToString();
                                        loSalesOrderDetail.UserId = GlobalVariables.UserId;
                                        loSalesOrderDetail.save(GlobalVariables.Operation.Add);
                                    }
                                    catch { }
                                }
                                else if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Edit")
                                {
                                    try
                                    {
                                        loSalesOrderDetail.DetailId = dgvDetailStockInventory.Rows[i].Cells["Id"].Value.ToString();
                                    }
                                    catch
                                    {
                                        loSalesOrderDetail.DetailId = "";
                                    }
                                    try
                                    {
                                        loSalesOrderDetail.SalesOrderId = _SalesOrderId;
                                        loSalesOrderDetail.StockId = dgvDetailStockInventory.Rows[i].Cells["StockId"].Value.ToString();
                                        loSalesOrderDetail.LocationId = dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value.ToString();
                                        loSalesOrderDetail.SOQty = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["SOQty"].Value.ToString());
                                        loSalesOrderDetail.QtyOut = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyOut"].Value.ToString());
                                        loSalesOrderDetail.QtyVariance = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value.ToString());
                                        loSalesOrderDetail.UnitPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value.ToString());
                                        loSalesOrderDetail.DiscountId = dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value.ToString();
                                        loSalesOrderDetail.DiscountAmount = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value.ToString());
                                        loSalesOrderDetail.TotalPrice = decimal.Parse(dgvDetailStockInventory.Rows[i].Cells["TotalPrice"].Value.ToString());
                                        loSalesOrderDetail.Remarks = dgvDetailStockInventory.Rows[i].Cells["Remarks"].Value.ToString();
                                        loSalesOrderDetail.UserId = GlobalVariables.UserId;
                                        loSalesOrderDetail.save(GlobalVariables.Operation.Edit);
                                    }
                                    catch { }
                                }
                                else if (dgvDetailStockInventory.Rows[i].Cells["Status"].Value.ToString() == "Delete")
                                {
                                    loSalesOrderDetail.remove(dgvDetailStockInventory.Rows[i].Cells[0].Value.ToString());
                                }
                            }
                            if (txtId.Text == _SalesOrderId)
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Sales Order has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }
                            else
                            {
                                MessageBoxUI _mb2 = new MessageBoxUI("Sales Order has been saved successfully! New Sales Order ID. : " + _SalesOrderId, GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                                _mb2.showDialog();
                            }

                            //previewDetail(_SalesOrderId);

                            object[] _params = { };
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
                txtContactPerson.Clear();
                try
                {
                    cboPriceQuotation.DataSource = null;
                }
                catch
                {
                    cboPriceQuotation.Items.Clear();
                }
                

                foreach (DataRow _dr in loCustomer.getAllData("", cboCustomer.SelectedValue.ToString(), "").Rows)
                {
                    txtContactPerson.Text = _dr["Contact Person"].ToString();
                }
            }
            catch
            {
                txtContactPerson.Clear();
            }

            try
            {
                cboPriceQuotation.DataSource = loPriceQuotation.getPriceQuotationByCustomer(cboCustomer.SelectedValue.ToString(), "");
                cboPriceQuotation.DisplayMember = "Id";
                cboPriceQuotation.ValueMember = "Id";
                cboPriceQuotation.SelectedIndex = -1;
            }
            catch
            {
                cboPriceQuotation.DataSource = null;
            }
        }

        private void cboPriceQuotation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataRow _dr in loPriceQuotation.getAllData("",cboPriceQuotation.SelectedValue.ToString(),"").Rows)
                {
                    txtReference.Text = _dr["Reference"].ToString();
                    txtTerms.Text = _dr["Terms"].ToString();
                    dtpDueDate.Value = DateTime.Now.AddDays(double.Parse(txtTerms.Text));
                    cboSalesPerson.Text = _dr["Sales Person"].ToString();
                    txtInstructions.Text = _dr["Instructions"].ToString();
                }
                if (lOperation == GlobalVariables.Operation.Add)
                {
                    dgvDetailStockInventory.Rows.Clear();
                    foreach (DataRow _drDetails in loPriceQuotationDetail.getPriceQuotationDetails("",cboPriceQuotation.SelectedValue.ToString()).Rows)
                    {
                        int i = dgvDetailStockInventory.Rows.Add();
                        dgvDetailStockInventory.Rows[i].Cells["Id"].Value = "";
                        dgvDetailStockInventory.Rows[i].Cells["StockId"].Value = _drDetails["StockId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockCode"].Value = _drDetails["Stock Code"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["StockDescription"].Value = _drDetails["Stock Description"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Unit"].Value = _drDetails["Unit"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["LocationId"].Value = _drDetails["LocationId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Location"].Value = _drDetails["Location"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["SOQty"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["QtyOut"].Value = "0.00";
                        dgvDetailStockInventory.Rows[i].Cells["QtyVariance"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Qty"].ToString()));
                        dgvDetailStockInventory.Rows[i].Cells["UnitPrice"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Unit Price"].ToString()));

                        dgvDetailStockInventory.Rows[i].Cells["DiscountId"].Value = _drDetails["DiscountId"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["Discount"].Value = _drDetails["Discount"].ToString();
                        dgvDetailStockInventory.Rows[i].Cells["DiscountAmount"].Value = string.Format("{0:n}", decimal.Parse(_drDetails["Discount Amount"].ToString()));

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

        private void btnFindSalesQuotation_Click(object sender, EventArgs e)
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
            LookUpPriceQuotationUI loLookUpPriceQuotation = new LookUpPriceQuotationUI();
            loLookUpPriceQuotation.lCustomerId = _customerId;
            loLookUpPriceQuotation.ShowDialog();
            if (loLookUpPriceQuotation.lFromSelection)
            {
                cboPriceQuotation.Text = loLookUpPriceQuotation.lId;
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
