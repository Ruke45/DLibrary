using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Certificate
{
    public class CertificateApproval
    {
        string RequestId;

        public string Request_Id
        {
            get { return RequestId; }
            set { RequestId = value; }
        }

        string CertificateId;

        public string Certificate_Id
        {
            get { return CertificateId; }
            set { CertificateId = value; }
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
        string CertificateName;

        public string Certificate_Name
        {
            get { return CertificateName; }
            set { CertificateName = value; }
        }
        string IsValid;

        public string Is_Valid
        {
            get { return IsValid; }
            set { IsValid = value; }
        }
    }
}
