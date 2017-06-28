using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.ParameterManagement
{
   public class NotitificationCount
    {

       public string getCertificateEntryCount(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISCertficateCountResult> Upd = datacontext.DCISCertficateCount(RequestId);

               foreach (DCISCertficateCountResult result in Upd)
               {

                   return result.Column1.ToString();
                   
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public string pendinguserCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISPendingUserCountResult> Upd = datacontext.DCISPendingUserCount();

               foreach (DCISPendingUserCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public string pendingCustomerCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISPendingCustomerCountResult> Upd = datacontext.DCISPendingCustomerCount();

               foreach (DCISPendingCustomerCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public string pendingCertificateRequestCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetPendingCertificateReqCountResult> Upd = datacontext.DCISgetPendingCertificateReqCount();

               foreach (DCISgetPendingCertificateReqCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public string pendingEmailCertificateRequestCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetEmailPendingCertificateReqCountResult> Upd = datacontext.DCISgetEmailPendingCertificateReqCount();

               foreach (DCISgetEmailPendingCertificateReqCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

     
            public string pendinguploadCertificateRequestCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetuploadReqCountResult> Upd = datacontext.DCISgetuploadReqCount();

               foreach (DCISgetuploadReqCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }



            public string DownloadCount(string cus,string userid)
            {
                try
                {
                    DCISLCDataContext datacontext = new DCISLCDataContext();
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetDownloadCountResult> Upd = datacontext.DCISgetDownloadCount(cus, userid);

                    foreach (DCISgetDownloadCountResult result in Upd)
                    {

                        return result.Column1.ToString();

                    }
                    return "";
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ex);
                    return "";
                }
            }
       
          public string supDOcCount(string cus)
            {
                try
                {
                    DCISLCDataContext datacontext = new DCISLCDataContext();
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISsupDocCountResult> Upd = datacontext.DCISsupDocCount(cus);

                    foreach (DCISsupDocCountResult result in Upd)
                    {

                        return result.Column1.ToString();

                    }
                    return "";
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ex);
                    return "";
                }
            }

       public string PendingsupDOcCount()
            {
                try
                {
                    DCISLCDataContext datacontext = new DCISLCDataContext();
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISPendingSupDocCountsResult> Upd = datacontext.DCISPendingSupDocCounts();

                    foreach (DCISPendingSupDocCountsResult result in Upd)
                    {

                        return result.Column1.ToString();

                    }
                    return "";
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ex);
                    return "";
                }
            }

       public string ContactformCount()
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetContactformCountResult> Upd = datacontext.DCISgetContactformCount();

               foreach (DCISgetContactformCountResult result in Upd)
               {

                   return result.Column1.ToString();

               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }


       


    }
}
