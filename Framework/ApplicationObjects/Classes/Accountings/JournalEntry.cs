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
    class JournalEntry
    {
        #region "CONSTRUCTORS"
        public JournalEntry()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string JournalEntryId { get; set; }
        public int FinancialYear { get; set; }
        public string Posted { get; set; }
        public string Cancel { get; set; }
        public string Journal { get; set; }
        public string Form { get; set; }
        public string VoucherNo { get; set; }
        public DateTime DatePrepared { get; set; }
        public string Explanation { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string Reference { get; set; }
        public string SupplierId { get; set; }
        public string CustomerId { get; set; }
        public string BegBal { get; set; }
        public string Adjustment { get; set; }
        public string ClosingEntry { get; set; }
        public string PreparedBy { get; set; }
        public string PostedBy { get; set; }
        public DateTime DatePosted { get; set; }
        public string CancelledBy { get; set; }
        public string CancelledReason { get; set; }
        public DateTime DateCancelled { get; set; }
        public string Remarks { get; set; }
        public string SOId { get; set; }
        public string POId { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pJournal, string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntrys?pJournal=" + pJournal + "&pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getJournalEntryStatus(string pJournalEntryId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntryStatus?pJournalEntryId=" + pJournalEntryId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getJournalEntryBySOId(string pSOId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntryBySOId?pSOId=" + pSOId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getJournalEntryByPOId(string pPOId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntryByPOId?pPOId=" + pPOId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getJournalEntryReport(int pFinancialYear)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getJournalEntryReport?pFinancialYear=" + pFinancialYear + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public string save(GlobalVariables.Operation pOperation)
        {
            string _result = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertJournalEntry/", this).Result;
                        _result = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateJournalEntry/", this).Result;
                        _result = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _result.Replace("\"", "");
        }

        public bool remove(string pJournalEntryId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeJournalEntry?pJournalEntryId=" + pJournalEntryId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool post(string pJournalEntryId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/postJournalEntry?pJournalEntryId=" + pJournalEntryId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool cancel(string pJournalEntryId, string pCancelledReason)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/cancelJournalEntry?pJournalEntryId=" + pJournalEntryId + "&pCancelledReason=" + pCancelledReason + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool closeFinancialYear(int pFinancialYear)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/closeFinancialYear?pFinancialYear=" + pFinancialYear + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        #endregion "END OF METHODS"
    }
}
