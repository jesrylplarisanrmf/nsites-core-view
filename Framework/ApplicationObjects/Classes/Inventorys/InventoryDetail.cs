using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Inventorys
{
    class InventoryDetail
    {
        #region "CONSTRUCTORS"
        public InventoryDetail()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string DetailId { get; set; }
        public string InventoryId { get; set; }
        public string PODetailId { get; set; }
        public string SODetailId { get; set; }
        public string StockId { get; set; }
        public string LocationId { get; set; }
        public decimal POQty { get; set; }
        public decimal QtyIn { get; set; }
        public decimal SOQty { get; set; }
        public decimal QtyOut { get; set; }
        public decimal Variance { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getInventoryDetails(string pDisplayType, string pId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getInventoryDetails?pDisplayType=" + pDisplayType + "&pId=" + pId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockInventory(string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockInventory?pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockInventoryByLocation(string pLocationId, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockInventoryByLocation?pLocationId=" + pLocationId + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockInventoryList(string pLocationId, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockInventoryList?pLocationId=" + pLocationId + "&pSearchString=" + pSearchString).Result;
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
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertInventoryDetail/", this).Result;
                        _result = bool.Parse(responseAdd.Content.ReadAsStringAsync().Result);
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateInventoryDetail/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeInventoryDetail?pDetailId=" + pDetailId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
