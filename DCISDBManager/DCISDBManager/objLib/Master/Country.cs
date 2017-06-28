using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class Country
    {

        System.Data.Linq.ISingleResult<DCISgetCountryResult> countryresultset;

        public System.Data.Linq.ISingleResult<DCISgetCountryResult> Countryresultset
        {
            get { return countryresultset; }
            set { countryresultset = value; }
        }
            

       
      /*  string countrycode;

        public string Countrycode
        {
            get { return countrycode; }
            set { countrycode = value; }
        }
        string countryname;

        public string Countryname
        {
            get { return countryname; }
            set { countryname = value; }
        }*/

    }
}
