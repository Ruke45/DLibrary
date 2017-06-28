using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Usr
{
    public class User
    {

        string Designation;

        public string Designation_
        {
            get { return Designation; }
            set { Designation = value; }
        }


        string Email;

        public string Email_
        {
            get { return Email; }
            set { Email = value; }
        }





        string UserID;

        public string User_ID
        {
            get { return UserID; }
            set { UserID = value; }
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
        string Password;

        public string Password_
        {
            get { return Password; }
            set { Password = value; }
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
        string UpdateDate;

        public string Update_Date
        {
            get { return UpdateDate; }
            set { UpdateDate = value; }
        }
        string IsActive;

        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }
        string PassowordExpiryDate;

        public string PassowordExpiry_Date
        {
            get { return PassowordExpiryDate; }
            set { PassowordExpiryDate = value; }
        }

        DateTime PassowordExpiryDateN;

        public DateTime PassowordExpiry_DateN
        {
            get { return PassowordExpiryDateN; }
            set { PassowordExpiryDateN = value; }
        }
        string IsVat;

        public string Is_Vat
        {
            get { return IsVat; }
            set { IsVat = value; }
        }
        string TemplateID;

        public string Template_ID
        {
            get { return TemplateID; }
            set { TemplateID = value; }
        }

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value;  }
        }

        string PFXpath;

        public string PFX_path
        {
            get { return PFXpath; }
            set { PFXpath = value; }
        }
        string SignatureIMGpath;

        public string SignatureIMG_path
        {
            get { return SignatureIMGpath; }
            set { SignatureIMGpath = value; }
        }

        string CustomerName;

        public string Customer_Name
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }
        string Telephone;

        public string Telephone_
        {
            get { return Telephone; }
            set { Telephone = value; }
        }

    }
}
