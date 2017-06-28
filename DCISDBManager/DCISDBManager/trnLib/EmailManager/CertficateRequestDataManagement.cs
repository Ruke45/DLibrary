using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Email;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.objLib.Usr;

namespace DCISDBManager.trnLib.EmailManager
{
   public  class CertficateRequestDataManagement
    {


       public String getuseridfrmreqid(string RequestId)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetUserIDfromReqIDResult> Upd = datacontext.DCISgetUserIDfromReqID(RequestId);

               foreach (DCISgetUserIDfromReqIDResult result in Upd)
               {

                   return result.CreatedBy;
                   
               }
               return "";
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return "";
           }
       }

       public List<EmailCertificateConfig> getRequestDetail(string Peq)
       {
           try
           {

               List<EmailCertificateConfig> lstpackage = new List<EmailCertificateConfig>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetEmialCertificateConfigResult> lst = datacontext.DCISgetEmialCertificateConfig(Peq);

                   EmailCertificateConfig pt;

                   foreach (DCISgetEmialCertificateConfigResult result in lst)
                   {
                       pt = new EmailCertificateConfig();
                       pt.Customer_ID = result.CustomerId;
                       pt.Customer_Name = result.CustomerName;
                       pt.URX_cordinates = Convert.ToDouble(result.URXcordinates);
                       pt.URY_cordinates = Convert.ToDouble(result.URYcordinates);
                       pt.LLX_Cordinates = Convert.ToDouble(result.LLXcordinates);
                       pt.LLY_Cordinates = Convert.ToDouble(result.LLYcordinates);

                       lstpackage.Add(pt);

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

        public bool ModifyCertificateConfig(DCISDBManager.objLib.Email.EmailCertificateConfig Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifycertificateconfig(Pt.Customer_ID,
                                                        Convert.ToDecimal(Pt.LLX_Cordinates), 
                                                        Convert.ToDecimal(Pt.LLY_Cordinates), 
                                                        Convert.ToDecimal(Pt.URX_cordinates), 
                                                        Convert.ToDecimal(Pt.URY_cordinates));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool setCertificateConfig(DCISDBManager.objLib.Email.EmailCertificateConfig Pt)
        {
            

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               


                datacontext.DCISsetCertificateConfig(Pt.Customer_ID,
                                                     Convert.ToDecimal(Pt.LLX_Cordinates),
                                                     Convert.ToDecimal(Pt.LLY_Cordinates), 
                                                     Convert.ToDecimal(Pt.URX_cordinates), 
                                                     Convert.ToDecimal(Pt.URY_cordinates));


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }
        public List<EmailRequest> getUserEmail(string Email)
        {
            try
            {

                List<EmailRequest> lstpackage = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerEmailResult> lst = datacontext.DCISgetCustomerEmail();

                    EmailRequest pt;

                    foreach (DCISgetCustomerEmailResult result in lst)
                    {
                        pt = new EmailRequest();
                        pt.User_ID = result.UserID;
                        pt.Customer_ID = result.CustomerId;
                        pt.Email_ = result.Email;
                        pt.User_Name = result.PersonName;

                        lstpackage.Add(pt);

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
        public List<User> getCustomerUser(string IsActive)
        {
            try
            {

                List<User> lstuser = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerUserResult> lst = datacontext.DCISgetCustomerUser(IsActive);

                    User usr;

                    foreach (DCISgetCustomerUserResult result in lst)
                    {
                        usr = new User();
                        usr.Person_Name = result.CustomerName;
                       
                        usr.User_ID = result.UserID;
                        usr.Customer_ID = result.CustomerId;

                        lstuser.Add(usr);

                    }
                }

                return lstuser;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyUserEmail(DCISDBManager.objLib.Email.EmailCertificateConfig Pt)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyCustomerEmail( Pt.User_ID, Pt.Email_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool setUserEmail(DCISDBManager.objLib.Email.EmailCertificateConfig Pt)
        {


            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();



                datacontext.DCISsetUserEmailC(Pt.User_ID, Pt.Email_, Pt.Customer_ID);


                return true;
            }
            catch (Exception ex)            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public List<EmailRequest> getEmailCustomerRequest(string requestid,string customerid,string status,string Startdate,string Enddate)
        {
            try
            {

                List<EmailRequest> lstpackage = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEmailCustomerReqResult> lst = datacontext.DCISgetEmailCustomerReq(requestid, customerid, status, Startdate, Enddate);

                    EmailRequest pt;

                    foreach (DCISgetEmailCustomerReqResult result in lst)
                    {
                        pt = new EmailRequest();
                        pt.Request_ID = result.RequestId;
                        pt.Customer_ID = result.CustomerId;
                        pt.Status_ = result.Status;
                        pt.Cerated_Date= result.CreatedDate.Value;
                      
                      
                        lstpackage.Add(pt);

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

        public List<EmailRequest> getEmailCustomerRequestn(string requestid, string customerid, string status, string Startdate, string Enddate,string type,string InvoiceNo)
        {
            try
            {

                List<EmailRequest> lstpackage = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEmailCustomerReqTestResult> lst = datacontext.DCISgetEmailCustomerReqTest(requestid, customerid, status, Startdate, Enddate, type, InvoiceNo);

                    EmailRequest pt;

                    foreach (DCISgetEmailCustomerReqTestResult result in lst)
                    {
                        string statuss = null;
                        if (result.Status == "y")
                        {
                            statuss = "yes";

                        }
                        else if (result.Status == "A")
                        {
                            statuss = "Approved";

                        }
                        else if (result.Status == "R")
                        {
                            statuss = "Reject";

                        }
                        else if (result.Status == "P")
                        {
                            statuss = "Pending";

                        }
                        else if (result.Status == "G")
                        {
                            statuss = "Pending";

                        }
                     
                       

                        pt = new EmailRequest();
                        pt.Invoice_No = result.InvoiceNo;
                        pt.User_Name = result.CustomerName;
                        pt.Request_ID = result.RequestId;
                        pt.Customer_ID = result.CustomerId;
                      //  pt.Status_ = result.Status;
                        pt.Cerated_By = result.Method;

                        pt.Status_ = statuss;
                        DateTime a ;
                        if (result.CreatedDate == null)
                        {
                            a = DateTime.Now;

                        }
                        else {

                            a = result.CreatedDate.Value;
                        }
                         pt.Cerated_Date=a;
                        pt.Is_Send = result.ReasonCode;


                        lstpackage.Add(pt);

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
        public List<EmailRequest> getstatusfromInvoice(string requestid, string customerid, string status, string Startdate, string Enddate, string type, string InvoiceNo)
        {
            try
            {

                List<EmailRequest> lstpackage = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetstatusInvoiceDataResult> lst = datacontext.DCISgetstatusInvoiceData(requestid, customerid, status, Startdate, Enddate, type, InvoiceNo);

                    EmailRequest pt;

                    foreach (DCISgetstatusInvoiceDataResult result in lst)
                    {
                        string statuss = null;
                        if (result.Status == "y")
                        {
                            statuss = "yes";

                        }
                        else if (result.Status == "A")
                        {
                            statuss = "Approved";

                        }
                        else if (result.Status == "R")
                        {
                            statuss = "Reject";

                        }
                        else if (result.Status == "P")
                        {
                            statuss = "Pending";

                        }
                        else if (result.Status == "G")
                        {
                            statuss = "Generated";

                        }



                        pt = new EmailRequest();
                        pt.User_Name = result.CustomerName;
                        pt.Request_ID = result.RequestId;
                        pt.Customer_ID = result.CustomerId;
                        //  pt.Status_ = result.Status;
                        pt.Cerated_By = result.Method;

                        pt.Status_ = statuss;
                        DateTime a;
                        if (result.CreatedDate == null)
                        {
                            a = DateTime.Now;

                        }
                        else
                        {

                            a = result.CreatedDate;
                        }
                        pt.Cerated_Date = a;
                        pt.Is_Send = result.ReasonCode;


                        lstpackage.Add(pt);

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

        public String getRejectReason(string reasonCode,string isactive)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetRejectReasonsnResult> Upd = datacontext.DCISgetRejectReasonsn(reasonCode, isactive);

                foreach (DCISgetRejectReasonsnResult result in Upd)
                {

                    return result.ReasonName;
                    
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
