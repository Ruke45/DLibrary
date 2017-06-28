using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.UserManagement;

namespace DCISDBUnitTest
{
    [TestClass]
    public class UserGroupTest
    {
        [TestMethod]
        public void CreateNewGroup()
        {
            UserGroup ug = new UserGroup();
            UserGroupManager um = new UserGroupManager();

            ug.CreatedBy1 = "Tharaka";
            ug.GroupId1 = "GRP1";
            ug.GroupName1 = "Group1";
            ug.IsActive1 = "Y";


            bool result = um.CreateNewUserGroup(ug);

            Assert.AreEqual(true,result);

           
        }
    }
}
