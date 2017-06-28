using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.trnLib.ParameterManagement;

namespace DCISDBManager.trnLib.UserManagement
{
    public class UserGroupManager
    {


        public bool CreateNewUserGroup(DCISDBManager.objLib.Usr.UserGroup grp){
            string GroupID = string.Empty;
            try {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                SequenceManager seqmanager = new SequenceManager();
                Int64 gname = seqmanager.getNextSequence("GroupID", datacontext);
                GroupID = "Group-mem" + gname.ToString();


                datacontext.DCISsetUserGroup(GroupID, grp.GroupName1, grp.CreatedBy1, grp.IsActive1, grp.ModifiedBy1);


                return true;
            }catch(Exception ex){
                ErrorLog.LogError(ex);
                return false;
            }
        
        }

        public bool ModifyUserGroup(DCISDBManager.objLib.Usr.UserGroup grp) { 
         try {
             DCISLCDataContext datacontext = new DCISLCDataContext();
             datacontext.DCISModifyUserGroup(grp.GroupId1, grp.GroupName1, grp.IsActive1, grp.ModifiedBy1);
             return true;
         }
         catch (Exception ex)
         {
             ErrorLog.LogError(ex);
             return false;
         }
        }

        public bool ChangeActiveStatusUserGroup(DCISDBManager.objLib.Usr.UserGroup grp) {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyUserGroupStatus(grp.GroupId1, grp.IsActive1, grp.ModifiedBy1);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        
        }

       

        public List<UserGroup> getUserGroup(string GroupId,string IsActive) {
            try
            {

                List<UserGroup> lstGroup = new List<UserGroup>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserGroupResult> lst = datacontext.DCISgetUserGroup(GroupId, IsActive);

                    UserGroup grp;
                    
                    foreach (DCISgetUserGroupResult result in lst)
                    {
                        grp = new UserGroup();
                        grp.GroupId1 = result.GroupId;
                        grp.GroupName1 = result.GroupName;
                        grp.CreatedBy1 = result.CreatedBy;
                        grp.IsActive1 = result.IsActive;
                        grp.ModifiedBy1 = result.ModifiedBy;
                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public List<UserGroup> getSelectedUserGroup(string GroupId, string IsActive)
        {
            try
            {

                List<UserGroup> lstGroup = new List<UserGroup>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSelectedUserGroupResult> lst = datacontext.DCISgetSelectedUserGroup(GroupId, IsActive);

                    UserGroup grp;

                    foreach (DCISgetSelectedUserGroupResult result in lst)
                    {
                        grp = new UserGroup();
                        grp.GroupId1 = result.GroupId;
                        grp.GroupName1 = result.GroupName;
                        grp.CreatedBy1 = result.CreatedBy;
                        grp.IsActive1 = result.IsActive;
                        grp.ModifiedBy1 = result.ModifiedBy;
                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public List<User> getSignataryUsers(string GroupId)
        {
            try
            {

                List<User> lstUsers = new List<User>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSignatoryUsersResult> lst = datacontext.DCISgetSignatoryUsers(GroupId);

                    User user;

                    foreach (DCISgetSignatoryUsersResult result in lst)
                    {
                        user = new User();

                        user.Person_Name = result.PersonName;
                        user.User_ID = result.UserID;

                        lstUsers.Add(user);

                    }
                }

                return lstUsers;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

    }
}
