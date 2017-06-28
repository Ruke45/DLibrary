using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.objLib.Certificate;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.objLib.Master;


namespace DCISDBManager.trnLib.CertificateManagement
{
   public  class DownloadCertificate
    {

       public List<CertificateDownld> getRequestID(string RequestId)
        {
            try
            {

                List<CertificateDownld> lstpackage = new List<CertificateDownld>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRequestIDResult> lst = datacontext.DCISgetRequestID(RequestId);

                    CertificateDownld cd;

                    foreach (DCISgetRequestIDResult result in lst)
                    {
                        cd = new CertificateDownld();
                        cd.User_ID = result.CreatedBy;
                        cd.Certificate_Name = result.CertificateName;
                        cd.Certificate_Path = result.CertificatePath;
                        cd.Downvery_ = result.IsDownloaded;
                        if (result.IsDownloaded == "z" || result.IsDownloaded == "Z")
                        {
                            cd.Is_Downloaded = "Printed";
                        }
                        else
                        {
                            cd.Is_Downloaded = "Not Printed";
                        }
                        cd.Is_Valid = result.IsValid;
                        cd.Request_Id = result.RequestId;
                        cd.CustomerName_ = result.CustomerName;
                        cd.CretifiedDate_ = result.CreatedDate.ToString();
                        cd.RequestedDate_ = result.RequestDate.ToString();

                        
                        lstpackage.Add(cd);

                    }
                }

                return lstpackage;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

       public List<CertificateDownld> getSupportingDocuments(string RequestId)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetSupDocIDResult> lst = datacontext.DCISgetSupDocID(RequestId);

                   CertificateDownld cd;

                   foreach (DCISgetSupDocIDResult result in lst)
                   {
                       cd = new CertificateDownld();
                       cd.Certificate_Path = result.DownloadPath;
                       cd.Request_Id = result.RequestID;
                       cd.Certificate_Name = result.DownloadDocName;
                       cd.Downvery_ = result.SupportingDocID;
                    


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }



       public List<CertificateDownld> getcertificateStatus(string RequestId, string Status, string CusID)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetCuscertificateStatusResult> lst = datacontext.DCISgetCuscertificateStatus(RequestId, Status,CusID);

                   CertificateDownld cd;

                   foreach (DCISgetCuscertificateStatusResult result in lst)
                   {
                       cd = new CertificateDownld();
                      // cd.Certificate_Name = result.CertificateName;
                      // cd.Certificate_Path = result.CertificatePath;
                       cd.Is_Downloaded = result.Status;
                      // cd.Is_Valid = result.IsValid;
                       cd.Request_Id = result.RequestId;


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }


       public List<CertificateDownld> getRequestIDUser(string RequestId, string CustID, string userid)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetRequestIDUserResult> lst = datacontext.DCISgetRequestIDUser(RequestId,CustID,userid);

                   CertificateDownld cd;

                   foreach (DCISgetRequestIDUserResult result in lst)
                   {
                       cd = new CertificateDownld();
                       cd.User_ID = result.CreatedBy;
                       cd.Downvery_ = result.IsDownloaded;
                       cd.Certificate_Name = result.CertificateId;
                       cd.Certificate_Path = result.CertificatePath;
                       if (result.IsDownloaded == "ZY" || result.IsDownloaded == "Z")
                       {
                           cd.Is_Downloaded = "Printed";
                       }
                       else
                       {
                           cd.Is_Downloaded = "Not Printed";
                       }
                       cd.Is_Valid = result.IsValid;

                       cd.Request_Id = result.RequestId;
                       cd.CustomerName_ = result.CustomerName;
                       cd.CretifiedDate_ = result.CreatedDate.ToString();
                       cd.RequestedDate_ = result.RequestDate.ToString();

                       string usernamecne = result.Consignee.Split('<')[0];
                       cd.Consignee_ = usernamecne;
                    //   cd.Consignor_ = result.Consignor;

                      
                       string username = result.Consignor.Split('<')[0];
                       cd.Consignor_ = username;

                       if (result.Consignor == "Not Given" || result.Consignor == "None")
                       {
                           cd.Is_Uploaded = "Upoladed";


                       }
                       else
                       {
                           cd.Is_Uploaded = "Web based Request";

                       }


                       if (result.SealRequired == "True")
                       {
                           cd.SealRequired_ = "Yes";
                       }
                       else
                       {

                           cd.SealRequired_ = "No";
                       }


                       cd.PersonName_ = result.created;
                       if (result.Designation == null)
                       {
                           cd.Designation_ = "not Given";
                       }
                       else
                       {
                           cd.Designation_ = result.Designation;
                       }
                       if (result.InvoiceNo == null)
                       {
                           cd.InvoiceId_ = "not Given";
                       }
                       else
                       {
                           cd.InvoiceId_ = result.InvoiceNo;
                       }

                      


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }

       public List<CertificateDownld> getRequestIDUserDate(string RequestId, string CustID,string startdate, string enddate,string certID,string seal,string invoiceNo)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetRequestIDUserdateResult> lst = datacontext.DCISgetRequestIDUserdate(RequestId, CustID, startdate, enddate, certID, seal, invoiceNo);

                   CertificateDownld cd;

                   foreach (DCISgetRequestIDUserdateResult result in lst)
                   {
                       cd = new CertificateDownld();
                       if (result.SealRequired == "True")
                       {
                           cd.SealRequired_ = "Yes";
                       }
                       else {

                           cd.SealRequired_ = "No";
                       }
                       cd.Downvery_ = result.IsDownloaded;
                       cd.User_ID = result.CreatedBy;
                       cd.Certificate_Name = result.CertificateId;
                       cd.Certificate_Path = result.CertificatePath;
                       if (result.IsDownloaded == "ZY" || result.IsDownloaded == "Z")
                       {
                           cd.Is_Downloaded = "Printed";
                       }
                       else
                       {
                           cd.Is_Downloaded = "Not Printed.";
                       }


                       cd.Is_Valid = result.IsValid;
                       cd.Request_Id = result.RequestId;
                       cd.CustomerName_ = result.CustomerName;
                     //  cd.Consignee_ = result.Consignee;
                    //   cd.Consignor_ = result.Consignor;
                      
                       string Con = result.Consignor.Split('<')[0];
                       cd.Consignor_ = Con;

                       string Con2 = result.Consignee.Split('<')[0];
                       cd.Consignee_ = Con2;

                       if (Con == "Not Given" || Con=="None")
                       {
                           cd.Is_Uploaded = "Upoladed";


                       }
                       else {
                           cd.Is_Uploaded = "Web based Request";
                       
                       }

                       cd.CretifiedDate_ = result.CreatedDate.ToString();
                       cd.RequestedDate_ = result.RequestDate.ToString();

                       cd.PersonName_ = result.created;
                       if (result.Designation == null)
                       {
                           cd.Designation_ = "not Given";
                       }
                       else
                       {
                           cd.Designation_ = result.Designation;
                       }
                       if (result.InvoiceNo == null)
                       {
                           cd.InvoiceId_ = "not Given";
                       }
                       else
                       {
                           cd.InvoiceId_ = result.InvoiceNo;
                       }


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }

       public List<CertificateDownld> getCertificateDetail(string Status, string IsValid)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetCertificateStatusAResult> lst = datacontext.DCISgetCertificateStatusA(Status, IsValid);

                   CertificateDownld cd;

                   foreach (DCISgetCertificateStatusAResult result in lst)
                   {
                       cd = new CertificateDownld();
                       cd.Request_Id = result.RequestId;
                       cd.Certificate_Name = result.CertificateName;
                       cd.Certificate_Path = result.CertificatePath;
                       cd.Is_Downloaded = result.IsDownloaded;
                       cd.Is_Valid = result.IsValid;
                       cd.Request_Id = result.RequestId;


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }


       public List<CertificateDownld> getEmailfromReqID(string Status)
       {
           try
           {

               List<CertificateDownld> lstpackage = new List<CertificateDownld>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetEmailfromReqIDResult> lst = datacontext.DCISgetEmailfromReqID(Status);

                   CertificateDownld cd;

                   foreach (DCISgetEmailfromReqIDResult result in lst)
                   {
                       cd = new CertificateDownld();
                       cd.Email_ = result.Email;
                    


                       lstpackage.Add(cd);

                   }
               }

               return lstpackage;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }


       }


       public String getCertificatePathe(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetRequestIDResult> Upd = datacontext.DCISgetRequestID(RequestId);

               foreach (DCISgetRequestIDResult result in Upd)
               {
                   if (result.RequestId.Equals(RequestId))
                   {
                       return result.CertificatePath;
                   }
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public String getCertificateName(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetRequestIDResult> Upd = datacontext.DCISgetRequestID(RequestId);

               foreach (DCISgetRequestIDResult result in Upd)
               {
                   if (result.RequestId.Equals(RequestId))
                   {
                       return result.CertificateName;
                   }
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }


       public DateTime getCertificateExpireDate(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetRequestIDResult> Upd = datacontext.DCISgetRequestID(RequestId);

               foreach (DCISgetRequestIDResult result in Upd)
               {
                   if (result.RequestId.Equals(RequestId))
                   {
                       return result.ExpiryDate;
                   }
               }
               return DateTime.Today;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return DateTime.Today;
           }
       }

       public bool ModifyCerficateDownload(DCISDBManager.objLib.Certificate.CertificateDownld cd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifytblCertificatedownload(cd.Request_Id, cd.Is_Downloaded);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }


       public bool removeCertificate(DCISDBManager.objLib.Certificate.CertificateDownld cd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifyCertificateValidity(cd.Request_Id, cd.Is_Valid);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }
       public bool removeCertificateReasons(DCISDBManager.objLib.Certificate.CertificateDownld cd)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISsetRemovedCertificate(cd.Request_Id, cd.Created_By, cd.Certificate_Name);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }


       public String getCustIDfrmUserID(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetCustIDfrmUserIDResult> Upd = datacontext.DCISgetCustIDfrmUserID(RequestId);

               foreach (DCISgetCustIDfrmUserIDResult result in Upd)
               {
                   if (result.UserID.Equals(RequestId))
                   {
                       return result.CustomerId;
                   }
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

