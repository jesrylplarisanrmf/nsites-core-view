using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using NSites_V.Global;

using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports.MasterfilesRpt;
//using NSites_V.ApplicationObjects.UserInterfaces.Systems;

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles
{
    public partial class ListFormInventoryUI : Form
    {
        #region "VARIABLES"
        public object lObject;
        Type lType;
        string[] lRecord;
        string[] lColumnName;
        int lCountCol;
        //SearchUI loSearch;
        SearchesUI loSearches;
        Common loCommon;
        System.Data.DataTable ldtShow;
        //System.Data.DataTable ldtReport;
        //System.Data.DataTable ldtReportSum;
        ReportViewerUI loReportViewer;
        bool lFromRefresh;
        #endregion "END OF VARIABLES"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "CONSTRUCTORS"
        public ListFormInventoryUI(object pObject, Type pType)
        {
            InitializeComponent();
            lObject = pObject;
            lType = pType;
            this.Text = pObject.GetType().Name + " List";
            loCommon = new Common();
            ldtShow = new System.Data.DataTable();
            //ldtReport = new System.Data.DataTable();
            //ldtReportSum = new System.Data.DataTable();
            loReportViewer = new ReportViewerUI();
            lFromRefresh = false;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        public void refresh(string pDisplayType,string pPrimaryKey, string pSearchString, bool pShowRecord)
        {
            lFromRefresh = true;
            loSearches.lQuery = "";
            try
            {
                dgvLists.Rows.Clear();
                dgvLists.Columns.Clear();
            }
            catch 
            {
                dgvLists.DataSource = null;
            }
            tsmiViewAllRecords.Visible = false;
            object[] _params = { pDisplayType, pPrimaryKey, pSearchString };

            ldtShow = (System.Data.DataTable)lObject.GetType().GetMethod("getAllData").Invoke(lObject, _params);
            if(ldtShow == null)
            {
                return;
            }
            lCountCol = ldtShow.Columns.Count;
            lColumnName = new string[lCountCol];
            lRecord = new string[lCountCol];
            for (int i = 0; i < lCountCol; i++)
            {
                dgvLists.Columns.Add(ldtShow.Columns[i].ColumnName, ldtShow.Columns[i].ColumnName);
            }
            if (pShowRecord)
            {
                foreach (DataRow _dr in ldtShow.Rows)
                {
                    int n = dgvLists.Rows.Add();
                    if (n < GlobalVariables.DisplayRecordLimit)
                    {
                        for (int i = 0; i < lCountCol; i++)
                        {
                            dgvLists.Rows[n].Cells[i].Value = _dr[i].ToString();
                        }
                    }
                    else
                    {
                        dgvLists.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                        dgvLists.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                        dgvLists.Rows[n].Height = 5;
                        dgvLists.Rows[n].ReadOnly = true;
                        tsmiViewAllRecords.Visible = true;
                        break;
                    }
                }
            }
            try
            {
                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }
            }
            catch { }
        }
        
        public void refreshAll()
        {
            tsmiViewAllRecords.Visible = false;
            dgvLists.Rows.Clear();
            foreach (DataRow _dr in ldtShow.Rows)
            {
                int n = dgvLists.Rows.Add();
                for (int i = 0; i < lCountCol; i++)
                {
                    dgvLists.Rows[n].Cells[i].Value = _dr[i].ToString();
                }
            }

            for (int i = 0; i < lCountCol; i++)
            {
                lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
            }
        }

        public void addData(string[] pRecordData)
        {
            try
            {
                int n = dgvLists.Rows.Add();
                for (int i = 0; i < pRecordData.Length; i++)
                {
                    dgvLists.Rows[n].Cells[i].Value = pRecordData[i];
                }
                dgvLists.CurrentRow.Selected = false;
                dgvLists.FirstDisplayedScrollingRowIndex = dgvLists.Rows[n].Index;
                dgvLists.Rows[n].Selected = true;
            }
            catch
            {
                refresh("ViewAll","", "", true);
            }
        }

        public void updateData(string[] pRecordData)
        {
            for (int i = 0; i < pRecordData.Length; i++)
            {
                dgvLists.CurrentRow.Cells[i].Value = pRecordData[i];
            }
        }
        #endregion "END OF METHODS"

        private void ListFormInventoryUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                switch (lType.Name)
                {
                    case "Customer":
                        _Type = typeof(Customer);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Supplier":
                        _Type = typeof(Supplier);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "InventoryGroup":
                        _Type = typeof(InventoryGroup);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Category":
                        _Type = typeof(Category);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Stock":
                        _Type = typeof(Stock);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Unit":
                        _Type = typeof(Unit);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "InventoryType":
                        _Type = typeof(InventoryType);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Location":
                        _Type = typeof(Location);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormInventoryUI_Load");
                em.ShowDialog();
                Application.Exit();
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
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Refresh"))
                {
                    return;
                }
                refresh("ViewAll", "", "", true);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Create"))
                {
                    return;
                }
                if (dgvLists.Rows.Count == 0)
                {
                    refresh("ViewAll", "", "", false);
                }
                switch (lType.Name)
                {
                    case "Customer":
                        CustomerDetailUI loCustomerDetail = new CustomerDetailUI();
                        loCustomerDetail.ParentList = this;
                        loCustomerDetail.ShowDialog();
                        break;
                    case "Supplier":
                        SupplierDetailUI loSupplierDetail = new SupplierDetailUI();
                        loSupplierDetail.ParentList = this;
                        loSupplierDetail.ShowDialog();
                        break;
                    case "InventoryGroup":
                        InventoryGroupDetailUI loInventoryGroupDetailUI = new InventoryGroupDetailUI();
                        loInventoryGroupDetailUI.ParentList = this;
                        loInventoryGroupDetailUI.ShowDialog();
                        break;
                    case "Category":
                        CategoryDetailUI loCategoryDetailUI = new CategoryDetailUI();
                        loCategoryDetailUI.ParentList = this;
                        loCategoryDetailUI.ShowDialog();
                        break;
                    case "Stock":
                        StockDetailUI loStockDetailUI = new StockDetailUI();
                        loStockDetailUI.ParentList = this;
                        loStockDetailUI.ShowDialog();
                        break;
                    case "Unit":
                        UnitDetailUI loUnitDetailUI = new UnitDetailUI();
                        loUnitDetailUI.ParentList = this;
                        loUnitDetailUI.ShowDialog();
                        break;
                    case "InventoryType":
                        InventoryTypeDetailUI loInventoryTypeDetailUI = new InventoryTypeDetailUI();
                        loInventoryTypeDetailUI.ParentList = this;
                        loInventoryTypeDetailUI.ShowDialog();
                        break;
                    case "Location":
                        LocationDetailUI loLocationDetailUI = new LocationDetailUI();
                        loLocationDetailUI.ParentList = this;
                        loLocationDetailUI.ShowDialog();
                        break;
                }
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
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Update"))
                {
                    return;
                }

                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }

                if (lRecord.Length > 0)
                {
                    if (lRecord[0].ToString() != "")
                    {
                        switch (lType.Name)
                        {
                            case "Customer":
                                CustomerDetailUI loCustomerDetail = new CustomerDetailUI(lRecord);
                                loCustomerDetail.ParentList = this;
                                loCustomerDetail.ShowDialog();
                                break;
                            case "Supplier":
                                SupplierDetailUI loSupplierDetail = new SupplierDetailUI(lRecord);
                                loSupplierDetail.ParentList = this;
                                loSupplierDetail.ShowDialog();
                                break;
                            case "InventoryGroup":
                                InventoryGroupDetailUI loInventoryGroupDetailUI = new InventoryGroupDetailUI(lRecord);
                                loInventoryGroupDetailUI.ParentList = this;
                                loInventoryGroupDetailUI.ShowDialog();
                                break;
                            case "Category":
                                CategoryDetailUI loCategoryDetailUI = new CategoryDetailUI(lRecord);
                                loCategoryDetailUI.ParentList = this;
                                loCategoryDetailUI.ShowDialog();
                                break;
                            case "Stock":
                                StockDetailUI loStockDetailUI = new StockDetailUI(lRecord);
                                loStockDetailUI.ParentList = this;
                                loStockDetailUI.ShowDialog();
                                break;
                            case "Unit":
                                UnitDetailUI loUnitDetailUI = new UnitDetailUI(lRecord);
                                loUnitDetailUI.ParentList = this;
                                loUnitDetailUI.ShowDialog();
                                break;
                            case "InventoryType":
                                InventoryTypeDetailUI loInventoryTypeDetailUI = new InventoryTypeDetailUI(lRecord);
                                loInventoryTypeDetailUI.ParentList = this;
                                loInventoryTypeDetailUI.ShowDialog();
                                break;
                            case "Location":
                                LocationDetailUI loLocationDetailUI = new LocationDetailUI(lRecord);
                                loLocationDetailUI.ParentList = this;
                                loLocationDetailUI.ShowDialog();
                                break;
                        }
                    }
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
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Remove"))
                {
                    return;
                }
                if (lRecord.Length > 0)
                {
                    if (lRecord[0].ToString() != null)
                    {
                        DialogResult _dr = new DialogResult();
                        MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                        _mb.ShowDialog();
                        _dr = _mb.Operation;
                        if (_dr == DialogResult.Yes)
                        {
                            object[] param = { lRecord[0].ToString() };
                            if ((bool)lObject.GetType().GetMethod("remove").Invoke(lObject, param))
                            {
                                MessageBoxUI _mb1 = new MessageBoxUI(lType.Name + " has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();
                                refresh("ViewAll", "", "", true);
                            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Search"))
                {
                    return;
                }

                string _DisplayFields = "";
                string _WhereFields = "";
                string _Alias = "";

                switch (lType.Name)
                {
                    case "Customer":
                        _DisplayFields = "SELECT Id,`Name`,`Default`,Address,DATE_FORMAT(Birthdate,'%m-%d-%Y') AS Birthdate,TIN, "+
		                    "ContactPerson AS `Contact Person`,ContactNo AS `Contact No.`, "+
                            "EmailAddress AS `Email Address`,Remarks " +
		                    "FROM customer ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY `Name` ASC;";
                        _Alias = "";
                        break;
                    case "Supplier":
                        _DisplayFields = "SELECT Id,`Name`,Address,TIN, "+
		                    "ContactPerson AS `Contact Person`,ContactNo AS `Contact No.`, "+
		                    "EmailAddress AS `Email Address`,BankName AS `Bank Name`, "+
                            "BankAccountNo AS `Bank Account No.`,Remarks " +
		                    "FROM supplier ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY `Name` ASC;";
                        _Alias = "";
                        break;
                    case "InventoryGroup":
                        _DisplayFields = "SELECT Id,Description,Remarks "+
		                    "FROM inventorygroup ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Category":
                        _DisplayFields = "SELECT c.Id,c.Description,ig.Description AS `Inventory Group`,c.Remarks "+
		                    "FROM category c "+
                            "LEFT JOIN inventorygroup ig " +
		                    "ON c.InventoryGroupId = ig.Id ";
                        _WhereFields = " AND c.`Status` = 'Active' ORDER BY c.Description ASC;";
                        _Alias = "c.";
                        break;
                    case "Stock":
                        _DisplayFields = "SELECT s.Id,s.Code,s.Description,c.Description AS `Category`, "+
		                    "u.Description AS Unit,s.UnitCost AS `Unit Cost`, "+
		                    "s.BasePrice AS `Base Price`,s.UnitPrice AS `Unit Price`, "+
		                    "s.ReorderLevel AS `Reorder Level`,s.Active,s.Saleable, "+
		                    "s.NonInventory AS `Non-Inventory`,s.Remarks "+
		                    "FROM stock s "+
		                    "LEFT JOIN category c "+
		                    "ON s.CategoryId = c.Id "+
                            "LEFT JOIN unit u " +
		                    "ON s.UnitId = u.Id ";
                        _WhereFields = " AND s.`Status` = 'Active' ORDER BY s.Description ASC;";
                        _Alias = "s.";
                        break;
                    case "Unit":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM unit ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "InventoryType":
                        _DisplayFields = "SELECT Id,Description,Qty,Source,Remarks "+
		                    "FROM inventorytype ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Location":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM location ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                }
                loSearches.lAlias = _Alias;
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtShow = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvLists, ldtShow);
                    lFromRefresh = false;
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
                if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Preview"))
                {
                    return;
                }
                if (dgvLists.Rows.Count != 0)
                {
                    switch (lType.Name)
                    {
                        case "Supplier":
                            SupplierRpt loSupplierRpt = new SupplierRpt();
                            loSupplierRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loSupplierRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loSupplierRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loSupplierRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loSupplierRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loSupplierRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loSupplierRpt.SetParameterValue("Title", "Supplier List");
                            loSupplierRpt.SetParameterValue("SubTitle", "Supplier List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loSupplierRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loSupplierRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loSupplierRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loSupplierRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Customer":
                            CustomerRpt loCustomerRpt = new CustomerRpt();
                            loCustomerRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loCustomerRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loCustomerRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loCustomerRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loCustomerRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loCustomerRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loCustomerRpt.SetParameterValue("Title", "Customer List");
                            loCustomerRpt.SetParameterValue("SubTitle", "Customer List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loCustomerRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loCustomerRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loCustomerRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loCustomerRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "InventoryGroup":
                            InventoryGroupRpt loInventoryGroupRpt = new InventoryGroupRpt();
                            loInventoryGroupRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loInventoryGroupRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loInventoryGroupRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loInventoryGroupRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loInventoryGroupRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loInventoryGroupRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loInventoryGroupRpt.SetParameterValue("Title", "Inventory Group List");
                            loInventoryGroupRpt.SetParameterValue("SubTitle", "Inventory Group List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loInventoryGroupRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loInventoryGroupRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loInventoryGroupRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loInventoryGroupRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Category":
                            CategoryRpt loCategoryRpt = new CategoryRpt();
                            loCategoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loCategoryRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loCategoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loCategoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loCategoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loCategoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loCategoryRpt.SetParameterValue("Title", "Category List");
                            loCategoryRpt.SetParameterValue("SubTitle", "Category List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loCategoryRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loCategoryRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loCategoryRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loCategoryRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Stock":
                            StockRpt loStockRpt = new StockRpt();
                            loStockRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loStockRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loStockRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loStockRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loStockRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loStockRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loStockRpt.SetParameterValue("Title", "Stock List");
                            loStockRpt.SetParameterValue("SubTitle", "Stock List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loStockRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loStockRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loStockRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loStockRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Unit":
                            UnitRpt loUnitRpt = new UnitRpt();
                            loUnitRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loUnitRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loUnitRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loUnitRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loUnitRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loUnitRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loUnitRpt.SetParameterValue("Title", "Unit List");
                            loUnitRpt.SetParameterValue("SubTitle", "Unit List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loUnitRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loUnitRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loUnitRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loUnitRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "InventoryType":
                            InventoryTypeRpt loInventoryTypeRpt = new InventoryTypeRpt();
                            loInventoryTypeRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loInventoryTypeRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loInventoryTypeRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loInventoryTypeRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loInventoryTypeRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loInventoryTypeRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loInventoryTypeRpt.SetParameterValue("Title", "Inventory Type List");
                            loInventoryTypeRpt.SetParameterValue("SubTitle", "Inventory Type List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loInventoryTypeRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loInventoryTypeRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loInventoryTypeRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loInventoryTypeRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Location":
                            LocationRpt loLocationRpt = new LocationRpt();
                            loLocationRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loLocationRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loLocationRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loLocationRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loLocationRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loLocationRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loLocationRpt.SetParameterValue("Title", "Location List");
                            loLocationRpt.SetParameterValue("SubTitle", "Location List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loLocationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loLocationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loLocationRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loLocationRpt;
                            loReportViewer.ShowDialog();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void dgvLists_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < lCountCol; i++)
                {
                    lRecord[i] = dgvLists.CurrentRow.Cells[i].Value.ToString();
                }
            }
            catch { }
        }

        private void dgvLists_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Drawing.Point pt = dgvLists.PointToScreen(e.Location);
                cmsFunction.Show(pt);
            }
        }

        private void tsmiViewAllRecords_Click(object sender, EventArgs e)
        {
            //GlobalFunctions.refreshAll(ref dgvLists, ldtShow);
            try
            {
                dgvLists.Rows.Clear();
                dgvLists.Columns.Clear();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
            try
            {
                dgvLists.DataSource = ldtShow;
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewAllRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsm" + lType.Name, "Search"))
            {
                return;
            }
            refresh("", "", txtSearch.Text, true);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnUpdate_Click(null, new EventArgs());
            }
        }

        private void dgvLists_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(null, new EventArgs());
        }

        private void dgvLists_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvLists.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvLists.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Code" || this.dgvLists.Columns[e.ColumnIndex].Name == "Username" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "TIN" || this.dgvLists.Columns[e.ColumnIndex].Name == "Default" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Cash Payment" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Active" || this.dgvLists.Columns[e.ColumnIndex].Name == "Saleable" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Non-Inventory")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Unit Cost" || this.dgvLists.Columns[e.ColumnIndex].Name == "Unit Price" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Reorder Level" || this.dgvLists.Columns[e.ColumnIndex].Name == "Base Price" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Credit Limit")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch { }
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

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tmsiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tmsiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }
    }
}
