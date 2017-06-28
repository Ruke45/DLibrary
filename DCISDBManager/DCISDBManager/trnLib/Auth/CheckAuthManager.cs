using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.CheckAuth
{
    public class CheckAuthManager
    {
        public bool IsUserGroupAuthorised(string GroupId,string FunctionId) {
            try {

                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    string id=null;
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISCheckAuthResult> lst = datacontext.DCISCheckAuth(GroupId, FunctionId);
                    foreach (DCISCheckAuthResult result in lst)
                    {
                      id = result.FunctionId;
                    }
                    if (id != null)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            
        }
    }
}
