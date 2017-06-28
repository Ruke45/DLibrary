using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;
namespace DCISDBManager.trnLib.MasterMaintainance
{
   public class SupportingDocumentManagement
    {
       public String getSupDocName(string SupID)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetSupDocNameResult> Upd = datacontext.DCISgetSupDocName(SupID);

               foreach (DCISgetSupDocNameResult result in Upd)
               {

                   return result.SupportingDocumentName;
                   
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }
       public List<SupportDocuments> getSupportingDocument(string supportingDocumentId, string createdBy)
        {
            try
            {

                List<SupportDocuments> lstUser = new List<SupportDocuments>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSupportDocumentResult> lst = datacontext.DCISgetSupportDocument(supportingDocumentId,createdBy);

                    SupportDocuments SD;

                    foreach (DCISgetSupportDocumentResult result in lst)
                    {
                        SD = new SupportDocuments();
                        SD.SupportingDocument_Id = result.SupportingDocumentId;
                        SD.SupportingDocument_Name = result.SupportingDocumentName;
                        SD.Created_By = result.CreatedBy;
                        SD.Created_Date = result.CreatedDate;
                        lstUser.Add(SD);

                    }
                }

                return lstUser;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

       public List<SupportDocuments> getSupportingDocumentDownload(string Status, string ReqID,string rereqID)
       {
           try
           {

               List<SupportDocuments> lstSDDoc = new List<SupportDocuments>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetSupportingDocumentDownResult> lst = datacontext.DCISgetSupportingDocumentDown(Status, ReqID, rereqID);

                   SupportDocuments SD;

                   foreach (DCISgetSupportingDocumentDownResult result in lst)
                   {
                       SD = new SupportDocuments();
                       SD.User_Id = result.UserID;
                       
                       SD.SupportingDocument_Id = result.SupportingDocID;
                       SD.SupportingDocument_Name = result.SupportingDocumentName;
                       SD.Request_ID = result.RequestID;
                       SD.Download_Path = result.DownloadPath;
                       SD.Certificate_RequestId = result.CertificateRequestId;
                       SD.Request_By = result.RequestBy;
                       SD.User_Id = result.InvoiceNo;
                      // SD.Request_Date = result.RequestDate.ToString();
                      SD.Request_Date = result.RequestDate.Value.ToString();
                      SD.Approved_Date = result.ApprovedDate.ToString();
                      SD.Approved_BY = result.ApprovedBy;
                      string Con = result.Consignor.Split('<')[0];
                      string Cone = result.Consignee.Split('<')[0];



                      SD.Consignee_ = Cone;
                      SD.Consignor_ = Con;
                      SD.Is_Downloaded = result.IsDownloaded;
                       lstSDDoc.Add(SD);

                   }
               }

               return lstSDDoc;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }


       public List<SupportDocuments> getSupportingDocumentDownloadDate(string Status, string ReqID, string rereqID, string FromDate, string ToDate)
       {
           try
           {

               List<SupportDocuments> lstSDDoc = new List<SupportDocuments>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetSupportingDocumentDownDateResult> lst = datacontext.DCISgetSupportingDocumentDownDate(Status, ReqID, rereqID, FromDate, ToDate);

                   SupportDocuments SD;

                   foreach (DCISgetSupportingDocumentDownDateResult result in lst)
                   {
                       SD = new SupportDocuments();
                       SD.User_Id = result.UserID;

                       SD.SupportingDocument_Id = result.SupportingDocID;
                       SD.SupportingDocument_Name = result.SupportingDocumentName;
                       SD.Request_ID = result.RequestID;
                       SD.Download_Path = result.DownloadPath;
                       SD.Certificate_RequestId = result.CertificateRequestId;
                       SD.Request_By = result.RequestBy;
                       SD.User_Id = result.InvoiceNo;

                       // SD.Request_Date = result.RequestDate.ToString();
                       SD.Request_Date = result.RequestDate.Value.ToString();
                       SD.Approved_Date = result.ApprovedDate.ToString();
                       SD.Approved_BY = result.ApprovedBy;

                       string Con = result.Consignor.Split('<')[0];
                       string Cone = result.Consignee.Split('<')[0];


                       SD.Consignee_ = Cone;
                       SD.Consignor_ = Con;
                       SD.Is_Downloaded = result.IsDownloaded;
                       lstSDDoc.Add(SD);

                   }
               }

               return lstSDDoc;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }
       public bool ModifySupportingDocumentDownload(DCISDBManager.objLib.MasterMaintenance.SupportDocuments sd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifySupDocDownload(sd.Request_ID, sd.Is_Downloaded);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }




       public bool ModifySupportingDocument(DCISDBManager.objLib.MasterMaintenance.SupportDocuments sd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifySupportingDocuments(sd.SupportingDocument_Id, sd.SupportingDocument_Name, sd.Modified_By);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }

       public bool CreateSupportingDocument(DCISDBManager.objLib.MasterMaintenance.SupportDocuments sd)
       {
           string SupDocID = string.Empty;
           try
           {

               DCISLCDataContext datacontext = new DCISLCDataContext();
             
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

              
               SequenceManager seqmanager = new SequenceManager();
               Int64 sdid = seqmanager.getNextSequence("SupDocID", datacontext);
               SupDocID = "SDID" + sdid.ToString();
               datacontext.DCISsetSupportingDocuments(SupDocID, sd.SupportingDocument_Name, sd.Created_By, sd.Is_Active);


               return true;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return false;
           }

       }

       public bool ModifySupportingDocumentStatus(DCISDBManager.objLib.MasterMaintenance.SupportDocuments sd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifySupportingDocumentsStatus(sd.SupportingDocument_Id, sd.Is_Active, sd.Modified_By);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }

       public List<SupportDocuments> getSupportingDocumentn(string supportingDocumentId, string IsActive)
       {
           try
           {

               List<SupportDocuments> lstUser = new List<SupportDocuments>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetSupportDocumentnResult> lst = datacontext.DCISgetSupportDocumentn(supportingDocumentId, IsActive);

                   SupportDocuments SD;

                   foreach (DCISgetSupportDocumentnResult result in lst)
                   {
                       SD = new SupportDocuments();
                       SD.SupportingDocument_Id = result.SupportingDocumentId;
                       SD.SupportingDocument_Name = result.SupportingDocumentName;
                       SD.Created_By = result.CreatedBy;
                       SD.Created_Date = result.CreatedDate;
                       SD.Modified_By = result.ModifiedBy;
                       SD.Is_Active = result.IsActive;
                       lstUser.Add(SD);

                   }
               }

               return lstUser;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }



       public bool SupportingDocumentName(string SName)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetSupprtngDocNameResult> Uid = datacontext.DCISgetSupprtngDocName(SName);

               foreach (DCISgetSupprtngDocNameResult result in Uid)
               {
                   if (result.SupportingDocumentName.Equals(SName))
                   {
                       return true;
                   }
               }
               return false;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return false;
           }
       }
       public List<SupportDocuments> getSupportingDocumentConfig(string supportingDocumentId)
       {
           try
           {

               List<SupportDocuments> lstUser = new List<SupportDocuments>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetSupportDocumentConfigResult> lst = datacontext.DCISgetSupportDocumentConfig(supportingDocumentId);

                   SupportDocuments SD;

                   foreach (DCISgetSupportDocumentConfigResult result in lst)
                   {
                       SD = new SupportDocuments();
                       SD.SupportingDocument_Id = result.SupportingDocId;
                       SD.SupportingDocument_Name = result.SupportingDocumentName;
                       SD.LLX_Cordinates =Convert.ToDouble(result.LLXcordinates);
                       SD.LLY_Cordinates = Convert.ToDouble(result.LLYcordinates);
                       SD.URX_cordinates =Convert.ToDouble (result.URXcordinates);
                       SD.URY_cordinates =Convert.ToDouble (result.URYcordinates);
                   
                       lstUser.Add(SD);

                   }
               }

               return lstUser;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }

       public bool ModifySupportingDocConfig(SupportDocuments SD)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifySupDocconfig(SD.SupportingDocument_Id,

                                                       Convert.ToDecimal(SD.LLX_Cordinates),
                                                       Convert.ToDecimal(SD.LLY_Cordinates),
                                                       Convert.ToDecimal(SD.URX_cordinates),
                                                       Convert.ToDecimal(SD.URY_cordinates));
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }

       public bool setSupportingConfig(SupportDocuments SD)
       {


           try
           {

               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();



               datacontext.DCISsetSupDocConfig(SD.SupportingDocument_Id,
                                                    Convert.ToDecimal(SD.LLX_Cordinates),
                                                    Convert.ToDecimal(SD.LLY_Cordinates),
                                                    Convert.ToDecimal(SD.URX_cordinates),
                                                    Convert.ToDecimal(SD.URY_cordinates));


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
