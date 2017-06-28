/*
//PROGRAM-ID.                   CustomerRegistration.cs
//AUTHOR.                             Nipun Munipura
//COMPANY.                         VOTRE IT (Pvt.) Ltd.
 
//DATE-WRITTEN.                                2016-11-08
 
//Version.                               1.0.0
 
//*******************************************************************************
 
//                                Copyright(c) 2016-2017 VOTRE IT Pvt Ltd
 
//                                                        ALL RIGHTS RESERVED
 
//*******************************************************************************
 
//This software is the confidential and proprietary information of VOTRE IT Pvt. Ltd.
 
//("Confidential Information").
 
//You shall not disclose such Confidential Information and shall use it only in
 
//accordance with the terms of the license agreement you entered into with VOTRE IT.
 
//*******************************************************************************
 
//AMENDMENT HISTORY.
 
//===================
 
//  1.  PROGRAMMER   : NIPUN MUNIPURA
 
//      DATE         : 2016-Dec-19
//      Version             : 1.0.1
//      DESCRIPTION  :add new veriable to ModifiCustomerDetails and create new method ModifiCustomerDetailsWithCAdmin
//      the command is:;
 
 

//******************************************************************************
 
//  ABSTRACT ( PROGRAM DESCRIPTION )
 
//  ================================
 
//******************************************************************************
 
//*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.CustomerRequest;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;


namespace DCISDBManager.trnLib.CustomerRequestManagement
{
    public class CustomerApproveManager
    {
        public string setUserDetails(CustomerApproved req)
        {
            string CustomerID = string.Empty;
            try
            {
                string userId = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();
                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        SequenceManager seqmanager = new SequenceManager();
                        Int64 RequestNo = seqmanager.getNextSequence("CustomerID", dbContext);
                        string value = String.Format("{0:D6}", RequestNo);
                        string ExportSector = null;
                        
                        CustomerID = "CUS" + value;
                        dbContext.DCISsetApprove(req.UserID1, req.UserGroupID1, req.Password1, req.CreatedBy1, req.IsActive1,
                            req.PassowordExpiryDate1, CustomerID, req.TemplateId1, req.CustomerName1, req.Telephone1, req.IsSVat1, req.Fax1,
                            req.Email1, req.Address11, req.Address21, req.Address31, req.Status1, req.ContactPersonName1
                            , req.ContactPersonDesignation1, req.ContactPersonDirectPhoneNumber1
                            , req.ContactPersonMobile1, req.ContactPersonEmail1, req.Productdetails1, req.requestNo1, req.NCEMember1, req.Admin1);
                        System.Data.Linq.ISingleResult<DCISgetCustomerExportSectorResult> lst = dbContext.DCISgetCustomerExportSector(CustomerID);

                        foreach (DCISgetCustomerExportSectorResult result in lst)
                        {
                            ExportSector = ExportSector+","+result.ExportSector;
                        }
                        dbContext.DCISsettblCustomerExportSector(CustomerID, ExportSector);
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();


                        return CustomerID;
                    }
                    catch (Exception ex) {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
               
                return null;
            }

        }


        public CustomerApproved getInvoicePrintHeader(string InvoiceNo)
        {
            try
            {
                CustomerApproved req = new CustomerApproved();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceHeaderResult> lst = datacontext.DCISgetInvoiceHeader(InvoiceNo);

                    foreach (DCISgetInvoiceHeaderResult result in lst)
                    {
                        req = new CustomerApproved();

                        req.CustomerName1 = result.CustomerName;
                        req.CustomerId1 = result.CustomerId;
                        req.CreatedDate1 = result.CreatedDate;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                        req.FromDate1 = result.FromDate.ToShortDateString();
                        req.Todate1 = result.ToDate.ToShortDateString();





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


        public CustomerApproved getCustomerDetails(string CustomerId,string GroupId)
        {
            try
            {
                CustomerApproved req = new CustomerApproved();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllCustomerDetailResult> lst = datacontext.DCISgetAllCustomerDetail(CustomerId, GroupId);

                    foreach (DCISgetAllCustomerDetailResult result in lst)
                    {
                        req = new CustomerApproved();

                        req.CustomerName1 = result.CustomerName;
                        req.CustomerId1 = result.CustomerId;
                        req.CreatedDate1 = result.CreatedDate;
                        req.Admin1 = result.ContactPersonName;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                        req.ContactPersonDesignation1 = result.ContactPersonDesignation;
                        req.ContactPersonDirectPhoneNumber1 = result.ContactPersonDirectPhoneNumber;
                        req.ContactPersonEmail1 = result.ContactPersonEmail;
                        req.ContactPersonMobile1 = result.ContactPersonMobile;
                        req.ContactPersonName1 = result.ContactPersonName;
                        req.CreatedBy1 = result.CreatedBy;
                        req.Email1 = result.Email;
                        //req.ExportId1 = result.ExportId;
                       // req.ExportSector1 = result.ExportSector;
                        req.Fax1 = result.Fax;
                        req.NCEMember1 = result.NCEMember;
                        req.Productdetails1 = result.ProductDetails;
                        req.Telephone1 = result.Telephone;
                        req.TemplateId1 = result.TemplateId;
                        req.TemplateName1 = result.TemplateName;
                        req.PaidType1 = result.PaidType;
                        req.IsSVat1 = result.IsSVat;
                        req.UserID1 = result.UserID;
                        req.Admin1 = result.ContactPersonName;
                       
                        
                       


                            


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




        public string ModifiCustomerDetails(CustomerApproved req)
        {
            string CustomerID = string.Empty;
            try
            {
                string userId = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();
                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        dbContext.DCISModifyCustomerDetails(req.CustomerId1, req.TemplateId1, req.CustomerName1, req.Telephone1, req.Fax1,
                            req.Email1, req.Address11, req.Address21, req.Address31, req.ContactPersonName1, req.ContactPersonDesignation1, req.ContactPersonDirectPhoneNumber1
                            , req.ContactPersonMobile1, req.ContactPersonEmail1, req.Productdetails1, req.ExportSector1, req.NCEMember1, req.PaidType1, req.Admin1);
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();


                        return CustomerID;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return null;
            }

        }




        public string ModifiCustomerDetailsWithCAdmin(CustomerApproved req)
        {
            string CustomerID = string.Empty;
            try
            {
                 string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
                 string Password = DECKey.Substring(12);
                string userId = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();
                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        string pass = EncDec.Encrypt(req.NewPassword1, Password);

                        dbContext.DCISModifyCustomerDetailWithCAdmin(req.CustomerId1, req.TemplateId1, req.CustomerName1, req.Telephone1, req.Fax1,
                            req.Email1, req.Address11, req.Address21, req.Address31, req.ContactPersonName1, req.ContactPersonDesignation1, req.ContactPersonDirectPhoneNumber1
                            , req.ContactPersonMobile1, req.ContactPersonEmail1, req.Productdetails1, req.ExportSector1, req.NCEMember1, req.PaidType1, req.Admin1, req.NewUserName1, pass, req.UserID1, req.CreatedBy1);
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();


                        return CustomerID;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return null;
            }

        }


        public List<CustomerApproved> getAllCustomer(string CustomerId, string GroupId)
        {
            try
            {

                List<CustomerApproved> Requests = new List<CustomerApproved>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllCustomerDetailResult> lst = datacontext.DCISgetAllCustomerDetail(CustomerId,GroupId);

                    CustomerApproved req;

                    foreach (DCISgetAllCustomerDetailResult result in lst)
                    {
                        req = new CustomerApproved();
                        req.CustomerName1 = result.CustomerName;
                        req.CustomerId1 = result.CustomerId;
                        req.Address11 = result.Address1;
                        req.Address21 = result.Address2;
                        req.Address31 = result.Address3;
                        req.ContactPersonDesignation1 = result.ContactPersonDesignation;
                        req.ContactPersonDirectPhoneNumber1 = result.ContactPersonDirectPhoneNumber;
                        req.ContactPersonEmail1 = result.ContactPersonEmail;
                        req.ContactPersonMobile1 = result.ContactPersonMobile;
                        req.ContactPersonName1 = result.ContactPersonName;
                        req.CreatedBy1 = result.CreatedBy;
                        req.Email1 = result.Email;
                       // req.ExportSector1 = result.ExportSector;
                        req.Fax1 = result.Fax;
                        req.NCEMember1 = result.NCEMember;
                        req.Productdetails1 = result.ProductDetails;
                        req.Telephone1 = result.Telephone;
                        req.TemplateId1 = result.TemplateId;
                        req.PaidType1 = result.PaidType;
                        req.IsSVat1 = result.IsSVat;
                        req.UserID1 = result.UserID;
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



        public List<CustomerApproved> getAllUserUsingCustomer(string CustomerId,string IsActive)
        {
            try
            {

                List<CustomerApproved> Requests = new List<CustomerApproved>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllUserUsingCustomerIdResult> lst = datacontext.DCISgetAllUserUsingCustomerId(CustomerId,IsActive);

                    CustomerApproved req;

                    foreach (DCISgetAllUserUsingCustomerIdResult result in lst)
                    {
                        req = new CustomerApproved();
                        req.UserID1 = result.UserID;
                        req.UserGroupID1 = result.UserGroupID;
                        req.PersonName1 =result.PersonName;
                        req.CreatedBy1 = result.CreatedBy;
                        req.CreatedDate1 = result.CreatedDate;
                        req.UserGroupName1 = result.GroupName;
                    
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




        public void ModifiUserDetails(CustomerApproved req)
        {
            
            try
            {
                string userId = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();
                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        dbContext.DCISModifyUserUsingCustomer(req.Admin1, req.IsActive1, req.PersonName1);
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                       
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

               
            }

        }

        public bool getCheckUserGroup(string CustomerId,string UserGroupId,string IsActive)
        {
             bool Check = false;
            try
            {
                CustomerApproved req = new CustomerApproved();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISCheckUserGroupResult> lst = datacontext.DCISCheckUserGroup(CustomerId,UserGroupId,IsActive);
                   
                    foreach (DCISCheckUserGroupResult result in lst)
                    {
                        Check = true;
                    }
                }
                
                return Check;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return Check;
            }

        }


        public bool CheckAllCustomerToEdit(string CustomerId,string Company)
        {
            try
            {
                CustomerApproved req = new CustomerApproved();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISCheckAllCustomerToEditResult> lst = datacontext.DCISCheckAllCustomerToEdit(CustomerId, Company);

                    foreach (DCISCheckAllCustomerToEditResult result in lst)
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
    }
}
