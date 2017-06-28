using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Email
{
    public class EmailRequest
    {
        string RequestID;

        public string Request_ID
        {
            get { return RequestID; }
            set { RequestID = value; }
        }


        string InvoiceNo;

        public string Invoice_No
        {
            get { return InvoiceNo; }
            set { InvoiceNo = value; }
        }

        string UserID;

        public string User_ID
        {
            get { return UserID; }
            set { UserID = value; }
        }
        string UserName;

        public string User_Name
        {
            get { return UserName; }
            set { UserName = value; }
        }

        string MailID;

        public string Mail_ID
        {
            get { return MailID; }
            set { MailID = value; }
        }
        string Email;

        public string Email_
        {
            get { return Email; }
            set { Email = value; }
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
        string MailSubject;

        public string Mail_Subject
        {
            get { return MailSubject; }
            set { MailSubject = value; }
        }
        DateTime CeratedDate;

        public DateTime Cerated_Date
        {
            get { return CeratedDate; }
            set { CeratedDate = value; }
        }
        DateTime ModifiedDate;

        public DateTime Modified_Date
        {
            get { return ModifiedDate; }
            set { ModifiedDate = value; }
        }
        string CeratedBy;

        public string Cerated_By
        {
            get { return CeratedBy; }
            set { CeratedBy = value; }
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


        string IsSend;

        public string Is_Send
        {
            get { return IsSend; }
            set { IsSend = value; }
        }
        string CertificateId;

        public string Certificate_Id
        {
            get { return CertificateId; }
            set { CertificateId = value; }
        }
        string CertificatePath;

        public string Certificate_Path
        {
            get { return CertificatePath; }
            set { CertificatePath = value; }
        }

        System.Data.Linq.ISingleResult<DCISgetNotSendEmailCertificatesResult> SendPendingEmails;

        public System.Data.Linq.ISingleResult<DCISgetNotSendEmailCertificatesResult> Send_Pending_Emails
        {
            get { return SendPendingEmails; }
            set { SendPendingEmails = value; }
        }

        public EmailRequest() {}

        public EmailRequest(string Email, string CustomerId)
        {
            this.Customer_ID = CustomerId;
            this.Email_ = Email;
        }

        public EmailRequest(DateTime ReciveDate, string ReqNo, string CID, string CName, string Eml)
        {
            this.Customer_ID = CID;
            this.Customer_Name = CName;
            this.Recived_Date = ReciveDate;
            this.Request_ID = ReqNo;
            this.Email_ = Eml;
        }



    }
}
