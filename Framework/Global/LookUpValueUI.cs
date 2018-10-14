using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace NSites_V.Global
{
    public partial class LookUpValueUI : Form
    {
        #region "VARIABLES"
        public string lCode;
        public string lDesc;
        public string lValue3;
        public string lValue4;
        public string lValue5;
        public string lValue6;
        public string lValue7;
        public string lValue8;
        public string lCodeName;
        public string lDescName;
        public bool lFromSelection;
        public object lObject;
        public Type lType;
        Type lTypeUI;
        public string lTableName;
        string[] lColumnName;
        int lCountCol;
        DataTable loRecord;
        #endregion "END OF VARIABLES"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "CONSTRUCTORS"

        public LookUpValueUI()
        {
            InitializeComponent();
            loRecord = new DataTable();
            lCode = "";
            lDesc = "";
            lValue3 = "";
            lValue4 = "";
            lValue5 = "";
            lValue6 = "";
            lValue7 = "";
            lValue8 = "";
            lCodeName = "";
            lDescName = "";
            lFromSelection= false;
        }

        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"

        public void refresh(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            try
            {
                loRecord.Rows.Clear();
                object[] _params = { pDisplayType,pPrimaryKey, pSearchString };
                loRecord = (DataTable)lObject.GetType().GetMethod("getAllData").Invoke(lObject, _params);
                dgvLookUp.Rows.Clear();
                dgvLookUp.Columns.Clear();
                lCountCol = loRecord.Columns.Count;
                lColumnName = new string[lCountCol];
                for (int i = 0; i < lCountCol; i++)
                {
                    string str = loRecord.Columns[i].ColumnName.Replace("*","");
                    if (str.Contains('_'))
                    {
                        if (str.Contains('*'))
                        {
                            string newstr = str.Replace('_', ' ');
                            dgvLookUp.Columns.Add(newstr.Replace("*", ""), newstr.Replace("*", ""));
                            dgvLookUp.Columns[i].Visible = false;
                            lColumnName[i] = newstr.Replace("*", "");
                        }
                        else
                        {
                            dgvLookUp.Columns.Add(str, str.Replace('_', ' '));
                            lColumnName[i] = str.Replace('_', ' ');
                        }
                    }
                    else
                    {
                        dgvLookUp.Columns.Add(str, str);
                        lColumnName[i] = str;
                    }
                }
                foreach (DataRow _dr in loRecord.Rows)
                {
                    int n = dgvLookUp.Rows.Add();
                    if (n < GlobalVariables.DisplayRecordLimit)
                    {
                        for (int i = 0; i < lCountCol; i++)
                        {
                            dgvLookUp.Rows[n].Cells[i].Value = _dr[i].ToString();
                        }
                    }
                    else
                    {
                        dgvLookUp.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                        dgvLookUp.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                        dgvLookUp.Rows[n].Height = 5;
                        dgvLookUp.Rows[n].ReadOnly = true;
                        break;
                    }
                }
                try
                {
                    lCode = dgvLookUp.CurrentRow.Cells[0].Value.ToString();
                    lDesc = dgvLookUp.CurrentRow.Cells[1].Value.ToString();
                    lCodeName = split(dgvLookUp.Columns[0].Name);
                    lDescName = split(dgvLookUp.Columns[1].Name);
                }
                catch
                { }
            }
            catch(Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                this.Close();
            }
        }
        public void refreshAll()
        {
            dgvLookUp.Rows.Clear();
            foreach (DataRow _dr in loRecord.Rows)
            {
                int n = dgvLookUp.Rows.Add();
                for (int i = 0; i < lCountCol; i++)
                {
                    dgvLookUp.Rows[n].Cells[i].Value = _dr[i].ToString();
                }
            }
            try
            {
                lCode = dgvLookUp.CurrentRow.Cells[0].Value.ToString();
                lDesc = dgvLookUp.CurrentRow.Cells[1].Value.ToString();
                lCodeName = split(dgvLookUp.Columns[0].Name);
                lDescName = split(dgvLookUp.Columns[1].Name);
            }
            catch
            { }
        }
        public void refreshFromSearch(string pViewType,string pPrimaryKey ,string pSearchString)
        {
            try
            {
                loRecord.Rows.Clear();
                object[] _params = { pViewType, pPrimaryKey, pSearchString };
                loRecord = (DataTable)lObject.GetType().GetMethod("getAllData").Invoke(lObject, _params);
                dgvLookUp.Rows.Clear();
                dgvLookUp.Columns.Clear();
                lCountCol = loRecord.Columns.Count;
                lColumnName = new string[lCountCol];
                for (int i = 0; i < lCountCol; i++)
                {
                    string str = loRecord.Columns[i].ColumnName;
                    if (str.Contains('_'))
                    {
                        if (str.Contains('*'))
                        {
                            string newstr = str.Replace('_', ' ');
                            dgvLookUp.Columns.Add(newstr.Replace("*", ""), newstr.Replace("*", ""));
                            dgvLookUp.Columns[i].Visible = false;
                            lColumnName[i] = newstr.Replace("*", "");
                        }
                        else
                        {
                            dgvLookUp.Columns.Add(str, str.Replace('_', ' '));
                            lColumnName[i] = str.Replace('_', ' ');
                        }
                    }
                    else
                    {
                        dgvLookUp.Columns.Add(str, str);
                        lColumnName[i] = str;
                    }
                }
                foreach (DataRow _dr in loRecord.Rows)
                {
                    int n = dgvLookUp.Rows.Add();
                    if (n < GlobalVariables.DisplayRecordLimit)
                    {
                        for (int i = 0; i < lCountCol; i++)
                        {
                            dgvLookUp.Rows[n].Cells[i].Value = _dr[i].ToString();
                        }
                    }
                    else
                    {
                        dgvLookUp.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                        dgvLookUp.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                        dgvLookUp.Rows[n].Height = 5;
                        dgvLookUp.Rows[n].ReadOnly = true;
                        break;
                    }
                }
                try
                {
                    lCode = dgvLookUp.CurrentRow.Cells[0].Value.ToString();
                    lDesc = dgvLookUp.CurrentRow.Cells[1].Value.ToString();
                    lCodeName = split(dgvLookUp.Columns[0].Name);
                    lDescName = split(dgvLookUp.Columns[1].Name);
                }
                catch
                { }
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                this.Close();
            }
        }
        private string split(string pstr)
        {
            char[] splitter = { '_' };
            string[] str = pstr.Split(splitter);
            string newstr = "";
            for (int i = 0; i < str.Length; i++)
            {
                newstr += str[i] + " ";
            }
            return newstr.Replace(" ", "");
        }

        #endregion "END OF METHODS"

        #region "EVENTS"

        private void LookUpValueUI_Load(object sender, EventArgs e)
        {
            lblTableName.Text = lTableName;
            lCode = "";
            lDesc = "";
            lValue3 = "";
            lValue4 = "";
            lValue5 = "";
            lValue6 = "";
            lValue7 = "";
            lValue8 = "";
            lCodeName = "";
            lDescName = "";
            lFromSelection = false;
            refresh("ViewAll","","");
        }

        private void dgvLookUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                try
                {
                    int row = dgvLookUp.CurrentRow.Index - 1;
                    lCode = dgvLookUp.Rows[row].Cells[0].Value.ToString();
                    lDesc = dgvLookUp.Rows[row].Cells[1].Value.ToString();
                    lCodeName = split(dgvLookUp.Columns[0].Name);
                    lDescName = split(dgvLookUp.Columns[1].Name);
                    lFromSelection = true;
                    this.Close();
                }
                catch
                {
                    try
                    {
                        lCode = dgvLookUp.CurrentRow.Cells[0].Value.ToString();
                        lDesc = dgvLookUp.CurrentRow.Cells[1].Value.ToString();
                        lCodeName = split(dgvLookUp.Columns[0].Name);
                        lDescName = split(dgvLookUp.Columns[1].Name);
                        lFromSelection = true;
                        this.Close();
                    }
                    catch
                    {
                        this.Close();
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            refreshFromSearch("", "", txtSearch.Text);
        }

        private void viewAllRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh("ViewAll", "", "");
        }

        private void dgvLookUp_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = dgvLookUp.PointToScreen(e.Location);
                cmsFunctions.Show(pt);
            }
        }

        private void viewHiddenRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshAll();
        }
        #endregion "END OF EVENTS"

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ConstructorInfo ctor = lTypeUI.GetConstructor(new[] { typeof(string[]), typeof(object), typeof(Type) });
            object instance = ctor.Invoke(new object[] { lColumnName, lObject, lType });
            PropertyInfo ParentListPropertyInfo = lTypeUI.GetProperty("ParentList");
            ParentListPropertyInfo.SetValue(instance, this, null);

            Form form = instance as Form;
            if (form != null)
            {
                form.ShowDialog();
                refresh("ViewAll", "", "");
            }
        }

        private void btnNull_Click(object sender, EventArgs e)
        {
            lCode = "";
            lDesc = "";
            try
            {
                lValue3 = "";
            }
            catch { }
            try
            {
                lValue4 = "";
            }
            catch { }
            try
            {
                lValue5 = "";
            }
            catch { }
            try
            {
                lValue6 = "";
            }
            catch { }
            try
            {
                lValue7 = "";
            }
            catch { }
            try
            {
                lValue8 = "";
            }
            catch { }
            lCodeName = split(dgvLookUp.Columns[0].Name);
            lDescName = split(dgvLookUp.Columns[1].Name);
            lFromSelection = true;
            this.Close();
        }

        private void dgvLookUp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lCode = dgvLookUp.CurrentRow.Cells[0].Value.ToString();
            lDesc = dgvLookUp.CurrentRow.Cells[1].Value.ToString();
            try
            {
                lValue3 = dgvLookUp.CurrentRow.Cells[2].Value.ToString();
            }
            catch { }
            try
            {
                lValue4 = dgvLookUp.CurrentRow.Cells[3].Value.ToString();
            }
            catch { }
            try
            {
                lValue5 = dgvLookUp.CurrentRow.Cells[4].Value.ToString();
            }
            catch { }
            try
            {
                lValue6 = dgvLookUp.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }
            try
            {
                lValue7 = dgvLookUp.CurrentRow.Cells[6].Value.ToString();
            }
            catch { }
            try
            {
                lValue8 = dgvLookUp.CurrentRow.Cells[7].Value.ToString();
            }
            catch { }
            lCodeName = split(dgvLookUp.Columns[0].Name);
            lDescName = split(dgvLookUp.Columns[1].Name);
            lFromSelection = true;
            this.Close();
        }

        private void dgvLookUp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvLookUp.Columns[e.ColumnIndex].Name == "Id")
                {
                    this.dgvLookUp.Columns[e.ColumnIndex].Visible = false;
                }
                else if (this.dgvLookUp.Columns[e.ColumnIndex].Name == "Code" ||
                    this.dgvLookUp.Columns[e.ColumnIndex].Name == "Username" ||
                    this.dgvLookUp.Columns[e.ColumnIndex].Name == "Employee No.")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch { }
        }
    }
}
