using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;

namespace NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions
{
    public partial class SalesCancelReasonUI : Form
    {
        public string lReason;
        
        public SalesCancelReasonUI()
        {
            InitializeComponent();
            lReason = "";
        }

        private void SalesCancelReasonUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            lReason = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lReason = Global.GlobalFunctions.replaceChar(txtReason.Text);
            this.Close();
        }
    }
}
