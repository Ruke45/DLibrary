using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class Tax
    {

        String TaxId;

        public string Tax_Id
        {
            get { return TaxId; }
            set { TaxId = value; }
        }


        DateTime TaxDate;

        public DateTime Tax_Date
        {
            get { return TaxDate; }
            set { TaxDate = value; }
        }


        string TaxCode;
        public string Tax_Code
        {
            get { return TaxCode; }
            set { TaxCode = value; }
        }


        string TaxName;

        public string Tax_Name
        {
            get { return TaxName; }
            set { TaxName = value; }
        }

        Decimal TaxPercentage;

        public Decimal Tax_Percentage
        {
            get { return TaxPercentage; }
            set { TaxPercentage = value; }
        }

        int TaxPriority;

        public int Tax_Priority
        {
            get { return TaxPriority; }
            set { TaxPriority = value; }
        }

        string CreatedBy;

        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }

        string ModifiedBy;

        public string Modified_By
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }

        string IsActive;

        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }




    }
}
