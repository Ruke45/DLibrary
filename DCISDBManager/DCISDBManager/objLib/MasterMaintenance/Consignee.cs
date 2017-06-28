using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class Consignee
    {
        String Code;
        public string Code_
        {
            get { return Code; }
            set { Code = value; }
        }

        String Description;

        public string Description_
        {
            get { return Description; }
            set { Description = value; }
        }

        String IsActive;
        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }


    }
}
