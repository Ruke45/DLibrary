using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Invoice;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.InvoiceManeger
{
   public class InvoiceManager
    {
       public List<InvoiceDetails> getInvoiceDetail(string Status, String StartDate, string EndDate, string CusId, string CetrificateRateId)
       {
           try
           {
               
               List<InvoiceDetails> Requests = new List<InvoiceDetails>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetInvoiceDetailsResult> lst = datacontext.DCISgetInvoiceDetails(Status, StartDate, EndDate,CusId,CetrificateRateId);

                   InvoiceDetails req;

                   foreach (DCISgetInvoiceDetailsResult result in lst)
                   {
                       req = new InvoiceDetails();
                     
                       req.CustomerId1 = result.CustomerId;
                       req.Consignee1 = result.Consignee;
                       req.Consignor1 = result.Consignor;
                       req.CreatedDate1 = result.CreatedDate.ToShortDateString().ToString();
                       req.Rate1 = result.Rates;
                       req.RequestId1 = result.RequestId;
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



       public List<InvoiceDetails> getAllInvoice(string Start, string End,string Status,string CustomerId)
        {
            try
            {

                List<InvoiceDetails> Requests = new List<InvoiceDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllInvoiceResult> lst = datacontext.DCISgetAllInvoice(Start, End, Status, CustomerId);

                    InvoiceDetails req;

                    foreach (DCISgetAllInvoiceResult result in lst)
                    {
                        req = new InvoiceDetails();
                        
                        req.CustomerName1 = result.CustomerName;
                        req.CustomerId1 = result.CustomerId;
                        
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
