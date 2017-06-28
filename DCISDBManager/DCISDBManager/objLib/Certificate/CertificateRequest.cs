using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class CertificateRequest
    {
         string RequestID;

        public string Request_ID
        {
            get { return RequestID; }
            set { RequestID = value; }
        }

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }

        string CustomerName;

        public string Customer_Name
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        DateTime RecivedDate;

        public DateTime Recived_Date
        {
            get { return RecivedDate; }
            set { RecivedDate = value; }
        }
        int NoOfAttachmments;

        public int No_Of_Attachmments
        {
            get { return NoOfAttachmments; }
            set { NoOfAttachmments = value; }
        }
        string Status;

        public string Status_
        {
            get { return Status; }
            set { Status = value; }
        }

        DateTime CreatedDate;

        public DateTime Created_Date
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        DateTime ModifiedDate;

        public DateTime Modified_Date
        {
            get { return ModifiedDate; }
            set { ModifiedDate = value; }
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
        string IndexNo; //For Ignored Emails.

        public string Index_No
        {
            get { return IndexNo; }
            set { IndexNo = value; }
        }

        string UploadPath;

        public string Upload_Path
        {
            get { return UploadPath; }
            set { UploadPath = value; }
        }

        string InvoiceNo;

        public string Invoice_No
        {
            get { return InvoiceNo; }
            set { InvoiceNo = value; }
        }

        string SealRequired;

        public string Seal_Required
        {
            get { return SealRequired; }
            set { SealRequired = value; }
        }

        public CertificateRequest() {}

        public CertificateRequest(DateTime ReciveDate, string ReqNo, string CID, string CName)
        {
            this.Customer_ID = CID;
            this.Customer_Name = CName;
            this.Recived_Date = ReciveDate;
            this.Request_ID = ReqNo;
        }

        public CertificateRequest(string RequestId, string CustomerId, string CustomerName, DateTime RequestDate,
            string Status, DateTime CreatedDate, string CreatedBy)
        {
            this.Request_ID = RequestId;
            this.Customer_ID = CustomerId;
            this.Customer_Name = CustomerName;
            this.Recived_Date = RequestDate;
            this.Status_ = Status;
            this.Created_Date = CreatedDate;
            this.Created_By = CreatedBy;
        }// For Upload Based Certificate Requests

        System.Data.Linq.ISingleResult<DCISgetPendingUBasedCertRequstResult> CertificateUploadList;

        public System.Data.Linq.ISingleResult<DCISgetPendingUBasedCertRequstResult> CertificateUpload_List
        {
            get { return CertificateUploadList; }
            set { CertificateUploadList = value; }
        }

        System.Data.Linq.ISingleResult<DCISgetPendingWebbasedCertificateDetailsResult> PendignWebBasedCertificateList;

        public System.Data.Linq.ISingleResult<DCISgetPendingWebbasedCertificateDetailsResult> PendignWebBasedCertificate_List
        {
            get { return PendignWebBasedCertificateList; }
            set { PendignWebBasedCertificateList = value; }
        }

        System.Data.Linq.ISingleResult<DCISGETALLPENDINGCERTIFICATEResult> AllPendingCertificateList;

        public System.Data.Linq.ISingleResult<DCISGETALLPENDINGCERTIFICATEResult> AllPendingCertificate_List
        {
            get { return AllPendingCertificateList; }
            set { AllPendingCertificateList = value; }
        }

        System.Data.Linq.ISingleResult<DCISgetCustomerConsigneesResult> DCISgetCustomerConsigneesList;

        public System.Data.Linq.ISingleResult<DCISgetCustomerConsigneesResult> DCISgetCustomerConsignees_List
        {
            get { return DCISgetCustomerConsigneesList; }
            set { DCISgetCustomerConsigneesList = value; }
        }
    }
}
