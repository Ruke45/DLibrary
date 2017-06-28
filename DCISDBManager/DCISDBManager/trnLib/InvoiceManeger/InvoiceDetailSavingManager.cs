using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Invoice;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.objLib.CustomerRequest;

namespace DCISDBManager.trnLib.InvoiceManeger
{
    public class InvoiceDetailSavingManager
    {

        string invoiceno = null;
        public string setInvoiceHeader(InvoiceDetailSaving req)
        {

            InvoiceDetailSaving Check = new InvoiceDetailSaving();
           
            string RequestId = req.RequestNo1;

                try
                {
                    string InvoiceNo = string.Empty;

                    using (DCISLCDataContext dbContext = new DCISLCDataContext())
                    {


                        dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                        dbContext.Connection.Open();

                        try
                        {
                            if (req.GrossTotal1 != 0)
                            {
                                dbContext.Transaction = dbContext.Connection.BeginTransaction();
                                SequenceManager seqmanager = new SequenceManager();
                                Int64 RequestNo = seqmanager.getNextSequence("InvoiceNo", dbContext);

                                InvoiceNo = "STM" + DateTime.Now.ToString("yy") + String.Format("{0:D9}", RequestNo);
                                invoiceno = InvoiceNo;
                                dbContext.DCISsetInvoiceheader(InvoiceNo, req.CustomerId1, req.FromDate1, req.ToDate1, req.GrossTotal1, req.InvoiceTotal1, req.IsTaxInvoice1, req.CreatedBy1, req.InvoicePrintTime1);

                                dbContext.SubmitChanges();
                                dbContext.Transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContext.Transaction.Rollback();
                            ErrorLog.LogError(ex);
                        }

                        return InvoiceNo;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    ErrorLog.LogError(ex);
                    return string.Empty;

                }
            

        }



        public bool setInvoiceDetails(InvoiceDetailSaving req)
        {



            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetInvoiceDetails(req.RequestNo1,req.UnitCharge1,req.CreatedBy1,req.InvoiceNo1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }



        public List<InvoiceDetailSaving> getInvoiceReport(string Start, string End, string CustomerId)
        {
            try
            {

                List<InvoiceDetailSaving> Requests = new List<InvoiceDetailSaving>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceReportResult> lst = datacontext.DCISgetInvoiceReport(Start, End, CustomerId);

                    InvoiceDetailSaving req;

                    foreach (DCISgetInvoiceReportResult result in lst)
                    {
                        req = new InvoiceDetailSaving();
                        req.InvoiceNo1 = result.InvoiceNo;
                        req.CustomerId1 = result.CustomerId;
                        req.CustomerName1 = result.CustomerName;
                        req.sFromDate1 = result.FromDate.ToShortDateString();
                        req.sToDate1 = result.ToDate.ToShortDateString();
                        req.sCreatedDate1 = result.CreatedDate.ToShortDateString();
                        req.GrossTotal1 = result.GrossTotal;
                        req.InvoiceTotal1 = result.InvoiceTotal;
                        req.sInvoiceTotal1 =Math.Round(result.InvoiceTotal,2).ToString();
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




        public List<InvoiceDetailSaving> getInvoiceDetails(string InvoiceNo)
        {
            try
            {

                List<InvoiceDetailSaving> Requests = new List<InvoiceDetailSaving>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceReportDetailsResult> lst = datacontext.DCISgetInvoiceReportDetails(InvoiceNo);

                    InvoiceDetailSaving req;

                    foreach (DCISgetInvoiceReportDetailsResult result in lst)
                    {
                        req = new InvoiceDetailSaving();
                        req.RequestNo1 = result.RequestNo;
                        req.UnitCharge1 = result.UnitCharge;
                        

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


        public List<InvoiceDetailSaving> getInvoiceBody(string InvoiceNo)
        {
            try
            {

                List<InvoiceDetailSaving> Requests = new List<InvoiceDetailSaving>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceBodyResult> lst = datacontext.DCISgetInvoiceBody(InvoiceNo);

                    InvoiceDetailSaving req;

                    foreach (DCISgetInvoiceBodyResult result in lst)
                    {
                        req = new InvoiceDetailSaving();
                        decimal value = Math.Round(result.UnitCharge, 2);
                        req.CertificateNo1 = result.CertificateId;
                        req.RequestNo1 = result.RequestNo;
                        req.UnitCharge21 = value.ToString();
                        req.Consignee1 = result.Consignee;
                        req.Consignor1 = result.Consignor;
                        req.CreatedDate21 = result.CreatedDate.ToShortDateString();

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

        public InvoiceDetailSaving getInvoiceTaxDetails(string InvoiceNo)
        {
            try
            {

                InvoiceDetailSaving Requests = new InvoiceDetailSaving();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoiceTaxValuesResult> lst = datacontext.DCISgetInvoiceTaxValues(InvoiceNo);

                    

                    foreach (DCISgetInvoiceTaxValuesResult result in lst)
                    {
                        Requests = new InvoiceDetailSaving();
                        Requests.GrossTotal1 = result.GrossTotal;
                        Requests.InvoiceTotal1 = result.InvoiceTotal;
                        Requests.IsTaxInvoice1 = result.IsTaxInvoice;

                        
                       
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


        public InvoiceDetailSaving getInvoicePrintTime(string InvoiceNo)
        {
            try
            {

                InvoiceDetailSaving Requests = new InvoiceDetailSaving();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetInvoicePrintTimeResult> lst = datacontext.DCISgetInvoicePrintTime(InvoiceNo);



                    foreach (DCISgetInvoicePrintTimeResult result in lst)
                    {
                        Requests = new InvoiceDetailSaving();
                        Requests.InvoicePrintTime1 = result.PrintTime;
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


        public bool setInvoicePrintCount(InvoiceDetailSaving data)
        {
            try
            {

                InvoiceDetailSaving Requests = new InvoiceDetailSaving();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetInvoicePrintReson(data.InvoiceNo1,data.InvoicePrintTime1,data.Rason1,data.CreatedBy1);

                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


         public List<InvoiceDetailSaving> getInvoiceCountDetails(string InvoiceNo)
        {
            try
            {

                List<InvoiceDetailSaving> Requests = new List<InvoiceDetailSaving>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCertificateIssueDetailsResult> lst = datacontext.DCISgetCertificateIssueDetails(InvoiceNo);

                    InvoiceDetailSaving req;

                    foreach (DCISgetCertificateIssueDetailsResult result in lst)
                    {
                        req = new InvoiceDetailSaving();
                        req.RowCount1 = Convert.ToInt16(result.certificaterow);
                        req.UnitCharge1 = result.UnitCharge;
                        

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

        public List<InvoiceDetailSaving> getStatementCountDiuringTimePeriod(string Start,string End,string CustomerId)
        {
            try
            {

                List<InvoiceDetailSaving> Requests = new List<InvoiceDetailSaving>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetStatementCountResult> lst = datacontext.DCISgetStatementCount(Start, End,CustomerId);

                    InvoiceDetailSaving req;

                    foreach (DCISgetStatementCountResult result in lst)
                    {
                        req = new InvoiceDetailSaving();
                        req.InvoiceNo1 = result.InvoiceNo;
                        req.sFromDate1 = result.FromDate.ToShortDateString();
                        req.sToDate1 = result.ToDate.ToShortDateString();
                        req.State1 = result.State;

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
