using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class objParameters
    {
        string ParameterCode;

        public string Parameter_Code
        {
            get { return ParameterCode; }
            set { ParameterCode = value; }
        }
        string ParameterDescription;

        public string Parameter_Description
        {
            get { return ParameterDescription; }
            set { ParameterDescription = value; }
        }
        string ParameterValue;

        public string Parameter_Value
        {
            get { return ParameterValue; }
            set { ParameterValue = value; }
        }

        public objParameters() { }

        public objParameters(string Code, string Value)
        {
            this.Parameter_Code = Code;
            this.Parameter_Value = Value;           
        }
        

    }
}
