using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace NSites_V.Global
{
    public partial class ErrorMessageUI : Form
    {
        #region "VARIABLES"
        string lMessage;
        string lFormName;
        string lEvent;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ErrorMessageUI(string pMessage, string pFormName, string pEvent)
        {
            InitializeComponent();
            lMessage = pMessage;
            lFormName = pFormName;
            lEvent = pEvent;
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

        private void ErrorMessageUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
            
            rtxtMessage.Text = lMessage;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            string _body = "<h4>Error Date : " + string.Format("{0:MM-dd-yyyy hh:mm tt}", DateTime.Now) + "</h4>" +
                            "<h4>Company Name : " + GlobalVariables.CompanyName + "</4>" +
                           "<h4>Application Name : " + GlobalVariables.ApplicationName + "</4>" +
                           "<h2>Processor Id  : " + GlobalFunctions.GetProcessorId() + "</h2>" +
                           "<h4>Computer Name  : " + Environment.MachineName + "</h4> </br>" +
                           "<h4>Form Name  : " + lFormName + "</h4> </br>" +
                           "<h4>Event  : " + lEvent + "</h4> </br>" +
                           "<p>ERROR MESSAGE : " + rtxtMessage.Text + "</p>";
            try
            {
                //OLD USING GMAIL SMTP
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("jbcsoftwares.info@gmail.com");
                //mail.To.Add(GlobalVariables.TechnicalSupportEmailAddress);
                mail.To.Add("jesrylplarisan@gmail.com");
                //mail.CC.Add("jesrylplarisan@gmail.com");
                mail.Subject = GlobalVariables.CompanyName + " : Error Message!";
                mail.IsBodyHtml = true;
                mail.Body = _body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("jbcsoftwares.info@gmail.com", "jbcadmin12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBoxUI mb = new MessageBoxUI("Email successfully send!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, "ErrorMessageUI", "btnSendEmail_Click");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
