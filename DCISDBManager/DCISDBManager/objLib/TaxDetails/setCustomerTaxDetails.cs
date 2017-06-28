using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.TaxDetails
{
    public class setCustomerTaxDetails
    {
        String CustomerId;

        public String CustomerId1
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }
        String TaxCode;

        public String TaxCode1
        {
            get { return TaxCode; }
            set { TaxCode = value; }
        }
        string TaxRegistrationNo;

        public string TaxRegistrationNo1
        {
            get { return TaxRegistrationNo; }
            set { TaxRegistrationNo = value; }
        }
        decimal TaxPersentage;

        public decimal TaxPersentage1
        {
            get { return TaxPersentage; }
            set { TaxPersentage = value; }
        }
        string TaxName;

        public string TaxName1
        {
            get { return TaxName; }
            set { TaxName = value; }
        }

        string CreatedBy;

        public string CreatedBy1
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }
        string IsActive;

        public string IsActive1
        {
            get { return IsActive; }
            set { IsActive = value; }
        }

        string RequestId;

        public string RequestId1
        {
            get { return RequestId; }
            set { RequestId = value; }
        }
    }
}
