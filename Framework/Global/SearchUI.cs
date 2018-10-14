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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Generics;

namespace NSites_V.Global
{
    public partial class SearchUI : Form
    {
        #region "VARIABLES"
        public string lQueryShow;
        public string lQueryReport;
        public string lQuerySum;
        public string lDisplay;
        public bool lFromShow;
        //public string lTableName;
        string lMenuName;
        public string lTitle;
        public string lSubTitle;
        //sum
        public string lSum1;
        public string lSum2;
        public string lSum3;
        public string lSum4;
        public string lSum5;
        public string lSum6;
        public string lSum7;
        public string lSum8;

        public int lNoOfParameters;

        public string lPaperLayout;
        public string lPaperSize;

        public ParameterFields lParamFields;
        FieldInfo[] lFieldInfo;
        Type lType;
        Hashtable itemHash = new Hashtable();
        List<string> lItems = new List<string>();
        LookUpValueUI loLookupValue;
        Common loCommon;
        GlobalVariables.Operation lOperation = GlobalVariables.Operation.Edit;
        bool lFromAdd = false;

        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SearchUI(FieldInfo[] pFieldInfo, Type pType, string pMenuName)
        {
            InitializeComponent();
            lFieldInfo = pFieldInfo;
            lType = pType;
            lMenuName = pMenuName;
            loadFields();
            loadComboBoxes();
            loLookupValue = new LookUpValueUI();
            loCommon = new Common();
            //lTableName = lType.Name;
            loadTemplateName();
            lPaperLayout = "Portrait";
            lPaperSize = "Letter";
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        private void loadFields()
        {
            string _name;
            string _fieldType;
            for (int i = 1; i < lFieldInfo.Length; i++)
            {
                _name = lFieldInfo[i].Name.ToString();
                _fieldType = lFieldInfo[i].FieldType.ToString();
                if (_name.Contains('<'))
                {
                    string[] words = _name.Split('<', '>');
                    lItems.Add(splitUpperCaseWord(words[1]));
                    itemHash.Add(splitUpperCaseWord(words[1]), _fieldType);
                }
                else
                {
                    lItems.Add(splitUpperCaseWord(_name));
                    itemHash.Add(splitUpperCaseWord(_name), _fieldType);
                }
            }
            lsbCurrentFields.DataSource = lItems;
        }

        private string splitUpperCaseWord(string pString)
        {
            String PreString = pString;
            System.Text.StringBuilder SB = new System.Text.StringBuilder();

            foreach (Char C in PreString)
            {
                if (Char.IsUpper(C))
                    SB.Append(' ');
                SB.Append(C);
            }
            return SB.ToString();
        }
        private void loadComboBoxes()
        {
            lItems.ForEach(delegate(String item)
            {
                cboField1.Items.Add(item);
                cboField2.Items.Add(item);
                cboField3.Items.Add(item);
                cboField4.Items.Add(item);
                cboField5.Items.Add(item);
                cboField6.Items.Add(item);
                cboField7.Items.Add(item);
                cboField8.Items.Add(item);
            });

            cboOperator1.SelectedIndex = 0;
            cboOperator2.SelectedIndex = 0;
            cboOperator3.SelectedIndex = 0;
            cboOperator4.SelectedIndex = 0;
            cboOperator5.SelectedIndex = 0;
            cboOperator6.SelectedIndex = 0;
            cboOperator7.SelectedIndex = 0;
            cboOperator8.SelectedIndex = 0;
        }
        private string setDataTime(string pDateTime)
        {
            DateTimePickerUI _datetime = new DateTimePickerUI(pDateTime);
            _datetime.ShowDialog();
            if (_datetime.lSelectDate)
            {
                return String.Format("{0:u}", _datetime.lDateTime).Replace("Z", "");
            }
            else
            {
                return null;
            }
        }
        private string CreateSelectQueryAndParameters()
        {
            ReportDocument reportDocument;
            ParameterFields paramFields;

            ParameterField paramField;
            ParameterDiscreteValue paramDiscreteValue;

            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();

            string query = "SELECT ";
            lQuerySum = "SELECT ";
            string condition = "";
            int columnNo = 0;
            int j = 1;

            //int NoOfParameters;
            if (lPaperLayout == "Portrait" && lPaperSize == "Letter")
            {
                lNoOfParameters = 8;
            }
            else if (lPaperLayout == "Portrait" && lPaperSize == "Legal")
            {
                lNoOfParameters = 8;
            }
            else if (lPaperLayout == "Landscape" && lPaperSize == "Letter")
            {
                lNoOfParameters = 15;
            }
            else if (lPaperLayout == "Landscape" && lPaperSize == "Legal")
            {
                lNoOfParameters = 15;
            }
            else
            {
                lNoOfParameters = 8;
            }

            if (lsbDisplayFields.Items.Count > lNoOfParameters)
            {
                for (int c = 0; c < lNoOfParameters; c++)
                {
                    columnNo++;
                    if (columnNo > 1)
                    {
                        query = query.Insert(query.Length, ", ");
                    }

                    string _ReportFields = lsbDisplayFields.Items[c].ToString().Replace(" - (Sum)", "");
                    string _DisplayFields = _ReportFields.Replace(" ", "");
                    //for sum
                    if (lsbDisplayFields.Items[c].ToString().Contains(" - (Sum)"))
                    {
                        if (j > 1)
                        {
                            lQuerySum = lQuerySum.Insert(lQuerySum.Length, ", ");
                        }
                        lQuerySum = lQuerySum.Insert(lQuerySum.Length, "SUM(" + _DisplayFields + ") as Sum" + columnNo.ToString());
                        j++;
                    }

                    query = query.Insert(query.Length, _DisplayFields + " as Column" +
                            columnNo.ToString());

                    paramField = new ParameterField();
                    paramField.Name = "col" + columnNo.ToString();
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = _ReportFields;
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
            }
            else
            {
                for (int c = 0; c < lsbDisplayFields.Items.Count; c++)
                {
                    columnNo++;
                    if (columnNo > 1)
                    {
                        query = query.Insert(query.Length, ", ");
                    }

                    string _ReportFields = lsbDisplayFields.Items[c].ToString().Replace(" - (Sum)", "");
                    string _DisplayFields = _ReportFields.Replace(" ", "");
                    //for sum
                    if (lsbDisplayFields.Items[c].ToString().Contains(" - (Sum)"))
                    {
                        if (j > 1)
                        {
                            lQuerySum = lQuerySum.Insert(lQuerySum.Length, ", ");
                        }
                        lQuerySum = lQuerySum.Insert(lQuerySum.Length, "SUM(" + _DisplayFields + ") as Sum" + columnNo.ToString());
                        j++;
                    }

                    query = query.Insert(query.Length, _DisplayFields + " as Column" +
                            columnNo.ToString());

                    paramField = new ParameterField();
                    paramField.Name = "col" + columnNo.ToString();
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = _ReportFields;
                    paramField.CurrentValues.Add(paramDiscreteValue);

                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }

                for (int i = columnNo; i < lNoOfParameters; i++)
                {
                    columnNo++;
                    paramField = new ParameterField();
                    paramField.Name = "col" + columnNo.ToString();
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = "";
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
            }

            #region"for where statements"

            if (cboField1.Text != "" && txtValue1.Text != "")
            {
                getCondition(ref condition, cboField1.Text, cboOperator1.Text, txtValue1.Text);
            }
            if (cboField2.Text != "" && txtValue2.Text != "")
            {
                checkCondition(ref condition, chkAND1);
                getCondition(ref condition, cboField2.Text, cboOperator2.Text, txtValue2.Text);
            }
            if (cboField3.Text != "" && txtValue3.Text != "")
            {
                checkCondition(ref condition, chkAND2);
                getCondition(ref condition, cboField3.Text, cboOperator3.Text, txtValue3.Text);
            }
            if (cboField4.Text != "" && txtValue4.Text != "")
            {
                checkCondition(ref condition, chkAND3);
                getCondition(ref condition, cboField4.Text, cboOperator4.Text, txtValue4.Text);
            }
            if (cboField5.Text != "" && txtValue5.Text != "")
            {
                checkCondition(ref condition, chkAND4);
                getCondition(ref condition, cboField5.Text, cboOperator5.Text, txtValue5.Text);
            }

            if (cboField6.Text != "" && txtValue6.Text != "")
            {
                checkCondition(ref condition, chkAND5);
                getCondition(ref condition, cboField6.Text, cboOperator6.Text, txtValue6.Text);
            }

            if (cboField7.Text != "" && txtValue7.Text != "")
            {
                checkCondition(ref condition, chkAND6);
                getCondition(ref condition, cboField7.Text, cboOperator7.Text, txtValue7.Text);
            }

            if (cboField8.Text != "" && txtValue8.Text != "")
            {
                checkCondition(ref condition, chkAND7);
                getCondition(ref condition, cboField8.Text, cboOperator8.Text, txtValue8.Text);
            }
            #endregion

            //if there is any remaining parameter, assign empty value for that 
            //parameter.
            for (int i = columnNo; i < lNoOfParameters; i++)
            {
                columnNo++;
                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "";
                paramField.CurrentValues.Add(paramDiscreteValue);
                //Add the paramField to paramFields
                paramFields.Add(paramField);
            }
            condition = condition.Replace('*', ' ');
            lParamFields = paramFields;
            string[] tables = lType.ToString().Split('.');
            int counttables = tables.Length;
            string tablename = tables[counttables - 1];
            if (condition != "")
            {
                query += " FROM " + tablename + " WHERE (" + condition + ") AND [Statu]='Active'";
                lQuerySum += " FROM " + tablename + " WHERE (" + condition + ") AND [Status]='Active'";
            }
            else
            {
                query += " FROM " + tablename + " [Status]='Active'";
                lQuerySum += " FROM " + tablename + " [Status]='Active'";
            }
            return query;
        }

        private string CreateSelectQuery()
        {
            string query = "SELECT ";
            string condition = "";
            string group = " GROUP BY ";
            int columnNo = 0;

            for (int c = 0; c < lsbDisplayFields.Items.Count; c++)
            {
                columnNo++;
                if (columnNo > 1)
                {
                    query = query.Insert(query.Length, ", ");
                }

                string _FieldDisplay = lsbDisplayFields.Items[c].ToString().Replace(" - (Sum)", "");
                string _Fields = _FieldDisplay.Replace(" ", "");
                query = query.Insert(query.Length, _Fields + " as [" + _FieldDisplay.Remove(0, 1) + "]");
            }

            #region"for where statements"

            if (cboField1.Text != "" && txtValue1.Text != "")
            {
                getCondition(ref condition, cboField1.Text, cboOperator1.Text, txtValue1.Text);
            }
            if (cboField2.Text != "" && txtValue2.Text != "")
            {
                checkCondition(ref condition, chkAND1);
                getCondition(ref condition, cboField2.Text, cboOperator2.Text, txtValue2.Text);
            }
            if (cboField3.Text != "" && txtValue3.Text != "")
            {
                checkCondition(ref condition, chkAND2);
                getCondition(ref condition, cboField3.Text, cboOperator3.Text, txtValue3.Text);
            }
            if (cboField4.Text != "" && txtValue4.Text != "")
            {
                checkCondition(ref condition, chkAND3);
                getCondition(ref condition, cboField4.Text, cboOperator4.Text, txtValue4.Text);
            }
            if (cboField5.Text != "" && txtValue5.Text != "")
            {
                checkCondition(ref condition, chkAND4);
                getCondition(ref condition, cboField5.Text, cboOperator5.Text, txtValue5.Text);
            }

            if (cboField6.Text != "" && txtValue6.Text != "")
            {
                checkCondition(ref condition, chkAND5);
                getCondition(ref condition, cboField6.Text, cboOperator6.Text, txtValue6.Text);
            }

            if (cboField7.Text != "" && txtValue7.Text != "")
            {
                checkCondition(ref condition, chkAND6);
                getCondition(ref condition, cboField7.Text, cboOperator7.Text, txtValue7.Text);
            }

            if (cboField8.Text != "" && txtValue8.Text != "")
            {
                checkCondition(ref condition, chkAND7);
                getCondition(ref condition, cboField8.Text, cboOperator8.Text, txtValue8.Text);
            }
            #endregion

            #region "GROUP BY"
            int i = 0;
            if (cboGroupField1.Text != "")
            {
                group = group.Insert(group.Length, chkGroupBy1.Text);
                i++;
            }
            if (cboGroupField2.Text != "")
            {
                if (i > 0)
                {
                    group = group.Insert(group.Length, ", " + chkGroupBy2.Text);
                }
                else
                {
                    group = group.Insert(group.Length, chkGroupBy2.Text);
                }
                i++;
            }
            if (cboGroupField3.Text != "")
            {
                if (i > 0)
                {
                    group = group.Insert(group.Length, ", " + chkGroupBy3.Text);
                }
                else
                {
                    group = group.Insert(group.Length, chkGroupBy3.Text);
                }
                i++;
            }
            #endregion
            condition = condition.Replace('*', ' ');
            string[] tables = lType.ToString().Split('.');
            int counttables = tables.Length;
            string tablename = tables[counttables - 1];
            if (condition != "")
            {
                query += " FROM " + tablename + " WHERE (" + condition + ") AND [Status]='Active'";
            }
            else
            {
                query += " FROM " + tablename + " [Status]='Active'";
            }
            return query;
        }

        private void getCondition(ref string pCondition, string pField, string pOperator, string pValue)
        {
            switch (pOperator)
            {
                case "Equals":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " = '" + pValue + "'*");
                    break;
                case "Not Equals":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " != '" + pValue + "'*");
                    break;
                case "Greater Than":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " > '" + pValue + "'*");
                    break;
                case "Lesser Than":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " < '" + pValue + "'*");
                    break;
                case "Greater Than or Equals":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " >= '" + pValue + "'*");
                    break;
                case "Lesser Than or Equals":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " <= '" + pValue + "'*");
                    break;
                case "Starts With":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " LIKE '" + pValue + "%'*");
                    break;
                case "Ends With":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " LIKE '%" + pValue + "'*");
                    break;
                case "Contains":
                    pCondition = pCondition.Insert(pCondition.Length, pField.Replace(" ", "") + " LIKE '%" + pValue + "%'*");
                    break;
            }
        }

        private string getOperatorSign(string pOperator)
        {
            switch (pOperator)
            {
                case "Equals":
                    return "=";
                case "Not Equals":
                    return "!=";
                case "Greater Than":
                    return ">";
                case "Lesser Than":
                    return "<";
                case "Greater Than or Equals":
                    return ">=";
                case "Lesser Than or Equals":
                    return "<=";
                case "Starts With":
                    return "";
                case "Ends With":
                    return "";
                case "Contains":
                    return "";
            }
            return "";
        }

        private void checkCondition(ref string pCondition, CheckBox pChkAND)
        {
            if (pCondition.Contains("*"))
            {
                if (pChkAND.Checked)
                {
                    pCondition = pCondition.Replace("*", " AND ");
                }
                else
                {
                    pCondition = pCondition.Replace("*", " OR ");
                }
            }
        }
        private void loadTemplateName()
        {
            cboTemplateName.Items.Clear();
            chkPrivate.Checked = false;
            /*
            foreach (DataRow dr in loCommon.getTemplateNames(lMenuName).Rows)
            {
                cboTemplateName.Items.Add(dr["TemplateName"].ToString());
            }
            */
        }
        /*
        public void displayDetails()
        {
            DataTable _DtDisplayFields = new DataTable();
            DataTable _DtFilters = new DataTable();
            DataTable _DtGroups = new DataTable();
            bool _Private = false;
            _DtDisplayFields = loCommon.getDisplayFields(lMenuName, cboTemplateName.Text);
            _DtFilters = loCommon.getFilters(lMenuName, cboTemplateName.Text);
            _DtGroups = loCommon.getGroups(lMenuName, cboTemplateName.Text);
            if (_DtDisplayFields.Rows.Count > 0)
            {
                lsbDisplayFields.Items.Clear();
                lOperation = GlobalVariables.Operation.Edit;
                foreach (DataRow dr in _DtDisplayFields.Rows)
                {
                    lsbDisplayFields.Items.Add(dr["DisplayFields"].ToString());
                    if (dr["Private"].ToString() == "Y")
                    {
                        _Private = true;
                    }
                }
            }
            else
            {
                if (!lFromAdd)
                {
                    lsbDisplayFields.Items.Clear();
                    chkPrivate.Checked = false;
                }
            }
            if (_DtFilters.Rows.Count > 0)
            {
                foreach (DataRow dr in _DtFilters.Rows)
                {
                    switch (dr["SequenceNo"].ToString())
                    {
                        case "1":
                            cboField1.SelectedItem = dr["Fields"].ToString();
                            cboOperator1.SelectedItem = dr["Operator"].ToString();
                            txtValue1.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND1.Checked = true;
                                chkOR1.Checked = false;
                            }
                            else
                            {
                                chkAND1.Checked = false;
                                chkOR1.Checked = true;
                            }
                            break;
                        case "2":
                            cboField2.SelectedItem = dr["Fields"].ToString();
                            cboOperator2.SelectedItem = dr["Operator"].ToString();
                            txtValue2.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND2.Checked = true;
                                chkOR2.Checked = false;
                            }
                            else
                            {
                                chkAND2.Checked = false;
                                chkOR2.Checked = true;
                            }
                            break;
                        case "3":
                            cboField3.SelectedItem = dr["Fields"].ToString();
                            cboOperator3.SelectedItem = dr["Operator"].ToString();
                            txtValue3.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND3.Checked = true;
                                chkOR3.Checked = false;
                            }
                            else
                            {
                                chkAND3.Checked = false;
                                chkOR3.Checked = true;
                            }
                            break;
                        case "4":
                            cboField4.SelectedItem = dr["Fields"].ToString();
                            cboOperator4.SelectedItem = dr["Operator"].ToString();
                            txtValue4.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND4.Checked = true;
                                chkOR4.Checked = false;
                            }
                            else
                            {
                                chkAND4.Checked = false;
                                chkOR4.Checked = true;
                            }
                            break;
                        case "5":
                            cboField5.SelectedItem = dr["Fields"].ToString();
                            cboOperator5.SelectedItem = dr["Operator"].ToString();
                            txtValue5.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND5.Checked = true;
                                chkOR5.Checked = false;
                            }
                            else
                            {
                                chkAND5.Checked = false;
                                chkOR5.Checked = true;
                            }
                            break;
                        case "6":
                            cboField6.SelectedItem = dr["Fields"].ToString();
                            cboOperator6.SelectedItem = dr["Operator"].ToString();
                            txtValue6.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND6.Checked = true;
                                chkOR6.Checked = false;
                            }
                            else
                            {
                                chkAND6.Checked = false;
                                chkOR6.Checked = true;
                            }
                            break;
                        case "7":
                            cboField7.SelectedItem = dr["Fields"].ToString();
                            cboOperator7.SelectedItem = dr["Operator"].ToString();
                            txtValue7.Text = dr["Values"].ToString();
                            if (dr["CheckAnd"].ToString() == "Y")
                            {
                                chkAND7.Checked = true;
                                chkOR7.Checked = false;
                            }
                            else
                            {
                                chkAND7.Checked = false;
                                chkOR7.Checked = true;
                            }
                            break;
                        case "8":
                            cboField8.SelectedItem = dr["Fields"].ToString();
                            cboOperator8.SelectedItem = dr["Operator"].ToString();
                            txtValue8.Text = dr["Values"].ToString();
                            break;
                    }
                    if (dr["Private"].ToString() == "Y")
                    {
                        _Private = true;
                    }
                }
            }
            else
            {
                if (!lFromAdd)
                {
                    cboField1.SelectedItem = null;
                    cboOperator1.SelectedItem = null;
                    cboOperator1.SelectedIndex = 0;
                    txtValue1.Text = "";
                    chkAND1.Checked = false;
                    chkOR1.Checked = false;
                    cboField2.SelectedItem = null;
                    cboOperator2.SelectedItem = null;
                    cboOperator2.SelectedIndex = 0;
                    txtValue2.Text = "";
                    chkAND2.Checked = false;
                    chkOR2.Checked = false;
                    cboField3.SelectedItem = null;
                    cboOperator3.SelectedItem = null;
                    cboOperator3.SelectedIndex = 0;
                    txtValue3.Text = "";
                    chkAND3.Checked = false;
                    chkOR3.Checked = false;
                    cboField4.SelectedItem = null;
                    cboOperator4.SelectedItem = null;
                    cboOperator4.SelectedIndex = 0;
                    txtValue4.Text = "";
                    chkAND4.Checked = false;
                    chkOR4.Checked = false;
                    cboField5.SelectedItem = null;
                    cboOperator5.SelectedItem = null;
                    cboOperator5.SelectedIndex = 0;
                    txtValue5.Text = "";
                    chkAND5.Checked = false;
                    chkOR5.Checked = false;
                    cboField6.SelectedItem = null;
                    cboOperator6.SelectedItem = null;
                    cboOperator6.SelectedIndex = 0;
                    txtValue6.Text = "";
                    chkAND6.Checked = false;
                    chkOR6.Checked = false;
                    cboField7.SelectedItem = null;
                    cboOperator7.SelectedItem = null;
                    cboOperator7.SelectedIndex = 0;
                    txtValue7.Text = "";
                    chkAND7.Checked = false;
                    chkOR7.Checked = false;
                    cboField8.SelectedItem = null;
                    cboOperator8.SelectedItem = null;
                    cboOperator8.SelectedIndex = 0;
                    txtValue8.Text = "";
                    chkPrivate.Checked = false;
                }
            }

            if (_DtGroups.Rows.Count > 0)
            {
                foreach (DataRow dr in _DtGroups.Rows)
                {
                    switch (dr["SequenceNo"].ToString())
                    {
                        case "1":
                            cboGroupField1.SelectedItem = dr["FieldName"].ToString();
                            if (dr["GroupBy"].ToString() == "Y")
                            {
                                chkGroupBy1.Checked = true;
                            }
                            else
                            {
                                chkGroupBy1.Checked = false;
                            }
                            if (dr["SortBy"].ToString() == "Ascending")
                            {
                                rdbAscesding1.Checked = true;
                            }
                            if (dr["SortBy"].ToString() == "Descending")
                            {
                                rdbDescending1.Checked = true;
                            }
                            break;
                        case "2":
                            cboGroupField2.SelectedItem = dr["FieldName"].ToString();
                            if (dr["GroupBy"].ToString() == "Y")
                            {
                                chkGroupBy2.Checked = true;
                            }
                            else
                            {
                                chkGroupBy2.Checked = false;
                            }
                            if (dr["SortBy"].ToString() == "Ascending")
                            {
                                rdbAscesding2.Checked = true;
                            }
                            if (dr["SortBy"].ToString() == "Descending")
                            {
                                rdbDescending2.Checked = true;
                            }
                            break;
                        case "3":
                            cboGroupField3.SelectedItem = dr["FieldName"].ToString();
                            if (dr["GroupBy"].ToString() == "Y")
                            {
                                chkGroupBy3.Checked = true;
                            }
                            else
                            {
                                chkGroupBy3.Checked = false;
                            }
                            if (dr["SortBy"].ToString() == "Ascending")
                            {
                                rdbAscesding3.Checked = true;
                            }
                            if (dr["SortBy"].ToString() == "Descending")
                            {
                                rdbDescending3.Checked = true;
                            }
                            break;
                    }
                    if (dr["Private"].ToString() == "Y")
                    {
                        _Private = true;
                    }
                }
            }
            else
            {
                if (!lFromAdd)
                {
                    cboGroupField1.SelectedItem = null;
                    chkGroupBy1.Checked = false;
                    rdbAscesding1.Checked = false;
                    rdbDescending1.Checked = false;
                    cboGroupField2.SelectedItem = null;
                    chkGroupBy2.Checked = false;
                    rdbAscesding2.Checked = false;
                    rdbDescending2.Checked = false;
                    cboGroupField3.SelectedItem = null;
                    chkGroupBy3.Checked = false;
                    rdbAscesding3.Checked = false;
                    rdbDescending3.Checked = false;
                    chkPrivate.Checked = false;
                }
            }
            if (_Private)
            {
                chkPrivate.Checked = true;
            }
            else
            {
                chkPrivate.Checked = false;
            }
        }
        */
        #endregion "END OF METHODS"

        #region "EVENTS"
        private void SearchUI_Load(object sender, EventArgs e)
        {
            lFromShow = false;
            lQueryShow = "";
            lQueryReport = "";
            lQuerySum = "";
            lSum1 = "";
            lSum2 = "";
            lSum3 = "";
            lSum4 = "";
            lSum5 = "";
            lSum6 = "";
            lSum7 = "";
            lSum8 = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLookUp1_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField1.Text))
            {
                object val = itemHash[cboField1.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue1.Text = setDataTime(txtValue1.Text);
                }
                else
                {
                    if (cboField1.Text.Contains("Code") || cboField1.Text.Contains("Username") ||
                        cboField1.Text.Contains("Id") || cboField1.Text == "Classification" ||
                        cboField1.Text == "Sub Classification" || cboField1.Text == "Main Account" ||
                        cboField1.Text.Contains("Owner") || cboField1.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField1.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField1.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField1.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField1.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField1.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField1.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField1.Text.Contains("Id"))
                        {
                            _classname = cboField1.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField1.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue1.Text = loLookupValue.lCode;
                            txtValue1.Focus();
                        }
                    }
                    else
                    {
                        txtValue1.Clear();
                        txtValue1.Focus();
                    }
                }
            }
        }

        private void btnLookUp2_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField2.Text))
            {
                object val = itemHash[cboField2.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue2.Text = setDataTime(txtValue2.Text);
                }
                else
                {
                    if (cboField2.Text.Contains("Code") || cboField2.Text.Contains("Username") ||
                        cboField2.Text.Contains("Id") || cboField2.Text == "Classification" ||
                        cboField2.Text == "Sub Classification" || cboField2.Text == "Main Account" ||
                        cboField2.Text.Contains("Owner") || cboField2.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField2.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField2.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField2.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField2.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField2.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField2.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField2.Text.Contains("Id"))
                        {
                            _classname = cboField2.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField2.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue2.Text = loLookupValue.lCode;
                            txtValue2.Focus();
                        }
                    }
                    else
                    {
                        txtValue2.Clear();
                        txtValue2.Focus();
                    }
                }
            }
        }

        private void btnLookUp3_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField3.Text))
            {
                object val = itemHash[cboField3.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue3.Text = setDataTime(txtValue3.Text);
                }
                else
                {
                    if (cboField3.Text.Contains("Code") || cboField3.Text.Contains("Username") ||
                        cboField3.Text.Contains("Id") || cboField3.Text == "Classification" ||
                        cboField3.Text == "Sub Classification" || cboField3.Text == "Main Account" ||
                        cboField3.Text.Contains("Owner") || cboField3.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField3.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField3.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField3.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField3.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField3.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField3.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField3.Text.Contains("Id"))
                        {
                            _classname = cboField3.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField3.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue3.Text = loLookupValue.lCode;
                            txtValue3.Focus();
                        }
                    }
                    else
                    {
                        txtValue3.Clear();
                        txtValue3.Focus();
                    }
                }
            }
        }

        private void btnLookUp4_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField4.Text))
            {
                object val = itemHash[cboField4.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue4.Text = setDataTime(txtValue4.Text);
                }
                else
                {
                    if (cboField4.Text.Contains("Code") || cboField4.Text.Contains("Username") ||
                        cboField4.Text.Contains("Id") || cboField4.Text == "Classification" ||
                        cboField4.Text == "Sub Classification" || cboField4.Text == "Main Account" ||
                        cboField4.Text.Contains("Owner") || cboField4.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField4.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField4.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField4.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField4.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField4.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField4.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField4.Text.Contains("Id"))
                        {
                            _classname = cboField4.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField4.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue4.Text = loLookupValue.lCode;
                            txtValue4.Focus();
                        }
                    }
                    else
                    {
                        txtValue4.Clear();
                        txtValue4.Focus();
                    }
                }
            }
        }

        private void btnLookUp5_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField5.Text))
            {
                object val = itemHash[cboField5.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue5.Text = setDataTime(txtValue5.Text);
                }
                else
                {
                    if (cboField5.Text.Contains("Code") || cboField5.Text.Contains("Username") ||
                        cboField5.Text.Contains("Id") || cboField5.Text == "Classification" ||
                        cboField5.Text == "Sub Classification" || cboField5.Text == "Main Account" ||
                        cboField5.Text.Contains("Owner") || cboField5.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField5.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField5.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField5.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField5.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField5.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField5.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField5.Text.Contains("Id"))
                        {
                            _classname = cboField5.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField5.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue5.Text = loLookupValue.lCode;
                            txtValue5.Focus();
                        }
                    }
                    else
                    {
                        txtValue5.Clear();
                        txtValue5.Focus();
                    }
                }
            }
        }

        private void btnLookUp6_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField6.Text))
            {
                object val = itemHash[cboField6.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue6.Text = setDataTime(txtValue6.Text);
                }
                else
                {
                    if (cboField6.Text.Contains("Code") || cboField6.Text.Contains("Username") ||
                        cboField6.Text.Contains("Id") || cboField6.Text == "Classification" ||
                        cboField6.Text == "Sub Classification" || cboField6.Text == "Main Account" ||
                        cboField6.Text.Contains("Owner") || cboField6.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField6.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField6.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField6.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField6.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField6.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField6.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField6.Text.Contains("Id"))
                        {
                            _classname = cboField6.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField6.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue6.Text = loLookupValue.lCode;
                            txtValue6.Focus();
                        }
                    }
                    else
                    {
                        txtValue6.Clear();
                        txtValue6.Focus();
                    }
                }
            }
        }

        private void btnLookUp7_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField7.Text))
            {
                object val = itemHash[cboField7.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue7.Text = setDataTime(txtValue7.Text);
                }
                else
                {
                    if (cboField7.Text.Contains("Code") || cboField7.Text.Contains("Username") ||
                        cboField7.Text.Contains("Id") || cboField7.Text == "Classification" ||
                        cboField7.Text == "Sub Classification" || cboField7.Text == "Main Account" ||
                        cboField7.Text.Contains("Owner") || cboField7.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField7.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField7.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField7.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField7.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField7.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField7.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField7.Text.Contains("Id"))
                        {
                            _classname = cboField7.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField7.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue7.Text = loLookupValue.lCode;
                            txtValue7.Focus();
                        }
                    }
                    else
                    {
                        txtValue7.Clear();
                        txtValue7.Focus();
                    }
                }
            }
        }

        private void btnLookUp8_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField8.Text))
            {
                object val = itemHash[cboField8.Text];
                if (val.ToString().Contains("System.DateTime"))
                {
                    txtValue8.Text = setDataTime(txtValue8.Text);
                }
                else
                {
                    if (cboField8.Text.Contains("Code") || cboField8.Text.Contains("Username") ||
                        cboField8.Text.Contains("Id") || cboField8.Text == "Classification" ||
                        cboField8.Text == "Sub Classification" || cboField8.Text == "Main Account" ||
                        cboField8.Text.Contains("Owner") || cboField8.Text.Contains("Company"))
                    {
                        string _classname = "";
                        if (cboField8.Text.Contains("Username"))
                        {
                            _classname = "User";
                        }
                        else if (cboField8.Text == "Classification")
                        {
                            _classname = "Classification";
                        }
                        else if (cboField8.Text == "Sub Classification")
                        {
                            _classname = "SubClassification";
                        }
                        else if (cboField8.Text == "Main Account")
                        {
                            _classname = "MainAccount";
                        }
                        else if (cboField8.Text.Contains("Owner"))
                        {
                            _classname = "Owner";
                        }
                        else if (cboField8.Text == "Company")
                        {
                            _classname = "Company";
                        }
                        else if (cboField8.Text.Contains("Id"))
                        {
                            _classname = cboField8.Text.Replace("Id", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        else
                        {
                            _classname = cboField8.Text.Replace("Code", "");
                            _classname = _classname.Replace(" ", "");
                        }
                        GlobalClassHandler ch = new GlobalClassHandler();
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            txtValue8.Text = loLookupValue.lCode;
                            txtValue8.Focus();
                        }
                    }
                    else
                    {
                        txtValue8.Clear();
                        txtValue8.Focus();
                    }
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lsbCurrentFields.SelectedItems.Count; i++)
                {
                    if (lsbDisplayFields.FindString(lsbCurrentFields.SelectedItems[i].ToString()) == -1)
                    {
                        lsbDisplayFields.Items.Add(lsbCurrentFields.SelectedItems[i].ToString());
                    }
                }
            }
            catch { }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (lsbDisplayFields.Items.Count != 0)
            {
                lQueryShow = CreateSelectQuery();
                lQueryReport = CreateSelectQueryAndParameters();
                lFromShow = true;
                try
                {
                    lTitle = cboTemplateName.SelectedItem.ToString();
                    lSubTitle = lType.Name;
                }
                catch
                {
                    lTitle = lType.Name;
                    lSubTitle = lType.Name;
                }
                this.Close();
            }
            else
            {
                MessageBoxUI mb = new MessageBoxUI("You must atleast 1 display fields!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
                return;
            }
        }

        private void chkAND1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND1.Checked)
            {
                chkOR1.Checked = false;
            }
        }

        private void chkAND2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND2.Checked)
            {
                chkOR2.Checked = false;
            }
        }

        private void chkAND3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND3.Checked)
            {
                chkOR3.Checked = false;
            }
        }

        private void chkAND4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND4.Checked)
            {
                chkOR4.Checked = false;
            }
        }

        private void chkAND5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND5.Checked)
            {
                chkOR5.Checked = false;
            }
        }

        private void chkAND6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND6.Checked)
            {
                chkOR6.Checked = false;
            }
        }

        private void chkAND7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAND7.Checked)
            {
                chkOR7.Checked = false;
            }
        }

        private void chkOR1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR1.Checked)
            {
                chkAND1.Checked = false;
            }
        }

        private void chkOR2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR2.Checked)
            {
                chkAND2.Checked = false;
            }
        }

        private void chkOR3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR3.Checked)
            {
                chkAND3.Checked = false;
            }
        }

        private void chkOR4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR4.Checked)
            {
                chkAND4.Checked = false;
            }
        }

        private void chkOR5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR5.Checked)
            {
                chkAND5.Checked = false;
            }
        }

        private void chkOR6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR6.Checked)
            {
                chkAND6.Checked = false;
            }
        }

        private void chkOR7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOR7.Checked)
            {
                chkAND7.Checked = false;
            }
        }

        private void cboField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND1.Checked = true;
        }

        private void cboField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND2.Checked = true;
        }

        private void cboField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND3.Checked = true;
        }

        private void cboField4_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND4.Checked = true;
        }

        private void cboField5_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND5.Checked = true;
        }

        private void cboField6_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND6.Checked = true;
        }

        private void cboField7_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND7.Checked = true;
        }

        #endregion "END OF EVENTS"

        private void btnSum_Click(object sender, EventArgs e)
        {
            if (lsbDisplayFields.SelectedItem.ToString().Contains(" - (Sum)"))
            {
                lsbDisplayFields.Items[lsbDisplayFields.SelectedIndex] = lsbDisplayFields.SelectedItem.ToString().Replace(" - (Sum)", "");
            }
            else
            {
                lsbDisplayFields.Items[lsbDisplayFields.SelectedIndex] = lsbDisplayFields.SelectedItem.ToString() + " - (Sum)";
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int i = this.lsbDisplayFields.SelectedIndex;
            object o = this.lsbDisplayFields.SelectedItem;

            if (i > 0)
            {
                this.lsbDisplayFields.Items.RemoveAt(i);
                this.lsbDisplayFields.Items.Insert(i - 1, o);
                this.lsbDisplayFields.SelectedIndex = i - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int i = this.lsbDisplayFields.SelectedIndex;
            object o = this.lsbDisplayFields.SelectedItem;

            if (i < this.lsbDisplayFields.Items.Count - 1)
            {
                this.lsbDisplayFields.Items.RemoveAt(i);
                this.lsbDisplayFields.Items.Insert(i + 1, o);
                this.lsbDisplayFields.SelectedIndex = i + 1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                lsbDisplayFields.Items.RemoveAt(lsbDisplayFields.SelectedIndex);
                lsbDisplayFields.SelectedIndex = 0;
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboTemplateName.SelectedItem.ToString() == "" || cboTemplateName.SelectedItem == null)
            {
                MessageBoxUI mb = new MessageBoxUI("You must have a template name!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                return;
            }
            try
            {
                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    loCommon.removeSearchFields(lMenuName, cboTemplateName.SelectedItem.ToString());
                }
                //for displayfields
                for (int i = 0; i < lsbDisplayFields.Items.Count; i++)
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.DisplayFields = lsbDisplayFields.Items[i].ToString();
                    loCommon.SequenceNo = i;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveDisplayField(GlobalVariables.Operation.Add);
                }

                //for filters
                if (cboField1.Text != "" && txtValue1.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField1.Text;
                    loCommon.Operator = cboOperator1.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator1.Text].ToString();
                    loCommon.Values = txtValue1.Text;
                    if (chkAND1.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 1;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }
                if (cboField2.Text != "" && txtValue2.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField2.Text;
                    loCommon.Operator = cboOperator2.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator2.Text].ToString();
                    loCommon.Values = txtValue2.Text;
                    if (chkAND2.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 2;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }
                if (cboField3.Text != "" && txtValue3.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField3.Text;
                    loCommon.Operator = cboOperator3.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator3.Text].ToString();
                    loCommon.Values = txtValue3.Text;
                    if (chkAND3.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 3;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }
                if (cboField4.Text != "" && txtValue4.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField4.Text;
                    loCommon.Operator = cboOperator4.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator4.Text].ToString();
                    loCommon.Values = txtValue4.Text;
                    if (chkAND4.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 4;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }
                if (cboField5.Text != "" && txtValue5.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField5.Text;
                    loCommon.Operator = cboOperator5.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator5.Text].ToString();
                    loCommon.Values = txtValue5.Text;
                    if (chkAND5.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 5;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }

                if (cboField6.Text != "" && txtValue6.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField6.Text;
                    loCommon.Operator = cboOperator6.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator6.Text].ToString();
                    loCommon.Values = txtValue6.Text;
                    if (chkAND6.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 6;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }

                if (cboField7.Text != "" && txtValue7.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField7.Text;
                    loCommon.Operator = cboOperator7.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator7.Text].ToString();
                    loCommon.Values = txtValue7.Text;
                    if (chkAND7.Checked)
                    {
                        loCommon.CheckAnd = "Y";
                        loCommon.CheckOr = "N";
                    }
                    else
                    {
                        loCommon.CheckAnd = "N";
                        loCommon.CheckOr = "Y";
                    }
                    loCommon.SequenceNo = 7;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }

                if (cboField8.Text != "" && txtValue8.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.Fields = cboField8.Text;
                    loCommon.Operator = cboOperator8.Text;
                    //loCommon.OperatorSign = operatorHash[cboOperator8.Text].ToString();
                    loCommon.Values = txtValue8.Text;
                    loCommon.SequenceNo = 8;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveFilters(GlobalVariables.Operation.Add);
                }
                /*
                //groupings
                if (cboGroupField1.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.FieldName = cboGroupField1.SelectedItem.ToString();
                    if (chkGroupBy1.Checked)
                    {
                        loCommon.GroupBy = "Y";
                    }
                    else
                    {
                        loCommon.GroupBy = "N";
                    }
                    if (rdbAscesding1.Checked)
                    {
                        loCommon.SortBy = "Ascending";
                    }
                    else if (rdbDescending1.Checked)
                    {
                        loCommon.SortBy = "Descending";
                    }
                    else
                    {
                        loCommon.SortBy = "";
                    }

                    loCommon.SequenceNo = 1;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveGroupings(lOperation);
                }
                if (cboGroupField2.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.FieldName = cboGroupField2.SelectedItem.ToString();
                    if (chkGroupBy2.Checked)
                    {
                        loCommon.GroupBy = "Y";
                    }
                    else
                    {
                        loCommon.GroupBy = "N";
                    }
                    if (rdbAscesding2.Checked)
                    {
                        loCommon.SortBy = "Ascending";
                    }
                    else if (rdbDescending2.Checked)
                    {
                        loCommon.SortBy = "Descending";
                    }
                    else
                    {
                        loCommon.SortBy = "";
                    }
                    loCommon.SequenceNo = 2;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveGroupings(lOperation);
                }
                if (cboGroupField3.Text != "")
                {
                    loCommon.MenuName = lMenuName;
                    loCommon.TemplateName = cboTemplateName.Text;
                    loCommon.FieldName = cboGroupField3.SelectedItem.ToString();
                    if (chkGroupBy3.Checked)
                    {
                        loCommon.GroupBy = "Y";
                    }
                    else
                    {
                        loCommon.GroupBy = "N";
                    }
                    if (rdbAscesding3.Checked)
                    {
                        loCommon.SortBy = "Ascending";
                    }
                    else if (rdbDescending3.Checked)
                    {
                        loCommon.SortBy = "Descending";
                    }
                    else
                    {
                        loCommon.SortBy = "";
                    }
                    loCommon.SequenceNo = 3;
                    if (chkPrivate.Checked)
                    {
                        loCommon.Private = "Y";
                    }
                    else
                    {
                        loCommon.Private = "N";
                    }
                    loCommon.saveGroupings(lOperation);
                }
                */
                MessageBoxUI mb = new MessageBoxUI("Template has been saved successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBoxInputUI mb = new MessageBoxInputUI();
            mb.lTitleString = "Template Name:";
            mb.lInputString = "";
            mb.ShowDialog();
            if (mb.lFromOk)
            {
                lFromAdd = true;
                cboTemplateName.Items.Add(mb.lInputString);
                cboTemplateName.SelectedItem = mb.lInputString;
                lOperation = GlobalVariables.Operation.Add;
                lFromAdd = false;
            }
        }

        private void cboTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //displayDetails();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lsbDisplayFields.Items.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboTemplateName.SelectedItem.ToString() == "" || cboTemplateName.SelectedItem == null)
            {
                MessageBoxUI mb = new MessageBoxUI("You must have a template name!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                return;
            }

            try
            {
                loCommon.removeTemplateName(lMenuName, cboTemplateName.SelectedItem.ToString());
                lsbDisplayFields.Items.Clear();
                loadTemplateName();
                MessageBoxUI mb = new MessageBoxUI("Template name has been deleted successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                return;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            MessageBoxInputUI mb = new MessageBoxInputUI();
            mb.lTitleString = "Current Template Name:";
            mb.lInputString = cboTemplateName.SelectedItem.ToString();
            mb.ShowDialog();
            if (mb.lFromOk)
            {
                try
                {
                    loCommon.renameTemplateName(lMenuName, cboTemplateName.SelectedItem.ToString(), mb.lInputString);
                    lsbDisplayFields.Items.Clear();
                    loadTemplateName();
                    MessageBoxUI mb1 = new MessageBoxUI("Template name has been renamed successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb1.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb2 = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                    mb2.ShowDialog();
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lsbDisplayFields.Items.Clear();
            chkPrivate.Checked = false;
            cboTemplateName.SelectedItem = null;
            cboOperator1.SelectedIndex = 0;
            cboOperator2.SelectedIndex = 0;
            cboOperator3.SelectedIndex = 0;
            cboOperator4.SelectedIndex = 0;
            cboOperator5.SelectedIndex = 0;
            cboOperator6.SelectedIndex = 0;
            cboOperator7.SelectedIndex = 0;
            cboOperator8.SelectedIndex = 0;
            txtValue1.Clear();
            txtValue2.Clear();
            txtValue3.Clear();
            txtValue4.Clear();
            txtValue5.Clear();
            txtValue6.Clear();
            txtValue7.Clear();
            txtValue8.Clear();
            cboField1.SelectedItem = null;
            cboField2.SelectedItem = null;
            cboField3.SelectedItem = null;
            cboField4.SelectedItem = null;
            cboField5.SelectedItem = null;
            cboField6.SelectedItem = null;
            cboField7.SelectedItem = null;
            cboField8.SelectedItem = null;
            cboGroupField1.SelectedItem = null;
            cboGroupField2.SelectedItem = null;
            cboGroupField3.SelectedItem = null;
            chkGroupBy1.Checked = false;
            chkGroupBy2.Checked = false;
            chkGroupBy3.Checked = false;
            rdbAscesding1.Checked = false;
            rdbAscesding2.Checked = false;
            rdbAscesding3.Checked = false;
            rdbDescending1.Checked = false;
            rdbDescending2.Checked = false;
            rdbDescending3.Checked = false;
        }

        private void chkGroupBy1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGroupBy1.Checked)
            {
                cboGroupField1.SelectedIndex = 0;
            }
            else
            {
                cboGroupField1.SelectedItem = null;
            }
        }

        private void chkGroupBy2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGroupBy2.Checked)
            {
                cboGroupField2.SelectedIndex = 0;
            }
            else
            {
                cboGroupField2.SelectedItem = null;
            }
        }

        private void chkGroupBy3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGroupBy3.Checked)
            {
                cboGroupField3.SelectedIndex = 0;
            }
            else
            {
                cboGroupField3.SelectedItem = null;
            }
        }

        private void rdbPortrait_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPortrait.Checked)
            {
                lPaperLayout = "Portrait";
            }
        }

        private void rdbLandscape_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLandscape.Checked)
            {
                lPaperLayout = "Landscape";
            }
        }

        private void rdbLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLetter.Checked)
            {
                lPaperSize = "Letter";
            }
        }

        private void rdbLegal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLegal.Checked)
            {
                lPaperSize = "Legal";
            }
        }
    }
}
