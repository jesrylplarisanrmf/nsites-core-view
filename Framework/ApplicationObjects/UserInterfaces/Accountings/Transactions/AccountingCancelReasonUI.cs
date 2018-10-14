﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;

namespace NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions
{
    public partial class AccountingCancelReasonUI : Form
    {
        public string lReason;
        
        public AccountingCancelReasonUI()
        {
            InitializeComponent();
            lReason = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lReason = "";
            this.Close();
        }

        private void ReasonUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));

            lReason = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lReason = Global. GlobalFunctions.replaceChar(txtReason.Text);
            this.Close();
        }
    }
}