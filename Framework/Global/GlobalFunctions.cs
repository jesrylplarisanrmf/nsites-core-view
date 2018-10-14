using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Speech.Synthesis;
using System.Net.Mail;

namespace NSites_V.Global
{
    public static class GlobalFunctions
    {
        public static DataTable convertDataGridToDataTable(DataGridView pDataGrid)
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in pDataGrid.Columns)
            {
                dt.Columns.Add(col.Name, typeof(string));
            }

            foreach (DataGridViewRow gridRow in pDataGrid.Rows)
            {
                if (gridRow.IsNewRow)
                {
                    continue;
                }
                DataRow dtRow = dt.NewRow();
                for (int i1 = 0; i1 < pDataGrid.Columns.Count; i1++)
                {
                    dtRow[i1] = (gridRow.Cells[i1].Value == null ? DBNull.Value : gridRow.Cells[i1].Value);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public static DataTable convertDataGridToReportViewerDataTable(DataGridView pDataGrid)
        {
            DataTable dt = new DataTable();

            int i = 1;
            int[] indexno = new int[8];
            int j = 0;
            foreach (DataGridViewColumn col in pDataGrid.Columns)
            {
                if (col.Visible)
                {
                    if (col.Name.Contains("Compensation") || col.Name.Contains("Salary") || col.Name.Contains("Total") ||
                        col.Name.Contains("Amount") || col.Name.Contains("Contribution") || col.Name.Contains("Range") ||
                        col.Name.Contains("Premium") || col.Name.Contains("Share") || 
                        col.Name.Contains("Rate") || col.Name.Equals(" Reorder Level") || col.Name == "Over" ||
                        col.Name == "But Not Over" || col.Name == "Base Tax" || col.Name == "Percent Over")
                    {
                        dt.Columns.Add("Column" + i, typeof(decimal));
                    }
                    else
                    {
                        dt.Columns.Add("Column" + i, typeof(string));
                    }
                    indexno[i-1] = j;
                    i++;
                    if (i > 8)
                    {
                        break;
                    }
                }
                
                j++;
            }
            foreach (DataGridViewRow gridRow in pDataGrid.Rows)
            {
                if (gridRow.IsNewRow)
                {
                    continue;
                }
                DataRow dtRow = dt.NewRow();
                for (int i1 = 0; i1 < i - 1; i1++)
                {
                    try
                    {
                        dtRow[i1] = (gridRow.Cells[indexno[i1]].Value == null ? DBNull.Value : gridRow.Cells[indexno[i1]].Value);
                    }
                    catch { }
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public static DataTable convertDataTableToReportDataTable(DataTable pDataTable, int pNoOfParameters)
        {
            int _rowCount = pDataTable.Columns.Count;
            for (int k = _rowCount; k < pNoOfParameters; k++)
            {
                pDataTable.Columns.Add("Column" + (k + 1), typeof(string));
            }

            for (int j = 0; j < pDataTable.Rows.Count; j++)
            {
                for (int i1 = _rowCount; i1 < pNoOfParameters; i1++)
                {
                    pDataTable.Rows[j][i1] = "";
                }
            }

            return pDataTable;
        }

        public static DataTable convertDataGridToDataTableNoComma(DataGridView pDataGrid)
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in pDataGrid.Columns)
            {
                dt.Columns.Add(col.Name, typeof(string));
            }

            foreach (DataGridViewRow gridRow in pDataGrid.Rows)
            {
                if (gridRow.IsNewRow)
                {
                    continue;
                }
                DataRow dtRow = dt.NewRow();
                for (int i1 = 0; i1 < pDataGrid.Columns.Count; i1++)
                {
                    if (gridRow.Cells[i1].Value.ToString() == ",")
                    {
                        dtRow[i1] = "";
                    }
                    else
                    {
                        dtRow[i1] = (gridRow.Cells[i1].Value == null ? DBNull.Value : gridRow.Cells[i1].Value);
                    }
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        public static DataTable getReportLogo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Image", System.Type.GetType("System.Byte[]"));
            byte[] hextobyte = GlobalFunctions.HexToBytes(GlobalVariables.ReportLogo);
            dt.Rows.Add(hextobyte);
            return dt;
        }

        public static string ToHex(this byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }

        public static byte[] HexToBytes(this string str)
        {
            if (str.Length == 0 || str.Length % 2 != 0)
                return new byte[0];

            byte[] buffer = new byte[str.Length / 2];
            char c;
            for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
            {
                // Convert first half of byte
                c = str[sx];
                buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

                // Convert second half of byte
                c = str[++sx];
                buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
            }

            return buffer;
        }

        public static string replaceChar(string pString)
        {
            if (pString != "")
            {
                return pString.Replace('\'', '`');
            }
            else
            {
                return "";
            }
        }

        public static void SendEmail(string pTo, string pCC, string pSubject, string pBody)
        {
            try
            {
                //OLD USING GMAIL SMTP
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("jbcsoftwares.info@gmail.com");
                mail.To.Add(pTo);
                if (pCC != "")
                {
                    mail.CC.Add(pCC);
                }
                mail.Subject = pSubject;
                mail.IsBodyHtml = true;
                mail.Body = pBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("jbcsoftwares.info@gmail.com", "jbcadmin12345678");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                MessageBoxUI mb = new MessageBoxUI("Email successfully send!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBoxUI mb = new MessageBoxUI(ex.ToString(), GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }

        }

        public static Image ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray != null)
            {
                MemoryStream ms = new MemoryStream(byteArray, 0,
                byteArray.Length);
                ms.Write(byteArray, 0, byteArray.Length);
                return Image.FromStream(ms, true);
            }
            return null;
        }

        public static DateTime ConvertToDate(string pDate)
        {
            string[] _date = pDate.ToString().Split('-');
            return DateTime.Parse(_date[2].ToString() + "-" + _date[0].ToString() + "-" + _date[1].ToString());
        }

        public static string GetDate(string pYear, string pMonth)
        {
            string _date = "";
            string _month = "";
            DateTime _testDate;
            switch (pMonth)
            { 
                case "January": _month = "01";
                    break;
                case "February": _month = "02";
                    break;
                case "March": _month = "03";
                    break;
                case "April": _month = "04";
                    break;
                case "May": _month = "05";
                    break;
                case "June": _month = "06";
                    break;
                case "July": _month = "07";
                    break;
                case "August": _month = "08";
                    break;
                case "September": _month = "09";
                    break;
                case "October": _month = "10";
                    break;
                case "November": _month = "11";
                    break;
                case "December": _month = "12";
                    break;
            }

            try
            {
                _testDate = DateTime.Parse(pYear + "-" + _month+"-31");
                _date = pYear + "-" + _month + "-31";
            }
            catch
            {
                try
                {
                    _testDate = DateTime.Parse(pYear + "-" + _month + "-30");
                    _date = pYear + "-" + _month + "-30";
                }
                catch
                {
                    try
                    {
                        _testDate = DateTime.Parse(pYear + "-" + _month + "-29");
                        _date = pYear + "-" + _month + "-29";
                    }
                    catch 
                    {
                        _testDate = DateTime.Parse(pYear + "-" + _month + "-28");
                        _date = pYear + "-" + _month + "-28";
                    }
                }
            }

            return _date;
        }

        public static void refreshGrid(ref DataGridView pDataGridView, DataTable pDataTable)
        {
            try
            {
                pDataGridView.Rows.Clear();
                pDataGridView.Columns.Clear();
            }
            catch
            {
                pDataGridView.DataSource = null;
            }

            for (int i = 0; i < pDataTable.Columns.Count; i++)
            {
                pDataGridView.Columns.Add(pDataTable.Columns[i].ColumnName, pDataTable.Columns[i].ColumnName);
            }

            foreach (DataRow _dr in pDataTable.Rows)
            {
                int n = pDataGridView.Rows.Add();
                if (n < GlobalVariables.DisplayRecordLimit)
                {
                    for (int i = 0; i < pDataTable.Columns.Count; i++)
                    {
                        pDataGridView.Rows[n].Cells[i].Value = _dr[i].ToString();
                    }
                }
                else
                {
                    pDataGridView.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    pDataGridView.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                    pDataGridView.Rows[n].Height = 5;
                    pDataGridView.Rows[n].ReadOnly = true;
                    break;
                }
            }
        }

        public static void refreshAll(ref DataGridView pDataGridView, DataTable pAllData)
        {
            try
            {
                pDataGridView.Rows.Clear();
                pDataGridView.Columns.Clear();
            }
            catch
            {
                pDataGridView.DataSource = null;
            }
            pDataGridView.DataSource = pAllData;
        }
       
        public static bool checkRights(string pItemName, string pRights)
        {
            bool _Status = true;
            if (!(GlobalVariables.Username == "admin") && !(GlobalVariables.Username == "technicalsupport"))
            {
                try
                {
                    GlobalVariables.DVRights.RowFilter = "ItemRights = '" + pItemName + pRights + "'";

                    if (GlobalVariables.DVRights.Count == 0)
                    {
                        MessageBoxUI mb = new MessageBoxUI("You don't have a rights to do this procedure!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                        mb.ShowDialog();
                        _Status = false;
                    }
                }
                catch 
                {
                    MessageBoxUI mb = new MessageBoxUI("You don't have a rights to do this procedure!", GlobalVariables.Icons.Warning, GlobalVariables.Buttons.OK);
                    mb.ShowDialog();
                    _Status = false;
                }
            }
            return _Status;
        }

        public static void displayReportPreview(DataGridView pDataGridView, DataTable pDataTable, string pTitle, string pSubTitle)
        {
            ReportDocument reportDocument;
            ParameterFields paramFields;

            ParameterField paramField;
            ParameterDiscreteValue paramDiscreteValue;

            reportDocument = new ReportDocument();
            paramFields = new ParameterFields();

            int columnNo = 0;

            for (int c = 0; c < pDataGridView.Columns.Count; c++)
            {
                if (columnNo > 7)
                {
                    break;
                }
                if (pDataGridView.Columns[c].Visible == true)
                {
                    columnNo++;
                    string _DisplayFields = pDataGridView.Columns[c].Name;

                    paramField = new ParameterField();
                    paramField.Name = "col" + columnNo.ToString();
                    paramDiscreteValue = new ParameterDiscreteValue();
                    paramDiscreteValue.Value = _DisplayFields;
                    paramField.CurrentValues.Add(paramDiscreteValue);
                    //Add the paramField to paramFields
                    paramFields.Add(paramField);
                }
            }

            for (int i = columnNo; i < 8; i++)
            {
                columnNo++;
                paramField = new ParameterField();
                paramField.Name = "col" + columnNo.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "";
                paramField.CurrentValues.Add(paramDiscreteValue);
                //Add the paramField to paramFields
                paramFields.Add(paramField);
            }
                
                if (pTitle == "Company")
                {
                    GlobalVariables.loReportPreviewer.crystalReportViewer.ParameterFieldInfo = paramFields;
                    GlobalVariables.loCrystalReport1.SetDataSource(pDataTable);
                    GlobalVariables.loCrystalReport1.Database.Tables[1].SetDataSource(GlobalVariables.DTCompanyLogo);
                    GlobalVariables.loCrystalReport1.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    GlobalVariables.loCrystalReport1.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    GlobalVariables.loCrystalReport1.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    GlobalVariables.loCrystalReport1.SetParameterValue("Username", GlobalVariables.Userfullname);
                    GlobalVariables.loCrystalReport1.SetParameterValue("Title", pTitle);
                    GlobalVariables.loCrystalReport1.SetParameterValue("SubTitle", pSubTitle);
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum1", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum2", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum3", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum4", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum5", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum6", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum7", "");
                    GlobalVariables.loCrystalReport1.SetParameterValue("sum8", "");
                    GlobalVariables.loReportPreviewer.crystalReportViewer.ReportSource = GlobalVariables.loCrystalReport1;
                    GlobalVariables.loReportPreviewer.ShowDialog();
                    GlobalVariables.loCrystalReport1.Close();
                }
                else
                {
                    GlobalVariables.loReportPreviewer.crystalReportViewer.ParameterFieldInfo = paramFields;
                    GlobalVariables.loCrystalReport.SetDataSource(pDataTable);
                    GlobalVariables.loCrystalReport.Database.Tables[1].SetDataSource(GlobalVariables.DTCompanyLogo);
                    GlobalVariables.loCrystalReport.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                    GlobalVariables.loCrystalReport.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                    GlobalVariables.loCrystalReport.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                    GlobalVariables.loCrystalReport.SetParameterValue("Username", GlobalVariables.Userfullname);
                    GlobalVariables.loCrystalReport.SetParameterValue("Title", pTitle);
                    GlobalVariables.loCrystalReport.SetParameterValue("SubTitle", pSubTitle);
                    GlobalVariables.loCrystalReport.SetParameterValue("sum1", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum2", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum3", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum4", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum5", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum6", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum7", "");
                    GlobalVariables.loCrystalReport.SetParameterValue("sum8", "");
                    GlobalVariables.loReportPreviewer.crystalReportViewer.ReportSource = GlobalVariables.loCrystalReport;
                    GlobalVariables.loReportPreviewer.ShowDialog();
                    GlobalVariables.loCrystalReport.Close();
                }
            }

        public static string NumberToCurrencyText(decimal number, MidpointRounding midpointRounding)
        {
            number = decimal.Round(number, 2, midpointRounding);
            string wordNumber = string.Empty;
            string[] arrNumber = number.ToString().Split('.');

            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);
            wordNumber = (wholePart == 0 ? "" : strWholePart) + (wholePart == 1 ? " Peso " : " Pesos ");
            if (arrNumber.Length > 1)
            {
                long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
                string strFarctionPart = NumberToText(fractionPart);
                if (fractionPart != 0)
                    wordNumber += "and " + strFarctionPart + (fractionPart == 1 ? " Cent Only." : " Cents Only.");
                else
                    wordNumber += "Only.";
            }
            else
                wordNumber += "Only.";

            return wordNumber;
        }

        private static string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }
            return wordNumber.ToString().Trim();
        }

        public static string GetProcessorId()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (System.Management.ManagementClass theClass = new System.Management.ManagementClass("Win32_Processor"))
                {
                    using (System.Management.ManagementObjectCollection theCollectionOfResults = theClass.GetInstances())
                    {
                        foreach (System.Management.ManagementObject currentResult in theCollectionOfResults)
                        {
                            sb.Append(currentResult["ProcessorID"].ToString());
                        }
                    }
                }
                return sb.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return "";
            }
        }

        public static string EncryptData(string pData)
        {
            string _ReturnData = "";
            string _LastChar = pData.Substring(pData.Length - 1, 1);

            foreach (Char c in pData)
            {
                _ReturnData += getValue(_LastChar, c.ToString());
            }

            return _ReturnData;
        }

        private static string getValue(string pLastChar, string pValue)
        {
            string returnValue = "";
            switch (pLastChar)
            {
                case "0":
                    #region "Last number end with 0"
                    switch (pValue)
                    {
                        case "A": returnValue = "L"; break;
                        case "B": returnValue = "K"; break;
                        case "C": returnValue = "R"; break;
                        case "D": returnValue = "C"; break;
                        case "E": returnValue = "M"; break;
                        case "F": returnValue = "1"; break;
                        case "G": returnValue = "8"; break;
                        case "H": returnValue = "0"; break;
                        case "I": returnValue = "Y"; break;
                        case "J": returnValue = "F"; break;
                        case "K": returnValue = "W"; break;
                        case "L": returnValue = "N"; break;
                        case "M": returnValue = "2"; break;
                        case "N": returnValue = "9"; break;
                        case "O": returnValue = "A"; break;
                        case "P": returnValue = "O"; break;
                        case "Q": returnValue = "J"; break;
                        case "R": returnValue = "E"; break;
                        case "S": returnValue = "H"; break;
                        case "T": returnValue = "U"; break;
                        case "U": returnValue = "3"; break;
                        case "V": returnValue = "X"; break;
                        case "W": returnValue = "B"; break;
                        case "X": returnValue = "T"; break;
                        case "Y": returnValue = "V"; break;
                        case "Z": returnValue = "P"; break;
                        case "1": returnValue = "Q"; break;
                        case "2": returnValue = "G"; break;
                        case "3": returnValue = "4"; break;
                        case "4": returnValue = "Z"; break;
                        case "5": returnValue = "6"; break;
                        case "6": returnValue = "5"; break;
                        case "7": returnValue = "D"; break;
                        case "8": returnValue = "S"; break;
                        case "9": returnValue = "7"; break;
                        case "0": returnValue = "I"; break;
                    }
                    #endregion ""
                    break;
                case "1":
                    #region "Last number end with 1"
                    switch (pValue)
                    {
                        case "A": returnValue = "C"; break;
                        case "B": returnValue = "T"; break;
                        case "C": returnValue = "U"; break;
                        case "D": returnValue = "L"; break;
                        case "E": returnValue = "M"; break;
                        case "F": returnValue = "N"; break;
                        case "G": returnValue = "B"; break;
                        case "H": returnValue = "S"; break;
                        case "I": returnValue = "K"; break;
                        case "J": returnValue = "1"; break;
                        case "K": returnValue = "I"; break;
                        case "L": returnValue = "G"; break;
                        case "M": returnValue = "Y"; break;
                        case "N": returnValue = "2"; break;
                        case "O": returnValue = "X"; break;
                        case "P": returnValue = "J"; break;
                        case "Q": returnValue = "F"; break;
                        case "R": returnValue = "O"; break;
                        case "S": returnValue = "P"; break;
                        case "T": returnValue = "V"; break;
                        case "U": returnValue = "3"; break;
                        case "V": returnValue = "W"; break;
                        case "W": returnValue = "H"; break;
                        case "X": returnValue = "4"; break;
                        case "Y": returnValue = "5"; break;
                        case "Z": returnValue = "6"; break;
                        case "1": returnValue = "D"; break;
                        case "2": returnValue = "R"; break;
                        case "3": returnValue = "Q"; break;
                        case "4": returnValue = "Z"; break;
                        case "5": returnValue = "7"; break;
                        case "6": returnValue = "E"; break;
                        case "7": returnValue = "8"; break;
                        case "8": returnValue = "9"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "A"; break;
                    }
                    #endregion ""
                    break;
                case "2":
                    #region "Last number end with 2"
                    switch (pValue)
                    {
                        case "A": returnValue = "B"; break;
                        case "B": returnValue = "A"; break;
                        case "C": returnValue = "U"; break;
                        case "D": returnValue = "O"; break;
                        case "E": returnValue = "M"; break;
                        case "F": returnValue = "Z"; break;
                        case "G": returnValue = "1"; break;
                        case "H": returnValue = "V"; break;
                        case "I": returnValue = "D"; break;
                        case "J": returnValue = "C"; break;
                        case "K": returnValue = "6"; break;
                        case "L": returnValue = "T"; break;
                        case "M": returnValue = "P"; break;
                        case "N": returnValue = "5"; break;
                        case "O": returnValue = "X"; break;
                        case "P": returnValue = "E"; break;
                        case "Q": returnValue = "F"; break;
                        case "R": returnValue = "4"; break;
                        case "S": returnValue = "3"; break;
                        case "T": returnValue = "S"; break;
                        case "U": returnValue = "8"; break;
                        case "V": returnValue = "H"; break;
                        case "W": returnValue = "G"; break;
                        case "X": returnValue = "N"; break;
                        case "Y": returnValue = "7"; break;
                        case "Z": returnValue = "Q"; break;
                        case "1": returnValue = "W"; break;
                        case "2": returnValue = "0"; break;
                        case "3": returnValue = "I"; break;
                        case "4": returnValue = "J"; break;
                        case "5": returnValue = "9"; break;
                        case "6": returnValue = "R"; break;
                        case "7": returnValue = "2"; break;
                        case "8": returnValue = "Y"; break;
                        case "9": returnValue = "K"; break;
                        case "0": returnValue = "L"; break;
                    }
                    #endregion ""
                    break;
                case "3":
                    #region "Last number end with 3"
                    switch (pValue)
                    {
                        case "A": returnValue = "K"; break;
                        case "B": returnValue = "L"; break;
                        case "C": returnValue = "M"; break;
                        case "D": returnValue = "N"; break;
                        case "E": returnValue = "1"; break;
                        case "F": returnValue = "3"; break;
                        case "G": returnValue = "2"; break;
                        case "H": returnValue = "4"; break;
                        case "I": returnValue = "A"; break;
                        case "J": returnValue = "B"; break;
                        case "K": returnValue = "C"; break;
                        case "L": returnValue = "O"; break;
                        case "M": returnValue = "P"; break;
                        case "N": returnValue = "Q"; break;
                        case "O": returnValue = "U"; break;
                        case "P": returnValue = "V"; break;
                        case "Q": returnValue = "D"; break;
                        case "R": returnValue = "E"; break;
                        case "S": returnValue = "F"; break;
                        case "T": returnValue = "5"; break;
                        case "U": returnValue = "6"; break;
                        case "V": returnValue = "7"; break;
                        case "W": returnValue = "R"; break;
                        case "X": returnValue = "S"; break;
                        case "Y": returnValue = "T"; break;
                        case "Z": returnValue = "8"; break;
                        case "1": returnValue = "W"; break;
                        case "2": returnValue = "X"; break;
                        case "3": returnValue = "Y"; break;
                        case "4": returnValue = "9"; break;
                        case "5": returnValue = "G"; break;
                        case "6": returnValue = "H"; break;
                        case "7": returnValue = "I"; break;
                        case "8": returnValue = "0"; break;
                        case "9": returnValue = "Z"; break;
                        case "0": returnValue = "J"; break;
                    }
                    #endregion ""
                    break;
                case "4":
                    #region "Last number end with 4"
                    switch (pValue)
                    {
                        case "A": returnValue = "O"; break;
                        case "B": returnValue = "A"; break;
                        case "C": returnValue = "P"; break;
                        case "D": returnValue = "0"; break;
                        case "E": returnValue = "9"; break;
                        case "F": returnValue = "R"; break;
                        case "G": returnValue = "F"; break;
                        case "H": returnValue = "8"; break;
                        case "I": returnValue = "S"; break;
                        case "J": returnValue = "T"; break;
                        case "K": returnValue = "A"; break;
                        case "L": returnValue = "7"; break;
                        case "M": returnValue = "6"; break;
                        case "N": returnValue = "U"; break;
                        case "O": returnValue = "V"; break;
                        case "P": returnValue = "5"; break;
                        case "Q": returnValue = "4"; break;
                        case "R": returnValue = "B"; break;
                        case "S": returnValue = "D"; break;
                        case "T": returnValue = "C"; break;
                        case "U": returnValue = "L"; break;
                        case "V": returnValue = "W"; break;
                        case "W": returnValue = "X"; break;
                        case "X": returnValue = "E"; break;
                        case "Y": returnValue = "M"; break;
                        case "Z": returnValue = "N"; break;
                        case "1": returnValue = "Y"; break;
                        case "2": returnValue = "Z"; break;
                        case "3": returnValue = "3"; break;
                        case "4": returnValue = "G"; break;
                        case "5": returnValue = "2"; break;
                        case "6": returnValue = "J"; break;
                        case "7": returnValue = "K"; break;
                        case "8": returnValue = "1"; break;
                        case "9": returnValue = "I"; break;
                        case "0": returnValue = "H"; break;
                    }
                    #endregion ""
                    break;
                case "5":
                    #region "Last number end with 5"
                    switch (pValue)
                    {
                        case "A": returnValue = "A"; break;
                        case "B": returnValue = "C"; break;
                        case "C": returnValue = "F"; break;
                        case "D": returnValue = "H"; break;
                        case "E": returnValue = "J"; break;
                        case "F": returnValue = "L"; break;
                        case "G": returnValue = "N"; break;
                        case "H": returnValue = "P"; break;
                        case "I": returnValue = ""; break;
                        case "J": returnValue = "R"; break;
                        case "K": returnValue = "T"; break;
                        case "L": returnValue = "V"; break;
                        case "M": returnValue = "X"; break;
                        case "N": returnValue = "Z"; break;
                        case "O": returnValue = "2"; break;
                        case "P": returnValue = "4"; break;
                        case "Q": returnValue = "6"; break;
                        case "R": returnValue = "8"; break;
                        case "S": returnValue = "0"; break;
                        case "T": returnValue = "9"; break;
                        case "U": returnValue = "7"; break;
                        case "V": returnValue = "5"; break;
                        case "W": returnValue = "3"; break;
                        case "X": returnValue = "1"; break;
                        case "Y": returnValue = "T"; break;
                        case "Z": returnValue = "W"; break;
                        case "1": returnValue = "U"; break;
                        case "2": returnValue = "S"; break;
                        case "3": returnValue = "Q"; break;
                        case "4": returnValue = "O"; break;
                        case "5": returnValue = "M"; break;
                        case "6": returnValue = "K"; break;
                        case "7": returnValue = "I"; break;
                        case "8": returnValue = "G"; break;
                        case "9": returnValue = "D"; break;
                        case "0": returnValue = "B"; break;
                    }
                    #endregion ""
                    break;
                case "6":
                    #region "Last number end with 6"
                    switch (pValue)
                    {
                        case "A": returnValue = "8"; break;
                        case "B": returnValue = "6"; break;
                        case "C": returnValue = "4"; break;
                        case "D": returnValue = "2"; break;
                        case "E": returnValue = "0"; break;
                        case "F": returnValue = "Z"; break;
                        case "G": returnValue = "X"; break;
                        case "H": returnValue = "U"; break;
                        case "I": returnValue = "S"; break;
                        case "J": returnValue = "Q"; break;
                        case "K": returnValue = "O"; break;
                        case "L": returnValue = "M"; break;
                        case "M": returnValue = "K"; break;
                        case "N": returnValue = "I"; break;
                        case "O": returnValue = "G"; break;
                        case "P": returnValue = "E"; break;
                        case "Q": returnValue = "C"; break;
                        case "R": returnValue = "A"; break;
                        case "S": returnValue = "B"; break;
                        case "T": returnValue = "D"; break;
                        case "U": returnValue = "F"; break;
                        case "V": returnValue = "H"; break;
                        case "W": returnValue = "J"; break;
                        case "X": returnValue = "L"; break;
                        case "Y": returnValue = "N"; break;
                        case "Z": returnValue = "P"; break;
                        case "1": returnValue = "R"; break;
                        case "2": returnValue = "T"; break;
                        case "3": returnValue = "V"; break;
                        case "4": returnValue = "W"; break;
                        case "5": returnValue = "Y"; break;
                        case "6": returnValue = "1"; break;
                        case "7": returnValue = "3"; break;
                        case "8": returnValue = "5"; break;
                        case "9": returnValue = "7"; break;
                        case "0": returnValue = "9"; break;
                    }
                    #endregion ""
                    break;
                case "7":
                    #region "Last number end with 7"
                    switch (pValue)
                    {
                        case "A": returnValue = "A"; break;
                        case "B": returnValue = "C"; break;
                        case "C": returnValue = "D"; break;
                        case "D": returnValue = "B"; break;
                        case "E": returnValue = "E"; break;
                        case "F": returnValue = "G"; break;
                        case "G": returnValue = "F"; break;
                        case "H": returnValue = "I"; break;
                        case "I": returnValue = "H"; break;
                        case "J": returnValue = "O"; break;
                        case "K": returnValue = "Q"; break;
                        case "L": returnValue = "P"; break;
                        case "M": returnValue = "J"; break;
                        case "N": returnValue = "R"; break;
                        case "O": returnValue = "V"; break;
                        case "P": returnValue = "W"; break;
                        case "Q": returnValue = "K"; break;
                        case "R": returnValue = "9"; break;
                        case "S": returnValue = "L"; break;
                        case "T": returnValue = "N"; break;
                        case "U": returnValue = "M"; break;
                        case "V": returnValue = "U"; break;
                        case "W": returnValue = "T"; break;
                        case "X": returnValue = "S"; break;
                        case "Y": returnValue = "0"; break;
                        case "Z": returnValue = "1"; break;
                        case "1": returnValue = "4"; break;
                        case "2": returnValue = "Z"; break;
                        case "3": returnValue = "2"; break;
                        case "4": returnValue = "3"; break;
                        case "5": returnValue = "5"; break;
                        case "6": returnValue = "6"; break;
                        case "7": returnValue = "7"; break;
                        case "8": returnValue = "8"; break;
                        case "9": returnValue = "Y"; break;
                        case "0": returnValue = "X"; break;
                    }
                    #endregion ""
                    break;
                case "8":
                    #region "Last number end with 8"
                    switch (pValue)
                    {
                        case "A": returnValue = "Q"; break;
                        case "B": returnValue = "0"; break;
                        case "C": returnValue = "R"; break;
                        case "D": returnValue = "S"; break;
                        case "E": returnValue = "G"; break;
                        case "F": returnValue = "A"; break;
                        case "G": returnValue = "Y"; break;
                        case "H": returnValue = "1"; break;
                        case "I": returnValue = "Z"; break;
                        case "J": returnValue = "F"; break;
                        case "K": returnValue = "9"; break;
                        case "L": returnValue = "P"; break;
                        case "M": returnValue = "T"; break;
                        case "N": returnValue = "U"; break;
                        case "O": returnValue = "D"; break;
                        case "P": returnValue = "V"; break;
                        case "Q": returnValue = "W"; break;
                        case "R": returnValue = "X"; break;
                        case "S": returnValue = "H"; break;
                        case "T": returnValue = "8"; break;
                        case "U": returnValue = "B"; break;
                        case "V": returnValue = "2"; break;
                        case "W": returnValue = "3"; break;
                        case "X": returnValue = "I"; break;
                        case "Y": returnValue = "4"; break;
                        case "Z": returnValue = "J"; break;
                        case "1": returnValue = "K"; break;
                        case "2": returnValue = "L"; break;
                        case "3": returnValue = "C"; break;
                        case "4": returnValue = "5"; break;
                        case "5": returnValue = "6"; break;
                        case "6": returnValue = "D"; break;
                        case "7": returnValue = "7"; break;
                        case "8": returnValue = "N"; break;
                        case "9": returnValue = "M"; break;
                        case "0": returnValue = "O"; break;
                    }
                    #endregion ""
                    break;
                case "9":
                    #region "Last number end with 9"
                    switch (pValue)
                    {
                        case "A": returnValue = "9"; break;
                        case "B": returnValue = "8"; break;
                        case "C": returnValue = "7"; break;
                        case "D": returnValue = "6"; break;
                        case "E": returnValue = "5"; break;
                        case "F": returnValue = "0"; break;
                        case "G": returnValue = "1"; break;
                        case "H": returnValue = "2"; break;
                        case "I": returnValue = "3"; break;
                        case "J": returnValue = "4"; break;
                        case "K": returnValue = "A"; break;
                        case "L": returnValue = "B"; break;
                        case "M": returnValue = "C"; break;
                        case "N": returnValue = "D"; break;
                        case "O": returnValue = "E"; break;
                        case "P": returnValue = "F"; break;
                        case "Q": returnValue = "G"; break;
                        case "R": returnValue = "H"; break;
                        case "S": returnValue = "W"; break;
                        case "T": returnValue = "X"; break;
                        case "U": returnValue = "Y"; break;
                        case "V": returnValue = "Z"; break;
                        case "W": returnValue = "P"; break;
                        case "X": returnValue = "O"; break;
                        case "Y": returnValue = "N"; break;
                        case "Z": returnValue = "M"; break;
                        case "1": returnValue = "L"; break;
                        case "2": returnValue = "K"; break;
                        case "3": returnValue = "J"; break;
                        case "4": returnValue = "I"; break;
                        case "5": returnValue = "Q"; break;
                        case "6": returnValue = "R"; break;
                        case "7": returnValue = "S"; break;
                        case "8": returnValue = "T"; break;
                        case "9": returnValue = "U"; break;
                        case "0": returnValue = "V"; break;
                    }
                    #endregion ""
                    break;
                case "A":
                    #region "Last number end with A"
                    switch (pValue)
                    {
                        case "A": returnValue = "D"; break;
                        case "B": returnValue = "E"; break;
                        case "C": returnValue = "1"; break;
                        case "D": returnValue = "2"; break;
                        case "E": returnValue = "3"; break;
                        case "F": returnValue = "A"; break;
                        case "G": returnValue = "F"; break;
                        case "H": returnValue = "G"; break;
                        case "I": returnValue = "Y"; break;
                        case "J": returnValue = "Z"; break;
                        case "K": returnValue = "B"; break;
                        case "L": returnValue = "4"; break;
                        case "M": returnValue = "T"; break;
                        case "N": returnValue = "U"; break;
                        case "O": returnValue = "V"; break;
                        case "P": returnValue = "D"; break;
                        case "Q": returnValue = "J"; break;
                        case "R": returnValue = "K"; break;
                        case "S": returnValue = "L"; break;
                        case "T": returnValue = "H"; break;
                        case "U": returnValue = "N"; break;
                        case "V": returnValue = "M"; break;
                        case "W": returnValue = "O"; break;
                        case "X": returnValue = "5"; break;
                        case "Y": returnValue = "I"; break;
                        case "Z": returnValue = "W"; break;
                        case "1": returnValue = "X"; break;
                        case "2": returnValue = "6"; break;
                        case "3": returnValue = "P"; break;
                        case "4": returnValue = "7"; break;
                        case "5": returnValue = "R"; break;
                        case "6": returnValue = "S"; break;
                        case "7": returnValue = "8"; break;
                        case "8": returnValue = "9"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "Q"; break;
                    }
                    #endregion ""
                    break;
                case "B":
                    #region "Last number end with B"
                    switch (pValue)
                    {
                        case "A": returnValue = "D"; break;
                        case "B": returnValue = "1"; break;
                        case "C": returnValue = "2"; break;
                        case "D": returnValue = "E"; break;
                        case "E": returnValue = "C"; break;
                        case "F": returnValue = "3"; break;
                        case "G": returnValue = "H"; break;
                        case "H": returnValue = "4"; break;
                        case "I": returnValue = "I"; break;
                        case "J": returnValue = "F"; break;
                        case "K": returnValue = "U"; break;
                        case "L": returnValue = "V"; break;
                        case "M": returnValue = "W"; break;
                        case "N": returnValue = "G"; break;
                        case "O": returnValue = "R"; break;
                        case "P": returnValue = "5"; break;
                        case "Q": returnValue = "S"; break;
                        case "R": returnValue = "T"; break;
                        case "S": returnValue = "N"; break;
                        case "T": returnValue = "O"; break;
                        case "U": returnValue = "X"; break;
                        case "V": returnValue = "Y"; break;
                        case "W": returnValue = "6"; break;
                        case "X": returnValue = "B"; break;
                        case "Y": returnValue = "Z"; break;
                        case "Z": returnValue = "P"; break;
                        case "1": returnValue = "7"; break;
                        case "2": returnValue = "8"; break;
                        case "3": returnValue = "K"; break;
                        case "4": returnValue = "L"; break;
                        case "5": returnValue = "M"; break;
                        case "6": returnValue = "9"; break;
                        case "7": returnValue = "Q"; break;
                        case "8": returnValue = "J"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "A"; break;
                    }
                    #endregion ""
                    break;
                case "C":
                    #region "Last number end with C"
                    switch (pValue)
                    {
                        case "A": returnValue = "K"; break;
                        case "B": returnValue = "O"; break;
                        case "C": returnValue = "R"; break;
                        case "D": returnValue = "L"; break;
                        case "E": returnValue = "P"; break;
                        case "F": returnValue = "S"; break;
                        case "G": returnValue = "M"; break;
                        case "H": returnValue = "U"; break;
                        case "I": returnValue = "N"; break;
                        case "J": returnValue = "Q"; break;
                        case "K": returnValue = "T"; break;
                        case "L": returnValue = "V"; break;
                        case "M": returnValue = "W"; break;
                        case "N": returnValue = "A"; break;
                        case "O": returnValue = "E"; break;
                        case "P": returnValue = "F"; break;
                        case "Q": returnValue = "B"; break;
                        case "R": returnValue = "J"; break;
                        case "S": returnValue = "G"; break;
                        case "T": returnValue = "C"; break;
                        case "U": returnValue = "H"; break;
                        case "V": returnValue = "I"; break;
                        case "W": returnValue = "D"; break;
                        case "X": returnValue = "1"; break;
                        case "Y": returnValue = "9"; break;
                        case "Z": returnValue = "3"; break;
                        case "1": returnValue = "4"; break;
                        case "2": returnValue = "7"; break;
                        case "3": returnValue = "2"; break;
                        case "4": returnValue = "Z"; break;
                        case "5": returnValue = "8"; break;
                        case "6": returnValue = "5"; break;
                        case "7": returnValue = "6"; break;
                        case "8": returnValue = "0"; break;
                        case "9": returnValue = "Y"; break;
                        case "0": returnValue = "X"; break;
                    }
                    #endregion ""
                    break;
                case "D":
                    #region "Last number end with D"
                    switch (pValue)
                    {
                        case "A": returnValue = "M"; break;
                        case "B": returnValue = "E"; break;
                        case "C": returnValue = "N"; break;
                        case "D": returnValue = "F"; break;
                        case "E": returnValue = "G"; break;
                        case "F": returnValue = "Q"; break;
                        case "G": returnValue = "R"; break;
                        case "H": returnValue = "H"; break;
                        case "I": returnValue = "0"; break;
                        case "J": returnValue = "S"; break;
                        case "K": returnValue = "4"; break;
                        case "L": returnValue = "C"; break;
                        case "M": returnValue = "1"; break;
                        case "N": returnValue = "O"; break;
                        case "O": returnValue = "2"; break;
                        case "P": returnValue = "A"; break;
                        case "Q": returnValue = "P"; break;
                        case "R": returnValue = "3"; break;
                        case "S": returnValue = "W"; break;
                        case "T": returnValue = "X"; break;
                        case "U": returnValue = "I"; break;
                        case "V": returnValue = "Z"; break;
                        case "W": returnValue = "V"; break;
                        case "X": returnValue = "J"; break;
                        case "Y": returnValue = "5"; break;
                        case "Z": returnValue = "B"; break;
                        case "1": returnValue = "7"; break;
                        case "2": returnValue = "8"; break;
                        case "3": returnValue = "6"; break;
                        case "4": returnValue = "K"; break;
                        case "5": returnValue = "L"; break;
                        case "6": returnValue = "D"; break;
                        case "7": returnValue = "U"; break;
                        case "8": returnValue = "T"; break;
                        case "9": returnValue = "Y"; break;
                        case "0": returnValue = "9"; break;
                    }
                    #endregion ""
                    break;
                case "E":
                    #region "Last number end with E"
                    switch (pValue)
                    {
                        case "A": returnValue = "G"; break;
                        case "B": returnValue = "C"; break;
                        case "C": returnValue = "I"; break;
                        case "D": returnValue = "W"; break;
                        case "E": returnValue = "2"; break;
                        case "F": returnValue = "H"; break;
                        case "G": returnValue = "4"; break;
                        case "H": returnValue = "D"; break;
                        case "I": returnValue = "O"; break;
                        case "J": returnValue = "X"; break;
                        case "K": returnValue = "E"; break;
                        case "L": returnValue = "8"; break;
                        case "M": returnValue = "Y"; break;
                        case "N": returnValue = "S"; break;
                        case "O": returnValue = "3"; break;
                        case "P": returnValue = "P"; break;
                        case "Q": returnValue = "F"; break;
                        case "R": returnValue = "Z"; break;
                        case "S": returnValue = "T"; break;
                        case "T": returnValue = "5"; break;
                        case "U": returnValue = "Q"; break;
                        case "V": returnValue = "0"; break;
                        case "W": returnValue = "U"; break;
                        case "X": returnValue = "B"; break;
                        case "Y": returnValue = "N"; break;
                        case "Z": returnValue = "9"; break;
                        case "1": returnValue = "V"; break;
                        case "2": returnValue = "G"; break;
                        case "3": returnValue = "6"; break;
                        case "4": returnValue = "A"; break;
                        case "5": returnValue = "1"; break;
                        case "6": returnValue = "R"; break;
                        case "7": returnValue = "7"; break;
                        case "8": returnValue = "M"; break;
                        case "9": returnValue = "L"; break;
                        case "0": returnValue = "K"; break;
                    }
                    #endregion ""
                    break;
                case "F":
                    #region "Last number end with F"
                    switch (pValue)
                    {
                        case "A": returnValue = "H"; break;
                        case "B": returnValue = "R"; break;
                        case "C": returnValue = "A"; break;
                        case "D": returnValue = "4"; break;
                        case "E": returnValue = "T"; break;
                        case "F": returnValue = "I"; break;
                        case "G": returnValue = "5"; break;
                        case "H": returnValue = "Z"; break;
                        case "I": returnValue = "W"; break;
                        case "J": returnValue = "6"; break;
                        case "K": returnValue = "J"; break;
                        case "L": returnValue = "K"; break;
                        case "M": returnValue = "S"; break;
                        case "N": returnValue = "E"; break;
                        case "O": returnValue = "O"; break;
                        case "P": returnValue = "3"; break;
                        case "Q": returnValue = "P"; break;
                        case "R": returnValue = "F"; break;
                        case "S": returnValue = "X"; break;
                        case "T": returnValue = "8"; break;
                        case "U": returnValue = "9"; break;
                        case "V": returnValue = "G"; break;
                        case "W": returnValue = "0"; break;
                        case "X": returnValue = "V"; break;
                        case "Y": returnValue = "Y"; break;
                        case "Z": returnValue = "L"; break;
                        case "1": returnValue = "C"; break;
                        case "2": returnValue = "M"; break;
                        case "3": returnValue = "D"; break;
                        case "4": returnValue = "U"; break;
                        case "5": returnValue = "Q"; break;
                        case "6": returnValue = "1"; break;
                        case "7": returnValue = "N"; break;
                        case "8": returnValue = "2"; break;
                        case "9": returnValue = "7"; break;
                        case "0": returnValue = "B"; break;
                    }
                    #endregion ""
                    break;
                case "G":
                    #region "Last number end with G"
                    switch (pValue)
                    {
                        case "A": returnValue = "C"; break;
                        case "B": returnValue = "I"; break;
                        case "C": returnValue = "O"; break;
                        case "D": returnValue = "D"; break;
                        case "E": returnValue = "N"; break;
                        case "F": returnValue = "1"; break;
                        case "G": returnValue = "R"; break;
                        case "H": returnValue = "5"; break;
                        case "I": returnValue = "G"; break;
                        case "J": returnValue = "S"; break;
                        case "K": returnValue = "3"; break;
                        case "L": returnValue = "U"; break;
                        case "M": returnValue = "W"; break;
                        case "N": returnValue = "6"; break;
                        case "O": returnValue = "J"; break;
                        case "P": returnValue = "E"; break;
                        case "Q": returnValue = "H"; break;
                        case "R": returnValue = "T"; break;
                        case "S": returnValue = "2"; break;
                        case "T": returnValue = "V"; break;
                        case "U": returnValue = "7"; break;
                        case "V": returnValue = "K"; break;
                        case "W": returnValue = "P"; break;
                        case "X": returnValue = "4"; break;
                        case "Y": returnValue = "A"; break;
                        case "Z": returnValue = "X"; break;
                        case "1": returnValue = "L"; break;
                        case "2": returnValue = "Q"; break;
                        case "3": returnValue = "B"; break;
                        case "4": returnValue = "8"; break;
                        case "5": returnValue = "Y"; break;
                        case "6": returnValue = "F"; break;
                        case "7": returnValue = "M"; break;
                        case "8": returnValue = "9"; break;
                        case "9": returnValue = "Z"; break;
                        case "0": returnValue = "0"; break;
                    }
                    #endregion ""
                    break;
                case "H":
                    #region "Last number end with H"
                    switch (pValue)
                    {
                        case "A": returnValue = "0"; break;
                        case "B": returnValue = "9"; break;
                        case "C": returnValue = "8"; break;
                        case "D": returnValue = "7"; break;
                        case "E": returnValue = "6"; break;
                        case "F": returnValue = "5"; break;
                        case "G": returnValue = "4"; break;
                        case "H": returnValue = "3"; break;
                        case "I": returnValue = "2"; break;
                        case "J": returnValue = "1"; break;
                        case "K": returnValue = "Z"; break;
                        case "L": returnValue = "Y"; break;
                        case "M": returnValue = "X"; break;
                        case "N": returnValue = "W"; break;
                        case "O": returnValue = "V"; break;
                        case "P": returnValue = "U"; break;
                        case "Q": returnValue = "T"; break;
                        case "R": returnValue = "S"; break;
                        case "S": returnValue = "R"; break;
                        case "T": returnValue = "Q"; break;
                        case "U": returnValue = "P"; break;
                        case "V": returnValue = "O"; break;
                        case "W": returnValue = "N"; break;
                        case "X": returnValue = "M"; break;
                        case "Y": returnValue = "L"; break;
                        case "Z": returnValue = "K"; break;
                        case "1": returnValue = "J"; break;
                        case "2": returnValue = "I"; break;
                        case "3": returnValue = "H"; break;
                        case "4": returnValue = "G"; break;
                        case "5": returnValue = "F"; break;
                        case "6": returnValue = "E"; break;
                        case "7": returnValue = "D"; break;
                        case "8": returnValue = "C"; break;
                        case "9": returnValue = "B"; break;
                        case "0": returnValue = "A"; break;
                    }
                    #endregion ""
                    break;
                case "I":
                    #region "Last number end with I"
                    switch (pValue)
                    {
                        case "A": returnValue = "K"; break;
                        case "B": returnValue = "B"; break;
                        case "C": returnValue = "O"; break;
                        case "D": returnValue = "Z"; break;
                        case "E": returnValue = "Q"; break;
                        case "F": returnValue = "A"; break;
                        case "G": returnValue = "T"; break;
                        case "H": returnValue = "1"; break;
                        case "I": returnValue = "L"; break;
                        case "J": returnValue = "X"; break;
                        case "K": returnValue = "9"; break;
                        case "L": returnValue = "Y"; break;
                        case "M": returnValue = "E"; break;
                        case "N": returnValue = "M"; break;
                        case "O": returnValue = "P"; break;
                        case "P": returnValue = "4"; break;
                        case "Q": returnValue = "U"; break;
                        case "R": returnValue = "C"; break;
                        case "S": returnValue = "N"; break;
                        case "T": returnValue = "R"; break;
                        case "U": returnValue = "V"; break;
                        case "V": returnValue = "H"; break;
                        case "W": returnValue = "6"; break;
                        case "X": returnValue = "8"; break;
                        case "Y": returnValue = "7"; break;
                        case "Z": returnValue = "D"; break;
                        case "1": returnValue = "0"; break;
                        case "2": returnValue = "S"; break;
                        case "3": returnValue = "I"; break;
                        case "4": returnValue = "J"; break;
                        case "5": returnValue = "2"; break;
                        case "6": returnValue = "F"; break;
                        case "7": returnValue = "W"; break;
                        case "8": returnValue = "G"; break;
                        case "9": returnValue = "5"; break;
                        case "0": returnValue = "3"; break;
                    }
                    #endregion ""
                    break;
                case "J":
                    #region "Last number end with J"
                    switch (pValue)
                    {
                        case "A": returnValue = "A"; break;
                        case "B": returnValue = "E"; break;
                        case "C": returnValue = "I"; break;
                        case "D": returnValue = "3"; break;
                        case "E": returnValue = "6"; break;
                        case "F": returnValue = "N"; break;
                        case "G": returnValue = "4"; break;
                        case "H": returnValue = "8"; break;
                        case "I": returnValue = "5"; break;
                        case "J": returnValue = "O"; break;
                        case "K": returnValue = "B"; break;
                        case "L": returnValue = "F"; break;
                        case "M": returnValue = "M"; break;
                        case "N": returnValue = "Z"; break;
                        case "O": returnValue = "D"; break;
                        case "P": returnValue = "9"; break;
                        case "Q": returnValue = "7"; break;
                        case "R": returnValue = "R"; break;
                        case "S": returnValue = "2"; break;
                        case "T": returnValue = "0"; break;
                        case "U": returnValue = "G"; break;
                        case "V": returnValue = "H"; break;
                        case "W": returnValue = "J"; break;
                        case "X": returnValue = "1"; break;
                        case "Y": returnValue = "P"; break;
                        case "Z": returnValue = "V"; break;
                        case "1": returnValue = "T"; break;
                        case "2": returnValue = "S"; break;
                        case "3": returnValue = "Q"; break;
                        case "4": returnValue = "W"; break;
                        case "5": returnValue = "K"; break;
                        case "6": returnValue = "L"; break;
                        case "7": returnValue = "X"; break;
                        case "8": returnValue = "Y"; break;
                        case "9": returnValue = "U"; break;
                        case "0": returnValue = "C"; break;
                    }
                    #endregion ""
                    break;
                case "K":
                    #region "Last number end with K"
                    switch (pValue)
                    {
                        case "A": returnValue = "D"; break;
                        case "B": returnValue = "N"; break;
                        case "C": returnValue = "U"; break;
                        case "D": returnValue = "M"; break;
                        case "E": returnValue = "Y"; break;
                        case "F": returnValue = "P"; break;
                        case "G": returnValue = "A"; break;
                        case "H": returnValue = "W"; break;
                        case "I": returnValue = "4"; break;
                        case "J": returnValue = "6"; break;
                        case "K": returnValue = "X"; break;
                        case "L": returnValue = "G"; break;
                        case "M": returnValue = "V"; break;
                        case "N": returnValue = "B"; break;
                        case "O": returnValue = "2"; break;
                        case "P": returnValue = "H"; break;
                        case "Q": returnValue = "R"; break;
                        case "R": returnValue = "5"; break;
                        case "S": returnValue = "7"; break;
                        case "T": returnValue = "K"; break;
                        case "U": returnValue = "8"; break;
                        case "V": returnValue = "C"; break;
                        case "W": returnValue = "O"; break;
                        case "X": returnValue = "9"; break;
                        case "Y": returnValue = "L"; break;
                        case "Z": returnValue = "Z"; break;
                        case "1": returnValue = "Q"; break;
                        case "2": returnValue = "0"; break;
                        case "3": returnValue = "E"; break;
                        case "4": returnValue = "F"; break;
                        case "5": returnValue = "S"; break;
                        case "6": returnValue = "J"; break;
                        case "7": returnValue = "1"; break;
                        case "8": returnValue = "3"; break;
                        case "9": returnValue = "T"; break;
                        case "0": returnValue = "I"; break;
                    }
                    #endregion ""
                    break;
                case "L":
                    #region "Last number end with L"
                    switch (pValue)
                    {
                        case "A": returnValue = "C"; break;
                        case "B": returnValue = "L"; break;
                        case "C": returnValue = "Q"; break;
                        case "D": returnValue = "1"; break;
                        case "E": returnValue = "B"; break;
                        case "F": returnValue = "O"; break;
                        case "G": returnValue = "6"; break;
                        case "H": returnValue = "K"; break;
                        case "I": returnValue = "N"; break;
                        case "J": returnValue = "P"; break;
                        case "K": returnValue = "7"; break;
                        case "L": returnValue = "A"; break;
                        case "M": returnValue = "V"; break;
                        case "N": returnValue = "Y"; break;
                        case "O": returnValue = "M"; break;
                        case "P": returnValue = "H"; break;
                        case "Q": returnValue = "J"; break;
                        case "R": returnValue = "R"; break;
                        case "S": returnValue = "3"; break;
                        case "T": returnValue = "8"; break;
                        case "U": returnValue = "Z"; break;
                        case "V": returnValue = "9"; break;
                        case "W": returnValue = "0"; break;
                        case "X": returnValue = "S"; break;
                        case "Y": returnValue = "I"; break;
                        case "Z": returnValue = "D"; break;
                        case "1": returnValue = "F"; break;
                        case "2": returnValue = "U"; break;
                        case "3": returnValue = "W"; break;
                        case "4": returnValue = "4"; break;
                        case "5": returnValue = "X"; break;
                        case "6": returnValue = "2"; break;
                        case "7": returnValue = "5"; break;
                        case "8": returnValue = "T"; break;
                        case "9": returnValue = "G"; break;
                        case "0": returnValue = "E"; break;
                    }
                    #endregion ""
                    break;
                case "N":
                    #region "Last number end with N"
                    switch (pValue)
                    {
                        case "A": returnValue = "A"; break;
                        case "B": returnValue = "B"; break;
                        case "C": returnValue = "D"; break;
                        case "D": returnValue = "C"; break;
                        case "E": returnValue = "F"; break;
                        case "F": returnValue = "E"; break;
                        case "G": returnValue = "H"; break;
                        case "H": returnValue = "G"; break;
                        case "I": returnValue = "J"; break;
                        case "J": returnValue = "I"; break;
                        case "K": returnValue = "L"; break;
                        case "L": returnValue = "K"; break;
                        case "M": returnValue = "N"; break;
                        case "N": returnValue = "M"; break;
                        case "O": returnValue = "P"; break;
                        case "P": returnValue = "O"; break;
                        case "Q": returnValue = "R"; break;
                        case "R": returnValue = "Q"; break;
                        case "S": returnValue = "T"; break;
                        case "T": returnValue = "S"; break;
                        case "U": returnValue = "V"; break;
                        case "V": returnValue = "U"; break;
                        case "W": returnValue = "X"; break;
                        case "X": returnValue = "W"; break;
                        case "Y": returnValue = "Z"; break;
                        case "Z": returnValue = "Y"; break;
                        case "1": returnValue = "2"; break;
                        case "2": returnValue = "1"; break;
                        case "3": returnValue = "4"; break;
                        case "4": returnValue = "3"; break;
                        case "5": returnValue = "6"; break;
                        case "6": returnValue = "5"; break;
                        case "7": returnValue = "8"; break;
                        case "8": returnValue = "7"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "9"; break;
                    }
                    #endregion ""
                    break;
                case "O":
                    #region "Last number end with O"
                    switch (pValue)
                    {
                        case "A": returnValue = "H"; break;
                        case "B": returnValue = "O"; break;
                        case "C": returnValue = "T"; break;
                        case "D": returnValue = "A"; break;
                        case "E": returnValue = "1"; break;
                        case "F": returnValue = "K"; break;
                        case "G": returnValue = "X"; break;
                        case "H": returnValue = "N"; break;
                        case "I": returnValue = "C"; break;
                        case "J": returnValue = "G"; break;
                        case "K": returnValue = "L"; break;
                        case "L": returnValue = "3"; break;
                        case "M": returnValue = "Y"; break;
                        case "N": returnValue = "W"; break;
                        case "O": returnValue = "I"; break;
                        case "P": returnValue = "E"; break;
                        case "Q": returnValue = "P"; break;
                        case "R": returnValue = "R"; break;
                        case "S": returnValue = "2"; break;
                        case "T": returnValue = "4"; break;
                        case "U": returnValue = "5"; break;
                        case "V": returnValue = "S"; break;
                        case "W": returnValue = "M"; break;
                        case "X": returnValue = "J"; break;
                        case "Y": returnValue = "B"; break;
                        case "Z": returnValue = "U"; break;
                        case "1": returnValue = "6"; break;
                        case "2": returnValue = "7"; break;
                        case "3": returnValue = "Z"; break;
                        case "4": returnValue = "F"; break;
                        case "5": returnValue = "Q"; break;
                        case "6": returnValue = "8"; break;
                        case "7": returnValue = "9"; break;
                        case "8": returnValue = "0"; break;
                        case "9": returnValue = "V"; break;
                        case "0": returnValue = "D"; break;
                    }
                    #endregion ""
                    break;
                case "P":
                    #region "Last number end with P"
                    switch (pValue)
                    {
                        case "A": returnValue = "1"; break;
                        case "B": returnValue = "Q"; break;
                        case "C": returnValue = "N"; break;
                        case "D": returnValue = "I"; break;
                        case "E": returnValue = "9"; break;
                        case "F": returnValue = "C"; break;
                        case "G": returnValue = "3"; break;
                        case "H": returnValue = "M"; break;
                        case "I": returnValue = "H"; break;
                        case "J": returnValue = "F"; break;
                        case "K": returnValue = "8"; break;
                        case "L": returnValue = "W"; break;
                        case "M": returnValue = "R"; break;
                        case "N": returnValue = "6"; break;
                        case "O": returnValue = "Z"; break;
                        case "P": returnValue = "V"; break;
                        case "Q": returnValue = "0"; break;
                        case "R": returnValue = "O"; break;
                        case "S": returnValue = "G"; break;
                        case "T": returnValue = "5"; break;
                        case "U": returnValue = "J"; break;
                        case "V": returnValue = "K"; break;
                        case "W": returnValue = "A"; break;
                        case "X": returnValue = "S"; break;
                        case "Y": returnValue = "Y"; break;
                        case "Z": returnValue = "B"; break;
                        case "1": returnValue = "4"; break;
                        case "2": returnValue = "L"; break;
                        case "3": returnValue = "D"; break;
                        case "4": returnValue = "T"; break;
                        case "5": returnValue = "7"; break;
                        case "6": returnValue = "P"; break;
                        case "7": returnValue = "E"; break;
                        case "8": returnValue = "U"; break;
                        case "9": returnValue = "X"; break;
                        case "0": returnValue = "2"; break;
                    }
                    #endregion ""
                    break;
                case "Q":
                    #region "Last number end with Q"
                    switch (pValue)
                    {
                        case "A": returnValue = "C"; break;
                        case "B": returnValue = "O"; break;
                        case "C": returnValue = "T"; break;
                        case "D": returnValue = "S"; break;
                        case "E": returnValue = "1"; break;
                        case "F": returnValue = "0"; break;
                        case "G": returnValue = "P"; break;
                        case "H": returnValue = "A"; break;
                        case "I": returnValue = "5"; break;
                        case "J": returnValue = "3"; break;
                        case "K": returnValue = "9"; break;
                        case "L": returnValue = "H"; break;
                        case "M": returnValue = "W"; break;
                        case "N": returnValue = "B"; break;
                        case "O": returnValue = "Q"; break;
                        case "P": returnValue = "4"; break;
                        case "Q": returnValue = "X"; break;
                        case "R": returnValue = "I"; break;
                        case "S": returnValue = "2"; break;
                        case "T": returnValue = "F"; break;
                        case "U": returnValue = "M"; break;
                        case "V": returnValue = "R"; break;
                        case "W": returnValue = "G"; break;
                        case "X": returnValue = "U"; break;
                        case "Y": returnValue = "V"; break;
                        case "Z": returnValue = "Z"; break;
                        case "1": returnValue = "J"; break;
                        case "2": returnValue = "D"; break;
                        case "3": returnValue = "6"; break;
                        case "4": returnValue = "8"; break;
                        case "5": returnValue = "K"; break;
                        case "6": returnValue = "7"; break;
                        case "7": returnValue = "E"; break;
                        case "8": returnValue = "L"; break;
                        case "9": returnValue = "Y"; break;
                        case "0": returnValue = "N"; break;
                    }
                    #endregion ""
                    break;
                case "R":
                    #region "Last number end with R"
                    switch (pValue)
                    {
                        case "A": returnValue = "D"; break;
                        case "B": returnValue = "M"; break;
                        case "C": returnValue = "V"; break;
                        case "D": returnValue = "1"; break;
                        case "E": returnValue = "N"; break;
                        case "F": returnValue = "8"; break;
                        case "G": returnValue = "A"; break;
                        case "H": returnValue = "3"; break;
                        case "I": returnValue = "T"; break;
                        case "J": returnValue = "7"; break;
                        case "K": returnValue = "U"; break;
                        case "L": returnValue = "5"; break;
                        case "M": returnValue = "K"; break;
                        case "N": returnValue = "X"; break;
                        case "O": returnValue = "4"; break;
                        case "P": returnValue = "B"; break;
                        case "Q": returnValue = "O"; break;
                        case "R": returnValue = "H"; break;
                        case "S": returnValue = "Y"; break;
                        case "T": returnValue = "P"; break;
                        case "U": returnValue = "2"; break;
                        case "V": returnValue = "I"; break;
                        case "W": returnValue = "C"; break;
                        case "X": returnValue = "G"; break;
                        case "Y": returnValue = "W"; break;
                        case "Z": returnValue = "Z"; break;
                        case "1": returnValue = "L"; break;
                        case "2": returnValue = "J"; break;
                        case "3": returnValue = "R"; break;
                        case "4": returnValue = "6"; break;
                        case "5": returnValue = "Q"; break;
                        case "6": returnValue = "S"; break;
                        case "7": returnValue = "9"; break;
                        case "8": returnValue = "E"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "F"; break;
                    }
                    #endregion ""
                    break;
                case "S":
                    #region "Last number end with S"
                    switch (pValue)
                    {
                        case "A": returnValue = "G"; break;
                        case "B": returnValue = "H"; break;
                        case "C": returnValue = "Q"; break;
                        case "D": returnValue = "R"; break;
                        case "E": returnValue = "U"; break;
                        case "F": returnValue = "V"; break;
                        case "G": returnValue = "W"; break;
                        case "H": returnValue = "A"; break;
                        case "I": returnValue = "B"; break;
                        case "J": returnValue = "S"; break;
                        case "K": returnValue = "T"; break;
                        case "L": returnValue = "I"; break;
                        case "M": returnValue = "J"; break;
                        case "N": returnValue = "O"; break;
                        case "O": returnValue = "P"; break;
                        case "P": returnValue = "X"; break;
                        case "Q": returnValue = "Y"; break;
                        case "R": returnValue = "K"; break;
                        case "S": returnValue = "L"; break;
                        case "T": returnValue = "2"; break;
                        case "U": returnValue = "3"; break;
                        case "V": returnValue = "6"; break;
                        case "W": returnValue = "7"; break;
                        case "X": returnValue = "C"; break;
                        case "Y": returnValue = "D"; break;
                        case "Z": returnValue = "8"; break;
                        case "1": returnValue = "4"; break;
                        case "2": returnValue = "0"; break;
                        case "3": returnValue = "9"; break;
                        case "4": returnValue = "M"; break;
                        case "5": returnValue = "N"; break;
                        case "6": returnValue = "Z"; break;
                        case "7": returnValue = "1"; break;
                        case "8": returnValue = "E"; break;
                        case "9": returnValue = "F"; break;
                        case "0": returnValue = "5"; break;
                    }
                    #endregion ""
                    break;
                case "T":
                    #region "Last number end with T"
                    switch (pValue)
                    {
                        case "A": returnValue = "5"; break;
                        case "B": returnValue = "7"; break;
                        case "C": returnValue = "E"; break;
                        case "D": returnValue = "J"; break;
                        case "E": returnValue = "S"; break;
                        case "F": returnValue = "T"; break;
                        case "G": returnValue = "K"; break;
                        case "H": returnValue = "C"; break;
                        case "I": returnValue = "8"; break;
                        case "J": returnValue = "6"; break;
                        case "K": returnValue = "1"; break;
                        case "L": returnValue = "3"; break;
                        case "M": returnValue = "E"; break;
                        case "N": returnValue = "L"; break;
                        case "O": returnValue = "Q"; break;
                        case "P": returnValue = "W"; break;
                        case "Q": returnValue = "X"; break;
                        case "R": returnValue = "R"; break;
                        case "S": returnValue = "M"; break;
                        case "T": returnValue = "F"; break;
                        case "U": returnValue = "4"; break;
                        case "V": returnValue = "2"; break;
                        case "W": returnValue = "9"; break;
                        case "X": returnValue = "A"; break;
                        case "Y": returnValue = "G"; break;
                        case "Z": returnValue = "I"; break;
                        case "1": returnValue = "N"; break;
                        case "2": returnValue = "P"; break;
                        case "3": returnValue = "U"; break;
                        case "4": returnValue = "Y"; break;
                        case "5": returnValue = "Z"; break;
                        case "6": returnValue = "V"; break;
                        case "7": returnValue = "O"; break;
                        case "8": returnValue = "H"; break;
                        case "9": returnValue = "B"; break;
                        case "0": returnValue = "0"; break;
                    }
                    #endregion ""
                    break;
                case "U":
                    #region "Last number end with U"
                    switch (pValue)
                    {
                        case "A": returnValue = "U"; break;
                        case "B": returnValue = "V"; break;
                        case "C": returnValue = "W"; break;
                        case "D": returnValue = "X"; break;
                        case "E": returnValue = "Y"; break;
                        case "F": returnValue = "A"; break;
                        case "G": returnValue = "B"; break;
                        case "H": returnValue = "C"; break;
                        case "I": returnValue = "D"; break;
                        case "J": returnValue = "N"; break;
                        case "K": returnValue = "O"; break;
                        case "L": returnValue = "P"; break;
                        case "M": returnValue = "Q"; break;
                        case "N": returnValue = "1"; break;
                        case "O": returnValue = "2"; break;
                        case "P": returnValue = "3"; break;
                        case "Q": returnValue = "4"; break;
                        case "R": returnValue = "E"; break;
                        case "S": returnValue = "F"; break;
                        case "T": returnValue = "G"; break;
                        case "U": returnValue = "H"; break;
                        case "V": returnValue = "R"; break;
                        case "W": returnValue = "S"; break;
                        case "X": returnValue = "T"; break;
                        case "Y": returnValue = "Z"; break;
                        case "Z": returnValue = "5"; break;
                        case "1": returnValue = "6"; break;
                        case "2": returnValue = "7"; break;
                        case "3": returnValue = "I"; break;
                        case "4": returnValue = "J"; break;
                        case "5": returnValue = "K"; break;
                        case "6": returnValue = "L"; break;
                        case "7": returnValue = "M"; break;
                        case "8": returnValue = "8"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "9"; break;
                    }
                    #endregion ""
                    break;
                case "V":
                    #region "Last number end with V"
                    switch (pValue)
                    {
                        case "A": returnValue = "5"; break;
                        case "B": returnValue = "W"; break;
                        case "C": returnValue = "6"; break;
                        case "D": returnValue = "I"; break;
                        case "E": returnValue = "C"; break;
                        case "F": returnValue = "O"; break;
                        case "G": returnValue = "T"; break;
                        case "H": returnValue = "1"; break;
                        case "I": returnValue = "U"; break;
                        case "J": returnValue = "P"; break;
                        case "K": returnValue = "D"; break;
                        case "L": returnValue = "J"; break;
                        case "M": returnValue = "2"; break;
                        case "N": returnValue = "E"; break;
                        case "O": returnValue = "X"; break;
                        case "P": returnValue = "7"; break;
                        case "Q": returnValue = "K"; break;
                        case "R": returnValue = "8"; break;
                        case "S": returnValue = "F"; break;
                        case "T": returnValue = "A"; break;
                        case "U": returnValue = "3"; break;
                        case "V": returnValue = "R"; break;
                        case "W": returnValue = "4"; break;
                        case "X": returnValue = "G"; break;
                        case "Y": returnValue = "L"; break;
                        case "Z": returnValue = "Q"; break;
                        case "1": returnValue = "9"; break;
                        case "2": returnValue = "H"; break;
                        case "3": returnValue = "M"; break;
                        case "4": returnValue = "V"; break;
                        case "5": returnValue = "0"; break;
                        case "6": returnValue = "N"; break;
                        case "7": returnValue = "S"; break;
                        case "8": returnValue = "Y"; break;
                        case "9": returnValue = "Z"; break;
                        case "0": returnValue = "B"; break;
                    }
                    #endregion ""
                    break;
                case "W":
                    #region "Last number end with W"
                    switch (pValue)
                    {
                        case "A": returnValue = "K"; break;
                        case "B": returnValue = "L"; break;
                        case "C": returnValue = "M"; break;
                        case "D": returnValue = "N"; break;
                        case "E": returnValue = "O"; break;
                        case "F": returnValue = "P"; break;
                        case "G": returnValue = "Q"; break;
                        case "H": returnValue = "R"; break;
                        case "I": returnValue = "S"; break;
                        case "J": returnValue = "T"; break;
                        case "K": returnValue = "U"; break;
                        case "L": returnValue = "V"; break;
                        case "M": returnValue = "W"; break;
                        case "N": returnValue = "X"; break;
                        case "O": returnValue = "Y"; break;
                        case "P": returnValue = "Z"; break;
                        case "Q": returnValue = "1"; break;
                        case "R": returnValue = "2"; break;
                        case "S": returnValue = "3"; break;
                        case "T": returnValue = "4"; break;
                        case "U": returnValue = "5"; break;
                        case "V": returnValue = "6"; break;
                        case "W": returnValue = "7"; break;
                        case "X": returnValue = "8"; break;
                        case "Y": returnValue = "9"; break;
                        case "Z": returnValue = "0"; break;
                        case "1": returnValue = "A"; break;
                        case "2": returnValue = "B"; break;
                        case "3": returnValue = "C"; break;
                        case "4": returnValue = "D"; break;
                        case "5": returnValue = "E"; break;
                        case "6": returnValue = "F"; break;
                        case "7": returnValue = "G"; break;
                        case "8": returnValue = "H"; break;
                        case "9": returnValue = "I"; break;
                        case "0": returnValue = "J"; break;
                    }
                    #endregion ""
                    break;
                case "X":
                    #region "Last number end with X"
                    switch (pValue)
                    {
                        case "A": returnValue = "G"; break;
                        case "B": returnValue = "I"; break;
                        case "C": returnValue = "H"; break;
                        case "D": returnValue = "2"; break;
                        case "E": returnValue = "1"; break;
                        case "F": returnValue = "3"; break;
                        case "G": returnValue = "0"; break;
                        case "H": returnValue = "A"; break;
                        case "I": returnValue = "R"; break;
                        case "J": returnValue = "P"; break;
                        case "K": returnValue = "Q"; break;
                        case "L": returnValue = "Y"; break;
                        case "M": returnValue = "X"; break;
                        case "N": returnValue = "Z"; break;
                        case "O": returnValue = "L"; break;
                        case "P": returnValue = "J"; break;
                        case "Q": returnValue = "K"; break;
                        case "R": returnValue = "6"; break;
                        case "S": returnValue = "S"; break;
                        case "T": returnValue = "4"; break;
                        case "U": returnValue = "5"; break;
                        case "V": returnValue = "B"; break;
                        case "W": returnValue = "N"; break;
                        case "X": returnValue = "M"; break;
                        case "Y": returnValue = "O"; break;
                        case "Z": returnValue = "T"; break;
                        case "1": returnValue = "U"; break;
                        case "2": returnValue = "C"; break;
                        case "3": returnValue = "V"; break;
                        case "4": returnValue = "7"; break;
                        case "5": returnValue = "W"; break;
                        case "6": returnValue = "8"; break;
                        case "7": returnValue = "D"; break;
                        case "8": returnValue = "9"; break;
                        case "9": returnValue = "E"; break;
                        case "0": returnValue = "F"; break;
                    }
                    #endregion ""
                    break;
                case "Y":
                    #region "Last number end with Y"
                    switch (pValue)
                    {
                        case "A": returnValue = "C"; break;
                        case "B": returnValue = "J"; break;
                        case "C": returnValue = "M"; break;
                        case "D": returnValue = "Q"; break;
                        case "E": returnValue = "A"; break;
                        case "F": returnValue = "T"; break;
                        case "G": returnValue = "Z"; break;
                        case "H": returnValue = "H"; break;
                        case "I": returnValue = "W"; break;
                        case "J": returnValue = "2"; break;
                        case "K": returnValue = "4"; break;
                        case "L": returnValue = "E"; break;
                        case "M": returnValue = "7"; break;
                        case "N": returnValue = "V"; break;
                        case "O": returnValue = "8"; break;
                        case "P": returnValue = "K"; break;
                        case "Q": returnValue = "0"; break;
                        case "R": returnValue = "O"; break;
                        case "S": returnValue = "5"; break;
                        case "T": returnValue = "F"; break;
                        case "U": returnValue = "X"; break;
                        case "V": returnValue = "9"; break;
                        case "W": returnValue = "3"; break;
                        case "X": returnValue = "P"; break;
                        case "Y": returnValue = "Y"; break;
                        case "Z": returnValue = "L"; break;
                        case "1": returnValue = "U"; break;
                        case "2": returnValue = "R"; break;
                        case "3": returnValue = "B"; break;
                        case "4": returnValue = "N"; break;
                        case "5": returnValue = "6"; break;
                        case "6": returnValue = "G"; break;
                        case "7": returnValue = "1"; break;
                        case "8": returnValue = "S"; break;
                        case "9": returnValue = "I"; break;
                        case "0": returnValue = "D"; break;
                    }
                    #endregion ""
                    break;
                case "Z":
                    #region "Last number end with Z"
                    switch (pValue)
                    {
                        case "A": returnValue = "3"; break;
                        case "B": returnValue = "4"; break;
                        case "C": returnValue = "H"; break;
                        case "D": returnValue = "I"; break;
                        case "E": returnValue = "1"; break;
                        case "F": returnValue = "2"; break;
                        case "G": returnValue = "J"; break;
                        case "H": returnValue = "K"; break;
                        case "I": returnValue = "L"; break;
                        case "J": returnValue = "M"; break;
                        case "K": returnValue = "B"; break;
                        case "L": returnValue = "C"; break;
                        case "M": returnValue = "T"; break;
                        case "N": returnValue = "U"; break;
                        case "O": returnValue = "5"; break;
                        case "P": returnValue = "6"; break;
                        case "Q": returnValue = "N"; break;
                        case "R": returnValue = "O"; break;
                        case "S": returnValue = "D"; break;
                        case "T": returnValue = "E"; break;
                        case "U": returnValue = "P"; break;
                        case "V": returnValue = "Q"; break;
                        case "W": returnValue = "7"; break;
                        case "X": returnValue = "8"; break;
                        case "Y": returnValue = "V"; break;
                        case "Z": returnValue = "W"; break;
                        case "1": returnValue = "F"; break;
                        case "2": returnValue = "G"; break;
                        case "3": returnValue = "R"; break;
                        case "4": returnValue = "S"; break;
                        case "5": returnValue = "X"; break;
                        case "6": returnValue = "Y"; break;
                        case "7": returnValue = "Z"; break;
                        case "8": returnValue = "9"; break;
                        case "9": returnValue = "0"; break;
                        case "0": returnValue = "A"; break;
                    }
                    #endregion ""
                    break;
            }

            return returnValue;
        }
    }
}
