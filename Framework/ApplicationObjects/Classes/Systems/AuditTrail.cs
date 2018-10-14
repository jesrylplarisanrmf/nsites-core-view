using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Systems
{
    class AuditTrail
    {
        #region "CONSTRUCTORS"
        public AuditTrail()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string LogDescription
        {
            get;
            set;
        }
        public string Username
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAuditTrailByDate(DateTime pFrom,DateTime pTo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getAuditTrailByDate?pFrom=" + string.Format("{0:yyyy-MM-dd}", pFrom) + "&pTo=" + string.Format("{0:yyyy-MM-dd}", pTo) + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool removeAuditTrail(DateTime pFrom, DateTime pTo)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeAuditTrail?pFrom=" + string.Format("{0:yyyy-MM-dd}", pFrom) + "&pTo=" + string.Format("{0:yyyy-MM-dd}", pTo) + "").Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
