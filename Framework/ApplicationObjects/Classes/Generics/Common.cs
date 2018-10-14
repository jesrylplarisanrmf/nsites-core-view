using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

using NSites_V.Global;
using System.Net.Http;

namespace NSites_V.ApplicationObjects.Classes.Generics
{
    class Common
    {
        #region "CONSTRUCTORS"
        public Common()
        {
        }
        #endregion "END OF CONSTTRUCTORS"

        #region "METHODS"
        public DataTable getDataFromSearch(string pQueryString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getDataFromSearch?pQueryString=" + pQueryString + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getNextTabelSequenceId(string pDescription)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getNextTabelSequenceId?pDescription=" + pDescription + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public bool backupDatabase(string pSaveFileTo, string pBackupMySqlDumpAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/backupDatabase?pSaveFileTo=" + pSaveFileTo +
                    "&pBackupMySqlDumpAddress=" + pBackupMySqlDumpAddress + "&pUserId=" + pUserId +
                    "&pUserId=" + pUserId + "&pPassword=" + pPassword +
                    "&pServer=" + pServer + "&pDatabase=" + pDatabase + "").Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }

        public bool restoreDatabase(string pSQLFileFrom, string pRestoreMySqlAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            bool _result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/restoreDatabase?pSQLFileFrom=" + pSQLFileFrom +
                    "&pRestoreMySqlAddress=" + pRestoreMySqlAddress + "&pUserId=" + pUserId +
                    "&pUserId=" + pUserId + "&pPassword=" + pPassword +
                    "&pServer=" + pServer + "&pDatabase=" + pDatabase + "").Result;
                _result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _result;
        }
        #endregion "END OF METHODS"

        #region "SEARCH"
        //displayfields
        public string MenuName
        {
            get;
            set;
        }
        public string TemplateName
        {
            get;
            set;
        }
        public string DisplayFields
        {
            get;
            set;
        }
        public int SequenceNo
        {
            get;
            set;
        }
        public string Private
        {
            get;
            set;
        }
        //filters
        public string Fields
        {
            get;
            set;
        }
        public string Operator
        {
            get;
            set;
        }
        public string OperatorSign
        {
            get;
            set;
        }
        public string Values
        {
            get;
            set;
        }
        public string CheckAnd
        {
            get;
            set;
        }
        public string CheckOr
        {
            get;
            set;
        }
        //groupings
        public string FieldName
        {
            get;
            set;
        }
        public string GroupBy
        {
            get;
            set;
        }
        public string SortBy
        {
            get;
            set;
        }

        public DataTable getTemplateNames(string pMenuName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTemplateNames?pMenuName=" + pMenuName + "&pUserId=" + GlobalVariables.UserId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTemplateName(string pId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTemplateName?pId=" + pId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSearchFilters(string pTemplateId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSearchFilters?pTemplateId=" + pTemplateId + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getTableDetails(string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getTableDetails").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getViewDetails(string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getViewDetails").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getStoredProcedureDetails(string pDatabaseName,string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getStoredProcedureDetails?pDatabaseName="+pDatabaseName+"").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getFunctionDetails(string pDatabaseName,string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getFunctionDetails?pDatabaseName=" + pDatabaseName + "").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getMenuItemDetails(string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getMenuItemDetails").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getItemRightDetails(string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getItemRightDetails").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public DataTable getSystemConfigurationDetails(string pBaseAddress)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pBaseAddress);
            HttpResponseMessage response = client.GetAsync("api/main/getSystemConfigurationDetails").Result;
            return response.Content.ReadAsAsync<DataTable>().Result;
        }

        public string insertSearchTemplate(string pTemplateName, string pItemName, string pPrivate)
        {
            string _Id = "";
            try
            {
                HttpClient clientAdd = new HttpClient();
                clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage responseAdd = clientAdd.GetAsync("api/main/insertSearchTemplate?pTemplateName=" + pTemplateName + "&pItemName=" + pItemName + "&pPrivate=" + pPrivate + "&pUserId=" + GlobalVariables.UserId).Result;
                _Id = responseAdd.Content.ReadAsStringAsync().Result;
            }
            catch { }
            return _Id;
        }

        public bool updateSearchTemplate(string pId,string pTemplateName, string pItemName, string pPrivate)
        {
            bool _Status = false;
            try
            {
                HttpClient clientAdd = new HttpClient();
                clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = clientAdd.GetAsync("api/main/updateSearchTemplate?pId=" + pId + "&pTemplateName=" + pTemplateName + "&pItemName=" + pItemName + "&pPrivate=" + pPrivate).Result;
                _Status = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _Status;
        }

        public bool removeSearchTemplate(string pId)
        {
            bool _Status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeSearchTemplate?pId=" + pId).Result;
                _Status = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _Status;
        }

        public bool renameSearchTemplate(string pId, string pTemplateName)
        {
            bool _Status = false;
            try
            {
                HttpClient clientAdd = new HttpClient();
                clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = clientAdd.GetAsync("api/main/renameSearchTemplate?pId=" + pId + "&pTemplateName=" + pTemplateName).Result;
                _Status = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _Status;
        }

        public bool insertSearchFilter(string pTemplateId, string pField, string pOperator, string pValue, string pCheckAnd, string pCheckOr, int pSequence)
        {
            bool _Status = false;
            try
            {
                HttpClient clientAdd = new HttpClient();
                clientAdd.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = clientAdd.GetAsync("api/main/insertSearchFilter?pTemplateId=" + pTemplateId + "&pField=" + pField + "&pOperator=" + pOperator + "&pValue=" + pValue + "&pCheckAnd=" + pCheckAnd + "&pCheckOr=" + pCheckOr + "&pSequence=" + pSequence).Result;
                _Status = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _Status;
        }

        public bool removeSearchFilter(string pTemplateId)
        {
            bool _Status = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.BaseAddress);
                HttpResponseMessage response = client.GetAsync("api/main/removeSearchFilter?pTemplateId=" + pTemplateId).Result;
                _Status = bool.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return _Status;
        }

        public bool saveDisplayField(GlobalVariables.Operation pOperation)
        {
            bool _status = false;
            switch (pOperation)
            {
                case GlobalVariables.Operation.Add:
                    //_status = loCommonDAO.insertDisplayFields(this);
                    break;
                default:
                    break;
            }
            return _status;
        }
        public bool removeSearchFields(string pTableName, string pTemplateName)
        {
            bool _Status = false;
            try
            {
                //_Status = loCommonDAO.removeSearchFields(pTableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Status;
        }
        public bool removeTemplateName(string pTableName, string pTemplateName)
        {
            bool _Status = false;
            try
            {
                //_Status = loCommonDAO.removeTemplateName(pTableName, pTemplateName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Status;
        }
        public bool renameTemplateName(string pTableName, string pTemplateName, string pNewTemplateName)
        {
            bool _Status = false;
            try
            {
                //_Status = loCommonDAO.renameTemplateName(pTableName, pTemplateName, pNewTemplateName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Status;
        }
        //filters
        
        public bool saveFilters(GlobalVariables.Operation pOperation)
        {
            bool _status = false;
            switch (pOperation)
            {
                case GlobalVariables.Operation.Add:
                    //_status = loCommonDAO.insertFilters(this);
                    break;
                default:
                    break;
            }
            return _status;
        }
        //groupings
        public DataTable getGroups(string pTableName, string pTemplateName)
        {
            try
            {
                return null;//loCommonDAO.getGroups(pTableName, pTemplateName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool saveGroupings(GlobalVariables.Operation pOperation)
        {
            bool _status = false;
            switch (pOperation)
            {
                case GlobalVariables.Operation.Add:
                    //_status = loCommonDAO.insertGroupings(this);
                    break;
                default:
                    break;
            }
            return _status;
        }
        #endregion "END OF SEARCH"
        
    }
}
