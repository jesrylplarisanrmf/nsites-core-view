using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NSites_V.Global;
using NSites_V.ApplicationObjects.Classes.Systems;

namespace NSites_V.ApplicationObjects.UserInterfaces.Systems
{
    public partial class ScreenSaverUI : Form
    {
        #region "VARIABLES"
        SystemConfiguration loSystemConfiguration;
        string lChosenFile = "";
        //string lTargetFile = "";
        string lScreenSaverImage = "";
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public ScreenSaverUI()
        {
            InitializeComponent();
            loSystemConfiguration = new SystemConfiguration();
        }
        #endregion "END OF CONSTRUCTORS"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "EVENTS"
        private void ScreenSaverUI_Load(object sender, EventArgs e)
        {
            try
            {
                byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                pctCurrentPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                pctCurrentPicture.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
                }
                catch
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnCancel_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmScreenSaver", "Find"))
                {
                    return;
                }
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
                    lChosenFile = openFD.FileName;
                    string _FileName = openFD.SafeFileName;
                    byte[] m_Bitmap = null;

                    FileStream fs = new FileStream(lChosenFile, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    int length = (int)br.BaseStream.Length;
                    m_Bitmap = new byte[length];
                    m_Bitmap = br.ReadBytes(length);
                    br.Close();
                    fs.Close();

                    lScreenSaverImage = GlobalFunctions.ToHex(m_Bitmap);

                    pctCurrentPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                    pctCurrentPicture.BackgroundImageLayout = ImageLayout.Stretch;

                    byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ScreenSaverImage);
                    pctPreviousPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pctPreviousPicture.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmScreenSaver", "Save"))
                {
                    return;
                }
                loSystemConfiguration.Value = lScreenSaverImage;
                loSystemConfiguration.Key = "ScreenSaverImage";
     
                if (loSystemConfiguration.saveSystemConfiguration(GlobalVariables.Operation.Edit))
                {
                    GlobalVariables.ScreenSaverImage = lScreenSaverImage;
                    MessageBoxUI ms = new MessageBoxUI("Screen Saver has been change successfully!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    ms.ShowDialog();
                    ParentList.GetType().GetMethod("changeHomeImage").Invoke(ParentList, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnSave_Click");
                em.ShowDialog();
                return;
            }
        }

        private void btnNoPicture_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFunctions.checkRights("tsmScreenSaver", "No Picture"))
                {
                    return;
                }
                lScreenSaverImage = "";
            }
            catch (Exception ex)
            {
                ErrorMessageUI em = new ErrorMessageUI(ex.Message, this.Name, "btnNoPicture_Click");
                em.ShowDialog();
                return;
            }
        }

        #endregion "END OF EVENTS"
    }
}
