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
using System.Speech.Synthesis;
using System.Xml;
using System.Net;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes;
using NSites_V.ApplicationObjects.Classes.Procurements;
using NSites_V.ApplicationObjects.Classes.Sales;
using NSites_V.ApplicationObjects.Classes.Inventorys;
using NSites_V.ApplicationObjects.Classes.Accountings;
using NSites_V.ApplicationObjects.Classes.Systems;

using NSites_V.ApplicationObjects.UserInterfaces;
using NSites_V.ApplicationObjects.UserInterfaces.Generics;

using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Procurements.Reports;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Sales.Reports;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Inventorys.Reports;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Transactions;
using NSites_V.ApplicationObjects.UserInterfaces.Accountings.Reports;

using NSites_V.ApplicationObjects.UserInterfaces.Systems;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Masterfiles;
using NSites_V.ApplicationObjects.UserInterfaces.Systems.Reports;

namespace NSites_V.ApplicationObjects.UserInterfaces
{
    public partial class MDINSites_VUI : Form
    {
        #region "VARIABLES"
        UserGroup loUserGroup;
        DataView ldvUserGroup;
        DataTable ldtUserGroup;
        SystemConfiguration loSystemConfiguration;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public MDINSites_VUI()
        {
            InitializeComponent();
            loUserGroup = new UserGroup();
            ldtUserGroup = new DataTable();
            loSystemConfiguration = new SystemConfiguration();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        private void disabledMenuStrip()
        {
            try
            {
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    item.Enabled = false;
                    foreach (ToolStripItem subitem in item.DropDownItems)
                    {
                        if (subitem is ToolStripMenuItem)
                        {
                            subitem.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void enabledMenuStrip()
        {
            try
            {
                ldtUserGroup = loUserGroup.getUserGroupMenuItems();

                GlobalVariables.DVRights = new DataView(loUserGroup.getUserGroupRights());
                ldvUserGroup = new DataView(ldtUserGroup);
                foreach (ToolStripMenuItem item in mnsNSites_V.Items)
                {
                    try
                    {
                        ldvUserGroup.RowFilter = "Menu = '" + item.Name + "'";
                    }
                    catch { }
                    if (ldvUserGroup.Count != 0)
                    {
                        item.Enabled = true;
                        processMenuItems(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void processMenuItems(ToolStripMenuItem pitem)
        {
            try
            {
                if (true)
                {
                    pitem.Enabled = true;
                }

                foreach (ToolStripItem subitem in pitem.DropDownItems)
                {
                    if (subitem is ToolStripMenuItem)
                    {
                        ldvUserGroup.RowFilter = "Item = '" + subitem.Name + "'";
                        if (ldvUserGroup.Count != 0)
                        {
                            subitem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int displayControlOnTab(Control pControl, TabPage pTabPage)
        {
            try
            {
                // The tabpage.
                Form _FormControl = new Form();
                _FormControl = (Form)pControl;

                // Add to the tab control.
                pTabPage.Text = _FormControl.Text;
                pTabPage.Name = _FormControl.Name;
                tbcNSites_V.TabPages.Add(pTabPage);
                tbcNSites_V.SelectTab(pTabPage);
                _FormControl.TopLevel = false;
                _FormControl.Parent = this;
                _FormControl.Dock = DockStyle.Fill;
                _FormControl.FormBorderStyle = FormBorderStyle.None;
                pTabPage.Controls.Add(_FormControl);
                tbcNSites_V.SelectTab(tbcNSites_V.SelectedIndex);
                _FormControl.Show();
                return tbcNSites_V.SelectedIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeTabPage()
        {
            try
            {
                tbcNSites_V.TabPages.RemoveAt(tbcNSites_V.SelectedIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void changeHomeImage()
        {
            try
            {
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    pctScreenSaver.BackgroundImage = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getGlobalVariablesData()
        {
            try
            {
                foreach (DataRow _drSystemConfig in loSystemConfiguration.getAllData().Rows)
                {
                    if (_drSystemConfig["Key"].ToString() == "CompanyLogo")
                    {
                        GlobalVariables.CompanyLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReportLogo")
                    {
                        GlobalVariables.ReportLogo = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "DisplayRecordLimit")
                    {
                        GlobalVariables.DisplayRecordLimit = int.Parse(_drSystemConfig["Value"].ToString());
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailAddress")
                    {
                        GlobalVariables.EmailAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EmailPassword")
                    {
                        GlobalVariables.EmailPassword = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CurrentFinancialYear")
                    {
                        GlobalVariables.CurrentFinancialYear = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ReviewedBy")
                    {
                        GlobalVariables.ReviewedBy = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ApprovedBy")
                    {
                        GlobalVariables.ApprovedBy = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "SODebitAccount")
                    {
                        GlobalVariables.SODebitAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "SOCreditAccount")
                    {
                        GlobalVariables.SOCreditAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "PODebitAccount")
                    {
                        GlobalVariables.PODebitAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "POCreditAccount")
                    {
                        GlobalVariables.POCreditAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CRDebitAccount")
                    {
                        GlobalVariables.CRDebitAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CRCreditAccount")
                    {
                        GlobalVariables.CRCreditAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CDDebitAccount")
                    {
                        GlobalVariables.CDDebitAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CDCreditAccount")
                    {
                        GlobalVariables.CDCreditAccount = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CashierPeriodDebit")
                    {
                        GlobalVariables.CashierPeriodDebit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "CashierPeriodCredit")
                    {
                        GlobalVariables.CashierPeriodCredit = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "RetainedEarningsCode")
                    {
                        GlobalVariables.RetainedEarningsCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "IncomeAndExpenseSummaryCode")
                    {
                        GlobalVariables.IncomeAndExpenseSummaryCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "AssetClassificationCode")
                    {
                        GlobalVariables.AssetClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "LiabilityClassificationCode")
                    {
                        GlobalVariables.LiabilityClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "EquityClassificationCode")
                    {
                        GlobalVariables.EquityClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "IncomeClassificationCode")
                    {
                        GlobalVariables.IncomeClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ExpensesClassificationCode")
                    {
                        GlobalVariables.ExpensesClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "IncomeAndExpenseSummaryClassificationCode")
                    {
                        GlobalVariables.IncomeAndExpenseSummaryClassificationCode = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "OverridePassword")
                    {
                        GlobalVariables.OverridePassword = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "ScreenSaverImage")
                    {
                        GlobalVariables.ScreenSaverImage = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "MDITabAlignment")
                    {
                        GlobalVariables.TabAlignment = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "BackupMySqlDumpAddress")
                    {
                        GlobalVariables.BackupMySqlDumpAddress = _drSystemConfig["Value"].ToString();
                    }
                    else if (_drSystemConfig["Key"].ToString() == "RestoreMySqlAddress")
                    {
                        GlobalVariables.RestoreMySqlAddress = _drSystemConfig["Value"].ToString();
                    }
                }

                //byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ReportLogo);
                GlobalVariables.DTCompanyLogo = GlobalFunctions.getReportLogo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion "END OF METHODS"

        #region "EVENTS"
        private void MDIFrameWork_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += " [" + GlobalVariables.CurrentConnection + "]";
                pnlMenu.BackColor = Color.FromArgb(int.Parse(GlobalVariables.SecondaryColor));
                getGlobalVariablesData();
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctScreenSaver.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctScreenSaver.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch { }
                try
                {
                    byte[] hextobyteLogo = GlobalFunctions.HexToBytes(GlobalVariables.CompanyLogo);
                    pctLogo.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyteLogo);
                }
                catch { }
                try
                {
                    switch (GlobalVariables.TabAlignment)
                    {
                        case "Top":
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                        case "Bottom":
                            tbcNSites_V.Alignment = TabAlignment.Bottom;
                            break;
                        case "Left":
                            tbcNSites_V.Alignment = TabAlignment.Left;
                            break;
                        case "Right":
                            tbcNSites_V.Alignment = TabAlignment.Right;
                            break;
                        default:
                            tbcNSites_V.Alignment = TabAlignment.Top;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                lblUsername.Text = "Welcome!  " + GlobalVariables.Userfullname;
                lblDateTime.Text = DateTime.Now.ToLongDateString();
                lblOwnerName.UseMnemonic = false;
                lblOwnerName.Text = GlobalVariables.CompanyName;
                lblApplicationName.Text = GlobalVariables.ApplicationName;
                if (GlobalVariables.Username != "admin" && GlobalVariables.Username != "technicalsupport")
                {
                    disabledMenuStrip();
                    enabledMenuStrip();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "MDIFrameWork_Load");
                em.ShowDialog();
                Application.Exit();
            }
        }

        private void tsmSystemConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "System Configuration")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SystemConfigurationUI _SystemConfiguration = new SystemConfigurationUI();
                TabPage _SystemConfigurationTab = new TabPage();
                _SystemConfigurationTab.ImageIndex = 1;
                _SystemConfiguration.ParentList = this;
                displayControlOnTab(_SystemConfiguration, _SystemConfigurationTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSystemConfiguration_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUser_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                User _User = new User();
                Type _Type = typeof(User);
                ListFormSystemUI _ListForm = new ListFormSystemUI((object)_User, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 2;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUser_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUserGroup_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "User Group List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                UserGroupListUI _UserGroupList = new UserGroupListUI();
                TabPage _UserGroupTab = new TabPage();
                _UserGroupTab.ImageIndex = 3;
                _UserGroupList.ParentList = this;
                displayControlOnTab(_UserGroupList, _UserGroupTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUserGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmChangeUserPassword_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Change User Password")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ChangeUserPasswordUI _ChangeUserPassword = new ChangeUserPasswordUI();
                TabPage _ChangeUserPasswordTab = new TabPage();
                _ChangeUserPasswordTab.ImageIndex = 4;
                _ChangeUserPassword.ParentList = this;
                displayControlOnTab(_ChangeUserPassword, _ChangeUserPasswordTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmChangeUserPassword_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLockScreen_Click(object sender, EventArgs e)
        {
            try
            {
                UnlockScreenUI _UnlockScreen = new UnlockScreenUI();
                _UnlockScreen.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLockScreen_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion "END OF EVENTS"

        private void tsmScreenSaver_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Screen Saver")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ScreenSaverUI _ScreenSaver = new ScreenSaverUI();
                TabPage _ScreenSaverTab = new TabPage();
                _ScreenSaverTab.ImageIndex = 5;
                _ScreenSaver.ParentList = this;
                displayControlOnTab(_ScreenSaver, _ScreenSaverTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmScreenSaver_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmAuditTrail_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Audit Trail")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                AuditTrailUI _AuditTrail = new AuditTrailUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 55;
                _AuditTrail.ParentList = this;
                displayControlOnTab(_AuditTrail, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmAuditTrail_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmChartOfAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "ChartOfAccount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ChartOfAccount _ChartOfAccount = new ChartOfAccount();
                Type _Type = typeof(ChartOfAccount);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_ChartOfAccount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 8;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmChartOfAccounts_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmMainAccount_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "MainAccount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                MainAccount _MainAccount = new MainAccount();
                Type _Type = typeof(MainAccount);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_MainAccount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 11;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmMainAccount_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmClassification_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Classification List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Classification _Classification = new Classification();
                Type _Type = typeof(Classification);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_Classification, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 9;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmClassification_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSubClassification_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "SubClassification List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SubClassification _SubClassification = new SubClassification();
                Type _Type = typeof(SubClassification);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_SubClassification, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 10;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSubClassification_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Supplier List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Supplier _Supplier = new Supplier();
                Type _Type = typeof(Supplier);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Supplier, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 14;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSupplier_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Customer List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Customer _Customer = new Customer();
                Type _Type = typeof(Customer);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Customer, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 13;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCustomer_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmGeneralJournal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "General Journal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                GeneralJournalUI _GeneralJournal = new GeneralJournalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 38;
                _GeneralJournal.ParentList = this;
                displayControlOnTab(_GeneralJournal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmGeneralJournal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCashReceiptJournal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Cash Receipt Journal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                CashReceiptJournalUI _CashReceiptJournal = new CashReceiptJournalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 36;
                _CashReceiptJournal.ParentList = this;
                displayControlOnTab(_CashReceiptJournal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCashReceiptJournal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmFinancialYearClosing_Click(object sender, EventArgs e)
        {
            try
            {
                FinancialYearClosingUI _FinancialYearClosing = new FinancialYearClosingUI();
                _FinancialYearClosing.ParentList = this;
                _FinancialYearClosing.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmFinancialYearClosing_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmTrialBalance_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Trial Balance")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                TrialBalanceUI _TrialBalance = new TrialBalanceUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 51;
                _TrialBalance.ParentList = this;
                displayControlOnTab(_TrialBalance, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmTrialBalance_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmWorkSheet_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Work Sheet")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                WorkSheetUI _WorkSheet = new WorkSheetUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 52;
                _WorkSheet.ParentList = this;
                displayControlOnTab(_WorkSheet, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmWorkSheet_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBank_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Bank List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Bank _Bank = new Bank();
                Type _Type = typeof(Bank);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_Bank, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 15;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBank_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSubsidiaryLedger_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Subsidiary Ledger")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SubsidiaryLedgerUI _SubsidiaryLedger = new SubsidiaryLedgerUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 50;
                _SubsidiaryLedger.ParentList = this;
                displayControlOnTab(_SubsidiaryLedger, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSubsidiaryLedger_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmFinancialYearOpening_Click(object sender, EventArgs e)
        {
            try
            {
                FinancialYearOpeningUI _FinancialYearOpening = new FinancialYearOpeningUI();
                _FinancialYearOpening.ParentList = this;
                _FinancialYearOpening.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmFinancialYearOpening_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesJournal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Sales Journal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SalesJournalUI _SalesJournal = new SalesJournalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 34;
                _SalesJournal.ParentList = this;
                displayControlOnTab(_SalesJournal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSalesJournal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCashDisbursementJournal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Cash Disbursement Journal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                CashDisbursementJournalUI _CashDisbursementJournal = new CashDisbursementJournalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 37;
                _CashDisbursementJournal.ParentList = this;
                displayControlOnTab(_CashDisbursementJournal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCashDisbursementJournal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmTechnicalUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //check technical support username and password
                if (GlobalVariables.Username == "technicalsupport")
                {
                    foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                    {
                        if (_tab.Text == "Technical Update")
                        {
                            tbcNSites_V.SelectedTab = _tab;
                            return;
                        }
                    }

                    TechnicalUpdateUI _TechnicalUpdate = new TechnicalUpdateUI();
                    TabPage _TechnicalUpdateTab = new TabPage();
                    _TechnicalUpdateTab.ImageIndex = 7;
                    _TechnicalUpdate.ParentList = this;
                    displayControlOnTab(_TechnicalUpdate, _TechnicalUpdateTab);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Only JC Technical Support can open this Function!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmTechnicalUpdate_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmUnit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Unit List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Unit _Unit = new Unit();
                Type _Type = typeof(Unit);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Unit, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 22;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmUnit_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmInventoryGroup_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "InventoryGroup List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                InventoryGroup _InventoryGroup = new InventoryGroup();
                Type _Type = typeof(InventoryGroup);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_InventoryGroup, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 19;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmInventoryGroup_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCategory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Category List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Category _Category = new Category();
                Type _Type = typeof(Category);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Category, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 20;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCategory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStock_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Stock _Stock = new Stock();
                Type _Type = typeof(Stock);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Stock, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 21;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStock_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmLocation_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Location List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Location _Location = new Location();
                Type _Type = typeof(Location);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_Location, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 24;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmLocation_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Adjustment")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockAdjustmentUI _StockAdjustment = new StockAdjustmentUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 33;
                _StockAdjustment.ParentList = this;
                displayControlOnTab(_StockAdjustment, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockAdjustment_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockReceiving_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Receiving")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockReceivingUI _StockReceiving = new StockReceivingUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 30;
                _StockReceiving.ParentList = this;
                displayControlOnTab(_StockReceiving, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockReceiving_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockWithdrawal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Withdrawal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockWithdrawalUI _StockWithdrawal = new StockWithdrawalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 29;
                _StockWithdrawal.ParentList = this;
                displayControlOnTab(_StockWithdrawal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockWithdrawal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPriceQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Price Quotation")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PriceQuotationUI _PriceQuotation = new PriceQuotationUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 25;
                _PriceQuotation.ParentList = this;
                displayControlOnTab(_PriceQuotation, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPriceQuotation_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPurchaseRequest_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Purchase Request")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PurchaseRequestUI _PurchaseRequest = new PurchaseRequestUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 27;
                _PurchaseRequest.ParentList = this;
                displayControlOnTab(_PurchaseRequest, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPurchaseRequest_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesOrder_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Sales Order")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SalesOrderUI _SalesOrder = new SalesOrderUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 26;
                _SalesOrder.ParentList = this;
                displayControlOnTab(_SalesOrder, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSalesOrder_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Purchase Order")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PurchaseOrderUI _PurchaseOrder = new PurchaseOrderUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 28;
                _PurchaseOrder.ParentList = this;
                displayControlOnTab(_PurchaseOrder, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPurchaseOrder_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockInventory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Inventory")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockInventoryUI _StockInventory = new StockInventoryUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 41;
                _StockInventory.ParentList = this;
                displayControlOnTab(_StockInventory, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockCard_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Card")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockCardUI _StockCard = new StockCardUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 42;
                _StockCard.ParentList = this;
                displayControlOnTab(_StockCard, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockCard_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesPerson_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "SalesPerson List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SalesPerson _SalesPerson = new SalesPerson();
                Type _Type = typeof(SalesPerson);
                ListFormSalesUI _ListForm = new ListFormSalesUI((object)_SalesPerson, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 18;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSalesPerson_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Discount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ProcurementDiscount _ProcurementDiscount = new ProcurementDiscount();
                Type _Type = typeof(ProcurementDiscount);
                ListFormSystemUI _ListForm = new ListFormSystemUI((object)_ProcurementDiscount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 16;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmDiscount_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmInventoryType_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "InventoryType List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                InventoryType _InventoryType = new InventoryType();
                Type _Type = typeof(InventoryType);
                ListFormInventoryUI _ListForm = new ListFormInventoryUI((object)_InventoryType, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 23;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmInventoryType_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockTransferOut_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Transfer Out")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockTransferOutUI _StockTransferOut = new StockTransferOutUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 31;
                _StockTransferOut.ParentList = this;
                displayControlOnTab(_StockTransferOut, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockTransferOut_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStockTransferIn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Stock Transfer In")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StockTransferInUI _StockTransferIn = new StockTransferInUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 32;
                _StockTransferIn.ParentList = this;
                displayControlOnTab(_StockTransferIn, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStockTransferIn_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmReorderLevel_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Reorder Level")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ReorderLevelUI _ReorderLevel = new ReorderLevelUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _ReorderLevel.ParentList = this;
                displayControlOnTab(_ReorderLevel, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmReorderLevel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmCheckIssuance_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Check Issuance")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                CheckIssuanceUI _CheckIssuance = new CheckIssuanceUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 47;
                _CheckIssuance.ParentList = this;
                displayControlOnTab(_CheckIssuance, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmCheckIssuance_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmStatementOfAccount_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Statement of Account")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                StatementOfAccountUI _StatementOfAccount = new StatementOfAccountUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 45;
                _StatementOfAccount.ParentList = this;
                displayControlOnTab(_StatementOfAccount, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmStatementOfAccount_Click");
                em.ShowDialog();
                return;
            }
        }
        
        private void tsmJournalEntry_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Journal Entry")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                JournalEntryUI _JournalEntry = new JournalEntryUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 48;
                _JournalEntry.ParentList = this;
                displayControlOnTab(_JournalEntry, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmJournalEntry_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnClearTab_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text != "Home")
                    {
                        tbcNSites_V.TabPages.Remove(_tab);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnClearTab_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPurchaseJournal_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Purchase Journal")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PurchaseJournalUI _PurchasesJournal = new PurchaseJournalUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 35;
                _PurchasesJournal.ParentList = this;
                displayControlOnTab(_PurchasesJournal, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPurchaseJournal_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmGeneralLedger_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "General Ledger")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                GeneralLedgerUI _GeneralLedger = new GeneralLedgerUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 49;
                _GeneralLedger.ParentList = this;
                displayControlOnTab(_GeneralLedger, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmGeneralLedger_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmIncomeStatement_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Income Statement")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                IncomeStatementUI _IncomeStatement = new IncomeStatementUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 54;
                _IncomeStatement.ParentList = this;
                displayControlOnTab(_IncomeStatement, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmIncomeStatement_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBalanceSheet_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Balance Sheet")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                BalanceSheetUI _BalanceSheet = new BalanceSheetUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 53;
                _BalanceSheet.ParentList = this;
                displayControlOnTab(_BalanceSheet, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBalanceSheet_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmProcurementDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Procurement Discount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                ProcurementDiscount _ProcurementDiscount = new ProcurementDiscount();
                Type _Type = typeof(ProcurementDiscount);
                ListFormProcurementUI _ListForm = new ListFormProcurementUI((object)_ProcurementDiscount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 14;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmProcurementDiscount_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "SalesDiscount List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SalesDiscount _SalesDiscount = new SalesDiscount();
                Type _Type = typeof(SalesDiscount);
                ListFormSalesUI _ListForm = new ListFormSalesUI((object)_SalesDiscount, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 16;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSalesDiscount_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSalesConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void tsmPurchaseInventory_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Purchase Inventory")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                PurchaseInventoryUI _PurchaseInventory = new PurchaseInventoryUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 43;
                _PurchaseInventory.ParentList = this;
                displayControlOnTab(_PurchaseInventory, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmiPurchaseInventory_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBuilding_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Building List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Building _Building = new Building();
                Type _Type = typeof(Building);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_Building, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 15;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBuilding_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmEquipment_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Equipment List")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                Equipment _Equipment = new Equipment();
                Type _Type = typeof(Equipment);
                ListFormAccountingUI _ListForm = new ListFormAccountingUI((object)_Equipment, _Type);
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 15;
                _ListForm.ParentList = this;
                displayControlOnTab(_ListForm, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmEquipment_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmPOPayableReport_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "P.O. Payable Report")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                POPayableReportUI _POPayableReport = new POPayableReportUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 46;
                _POPayableReport.ParentList = this;
                displayControlOnTab(_POPayableReport, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmPOPayableReport_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmSOReceivableReport_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "S.O. Receivable Report")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                SOReceivableReportUI _SOReceivableReport = new SOReceivableReportUI();
                TabPage _ListFormTab = new TabPage();
                _ListFormTab.ImageIndex = 44;
                _SOReceivableReport.ParentList = this;
                displayControlOnTab(_SOReceivableReport, _ListFormTab);
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmSOReceivableReport_Click");
                em.ShowDialog();
                return;
            }
        }

        private void tsmBackupRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage _tab in this.tbcNSites_V.TabPages)
                {
                    if (_tab.Text == "Database Backup/Restore")
                    {
                        tbcNSites_V.SelectedTab = _tab;
                        return;
                    }
                }

                DatabaseBackupRestoreUI loDatabaseBackupRestore = new DatabaseBackupRestoreUI();
                loDatabaseBackupRestore.ParentList = this;
                loDatabaseBackupRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "tsmBackupRestoreDatabase_Click");
                em.ShowDialog();
                return;
            }
        }
    }
}
