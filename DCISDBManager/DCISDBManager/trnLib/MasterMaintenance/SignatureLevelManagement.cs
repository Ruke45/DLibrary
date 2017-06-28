using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.Utility;
using System.Configuration;

namespace DCISDBManager.trnLib.MasterMaintenance
{
    public class SignatureLevelManagement
    {

        public List<SignatureLevels> getSignatureLevels(string UserId, string LevelId,string status)
        {
            try
            {

                List<SignatureLevels> lstUser = new List<SignatureLevels>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSiqnatureLevelsResult> lst = datacontext.DCISgetSiqnatureLevels(UserId, LevelId,status);

                    SignatureLevels SD;

                    foreach (DCISgetSiqnatureLevelsResult result in lst)
                    {
                        SD = new SignatureLevels();
                        SD.User_Id = result.UserId;
                        SD.Level_Id = result.LevelId;
                        SD.Template_Id = result.TemplateId;
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

        public bool ModifySignatureLevel(DCISDBManager.objLib.MasterMaintenance.SignatureLevels sgl)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifySignatureLevels(sgl.Level_Id, sgl.User_Id,sgl.Template_Id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreateSignatureLevels(DCISDBManager.objLib.MasterMaintenance.SignatureLevels sgl)
        {

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetSignatureLevels(sgl.User_Id, sgl.Created_By, sgl.Is_Active, sgl.Level_Id, sgl.Template_Id);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public List<User> getLeveledUser(string UserID, string UserGroupID)
        {
            try
            {

                List<User> lstUser = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetLeveledUserResult> lst = datacontext.DCISgetLeveledUser(UserID, UserGroupID);

                    User UR;

                    foreach (DCISgetLeveledUserResult result in lst)
                    {
                        UR= new User();
                        UR.User_ID = result.UserID;
                        UR.UserGroup_ID = result.UserGroupID;

                        lstUser.Add(UR);

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

        public List<SignatureLevels> getSignatureLevelID(string LevelID)
        {
            try
            {

                List<SignatureLevels> lstUser = new List<SignatureLevels>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSignatureLevelIDResult> lst = datacontext.DCISgetSignatureLevelID(LevelID);

                    SignatureLevels SL;

                    foreach (DCISgetSignatureLevelIDResult result in lst)
                    {
                        SL = new SignatureLevels();
                        SL.Level_Id = result.LevelID;


                        lstUser.Add(SL);

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
        public bool ActivateDeactivetSignatureLevel(string UserId, string status, string TemplateID)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISDeactivatSignatureLevel(UserId, status,TemplateID);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public bool CheckTemplateIDandUserID(string UserID,string TempID,string IsActive)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetSigLevelNameandUserResult> Uid = datacontext.DCISgetSigLevelNameandUser(UserID, TempID, IsActive);

                foreach (DCISgetSigLevelNameandUserResult result in Uid)
                {
                   
                        return true;
                    
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
