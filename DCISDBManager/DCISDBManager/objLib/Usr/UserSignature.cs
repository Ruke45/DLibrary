using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Usr
{
    public class UserSignature
    {
        string PFXpath;

        public string PFX_path
        {
            get { return PFXpath; }
            set { PFXpath = value; }
        }
        string SignatureIMGPath;

        public string SignatureIMG_Path
        {
            get { return SignatureIMGPath; }
            set { SignatureIMGPath = value; }
        }
        string UserID;

        public string User_ID
        {
            get { return UserID; }
            set { UserID = value; }
        }
        string CreatedBy;

        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }
        string CreatedDate;

        public string Created_Date
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
    }
}
