using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Systems;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles
{
    public partial class UserDetailUI : Form
    {
        #region "VARIABLES"
        string[] lRecordData = new string[5];
        GlobalVariables.Operation lOperation;
        User loUser;
        UserGroup loUserGroup;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public UserDetailUI()
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Add;
            loUser = new User();
            loUserGroup = new UserGroup();
        }
        public UserDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loUser = new User();
            loUserGroup = new UserGroup();
            lRecordData = pRecords;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void clear()
        {
            try
            {
                txtUsername.Clear();
                txtPassword.Clear();
                txtFullname.Clear();
                cboUserGroup.SelectedIndex = -1;
                txtRemarks.Clear();
                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "EVENTS"
        private void UserUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
                
                cboUserGroup.DataSource = loUserGroup.getAllData("ViewAll", "", "");
                cboUserGroup.DisplayMember = "Description";
                cboUserGroup.ValueMember = "Id";
                cboUserGroup.SelectedIndex = -1;

                if (lOperation == GlobalVariables.Operation.Edit)
                {
                    foreach (DataRow _dr in loUser.getAllData("", lRecordData[0], "").Rows)
                    {
                        txtUsername.Text = _dr["Username"].ToString();
                        txtUsername.ReadOnly = true;
                        txtUsername.BackColor = SystemColors.ControlLight;
                        txtUsername.TabStop = false;
                        txtPassword.Text = "";
                        txtFullname.Text = _dr["Fullname"].ToString();
                        cboUserGroup.Text = _dr["User Group"].ToString();
                        txtRemarks.Text = _dr["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "UserUI_Load");
                em.ShowDialog();
                return;
            }
        }
        #endregion "END OF EVENTS"

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loUser.Id = lRecordData[0];

                if (txtUsername.Text != "")
                {
                    loUser.Username = GlobalFunctions.replaceChar(txtUsername.Text);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Username must have a value!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    txtUsername.Focus();
                    return;
                }
                loUser.Password = txtPassword.Text;
                loUser.Fullname = txtFullname.Text;
                
                try
                {
                    loUser.UserGroupId = cboUserGroup.SelectedValue.ToString();
                }
                catch
                {
                    loUser.UserGroupId = "";
                }
                loUser.Remarks = txtRemarks.Text;
                loUser.UserId = GlobalVariables.UserId;

                string _Id = loUser.saveUser(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("User has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecordData[0] = _Id;
                    lRecordData[1] = txtUsername.Text;
                    lRecordData[2] = txtFullname.Text;
                    lRecordData[3] = cboUserGroup.Text;
                    lRecordData[4] = txtRemarks.Text;
                    object[] _params = { lRecordData };
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
