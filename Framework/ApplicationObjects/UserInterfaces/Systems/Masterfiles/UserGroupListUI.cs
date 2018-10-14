using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Systems;
using NSites_V.ApplicationObjects.Classes.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Reports.ReportRpt;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles;

using oUserGroup = NSites_V.ApplicationObjects.Classes.Systems.UserGroup;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles
{
    public partial class UserGroupListUI : Form
    {
        #region "VARIABLES"
        UserGroup loUserGroup;

        string lId;
        string lDescription;
        string lRemarks;

        DataTable lMenuItemsDT;
        DataTable lAllMenuItemDT;
        DataView lUserGroupDV;
        SearchesUI loSearches;
        Common loCommon;
        DataTable ldtShow;
        ReportViewerUI loReportViewer;
        bool lFromRefresh;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public UserGroupListUI()
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            lMenuItemsDT = new DataTable();
            lAllMenuItemDT = new DataTable();
            loCommon = new Common();
            ldtShow = new DataTable();
            loReportViewer = new ReportViewerUI();
            lFromRefresh = false;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public void refresh(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            try
            {
                ldtShow = loUserGroup.getAllData(pDisplayType, pPrimaryKey, pSearchString);
                GlobalFunctions.refreshGrid(ref dgvUserGroups, ldtShow);
                
                lAllMenuItemDT = loUserGroup.getAllMenuItems();
                lFromRefresh = true;

                lId = dgvUserGroups.CurrentRow.Cells[0].Value.ToString();
                lDescription = dgvUserGroups.CurrentRow.Cells[1].Value.ToString();
                lRemarks = dgvUserGroups.CurrentRow.Cells[2].Value.ToString();
                loadAllMenuItems();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void loadAllMenuItems()
        {
            try
            {
                lMenuItemsDT = loUserGroup.getMenuItemsByGroup(dgvUserGroups.CurrentRow.Cells[0].Value.ToString());
                lUserGroupDV = new DataView(lMenuItemsDT);
                dgvMenuItems.Rows.Clear();
                string _menuText = "";
                foreach (DataRow _dr in lAllMenuItemDT.Rows)
                {
                    int n = dgvMenuItems.Rows.Add();

                    dgvMenuItems.Rows[n].Cells[0].Value = _dr["MenuName"].ToString();

                    if (_menuText != _dr["MenuText"].ToString())
                    {
                        dgvMenuItems.Rows[n].Cells[1].Value = _dr["MenuText"].ToString();
                        _menuText = _dr["MenuText"].ToString();
                    }
                    dgvMenuItems.Rows[n].Cells[2].Value = _dr["ItemName"].ToString();
                    dgvMenuItems.Rows[n].Cells[3].Value = _dr["ItemText"].ToString();
                    dgvMenuItems.Rows[n].Cells[4].Value = "Disable";
                    try
                    {
                        lUserGroupDV.RowFilter = "Menu = '" + _dr["MenuName"].ToString() + "'";
                        lUserGroupDV.RowFilter = "Item = '" + _dr["ItemName"].ToString() + "'";
                        if (lUserGroupDV.Count != 0)
                        {
                            dgvMenuItems.Rows[n].Cells[4].Value = "Enable";
                        }
                    }
                    catch { }
                }

                if (dgvMenuItems.CurrentRow.Cells[4].Value.ToString() == "Enable")
                {
                    loadAllEnableRights(dgvMenuItems.CurrentRow.Cells[2].Value.ToString(), lId);
                }
                else
                {
                    dgvRights.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadAllEnableRights(string pItemName, string pUserGroupId)
        {
            try
            {
                DataTable _dtAll = new DataTable();
                DataTable _dtEnable = new DataTable();
                _dtAll = loUserGroup.getAllRights(pItemName);
                _dtEnable = loUserGroup.getEnableRights(pItemName, pUserGroupId);
                DataView _dvEnable = new DataView(_dtEnable);
                dgvRights.Rows.Clear();
                foreach (DataRow _dr in _dtAll.Rows)
                {
                    int n = dgvRights.Rows.Add();
                    dgvRights.Rows[n].Cells[0].Value = _dr["ItemName"].ToString();
                    dgvRights.Rows[n].Cells[1].Value = _dr["Rights"].ToString();
                    dgvRights.Rows[n].Cells[2].Value = "Disable";
                    try
                    {
                        _dvEnable.RowFilter = "Rights = '" + _dr["Rights"].ToString() + "'";
                        if (_dvEnable.Count != 0)
                        {
                            dgvRights.Rows[n].Cells[2].Value = "Enable";
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void loadAllRights(string pItemName, string pUserGroupId)
        {
            try
            {
                DataTable _dtAll = new DataTable();
                DataTable _dtEnable = new DataTable();
                _dtAll = loUserGroup.getAllRights(pItemName);
                dgvRights.Rows.Clear();
                foreach (DataRow _dr in _dtAll.Rows)
                {
                    int n = dgvRights.Rows.Add();
                    dgvRights.Rows[n].Cells[0].Value = _dr["ItemName"].ToString();
                    dgvRights.Rows[n].Cells[1].Value = _dr["Rights"].ToString();
                    dgvRights.Rows[n].Cells[2].Value = "Disable";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion "END OF METHODS"

        #region "EVENTS"
        private void UserGroupListUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                _Type = typeof(UserGroup);
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmUserGroup");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormSystemUI_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void dgvUserGroups_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point pt = dgvUserGroups.PointToScreen(e.Location);
                    cmsFunctions.Show(pt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvUserGroups_MouseClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvUserGroups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lId = dgvUserGroups.CurrentRow.Cells[0].Value.ToString();
                lDescription = dgvUserGroups.CurrentRow.Cells[1].Value.ToString();
                lRemarks = dgvUserGroups.CurrentRow.Cells[2].Value.ToString();
                loadAllMenuItems();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvUserGroups_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvUserGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lId = dgvUserGroups.CurrentRow.Cells[0].Value.ToString();
                lDescription = dgvUserGroups.CurrentRow.Cells[1].Value.ToString();
                lRemarks = dgvUserGroups.CurrentRow.Cells[2].Value.ToString();
                btnUpdate_Click(null, new EventArgs());
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvUserGroups_CellDoubleClick");
                em.ShowDialog();
                return;
            }
        }

        private void dgvMenuItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                chkRights.Checked = false;
                if (this.dgvMenuItems.Columns[e.ColumnIndex].Name == "Status")
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)dgvMenuItems.Rows[dgvMenuItems.CurrentRow.Index].Cells[4];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "Enable":
                            ch1.Value = "Disable";
                            dgvRights.Rows.Clear();
                            break;
                        case "Disable":
                            ch1.Value = "Enable";
                            loadAllEnableRights(dgvMenuItems.CurrentRow.Cells[2].Value.ToString(), lId);
                            break;
                    }
                }
                else
                {
                    if (dgvMenuItems.CurrentRow.Cells[4].Value.ToString() == "Enable")
                    {
                        loadAllEnableRights(dgvMenuItems.CurrentRow.Cells[2].Value.ToString(), lId);
                    }
                    else
                    {
                        loadAllRights(dgvMenuItems.CurrentRow.Cells[2].Value.ToString(), lId);
                    }
                }
            }
            catch { }
            /*(Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvMenuItems_CellClick");
                em.ShowDialog();
                return;
            }*/
        }

        private void dgvRights_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dgvRights.Columns[e.ColumnIndex].Name == "RightStatus")
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)dgvRights.Rows[dgvRights.CurrentRow.Index].Cells[2];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "Enable":
                            ch1.Value = "Disable";
                            break;
                        case "Disable":
                            ch1.Value = "Enable";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvRights_CellClick");
                em.ShowDialog();
                return;
            }
        }

        private void btnSaveRights_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Save Rights"))
                {
                    return;
                }

                if (loUserGroup.updateUserGroupRights(lId, dgvMenuItems.CurrentRow.Cells[2].Value.ToString(), GlobalFunctions.convertDataGridToDataTable(dgvRights)))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Rights has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSaveRights_Click");
                em.ShowDialog();
                return;
            }
        }

        private void chkRights_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRights.Checked)
                {
                    for (int i = 0; i < dgvRights.Rows.Count; i++)
                    {
                        dgvRights.Rows[i].Cells[2].Value = "Enable";
                    }
                }
                else
                {
                    for (int i = 0; i < dgvRights.Rows.Count; i++)
                    {
                        dgvRights.Rows[i].Cells[2].Value = "Disable";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "chkRights_CheckedChanged");
                em.ShowDialog();
                return;
            }
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCheckAll.Checked)
                {
                    for (int i = 0; i < dgvMenuItems.Rows.Count; i++)
                    {
                        dgvMenuItems.Rows[i].Cells[4].Value = "Enable";
                    }
                }
                else
                {
                    for (int i = 0; i < dgvMenuItems.Rows.Count; i++)
                    {
                        dgvMenuItems.Rows[i].Cells[4].Value = "Disable";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "chkCheckAll_CheckedChanged");
                em.ShowDialog();
                return;
            }
        }

        #endregion "END OF REGION"

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Refresh"))
                {
                    return;
                }
                refresh("ViewAll", "", "");
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
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Create"))
                {
                    return;
                }
                UserGroupDetailUI _UserGroup = new UserGroupDetailUI();
                _UserGroup.ParentList = this;
                _UserGroup.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Update"))
                {
                    return;
                }
                if (lId != null)
                {
                    UserGroupDetailUI _UserGroupDetail = new UserGroupDetailUI(lId, lDescription, lRemarks);
                    _UserGroupDetail.ParentList = this;
                    _UserGroupDetail.ShowDialog();
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
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Remove"))
                {
                    return;
                }
                if (lId != null)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        if (loUserGroup.removeUserGroup(lId))
                        {
                            MessageBoxUI _mb1 = new MessageBoxUI("User Group has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                            _mb1.ShowDialog();
                            refresh("ViewAll", "", "");
                            loadAllMenuItems();
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
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Search"))
                {
                    return;
                }

                string _DisplayFields = "";
                string _WhereFields = "";
                string _Alias = "";

                _DisplayFields = "SELECT Id,Description,Remarks " +
		            "FROM usergroup ";
                _WhereFields = " AND `Status` = 'Active' ORDER BY Description ASC;";
                _Alias = "";

                loSearches.lAlias = _Alias;
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtShow = loCommon.getDataFromSearch(_DisplayFields + loSearches.lQuery + _WhereFields);
                    GlobalFunctions.refreshGrid(ref dgvUserGroups, ldtShow);
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
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Preview"))
                {
                    return;
                }
                if (dgvUserGroups.Rows.Count != 0)
                {
                    UserGroupRpt loUserGroupRpt = new UserGroupRpt();
                    loUserGroupRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loUserGroupRpt.Database.Tables[1].SetDataSource(ldtShow);
                    loUserGroupRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loUserGroupRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loUserGroupRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loUserGroupRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loUserGroupRpt.SetParameterValue("Title", "Users List");
                    loUserGroupRpt.SetParameterValue("SubTitle", "Users List");
                    try
                    {
                        if (loSearches.lAlias == "")
                        {
                            loUserGroupRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                        }
                        else
                        {
                            loUserGroupRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                        }

                    }
                    catch
                    {
                        loUserGroupRpt.SetParameterValue("QueryString", "");
                    }
                    loReportViewer.crystalReportViewer.ReportSource = loUserGroupRpt;
                    loReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnPreview_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                refresh("", "", txtSearch.Text);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "txtSearch_TextChanged");
                em.ShowDialog();
                return;
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

        private void dgvUserGroups_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                lId = dgvUserGroups.CurrentRow.Cells[0].Value.ToString();
                lDescription = dgvUserGroups.CurrentRow.Cells[1].Value.ToString();
                lRemarks = dgvUserGroups.CurrentRow.Cells[3].Value.ToString();
                loadAllMenuItems();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvUserGroups_KeyUp");
                em.ShowDialog();
                return;
            }
        }

        private void dgvUserGroups_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvUserGroups.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvUserGroups.Columns[e.ColumnIndex].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "dgvUserGroups_CellFormatting");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiViewHiddenRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvUserGroups, ldtShow);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiViewHiddenRecords_Click");
                em.ShowDialog();
                return;
            }
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

        private void tsmiSaveMenu_Click(object sender, EventArgs e)
        {
            btnSaveMenu_Click(null, new EventArgs());
        }

        private void btnSaveMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Save Menu"))
                {
                    return;
                }

                if (loUserGroup.updateUserGroupMenuItem(dgvUserGroups.CurrentRow.Cells[0].Value.ToString(), GlobalFunctions.convertDataGridToDataTable(dgvMenuItems)))
                {
                    MessageBoxUI _mb = new MessageBoxUI("User Rights has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Failure to save the record!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSaveMenu_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiSaveRights_Click(object sender, EventArgs e)
        {
            btnSaveRights_Click(null, new EventArgs());
        }

        private void tsmiReload_Click(object sender, EventArgs e)
        {
            btnReload_Click(null, new EventArgs());      
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmUserGroup", "Reload"))
                {
                    return;
                }
                loadAllMenuItems();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnReload_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }
    }
}
