using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Master;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.MasterDataManagement
{
    public class PackageTypeManager
    {


        public PackageTypeList getPackageTypeList(string PackageType)
        {
            try
            {

                PackageTypeList packagelist = new PackageTypeList();
                DCISLCDataContext datacontext = new DCISLCDataContext();

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetPackageTypeResult> lst = datacontext.DCISgetPackageType(PackageType);
                    packagelist.Packageresultset = lst;

                    return packagelist;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }
    }
}
