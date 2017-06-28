using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class SupportingDocList
    {
        System.Data.Linq.ISingleResult<DCISgetSupportingDOCforRequestResult> SuportingDOC;

        public System.Data.Linq.ISingleResult<DCISgetSupportingDOCforRequestResult> SupportingDOCset
        {
            get { return SuportingDOC; }
            set { SuportingDOC = value; }
        }
    }
}
