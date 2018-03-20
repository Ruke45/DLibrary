using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class CertificateRequestHeader
    {
        string RequestId;

        public string RequestId1
        {
            get { return RequestId; }
            set { RequestId = value; }
        }

      
        string TemplateId;

        public string TemplateId1
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }

        string TemplateName;

        public string TemplateName1
        {
            get { return TemplateName; }
            set { TemplateName = value; }
        }

        string CustomerId;

        public string CustomerId1
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }
        DateTime RequestDate;

        public DateTime RequestDate1
        {
            get { return RequestDate; }
            set { RequestDate = value; }
        }
        DateTime ModifiedDate;

        public DateTime ModifiedDate1
        {
            get { return ModifiedDate; }
            set { ModifiedDate = value; }
        }
        string ModifiedBy;

        public string ModifiedBy1
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
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
        string Status;

        public string Status1
        {
            get { return Status; }
            set { Status = value; }
        }
        string Consignor;

        public string Consignor1
        {
            get { return Consignor; }
            set { Consignor = value; }
        }
        string Consignee;

        public string Consignee1
        {
            get { return Consignee; }
            set { Consignee = value; }
        }
        string InvoiceNo;

        public string InvoiceNo1
        {
            get { return InvoiceNo; }
            set { InvoiceNo = value; }
        }
        DateTime InvoiceDate;

        public DateTime InvoiceDate1
        {
            get { return InvoiceDate; }
            set { InvoiceDate = value; }
        }
        string CountryCode;

        public string CountryCode1
        {
            get { return CountryCode; }
            set { CountryCode = value; }
        }

        string CountryName;

        public string CountryName1
        {
            get { return CountryName; }
            set { CountryName = value; }
        }

        string LoadingPort;

        public string LoadingPort1
        {
            get { return LoadingPort; }
            set { LoadingPort = value; }
        }
        string PortOfDischarge;

        public string PortOfDischarge1
        {
            get { return PortOfDischarge; }
            set { PortOfDischarge = value; }
        }
        string Vessel;

        public string Vessel1
        {
            get { return Vessel; }
            set { Vessel = value; }
        }
        string PlaceOfDelivery;

        public string PlaceOfDelivery1
        {
            get { return PlaceOfDelivery; }
            set { PlaceOfDelivery = value; }
        }
        string TotalInvoiceValue;

        public string TotalInvoiceValue1
        {
            get { return TotalInvoiceValue; }
            set { TotalInvoiceValue = value; }
        }
        string TotalQuantity;

        public string TotalQuantity1
        {
            get { return TotalQuantity; }
            set { TotalQuantity = value; }
        }
        string CustomerTelephone;

        public string Customer_Telephone
        {
            get { return CustomerTelephone; }
            set { CustomerTelephone = value; }
        }


        System.Data.Linq.ISingleResult<DCISgetCertificateRequestsResult> CertificateRequestsLinq;

        public System.Data.Linq.ISingleResult<DCISgetCertificateRequestsResult> CertificateRequestsList
        {
            get { return CertificateRequestsLinq; }
            set { CertificateRequestsLinq = value; }
        }

        System.Data.Linq.ISingleResult<DCISgetUserCertificateRequestsByResult> UserCertificateRequestsList;

        public System.Data.Linq.ISingleResult<DCISgetUserCertificateRequestsByResult> UserCertificateRequestsList1
        {
            get { return UserCertificateRequestsList; }
            set { UserCertificateRequestsList = value; }
        }
        string CustomerName;

        public string CustomerName1
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }
        string CertificateId;

        public string CertificateId1
        {
            get { return CertificateId; }
            set { CertificateId = value; }
        }
        string NECMember;

        public string NECMember1
        {
            get { return NECMember; }
            set { NECMember = value; }
        }
        string PaidType;

        public string PaidType1
        {
            get { return PaidType; }
            set { PaidType = value; }
        }
        string CertificatePath;

        public string CertificatePath1
        {
            get { return CertificatePath; }
            set { CertificatePath = value; }
        }
        string RateId;

        public string RateId1
        {
            get { return RateId; }
            set { RateId = value; }
        }
        decimal Rate;

        public decimal Rate1
        {
            get { return Rate; }
            set { Rate = value; }
        }
        string SuportingDocId;

        public string SuportingDocId1
        {
            get { return SuportingDocId; }
            set { SuportingDocId = value; }
        }
        string SuportingDocName;

        public string SuportingDocName1
        {
            get { return SuportingDocName; }
            set { SuportingDocName = value; }
        }
        string RateValue;

        public string RateValue1
        {
            get { return RateValue; }
            set { RateValue = value; }
        }
        string CreatedDate2;

        public string CreatedDate21
        {
            get { return CreatedDate2; }
            set { CreatedDate2 = value; }
        }

        string OtherComments;

        public string OtherComments1
        {
            get { return OtherComments; }
            set { OtherComments = value; }
        }

        string OtherDetails;

        public string OtherDetails1
        {
            get { return OtherDetails; }
            set { OtherDetails = value; }
        }
        string CertificateName;

        public string CertificateName1
        {
            get { return CertificateName; }
            set { CertificateName = value; }
        }
        string SCreatedDate;

        public string SCreatedDate1
        {
            get { return SCreatedDate; }
            set { SCreatedDate = value; }
        }
        string SExpiryDate;

        public string SExpiryDate1
        {
            get { return SExpiryDate; }
            set { SExpiryDate = value; }
        }
        string Download;

        public string Download1
        {
            get { return Download; }
            set { Download = value; }
        }
        string DocType;

        public string DocType1
        {
            get { return DocType; }
            set { DocType = value; }
        }
        string Remark;

        public string Remark1
        {
            get { return Remark; }
            set { Remark = value; }
        }

        string RequesterName;

        public string Requester_Name
        {
            get { return RequesterName; }
            set { RequesterName = value; }
        }
        string RequesterDesignation;

        public string Requester_Designation
        {
            get { return RequesterDesignation; }
            set { RequesterDesignation = value; }
        }
        string SrequestDate;

        public string SrequestDate1
        {
            get { return SrequestDate; }
            set { SrequestDate = value; }
        }
        string InvoiceSupDocId;

        public string InvoiceSupDocId1
        {
            get { return InvoiceSupDocId; }
            set { InvoiceSupDocId = value; }
        }
        string GoodItem;

        public string GoodItem1
        {
            get { return GoodItem; }
            set { GoodItem = value; }
        }

        string SealRequired;

        public string Seal_Required
        {
            get { return SealRequired; }
            set { SealRequired = value; }
        }

        bool AddtoRefference;
        public bool Addto_Refference
        {
            get { return AddtoRefference; }
            set { AddtoRefference = value; }
        }

        string SaveTemplateName;
        public string Save_TemplateName
        {
            get { return SaveTemplateName; }
            set { SaveTemplateName = value; }
        }
    }
}
