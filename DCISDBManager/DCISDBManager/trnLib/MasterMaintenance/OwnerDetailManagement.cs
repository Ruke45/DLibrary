using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.MasterMaintenance
{
   public  class OwnerDetailManagement
    {


       public List<OwnerDetailsobj> getOwnerDetails(string IsActive)
       {
           try
           {

               List<OwnerDetailsobj> lsto = new List<OwnerDetailsobj>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetOwnerDetailorderedResult> lst = datacontext.DCISgetOwnerDetailordered(IsActive);

                   OwnerDetailsobj od;

                   foreach (DCISgetOwnerDetailorderedResult result in lst)
                   {
                       od = new OwnerDetailsobj();
                       od.Name_ = result.Name;
                       od.company_ = result.OrganizationName;

                       od.Address1_ = result.Address1;
                       od.Address2_ = result.Address2;
                       od.Address_3 = result.Address3;
                       od.Email_ = result.Email;
                       od.Fax_No = result.FaxNo;
                       od.Image_Urls = result.ImageUrls;
                       od.Telephone_No = result.TelephoneNo;
                       od.Web_Address = result.WebAddress;
                     
                       lsto.Add(od);

                   }
               }

               return lsto;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }



       public bool ModifyOwner(OwnerDetailsobj odo)
       {
           try
           {

               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifyOwnerDetails(odo.company_,odo.id_,odo.Name_,odo.Address1_,odo.Address2_,odo.Address_3,odo.Telephone_No,odo.Email_);


               return true;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return false;
           }
       }



       public String getCutomerName (string CusID)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetCustomerDetailResult> Upd = datacontext.DCISgetCustomerDetail(CusID);

               foreach (DCISgetCustomerDetailResult result in Upd)
               {

                   return result.CustomerName;
                   
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }






       public OwnerDetailsobj getNCEContactPerson()
       {
           try
           {

               OwnerDetailsobj details = new OwnerDetailsobj();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetNCEContactPersonDetailResult> lst = datacontext.DCISgetNCEContactPersonDetail();

                   foreach (DCISgetNCEContactPersonDetailResult result in lst)
                   {
                       details = new OwnerDetailsobj();

                       details.Name_ = result.Name;
                       details.Telephone_No = result.TelephoneNo;
                       details.Fax_No = result.FaxNo;
                       details.Email_ = result.Email;
                       details.Web_Address = result.WebAddress;

                   }

               }

               return details;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }



       public bool ModifyNCEContactPerson(OwnerDetailsobj odo)
       {
           try
           {

               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifyNECContactPersonDetail(odo.Name_,odo.Telephone_No,odo.Email_,odo.Web_Address,odo.Fax_No);


               return true;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return false;
           }
       }


    }
}
