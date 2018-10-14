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

using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports.MasterfileRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Masterfiles
{
    public partial class ListFormAccountingUI : Form
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
        public ListFormAccountingUI(object pObject, Type pType)
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

        private void ListFormAccountingUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                switch (lType.Name)
                {
                    case "Bank":
                        _Type = typeof(Bank);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "ChartOfAccount":
                        _Type = typeof(ChartOfAccount);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "MainAccount":
                        _Type = typeof(MainAccount);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Classification":
                        _Type = typeof(Classification);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "SubClassification":
                        _Type = typeof(SubClassification);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Equipment":
                        _Type = typeof(Equipment);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                    case "Building":
                        _Type = typeof(Building);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormAccountingUI_Load");
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
                    case "ChartOfAccount":
                        ChartOfAccountDetailUI loChartOfAccountDetail = new ChartOfAccountDetailUI();
                        loChartOfAccountDetail.ParentList = this;
                        loChartOfAccountDetail.ShowDialog();
                        break;
                    case "MainAccount":
                        MainAccountDetailUI loMainAccountDetail = new MainAccountDetailUI();
                        loMainAccountDetail.ParentList = this;
                        loMainAccountDetail.ShowDialog();
                        break;
                    case "Classification":
                        ClassificationDetailUI loClassificationDetail = new ClassificationDetailUI();
                        loClassificationDetail.ParentList = this;
                        loClassificationDetail.ShowDialog();
                        break;
                    case "SubClassification":
                        SubClassificationDetailUI loSubClassificationDetail = new SubClassificationDetailUI();
                        loSubClassificationDetail.ParentList = this;
                        loSubClassificationDetail.ShowDialog();
                        break;
                    case "Bank":
                        BankDetailUI loBankDetail = new BankDetailUI();
                        loBankDetail.ParentList = this;
                        loBankDetail.ShowDialog();
                        break;
                    case "Equipment":
                        EquipmentDetailUI loEquipmentDetail = new EquipmentDetailUI();
                        loEquipmentDetail.ParentList = this;
                        loEquipmentDetail.ShowDialog();
                        break;
                    case "Building":
                        BuildingDetailUI loBuildingDetail = new BuildingDetailUI();
                        loBuildingDetail.ParentList = this;
                        loBuildingDetail.ShowDialog();
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
                            case "ChartOfAccount":
                                ChartOfAccountDetailUI loChartOfAccountDetail = new ChartOfAccountDetailUI(lRecord);
                                loChartOfAccountDetail.ParentList = this;
                                loChartOfAccountDetail.ShowDialog();
                                break;
                            case "MainAccount":
                                MainAccountDetailUI loMainAccountDetail = new MainAccountDetailUI(lRecord);
                                loMainAccountDetail.ParentList = this;
                                loMainAccountDetail.ShowDialog();
                                break;
                            case "Classification":
                                ClassificationDetailUI loClassificationDetail = new ClassificationDetailUI(lRecord);
                                loClassificationDetail.ParentList = this;
                                loClassificationDetail.ShowDialog();
                                break;
                            case "SubClassification":
                                SubClassificationDetailUI loSubClassificationDetail = new SubClassificationDetailUI(lRecord);
                                loSubClassificationDetail.ParentList = this;
                                loSubClassificationDetail.ShowDialog();
                                break;
                            case "Bank":
                                BankDetailUI loBankDetail = new BankDetailUI(lRecord);
                                loBankDetail.ParentList = this;
                                loBankDetail.ShowDialog();
                                break;
                            case "Equipment":
                                EquipmentDetailUI loEquipmentDetail = new EquipmentDetailUI(lRecord);
                                loEquipmentDetail.ParentList = this;
                                loEquipmentDetail.ShowDialog();
                                break;
                            case "Building":
                                BuildingDetailUI loBuildingDetail = new BuildingDetailUI(lRecord);
                                loBuildingDetail.ParentList = this;
                                loBuildingDetail.ShowDialog();
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
                    case "ChartOfAccount":
                        _DisplayFields = "SELECT coa.Id, coa.Code, c.Description AS Classification, "+
		                    "sc.Description AS `Sub Classification`,ma.Description AS `Main Account`, "+
		                    "coa.AccountTitle AS `Account Title`,coa.TypeOfAccount AS `Type of Account`, "+
		                    "coa.Subsidiary,coa.ContraAccount AS `Contra Account`,coa.Remarks "+
		                    "FROM chartofaccount coa "+
		                    "LEFT JOIN classification c "+
		                    "ON coa.ClassificationId = c.Id "+
		                    "LEFT JOIN subclassification sc "+
		                    "ON coa.SubClassificationId = sc.Id "+
                            "LEFT JOIN mainaccount ma " +
		                    "ON coa.MainAccountId = ma.Id ";
                        _WhereFields = " AND coa.`Status` = 'Active' ORDER BY coa.AccountTitle ASC;";
                        _Alias = "coa.";
                        break;
                    case "MainAccount":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM mainaccount ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Classification":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM classification ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "SubClassification":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM subclassification ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Bank":
                        _DisplayFields = "SELECT Id,`Code`,Description,AccountNo AS `Account No.`,EmailAddress AS `Email Address`, "+
                            "ContactPerson AS `Contact Person`, Remarks " +
		                    "FROM bank ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Equipment":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM equipment ";
                        _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                        _Alias = "";
                        break;
                    case "Building":
                        _DisplayFields = "SELECT Id,`Code`,Description,Remarks "+
		                    "FROM building ";
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
                        case "ChartOfAccount":
                            ChartOfAccountRpt loChartOfAccountRpt = new ChartOfAccountRpt();
                            loChartOfAccountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loChartOfAccountRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loChartOfAccountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loChartOfAccountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loChartOfAccountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loChartOfAccountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loChartOfAccountRpt.SetParameterValue("Title", "Chart of Account List");
                            loChartOfAccountRpt.SetParameterValue("SubTitle", "Chart of Account List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loChartOfAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loChartOfAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loChartOfAccountRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loChartOfAccountRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Classification":
                            ClassificationRpt loClassification = new ClassificationRpt();
                            loClassification.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loClassification.Database.Tables[1].SetDataSource(ldtShow);
                            loClassification.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loClassification.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loClassification.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loClassification.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loClassification.SetParameterValue("Title", "Classification List");
                            loClassification.SetParameterValue("SubTitle", "Classification List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loClassification.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loClassification.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loClassification.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loClassification;
                            loReportViewer.ShowDialog();
                            break;
                        case "SubClassification":
                            SubClassificationRpt loSubClassificationRpt = new SubClassificationRpt();
                            loSubClassificationRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loSubClassificationRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loSubClassificationRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loSubClassificationRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loSubClassificationRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loSubClassificationRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loSubClassificationRpt.SetParameterValue("Title", "Sub Classification List");
                            loSubClassificationRpt.SetParameterValue("SubTitle", "Sub Classification List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loSubClassificationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loSubClassificationRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loSubClassificationRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loSubClassificationRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "MainAccount":
                            MainAccountRpt loMainAccountRpt = new MainAccountRpt();
                            loMainAccountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loMainAccountRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loMainAccountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loMainAccountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loMainAccountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loMainAccountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loMainAccountRpt.SetParameterValue("Title", "Main Account List");
                            loMainAccountRpt.SetParameterValue("SubTitle", "Main Account List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loMainAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loMainAccountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loMainAccountRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loMainAccountRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Equipment":
                            EquipmentRpt loEquipmentRpt = new EquipmentRpt();
                            loEquipmentRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loEquipmentRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loEquipmentRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loEquipmentRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loEquipmentRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loEquipmentRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loEquipmentRpt.SetParameterValue("Title", "Equipment List");
                            loEquipmentRpt.SetParameterValue("SubTitle", "Equipment List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loEquipmentRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loEquipmentRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loEquipmentRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loEquipmentRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Building":
                            BuildingRpt loBuildingRpt = new BuildingRpt();
                            loBuildingRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loBuildingRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loBuildingRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loBuildingRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loBuildingRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loBuildingRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loBuildingRpt.SetParameterValue("Title", "Building List");
                            loBuildingRpt.SetParameterValue("SubTitle", "Building List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loBuildingRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loBuildingRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loBuildingRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loBuildingRpt;
                            loReportViewer.ShowDialog();
                            break;
                        case "Bank":
                            BankRpt loBankRpt = new BankRpt();
                            loBankRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loBankRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loBankRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loBankRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loBankRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loBankRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loBankRpt.SetParameterValue("Title", "Bank List");
                            loBankRpt.SetParameterValue("SubTitle", "Bank List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loBankRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loBankRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }

                            }
                            catch
                            {
                                loBankRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loBankRpt;
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
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Code" || this.dgvLists.Columns[e.ColumnIndex].Name == "Contra Account")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                /*
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Value")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                */
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
