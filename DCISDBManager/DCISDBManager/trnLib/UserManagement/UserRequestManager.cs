using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DCISDBManager.trnLib.ParameterManagement;


namespace DCISDBManager.trnLib.UserManagement
{
    public class UserRequestManager
    {
        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(12);

        public bool NewUserRequest(UserRequest usr)
        {
            string UsrReqID = string.Empty;
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    string pass = EncDec.Encrypt(usr.Password_, Password);
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    SequenceManager seqmanager = new SequenceManager();
                    Int64 gname = seqmanager.getNextSequence("UsrReqID", datacontext);
                    UsrReqID = "Req-No" + gname.ToString();

                    datacontext.DCISsetUserRequest(UsrReqID, usr.User_Id, usr.Status_, usr.Created_By, usr.Approved_By, pass,usr.Person_Name, usr.UserGroup_ID, usr.Modified_By,usr.Customer_ID);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool ApproveUserRequest(string UserId, string ApprovedBy, string Status)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.DCISsetApproveUserRequest(UserId,Status,ApprovedBy);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public bool CheckUserReqUserIDAvailability(string UserId)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetRequestedUserIdResult> Uid = datacontext.DCISgetRequestedUserId(UserId);

                foreach (DCISgetRequestedUserIdResult result in Uid)
                {
                    if (result.UserId.Equals(UserId))
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



        public List<UserRequest> getCustomerRequest(string Status)
        {
            try
            {

                List<UserRequest> Requests = new List<UserRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserRequestResult> lst = datacontext.DCISgetUserRequest(Status);

                    UserRequest req;

                    foreach (DCISgetUserRequestResult result in lst)
                    {
                        req = new UserRequest();
                      
                        req.UserRequest_Id = result.UserRequestId;
                        req.User_Id = result.UserId;
                        req.Password_ = result.Password;
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

        public UserRequest getUserRequestDetails(string UserRequestId)
        {
            try
            {

                UserRequest req = new UserRequest();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserRequestDetailResult> lst = datacontext.DCISgetUserRequestDetail(UserRequestId);

                    foreach (DCISgetUserRequestDetailResult result in lst)
                    {
                        req = new UserRequest();
                        req.UserRequest_Id = result.UserRequestId;
                        req.Password_ = result.Password;
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

    }
}
