using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class RejectResons
    {
        string ReasonCode;

        public string Reason_Code
        {
            get { return ReasonCode; }
            set { ReasonCode = value; }
        }
        string Reason;

        public string Reason_
        {
            get { return Reason; }
            set { Reason = value; }
        }
        string ReasonTypeID;

        public string Reason_Type_ID
        {
            get { return ReasonTypeID; }
            set { ReasonTypeID = value; }
        }
    }
}
