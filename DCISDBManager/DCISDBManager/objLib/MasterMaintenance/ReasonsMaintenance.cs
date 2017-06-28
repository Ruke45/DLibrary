using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class ReasonsMaintenance
    {
        String RejectCode;

        public string Reject_Code
        {
            get { return RejectCode; }
            set { RejectCode = value; }
        }

        String ReasonName;

        public string Reason_Name
        {
            get { return ReasonName; }
            set { ReasonName = value; }
        }

        String Category;

        public string Category_
        {
            get { return Category; }
            set { Category = value; }
        }

        String IsActive;
        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }

        String CreatedBy;
        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }

        String ModifiedBy;
        public string Modified_By
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }







    }
}
