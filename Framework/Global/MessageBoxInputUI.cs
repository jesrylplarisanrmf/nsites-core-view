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
    public partial class MessageBoxInputUI : Form
    {
        public string lInputString;
        public string lTitleString;
        public bool lFromOk = false;

        public MessageBoxInputUI()
        {
            InitializeComponent();
        }

        private void MessageBoxInputUI_Load(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }
            }
            txtInputString.Text = lInputString;
            lblTitleString.Text = lTitleString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lFromOk = true;
            lInputString = txtInputString.Text;
            this.Close();
        }
    }
}
