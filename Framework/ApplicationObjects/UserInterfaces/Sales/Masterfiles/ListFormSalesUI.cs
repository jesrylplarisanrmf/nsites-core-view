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
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports.MasterfileRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Masterfiles
{
    public partial class ListFormSalesUI : Form
    {
        #region "VARIABLES"
        public object lObject;
        Type lType;
        string[] lRecord;
        string[] lColumnName;
        int lCountCol;
        SearchesUI loSearches;
        Common loCommon;
        System.Data.DataTable ldtShow;
        //System.Data.DataTable ldtReport;
        //System.Data.DataTable ldtReportSum;
        bool lFromRefresh;
        ReportViewerUI loReportViewer;
        #endregion "END OF VARIABLES"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "CONSTRUCTORS"
        public ListFormSalesUI(object pObject, Type pType)
        {
            InitializeComponent();
            lObject = pObject;
            lType = pType;
            this.Text = pObject.GetType().Name + " List";
            loCommon = new Common();
            ldtShow = new System.Data.DataTable();
            //ldtReport = new System.Data.DataTable();
            //ldtReportSum = new System.Data.DataTable();
            lFromRefresh = false;
            loReportViewer = new ReportViewerUI();
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

        private void ListFormSalesUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                switch (lType.Name)
                {
                    case "SalesDiscount":
                        _Type = typeof(SalesDiscount);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "SalesPerson":
                        _Type = typeof(SalesPerson);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormSalesUI_Load");
                em.ShowDialog();
                Application.Exit();
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
                    //Sales
                    case "SalesDiscount":
                        SalesDiscountDetailUI loSalesDiscountDetail = new SalesDiscountDetailUI();
                        loSalesDiscountDetail.ParentList = this;
                        loSalesDiscountDetail.ShowDialog();
                        break;
                    case "SalesPerson":
                        SalesPersonDetailUI loSalesPersonDetail = new SalesPersonDetailUI();
                        loSalesPersonDetail.ParentList = this;
                        loSalesPersonDetail.ShowDialog();
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
                            //Sales
                            case "SalesDiscount":
                                SalesDiscountDetailUI loSalesDiscountDetail = new SalesDiscountDetailUI(lRecord);
                                loSalesDiscountDetail.ParentList = this;
                                loSalesDiscountDetail.ShowDialog();
                                break;
                            case "SalesPerson":
                                SalesPersonDetailUI loSalesPersonDetail = new SalesPersonDetailUI(lRecord);
                                loSalesPersonDetail.ParentList = this;
                                loSalesPersonDetail.ShowDialog();
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
                    case "SalesDiscount":
                        _DisplayFields = "SELECT Id,`Code`,Description,`Type`,`Value`,Remarks "+
		                    "FROM salesdiscount ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "ModeOfPayment":
                        _DisplayFields = "SELECT Id,`Code`,Description,`Default`,CashPayment AS ` Cash Payment`,Remarks "+
		                    "FROM modeofpayment ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "SalesPerson":
                        _DisplayFields = "SELECT Id,`Name`,Address,ContactNo,EmailAddress AS `Email Address`,Remarks "+
		                    "FROM salesperson ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY `Name` ASC;";
                        _Alias = "";
                        break;
                    case "Cashier":
                        _DisplayFields = "SELECT c.Id,c.`Code`,c.`Name`,u.Username,c.Remarks "+
		                    "FROM cashier c "+
                            "LEFT JOIN `user` u " +
		                    "ON c.UserId = u.Id ";
                        _WhereFields = " AND c.`Status` = 'Active' ORDER BY c.`Name` ASC;";
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
                        case "SalesDiscount":
                            SalesDiscountRpt loSalesDiscountRpt = new SalesDiscountRpt();
                            loSalesDiscountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loSalesDiscountRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loSalesDiscountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loSalesDiscountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loSalesDiscountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loSalesDiscountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loSalesDiscountRpt.SetParameterValue("Title", "Sales Discount List");
                            loSalesDiscountRpt.SetParameterValue("SubTitle", "Sales Discount List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loSalesDiscountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loSalesDiscountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loSalesDiscountRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loSalesDiscountRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "SalesPerson":
                            SalesPersonRpt loSalesPersonRpt = new SalesPersonRpt();
                            loSalesPersonRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loSalesPersonRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loSalesPersonRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loSalesPersonRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loSalesPersonRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loSalesPersonRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loSalesPersonRpt.SetParameterValue("Title", "Sales Person List");
                            loSalesPersonRpt.SetParameterValue("SubTitle", "Sales Person List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loSalesPersonRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loSalesPersonRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loSalesPersonRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loSalesPersonRpt;
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
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Code" || this.dgvLists.Columns[e.ColumnIndex].Name == "Default" ||
                    this.dgvLists.Columns[e.ColumnIndex].Name == "Cash Payment" || this.dgvLists.Columns[e.ColumnIndex].Name == "Username")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Value")
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

        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            btnRemove_Click(null, new EventArgs());
        }

        private void tsmiSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tsmiPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
        }
    }
}
