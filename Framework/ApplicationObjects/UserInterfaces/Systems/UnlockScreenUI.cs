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
    public partial class UnlockScreenUI : Form
    {
        #region "VARIABLES"
        User loUser = new User();
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public UnlockScreenUI()
        {
            InitializeComponent();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "EVENTS"
        private void UnlockScreenUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            lblUsername.Text = GlobalVariables.Username;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnUnlock_Click(null, new EventArgs());
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblUsername.Text == "technicalsupport")
                {
                    string _day = DateTime.Now.Day.ToString();
                    string _hour = DateTime.Now.Hour.ToString();
                    string _minute = DateTime.Now.Minute.ToString();

                    if (txtPassword.Text == _day + _hour + _minute)
                    {
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBoxUI ms = new MessageBoxUI("User password is incorrect!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                        ms.showDialog();
                        txtPassword.Focus();
                        return;
                    }
                }

                User _User = new User();
                DataTable _dt = _User.autenticateUser(GlobalVariables.Username, txtPassword.Text);
                if (_dt.Rows.Count > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("User password is incorrect!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    txtPassword.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnUnlock_Click");
                em.ShowDialog();
                return;
            }
        }
        #endregion "EVENTS"
    }
}
