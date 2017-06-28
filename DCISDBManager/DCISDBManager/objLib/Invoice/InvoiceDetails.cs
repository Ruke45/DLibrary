using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Invoice
{
    public class InvoiceDetails
    {
        string TemplateId;

        public string TemplateId1
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }
       
        string RequestId;

        public string RequestId1
        {
            get { return RequestId; }
            set { RequestId = value; }
        }
       
        string CustomerId;

        public string CustomerId1
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }
        string CreatedDate;

        public string CreatedDate1
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        string Status;

        public string Status1
        {
            get { return Status; }
            set { Status = value; }
        }
        string CustomerName;

        public string CustomerName1
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }
        string Consignor;

        public string Consignor1
        {
            get { return Consignor; }
            set { Consignor = value; }
        }
        string Consignee;

        public string Consignee1
        {
            get { return Consignee; }
            set { Consignee = value; }
        }

        decimal Rate;

        public decimal Rate1
        {
            get { return Rate; }
            set { Rate = value; }
        }
       
       
    }
}
