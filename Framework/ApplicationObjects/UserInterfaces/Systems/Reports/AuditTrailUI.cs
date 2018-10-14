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

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems.Reports
{
    public partial class AuditTrailUI : Form
    {
        #region "VARIABLES"
        AuditTrail loAuditTrail;
        DataTable ldtAuditTrail;
        SearchesUI loSearches;
        Common loCommon;
        ReportViewerUI loReportViewer;
        AuditTrailRpt loAuditTrailRpt;
        #endregion

        #region "CONTRUCTORS"
        public AuditTrailUI()
        {
            InitializeComponent();
            loAuditTrail = new AuditTrail();
            ldtAuditTrail = new DataTable();
            loCommon = new Common();
            loReportViewer = new ReportViewerUI();
            loAuditTrailRpt = new AuditTrailRpt();
        }
        #endregion 

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public void refresh()
        {
            try
            {
                ldtAuditTrail = loAuditTrail.getAuditTrailByDate(dtpFrom.Value, dtpTo.Value);
                GlobalFunctions.refreshGrid(ref dgvAuditTrail, ldtAuditTrail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void remove()
        {
            try
            {
                DialogResult _dr = new DialogResult();
                MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                _mb.ShowDialog();
                _dr = _mb.Operation;
                if (_dr == DialogResult.Yes)
                {
                    loAuditTrail.removeAuditTrail(dtpFrom.Value, dtpTo.Value);
                    MessageBoxUI _mb1 = new MessageBoxUI("Audit Trail from " + dtpFrom.Value.ToShortDateString() + " until " + dtpTo.Value.ToShortDateString() + " has been successfully removed!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb1.ShowDialog();
                    refresh();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmAuditTrail", "Search"))
                {
                    return;
                }

                string _AuditTrailDisplayFields = "SELECT CONVERT(VARCHAR(12), [Date], 107) AS [Transaction Date], " +
                            "CONVERT(VARCHAR(12), [Date], 108) AS [Transaction Time], " +
                            "LogDescription AS [Log Description], Hostname AS [Computer Name] " +
                            "FROM audittrail ";
                string _AuditTrailWhereFields = " ORDER BY [Date] ASC;";
                loSearches.lAlias = "";
                loSearches.ShowDialog();
                if (loSearches.lFromShow)
                {
                    ldtAuditTrail = loCommon.getDataFromSearch(_AuditTrailDisplayFields + loSearches.lQuery + _AuditTrailWhereFields);
                    GlobalFunctions.refreshGrid(ref dgvAuditTrail, ldtAuditTrail);
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
                if (!GlobalFunctions.checkRights("tsmAuditTrail", "Preview"))
                {
                    return;
                }

                if (dgvAuditTrail.Rows.Count > 0)
                {
                    loAuditTrailRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                    loAuditTrailRpt.Database.Tables[1].SetDataSource(ldtAuditTrail);
                    loAuditTrailRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    loAuditTrailRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    loAuditTrailRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    loAuditTrailRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                    loAuditTrailRpt.SetParameterValue("Title", "Audit Trail");
                    loAuditTrailRpt.SetParameterValue("SubTitle", "Audit Trail");
                    loReportViewer.crystalReportViewer.ReportSource = loAuditTrailRpt;
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmAuditTrail", "Refresh"))
                {
                    return;
                }
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRefresh_Click");
                em.ShowDialog();
                return;
            }
        }

        private void AuditTrailUI_Load(object sender, EventArgs e)
        {
            try
            {
                Type _Type = typeof(AuditTrail);
                FieldInfo[] myFieldInfo;
                myFieldInfo = _Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
                loSearches = new SearchesUI(myFieldInfo, _Type, "tsmAuditTrail");
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "AuditTrailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void dgvAuditTrail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = dgvAuditTrail.PointToScreen(e.Location);
                cmsFunctions.Show(pt);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmAuditTrail", "View"))
                {
                    return;
                }
                refresh();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnView_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmAuditTrail", "Remove"))
                {
                    return;
                }
                remove();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRemove_Click");
                em.ShowDialog();
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmAuditTrail", "Search"))
            {
                return;
            }
        }

        private void tsmViewHiddenRecords_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalFunctions.refreshAll(ref dgvAuditTrail, ldtAuditTrail);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmViewHiddenRecords_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh_Click(null, new EventArgs());
        }

        private void tsmSearch_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void tsmPreview_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, new EventArgs());
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
    }
}
