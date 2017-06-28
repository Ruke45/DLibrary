using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class PackageTypeList
    {

        System.Data.Linq.ISingleResult<DCISgetPackageTypeResult> packageresultset;

        public System.Data.Linq.ISingleResult<DCISgetPackageTypeResult> Packageresultset
        {
            get { return packageresultset; }
            set { packageresultset = value; }
        }
    }
}
