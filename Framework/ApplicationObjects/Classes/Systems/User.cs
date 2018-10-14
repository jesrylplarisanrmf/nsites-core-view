using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NSites_V.Global;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace NSites_V.ApplicationObjects.Classes.Systems
{
    public class User
    {
        #region "VARIABLES"
        //UserDAO lUserDAO;
        #endregion

        #region "CONSTRUCTORS"
        public User()
        {
            //lUserDAO = new UserDAO();
        }
        #endregion

        #region "PROPERTIES"
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string UserGroupId { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        #endregion

        #region "METHODS"
        public DataTable getAllData(string pDisplayType,string pPrimaryKey, string pSearchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUsers?pDisplayType=" + pDisplayType + "&pPrimaryKey=" + pPrimaryKey + "&pSearchString=" + pSearchString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getUser(string pUsername)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getUser?pUsername=" + pUsername + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable autenticateUser(string pUsername, string pPassword)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/authenticateUser?pUsername=" + pUsername + "&pPassword=" + pPassword).Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool checkUserPassword(string pCurrentPassword)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/checkUserPassword?pUserId=" + GlobalVariables.UserId + "&pCurrentPassword=" + pCurrentPassword).Result;
                return bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return false;
            }
        }

        public bool changePassword(string pNewPassword)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/changePassword?pUserId=" + GlobalVariables.UserId + "&pNewPassword=" + pNewPassword).Result;
                return bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch
            {
                return false;
            }
        }

        public string saveUser(GlobalVariables.Operation pOperation)
        {
            string _Id = "";
            try
            {
                switch (pOperation)
                {
                    case GlobalVariables.Operation.Add:
                        HttpClient clientAdd = new HttpClient();
                        clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseAdd = clientAdd.PostAsJsonAsync("api/main/insertUser/", this).Result;
                        _Id = responseAdd.Content.ReadAsStringAsync().Result;
                        break;
                    case GlobalVariables.Operation.Edit:
                        HttpClient clientEdit = new HttpClient();
                        clientEdit.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                        HttpResponseMessage responseEdit = clientEdit.PostAsJsonAsync("api/main/updateUser/", this).Result;
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
                HttpResponseMessage response = client.GetAsync("api/main/removeUser?pId=" + pId + "&pUserId=" + GlobalVariables.UserId).Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion
    }
}
