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
    public partial class SearchesUI : Form
    {
        #region "VARIABLES"
        public string lQuery;
        public string lAlias;
        public bool lFromShow;
        public string lMenuName;
        public string lTitle;
        public ParameterFields lParamFields;
        FieldInfo[] lFieldInfo;
        Type lType;
        Hashtable itemHash = new Hashtable();
        //Hashtable operatorHash = new Hashtable();
        List<string> lItems = new List<string>();
        //List<string> lOperators = new List<string>();
        LookUpValueUI loLookupValue;
        Common loCommon;
        GlobalVariables.Operation lOperation = GlobalVariables.Operation.Edit;
        bool lFromAdd = false;
       // KeyboardUI loKeyboardUI;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SearchesUI(FieldInfo[] pFieldInfo, Type pType, string pMenuName)
        {
            InitializeComponent();
            lFieldInfo = pFieldInfo;
            lType = pType;
            loadFields();
            //loadOperatorsToHashTable();
            loadComboBoxes();
            loLookupValue = new LookUpValueUI();
            loCommon = new Common();
            lMenuName = pMenuName;
            loadTemplateName();
        }

        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        private void loadFields()
        {
            string _name;
            string _fieldType;
            if (lFieldInfo[0].Name.ToString() != "<Username>k__BackingField")
            {
                if (lType.Name == "PurchaseRequest" || lType.Name == "PurchaseOrder" ||
                    lType.Name == "PriceQuotation" || lType.Name == "SalesOrder" ||
                    lType.Name == "Inventory" || lType.Name == "JournalEntry")
                {
                    for (int i = 0; i < lFieldInfo.Length - 1; i++)
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
                }
                else
                {
                    for (int i = 1; i < lFieldInfo.Length - 1; i++)
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
                }
            }
            else
            {
                for (int i = 0; i < lFieldInfo.Length - 1; i++)
                {
                    _name = lFieldInfo[i].Name.ToString();
                    _fieldType = lFieldInfo[i].FieldType.ToString();
                    if (_name != "<Password>k__BackingField")
                    {
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
                }
            }
            //lsbCurrentFields.DataSource = lItems;
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
        private string setDataTime(string pCBOValue,string pDateTime)
        {
            string _returnString = "";
            DateTimePickerUI _datetime = new DateTimePickerUI(pDateTime);
            _datetime.ShowDialog();
            if (_datetime.lSelectDate)
            {
                if (pCBOValue.Contains("Date"))
                {
                    _returnString = String.Format("{0:yyyy-MM-dd}", _datetime.lDateTime);
                }
                else if (pCBOValue.Contains("Time"))
                {
                    _returnString = String.Format("{0:HH:mm:ss}", _datetime.lDateTime);
                }
                else if (pCBOValue == " Month")
                {
                    _returnString = String.Format("{0:MMMM}", _datetime.lDateTime);
                }
                else if (pCBOValue == " Year")
                {
                    _returnString = String.Format("{0:yyyy}", _datetime.lDateTime);
                }
                else
                {
                    _returnString = String.Format("{0:u}", _datetime.lDateTime).Replace("Z", "");
                }
            }
            return _returnString;
        }

        private string CreateSelectQuery()
        {
            string query = "";
            string condition = "";

            #region"for where statements"

            if (cboField1.Text != "" && cboValue1.Text != "")
            {
                getCondition(ref condition, cboField1.Text, cboOperator1.Text, cboValue1.Text);
            }
            if (cboField2.Text != "" && cboValue2.Text != "")
            {
                checkCondition(ref condition, chkAND1);
                getCondition(ref condition, cboField2.Text, cboOperator2.Text, cboValue2.Text);
            }
            if (cboField3.Text != "" && cboValue3.Text != "")
            {
                checkCondition(ref condition, chkAND2);
                getCondition(ref condition, cboField3.Text, cboOperator3.Text, cboValue3.Text);
            }
            if (cboField4.Text != "" && cboValue4.Text != "")
            {
                checkCondition(ref condition, chkAND3);
                getCondition(ref condition, cboField4.Text, cboOperator4.Text, cboValue4.Text);
            }
            if (cboField5.Text != "" && cboValue5.Text != "")
            {
                checkCondition(ref condition, chkAND6);
                getCondition(ref condition, cboField5.Text, cboOperator5.Text, cboValue5.Text);
            }

            if (cboField6.Text != "" && cboValue6.Text != "")
            {
                checkCondition(ref condition, chkAND5);
                getCondition(ref condition, cboField6.Text, cboOperator6.Text, cboValue6.Text);
            }

            if (cboField7.Text != "" && cboValue7.Text != "")
            {
                checkCondition(ref condition, chkAND6);
                getCondition(ref condition, cboField7.Text, cboOperator7.Text, cboValue7.Text);
            }

            if (cboField8.Text != "" && cboValue8.Text != "")
            {
                checkCondition(ref condition, chkAND7);
                getCondition(ref condition, cboField8.Text, cboOperator8.Text, cboValue8.Text);
            }
            #endregion

            condition = condition.Replace('*', ' ');
            string[] tables = lType.ToString().Split('.');
            int counttables = tables.Length;
            string tablename = tables[counttables - 1];
            if (condition != "")
            {
                query += " WHERE " + condition;
            }
            else
            {
                return "";
            }
            return query;
        }
        private void getCondition(ref string pCondition, string pField, string pOperator, string pValue)
        {
            if (pField.Contains("Date"))
            {
                switch (pOperator)
                {
                    case "Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " = '", pValue, "'*"));
                        break;
                    case "Not Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " != '", pValue, "'*"));
                        break;
                    case "Greater Than":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " > '", pValue, "'*"));
                        break;
                    case "Lesser Than":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " < '", pValue, "'*"));
                        break;
                    case "Greater Than or Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " >= '", pValue, "'*"));
                        break;
                    case "Lesser Than or Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " <= '", pValue, "'*"));
                        break;
                    case "Starts With":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '", pValue, "%'*"));

                        break;
                    case "Ends With":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '%", pValue, "'*"));
                        break;
                    case "Contains":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '%", pValue, "%'*"));
                        break;
                }
            }
            else
            {
                switch (pOperator)
                {
                    case "Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " = '", pValue, "'*"));
                        break;
                    case "Not Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " != '", pValue, "'*"));
                        break;
                    case "Greater Than":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " > '", pValue, "'*"));
                        break;
                    case "Lesser Than":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " < '", pValue, "'*"));
                        break;
                    case "Greater Than or Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " >= '", pValue, "'*"));
                        break;
                    case "Lesser Than or Equals":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " <= '", pValue, "'*"));
                        break;
                    case "Starts With":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '", pValue, "%'*"));
                        break;
                    case "Ends With":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '%", pValue, "'*"));
                        break;
                    case "Contains":
                        pCondition = pCondition.Insert(pCondition.Length, string.Concat(lAlias, pField.Replace(" ", ""), " LIKE '%", pValue, "%'*"));
                        break;
                }
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
            try
            {
                cboTemplateName.DataSource = null;
                chkPrivate.Checked = false;
                cboTemplateName.DataSource = loCommon.getTemplateNames(lMenuName);
                cboTemplateName.ValueMember = "Id";
                cboTemplateName.DisplayMember = "TemplateName";
                cboTemplateName.SelectedIndex = -1;
            }
            catch { }
        }

        #endregion "END OF METHODS"

        #region "EVENTS"
        private void SearchesUI_Load(object sender, EventArgs e)
        {
            lFromShow = false;
            lQuery = "";
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
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue1.Text = setDataTime(cboField1.Text, cboValue1.Text);
                    _hasValue = true;
                }
                else if (cboField1.Text.Contains("Username") || cboField1.Text == " Prepared By" ||
                         cboField1.Text == " Certified Correct" || 
                         cboField1.Text == " Approved By" || cboField1.Text == " Cancelled By" ||
                         cboField1.Text == " Finalized By" || cboField1.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField1.Text == " Requested By")
                {
                    _classname = "Employee";
                }
                else if (cboField1.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField1.Text == " From Location Id" || cboField1.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField1.Text.Contains("Id"))
                {
                    _classname = cboField1.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue1.Text = loLookupValue.lCode;
                            cboValue1.Focus();
                        }
                    }
                    catch
                    {
                        cboValue1.Text = "";
                        cboValue1.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue1.Text = "";
                        cboValue1.Focus();
                    }
                }
            }
        }

        private void btnLookUp2_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField2.Text))
            {
                object val = itemHash[cboField2.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue2.Text = setDataTime(cboField2.Text, cboValue2.Text);
                    _hasValue = true;
                }
                else if (cboField2.Text.Contains("Username") || cboField2.Text == " Prepared By" ||
                         cboField2.Text == " Certified Correct" || cboField2.Text == " Requested By" ||
                         cboField2.Text == " Approved By" || cboField2.Text == " Cancelled By" ||
                         cboField2.Text == " Finalized By" || cboField2.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField2.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField2.Text == " From Location Id" || cboField2.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField2.Text.Contains("Id"))
                {
                    _classname = cboField2.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue2.Text = loLookupValue.lCode;
                            cboValue2.Focus();
                        }
                    }
                    catch
                    {
                        cboValue2.Text = "";
                        cboValue2.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue2.Text = "";
                        cboValue2.Focus();
                    }
                }
            }
        }

        private void btnLookUp3_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField3.Text))
            {
                object val = itemHash[cboField3.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue3.Text = setDataTime(cboField3.Text, cboValue3.Text);
                    _hasValue = true;
                }
                else if (cboField3.Text.Contains("Username") || cboField3.Text == " Prepared By" ||
                         cboField3.Text == " Certified Correct" || cboField3.Text == " Requested By" ||
                         cboField3.Text == " Approved By" || cboField3.Text == " Cancelled By" ||
                         cboField3.Text == " Finalized By" || cboField3.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField3.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField3.Text == " From Location Id" || cboField3.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField3.Text.Contains("Id"))
                {
                    _classname = cboField3.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue3.Text = loLookupValue.lCode;
                            cboValue3.Focus();
                        }
                    }
                    catch
                    {
                        cboValue3.Text = "";
                        cboValue3.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue3.Text = "";
                        cboValue3.Focus();
                    }
                }
            }
        }

        private void btnLookUp5_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField5.Text))
            {
                object val = itemHash[cboField5.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue5.Text = setDataTime(cboField5.Text, cboValue5.Text);
                    _hasValue = true;
                }
                else if (cboField5.Text.Contains("Username") || cboField5.Text == " Prepared By" ||
                         cboField5.Text == " Certified Correct" || cboField5.Text == " Requested By" ||
                         cboField5.Text == " Approved By" || cboField5.Text == " Cancelled By" ||
                         cboField5.Text == " Finalized By" || cboField5.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField5.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField5.Text == " From Location Id" || cboField5.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField5.Text.Contains("Id"))
                {
                    _classname = cboField5.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue5.Text = loLookupValue.lCode;
                            cboValue5.Focus();
                        }
                    }
                    catch
                    {
                        cboValue5.Text = "";
                        cboValue5.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue5.Text = "";
                        cboValue5.Focus();
                    }
                }
            }
        }

        private void btnLookUp6_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField6.Text))
            {
                object val = itemHash[cboField6.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue6.Text = setDataTime(cboField6.Text, cboValue6.Text);
                    _hasValue = true;
                }
                else if (cboField6.Text.Contains("Username") || cboField6.Text == " Prepared By" ||
                         cboField6.Text == " Certified Correct" || cboField6.Text == " Requested By" ||
                         cboField6.Text == " Approved By" || cboField6.Text == " Cancelled By" ||
                         cboField6.Text == " Finalized By" || cboField6.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField6.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField6.Text == " From Location Id" || cboField6.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField6.Text.Contains("Id"))
                {
                    _classname = cboField6.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue6.Text = loLookupValue.lCode;
                            cboValue6.Focus();
                        }
                    }
                    catch
                    {
                        cboValue6.Text = "";
                        cboValue6.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue6.Text = "";
                        cboValue6.Focus();
                    }
                }
            }
        }

        private void btnLookUp7_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField7.Text))
            {
                object val = itemHash[cboField7.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue7.Text = setDataTime(cboField7.Text, cboValue7.Text);
                    _hasValue = true;
                }
                else if (cboField7.Text.Contains("Username") || cboField7.Text == " Prepared By" ||
                         cboField7.Text == " Certified Correct" || cboField7.Text == " Requested By" ||
                         cboField7.Text == " Approved By" || cboField7.Text == " Cancelled By" ||
                         cboField7.Text == " Finalized By" || cboField7.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField7.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField7.Text == " From Location Id" || cboField7.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField7.Text.Contains("Id"))
                {
                    _classname = cboField7.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue7.Text = loLookupValue.lCode;
                            cboValue7.Focus();
                        }
                    }
                    catch
                    {
                        cboValue7.Text = "";
                        cboValue7.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue7.Text = "";
                        cboValue7.Focus();
                    }
                }
            }
        }

        private void btnLookUp8_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField8.Text))
            {
                object val = itemHash[cboField8.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue8.Text = setDataTime(cboField8.Text, cboValue8.Text);
                    _hasValue = true;
                }
                else if (cboField8.Text.Contains("Username") || cboField8.Text == " Prepared By" ||
                         cboField8.Text == " Certified Correct" || cboField8.Text == " Requested By" ||
                         cboField8.Text == " Approved By" || cboField8.Text == " Cancelled By" ||
                         cboField8.Text == " Finalized By" || cboField8.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField8.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField8.Text == " From Location Id" || cboField8.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField8.Text.Contains("Id"))
                {
                    _classname = cboField8.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue8.Text = loLookupValue.lCode;
                            cboValue8.Focus();
                        }
                    }
                    catch
                    {
                        cboValue8.Text = "";
                        cboValue8.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue8.Text = "";
                        cboValue8.Focus();
                    }
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string _Query = "";
            _Query = CreateSelectQuery();
            lFromShow = true;
            lQuery = _Query;
            if (lQuery == "")
            {
                MessageBoxUI _mb = new MessageBoxUI("There must be atleast one(1) field value!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                return;
            }
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _TemplateId = "";
            try
            {
                _TemplateId = cboTemplateName.SelectedValue.ToString();

                loCommon.updateSearchTemplate(_TemplateId, cboTemplateName.Text, lMenuName, (chkPrivate.Checked ? "Y" : "N"));
                insertFilters(_TemplateId);
                MessageBoxUI mb = new MessageBoxUI("Template has been saved successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            catch 
            {
                MessageBoxUI mb = new MessageBoxUI("Unable to save! Incorrect template name.", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBoxInputUI mbName = new MessageBoxInputUI();
            mbName.lTitleString = "Template Name:";
            mbName.lInputString = "";
            mbName.ShowDialog();
            if (mbName.lFromOk)
            {
                try
                {
                    string _id = "";
                    _id = loCommon.insertSearchTemplate(mbName.lInputString, lMenuName , (chkPrivate.Checked ? "Y" :"N")).Replace("\"", "");;
                    insertFilters(_id);
                    loadTemplateName();
                    cboTemplateName.SelectedValue = _id;
                    
                    MessageBoxUI mb = new MessageBoxUI("Template has been added successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                    mb.ShowDialog();
                    return;
                }
            }
        }

        private void cboTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loCommon.getTemplateName(cboTemplateName.SelectedValue.ToString()).Rows)
                {
                    chkPrivate.Checked = (_dr["Private"].ToString() == "Y" ? true : false);
                }
                displayFilters();
            }
            catch { }
        }

        private void displayFilters()
        {
            string _TemplateId = "";
            try
            {
                _TemplateId = cboTemplateName.SelectedValue.ToString();
                clear();
                foreach (DataRow _dr in loCommon.getSearchFilters(_TemplateId).Rows)
                {
                    if(_dr["Sequence"].ToString() == "1")
                    {
                        cboField1.Text = _dr["Field"].ToString();
                        cboOperator1.Text = _dr["Operator"].ToString();
                        cboValue1.Text = _dr["Value"].ToString();
                        chkAND1.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR1.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "2")
                    {
                        cboField2.Text = _dr["Field"].ToString();
                        cboOperator2.Text = _dr["Operator"].ToString();
                        cboValue2.Text = _dr["Value"].ToString();
                        chkAND2.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR2.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "3")
                    {
                        cboField3.Text = _dr["Field"].ToString();
                        cboOperator3.Text = _dr["Operator"].ToString();
                        cboValue3.Text = _dr["Value"].ToString();
                        chkAND3.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR3.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "4")
                    {
                        cboField4.Text = _dr["Field"].ToString();
                        cboOperator4.Text = _dr["Operator"].ToString();
                        cboValue4.Text = _dr["Value"].ToString();
                        chkAND4.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR4.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "5")
                    {
                        cboField5.Text = _dr["Field"].ToString();
                        cboOperator5.Text = _dr["Operator"].ToString();
                        cboValue5.Text = _dr["Value"].ToString();
                        chkAND5.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR5.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "6")
                    {
                        cboField6.Text = _dr["Field"].ToString();
                        cboOperator6.Text = _dr["Operator"].ToString();
                        cboValue6.Text = _dr["Value"].ToString();
                        chkAND6.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR6.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "7")
                    {
                        cboField7.Text = _dr["Field"].ToString();
                        cboOperator7.Text = _dr["Operator"].ToString();
                        cboValue7.Text = _dr["Value"].ToString();
                        chkAND7.Checked = _dr["CheckAnd"].ToString() == "Y" ? true : false;
                        chkOR7.Checked = _dr["CheckOr"].ToString() == "Y" ? true : false;
                    }
                    else if (_dr["Sequence"].ToString() == "8")
                    {
                        cboField8.Text = _dr["Field"].ToString();
                        cboOperator8.Text = _dr["Operator"].ToString();
                        cboValue8.Text = _dr["Value"].ToString();
                    }
                }
            }
            catch { }
        }

        private void insertFilters(string pId)
        {
            try
            {
                loCommon.removeSearchFilter(pId);

                if (cboField1.Text != "" && cboValue1.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField1.Text, cboOperator1.Text, cboValue1.Text, (chkAND1.Checked ? "Y" : "N"), (chkOR1.Checked ? "Y" : "N"), 1);
                }
                if (cboField2.Text != "" && cboValue2.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField2.Text, cboOperator2.Text, cboValue2.Text, (chkAND2.Checked ? "Y" : "N"), (chkOR2.Checked ? "Y" : "N"), 2);
                }
                if (cboField3.Text != "" && cboValue3.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField3.Text, cboOperator3.Text, cboValue3.Text, (chkAND3.Checked ? "Y" : "N"), (chkOR3.Checked ? "Y" : "N"), 3);
                }
                if (cboField4.Text != "" && cboValue4.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField4.Text, cboOperator4.Text, cboValue4.Text, (chkAND4.Checked ? "Y" : "N"), (chkOR4.Checked ? "Y" : "N"), 4);
                }
                if (cboField5.Text != "" && cboValue5.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField5.Text, cboOperator5.Text, cboValue5.Text, (chkAND5.Checked ? "Y" : "N"), (chkOR5.Checked ? "Y" : "N"), 5);
                }
                if (cboField6.Text != "" && cboValue6.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField6.Text, cboOperator6.Text, cboValue6.Text, (chkAND6.Checked ? "Y" : "N"), (chkOR6.Checked ? "Y" : "N"), 6);
                }
                if (cboField7.Text != "" && cboValue7.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField7.Text, cboOperator7.Text, cboValue7.Text, (chkAND7.Checked ? "Y" : "N"), (chkOR7.Checked ? "Y" : "N"), 7);
                }
                if (cboField8.Text != "" && cboValue8.Text != "")
                {
                    loCommon.insertSearchFilter(pId, cboField8.Text, cboOperator8.Text, cboValue8.Text, "N", "N", 8);
                }
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.Close);
                mb.ShowDialog();
                return;
            }
        }

        private void clear()
        {
            cboField1.SelectedItem = null;
            cboField2.SelectedItem = null;
            cboField3.SelectedItem = null;
            cboField4.SelectedItem = null;
            cboField5.SelectedItem = null;
            cboField6.SelectedItem = null;
            cboField7.SelectedItem = null;
            cboField8.SelectedItem = null;
            cboOperator1.SelectedIndex = 0;
            cboOperator2.SelectedIndex = 0;
            cboOperator3.SelectedIndex = 0;
            cboOperator4.SelectedIndex = 0;
            cboOperator5.SelectedIndex = 0;
            cboOperator6.SelectedIndex = 0;
            cboOperator7.SelectedIndex = 0;
            cboOperator8.SelectedIndex = 0;
            cboValue1.Text = "";
            cboValue2.Text = "";
            cboValue3.Text = "";
            cboValue4.Text = "";
            cboValue5.Text = "";
            cboValue6.Text = "";
            cboValue7.Text = "";
            cboValue8.Text = "";
            chkAND1.Checked = false;
            chkAND2.Checked = false;
            chkAND3.Checked = false;
            chkAND4.Checked = false;
            chkAND5.Checked = false;
            chkAND6.Checked = false;
            chkAND7.Checked = false;
            chkOR1.Checked = false;
            chkOR2.Checked = false;
            chkOR3.Checked = false;
            chkOR4.Checked = false;
            chkOR5.Checked = false;
            chkOR6.Checked = false;
            chkOR7.Checked = false;
            cboValue1.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult _dr = new DialogResult();
            MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue removing this template?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
            _mb.ShowDialog();
            _dr = _mb.Operation;
            if (_dr == DialogResult.Yes)
            {
                string _TemplateId = "";
                try
                {
                    _TemplateId = cboTemplateName.SelectedValue.ToString();

                    loCommon.removeSearchTemplate(_TemplateId);
                    loCommon.removeSearchFilter(_TemplateId);
                    loadTemplateName();
                    clear();
                    MessageBoxUI mb = new MessageBoxUI("Template has been removed successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                catch
                {
                    MessageBoxUI mb = new MessageBoxUI("Unable to save! Incorrect template name.", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            MessageBoxInputUI mb = new MessageBoxInputUI();
            mb.lTitleString = "Current Template Name:";
            mb.lInputString = cboTemplateName.Text;
            mb.ShowDialog();
            if (mb.lFromOk)
            {
                string _TemplateId = "";
                try
                {
                    _TemplateId = cboTemplateName.SelectedValue.ToString();

                    loCommon.renameSearchTemplate(_TemplateId, mb.lInputString);
                    insertFilters(_TemplateId);
                    loadTemplateName();
                    clear();
                    cboTemplateName.SelectedValue = _TemplateId;
                    MessageBoxUI mb1 = new MessageBoxUI("Template has been renamed successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
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
            chkPrivate.Checked = false;
            cboTemplateName.SelectedItem = null;
            cboField1.SelectedItem = null;
            cboField2.SelectedItem = null;
            cboField3.SelectedItem = null;
            cboField4.SelectedItem = null;
            cboField5.SelectedItem = null;
            cboField6.SelectedItem = null;
            cboField7.SelectedItem = null;
            cboField8.SelectedItem = null;
            cboOperator1.SelectedIndex = 0;
            cboOperator2.SelectedIndex = 0;
            cboOperator3.SelectedIndex = 0;
            cboOperator4.SelectedIndex = 0;
            cboOperator5.SelectedIndex = 0;
            cboOperator6.SelectedIndex = 0;
            cboOperator7.SelectedIndex = 0;
            cboOperator8.SelectedIndex = 0;
            cboValue1.Text = "";
            cboValue2.Text = "";
            cboValue3.Text = "";
            cboValue4.Text = "";
            cboValue5.Text = "";
            cboValue6.Text = "";
            cboValue7.Text = "";
            cboValue8.Text = "";
            chkAND1.Checked = false;
            chkAND2.Checked = false;
            chkAND3.Checked = false;
            chkAND4.Checked = false;
            chkAND5.Checked = false;
            chkAND6.Checked = false;
            chkAND7.Checked = false;
            chkOR1.Checked = false;
            chkOR2.Checked = false;
            chkOR3.Checked = false;
            chkOR4.Checked = false;
            chkOR5.Checked = false;
            chkOR6.Checked = false;
            chkOR7.Checked = false;
        }

        private void cboField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND1.Checked = true;
            cboValue1.Items.Clear();
            cboValue1.Text = "";
            cboValue1.Focus();
            if (cboField1.Text == " Posted" || cboField1.Text == " Beg Bal" ||
                cboField1.Text == " Adjustment" || cboField1.Text == " Cancel" ||
                cboField1.Text == " Approve" || cboField1.Text == " Final" ||
                cboField1.Text == " Post" || cboField1.Text == " Default" ||
                cboField1.Text == " Cash Payment" || cboField1.Text == " Active" ||
                cboField1.Text == " Saleable" || cboField1.Text == " Non Inventory" ||
                cboField1.Text == " Contra Account" || cboField1.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y","N" };
                cboValue1.Items.AddRange(YesOrNo);
            }
            else if (cboField1.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue1.Items.AddRange(typeofaccount);
            }
            else if (cboField1.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue1.Items.AddRange(subsidiary);
            }
            else if (cboField1.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue1.Items.AddRange(journal);
            }
            else if (cboField1.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue1.Items.AddRange(form);
            }
        }

        private void cboField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND2.Checked = true;
            cboValue2.Items.Clear();
            cboValue2.Text = "";
            cboValue2.Focus();
            if (cboField2.Text == " Posted" || cboField2.Text == " Beg Bal" ||
                cboField2.Text == " Adjustment" || cboField2.Text == " Cancel" ||
                cboField2.Text == " Approve" || cboField2.Text == " Final" ||
                cboField2.Text == " Post" || cboField2.Text == " Default" ||
                cboField2.Text == " Cash Payment" || cboField2.Text == " Active" ||
                cboField2.Text == " Saleable" || cboField2.Text == " Non Inventory" ||
                cboField2.Text == " Contra Account" || cboField2.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue2.Items.AddRange(YesOrNo);
            }
            else if (cboField2.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue2.Items.AddRange(typeofaccount);
            }
            else if (cboField2.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue2.Items.AddRange(subsidiary);
            }
            else if (cboField2.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue2.Items.AddRange(journal);
            }
            else if (cboField2.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue2.Items.AddRange(form);
            }
        }

        private void cboField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND3.Checked = true;
            cboValue3.Items.Clear();
            cboValue3.Text = "";
            cboValue3.Focus();
            if (cboField3.Text == " Posted" || cboField3.Text == " Beg Bal" ||
                cboField3.Text == " Adjustment" || cboField3.Text == " Cancel" ||
                cboField3.Text == " Approve" || cboField3.Text == " Final" ||
                cboField3.Text == " Post" || cboField3.Text == " Default" ||
                cboField3.Text == " Cash Payment" || cboField3.Text == " Active" ||
                cboField3.Text == " Saleable" || cboField3.Text == " Non Inventory" ||
                cboField3.Text == " Contra Account" || cboField3.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue3.Items.AddRange(YesOrNo);
            }
            else if (cboField3.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue3.Items.AddRange(typeofaccount);
            }
            else if (cboField3.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue3.Items.AddRange(subsidiary);
            }
            else if (cboField3.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue3.Items.AddRange(journal);
            }
            else if (cboField3.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue3.Items.AddRange(form);
            }
        }

        private void cboField4_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND4.Checked = true;
            cboValue4.Items.Clear();
            cboValue4.Text = "";
            cboValue4.Focus();
            if (cboField4.Text == " Posted" || cboField4.Text == " Beg Bal" ||
                cboField4.Text == " Adjustment" || cboField4.Text == " Cancel" ||
                cboField4.Text == " Approve" || cboField4.Text == " Final" ||
                cboField4.Text == " Post" || cboField4.Text == " Default" ||
                cboField4.Text == " Cash Payment" || cboField4.Text == " Active" ||
                cboField4.Text == " Saleable" || cboField4.Text == " Non Inventory" ||
                cboField4.Text == " Contra Account" || cboField4.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue4.Items.AddRange(YesOrNo);
            }
            else if (cboField4.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue4.Items.AddRange(typeofaccount);
            }
            else if (cboField4.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue4.Items.AddRange(subsidiary);
            }
            else if (cboField4.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue4.Items.AddRange(journal);
            }
            else if (cboField4.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue4.Items.AddRange(form);
            }
        }

        private void cboField5_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND5.Checked = true;
            cboValue5.Items.Clear();
            cboValue5.Text = "";
            cboValue5.Focus();
            if (cboField5.Text == " Posted" || cboField5.Text == " Beg Bal" ||
                cboField5.Text == " Adjustment" || cboField5.Text == " Cancel" ||
                cboField5.Text == " Approve" || cboField5.Text == " Final" ||
                cboField5.Text == " Post" || cboField5.Text == " Default" ||
                cboField5.Text == " Cash Payment" || cboField5.Text == " Active" ||
                cboField5.Text == " Saleable" || cboField5.Text == " Non Inventory" ||
                cboField5.Text == " Contra Account" || cboField5.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue5.Items.AddRange(YesOrNo);
            }
            else if (cboField5.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue5.Items.AddRange(typeofaccount);
            }
            else if (cboField5.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue5.Items.AddRange(subsidiary);
            }
            else if (cboField5.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue5.Items.AddRange(journal);
            }
            else if (cboField5.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue5.Items.AddRange(form);
            }
        }

        private void cboField6_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND6.Checked = true;
            cboValue6.Items.Clear();
            cboValue6.Text = "";
            cboValue6.Focus();
            if (cboField6.Text == " Posted" || cboField6.Text == " Beg Bal" ||
                cboField6.Text == " Adjustment" || cboField6.Text == " Cancel" ||
                cboField6.Text == " Approve" || cboField6.Text == " Final" ||
                cboField6.Text == " Post" || cboField6.Text == " Default" ||
                cboField6.Text == " Cash Payment" || cboField6.Text == " Active" ||
                cboField6.Text == " Saleable" || cboField6.Text == " Non Inventory" ||
                cboField6.Text == " Contra Account" || cboField6.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue6.Items.AddRange(YesOrNo);
            }
            else if (cboField6.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue6.Items.AddRange(typeofaccount);
            }
            else if (cboField6.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue6.Items.AddRange(subsidiary);
            }
            else if (cboField6.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue6.Items.AddRange(journal);
            }
            else if (cboField6.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue6.Items.AddRange(form);
            }
        }

        private void cboField7_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkAND7.Checked = true;
            cboValue7.Items.Clear();
            cboValue7.Text = "";
            cboValue7.Focus();
            if (cboField7.Text == " Posted" || cboField7.Text == " Beg Bal" ||
                cboField7.Text == " Adjustment" || cboField7.Text == " Cancel" ||
                cboField7.Text == " Approve" || cboField7.Text == " Final" ||
                cboField7.Text == " Post" || cboField7.Text == " Default" ||
                cboField7.Text == " Cash Payment" || cboField7.Text == " Active" ||
                cboField7.Text == " Saleable" || cboField7.Text == " Non Inventory" ||
                cboField7.Text == " Contra Account" || cboField7.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue7.Items.AddRange(YesOrNo);
            }
            else if (cboField7.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue7.Items.AddRange(typeofaccount);
            }
            else if (cboField7.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue7.Items.AddRange(subsidiary);
            }
            else if (cboField7.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue7.Items.AddRange(journal);
            }
            else if (cboField7.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue7.Items.AddRange(form);
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
        #endregion "END OF EVENTS"

        private void cboField8_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboValue8.Items.Clear();
            cboValue8.Text = "";
            cboValue8.Focus();
            if (cboField8.Text == " Posted" || cboField8.Text == " Beg Bal" ||
                cboField8.Text == " Adjustment" || cboField8.Text == " Cancel" ||
                cboField8.Text == " Approve" || cboField8.Text == " Final" ||
                cboField8.Text == " Post" || cboField8.Text == " Default" ||
                cboField8.Text == " Cash Payment" || cboField8.Text == " Active" ||
                cboField8.Text == " Saleable" || cboField8.Text == " Non Inventory" ||
                cboField8.Text == " Contra Account" || cboField8.Text == " Closing Entry")
            {
                object[] YesOrNo = { "Y", "N" };
                cboValue8.Items.AddRange(YesOrNo);
            }
            else if (cboField8.Text == " Type Of Account")
            {
                object[] typeofaccount = { "Balance Sheet", "Income Statement" };
                cboValue8.Items.AddRange(typeofaccount);
            }
            else if (cboField8.Text == " Subsidiary")
            {
                object[] subsidiary = { "Bank", "Customer", "Supplier", "Employee", "Sales Person", "Equipment", "Building" };
                cboValue8.Items.AddRange(subsidiary);
            }
            else if (cboField8.Text == " Journal")
            {
                object[] journal = { "SJ", "PJ", "CDJ", "CRJ", "GJ" };
                cboValue8.Items.AddRange(journal);
            }
            else if (cboField8.Text == " Form")
            {
                object[] form = { "SV", "PV", "CV", "RV", "JV" };
                cboValue8.Items.AddRange(form);
            }
        }

        private void btnLookUp4_Click(object sender, EventArgs e)
        {
            if (itemHash.ContainsKey(cboField4.Text))
            {
                object val = itemHash[cboField4.Text];
                bool _hasValue = false;
                string _classname = "";
                GlobalClassHandler ch = new GlobalClassHandler();
                if (val.ToString().Contains("System.DateTime"))
                {
                    cboValue4.Text = setDataTime(cboField4.Text, cboValue4.Text);
                    _hasValue = true;
                }
                else if (cboField4.Text.Contains("Username") || cboField4.Text == " Prepared By" ||
                         cboField4.Text == " Certified Correct" || cboField4.Text == " Requested By" ||
                         cboField4.Text == " Approved By" || cboField4.Text == " Cancelled By" ||
                         cboField4.Text == " Finalized By" || cboField4.Text == " Posted By")
                {
                    _classname = "User";
                }
                else if (cboField4.Text == " Sales Person")
                {
                    _classname = "SalesPerson";
                }
                else if (cboField4.Text == " From Location Id" || cboField4.Text == " To Location Id")
                {
                    _classname = "Location";
                }
                else if (cboField4.Text.Contains("Id"))
                {
                    _classname = cboField4.Text.Replace("Id", "");
                    _classname = _classname.Replace(" ", "");
                }
                if (_classname != "")
                {
                    try
                    {
                        Type _ObjectType = ch.createObjectFromClass(_classname);
                        object ClassInstance = Activator.CreateInstance(_ObjectType);

                        loLookupValue.lObject = ClassInstance;
                        loLookupValue.lType = _ObjectType;
                        loLookupValue.lTableName = _classname;
                        loLookupValue.ShowDialog();
                        if (loLookupValue.lFromSelection)
                        {
                            cboValue4.Text = loLookupValue.lCode;
                            cboValue4.Focus();
                        }
                    }
                    catch
                    {
                        cboValue4.Text = "";
                        cboValue4.Focus();
                    }
                }
                else
                {
                    if (!_hasValue)
                    {
                        cboValue4.Text = "";
                        cboValue4.Focus();
                    }
                }
            }
        }
    }
}
