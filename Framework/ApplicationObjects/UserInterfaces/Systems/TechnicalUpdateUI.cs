using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Generics;
using System.IO;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class TechnicalUpdateUI : Form
    {
        Common loCommon;
        
        public TechnicalUpdateUI()
        {
            InitializeComponent();
            loCommon = new Common();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void TechnicalUpdateUI_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(int.Parse(GlobalVariables.FormBackColor));
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

        private void btnCheckDatabaseTestProd_Click(object sender, EventArgs e)
        {
            try
            {
                #region "BASE ADDRESSES"
                string _baseAddressTest = null;
                string _baseAddressProd = null;

                //get test base address
                try
                {
                    string filename = @"BaseAddress-TestServer.txt";
                    TextReader readFile = new StreamReader(filename);
                    _baseAddressTest = readFile.ReadLine();
                    readFile.Close();
                    readFile = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //get prod base address
                try
                {
                    string filename = @"BaseAddress-ProductionServer.txt";
                    TextReader readFile = new StreamReader(filename);
                    _baseAddressProd = readFile.ReadLine();
                    readFile.Close();
                    readFile = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion

                dgvTestProdTables.Rows.Clear();
                dgvTestProdMenuItems.Rows.Clear();
                dgvTestProdItemRights.Rows.Clear();
                dgvTestProdSystemConfiguration.Rows.Clear();
                
                #region "TABLES"
                foreach (DataRow _dr in loCommon.getTableDetails(_baseAddressTest).Rows)
                {
                    int n = dgvTestProdTables.Rows.Add();
                    dgvTestProdTables.Rows[n].Cells[0].Value = _dr["Table_Name"].ToString();
                    dgvTestProdTables.Rows[n].Cells[1].Value = _dr["Column_Name"].ToString();
                    dgvTestProdTables.Rows[n].Cells[2].Value = _dr["Column_Type"].ToString();
                }

                int i = 0;
                int error = 0;
                string _status;
                foreach (DataRow _dr in loCommon.getTableDetails(_baseAddressProd).Rows)
                {
                    _status = "";
                    try
                    {
                        dgvTestProdTables.Rows[i].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvTestProdTables.Rows[i].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvTestProdTables.Rows[i].Cells[6].Value = _dr["Column_Type"].ToString();

                        if (dgvTestProdTables.Rows[i].Cells[0].Value.ToString() != _dr["Table_Name"].ToString())
                        {
                            _status = ";TableName";
                        }
                        if (dgvTestProdTables.Rows[i].Cells[1].Value.ToString() != _dr["Column_Name"].ToString())
                        {
                            _status = _status + ";Fields";
                        }
                        if (dgvTestProdTables.Rows[i].Cells[2].Value.ToString() != _dr["Column_Type"].ToString())
                        {
                            _status = _status + ";DataType";
                        }

                        if (_status != "")
                        {
                            error++;
                            dgvTestProdTables.Rows[i].Cells[7].Value = "Error!" + _status;
                        }
                        i++;
                    }
                    catch
                    {
                        int n = dgvTestProdTables.Rows.Add();
                        dgvTestProdTables.Rows[n].Cells[0].Value = "";
                        dgvTestProdTables.Rows[n].Cells[1].Value = "";
                        dgvTestProdTables.Rows[n].Cells[2].Value = "";
                        dgvTestProdTables.Rows[n].Cells[3].Value = "";
                        dgvTestProdTables.Rows[n].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvTestProdTables.Rows[n].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvTestProdTables.Rows[n].Cells[6].Value = _dr["Column_Type"].ToString();
                        dgvTestProdTables.Rows[n].Cells[7].Value = "Error!";
                        error++;
                    }
                }
                if (error > 0)
                {
                    lblTestProdTableError.ForeColor = Color.Red;
                }
                else
                {
                    lblTestProdTableError.ForeColor = Color.Black;
                }
                lblTestProdTableError.Text = "Table Error(s) : " + error.ToString();
                #endregion

                #region "MENU ITEMS"
                foreach (DataRow _dr in loCommon.getMenuItemDetails(_baseAddressTest).Rows)
                {
                    int n = dgvTestProdMenuItems.Rows.Add();
                    dgvTestProdMenuItems.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvTestProdMenuItems.Rows[n].Cells[1].Value = _dr[1].ToString();
                    dgvTestProdMenuItems.Rows[n].Cells[2].Value = _dr[2].ToString();
                    dgvTestProdMenuItems.Rows[n].Cells[3].Value = _dr[3].ToString();
                    dgvTestProdMenuItems.Rows[n].Cells[4].Value = _dr[4].ToString();
                    dgvTestProdMenuItems.Rows[n].Cells[5].Value = _dr[5].ToString();
                }

                int iMenu = 0;
                int errorMenu = 0;
                string _statusMenu;
                foreach (DataRow _dr in loCommon.getMenuItemDetails(_baseAddressProd).Rows)
                {
                    _statusMenu = "";
                    try
                    {
                        dgvTestProdMenuItems.Rows[iMenu].Cells[7].Value = _dr[0].ToString();
                        dgvTestProdMenuItems.Rows[iMenu].Cells[8].Value = _dr[1].ToString();
                        dgvTestProdMenuItems.Rows[iMenu].Cells[9].Value = _dr[2].ToString();
                        dgvTestProdMenuItems.Rows[iMenu].Cells[10].Value = _dr[3].ToString();
                        dgvTestProdMenuItems.Rows[iMenu].Cells[11].Value = _dr[4].ToString();
                        dgvTestProdMenuItems.Rows[iMenu].Cells[12].Value = _dr[5].ToString();

                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusMenu = ";MenuName";
                        }
                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusMenu = _statusMenu + ";MenuText";
                        }
                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[2].Value.ToString() != _dr[2].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemName";
                        }
                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[3].Value.ToString() != _dr[3].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemText";
                        }
                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[4].Value.ToString() != _dr[4].ToString())
                        {
                            _statusMenu = _statusMenu + ";MenuSeqNo";
                        }
                        if (dgvTestProdMenuItems.Rows[iMenu].Cells[5].Value.ToString() != _dr[5].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemSeqNo";
                        }

                        if (_statusMenu != "")
                        {
                            errorMenu++;
                            dgvTestProdMenuItems.Rows[iMenu].Cells[13].Value = "Error!" + _statusMenu;
                        }
                        iMenu++;
                    }
                    catch
                    {
                        int n = dgvTestProdMenuItems.Rows.Add();
                        dgvTestProdMenuItems.Rows[n].Cells[0].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[1].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[2].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[3].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[4].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[5].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[6].Value = "";
                        dgvTestProdMenuItems.Rows[n].Cells[7].Value = _dr[0].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[8].Value = _dr[1].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[9].Value = _dr[2].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[10].Value = _dr[3].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[11].Value = _dr[4].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[12].Value = _dr[5].ToString();
                        dgvTestProdMenuItems.Rows[n].Cells[13].Value = "Error!";
                        errorMenu++;
                    }
                }
                if (errorMenu > 0)
                {
                    lblTestProdMenuItem.ForeColor = Color.Red;
                }
                else
                {
                    lblTestProdMenuItem.ForeColor = Color.Black;
                }
                lblTestProdMenuItem.Text = "Menu Item Error(s) : " + errorMenu.ToString();
                #endregion

                #region "ITEM RIGHTS"
                foreach (DataRow _dr in loCommon.getItemRightDetails(_baseAddressTest).Rows)
                {
                    int n = dgvTestProdItemRights.Rows.Add();
                    dgvTestProdItemRights.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvTestProdItemRights.Rows[n].Cells[1].Value = _dr[1].ToString();
                    dgvTestProdItemRights.Rows[n].Cells[2].Value = _dr[2].ToString();
                }

                int iRight = 0;
                int errorRight = 0;
                string _statusRight;
                foreach (DataRow _dr in loCommon.getItemRightDetails(_baseAddressProd).Rows)
                {
                    _statusRight = "";
                    try
                    {
                        dgvTestProdItemRights.Rows[iRight].Cells[4].Value = _dr[0].ToString();
                        dgvTestProdItemRights.Rows[iRight].Cells[5].Value = _dr[1].ToString();
                        dgvTestProdItemRights.Rows[iRight].Cells[6].Value = _dr[2].ToString();

                        if (dgvTestProdItemRights.Rows[iRight].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusRight = ";ItemName";
                        }
                        if (dgvTestProdItemRights.Rows[iRight].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusRight = _statusRight + ";Rights";
                        }
                        if (dgvTestProdItemRights.Rows[iRight].Cells[2].Value.ToString() != _dr[2].ToString())
                        {
                            _statusRight = _statusRight + ";RightSeqNo";
                        }

                        if (_statusRight != "")
                        {
                            errorRight++;
                            dgvTestProdItemRights.Rows[iRight].Cells[7].Value = "Error!" + _statusRight;
                        }
                        iRight++;
                    }
                    catch
                    {
                        int n = dgvTestProdItemRights.Rows.Add();
                        dgvTestProdItemRights.Rows[n].Cells[0].Value = "";
                        dgvTestProdItemRights.Rows[n].Cells[1].Value = "";
                        dgvTestProdItemRights.Rows[n].Cells[2].Value = "";
                        dgvTestProdItemRights.Rows[n].Cells[3].Value = "";
                        dgvTestProdItemRights.Rows[n].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvTestProdItemRights.Rows[n].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvTestProdItemRights.Rows[n].Cells[6].Value = _dr["Column_Type"].ToString();
                        dgvTestProdItemRights.Rows[n].Cells[7].Value = "Error!";
                        errorRight++;
                    }
                }
                if (errorRight > 0)
                {
                    lblTestProdItemRightError.ForeColor = Color.Red;
                }
                else
                {
                    lblTestProdItemRightError.ForeColor = Color.Black;
                }
                lblTestProdItemRightError.Text = "Item Rights Error(s) : " + errorRight.ToString();
                #endregion
                
                #region "SYSTEM CONFIGURATIONS"
                foreach (DataRow _dr in loCommon.getSystemConfigurationDetails(_baseAddressTest).Rows)
                {
                    int n = dgvTestProdSystemConfiguration.Rows.Add();
                    dgvTestProdSystemConfiguration.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvTestProdSystemConfiguration.Rows[n].Cells[1].Value = _dr[1].ToString();
                }

                int iConfig = 0;
                int errorConfig = 0;
                string _statusConfig;
                foreach (DataRow _dr in loCommon.getSystemConfigurationDetails(_baseAddressProd).Rows)
                {
                    _statusConfig = "";
                    try
                    {
                        dgvTestProdSystemConfiguration.Rows[iConfig].Cells[3].Value = _dr[0].ToString();
                        dgvTestProdSystemConfiguration.Rows[iConfig].Cells[4].Value = _dr[1].ToString();

                        if (dgvTestProdSystemConfiguration.Rows[iConfig].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusConfig = ";Key";
                        }
                        if (dgvTestProdSystemConfiguration.Rows[iConfig].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusConfig = _statusConfig + ";Value";
                        }

                        if (_statusConfig != "")
                        {
                            errorConfig++;
                            dgvTestProdSystemConfiguration.Rows[iConfig].Cells[5].Value = "Error!" + _statusConfig;
                        }
                        iConfig++;
                    }
                    catch
                    {
                        int n = dgvTestProdSystemConfiguration.Rows.Add();
                        dgvTestProdSystemConfiguration.Rows[n].Cells[0].Value = "";
                        dgvTestProdSystemConfiguration.Rows[n].Cells[1].Value = "";
                        dgvTestProdSystemConfiguration.Rows[n].Cells[2].Value = "";
                        dgvTestProdSystemConfiguration.Rows[n].Cells[3].Value = _dr[0].ToString();
                        dgvTestProdSystemConfiguration.Rows[n].Cells[4].Value = _dr[1].ToString();
                        dgvTestProdSystemConfiguration.Rows[n].Cells[5].Value = "Error!";
                        errorConfig++;
                    }
                }
                if (errorConfig > 0)
                {
                    lblTestProdSystemConfiguration.ForeColor = Color.Red;
                }
                else
                {
                    lblTestProdSystemConfiguration.ForeColor = Color.Black;
                }
                lblTestProdSystemConfiguration.Text = "System Configuration Error(s) : " + errorConfig.ToString();
                #endregion
                
                MessageBoxUI ms = new MessageBoxUI("TEST AND PRODUCTION records has been loaded successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                ms.showDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCheckDatabaseTestProd_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnCheckDatabaseDevTest_Click(object sender, EventArgs e)
        {
            try
            {
                #region "BASE ADDRESSES"

                string _baseAddressDev = null;
                string _baseAddressTest = null;
                //get dev base address
                try
                {
                    string filename = @"BaseAddress-DevelopmentServer.txt";
                    TextReader readFile = new StreamReader(filename);
                    _baseAddressDev = readFile.ReadLine();
                    readFile.Close();
                    readFile = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //get test base address
                try
                {
                    string filename = @"BaseAddress-TestServer.txt";
                    TextReader readFile = new StreamReader(filename);
                    _baseAddressTest = readFile.ReadLine();
                    readFile.Close();
                    readFile = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                #endregion

                dgvDevTestTables.Rows.Clear();
                dgvDevTestMenuItems.Rows.Clear();
                dgvDevTestItemRights.Rows.Clear();
                dgvDevTestSystemConfiguration.Rows.Clear();

                #region "TABLES"
                foreach (DataRow _dr in loCommon.getTableDetails(_baseAddressDev).Rows)
                {
                    int n = dgvDevTestTables.Rows.Add();
                    dgvDevTestTables.Rows[n].Cells[0].Value = _dr["Table_Name"].ToString();
                    dgvDevTestTables.Rows[n].Cells[1].Value = _dr["Column_Name"].ToString();
                    dgvDevTestTables.Rows[n].Cells[2].Value = _dr["Column_Type"].ToString();
                }
              
                int i = 0;
                int error = 0;
                string _status;
                foreach (DataRow _dr in loCommon.getTableDetails(_baseAddressTest).Rows)
                {
                    _status = "";
                    try
                    {
                        dgvDevTestTables.Rows[i].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvDevTestTables.Rows[i].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvDevTestTables.Rows[i].Cells[6].Value = _dr["Column_Type"].ToString();

                        if (dgvDevTestTables.Rows[i].Cells[0].Value.ToString() != _dr["Table_Name"].ToString())
                        {
                            _status = ";TableName";
                        }
                        if (dgvDevTestTables.Rows[i].Cells[1].Value.ToString() != _dr["Column_Name"].ToString())
                        {
                            _status = _status + ";Fields";
                        }
                        if (dgvDevTestTables.Rows[i].Cells[2].Value.ToString() != _dr["Column_Type"].ToString())
                        {
                            _status = _status + ";DataType";
                        }

                        if (_status != "")
                        {
                            error++;
                            dgvDevTestTables.Rows[i].Cells[7].Value = "Error!" + _status;
                        }
                        i++;
                    }
                    catch
                    {
                        int n = dgvDevTestTables.Rows.Add();
                        dgvDevTestTables.Rows[n].Cells[0].Value = "";
                        dgvDevTestTables.Rows[n].Cells[1].Value = "";
                        dgvDevTestTables.Rows[n].Cells[2].Value = "";
                        dgvDevTestTables.Rows[n].Cells[3].Value = "";
                        dgvDevTestTables.Rows[n].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvDevTestTables.Rows[n].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvDevTestTables.Rows[n].Cells[6].Value = _dr["Column_Type"].ToString();
                        dgvDevTestTables.Rows[n].Cells[7].Value = "Error!";
                        error++;
                    }
                }
                if (error > 0)
                {
                    lblTableErrorsDevTest.ForeColor = Color.Red;
                }
                else
                {
                    lblTableErrorsDevTest.ForeColor = Color.Black;
                }
                lblTableErrorsDevTest.Text = "Table Error(s) : " + error.ToString();
                #endregion "END OF TABLES"

                #region "MENU ITEMS"
                foreach (DataRow _dr in loCommon.getMenuItemDetails(_baseAddressDev).Rows)
                {
                    int n = dgvDevTestMenuItems.Rows.Add();
                    dgvDevTestMenuItems.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvDevTestMenuItems.Rows[n].Cells[1].Value = _dr[1].ToString();
                    dgvDevTestMenuItems.Rows[n].Cells[2].Value = _dr[2].ToString();
                    dgvDevTestMenuItems.Rows[n].Cells[3].Value = _dr[3].ToString();
                    dgvDevTestMenuItems.Rows[n].Cells[4].Value = _dr[4].ToString();
                    dgvDevTestMenuItems.Rows[n].Cells[5].Value = _dr[5].ToString();
                }
                
                int iMenu = 0;
                int errorMenu = 0;
                string _statusMenu;
                foreach (DataRow _dr in loCommon.getMenuItemDetails(_baseAddressTest).Rows)
                {
                    _statusMenu = "";
                    try
                    {
                        dgvDevTestMenuItems.Rows[iMenu].Cells[7].Value = _dr[0].ToString();
                        dgvDevTestMenuItems.Rows[iMenu].Cells[8].Value = _dr[1].ToString();
                        dgvDevTestMenuItems.Rows[iMenu].Cells[9].Value = _dr[2].ToString();
                        dgvDevTestMenuItems.Rows[iMenu].Cells[10].Value = _dr[3].ToString();
                        dgvDevTestMenuItems.Rows[iMenu].Cells[11].Value = _dr[4].ToString();
                        dgvDevTestMenuItems.Rows[iMenu].Cells[12].Value = _dr[5].ToString();

                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusMenu = ";MenuName";
                        }
                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusMenu = _statusMenu + ";MenuText";
                        }
                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[2].Value.ToString() != _dr[2].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemName";
                        }
                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[3].Value.ToString() != _dr[3].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemText";
                        }
                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[4].Value.ToString() != _dr[4].ToString())
                        {
                            _statusMenu = _statusMenu + ";MenuSeqNo";
                        }
                        if (dgvDevTestMenuItems.Rows[iMenu].Cells[5].Value.ToString() != _dr[5].ToString())
                        {
                            _statusMenu = _statusMenu + ";ItemSeqNo";
                        }

                        if (_statusMenu != "")
                        {
                            errorMenu++;
                            dgvDevTestMenuItems.Rows[iMenu].Cells[13].Value = "Error!" + _statusMenu;
                        }
                        iMenu++;
                    }
                    catch
                    {
                        int n = dgvDevTestMenuItems.Rows.Add();
                        dgvDevTestMenuItems.Rows[n].Cells[0].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[1].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[2].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[3].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[4].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[5].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[6].Value = "";
                        dgvDevTestMenuItems.Rows[n].Cells[7].Value = _dr[0].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[8].Value = _dr[1].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[9].Value = _dr[2].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[10].Value = _dr[3].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[11].Value = _dr[4].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[12].Value = _dr[5].ToString();
                        dgvDevTestMenuItems.Rows[n].Cells[13].Value = "Error!";
                        errorMenu++;
                    }
                }
                if (errorMenu > 0)
                {
                    lblMenuItemErrorsDevTest.ForeColor = Color.Red;
                }
                else
                {
                    lblMenuItemErrorsDevTest.ForeColor = Color.Black;
                }
                lblMenuItemErrorsDevTest.Text = "Menu Item Error(s) : " + errorMenu.ToString();
                #endregion
                
                #region "ITEM RIGHTS"
                foreach (DataRow _dr in loCommon.getItemRightDetails(_baseAddressDev).Rows)
                {
                    int n = dgvDevTestItemRights.Rows.Add();
                    dgvDevTestItemRights.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvDevTestItemRights.Rows[n].Cells[1].Value = _dr[1].ToString();
                    dgvDevTestItemRights.Rows[n].Cells[2].Value = _dr[2].ToString();
                }
                
                int iRight = 0;
                int errorRight = 0;
                string _statusRight;
                foreach (DataRow _dr in loCommon.getItemRightDetails(_baseAddressTest).Rows)
                {
                    _statusRight = "";
                    try
                    {
                        dgvDevTestItemRights.Rows[iRight].Cells[4].Value = _dr[0].ToString();
                        dgvDevTestItemRights.Rows[iRight].Cells[5].Value = _dr[1].ToString();
                        dgvDevTestItemRights.Rows[iRight].Cells[6].Value = _dr[2].ToString();

                        if (dgvDevTestItemRights.Rows[iRight].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusRight = ";ItemName";
                        }
                        if (dgvDevTestItemRights.Rows[iRight].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusRight = _statusRight + ";Rights";
                        }
                        if (dgvDevTestItemRights.Rows[iRight].Cells[2].Value.ToString() != _dr[2].ToString())
                        {
                            _statusRight = _statusRight + ";RightSeqNo";
                        }

                        if (_statusRight != "")
                        {
                            errorRight++;
                            dgvDevTestItemRights.Rows[iRight].Cells[7].Value = "Error!" + _statusRight;
                        }
                        iRight++;
                    }
                    catch
                    {
                        int n = dgvDevTestItemRights.Rows.Add();
                        dgvDevTestItemRights.Rows[n].Cells[0].Value = "";
                        dgvDevTestItemRights.Rows[n].Cells[1].Value = "";
                        dgvDevTestItemRights.Rows[n].Cells[2].Value = "";
                        dgvDevTestItemRights.Rows[n].Cells[3].Value = "";
                        dgvDevTestItemRights.Rows[n].Cells[4].Value = _dr["Table_Name"].ToString();
                        dgvDevTestItemRights.Rows[n].Cells[5].Value = _dr["Column_Name"].ToString();
                        dgvDevTestItemRights.Rows[n].Cells[6].Value = _dr["Column_Type"].ToString();
                        dgvDevTestItemRights.Rows[n].Cells[7].Value = "Error!";
                        errorRight++;
                    }
                }
                if (errorRight > 0)
                {
                    lblItemRightErrorsDevTest.ForeColor = Color.Red;
                }
                else
                {
                    lblItemRightErrorsDevTest.ForeColor = Color.Black;
                }
                lblItemRightErrorsDevTest.Text = "Item Rights Error(s) : " + errorRight.ToString();
                #endregion

                #region "SYSTEM CONFIGURATIONS"
                foreach (DataRow _dr in loCommon.getSystemConfigurationDetails(_baseAddressDev).Rows)
                {
                    int n = dgvDevTestSystemConfiguration.Rows.Add();
                    dgvDevTestSystemConfiguration.Rows[n].Cells[0].Value = _dr[0].ToString();
                    dgvDevTestSystemConfiguration.Rows[n].Cells[1].Value = _dr[1].ToString();
                }

                int iConfig = 0;
                int errorConfig = 0;
                string _statusConfig;
                foreach (DataRow _dr in loCommon.getSystemConfigurationDetails(_baseAddressTest).Rows)
                {
                    _statusConfig = "";
                    try
                    {
                        dgvDevTestSystemConfiguration.Rows[iConfig].Cells[3].Value = _dr[0].ToString();
                        dgvDevTestSystemConfiguration.Rows[iConfig].Cells[4].Value = _dr[1].ToString();

                        if (dgvDevTestSystemConfiguration.Rows[iConfig].Cells[0].Value.ToString() != _dr[0].ToString())
                        {
                            _statusConfig = ";Key";
                        }
                        if (dgvDevTestSystemConfiguration.Rows[iConfig].Cells[1].Value.ToString() != _dr[1].ToString())
                        {
                            _statusConfig = _statusConfig + ";Value";
                        }

                        if (_statusConfig != "")
                        {
                            errorConfig++;
                            dgvDevTestSystemConfiguration.Rows[iConfig].Cells[5].Value = "Error!" + _statusConfig;
                        }
                        iConfig++;
                    }
                    catch
                    {
                        int n = dgvDevTestSystemConfiguration.Rows.Add();
                        dgvDevTestSystemConfiguration.Rows[n].Cells[0].Value = "";
                        dgvDevTestSystemConfiguration.Rows[n].Cells[1].Value = "";
                        dgvDevTestSystemConfiguration.Rows[n].Cells[2].Value = "";
                        dgvDevTestSystemConfiguration.Rows[n].Cells[3].Value = _dr[0].ToString();
                        dgvDevTestSystemConfiguration.Rows[n].Cells[4].Value = _dr[1].ToString();
                        dgvDevTestSystemConfiguration.Rows[n].Cells[5].Value = "Error!";
                        errorConfig++;
                    }
                }
                if (errorConfig > 0)
                {
                    lblSystemConfigurationErrorsDevTest.ForeColor = Color.Red;
                }
                else
                {
                    lblSystemConfigurationErrorsDevTest.ForeColor = Color.Black;
                }
                lblSystemConfigurationErrorsDevTest.Text = "System Configuration Error(s) : " + errorConfig.ToString();
                #endregion
                
                MessageBoxUI ms = new MessageBoxUI("DEVELOPMENT AND TEST records has been loaded successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                ms.showDialog();
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCheckDatabaseDevTest_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {

        }
    }
}
