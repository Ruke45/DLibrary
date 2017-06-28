using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.CustomerRequest;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.CustomerRequestManagement
{
   public class CustomerDetailManager
    {
        public CustomerDetails getRequestDetails(string UserId)
        {
            try
            {

                CustomerDetails req = new CustomerDetails();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerDetailResult> lst = datacontext.DCISgetCustomerDetail(UserId);

                    foreach (DCISgetCustomerDetailResult result in lst)
                    {
                        req = new CustomerDetails();
                       
                        req.CustomerName1 = result.CustomerName;
                        req.IsSVat1 = result.IsSVat;
                        req.Email1 = result.Email;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                       




                    }
                }

                return req;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }


        }


        public List<CustomerDetails> getAllCustomer(string Status)
        {
            try
            {

                List<CustomerDetails> Requests = new List<CustomerDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllCustomerResult> lst = datacontext.DCISgetAllCustomer();

                    CustomerDetails req;

                    foreach (DCISgetAllCustomerResult result in lst)
                    {
                        req = new CustomerDetails();
                        req.CustomerId1 = result.CustomerId;
                        req.CustomerName1 = result.CustomerName;
                       
                        Requests.Add(req);



                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }


        }

        public bool setIsVat(CustomerDetails req)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetCustomerIsVat(req.CustomerId1, req.IsSVat1);


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public string setCustomer(CustomerDetails req)
        {
            try
            {
                string CustomerID = string.Empty;

                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    DCISLCDataContext datacontext = new DCISLCDataContext();
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();
                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();
                        SequenceManager seqmanager = new SequenceManager();
                        Int64 RequestNo = seqmanager.getNextSequence("CustomerID", dbContext);
                        string value = String.Format("{0:D6}", RequestNo);
                        
                        CustomerID = "CUS" + value;
                        datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                        datacontext.DCISsetCustomer(CustomerID, req.CustomerName1,req.Telephone1,req.Fax1,req.Email1,req.Address11,req.Address21,req.Address31,req.Status1,req.CreatedBy1);


                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContext.Transaction.Rollback();
                        ErrorLog.LogError(ex);
                    }
                    return CustomerID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ErrorLog.LogError(ex);
                return string.Empty;
            }
        }

        public List<CustomerDetails> getCustomerExportSector(string CustomerId)
        {
            try
            {

                List<CustomerDetails> Requests = new List<CustomerDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerExportSectorResult> lst = datacontext.DCISgetCustomerExportSector(CustomerId);

                    CustomerDetails req;

                    foreach (DCISgetCustomerExportSectorResult result in lst)
                    {
                        req = new CustomerDetails();
                        req.Id1 = result.CustomerExportSectorId;
                        req.ExportId1 = result.ExportId;
                        req.ExportSector1 = result.ExportSector;
                       
                        Requests.Add(req);



                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }


        }

        public string getMemberDetails(string CustomerId)
        {
            try
            {

               
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetMemberStatusResult> Manager = datacontext.DCISgetMemberStatus(CustomerId);

                foreach (DCISgetMemberStatusResult result in Manager)
                {
                   
                        return result.NCEMember;
                    
                }
                return "";

             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return "";
            }
        }

        public string getIsCashOrCerdit(string CustomerId)
        {
            try
            {


                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetISCASHORCRETIDResult> Manager = datacontext.DCISgetISCASHORCRETID(CustomerId);

                foreach (DCISgetISCASHORCRETIDResult result in Manager)
                {

                    return result.PaidType;

                }
                return "";


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return "";
            }
        }

    }
}
