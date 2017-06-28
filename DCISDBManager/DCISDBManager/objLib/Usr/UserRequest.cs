using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Usr
{
    public class UserRequest
    {
        string UserRequestId;

        public string UserRequest_Id
        {
            get { return UserRequestId; }
            set { UserRequestId = value; }
        }
        string UserId;

        public string User_Id
        {
            get { return UserId; }
            set { UserId = value; }
        }
        string UserGroupID;

        public string UserGroup_ID
        {
            get { return UserGroupID; }
            set { UserGroupID = value; }
        }
        string PersonName;

        public string Person_Name
        {
            get { return PersonName; }
            set { PersonName = value; }
        }
        string Status;

        public string Status_
        {
            get { return Status; }
            set { Status = value; }
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
        string ApprovedBy;

        public string Approved_By
        {
            get { return ApprovedBy; }
            set { ApprovedBy = value; }
        }
        string Password;

        public string Password_
        {
            get { return Password; }
            set { Password = value; }
        }
        string ModifiedBy;

        public string Modified_By
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }
        string ModifiedDate;

        public string Modified_Date
        {
            get { return ModifiedDate; }
            set { ModifiedDate = value; }
        }

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }
    }
}
