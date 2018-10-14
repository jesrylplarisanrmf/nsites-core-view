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

namespace NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles
{
    public partial class CategoryDetailUI : Form
    {
        #region "VARIABLES"
        string lId;
        string[] lRecords = new string[4];
        GlobalVariables.Operation lOperation;
        Category loCategory;
        InventoryGroup loInventoryGroup;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public CategoryDetailUI()
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Add;
            loCategory = new Category();
            loInventoryGroup = new InventoryGroup();
        }
        public CategoryDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lId = "";
            lOperation = GlobalVariables.Operation.Edit;
            loCategory = new Category();
            loInventoryGroup = new InventoryGroup();
            lRecords = pRecords;
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
        private void clear()
        {
            lId = "";
            txtDescription.Clear();
            cboInventoryGroup.SelectedIndex = -1;
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void CategoryDetailUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                try
                {
                    cboInventoryGroup.DataSource = loInventoryGroup.getAllData("ViewAll", "", "");
                    cboInventoryGroup.DisplayMember = "Description";
                    cboInventoryGroup.ValueMember = "Id";
                    cboInventoryGroup.SelectedIndex = -1;
                }
                catch { }

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    lId = lRecords[0];
                    txtDescription.Text = lRecords[1];
                    cboInventoryGroup.Text = lRecords[2];
                    txtRemarks.Text = lRecords[3];
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "CategoryDetailUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loCategory.Id = lId;
                loCategory.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                try
                {
                    loCategory.InventoryGroupId = cboInventoryGroup.SelectedValue.ToString();
                }
                catch
                {
                    loCategory.InventoryGroupId = "";
                }
                loCategory.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loCategory.UserId = GlobalVariables.UserId;

                string _Id = loCategory.save(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("Category has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _Id;
                    lRecords[1] = txtDescription.Text;
                    lRecords[2] = cboInventoryGroup.Text;
                    lRecords[3] = txtRemarks.Text;
                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                    }
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
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
