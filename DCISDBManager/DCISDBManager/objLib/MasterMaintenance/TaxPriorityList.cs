using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class TaxPriorityList
    {

       int PriorityNo; 

             public int Priority_No
        {
            get { return PriorityNo; }
            set { PriorityNo = value; }
        }

             String PriorityDescription;
             public string Priority_Description
        {
            get { return PriorityDescription; }
            set { PriorityDescription = value; }
        }




    }
}
