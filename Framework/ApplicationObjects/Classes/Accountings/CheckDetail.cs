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
    class CheckDetail
    {
        #region "CONSTRUCTORS"
        public CheckDetail()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string JournalEntryId { get; set; }
        public string BankId { get; set; }
        public string CheckNo { get; set; }
        public decimal CheckAmount { get; set; }
        public DateTime CheckDate { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getCheckDetails(string pJournalEntryId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCheckDetails?pJournalEntryId=" + pJournalEntryId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCheckDetail(string pDetailId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCheckDetail?pDetailId=" + pDetailId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCheckDetailsForEdit(string pJournalEntryId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCheckDetailsForEdit?pJournalEntryId=" + pJournalEntryId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getCheckIssuance(DateTime pStartDate, DateTime pEndDate)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getCheckIssuance?pStartDate=" + String.Format("{0:MM/dd/yyyy}", pStartDate) + "&pEndDate=" + String.Format("{0:MM/dd/yyyy}", pEndDate)).Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertCheckDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateCheckDetail/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeCheckDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
