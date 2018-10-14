using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Accountings
{
    class JournalEntryDetail
    {
        #region "CONSTRUCTORS"
        public JournalEntryDetail()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string JournalEntryId { get; set; }
        public string AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Subsidiary { get; set; }
        public string SubsidiaryId { get; set; }
        public string SubsidiaryDescription { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getJournalEntryDetails(string pDisplayType, string pId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntryDetails?pDisplayType=" + pDisplayType + "&pId=" + pId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        #region "REPORTS"
        public DataTable getGeneralLedgerAccounts(int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getGeneralLedgerAccounts?pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getGeneralLedgerDetails(string pAccountId,int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getGeneralLedgerDetails?pAccountId=" + pAccountId + "&pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getGeneralLedgerDetailsByDate(string pAccountId, int pFinancialYear, DateTime pDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getGeneralLedgerDetailsByDate?pAccountId=" + pAccountId + "&pFinancialYear=" + pFinancialYear + "&pDate=" + string.Format("{0:MM/dd/yyyy}", pDate) + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSubsidiaryLedgerAccounts(int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSubsidiaryLedgerAccounts?pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSubsidiaries(string pAccountId, string pSubsidiary, int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSubsidiaries?pAccountId=" + pAccountId + "&pSubsidiary=" + pSubsidiary + "&pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSubsidiaryLedgerDetails(string pAccountId, string pSubsidiaryId, string pSubsidiary, int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSubsidiaryLedgerDetails?pAccountId=" + pAccountId + "&pSubsidiaryId=" + pSubsidiaryId + "&pSubsidiary=" + pSubsidiary + "&pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSubsidiaryLedgerDetailsByDate(string pAccountId, string pSubsidiaryId, string pSubsidiary, int pFinancialYear, DateTime pDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSubsidiaryLedgerDetailsByDate?pAccountId=" + pAccountId + "&pSubsidiaryId=" + pSubsidiaryId + "&pSubsidiary=" + pSubsidiary + "&pFinancialYear=" + pFinancialYear + "&pDate=" + string.Format("{0:yyyy-MM-dd}", pDate) + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTrialBalance(int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTrialBalance?pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetAccounts(int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetAccounts?pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetBeginningBalance(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetBeginningBalance?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetTrialBalance(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetTrialBalance?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetAdjustment(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetAdjustment?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetBalanceSheet(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetBalanceSheet?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetIncomeStatement(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetIncomeStatement?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getWorkSheetClosingEntry(string pAccountCode, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getWorkSheetClosingEntry?pAccountCode=" + pAccountCode + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getBalanceSheetClassifications(string pClassification, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getBalanceSheetClassifications?pClassification=" + pClassification + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getAccountBeginningBalance(int pFinancialYear, string pAccountCode)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getAccountBeginningBalance?pFinancialYear=" + pFinancialYear + "&pAccountCode=" + pAccountCode + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getBalanceSheetSubClassifications(string pClassification, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getBalanceSheetSubClassifications?pClassification=" + pClassification + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getBalanceSheetMainAccounts(string pSubClassification, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getBalanceSheetMainAccounts?pSubClassification=" + pSubClassification + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getBalanceSheetMainAccountsForRetainedEarnings(string pSubClassification, int pFinancialYear, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getBalanceSheetMainAccountsForRetainedEarnings?pSubClassification=" + pSubClassification + "&pFinancialYear=" + pFinancialYear + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCOABeginningBalance(int pFinancialYear, string pAccountCode, string pAsOf)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCOABeginningBalance?pFinancialYear=" + pFinancialYear + "&pAccountCode=" + pAccountCode + "&pAsOf=" + pAsOf + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        #endregion ""

        public DataTable getIncomeStatementForClosingEntry(int pFinancialYear, string pClassification)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getIncomeStatementForClosingEntry?pFinancialYear=" + pFinancialYear + "&pClassification=" + pClassification + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getBalanceForwardedAccounts(int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getBalanceForwardedAccounts?pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool save(GlobalVariables.Operation pOperation)
        {
            bool _result = false;
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertJournalEntryDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateJournalEntryDetail/", this).Result;
                        _result = bool.Parse(responseEdit.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _result;
        }

        public bool remove(string pDetailId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeJournalEntryDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
