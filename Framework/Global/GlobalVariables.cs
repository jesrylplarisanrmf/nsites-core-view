using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

using NSites_V.ApplicationObjects.UserInterfaces.Generics;

namespace NSites_V.Global
{
    public class GlobalVariables
    {
        public static string ConnectionString = "";
        public static string BaseAddress = "";
        //application
        public static string ApplicationId = "";
        public static string ApplicationName = "";
        public static string ProcessorId = "";
        public static string TrialVersion = "";
        public static string LicenseKey = "";
        public static DateTime lLicenseExpiry;
        public static string VersionNo = "";
        public static string DevelopedBy = "";
        //company
        public static string CompanyName = "";
        public static string CompanyAddress = "";
        public static string ContactNumber = "";
        public static string CompanyLogo = "";
        public static string BannerImage = "";
        public static string ReportLogo = "";
        //system settings
        public static string CurrentLocationId = "";
        public static string CurrentBranchId = "";
        public static string CurrentConnection = "";
        //database backup
        public static string BackupPath = "";
        public static string BackupMySqlDumpAddress = "";
        public static string RestoreMySqlAddress = "";
        //display
        public static string ScreenSaverImage = "";
        public static string TabAlignment = "";
        public static int DisplayRecordLimit = 0;
        public static string PrimaryColor = "";
        public static string SecondaryColor = "";
        public static string FormBackColor = "";
        public static string EmailAddress = "";
        public static string EmailPassword = "";
        public static string CurrentFinancialYear = "";
        public static string ReviewedBy = "";
        public static string ApprovedBy = "";
        //accounts setup
        public static string SODebitAccount = "";
        public static string SOCreditAccount = "";
        public static string PODebitAccount = "";
        public static string POCreditAccount = "";
        public static string CRDebitAccount = "";
        public static string CRCreditAccount = "";
        public static string CDDebitAccount = "";
        public static string CDCreditAccount = "";
        public static string CashierPeriodDebit = "";
        public static string CashierPeriodCredit = "";

        public static string RetainedEarningsCode = "";
        public static string IncomeAndExpenseSummaryCode = "";

        public static string AssetClassificationCode = "";
        public static string LiabilityClassificationCode = "";
        public static string EquityClassificationCode = "";
        public static string IncomeClassificationCode = "";
        public static string ExpensesClassificationCode = "";
        public static string IncomeAndExpenseSummaryClassificationCode = "";

        public static string CashierPeriodId = "0";
        public static string CashierId = "0";
        public static string CashierName = "";
        public static string DefaultCustomerId = "0";
        public static string DefaultCustomerName = "";
        public static string DefaultModeOfPaymentId = "";
        public static string DefaultModeOfPaymentDescription = "";
        public static string DefaultTableId = "";
        public static string DefaultTableDescription = "";

        public static string POSType = "";
        public static bool TouchScreen = false;
        public static bool PreviewOR = false;
        public static bool PreviewSlip = false;
        public static string PrinterName = "";
        public static string Terminal = "";
        public static string ORSize = "";
        public static string OverridePassword = "";

        public static bool Speak = false;
        public static bool RecognizedSpeech = false;
        public static decimal ShortTermPlacementTax = 0;
        public static decimal LongTermPlacementTax = 0;
        public static decimal PortfolioManagedTax = 0;

        public static string UserId = "";
        public static string Username = "";
        public static string UserPassword = "";
        public static string Userfullname = "";
        public static string UserGroupId = "";
        public static string UserEmailAddress = "";
        public static DateTime LastBackupDate;
        public static string Hostname = "";
        public static int xLocation;
        public static int yLocation;
        //Dataview
        public static DataView DVRights;
        //data table
        public static DataTable DTCompanyLogo = new DataTable();
        //report
        public static ReportViewerUI loReportPreviewer = new ReportViewerUI();
        public static CrystalReport loCrystalReport = new CrystalReport();
        public static CrystalReport1 loCrystalReport1 = new CrystalReport1();
        //set limitations
        public static int MaxNoOfEmployees = 0;
        public static int MaxNoOfCompanies = 0;
        //technical support
        public static string TechnicalSupportEmailAddress = "";

        public enum Operation
        {
            Add = 0,
            Edit = 1,
            Delete = 2,
            Open = 3,
            Close = 4
        };
        public enum Icons
        {
            Information = 0,
            Save = 1,
            Ok = 2,
            QuestionMark = 3,
            Delete = 4,
            Warning = 5,
            Error = 6,
            Close = 7
        };
        public enum Buttons
        {
            OK = 0,
            OKCancel = 1,
            YesNo = 2,
            YesNoCancel = 3,
            Close = 4
        };
    }
}
