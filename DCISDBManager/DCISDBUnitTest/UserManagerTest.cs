using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DCISDBManager.objLib.Usr;
using DCISDBManager.trnLib.UserManagement;

namespace DCISDBUnitTest
{
    [TestClass]
    public class UserManagerTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            User usr = new User();
            UserManager usm = new UserManager();

            usr.User_ID = "UID1";
            usr.UserGroup_ID = "GRP1";
            usr.Is_Active = "Y";
            usr.Is_Vat = "Y";
            usr.Password_ = "pass123";
            usr.Person_Name = "Roshana Madusanka";
            usr.PassowordExpiry_Date = DateTime.Now.ToString();
            usr.Update_Date = DateTime.Now.ToString();
            usr.Created_By = "Admin";

            bool result = usm.CreateNewUser(usr);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ModifyPassword()
        {
            UserManager usm = new UserManager();

            bool result = usm.ModifyUserPassword("Tharaka", "pass1234");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CheckUserID()
        {
            UserManager usm = new UserManager();

            bool result = usm.CheckUserIDAvailability("UID3");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeactivateUser()
        {
            UserManager usm = new UserManager();

            bool result = usm.ActivateDeactivetUser("UID1", "N");
            Assert.AreEqual(true, result);
        }
    }


}
