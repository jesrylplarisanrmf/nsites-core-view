using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Systems
{
    class UserGroup
    {
        #region "CONSTRUCTORS"
        public UserGroup()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "PROPERTIES"
        public string Id { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        public DataTable getAllData(string pDisplayType,string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUserGroups?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getUserGroupMenuItems()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUserGroupMenuItems?pUsername=" + GlobalVariables.Username).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getUserGroupRights()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUserGroupRights?pUsername=" + GlobalVariables.Username).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getMenuItemsByGroup(string pUserGroupId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getMenuItemsByGroup?pUserGroupId=" + pUserGroupId).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getAllMenuItems()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getAllMenuItems").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getAllRights(string pItemName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getAllRights?pItemName=" + pItemName).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getEnableRights(string pItemName, string pUserGroupId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEnableRights?pItemName=" + pItemName + "&pUserGroupId=" + pUserGroupId).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public DataTable getEnableCompanys(string pUserGroupId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getEnableCompanys?pUserGroupId=" + pUserGroupId).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }
        public string saveUserGroup(GlobalVariables.Operation pOperation)
        {
            string _Id = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertUserGroup/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateUserGroup/", this).Result;
                        _Id = responseEdit.Content.ReadAsStringAsync().Result;
                        break;
                    default:
                        break;
                }
            }
            catch { }
            return _Id;
        }
        public bool updateUserGroupMenuItem(string pUserGroupId, DataTable pMenuItems)
        {
            bool _success = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeAllUserGroup?pUserGroupId=" + pUserGroupId).Result;
                _success = bool.Parse(response.Content.ReadAsStringAsync().Result);


                foreach (DataRow _dr in pMenuItems.Rows)
                {
                    if (_dr["Status"].ToString() == "Enable")
                    {
                        HttpClient client1 = new HttpClient();
                        client1.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage response1 = client1.GetAsync("api/main/updateUserGroupMenuItems?pUserGroupId=" + pUserGroupId + "&pMenuItem=" + _dr["MenuName"].ToString() + "&pItemName=" + _dr["ItemName"].ToString()).Result;
                        _success = bool.Parse(response1.Content.ReadAsStringAsync().Result);
                    }
                }
                return _success;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool updateUserGroupRights(string pUserGroupId, string pItemName, DataTable pRights)
        {
            bool _success = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeAllRights?pUserGroupId=" + pUserGroupId + "&pItemName=" + pItemName).Result;
                _success = bool.Parse(response.Content.ReadAsStringAsync().Result);

                foreach (DataRow _dr in pRights.Rows)
                {
                    _success = true;
                    if (_dr["RightStatus"].ToString() == "Enable")
                    {
                        HttpClient client1 = new HttpClient();
                        client1.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage response1 = client1.GetAsync("api/main/updateUserGroupRights?pUserGroupId=" + pUserGroupId + "&pItemName=" + _dr["RightsItemName"].ToString() + "&pRights=" + _dr["Rights"].ToString()).Result;
                        _success = bool.Parse(response1.Content.ReadAsStringAsync().Result);
                    }
                }
                return _success;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool removeUserGroup(string pId)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeUserGroup?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"
    }
}
