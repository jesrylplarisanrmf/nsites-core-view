using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Systems
{
    class SystemConfiguration
    {
        #region "CONSTRUCTORS"
        public SystemConfiguration()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Key
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }

        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSystemConfigurations").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool saveSystemConfiguration(GlobalVariables.Operation pOperation)
        {
            bool _result = false;
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateSystemConfiguration/", this).Result;
                        _result = bool.Parse(responseEdit.Content.ReadAsStringAsync().Result);
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
