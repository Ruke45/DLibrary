using DCISDBManager.objLib.ExportSector;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.ExportSectorManagement
{
    public class ExportSectorManager
    {
        public List<ExportSector> getAllExportSector(string status)
        {
            try
            {

                List<ExportSector> Requests = new List<ExportSector>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllExportSectorResult> lst = datacontext.DCISgetAllExportSector(status);

                    ExportSector req;

                    foreach (DCISgetAllExportSectorResult result in lst)
                    {
                        req = new ExportSector();
                        req.ExportId1 = result.ExportId;
                        req.ExportSector1 = result.ExportSector;



                        Requests.Add(req);



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
        public bool UpdateCustomerExportSector(int CustomerExportSectorId, string ExportSectorId,string CustomerId,string status)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                    datacontext.DCISModifyCustomerExportSector(CustomerExportSectorId, ExportSectorId, CustomerId,status);



                    return true;

                }
              ///  return false;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return false;
            }

        }
    }

    }



    