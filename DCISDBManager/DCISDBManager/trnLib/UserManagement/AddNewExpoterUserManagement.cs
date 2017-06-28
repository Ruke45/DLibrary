using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.UserManagement
{
    public class AddNewExportUserManagement
    {
        public bool setUserDetails(DCISDBManager.objLib.Usr.AddNewExportUser req)
        {
            try
            {
                string userId = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {  
                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.DCISsetUser(req.UserID1, req.UserGroupID1, req.PersonName1, req.Password1, req.CreatedBy1, req.CreatedDate1, req.IsActive1, req.PassowordExpiryDate1);


                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
               
                return false;
            }

        }



        public List<AddNewExportUser> getUser(string Status)
        {
            try
            {

                List<AddNewExportUser> lstUser = new List<AddNewExportUser>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetPendingUserRequestResult> lst = datacontext.DCISgetPendingUserRequest(Status);

                    AddNewExportUser usr;

                    foreach (DCISgetPendingUserRequestResult result in lst)
                    {
                        usr = new AddNewExportUser();
                        usr.UserID1 = result.UserId;
                        usr.UserRequestId1 = result.UserRequestId;
                        usr.UserGroupName1 = result.GroupName;
                        usr.UserGroupID1 = result.GroupId;
                        usr.PersonName1 = result.PersonName;
                        usr.Password1 = result.Password;
                        usr.CustomerId1 = result.CustomerId;
                        usr.CustomerName1 = result.CustomerName;
                        lstUser.Add(usr);

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

        public bool ApproveUser(DCISDBManager.objLib.Usr.AddNewExportUser usr)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

                    datacontext.Connection.Open();

                    try
                    {
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        datacontext.DCISsetUserApproval(usr.UserID1, usr.UserGroupID1, usr.PersonName1, usr.Password1, usr.CreatedBy1, usr.IsActive1, usr.PassowordExpiryDate1, usr.UserRequestId1, usr.Status1, usr.CustomerId1);
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        datacontext.Transaction.Rollback();
                        return false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public bool RejectUser(DCISDBManager.objLib.Usr.AddNewExportUser usr)
        {
            try{
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

                   
                    try
                    {

                       
                        datacontext.DCISsetRejectUser(usr.UserRequestId1, usr.Status1,usr.RejectReason1);
                       
                        return true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                       
                        return false;
                    }
                }
            }
            catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        return false;
                    }

                
            
        }
       
     }
 }

     