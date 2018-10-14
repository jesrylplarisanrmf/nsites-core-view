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

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class DatabaseBackupRestoreUI : Form
    {
        Common loCommon;
        
        public DatabaseBackupRestoreUI()
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

        private void DatabaseBackupRestoreUI_Load(object sender, EventArgs e)
        {
            txtSaveFileTo.Text = GlobalVariables.BackupPath;
        }

        private void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmBackupRestoreDatabase", "Backup"))
                {
                    return;
                }

                if (loCommon.backupDatabase(txtSaveFileTo.Text, GlobalVariables.BackupMySqlDumpAddress, txtBackupUserId.Text, txtBackupPassword.Text, txtBackupServer.Text, txtBackupDatabase.Text))
                {
                    MessageBoxUI ms = new MessageBoxUI("Your database has been backup successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    this.Close();
                    //ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Failure to backup!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnBackupDatabase_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmBackupRestoreDatabase", "Restore"))
                {
                    return;
                }

                if (loCommon.restoreDatabase(txtSqlFileFrom.Text, GlobalVariables.RestoreMySqlAddress, txtRestoreUserId.Text, txtRestorePassword.Text, txtRestoreServer.Text, txtRestoreDatabase.Text))
                {
                    MessageBoxUI ms = new MessageBoxUI("Your database has been restore successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    this.Close();
                    //ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
                }
                else
                {
                    MessageBoxUI ms = new MessageBoxUI("Failure to restore!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    ms.showDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnRestoreDatabase_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnOpenRestoreFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.InitialDirectory = ".../Main/MySqlBackUp/";
            openFD.Title = "Insert an SQL file";
            openFD.Filter = "SQL Files|*.sql";
            if (openFD.ShowDialog() == DialogResult.Cancel)
            {
                MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            else
            {
                txtSqlFileFrom.Text = openFD.FileName;
            }
        }
    }
}
