using DCISDBManager.objLib.CustomerRequest;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace DCISDBManager.trnLib.CustomerRequestManagement
{
    public class CustomerRequestManager
    {
        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(12);

        public string setCustomerRequest(CustomerRequest req)
        {
         try {

            

             string ExporterRequestNo = string.Empty;

             using (DCISLCDataContext dbContext = new DCISLCDataContext())
             {

                 dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                 dbContext.Connection.Open();

                 try
                 {
                     dbContext.Transaction = dbContext.Connection.BeginTransaction();

                     string pass = EncDec.Encrypt(req.AdminPassword1, Password);

                     SequenceManager seqmanager = new SequenceManager();
                     Int64 RequestNo = seqmanager.getNextSequence("ExporterRequestNo", dbContext);
                     ExporterRequestNo = "ERN" + RequestNo.ToString();

                     dbContext.DCISsetCustomerRequest(ExporterRequestNo, req.Name1, req.Telephone1, req.Email1, req.Fax1, req.Status1, req.Address11, req.Address21, req.Address31, req.TemplateId1, req.CreatedBy1, req.SVat1, req.AdminUserId1, pass, req.ContactPersonName1
                         , req.ContactPersonDesignation1, req.ContactPersonDirectPhoneNumber1, req.ContactPersonMobile1, req.ContactPersonEmail1, req.Productdetails1, req.ExportSector1, req.NCEMember1,req.AdminName1);
                  
                   //  ExporterRequestNo, req.Name1,
                     // req.Telephone1, req.Email1, req.Fax1, req.Status1, req.Address11, req.Address21, req.Address31, req.TemplateId1, req.CreatedBy1, req.SVat1, req.AdminUserId1, pass
                     dbContext.SubmitChanges();
                     dbContext.Transaction.Commit();
                 }catch(Exception ex){
                     dbContext.Transaction.Rollback();
                     ErrorLog.LogError(ex);
                 }

                 return ExporterRequestNo;
             }
            }catch(Exception ex){
                 
                Console.WriteLine(ex.Message.ToString());
                ErrorLog.LogError(ex);
                return string.Empty;
            }
        }


        public bool setTemplate(CustomerRequest req)
        {
         try {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetTemplate(req.RequestId1, req.TemplateId1);
                

                return true;
            }catch(Exception ex){
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public List<CustomerRequest> getCustomerRequest(string Status)
        {
            try
            {

                List<CustomerRequest> Requests = new List<CustomerRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerRequestResult> lst = datacontext.DCISgetCustomerRequest(Status);

                    CustomerRequest req;

                    foreach (DCISgetCustomerRequestResult result in lst)
                    {
                        req = new CustomerRequest();
                        req.RequestId1 = result.RequestId;
                        req.Name1 = result.Name;
                        req.Telephone1 = result.Telephone;
                        req.Email1 = result.Email;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                        req.Fax1 = result.Fax;
                        req.AdminUserId1 = result.AdminUserId;
                        req.AdminPassword1 = result.AdminPassword;
                       
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


        public CustomerRequest getRequestDetails(string RequestId)
        {
            try
            {

                CustomerRequest req = new CustomerRequest();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRequestDetailsResult> lst = datacontext.DCISgetRequestDetails(RequestId);

                    foreach (DCISgetRequestDetailsResult result in lst)
                    {
                        req = new CustomerRequest();
                        req.Name1 = result.Name;
                        req.Telephone1 = result.Telephone;
                        req.Email1 = result.Email;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                        req.Fax1 = result.Fax;
                        req.SVat1 = result.SVat;
                        req.TemplateId1 = result.TemplateId;
                        req.AdminUserId1 = result.AdminUserId;
                        req.AdminPassword1 = result.AdminPassword;
                        req.ContactPersonName1 = result.ContactPersonName;
                        req.ContactPersonDesignation1 = result.ContactPersonDesignation;
                        req.ContactPersonDirectPhoneNumber1 = result.ContactPersonDirectPhoneNumber;
                        req.ContactPersonMobile1 = result.ContactPersonMobile;
                        req.ContactPersonEmail1 = result.ContactPersonEmail;
                        req.Productdetails1 = result.ProductDetails;
                      //  req.ExportSectorId1 = result.ExportId;
                       // req.ExportSector1 = result.ExportSector;
                        req.NCEMember1 = result.NCEMember;
                        req.AdminName1 = result.AdminName;
                        req.RegFilePath1 = result.RegistrationLetterPath;
                        req.ReqFilePath1 = result.RequestLetterPath;


                    }
                }

                return req;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public bool setApproveCustomer(CustomerRequest req)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISApproveCustomerRequest(req.RequestId1, req.Status1, req.ModifiedBy1,req.RejectReason1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public CustomerRequest getAdministratorID(string AdminName,string status)
        
        {
            try
            {

                CustomerRequest Admin = new CustomerRequest();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRequestAdminNameResult> lst = datacontext.DCISgetRequestAdminName(AdminName,status);

                    foreach (DCISgetRequestAdminNameResult result in lst)
                    {
                        Admin = new CustomerRequest();

                        Admin.AdminUserId1 = result.ID;
                       
                    }

                }

                return Admin;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
       

        public CustomerRequest getCustomerName(string CustomerName,string status,string pending)
        
        {
            try
            {

                CustomerRequest Admin = new CustomerRequest();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISCheckCustomerResult> lst = datacontext.DCISCheckCustomer(CustomerName, status, pending);

                    foreach (DCISCheckCustomerResult result in lst)
                    {
                        Admin = new CustomerRequest();

                        Admin.AdminUserId1 = result.Name;
                      
                    }

                }

                return Admin;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public bool setCustomerExportSector(string RequestId, string sector, string Status)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetCustomerExportSector(RequestId, sector, Status);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public List<CustomerRequest> getRequestExportSector(string RequestNo)
        {
            try
            {

                List<CustomerRequest> Requests = new List<CustomerRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRequestExportSectorResult> lst = datacontext.DCISgetRequestExportSector(RequestNo);

                    CustomerRequest req;

                    foreach (DCISgetRequestExportSectorResult result in lst)
                    {
                        req = new CustomerRequest();
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


        public bool setCustomerletterPath(string RequestLetterpath, string RegistrationLetterPath, string CustomerId, string RequestId, string ISIN)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetLetterFilePath(RegistrationLetterPath, RequestLetterpath, CustomerId,RequestId,ISIN);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public CustomerRequest getCustomerLetterPath(string CustomerId)
        {
            try
            {

                CustomerRequest details = new CustomerRequest();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUploadRegistrationLetterResult> lst = datacontext.DCISgetUploadRegistrationLetter(CustomerId);

                    foreach (DCISgetUploadRegistrationLetterResult result in lst)
                    {
                        details = new CustomerRequest();

                        details.CustomerName1 = result.CustomerName;
                        details.RegFilePath1 = result.RegistrationLetterPath;
                        details.ReqFilePath1 = result.RequestLetterPath;
                        details.CreatedDate1=result.CreatedDate;

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




        public string CheckLetterTable(string CustomerId)
        {
            try
            {

                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    string id = null;
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISCheckLetterTableResult> lst = datacontext.DCISCheckLetterTable(CustomerId);
                    foreach (DCISCheckLetterTableResult result in lst)
                    {
                        id = result.CustomerId;
                    }
                    if (id != null)
                    {
                        return "Y";
                    }
                    else
                    {
                        return "N";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return "N";
            }

        }
    }
}
