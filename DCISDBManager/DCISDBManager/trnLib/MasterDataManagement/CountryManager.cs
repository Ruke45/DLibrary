using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Master;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.MasterDataManagement
{
    public class CountryManager
    {
        public Country getCountry(string CountryCode) {
            try {

             
                Country cntry = new Country();
                DCISLCDataContext datacontext = new DCISLCDataContext();
                

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCountryResult> lst = datacontext.DCISgetCountry(CountryCode);
                    cntry.Countryresultset = lst;
                    return cntry;
            }
            catch(Exception ex){
                ErrorLog.LogError(ex);
                return null;
            }
        
        }
    }
}
