using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class CertificateDownld
    {

        string UserID;

        public string User_ID
        {
            get { return UserID; }
            set { UserID = value; }
        }

        string RequestId;

        public string Request_Id
        {
            get { return RequestId; }
            set { RequestId = value; }
        }
        string CreatedBy;

        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }

        string IsDownloaded;

        public string Is_Downloaded
        {
            get { return IsDownloaded; }
            set { IsDownloaded = value; }
        }
        string CertificatePath;

        public string Certificate_Path
        {
            get { return CertificatePath; }
            set { CertificatePath = value; }
        }
        string IsValid;

        public string Is_Valid
        {
            get { return IsValid; }
            set { IsValid = value; }
        }
        string CertificateName;

        public string Certificate_Name
        {
            get { return CertificateName; }
            set { CertificateName = value; }
        }
        string Email;

        public string Email_
        {
            get { return Email; }
            set { Email = value; }
        }



        DateTime CreatedDate;

        public DateTime Created_Date
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        DateTime ExpiryDate;

        public DateTime Expiry_Date
        {
            get { return ExpiryDate; }
            set { ExpiryDate = value; }
        }
        string CustomerName;

        public string CustomerName_
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        string RequestedDate;

        public string RequestedDate_
        {
            get { return RequestedDate; }
            set { RequestedDate = value; }
        }
        string CretifiedDate;
        public string CretifiedDate_
        {
            get { return CretifiedDate; }
            set { CretifiedDate = value; }
        }

        string SealRequired;
        public string SealRequired_
        {
            get { return SealRequired; }
            set { SealRequired = value; }
        }

        string Designation;
        public string Designation_
        {
            get { return Designation; }
            set { Designation = value; }
        }

        string PersonName;
        public string PersonName_
        {
            get { return PersonName; }
            set { PersonName = value; }
        }
        string InvoiceId;
        public string InvoiceId_
        {
            get { return InvoiceId; }
            set { InvoiceId = value; }
        }

        string Consignor;
        public string Consignor_
        {
            get { return Consignor; }
            set { Consignor = value; }
        } string Consignee;
        public string Consignee_
        {
            get { return Consignee; }
            set { Consignee = value; }
        }
        string IsUploaded;
        public string Is_Uploaded
        {
            get { return IsUploaded; }
            set { IsUploaded = value; }
        }




        public string Downvery_ { get; set; }
    }
}
