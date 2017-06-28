using DCISDBManager.objLib.Parameters;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.ParameterManagement
{
    public class ParameateManager
    {
        public Parameters getEmailPassword(string password)
        {
            try
            {

                Parameters Requests = new Parameters();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetParameaterValuesResult> lst = datacontext.DCISgetParameaterValues(password);



                    foreach (DCISgetParameaterValuesResult result in lst)
                    {
                        Requests = new Parameters();
                        Requests.ParameterDescription1 = result.ParameterDescription;
                        Requests.ParameterValue1 = result.ParameterValue;




                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }



        public bool setEmailDetails(Parameters req)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetParameter(req.ParameterCode1, req.ParameterDescription1, req.ParameterValue1);


                return true;
            }
            catch (Exception ex )
            {
                try{
                
                     DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyParameter(req.ParameterCode1, req.ParameterDescription1, req.ParameterValue1);
                return true;
                }
                catch (Exception ex1) {
                    ErrorLog.LogError(ex1);
                    return false;
               }
                
            }
        }



        public Parameters getOwnerDetail()
        {
            try
            {

                Parameters Requests = new Parameters();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetOwnerDetailResult> lst = datacontext.DCISgetOwnerDetail();



                    foreach (DCISgetOwnerDetailResult result in lst)
                    {
                        Requests = new Parameters();
                        Requests.Address11 = result.Address1;
                        Requests.Address21 = result.Address2;
                        Requests.Address31 = result.Address3;
                        Requests.TelephoneNo1 = result.TelephoneNo;
                        Requests.FaxNo1 = result.FaxNo;
                        Requests.Email1 = result.Email;
                        




                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
