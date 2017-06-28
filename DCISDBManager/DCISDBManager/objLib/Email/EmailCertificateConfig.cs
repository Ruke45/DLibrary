using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Email
{
    public class EmailCertificateConfig
    {

         /*
         * llx(lower left x coordinate) = margin from left.
         * lly(lower left y coordinate) = margin from bottom(bottom of rectangle)
         * urx(upper right x coordinate) = width of article
         * ury(upper right y coordinate) = margin from bottom of upper boundary of article.
         */

        string CustomerID;

        public string Customer_ID
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }

        string UserID;

        public string User_ID
        {
            get { return UserID; }
            set { UserID = value; }
        }
        string Email;

        public string Email_
        {
            get { return Email; }
            set { Email = value; }
        }

        string CustomerName;

        public string Customer_Name
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        double LLXcordinates;

        public double LLX_Cordinates
        {
            get { return LLXcordinates; }
            set { LLXcordinates = value; }
        }
        double LLYcordinates;

        public double LLY_Cordinates
        {
            get { return LLYcordinates; }
            set { LLYcordinates = value; }
        }
        double URXcordinates;

        public double URX_cordinates
        {
            get { return URXcordinates; }
            set { URXcordinates = value; }
        }
        double URYcordinates;

        public double URY_cordinates
        {
            get { return URYcordinates; }
            set { URYcordinates = value; }
        }
    }
}
