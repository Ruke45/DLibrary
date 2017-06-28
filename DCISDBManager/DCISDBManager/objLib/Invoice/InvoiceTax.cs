using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Invoice
{
    public class InvoiceTax
    {
        string InvoiceNo;

        public string InvoiceNo1
        {
            get { return InvoiceNo; }
            set { InvoiceNo = value; }
        }
        string TaxCode;

        public string TaxCode1
        {
            get { return TaxCode; }
            set { TaxCode = value; }
        }
        decimal Amount;

        public decimal Amount1
        {
            get { return Amount; }
            set { Amount = value; }
        }
        string CreatedBy;

        public string CreatedBy1
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }
        decimal TaxPercentage;

        public decimal TaxPercentage1
        {
            get { return TaxPercentage; }
            set { TaxPercentage = value; }
        }
        string TaxName1;

        public string TaxName11
        {
            get { return TaxName1; }
            set { TaxName1 = value; }
        }
        string stAmount;

        public string stAmount1
        {
            get { return stAmount; }
            set { stAmount = value; }
        }
    }
}
