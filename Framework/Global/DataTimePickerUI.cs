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
    public partial class DateTimePickerUI : Form
    {
        public DateTime lDateTime;
        public string lTime;
        public string lDate;
        public bool lSelectDate;

        public DateTimePickerUI(string pDateTime)
        {
            InitializeComponent();
            lSelectDate = false;
            if (pDateTime != "")
            {
                try
                {
                    lDateTime = DateTime.Parse(pDateTime);
                    mocDateTime.SelectionStart = lDateTime;
                    mocDateTime.SelectionEnd = lDateTime;
                    dtpTime.Value = lDateTime;
                }
                catch { }
            }
        }
        private void DateTimePickerUI_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TimeSpan time = new TimeSpan(dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second);
            lDateTime = Convert.ToDateTime(mocDateTime.SelectionStart.ToShortDateString() + " " + time.ToString());
            lSelectDate = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
