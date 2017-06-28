using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class Packagetype
    {




        String PackageType;

        public string Package_Type
        {
            get { return PackageType; }
            set { PackageType = value; }
        }

        String PackageDescription;

        public string Package_Description
        {
            get { return PackageDescription; }
            set { PackageDescription = value; }
        }

        String IsActive;
        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }

        String ModifiedBy;
        public string Modified_By
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }

        String CreatedBy;
        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }




    }
}
