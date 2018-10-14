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

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class ChangeUserPasswordUI : Form
    {
        #region "VARIABLES"
        User loUser;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ChangeUserPasswordUI()
        {
            InitializeComponent();
            loUser = new User();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmChangeUserPassword", "Save"))
                {
                    return;
                }
                if (loUser.checkUserPassword(txtCurrentPassword.Text))
                {
                    MessageBoxUI ms = new MessageBoxUI("Please specify the correct current password.", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    txtCurrentPassword.Focus();
                    return;
                }

                if (txtNewPassword.Text != txtConfirmNewPassword.Text)
                {
                    MessageBoxUI ms = new MessageBoxUI("Your new password entries did not match.", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    txtNewPassword.Focus();
                    return;
                }

                if (loUser.changePassword(txtNewPassword.Text))
                {
                    MessageBoxUI ms = new MessageBoxUI("Your password has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
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
            try
            {
                try
                {
                    ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
                }
                catch
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void ChangeUserPasswordUI_Load(object sender, EventArgs e)
        {
            txtCurrentPassword.Focus();
        }

        #endregion "END OF EVENTS"
    }
}
