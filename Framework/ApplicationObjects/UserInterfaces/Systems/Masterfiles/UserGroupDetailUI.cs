using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Systems;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles
{
    public partial class UserGroupDetailUI : Form
    {
        #region "VARIABLES"
        UserGroup loUserGroup;
        public GlobalVariables.Operation lOperation;
        string lUserGroupId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public UserGroupDetailUI()
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            lOperation = GlobalVariables.Operation.Add;
            lUserGroupId = "";
        }
        public UserGroupDetailUI(string pId, string pDescription, string pRemarks)
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            lOperation = GlobalVariables.Operation.Edit;
            lUserGroupId = pId;
            txtDescription.Text = pDescription;
            txtRemarks.Text = pRemarks;
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "EVENTS"
        private void UserGroupUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            txtDescription.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion "END OF EVENTS"

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                loUserGroup.Id = lUserGroupId;

                if (txtDescription.Text != "")
                {
                    loUserGroup.Description = GlobalFunctions.replaceChar(txtDescription.Text);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("User Group description must have a value!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    txtDescription.Focus();
                    return;
                }
                loUserGroup.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);
                loUserGroup.UserId = GlobalVariables.UserId;
                string _Id = loUserGroup.saveUserGroup(lOperation);
                if (_Id != "")
                {
                    MessageBoxUI _mb = new MessageBoxUI("User Group has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    try
                    {
                        object[] _params = { "ViewAll", "", "" };
                        ParentList.GetType().GetMethod("refresh").Invoke(ParentList, _params);
                    }
                    catch { }
                    this.Close();
                }
                else
                {
                    MessageBoxUI _mb = new MessageBoxUI("Unable to save User Group!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
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
