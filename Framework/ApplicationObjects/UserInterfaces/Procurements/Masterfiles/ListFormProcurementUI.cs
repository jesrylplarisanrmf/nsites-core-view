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

using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports.MasterfileRpt;

namespace NSites_V.ApplicationObjects.UserInterfaces.Procurements.Masterfiles
{
    public partial class ListFormProcurementUI : Form
    {
        #region "VARIABLES"
        public object lObject;
        Type lType;
        string[] lRecord;
        string[] lColumnName;
        int lCountCol;
        SearchesUI loSearches;
        Common loCommon;
        ReportViewerUI loReportViewer;
        
        System.Data.DataTable ldtShow;
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
        public ListFormProcurementUI(object pObject, Type pType)
        {
            InitializeComponent();
            lObject = pObject;
            lType = pType;
            this.Text = pObject.GetType().Name + " List";
            loCommon = new Common();
            loReportViewer = new ReportViewerUI();
            
            ldtShow = new System.Data.DataTable();
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
                    //Procurement
                    case "ProcurementDiscount":
                        ProcurementDiscountDetailUI loProcurementDiscountDetail = new ProcurementDiscountDetailUI();
                        loProcurementDiscountDetail.ParentList = this;
                        loProcurementDiscountDetail.ShowDialog();
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
                            //Procurement
                            case "ProcurementDiscount":
                                ProcurementDiscountDetailUI loProcurementDiscountDetail = new ProcurementDiscountDetailUI(lRecord);
                                loProcurementDiscountDetail.ParentList = this;
                                loProcurementDiscountDetail.ShowDialog();
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
                    case "ProcurementDiscount":
                        _DisplayFields = "SELECT Id,`Code`,Description,`Type`,`Value`,Remarks "+
		                    "FROM procurementdiscount";
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
                        case "ProcurementDiscount":
                            ProcurementDiscountRpt loProcurementDiscountRpt = new ProcurementDiscountRpt();
                            loProcurementDiscountRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                            loProcurementDiscountRpt.Database.Tables[1].SetDataSource(ldtShow);
                            loProcurementDiscountRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                            loProcurementDiscountRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                            loProcurementDiscountRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                            loProcurementDiscountRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                            loProcurementDiscountRpt.SetParameterValue("Title", "Procurement Discount List");
                            loProcurementDiscountRpt.SetParameterValue("SubTitle", "Procurement Discount List");
                            try
                            {
                                if (loSearches.lAlias == "")
                                {
                                    loProcurementDiscountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", ""));
                                }
                                else
                                {
                                    loProcurementDiscountRpt.SetParameterValue("QueryString", loSearches.lQuery.Replace("WHERE ", "").Replace(loSearches.lAlias, ""));
                                }
                                
                            }
                            catch
                            {
                                loProcurementDiscountRpt.SetParameterValue("QueryString", "");
                            }
                            loReportViewer.crystalReportViewer.ReportSource = loProcurementDiscountRpt;
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
                else if (this.dgvLists.Columns[e.ColumnIndex].Name == "Code")
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

        private void ListFormProcurementUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type;
                FieldInfo[] myFieldInfo;
                switch (lType.Name)
                {
                    case "ProcurementDiscount":
                        _Type = typeof(ProcurementDiscount);
                        myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        loSearches = new SearchesUI(myFieldInfo, _Type, "tsm" + _Type.Name);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "ListFormProcurementUI_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }
    }
}
