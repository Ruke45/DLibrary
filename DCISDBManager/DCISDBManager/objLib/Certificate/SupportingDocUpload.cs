using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class SupportingDocUpload
    {
        string RequestRefNo;

        public string Request_Ref_No
        {
            get { return RequestRefNo; }
            set { RequestRefNo = value; }
        }
        string DocumentId;

        public string Document_Id
        {
            get { return DocumentId; }
            set { DocumentId = value; }
        }

        string DocumentName;

        public string Document_Name
        {
            get { return DocumentName; }
            set { DocumentName = value; }
        }

        string Remarks;

        public string _Remarks
        {
            get { return Remarks; }
            set { Remarks = value; }
        }
        string UploadedBy; // Also Request By

        public string Uploaded_By
        {
            get { return UploadedBy; }
            set { UploadedBy = value; }
        }
        string UploadedPath;

        public string Uploaded_Path
        {
            get { return UploadedPath; }
            set { UploadedPath = value; }
        }

        Int64 SeqNo;

        public Int64 Seq_No
        {
            get { return SeqNo; }
            set { SeqNo = value; }
        }
        string SupportingDocName;

        public string SupportingDoc_Name
        {
            get { return SupportingDocName; }
            set { SupportingDocName = value; }
        }

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }
        string RequestDate;

        public string Request_Date
        {
            get { return RequestDate; }
            set { RequestDate = value; }
        }
        string Status;

        public string Status_
        {
            get { return Status; }
            set { Status = value; }
        }
        string ApprovedBy;

        public string Approved_By
        {
            get { return ApprovedBy; }
            set { ApprovedBy = value; }
        }
        string ApprovedDate;

        public string Approved_Date
        {
            get { return ApprovedDate; }
            set { ApprovedDate = value; }
        }
        string CertifiedDocPath;

        public string Certified_Doc_Path
        {
            get { return CertifiedDocPath; }
            set { CertifiedDocPath = value; }
        }
        string CertifiedDocName;

        public string Certified_Doc_Name
        {
            get { return CertifiedDocName; }
            set { CertifiedDocName = value; }
        }

        string ExpireDate;

        public string Expire_Date
        {
            get { return ExpireDate; }
            set { ExpireDate = value; }
        }

        bool SignatureRequired;

        public bool Signature_Required
        {
            get { return SignatureRequired; }
            set { SignatureRequired = value; }
        }

        string CertificateRequestID;

        public string Certificate_Request_ID
        {
            get { return CertificateRequestID; }
            set { CertificateRequestID = value; }
        }


        System.Data.Linq.ISingleResult<DCISgetPendingSDocApprovalsResult> SDPendingApprovalList;

        public System.Data.Linq.ISingleResult<DCISgetPendingSDocApprovalsResult> SDPendingApproval_List
        {
            get { return SDPendingApprovalList; }
            set { SDPendingApprovalList = value; }
        }

        System.Data.Linq.ISingleResult<DCISnotUploadedSupportingDocumentsResult> NotUploadedSD;

        public System.Data.Linq.ISingleResult<DCISnotUploadedSupportingDocumentsResult> Not_Uploaded_SD
        {
            get { return NotUploadedSD; }
            set { NotUploadedSD = value; }
        }
    }
}
