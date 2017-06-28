using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class objResultSet
    {
        string StringValue;

        public string String_Value
        {
            get { return StringValue; }
            set { StringValue = value; }
        }
        Int64 IntegerValue;

        public Int64 Integer_Value
        {
            get { return IntegerValue; }
            set { IntegerValue = value; }
        }
        bool BoolenValue;

        public bool Boolen_Value
        {
            get { return BoolenValue; }
            set { BoolenValue = value; }
        }
    }
}
