using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DCISDBManager.trnLib.CustomerRequestAManagement;
using DCISDBManager.objLib.CustomerRequest;

namespace DCISDBUnitTest
{
    [TestClass]
    public class CustomerRequestTest
    {
        [TestMethod]
        public void setCustomerRequest()
        {
            CustomerRequest cr = new CustomerRequest();
            CustomerRequestManager crm = new CustomerRequestManager();

            cr.Address1 = "Address Unit Test 00022211";
            cr.AdminPassword1 = "password123456";
            cr.AdminUserId1 = "Admin123";
            cr.CreatedBy1 = "UID1";
            cr.Email1 = "admin@gmail.com";
            cr.Fax1 = "0772987651";
            cr.ModifiedBy1 = "Tharaka";
            cr.Name1 = "Nipun Maniperuma";
            cr.RequestId1 = "RQST1";
            cr.Status1 = "N";
            cr.SVat1 = "Y";
            cr.Telephone1 = "0778654321";
            cr.TemplateId1 = "TMPLT1";

            bool result = crm.setCustomerRequest(cr);
            Assert.AreEqual(true,result);
           

        }
    }
}
