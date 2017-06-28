using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;


namespace DCISDBManager.trnLib.MasterMaintenance
{
   public class ExportSectorManagement
    {
       public List<ExportSec> getExportSection(string IsActive)
       {
           try
           {

               List<ExportSec> lstGroup = new List<ExportSec>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetExportSectorResult> lst = datacontext.DCISgetExportSector( IsActive);

                   ExportSec grp;

                   foreach (DCISgetExportSectorResult result in lst)
                   {
                       grp = new ExportSec();
                       grp.ExportSector_Name = result.ExportId;
                       grp.ExportSector_Description = result.ExportSector;
                       grp.Is_Active = result.Status;
                       lstGroup.Add(grp);

                   }
               }

               return lstGroup;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }

       public bool ModifyExportSection(ExportSec Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyExportSector(Pt.ExportSector_Name, Pt.ExportSector_Description, Pt.Is_Active);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

       public bool CreateExportSection(ExportSec pt)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetExportSector(pt.ExportSector_Name, pt.Is_Active, pt.ExportSector_Description);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

       public bool ModifyExportSectionStatus(ExportSec Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyExportSectorStatus(Pt.ExportSector_Name,Pt.Is_Active);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

    }
}
