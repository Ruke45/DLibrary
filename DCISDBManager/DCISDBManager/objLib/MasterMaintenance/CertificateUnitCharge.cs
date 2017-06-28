using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class CertificateUnitCharge
    {

        String TemplateId;

        public string Template_Id
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }

        String TemplateName;

        public string Template_Name
        {
            get { return TemplateName; }
            set { TemplateName = value; }
        }




        Decimal UnitChargeValue;

        public Decimal UnitCharge_Value
        {
            get { return UnitChargeValue; }
            set { UnitChargeValue = value; }
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

        DateTime CreatedDate;

        public DateTime Created_Date
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }

        DateTime ModifiedDate;

        public DateTime Modified_Date
        {
            get { return ModifiedDate; }
            set { ModifiedDate = value; }
        }

        String IsActive;
        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }




    }
}
