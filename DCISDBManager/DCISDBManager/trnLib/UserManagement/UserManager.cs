using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.UserManagement
{
    public class UserManager
    {
        static string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        string Password = DECKey.Substring(12);

        public User getUserLogin(string UserId, string txtUpass)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    User usr = new User();
                    string pass = EncDec.Encrypt(txtUpass, Password);
                    
                    System.Data.Linq.ISingleResult<DCISgetUserloginResult> loggedUser = datacontext.DCISgetUserlogin(UserId, pass);
                    foreach (DCISgetUserloginResult result in loggedUser)
                    {
                        if (result.UserId.Equals(UserId) && result.Password.Equals(pass))
                        {
                            usr.User_ID = result.UserId;
                            usr.UserGroup_ID = result.UserGroupID;

                            usr.Is_Active = result.IsActive;
                           // usr.Is_Vat = result.IsVat;
                            usr.PassowordExpiry_Date = result.PassowordExpiryDate.ToString();
                            usr.Person_Name = result.PersonName;
                            usr.Customer_ID = result.CustomerId;
                            if (usr.Is_Active == "N")
                            {
                                return null;
                            }
                            else
                            {
                                return usr;
                            }
                        }
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex.Message.ToString());
                ErrorLog.LogError(ex);
                return null;
            }


        }

        //public bool CreateNewUser(User usr)
        //{

        //    try
        //    {
        //        using (DCISLCDataContext datacontext = new DCISLCDataContext())
        //        {

        //            string pass = EncDec.Encrypt(usr.Password_, Password);

        //            datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
        //            datacontext.DCISsetUserAddN(usr.User_ID, usr.UserGroup_ID, usr.Person_Name, pass, usr.Created_By, usr.Is_Active, usr.PassowordExpiry_DateN);
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.LogError(ex);
        //        return false;
        //    }

        //}
        public bool CreateNewUserN(User usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {

                    string pass = EncDec.Encrypt(usr.Password_, Password);

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUserAddN(usr.User_ID, usr.UserGroup_ID, usr.Person_Name, pass, usr.Created_By, usr.Is_Active, usr.PassowordExpiry_DateN,usr.Customer_ID,usr.Designation_,usr.Email_);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool ModifyUserPassword(string UserId, string usrPass)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                string pass = EncDec.Encrypt(usrPass, Password);
                datacontext.DCISChangeUserPassword(UserId, pass);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public bool CheckUserIDAvailability(string UserId)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetUserIdResult> Uid = datacontext.DCISgetUserId(UserId);

                foreach (DCISgetUserIdResult result in Uid)
                {
                    if (result.UserID.Equals(UserId))
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


        public bool ModifyUser(DCISDBManager.objLib.Usr.User usr)
        {
            string pass = EncDec.Encrypt(usr.Password_, Password);
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyUser(usr.User_ID, usr.UserGroup_ID, usr.Is_Active, usr.Person_Name, pass, usr.Email_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool ModifyUserC(DCISDBManager.objLib.Usr.User usr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyUserC(usr.User_ID, usr.UserGroup_ID, usr.Is_Active, usr.Person_Name, usr.Password_, usr.Email_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool ModifyUserSignature(User usr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISupdateUserSignatureDetails(usr.User_ID,usr.PFX_path,usr.SignatureIMG_path,usr.Created_By);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public bool DeleteUser(DCISDBManager.objLib.Usr.User usr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISDeleteUser(usr.User_ID);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public List<User> getUser(string UserId, string IsActive)
        {
            try
            {

                List<User> lstUser = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserResult> lst = datacontext.DCISgetUser(UserId, IsActive);

                    User usr;

                    foreach (DCISgetUserResult result in lst)
                    {
                        usr = new User();
                        usr.User_ID = result.UserID;
                        usr.UserGroup_ID = result.UserGroupID;
                        usr.Person_Name = result.PersonName;
                        usr.Is_Active = result.IsActive;

                        usr.Password_ = result.Password;
                        usr.Created_By = result.CreatedBy;


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



        public List<User> getUserEdit(string UserId, string IsActive,string grp,string compnayname )
        {
            try
            {

                List<User> lstUser = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserEditResult> lst = datacontext.DCISgetUserEdit(UserId, IsActive, grp, compnayname);

                    User usr;

                    foreach (DCISgetUserEditResult result in lst)
                    {
                        usr = new User();
                        usr.User_ID = result.UserID;
                        usr.UserGroup_ID = result.UserGroupID;
                        usr.Person_Name = result.PersonName;
                        usr.Is_Active = result.IsActive;
                        usr.Email_ = result.Email;
                        usr.Customer_ID = result.CustomerName;
                      //  usr.Template_ID=result.

                        usr.Password_ = result.Password;
                        usr.Created_By = result.CreatedBy;


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




        public List<User> getUserEditC(string UserId, string IsActive, string CusID)
        {
            try
            {

                List<User> lstUser = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserEditCAdminResult> lst = datacontext.DCISgetUserEditCAdmin(UserId, IsActive, CusID);

                    User usr;

                    foreach (DCISgetUserEditCAdminResult result in lst)
                    {
                        usr = new User();
                        usr.User_ID = result.UserID;
                        if (result.Designation == null) {
                            usr.UserGroup_ID = "Not given";
                        
                        }

                        else
                        {
                            usr.UserGroup_ID = result.Designation;

                        }
                        usr.Person_Name = result.PersonName;
                        usr.Is_Active = result.IsActive;
                        usr.Email_ = result.Email;

                        usr.Password_ = result.Password;
                        usr.Created_By = result.CreatedBy;


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


        public User getUserDetails(string UserId, string IsActive, string grp, string compnayname)
        {
            try
            {

                User req = new User();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserEditResult> lst = datacontext.DCISgetUserEdit(UserId, IsActive, grp, compnayname);

                    foreach (DCISgetUserEditResult result in lst)
                    {
                        req = new User();

                        req.Created_By = result.CreatedBy;
                        req.Email_ = result.Email;
                      //  req.User_ID = result.UserId;
                        req.UserGroup_ID = result.UserGroupID;

                        req.Is_Active = result.IsActive;
                        // usr.Is_Vat = result.IsVat;
                        req.PassowordExpiry_Date = result.PassowordExpiryDate.ToString();
                        req.Person_Name = result.PersonName;
                        req.Customer_ID = result.CustomerId;



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
        

        public UserSignature getUserSignatureDetails(string UserId)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetUserSignatureDetailsResult> Uid = datacontext.DCISgetUserSignatureDetails(UserId);
                UserSignature Sign = new UserSignature();
                
                foreach (DCISgetUserSignatureDetailsResult result in Uid)
                {
                    if (result.UserID.Equals(UserId))
                    {
                        Sign.User_ID = result.UserID;
                        Sign.SignatureIMG_Path = result.SignatureIMGPath;
                        Sign.PFX_path = result.PFXpath;
                        return Sign;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

        public User getCustomerTemplate(string userid)
        {
            try
            {
                User usr = new User();
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetCustomerTemplateResult> Uid = datacontext.DCISgetCustomerTemplate(userid);
                UserSignature Sign = new UserSignature();

                foreach (DCISgetCustomerTemplateResult result in Uid)
                {
                    usr.Template_ID = result.TemplateId;
                    usr.Customer_Name = result.CustomerName + "<br />" + result.Address1 + "<br />" + result.Address2 + "<br />" + result.Address3;
                    usr.Telephone_ = result.Telephone;
                    return usr;
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }


        public bool CheckUserPasswordMatching(string UserId, String Password)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetUserPasswordResult> Upd = datacontext.DCISgetUserPassword(UserId, Password);

                foreach (DCISgetUserPasswordResult result in Upd)
                {
                    if (result.Password.Equals(Password))
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

        public bool ActivateDeactivetUser(string UserId, string status)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.DCISDeactivateUser(UserId, status);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }



        public bool setUserSignature(User usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetSignatorySignatureDetails(usr.User_ID,usr.PFX_path,usr.SignatureIMG_path,usr.Created_By);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public String getRandomID(string RequestId)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetRandomIDResult> Upd = datacontext.DCISgetRandomID(RequestId);

                foreach (DCISgetRandomIDResult result in Upd)
                {
                   
                        return result.RandomID;
                    
                }
                return "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return "";
            }
        }


        public bool ModifyRandomNo(User usr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyRandomID(usr.Password_, usr.User_ID);
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
