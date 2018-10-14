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
    class Stock
    {
        #region "CONSTRUCTORS"
        public Stock()
        {
            
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string UnitId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal BasePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public string Active { get; set; }
        public string Saleable { get; set; }
        public string NonInventory { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStocks?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStocksByCode(string pCode)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStocksByCode?pCode=" + pCode + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSaleableStocks()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSaleableStocks").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSaleableStock(string pCode, string pDescription)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSaleableStock?pCode=" + pCode + "&pDescription=" + pDescription + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockCard(DateTime pFromDate, DateTime pToDate, string pStockId, string pLocationId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockCard?pFromDate=" + string.Format("{0:yyyy-MM-dd}", pFromDate) + "&pToDate=" + string.Format("{0:yyyy-MM-dd}", pToDate) + "&pStockId=" + pStockId + "&pLocationId=" + pLocationId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockCardBegBal(DateTime pFromDate, string pStockId, string pLocationId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockCardBegBal?pFromDate=" + string.Format("{0:yyyy-MM-dd}", pFromDate) + "&pStockId=" + pStockId + "&pLocationId=" + pLocationId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStockQtyOnHand(string pLocationId, string pStockId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStockQtyOnHand?pLocationId=" + pLocationId + "&pStockId=" + pStockId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getReorderLevel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getReorderLevel").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public string save(GlobalVariables.Operation pOperation)
        {
            string _Id = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertStock/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateStock/", this).Result;
                        _Id = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _Id.Replace("\"", "");
        }

        public bool remove(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeStock?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
