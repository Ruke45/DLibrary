using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//www.c-sharpcorner.com/uploadfile/9505ae/session-variables-as-objects/
namespace DCISDBManager.objLib.Usr
{
    [Serializable]
    public class UserSession
    {
        private const string UserSessionName = "LoggedUserInfo";

        string UserId;

        public string User_Id
        {
            get { return UserId; }
            set { UserId = value; Save(); }
        }
        string UserGroup;

        public string User_Group
        {
            get { return UserGroup; }
            set { UserGroup = value; Save(); }
        }
        string IsActive;

        public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; Save(); }
        }
        string IsVat;

        public string Is_Vat
        {
            get { return IsVat; }
            set { IsVat = value; Save(); }
        }
        string PasswordExpireDate;

        public string PasswordExpire_Date
        {
            get { return PasswordExpireDate; }
            set { PasswordExpireDate = value; Save(); }
        }

        string PFXpath;

        public string PFX_path
        {
            get { return PFXpath; }
            set { PFXpath = value; Save(); }
        }
        string SignatureIMGPath;

        public string SignatureIMG_Path
        {
            get { return SignatureIMGPath; }
            set { SignatureIMGPath = value; Save(); }
        }
        string PersonName;

        public string Person_Name
        {
            get { return PersonName; }
            set { PersonName = value; Save(); }
        }
        string TemplateID;

        public string Template_ID
        {
            get { return TemplateID; }
            set { TemplateID = value; Save(); }
        }

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value; Save(); }
        }

        string CustomerName;

        public string Customer_Name
        {
            get { return CustomerName; }
            set { CustomerName = value; Save(); }
        }
        string Telephone;

        public string Telephone_
        {
            get { return Telephone; }
            set { Telephone = value; Save(); }
        }

        string CPassword; // Certificate Password

        public string C_Password
        {
            get { return CPassword; }
            set { CPassword = value; Save(); }
        }



        private void CheckExisting()
        {
            if (HttpContext.Current.Session[UserSessionName] == null)
            {
                //Save this instance to the session
                HttpContext.Current.Session[UserSessionName] = this;
                User_Id = "";
                User_Group = "";
                Is_Active = "";
                Is_Vat = "";
                PasswordExpire_Date = "";
                PFX_path = "";
                SignatureIMG_Path = "";
                Person_Name = "";
                Template_ID = "";
                Customer_ID = "";
                Customer_Name = "";
                Telephone = "";
                CPassword = "";
            }
            else
            {
                //Initialize our object based on existing session
                UserSession oInfo = (UserSession)HttpContext.Current.Session[UserSessionName];
                this.User_Id = oInfo.User_Id;
                this.User_Group = oInfo.User_Group;
                this.Is_Active = oInfo.Is_Active;
                this.IsVat = oInfo.Is_Vat;
                this.PasswordExpire_Date = oInfo.PasswordExpire_Date;
                this.SignatureIMG_Path = oInfo.SignatureIMG_Path;
                this.PFX_path = oInfo.PFX_path;
                this.Person_Name = oInfo.Person_Name;
                this.Template_ID = oInfo.Template_ID;
                this.Customer_ID = oInfo.Customer_ID;
                this.Customer_Name = oInfo.Customer_Name;
                this.Telephone = oInfo.Telephone;
                this.C_Password = oInfo.C_Password;
                oInfo = null;
            }
        }

        private void Save()
        {
            //Save our object to the session
            HttpContext.Current.Session[UserSessionName] = this;
        }

        public UserSession()
        {
            this.CheckExisting();
        }
    }
}
