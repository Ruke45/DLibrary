using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class ManualCertificate
    {

        string RequestId;

        public string Request_Id
        {
            get { return RequestId; }
            set { RequestId = value; }
        }
        string CustomerId;

        public string Customer_Id
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }

        string Status;

        public string Status_
        {
            get { return Status; }
            set { Status = value; }
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

        string Remarks;

  public string Remarks_
        {
            get { return Remarks; }
            set { Remarks = value; }
        }
    }
}
