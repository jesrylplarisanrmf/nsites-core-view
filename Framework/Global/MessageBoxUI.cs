using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSites_V.Global
{
    public partial class MessageBoxUI : Form
    {
        #region "VARIABLES"
        string lMessage;
        Exception lEx;
        GlobalVariables.Icons lIcon;
        GlobalVariables.Buttons lButton;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public MessageBoxUI(string pMessage, GlobalVariables.Icons pIcon, GlobalVariables.Buttons pButton)
        {
            InitializeComponent();
            lMessage = pMessage;
            lIcon = pIcon;
            lButton = pButton;
            this.Text = GlobalVariables.ApplicationName;
        }
        public MessageBoxUI(Exception pEx, GlobalVariables.Icons pIcon, GlobalVariables.Buttons pButton)
        {
            InitializeComponent();
            lMessage = pEx.Message;
            lEx = pEx;
            lIcon = pIcon;
            lButton = pButton;
            this.Text = GlobalVariables.ApplicationName;
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public DialogResult Operation
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "EVENTS"
        private void MessageBoxUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            }
            catch{}

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(pctError, "Click to view error details");
            if (lMessage.Contains("Duplicate"))
            {
                lMessage = "Duplicate entry of data is not allowed!";
            }
            else if (lMessage.Contains("Object reference"))
            {
                lMessage = "Some fields must have a value!";
            }
            lblMessage.Text = lMessage;

            switch (lIcon)
            {
                case GlobalVariables.Icons.Information:
                    pctInfo.Visible = true;
                    break;
                case GlobalVariables.Icons.Save:
                    pctSave.Visible = true;
                    break;
                case GlobalVariables.Icons.Ok:
                    pctOk.Visible = true;
                    break;
                case GlobalVariables.Icons.QuestionMark:
                    pctQuestionMark.Visible = true;
                    break;
                case GlobalVariables.Icons.Delete:
                    pctDelete.Visible = true;
                    break;
                case GlobalVariables.Icons.Warning:
                    pctWarning.Visible = true;
                    break;
                case GlobalVariables.Icons.Error:
                    pctError.Visible = true;
                    break;
                default:
                    break;
            }

            switch (lButton)
            {
                case GlobalVariables.Buttons.OK:
                    btnOk.Visible = true;
                    break;
                case GlobalVariables.Buttons.OKCancel:
                    btnOK1.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case GlobalVariables.Buttons.YesNo:
                    btnYes1.Visible = true;
                    btnNo.Visible = true;
                    break;
                case GlobalVariables.Buttons.YesNoCancel:
                    btnYes2.Visible = true;
                    btnNo1.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case GlobalVariables.Buttons.Close:
                    btnClose.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void btnYes2_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.Yes;
            this.Close();
        }

        private void btnOK1_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.OK;
            this.Close();
        }

        private void btnNo1_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.No;
            this.Close();
        }

        private void btnYes1_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.Cancel;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.No;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Operation = DialogResult.Abort;
            this.Close();
        }
        #endregion "END OF EVENTS"

        #region "METHODS"
        public MessageBoxUI showDialog()
        {
            base.ShowDialog();
            return this;
        }
        #endregion "END OF METHODS"

        private void pctError_Click(object sender, EventArgs e)
        {
            if (this.Height != 300)
            {
                rtxtMessage.Visible = true;
                lblMessage.Visible = false;
                rtxtMessage.Text = lEx.Message + lEx.StackTrace;
                this.Height = 300;
            }
            else
            {
                this.Height = 176;
                lblMessage.Text = lMessage;
                lblMessage.Visible = true;
                rtxtMessage.Visible = false;
            }
        }
    }
}
