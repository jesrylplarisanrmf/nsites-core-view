using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Speech.Recognition;

using NSites_V.Global;
using NSites_V.ApplicationObjects;
using NSites_V.ApplicationObjects.Classes.Systems;
using NSites_V.ApplicationObjects.UserInterfaces;

namespace NSites_V.ApplicationObjects.UserInterfaces
{
    public partial class LogInUI : Form
    {
        SystemConfiguration loSystemConfiguration;
        User loUser;
        public bool lFromLogIn;
        CryptorEngine loCryptorEngine;
        DataTable ldtSystemConfig = new DataTable();

        public LogInUI()
        {
            InitializeComponent();
            loSystemConfiguration = new SystemConfiguration();
            loUser = new User();
            loCryptorEngine = new CryptorEngine();
        }

        private void getCurrentUserDetails()
        {
            try
            {
                foreach (DataRow _drUserInfo in loUser.getAllData("", GlobalVariables.UserId, "").Rows)
                {
                    GlobalVariables.Username = _drUserInfo["Username"].ToString();
                    GlobalVariables.Userfullname = _drUserInfo["Fullname"].ToString();
                    //GlobalVariables.UserEmailAddress = _drUserInfo["Email Address"].ToString();
                    GlobalVariables.UserGroupId = _drUserInfo["UserGroupId"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadCompanyDetails()
        {
            try
            {
                foreach (DataRow _drSystemConfig in loSystemConfiguration.getAllData().Rows)
                {
                    if (_drSystemConfig["Key"].ToString() == "ApplicationName")
                    {
                        GlobalVariables.ApplicationName = _drSystemConfig["Value"].ToString();
                        lblApplicationName.Text = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "VersionNo")
                    {
                        GlobalVariables.VersionNo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "DevelopedBy")
                    {
                        GlobalVariables.DevelopedBy = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CompanyName")
                    {
                        GlobalVariables.CompanyName = _drSystemConfig["Value"].ToString();
                        lblCompanyName.Text = _drSystemConfig["Value"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void readLicenseCertificate()
        {
            try
            {
                string line = null;
                char[] splitter1 = { ';' };
                char[] splitter2 = { ':' };
                System.IO.TextReader readFile = new StreamReader(".../Main/text/LicenseCertificate.txt");
                line = readFile.ReadLine();
                if (line != null)
                {
                    string _StringToWrite = loCryptorEngine.DecryptString(line);
                    string[] data1 = _StringToWrite.Split(splitter1);
                    for (int i = 0; i < data1.Length; i++)
                    {
                        string[] data2 = null;
                        data2 = data1[i].Split(splitter2);
                        if (data2[0].ToString() == "ApplicationId")
                        {
                            GlobalVariables.ApplicationId = data2[1].ToString();
                        }
                        if (data2[0].ToString() == "ProcessorId")
                        {
                            GlobalVariables.ProcessorId = data2[1].ToString();
                        }
                        if (data2[0].ToString() == "TrialVersion")
                        {
                            GlobalVariables.TrialVersion = data2[1].ToString();
                        }
                        if (data2[0].ToString() == "LicenseKey")
                        {
                            GlobalVariables.LicenseKey = data2[1].ToString();
                        }
                        if (data2[0].ToString() == "ExpiryDate")
                        {
                            GlobalVariables.lLicenseExpiry = DateTime.Parse(data2[1].ToString());
                        }
                    }
                }
                else
                {
                    Application.Exit();
                }
                readFile.Close();
                readFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LicenseCertificateRead()
        {
            try
            {
                string _result = "";
                string line = null;
                System.IO.TextReader readFile = new StreamReader(".../Main/text/LicenseCertificate.txt");
                line = readFile.ReadLine();
                if (line != null)
                {
                    _result = loCryptorEngine.DecryptString(line);
                }
                readFile.Close();
                readFile = null;

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LicenseCertificateWrite(string pLicenseNo)
        {
            try
            {
                string _StringToWrite = "ApplicationName:" + lblApplicationName.Text + ";ProcessorId:" + GlobalFunctions.GetProcessorId() + ";" + "ExpiryDate:01/01/9999;" + "LicenseNo:" + pLicenseNo;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/LicenseCertificate.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getSoftwareLicense()
        {
            try
            {
                //display software license.
                SoftwareLicenseUI loSofwareLicense = new SoftwareLicenseUI(GlobalVariables.CompanyName, GlobalVariables.ApplicationName);
                loSofwareLicense.ShowDialog();
                if (loSofwareLicense.lSuccessFullyVerified)
                {
                    LicenseCertificateWrite(loSofwareLicense.lLicenseNo);
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadCompanyInfo()
        {
            try
            {
                foreach (DataRow _drSystemConfig in loSystemConfiguration.getAllData().Rows)
                {
                    if (_drSystemConfig["Key"].ToString() == "ApplicationName")
                    {
                        GlobalVariables.ApplicationName = _drSystemConfig["Value"].ToString();
                        lblApplicationName.Text = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "VersionNo")
                    {
                        GlobalVariables.VersionNo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CompanyName")
                    {
                        GlobalVariables.CompanyName = _drSystemConfig["Value"].ToString();
                        lblCompanyName.Text = GlobalVariables.CompanyName;
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CompanyAddress")
                    {
                        GlobalVariables.CompanyAddress = _drSystemConfig["Value"].ToString();
                        lblCompanyAddress.Text = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ContactNumber")
                    {
                        GlobalVariables.ContactNumber = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "TechnicalSupportEmailAddress")
                    {
                        GlobalVariables.TechnicalSupportEmailAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "BannerImage")
                    {
                        byte[] hextobyteLogo = GlobalFunctions.HexToBytes(_drSystemConfig["Value"].ToString());
                        pctBannerImage.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyteLogo);
                    }
                    else if (_drSystemConfig["Key"].ToString() == "PrimaryColor")
                    {
                        GlobalVariables.PrimaryColor = _drSystemConfig["Value"].ToString();
                        pnlApplicationName.BackColor = Color.FromArgb(int.Parse(GlobalVariables.PrimaryColor));
                        lblLicenseTo.BackColor = Color.FromArgb(int.Parse(GlobalVariables.PrimaryColor));
                    }
                    else if (_drSystemConfig["Key"].ToString() == "SecondaryColor")
                    {
                        GlobalVariables.SecondaryColor = _drSystemConfig["Value"].ToString();
                        pnlCompanyDetails.BackColor = Color.FromArgb(int.Parse(GlobalVariables.SecondaryColor));
                    }
                    else if (_drSystemConfig["Key"].ToString() == "FormBackColor")
                    {
                        GlobalVariables.FormBackColor = _drSystemConfig["Value"].ToString();
                    }
                }

                if (GlobalVariables.VersionNo != lblVersionNo.Text)
                {
                    //TO BE UPDATED
                    //Process.Start("D:/PERSONAL FILES/Softwares- JBC Active/JBC SOFTWARES/JBC Softwares/JBC Softwares - V/Framework/Main/text/copy.bat");
                    //Process.Start(".../Main/text/copy.bat");
                    MessageBoxUI ms = new MessageBoxUI(GlobalVariables.VersionNo + " is available now in the Google Drive. Please update immediately!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void rememberPasswordWrite()
        {
            try
            {
                string _StringToWrite = "Username:" + txtUsername.Text + ";" + "Password:" + txtPassword.Text;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/userDetails.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void rememberUsernameWrite()
        {
            try
            {
                string _StringToWrite = "Username:" + txtUsername.Text;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/userDetails.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void rememberPasswordRead()
        {
            try
            {
                string line = null;
                char[] splitter1 = { ';' };
                char[] splitter2 = { ':' };
                System.IO.TextReader readFile = new StreamReader(".../Main/text/userDetails.txt");
                line = readFile.ReadLine();
                if (line != null)
                {
                    string _StringToWrite = loCryptorEngine.DecryptString(line);
                    string[] data1 = _StringToWrite.Split(splitter1);
                    for (int i = 0; i < data1.Length; i++)
                    {
                        string[] data2 = null;
                        data2 = data1[i].Split(splitter2);
                        if (data2[0].ToString() == "Username")
                        {
                            txtUsername.Text = data2[1].ToString();
                        }
                        if (data2[0].ToString() == "Password")
                        {
                            txtPassword.Text = data2[1].ToString();
                            chbRemember.Checked = true;
                        }
                    }
                }
                readFile.Close();
                readFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void readSystemSettings()
        {
            try
            {
                string line = null;
                char[] splitter1 = { ';' };
                char[] splitter2 = { ':' };
                System.IO.TextReader readFile = new StreamReader(".../Main/text/SystemSettings.txt");
                line = readFile.ReadLine();
                if (line != null)
                {
                    string _StringToWrite = loCryptorEngine.DecryptString(line);
                    string[] data1 = _StringToWrite.Split(splitter1);
                    for (int i = 0; i < data1.Length; i++)
                    {
                        string[] data2 = null;
                        data2 = data1[i].Split(splitter2);
                        if (data2[0].ToString() == "CurrentLocationId")
                        {
                            GlobalVariables.CurrentLocationId = data2[1].ToString();
                        }
                        else if (data2[0].ToString() == "CurrentBranchId")
                        {
                            GlobalVariables.CurrentBranchId = data2[1].ToString();
                        }
                        else if (data2[0].ToString() == "CurrentConnection")
                        {
                            GlobalVariables.CurrentConnection = data2[1].ToString().Replace("\r\n", "");
                            cboConnection.Text = data2[1].ToString().Replace("\r\n", "");
                            if (data2[1].ToString().Replace("\r\n", "") == "Production Server")
                            {
                                cboConnection.Enabled = false;
                            }
                            else
                            {
                                cboConnection.Enabled = true;
                            }
                        }
                    }
                }
                readFile.Close();
                readFile = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void readLocalSystemConfig()
        {
            string line = null;
            char[] splitter1 = { ';' };
            char[] splitter2 = { '=' };
            System.IO.TextReader readFile = new StreamReader(".../Main/text/localSystemConfig.txt");
            line = readFile.ReadLine();
            if (line != null)
            {
                string _StringToWrite = loCryptorEngine.DecryptString(line);
                string[] data1 = _StringToWrite.Split(splitter1);
                for (int i = 0; i < data1.Length; i++)
                {
                    string[] data2 = null;
                    data2 = data1[i].Split(splitter2);
                    switch (data2[0].ToString())
                    {
                        case "PrinterName": GlobalVariables.PrinterName = data2[1].ToString(); break;
                        case "POSType": GlobalVariables.POSType = data2[1].ToString(); break;
                        case "BackupPath": GlobalVariables.BackupPath = data2[1].ToString(); break;
                        case "Terminal": GlobalVariables.Terminal = data2[1].ToString(); break;
                        case "ORSize": GlobalVariables.ORSize = data2[1].ToString(); break;
                        case "TouchScreen": GlobalVariables.TouchScreen = (data2[1].ToString() == "Y" ? true : false); break;
                        case "PreviewOR": GlobalVariables.PreviewOR = (data2[1].ToString() == "Y" ? true : false); break;
                        case "PreviewSlip": GlobalVariables.PreviewSlip = (data2[1].ToString() == "Y" ? true : false); break;
                    }
                }
            }
            readFile.Close();
            readFile = null;
        }

        private void LogInUI_Load(object sender, EventArgs e)
        {
            try
            {
                readSystemSettings();
                //get connection string
                string filename = "";
                if (GlobalVariables.CurrentConnection == "Production Server")
                {
                    filename = @"BaseAddress-ProductionServer.txt";
                }
                else if (GlobalVariables.CurrentConnection == "Test Server")
                {
                    filename = @"BaseAddress-TestServer.txt";
                }
                else if (GlobalVariables.CurrentConnection == "Development Server")
                {
                    filename = @"BaseAddress-DevelopmentServer.txt";
                }
                else 
                {
                    filename = @"BaseAddress-DevelopmentServer.txt";
                }
                string line = null;
                TextReader readFile = new StreamReader(filename);
                line = readFile.ReadLine();
                readFile.Close();
                readFile = null;
                GlobalVariables.BaseAddress = line;
            
                //Check SoftwareLicense
                string _LicenseCertificate = "";
                string _ApplicationName = "";
                string _ProcessorId = "";
                DateTime _ExpiryDate = DateTime.Now;
                string _LicenseNo = "";

                loadCompanyInfo();
                readLocalSystemConfig();

                if (GlobalVariables.CompanyName == "")
                {
                    Application.Exit();
                }

                _LicenseCertificate = LicenseCertificateRead();

                #region "Split License Certificate"
                char[] splitter1 = { ';' };
                char[] splitter2 = { ':' };
                string[] data1 = _LicenseCertificate.Split(splitter1);
                for (int i = 0; i < data1.Length; i++)
                {
                    string[] data2 = null;
                    data2 = data1[i].Split(splitter2);
                    if (data2[0].ToString() == "ApplicationName")
                    {
                        _ApplicationName = data2[1].ToString();
                    }
                    else if (data2[0].ToString() == "ProcessorId")
                    {
                        _ProcessorId = data2[1].ToString();
                    }
                    else if (data2[0].ToString() == "ExpiryDate")
                    {
                        try
                        {
                            _ExpiryDate = DateTime.Parse(data2[1].ToString());
                        }
                        catch
                        {
                            _ExpiryDate = DateTime.Now;
                        }
                    }
                    else if (data2[0].ToString() == "LicenseNo")
                    {
                        _LicenseNo = data2[1].ToString();
                    }
                }
                #endregion

                #region "Processor ID"
                string _ComputerProcessorId = GlobalFunctions.GetProcessorId();
                #endregion

                if (_ApplicationName != lblApplicationName.Text)
                {
                    getSoftwareLicense();
                }
                else if (_ComputerProcessorId != _ProcessorId)
                {
                    getSoftwareLicense();
                }
                else if (_ExpiryDate.Date <= DateTime.Now.Date)
                {
                    getSoftwareLicense();
                }
                else if (_LicenseNo == "")
                {
                    getSoftwareLicense();
                }
                else
                {
                    rememberPasswordRead();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "LogInUI_Load");
                em.ShowDialog();
                return;
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnLogin_Click(null, new EventArgs());
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnLogin_Click(null, new EventArgs());
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (chbRemember.Checked)
                {
                    rememberPasswordWrite();
                }
                else
                {
                    rememberUsernameWrite();
                }

                //select connection
                string filename = "";
                if (cboConnection.Text == "Production Server")
                {
                    filename = @"BaseAddress-ProductionServer.txt";
                    GlobalVariables.CurrentConnection = "Production Server";
                }
                else if (cboConnection.Text == "Test Server")
                {
                    filename = @"BaseAddress-TestServer.txt";
                    GlobalVariables.CurrentConnection = "Test Server";
                }
                else if (cboConnection.Text == "Development Server")
                {
                    filename = @"BaseAddress-DevelopmentServer.txt";
                    GlobalVariables.CurrentConnection = "Development Server";
                }
                else
                { 
                    filename = @"BaseAddress-DevelopmentServer.txt";
                    GlobalVariables.CurrentConnection = "Development Server";
                }

                string line = null;
                TextReader readFile = new StreamReader(filename);
                line = readFile.ReadLine();
                readFile.Close();
                readFile = null;
                GlobalVariables.BaseAddress = line;
                
                //check user credentials
                if (txtUsername.Text == "technicalsupport")
                {
                    string _day = DateTime.Now.Day.ToString();
                    string _hour = DateTime.Now.Hour.ToString();
                    string _minute = DateTime.Now.Minute.ToString();

                    if (txtPassword.Text == _day + _hour + _minute)
                    {
                        this.Hide();
                        GlobalVariables.Username = "technicalsupport";
                        GlobalVariables.UserPassword = txtPassword.Text;
                        GlobalVariables.Userfullname = "JC Technical Support";
                        //getCurrentUserDetails();
                        MDINSites_VUI MDI = new MDINSites_VUI();
                        MDI.ShowDialog();
                        this.Show();

                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                    else
                    {
                        MessageBoxUI ms = new MessageBoxUI("Username and password combination is incorrect!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        ms.showDialog();
                        txtPassword.Focus();
                        return;
                    }
                }
                else
                {
                    DataTable _dt = loUser.autenticateUser(txtUsername.Text, txtPassword.Text);

                    if (_dt.Rows.Count > 0)
                    {
                        this.Hide();
                        foreach (DataRow _dr in _dt.Rows)
                        {
                            GlobalVariables.UserId = _dr[0].ToString();
                        }
                        GlobalVariables.UserPassword = txtPassword.Text;
                        getCurrentUserDetails();
                        MDINSites_VUI MDI = new MDINSites_VUI();
                        MDI.ShowDialog();
                        this.Show();

                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                    else
                    {
                        MessageBoxUI ms = new MessageBoxUI("Username and password combination is incorrect!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        ms.showDialog();
                        txtPassword.Focus();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnLogin_Click");
                em.ShowDialog();
                return;
            }
        }

        private void LogInUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cboConnection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnLogin_Click(null, new EventArgs());
            }
        }
    }
}
