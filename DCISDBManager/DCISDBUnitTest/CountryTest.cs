using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DCISDBManager.trnLib.MasterDataManagement;
using DCISDBManager.objLib.Master;
using DCISDBManager;

namespace DCISDBUnitTest
{
    [TestClass]
    public class CountryTest
    {
        [TestMethod]
        public void getCountryList()
        {
            Country cntry = new Country();
            CountryManager cm = new CountryManager();
            cntry  = cm.getCountry("SL");
            string countryCode = string.Empty;

            foreach(var tru in cntry.Countryresultset)
            {
                countryCode = tru.CountryCode;
         
            }

            Assert.AreEqual("SL",countryCode);

        }

        [TestMethod]
        public void getPackageList()
        {
            PackageTypeList pkg = new PackageTypeList();
            PackageTypeManager pktm = new PackageTypeManager();
            pkg = pktm.getPackageTypeList("PSL");
            string pkgCode = string.Empty;

            foreach (var tru in pkg.Packageresultset)
            {
                pkgCode = tru.PackageType;

            }

            Assert.AreEqual("PSL", pkgCode);

        }
    }
}
