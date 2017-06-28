using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Invoice;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.InvoiceManeger
{
    public class InvoiceTaxManager
    {
        public bool setInvoiceTaxDetails(InvoiceTax req)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetInvoiceTax(req.InvoiceNo1,req.TaxCode1,req.Amount1,req.CreatedBy1,req.TaxPercentage1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public List<InvoiceTax> getTaxDetails(string InvoiceNo)
        {
            try
            {

                List<InvoiceTax> Requests = new List<InvoiceTax>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceTaxResult> lst = datacontext.DCISgetInvoiceTax(InvoiceNo);

                    InvoiceTax req;

                    foreach (DCISgetInvoiceTaxResult result in lst)
                    {
                        req = new InvoiceTax();
                        req.TaxCode1 = result.TaxCode;
                        req.TaxPercentage1 = result.TaxPercentage;
                        req.Amount1 = result.Amount;
                        req.TaxName11 = result.TaxName;
                        req.stAmount1 = Math.Round(result.Amount, 2).ToString();

                        Requests.Add(req);
                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
