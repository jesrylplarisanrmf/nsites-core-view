using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Systems;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Inventorys;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class SystemConfigurationUI : Form
    {
        #region "VARIABLES"
        public GlobalVariables.Operation lOperation;
        SystemConfiguration loSystemConfiguration;
        Hashtable lSystemConfigHash;
        Location loLocation;
        ChartOfAccount loChartOfAccount;
        CryptorEngine loCryptorEngine;
        string lCompanyLogo = "";
        string lBannerImage = "";
        string lReportLogo = "";
        string lScreenSaverImage = "";
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SystemConfigurationUI()
        {
            InitializeComponent();
            lSystemConfigHash = new Hashtable();
            loSystemConfiguration = new SystemConfiguration();
            loCryptorEngine = new CryptorEngine();
            loLocation = new Location();
            loChartOfAccount = new ChartOfAccount();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"


        #region "METHODS"
        private void loadDataToHash()
        {
            try
            {
                lSystemConfigHash.Clear();
                lSystemConfigHash.Add("ApplicationName", lblApplicationName.Text);
                lSystemConfigHash.Add("VersionNo", lblVersionNo.Text);
                lSystemConfigHash.Add("DevelopedBy", lblDevelopedBy.Text);
                lSystemConfigHash.Add("CompanyName", txtCompanyName.Text);
                lSystemConfigHash.Add("CompanyAddress", txtCompanyAddress.Text);
                lSystemConfigHash.Add("ContactNumber", txtContactNumber.Text);
                lSystemConfigHash.Add("CompanyLogo", lCompanyLogo);
                lSystemConfigHash.Add("BannerImage", lBannerImage);
                lSystemConfigHash.Add("ReportLogo", lReportLogo);
                lSystemConfigHash.Add("PrimaryColor", txtPrimaryColor.Text);
                lSystemConfigHash.Add("SecondaryColor", txtSecondaryColor.Text);
                lSystemConfigHash.Add("FormBackColor", txtFormBackColor.Text);
                lSystemConfigHash.Add("DisplayRecordLimit", nudRecordLimit.Value.ToString());
                lSystemConfigHash.Add("CurrentFinancialYear", txtCurrentFinancialYear.Text);
                GlobalVariables.CurrentFinancialYear = txtCurrentFinancialYear.Text;
                lSystemConfigHash.Add("ReviewedBy", txtReviewedBy.Text);
                lSystemConfigHash.Add("ApprovedBy", txtApprovedBy.Text);
                try
                {
                    lSystemConfigHash.Add("SODebitAccount", cboSODebitAccount.SelectedValue.ToString());
                }
                catch 
                {
                    lSystemConfigHash.Add("SODebitAccount", ""); 
                }
                try
                {
                    lSystemConfigHash.Add("SOCreditAccount", cboSOCreditAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("SOCreditAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("PODebitAccount", cboPODebitAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("PODebitAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("POCreditAccount", cboPOCreditAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("POCreditAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("CRDebitAccount", cboCRDebitAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CRDebitAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("CRCreditAccount", cboCRCreditAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CRCreditAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("CDDebitAccount", cboCDDebitAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CDDebitAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("CDCreditAccount", cboCDCreditAccount.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CDCreditAccount", "");
                }
                try
                {
                    lSystemConfigHash.Add("CashierPeriodDebit", cboCashierPeriodDebit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CashierPeriodDebit", "");
                }
                try
                {
                    lSystemConfigHash.Add("CashierPeriodCredit", cboCashierPeriodCredit.SelectedValue.ToString());
                }
                catch
                {
                    lSystemConfigHash.Add("CashierPeriodCredit", "");
                }

                lSystemConfigHash.Add("TechnicalSupportEmailAddress", txtTechnicalSupportEmailAddress.Text);

                lSystemConfigHash.Add("EmailAddress", txtEmailAddress.Text);
                lSystemConfigHash.Add("EmailPassword", txtEmailPassword.Text);
                lSystemConfigHash.Add("OverridePassword", txtOverridePassword.Text);
                lSystemConfigHash.Add("ScreenSaverImage", lScreenSaverImage);
                if (rdbBottom.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Bottom");
                }
                else if (rdbLeft.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Left");
                }
                else if (rdbRight.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Right");
                }
                else if (rdbTop.Checked)
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Top");
                }
                else
                {
                    lSystemConfigHash.Add("MDITabAlignment", "Top");
                }

                writeLocalsystemConfig();
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
                            try
                            {
                                cboCurrentLocation.SelectedValue = data2[1].ToString();
                            }
                            catch { }
                        }
                        else if (data2[0].ToString() == "CurrentBranchId")
                        {
                            try
                            {
                                cboCurrentBranch.SelectedValue = data2[1].ToString();
                            }
                            catch { }
                        }
                        else if (data2[0].ToString() == "CurrentConnection")
                        {
                            try
                            {
                                cboCurrentConnection.Text = data2[1].ToString();
                            }
                            catch { }
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

        public void writeSystemSettings()
        {
            try
            {
                string _currentLocationId = "";
                string _currentBranchId = "";
                try
                {
                    _currentLocationId = cboCurrentLocation.SelectedValue.ToString();
                }
                catch { }
                try
                {
                    _currentBranchId = cboCurrentBranch.SelectedValue.ToString();
                }
                catch { }
                string _StringToWrite = "CurrentLocationId:" + _currentLocationId +
                    ";CurrentBranchId:" + _currentBranchId + 
                    ";CurrentConnection:" + cboCurrentConnection.Text;
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/SystemSettings.txt");
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

        public void refresh()
        {
            try
            {
                DataTable _dt = new DataTable();
                _dt = loSystemConfiguration.getAllData();
                foreach (DataRow _dr in _dt.Rows)
                {
                    try
                    {
                        if (_dr["Key"].ToString() == "ApplicationName")
                        {
                            lblApplicationName.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "VersionNo")
                        {
                            lblVersionNo.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "DevelopedBy")
                        {
                            lblDevelopedBy.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyName")
                        {
                            txtCompanyName.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyAddress")
                        {
                            txtCompanyAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "ContactNumber")
                        {
                            txtContactNumber.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CompanyLogo")
                        {
                            try
                            {
                                lCompanyLogo = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lCompanyLogo);
                                pctCompanyLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctCompanyLogo.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "BannerImage")
                        {
                            try
                            {
                                lBannerImage = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lBannerImage);
                                pctBannerImage.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctBannerImage.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "ReportLogo")
                        {
                            try
                            {
                                lReportLogo = _dr["Value"].ToString();
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lReportLogo);
                                pctReportLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctReportLogo.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "PrimaryColor")
                        {
                            txtPrimaryColor.Text = _dr["Value"].ToString();
                            txtPrimaryColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtPrimaryColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "SecondaryColor")
                        {
                            txtSecondaryColor.Text = _dr["Value"].ToString();
                            txtSecondaryColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtSecondaryColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "FormBackColor")
                        {
                            txtFormBackColor.Text = _dr["Value"].ToString();
                            txtFormBackColor.BackColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                            txtFormBackColor.ForeColor = Color.FromArgb(int.Parse(_dr["Value"].ToString()));
                        }
                        else if (_dr["Key"].ToString() == "DisplayRecordLimit")
                        {
                            nudRecordLimit.Value = int.Parse(_dr["Value"].ToString());
                        }
                        else if (_dr["Key"].ToString() == "EmailAddress")
                        {
                            txtEmailAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "EmailPassword")
                        {
                            txtEmailPassword.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CurrentFinancialYear")
                        {
                            txtCurrentFinancialYear.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "ReviewedBy")
                        {
                            txtReviewedBy.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "ApprovedBy")
                        {
                            txtApprovedBy.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "SODebitAccount")
                        {
                            cboSODebitAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "SOCreditAccount")
                        {
                            cboSOCreditAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "PODebitAccount")
                        {
                            cboPODebitAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "POCreditAccount")
                        {
                            cboPOCreditAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CRDebitAccount")
                        {
                            cboCRDebitAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CRCreditAccount")
                        {
                            cboCRCreditAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CDDebitAccount")
                        {
                            cboCDDebitAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CDCreditAccount")
                        {
                            cboCDCreditAccount.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CashierPeriodDebit")
                        {
                            cboCashierPeriodDebit.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "CashierPeriodCredit")
                        {
                            cboCashierPeriodCredit.SelectedValue = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "TechnicalSupportEmailAddress")
                        {
                            txtTechnicalSupportEmailAddress.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "OverridePassword")
                        {
                            txtOverridePassword.Text = _dr["Value"].ToString();
                        }
                        else if (_dr["Key"].ToString() == "ScreenSaverImage")
                        {
                            lScreenSaverImage = _dr["Value"].ToString();
                            try
                            {
                                byte[] hextobyte = GlobalFunctions.HexToBytes(lScreenSaverImage);
                                pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                                pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                        }
                        else if (_dr["Key"].ToString() == "MDITabAlignment")
                        {
                            if (_dr["Value"].ToString() == "Top")
                            {
                                rdbTop.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Bottom")
                            {
                                rdbBottom.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Left")
                            {
                                rdbLeft.Checked = true;
                            }
                            else if (_dr["Value"].ToString() == "Right")
                            {
                                rdbRight.Checked = true;
                            }
                            else
                            {
                                rdbTop.Checked = true;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                readLocalSystemConfig();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void writeLocalsystemConfig()
        {
            try
            {
                string _StringToWrite = "PrinterName=" + txtPrinterName.Text + ";POSType=" + cboPOSType.Text + ";BackupPath=" + txtBackupPath.Text + ";ORSize=" + cboORSize.Text +
                    ";Terminal=" + txtTerminal.Text + ";TouchScreen=" + (chkTouchMode.Checked ? "Y" : "N") + ";PreviewOR=" + (chkPreviewOR.Checked ? "Y" : "N") + ";PreviewSlip=" + (chkPreviewSlip.Checked ? "Y" : "N");
                _StringToWrite = loCryptorEngine.EncryptString(_StringToWrite);
                System.IO.TextWriter writeFile = new StreamWriter(".../Main/text/localSystemConfig.txt");
                writeFile.WriteLine(_StringToWrite);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        case "PrinterName": txtPrinterName.Text = data2[1].ToString(); break;
                        case "POSType": cboPOSType.Text = data2[1].ToString(); break;
                        case "BackupPath": txtBackupPath.Text = data2[1].ToString(); break;
                        case "Terminal": txtTerminal.Text = data2[1].ToString(); break;
                        case "ORSize": cboORSize.Text = data2[1].ToString(); break;
                        case "TouchScreen": chkTouchMode.Checked = (data2[1].ToString() == "Y" ? true : false); break;
                        case "PreviewOR": chkPreviewOR.Checked = (data2[1].ToString() == "Y" ? true : false); break;
                        case "PreviewSlip": chkPreviewSlip.Checked = (data2[1].ToString() == "Y" ? true : false); break;
                    }
                }
            }
            readFile.Close();
            readFile = null;
        }
        #endregion "END OF METHODS"

        #region "EVENTS"
        private void SystemConfigurationUI_Load(object sender, EventArgs e)
        {
            try
            {
                cboCurrentLocation.DataSource = loLocation.getAllData("ViewAll", "", "");
                cboCurrentLocation.DisplayMember = "Description";
                cboCurrentLocation.ValueMember = "Id";
                cboCurrentLocation.SelectedIndex = 0;

                cboSODebitAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboSODebitAccount.DisplayMember = "Account Title";
                cboSODebitAccount.ValueMember = "Id";
                cboSODebitAccount.SelectedIndex = -1;

                cboSOCreditAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboSOCreditAccount.DisplayMember = "Account Title";
                cboSOCreditAccount.ValueMember = "Id";
                cboSOCreditAccount.SelectedIndex = -1;

                cboPODebitAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboPODebitAccount.DisplayMember = "Account Title";
                cboPODebitAccount.ValueMember = "Id";
                cboPODebitAccount.SelectedIndex = -1;

                cboPOCreditAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboPOCreditAccount.DisplayMember = "Account Title";
                cboPOCreditAccount.ValueMember = "Id";
                cboPOCreditAccount.SelectedIndex = -1;

                cboCRDebitAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCRDebitAccount.DisplayMember = "Account Title";
                cboCRDebitAccount.ValueMember = "Id";
                cboCRDebitAccount.SelectedIndex = -1;

                cboCRCreditAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCRCreditAccount.DisplayMember = "Account Title";
                cboCRCreditAccount.ValueMember = "Id";
                cboCRCreditAccount.SelectedIndex = -1;

                cboCDDebitAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCDDebitAccount.DisplayMember = "Account Title";
                cboCDDebitAccount.ValueMember = "Id";
                cboCDDebitAccount.SelectedIndex = -1;

                cboCDCreditAccount.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCDCreditAccount.DisplayMember = "Account Title";
                cboCDCreditAccount.ValueMember = "Id";
                cboCDCreditAccount.SelectedIndex = -1;

                cboCashierPeriodDebit.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCashierPeriodDebit.DisplayMember = "Account Title";
                cboCashierPeriodDebit.ValueMember = "Id";
                cboCashierPeriodDebit.SelectedIndex = -1;

                cboCashierPeriodCredit.DataSource = loChartOfAccount.getAllData("ViewAll", "", "");
                cboCashierPeriodCredit.DisplayMember = "Account Title";
                cboCashierPeriodCredit.ValueMember = "Id";
                cboCashierPeriodCredit.SelectedIndex = -1;
            }
            catch { }
            try
            {
                refresh();
                readSystemSettings();
            }
            catch
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmSystemConfiguration", "Save"))
                {
                    return;
                }

                lOperation = GlobalVariables.Operation.Edit;
                loadDataToHash();
                foreach (DictionaryEntry Hash in lSystemConfigHash)
                {
                    try
                    {
                        loSystemConfiguration.Key = Hash.Key.ToString();
                        loSystemConfiguration.Value = Hash.Value.ToString();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                    loSystemConfiguration.saveSystemConfiguration(lOperation);

                }
                writeSystemSettings();
                MessageBoxUI _mb = new MessageBoxUI("System Configuration has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                _mb.ShowDialog();
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
                ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindScreenSaver_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/ScreenSaverImages/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lScreenSaverImage = GlobalFunctions.ToHex(m_Bitmap);

                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFindScreenSaver_Click");
                em.ShowDialog();
                return;
            }
        }

        #endregion "END OF EVENTS"

        private void btnFindCompanyLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lCompanyLogo = GlobalFunctions.ToHex(m_Bitmap);

                    pctCompanyLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctCompanyLogo.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindBannerImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lBannerImage = GlobalFunctions.ToHex(m_Bitmap);

                    pctBannerImage.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctBannerImage.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindReportLogo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFD = new OpenFileDialog();
                openFD.InitialDirectory = ".../Main/Images/";
                openFD.Title = "Insert an Image";
                openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
                if (openFD.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                }
                else
                {
                    string _ChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;

                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(_ChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lReportLogo = GlobalFunctions.ToHex(m_Bitmap);

                    pctReportLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctReportLogo.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnFind_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFindPrimaryColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtPrimaryColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtPrimaryColor.BackColor = clrColorDialog.Color;
                txtPrimaryColor.ForeColor = clrColorDialog.Color;
            }
        }

        private void btnFindSecondaryColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtSecondaryColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtSecondaryColor.BackColor = clrColorDialog.Color;
                txtSecondaryColor.ForeColor = clrColorDialog.Color;
            }
        }

        private void btnFindFormBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog clrColorDialog = new ColorDialog();
            if (clrColorDialog.ShowDialog() == DialogResult.OK)
            {
                txtFormBackColor.Text = clrColorDialog.Color.ToArgb().ToString();
                txtFormBackColor.BackColor = clrColorDialog.Color;
                txtFormBackColor.ForeColor = clrColorDialog.Color;
            }
        }
    }
}
