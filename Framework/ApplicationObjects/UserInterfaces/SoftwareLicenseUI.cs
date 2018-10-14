using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using System.IO;
using System.Net.Mail;

namespace NSites_V.ApplicationObjects.UserInterfaces
{
    public partial class SoftwareLicenseUI : Form
    {
        #region "VARIABLES"
        public DateTime lExpiryDate;
        string lCompanyName;
        string lApplicationName;

        CryptorEngine loCryptorEngine;
        Random _Random;
        string lExpectedValue1;
        string lExpectedValue2;
        string lExpectedValue3;
        string lExpectedValue4;
        string lNumbers1;
        string lNumbers2;
        string lNumbers3;
        string lNumbers4;
        string lGetExpectedValueNumber1;
        string lGetExpectedValueNumber2;
        string lGetExpectedValueNumber3;
        string lGetExpectedValueNumber4;
        public string lLicenseNo;
        public bool lSuccessFullyVerified;
        #endregion "END OF VARIABLES"

        public SoftwareLicenseUI(string pCompanyName, string pApplicationName)
        {
            InitializeComponent();
            lCompanyName = pCompanyName;
            lApplicationName = pApplicationName;

            loCryptorEngine = new CryptorEngine();
            lExpectedValue1 = "";
            lExpectedValue2 = "";
            lExpectedValue3 = "";
            lExpectedValue4 = "";
            lNumbers1 = "";
            lNumbers2 = "";
            lNumbers3 = "";
            lNumbers4 = "";
            lGetExpectedValueNumber1 = "";
            lGetExpectedValueNumber2 = "";
            lGetExpectedValueNumber3 = "";
            lGetExpectedValueNumber4 = "";
            _Random = new Random();
            lSuccessFullyVerified = false;
            lLicenseNo = "";
        }

        public bool sendSoftwareLicenseEmail(string pProcessorId, string pLicenseCode)
        {
            bool _status = false;

            string _body = "<h4>Date Requested : " + string.Format("{0:MM-dd-yyyy hh:mm tt}", DateTime.Now) + "</h4>" +
                            "<h4>Company Name : " + lCompanyName + "</4>" +
                           "<h4>Application Name : " + lApplicationName + "</4>" +
                           "<h2>Processor Id  : " + pProcessorId + "</h2>" +
                           "<h2>Computer Name  : " + Environment.MachineName + "</h2> </br>" +
                           "<h2>LICENSE CODE : " + pLicenseCode + "</h2>";
            try
            {
                //OLD USING GMAIL SMTP
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("jbcsoftwares.info@gmail.com");
                mail.To.Add("jesrylplarisan@gmail.com");
                //mail.CC.Add("jbc.cris2per@gmail.com");
                mail.Subject = lCompanyName + " : Request for Software License Key!";
                mail.IsBodyHtml = true;
                mail.Body = _body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("jbcsoftwares.info@gmail.com", "jbcadmin12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                _status = true;
                //MessageBoxUI mb = new MessageBoxUI("Email successfully send!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                //mb.ShowDialog();
            }
            catch
            {
                _status = false;
            }
            return _status;
        }

        public bool sendEmailForLicenseKeyRecording(string pProcessorId, string pLicenseKey)
        {
            bool _status = false;

            string _body = "<h4>Date Encoded : " + string.Format("{0:MM-dd-yyyy hh:mm tt}", DateTime.Now) + "</h4>" +
                            "<h4>Company Name : " + lCompanyName + "</4>" +
                           "<h4>Application Name : " + lApplicationName + "</4>" +
                           "<h2>Processor Id  : " + pProcessorId + "</h2>" +
                           "<h2>Computer Name  : " + Environment.MachineName + "</h2> </br>" +
                           "<h2>LICENSE KEY : " + pLicenseKey + "</h2>";
            try
            {
                //OLD USING GMAIL SMTP
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("jbcsoftwares.info@gmail.com");
                mail.To.Add("jesrylplarisan@gmail.com");
                //mail.CC.Add("jbc.cris2per@gmail.com");
                mail.Subject = "FYI : " + lCompanyName + " successfully acquired a Software License Key!";
                mail.IsBodyHtml = true;
                mail.Body = _body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("jbcsoftwares.info@gmail.com", "jbcadmin12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                _status = true;
                //MessageBoxUI mb = new MessageBoxUI("Email successfully send!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                //mb.ShowDialog();
            }
            catch
            {
                _status = false;
            }
            return _status;
        }


        private void SoftwareLicenseUI_Load(object sender, EventArgs e)
        {
            try
            {
                lblCompanyName.Text = lCompanyName;
                lSuccessFullyVerified = false;

                //for every software varies...
                string SoftwareNo = "1";

                string[] lValueWithNumbers2 = generateCode(ref txtCode2, ref lblKey2).Split('*');
                string[] lValueWithNumbers3 = generateCode(ref txtCode3, ref lblKey3).Split('*');
                string[] lValueWithNumbers4 = generateCode(ref txtCode4, ref lblKey4).Split('*');

                lExpectedValue2 = lValueWithNumbers2[0].ToString();
                lGetExpectedValueNumber2 = lValueWithNumbers2[1].ToString();
                lNumbers2 = lValueWithNumbers2[2].ToString();

                lExpectedValue3 = lValueWithNumbers3[0].ToString();
                lGetExpectedValueNumber3 = lValueWithNumbers3[1].ToString();
                lNumbers3 = lValueWithNumbers3[2].ToString();

                lExpectedValue4 = lValueWithNumbers4[0].ToString();
                lGetExpectedValueNumber4 = lValueWithNumbers4[1].ToString();
                lNumbers4 = lValueWithNumbers4[2].ToString();
                //for number 1
                string _Value1 = expectedValue(int.Parse(SoftwareNo), int.Parse(SoftwareNo));
                string _Value2 = expectedValue(int.Parse(SoftwareNo), int.Parse(lGetExpectedValueNumber2));
                string _Value3 = expectedValue(int.Parse(SoftwareNo), int.Parse(lGetExpectedValueNumber3));
                string _Value4 = expectedValue(int.Parse(SoftwareNo), int.Parse(lGetExpectedValueNumber4));

                lExpectedValue1 = _Value1 + _Value2 + _Value3 + _Value4;
                lGetExpectedValueNumber1 = "2";
                lNumbers1 = SoftwareNo + lGetExpectedValueNumber2 + lGetExpectedValueNumber3 + lGetExpectedValueNumber4;

                txtCode1.Text = lNumbers1;
                lblKey1.Text = lNumbers1 + " = " + SoftwareNo + ", " + lGetExpectedValueNumber2 + ", " + lGetExpectedValueNumber3 + ", " + lGetExpectedValueNumber4;

                if (!sendSoftwareLicenseEmail(GlobalFunctions.GetProcessorId(), txtCode1.Text + "-" + txtCode2.Text + "-" + txtCode3.Text + "-" + txtCode4.Text))
                {
                    MessageBoxUI mb = new MessageBoxUI("NO INTERNET CONNECTION!\nYou must have an internet connection or contact supplier!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    Application.Exit();
                }

                txtLicenseKey.Focus();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "SoftwareLicenseUI_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            try
            {
                string[] splitKey = txtLicenseKey.Text.Split('-');
                string _key1 = splitKey[0];
                string _key2 = splitKey[1];
                string _key3 = splitKey[2];
                string _key4 = splitKey[3];

                if (_key1 == lExpectedValue1 && _key2 == lExpectedValue2 &&
                    _key3 == lExpectedValue3 && _key4 == lExpectedValue4)
                {
                    if (!sendEmailForLicenseKeyRecording(GlobalFunctions.GetProcessorId(), txtLicenseKey.Text))
                    {
                        MessageBoxUI mb1 = new MessageBoxUI("NO INTERNET CONNECTION!\nYou must have an internet connection or contact supplier!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        mb1.ShowDialog();
                        Application.Exit();
                    }

                    MessageBoxUI mb = new MessageBoxUI("CONGRATULATIONS!\nLicense has been verified. Thank you for using NSites Business Applications!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    //writeLicenseCertificate();
                    lLicenseNo = lExpectedValue1 + "-" + lExpectedValue2 + "-" + lExpectedValue3 + "-" + lExpectedValue4;
                    lExpiryDate = DateTime.Parse("01/01/9999");
                    lSuccessFullyVerified = true;
                    this.Close();
                }
                else
                {
                    MessageBoxUI mb = new MessageBoxUI("INVALID KEY!\nPlease input correct license key or contact supplier!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnAuthenticate_Click");
                em.ShowDialog();
                Application.Exit();
            }
        }

        public string generateCode(ref TextBox pTxt, ref Label pLbl)
        {
            int _N1 = _Random.Next(1, 36);
            int _N2 = _Random.Next(1, 36);
            int _N3 = _Random.Next(1, 36);
            int _N4 = _Random.Next(1, 36);

            string _Value1 = getNumberValue(_N1);
            string _Value2 = getNumberValue(_N2);
            string _Value3 = getNumberValue(_N3);
            string _Value4 = getNumberValue(_N4);

            string _NumbersWithExpectedValue = "";
            string _NumbersValue = _Value1 + _Value2 + _Value3 + _Value4;
            pTxt.Text = _NumbersValue;
            pLbl.Text = _NumbersValue + " = " + getMilitaryValue(char.Parse(_Value1)) + ", " + getMilitaryValue(char.Parse(_Value2)) + ", " +
                getMilitaryValue(char.Parse(_Value3)) + ", " + getMilitaryValue(char.Parse(_Value4));

            int _getExpectedValueNumber = _Random.Next(1, 8);
            string _ExpectedValue1 = expectedValue(_getExpectedValueNumber, _N1);
            string _ExpectedValue2 = expectedValue(_getExpectedValueNumber, _N2);
            string _ExpectedValue3 = expectedValue(_getExpectedValueNumber, _N3);
            string _ExpectedValue4 = expectedValue(_getExpectedValueNumber, _N4);
            _NumbersWithExpectedValue = _ExpectedValue1 + _ExpectedValue2 + _ExpectedValue3 + _ExpectedValue4 + "*" + _getExpectedValueNumber + "*" + _N1 + " " + _N2 + " " + _N3 + " " + _N4;
            return _NumbersWithExpectedValue;
        }
        //get the Number and the value...
        private string getNumberValue(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "Y"; break;
                case 2: _Return = "D"; break;
                case 3: _Return = "I"; break;
                case 4: _Return = "O"; break;
                case 5: _Return = "J"; break;
                case 6: _Return = "1"; break;
                case 7: _Return = "A"; break;
                case 8: _Return = "7"; break;
                case 9: _Return = "P"; break;
                case 10: _Return = "R"; break;
                case 11: _Return = "E"; break;
                case 12: _Return = "0"; break;
                case 13: _Return = "B"; break;
                case 14: _Return = "H"; break;
                case 15: _Return = "9"; break;
                case 16: _Return = "6"; break;
                case 17: _Return = "Q"; break;
                case 18: _Return = "4"; break;
                case 19: _Return = "M"; break;
                case 20: _Return = "V"; break;
                case 21: _Return = "C"; break;
                case 22: _Return = "5"; break;
                case 23: _Return = "Z"; break;
                case 24: _Return = "K"; break;
                case 25: _Return = "S"; break;
                case 26: _Return = "3"; break;
                case 27: _Return = "F"; break;
                case 28: _Return = "W"; break;
                case 29: _Return = "N"; break;
                case 30: _Return = "X"; break;
                case 31: _Return = "T"; break;
                case 32: _Return = "G"; break;
                case 33: _Return = "8"; break;
                case 34: _Return = "2"; break;
                case 35: _Return = "U"; break;
                case 36: _Return = "L"; break;
            }
            return _Return;
        }
        //get the get the Expected Value
        private string getExpectedValue1(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "O"; break;
                case 2: _Return = "S"; break;
                case 3: _Return = "6"; break;
                case 4: _Return = "G"; break;
                case 5: _Return = "H"; break;
                case 6: _Return = "A"; break;
                case 7: _Return = "9"; break;
                case 8: _Return = "K"; break;
                case 9: _Return = "0"; break;
                case 10: _Return = "W"; break;
                case 11: _Return = "E"; break;
                case 12: _Return = "X"; break;
                case 13: _Return = "I"; break;
                case 14: _Return = "2"; break;
                case 15: _Return = "P"; break;
                case 16: _Return = "Y"; break;
                case 17: _Return = "7"; break;
                case 18: _Return = "Q"; break;
                case 19: _Return = "T"; break;
                case 20: _Return = "B"; break;
                case 21: _Return = "M"; break;
                case 22: _Return = "R"; break;
                case 23: _Return = "1"; break;
                case 24: _Return = "Z"; break;
                case 25: _Return = "F"; break;
                case 26: _Return = "V"; break;
                case 27: _Return = "4"; break;
                case 28: _Return = "J"; break;
                case 29: _Return = "C"; break;
                case 30: _Return = "8"; break;
                case 31: _Return = "U"; break;
                case 32: _Return = "5"; break;
                case 33: _Return = "D"; break;
                case 34: _Return = "N"; break;
                case 35: _Return = "3"; break;
                case 36: _Return = "L"; break;
            }
            return _Return;
        }

        private string getExpectedValue2(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "D"; break;
                case 2: _Return = "L"; break;
                case 3: _Return = "A"; break;
                case 4: _Return = "2"; break;
                case 5: _Return = "3"; break;
                case 6: _Return = "C"; break;
                case 7: _Return = "N"; break;
                case 8: _Return = "T"; break;
                case 9: _Return = "Z"; break;
                case 10: _Return = "X"; break;
                case 11: _Return = "B"; break;
                case 12: _Return = "V"; break;
                case 13: _Return = "5"; break;
                case 14: _Return = "1"; break;
                case 15: _Return = "M"; break;
                case 16: _Return = "W"; break;
                case 17: _Return = "4"; break;
                case 18: _Return = "E"; break;
                case 19: _Return = "O"; break;
                case 20: _Return = "K"; break;
                case 21: _Return = "7"; break;
                case 22: _Return = "G"; break;
                case 23: _Return = "6"; break;
                case 24: _Return = "Y"; break;
                case 25: _Return = "P"; break;
                case 26: _Return = "9"; break;
                case 27: _Return = "F"; break;
                case 28: _Return = "J"; break;
                case 29: _Return = "R"; break;
                case 30: _Return = "0"; break;
                case 31: _Return = "U"; break;
                case 32: _Return = "8"; break;
                case 33: _Return = "S"; break;
                case 34: _Return = "Q"; break;
                case 35: _Return = "H"; break;
                case 36: _Return = "I"; break;
            }
            return _Return;
        }

        private string getExpectedValue3(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "P"; break;
                case 2: _Return = "M"; break;
                case 3: _Return = "1"; break;
                case 4: _Return = "T"; break;
                case 5: _Return = "2"; break;
                case 6: _Return = "S"; break;
                case 7: _Return = "R"; break;
                case 8: _Return = "G"; break;
                case 9: _Return = "H"; break;
                case 10: _Return = "C"; break;
                case 11: _Return = "O"; break;
                case 12: _Return = "K"; break;
                case 13: _Return = "I"; break;
                case 14: _Return = "J"; break;
                case 15: _Return = "4"; break;
                case 16: _Return = "E"; break;
                case 17: _Return = "9"; break;
                case 18: _Return = "5"; break;
                case 19: _Return = "F"; break;
                case 20: _Return = "3"; break;
                case 21: _Return = "D"; break;
                case 22: _Return = "Z"; break;
                case 23: _Return = "U"; break;
                case 24: _Return = "6"; break;
                case 25: _Return = "8"; break;
                case 26: _Return = "V"; break;
                case 27: _Return = "L"; break;
                case 28: _Return = "7"; break;
                case 29: _Return = "X"; break;
                case 30: _Return = "B"; break;
                case 31: _Return = "Q"; break;
                case 32: _Return = "0"; break;
                case 33: _Return = "N"; break;
                case 34: _Return = "A"; break;
                case 35: _Return = "W"; break;
                case 36: _Return = "Y"; break;
            }
            return _Return;
        }

        private string getExpectedValue4(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "B"; break;
                case 2: _Return = "W"; break;
                case 3: _Return = "Q"; break;
                case 4: _Return = "X"; break;
                case 5: _Return = "N"; break;
                case 6: _Return = "M"; break;
                case 7: _Return = "5"; break;
                case 8: _Return = "A"; break;
                case 9: _Return = "4"; break;
                case 10: _Return = "1"; break;
                case 11: _Return = "6"; break;
                case 12: _Return = "O"; break;
                case 13: _Return = "2"; break;
                case 14: _Return = "T"; break;
                case 15: _Return = "L"; break;
                case 16: _Return = "Y"; break;
                case 17: _Return = "S"; break;
                case 18: _Return = "R"; break;
                case 19: _Return = "8"; break;
                case 20: _Return = "Z"; break;
                case 21: _Return = "P"; break;
                case 22: _Return = "C"; break;
                case 23: _Return = "3"; break;
                case 24: _Return = "H"; break;
                case 25: _Return = "7"; break;
                case 26: _Return = "9"; break;
                case 27: _Return = "D"; break;
                case 28: _Return = "0"; break;
                case 29: _Return = "U"; break;
                case 30: _Return = "F"; break;
                case 31: _Return = "E"; break;
                case 32: _Return = "G"; break;
                case 33: _Return = "J"; break;
                case 34: _Return = "K"; break;
                case 35: _Return = "V"; break;
                case 36: _Return = "I"; break;
            }
            return _Return;
        }

        private string getExpectedValue5(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "1"; break;
                case 2: _Return = "K"; break;
                case 3: _Return = "L"; break;
                case 4: _Return = "9"; break;
                case 5: _Return = "2"; break;
                case 6: _Return = "I"; break;
                case 7: _Return = "J"; break;
                case 8: _Return = "3"; break;
                case 9: _Return = "X"; break;
                case 10: _Return = "Q"; break;
                case 11: _Return = "R"; break;
                case 12: _Return = "Y"; break;
                case 13: _Return = "Z"; break;
                case 14: _Return = "S"; break;
                case 15: _Return = "E"; break;
                case 16: _Return = "G"; break;
                case 17: _Return = "F"; break;
                case 18: _Return = "0"; break;
                case 19: _Return = "M"; break;
                case 20: _Return = "T"; break;
                case 21: _Return = "H"; break;
                case 22: _Return = "N"; break;
                case 23: _Return = "6"; break;
                case 24: _Return = "W"; break;
                case 25: _Return = "O"; break;
                case 26: _Return = "U"; break;
                case 27: _Return = "8"; break;
                case 28: _Return = "P"; break;
                case 29: _Return = "B"; break;
                case 30: _Return = "C"; break;
                case 31: _Return = "D"; break;
                case 32: _Return = "5"; break;
                case 33: _Return = "7"; break;
                case 34: _Return = "A"; break;
                case 35: _Return = "V"; break;
                case 36: _Return = "4"; break;
            }
            return _Return;
        }

        private string getExpectedValue6(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "N"; break;
                case 2: _Return = "B"; break;
                case 3: _Return = "V"; break;
                case 4: _Return = "C"; break;
                case 5: _Return = "X"; break;
                case 6: _Return = "Z"; break;
                case 7: _Return = "L"; break;
                case 8: _Return = "F"; break;
                case 9: _Return = "J"; break;
                case 10: _Return = "H"; break;
                case 11: _Return = "G"; break;
                case 12: _Return = "K"; break;
                case 13: _Return = "2"; break;
                case 14: _Return = "S"; break;
                case 15: _Return = "A"; break;
                case 16: _Return = "P"; break;
                case 17: _Return = "Y"; break;
                case 18: _Return = "I"; break;
                case 19: _Return = "6"; break;
                case 20: _Return = "O"; break;
                case 21: _Return = "T"; break;
                case 22: _Return = "R"; break;
                case 23: _Return = "9"; break;
                case 24: _Return = "W"; break;
                case 25: _Return = "Q"; break;
                case 26: _Return = "E9"; break;
                case 27: _Return = "8"; break;
                case 28: _Return = "7"; break;
                case 29: _Return = "U"; break;
                case 30: _Return = "5"; break;
                case 31: _Return = "M"; break;
                case 32: _Return = "4"; break;
                case 33: _Return = "3"; break;
                case 34: _Return = "D"; break;
                case 35: _Return = "0"; break;
                case 36: _Return = "1"; break;
            }
            return _Return;
        }

        private string getExpectedValue7(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "5"; break;
                case 2: _Return = "Q"; break;
                case 3: _Return = "A"; break;
                case 4: _Return = "Z"; break;
                case 5: _Return = "2"; break;
                case 6: _Return = "R"; break;
                case 7: _Return = "S"; break;
                case 8: _Return = "C"; break;
                case 9: _Return = "3"; break;
                case 10: _Return = "E"; break;
                case 11: _Return = "D"; break;
                case 12: _Return = "X"; break;
                case 13: _Return = "4"; break;
                case 14: _Return = "W"; break;
                case 15: _Return = "Y"; break;
                case 16: _Return = "V"; break;
                case 17: _Return = "1"; break;
                case 18: _Return = "T"; break;
                case 19: _Return = "G"; break;
                case 20: _Return = "B"; break;
                case 21: _Return = "6"; break;
                case 22: _Return = "F"; break;
                case 23: _Return = "H"; break;
                case 24: _Return = "N"; break;
                case 25: _Return = "7"; break;
                case 26: _Return = "U"; break;
                case 27: _Return = "K"; break;
                case 28: _Return = "M"; break;
                case 29: _Return = "8"; break;
                case 30: _Return = "I"; break;
                case 31: _Return = "J"; break;
                case 32: _Return = "9"; break;
                case 33: _Return = "O"; break;
                case 34: _Return = "P"; break;
                case 35: _Return = "0"; break;
                case 36: _Return = "L"; break;
            }
            return _Return;
        }

        private string getExpectedValue8(int pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 1: _Return = "C"; break;
                case 2: _Return = "R"; break;
                case 3: _Return = "T"; break;
                case 4: _Return = "0"; break;
                case 5: _Return = "V"; break;
                case 6: _Return = "Y"; break;
                case 7: _Return = "B"; break;
                case 8: _Return = "P"; break;
                case 9: _Return = "X"; break;
                case 10: _Return = "7"; break;
                case 11: _Return = "8"; break;
                case 12: _Return = "I"; break;
                case 13: _Return = "9"; break;
                case 14: _Return = "O"; break;
                case 15: _Return = "Q"; break;
                case 16: _Return = "N"; break;
                case 17: _Return = "U"; break;
                case 18: _Return = "5"; break;
                case 19: _Return = "F"; break;
                case 20: _Return = "D"; break;
                case 21: _Return = "M"; break;
                case 22: _Return = "G"; break;
                case 23: _Return = "1"; break;
                case 24: _Return = "S"; break;
                case 25: _Return = "Z"; break;
                case 26: _Return = "6"; break;
                case 27: _Return = "K"; break;
                case 28: _Return = "E"; break;
                case 29: _Return = "W"; break;
                case 30: _Return = "A"; break;
                case 31: _Return = "4"; break;
                case 32: _Return = "J"; break;
                case 33: _Return = "3"; break;
                case 34: _Return = "H"; break;
                case 35: _Return = "2"; break;
                case 36: _Return = "L"; break;
            }
            return _Return;
        }

        public string expectedValue(int pExpectedNumber, int pNumber)
        {
            string _Return = "";
            switch (pExpectedNumber)
            {
                case 1:
                    _Return = getExpectedValue1(pNumber);
                    break;
                case 2:
                    _Return = getExpectedValue2(pNumber); ;
                    break;
                case 3:
                    _Return = getExpectedValue3(pNumber); ;
                    break;
                case 4:
                    _Return = getExpectedValue4(pNumber); ;
                    break;
                case 5:
                    _Return = getExpectedValue5(pNumber); ;
                    break;
                case 6:
                    _Return = getExpectedValue6(pNumber); ;
                    break;
                case 7:
                    _Return = getExpectedValue7(pNumber); ;
                    break;
                case 8:
                    _Return = getExpectedValue8(pNumber); ;
                    break;
            }
            return _Return;
        }

        private string getMilitaryValue(char pNumber)
        {
            string _Return = "";
            switch (pNumber)
            {
                case 'J':
                    _Return = "Juliet";
                    break;
                case 'O':
                    _Return = "Oscar";
                    break;
                case 'A':
                    _Return = "Alfa";
                    break;
                case 'P':
                    _Return = "Papa";
                    break;
                case 'K':
                    _Return = "Kilo";
                    break;
                case '9':
                    _Return = "9";
                    break;
                case 'Y':
                    _Return = "Yankee";
                    break;
                case 'C':
                    _Return = "Charlie";
                    break;
                case '8':
                    _Return = "8";
                    break;
                case 'U':
                    _Return = "Uniform";
                    break;
                case 'X':
                    _Return = "X-ray";
                    break;
                case 'H':
                    _Return = "Hotel";
                    break;
                case 'R':
                    _Return = "Romeo";
                    break;
                case '4':
                    _Return = "4";
                    break;
                case '6':
                    _Return = "6";
                    break;
                case 'W':
                    _Return = "Whiskey";
                    break;
                case '7':
                    _Return = "7";
                    break;
                case 'D':
                    _Return = "Delta";
                    break;
                case '1':
                    _Return = "1";
                    break;
                case 'Z':
                    _Return = "Zebra";
                    break;
                case '0':
                    _Return = "0";
                    break;
                case 'I':
                    _Return = "India";
                    break;
                case 'S':
                    _Return = "Sierra";
                    break;
                case 'E':
                    _Return = "Echo";
                    break;
                case 'N':
                    _Return = "November";
                    break;
                case 'G':
                    _Return = "Golf";
                    break;
                case '2':
                    _Return = "2";
                    break;
                case 'M':
                    _Return = "Mike";
                    break;
                case 'F':
                    _Return = "Foxtrot";
                    break;
                case 'V':
                    _Return = "Victor";
                    break;
                case 'Q':
                    _Return = "Quebec";
                    break;
                case '5':
                    _Return = "5";
                    break;
                case '3':
                    _Return = "3";
                    break;
                case 'L':
                    _Return = "Lima";
                    break;
                case 'B':
                    _Return = "Bravo";
                    break;
                case 'T':
                    _Return = "Tango";
                    break;
            }
            return _Return;
        }
    }
}
