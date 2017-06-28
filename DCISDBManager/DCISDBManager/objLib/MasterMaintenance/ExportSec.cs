using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
  public  class ExportSec
    {

      String ExportSectorName;

      public string ExportSector_Name
        {
            get { return ExportSectorName; }
            set { ExportSectorName = value; }
        }

      String ExportSectorDescription;

      public string ExportSector_Description
        {
            get { return ExportSectorDescription; }
            set { ExportSectorDescription = value; }
        }

        String IsActive;
        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }


    }
}
