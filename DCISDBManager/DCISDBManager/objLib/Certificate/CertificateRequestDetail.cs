using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class CertificateRequestDetail
    {

        Int64 SeqNo;

        public Int64 SeqNo1
        {
            get { return SeqNo; }
            set { SeqNo = value; }
        }
        string RequestId;

        public string RequestId1
        {
            get { return RequestId; }
            set { RequestId = value; }
        }
        string GoodItem;

        public string GoodItem1
        {
            get { return GoodItem; }
            set { GoodItem = value; }
        }
        string ShippingMark;

        public string ShippingMark1
        {
            get { return ShippingMark; }
            set { ShippingMark = value; }
        }
        string PackageType;

        public string PackageType1
        {
            get { return PackageType; }
            set { PackageType = value; }
        }

        string PackageDescription;

        public string PackageDescription1
        {
            get { return PackageDescription; }
            set { PackageDescription = value; }
        }

        string SummaryDesc;

        public string SummaryDesc1
        {
            get { return SummaryDesc; }
            set { SummaryDesc = value; }
        }
        string Quantity;

        public string Quantity1
        {
            get { return Quantity; }
            set { Quantity = value; }
        }
        string HSCode;

        public string HSCode1
        {
            get { return HSCode; }
            set { HSCode = value; }
        }
        DateTime CreatedDate;

        public DateTime CreatedDate1
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        string CreatedBy;

        public string CreatedBy1
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }

        string GoodDetails;

        public string Good_Details
        {
            get { return GoodDetails; }
            set { GoodDetails = value; }
        }

        string QuantityDetails;

        public string Quantity_Details
        {
            get { return QuantityDetails; }
            set { QuantityDetails = value; }
        }

        string HSCodeDetails;

        public string HSCode_Details
        {
            get { return HSCodeDetails; }
            set { HSCodeDetails = value; }
        }
    }
}
