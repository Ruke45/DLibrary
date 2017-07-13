/*
//PROGRAM-ID.                   CustomerRegistration.cs
//AUTHOR.                             Nipun Munipura
//COMPANY.                         VOTRE IT (Pvt.) Ltd.
 
//DATE-WRITTEN.                                2016-11-08
 
//Version.                               1.0.0
 
//*******************************************************************************
 
//                                Copyright(c) 2016-2017 VOTRE IT Pvt Ltd
 
//                                                        ALL RIGHTS RESERVED
 
//*******************************************************************************
 
//This software is the confidential and proprietary information of VOTRE IT Pvt. Ltd.
 
//("Confidential Information").
 
//You shall not disclose such Confidential Information and shall use it only in
 
//accordance with the terms of the license agreement you entered into with VOTRE IT.
 
//*******************************************************************************
 
//AMENDMENT HISTORY.
 
//===================
 
//  1.  PROGRAMMER   : NIPUN MUNIPURA
 
//      DATE         : 2016-Dec-19
//      Version             : 1.0.1
//      DESCRIPTION  :add function to search from Co number
//      the command is:;
 
 

//******************************************************************************
 
//  ABSTRACT ( PROGRAM DESCRIPTION )
 
//  ================================
 
//******************************************************************************
 
//*/
using DCISDBManager.objLib.Certificate;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.CertificateManagement
{
    public class CertificateManager
    {
        public CertificateRequestHeader getRequestDetails(string CertifiateNo)
        {
            try
            {

                CertificateRequestHeader req = new CertificateRequestHeader();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCertificateStatusResult> lst = datacontext.DCISgetCertificateStatus(CertifiateNo);

                    foreach (DCISgetCertificateStatusResult result in lst)
                    {
                        req = new CertificateRequestHeader();
                       // B.CustomerName,A.InvoiceDate,A.Consignee,A.InvoiceNo,A.TotalInvoiceValue
                        req.CustomerName1 = result.CustomerName;
                        req.InvoiceDate1 = Convert.ToDateTime(result.InvoiceDate);
                        req.Consignee1 = result.Consignee;
                        req.InvoiceNo1 = result.InvoiceNo;
                        req.TotalInvoiceValue1 = result.TotalInvoiceValue;
                        





                    }
                }

                return req;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
              

        }


          public List<CertificateRequestHeader> getAllConsignee(string CustomerId, string Status, DateTime fromdate, DateTime todate,string CoNo)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {


                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetCertificateConsigneeFindDetailsResult> lst = datacontext.DCISgetCertificateConsigneeFindDetails(CustomerId, Status, fromdate, todate, CoNo);
                      foreach (DCISgetCertificateConsigneeFindDetailsResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                          string brack = result.Consignee;
                          string Consigneename = null;
                          if (brack != null)
                          {
                              //Consigneename = brack.Replace("<br/>", ",");
                              string[] brackarray = null;
                              int count = 0;
                           
                              char[] brackchar = { '<' };
                              brackarray = brack.Split(brackchar);
                              for (count = 0; count <= brackarray.Length - 1; count++)
                              {
                                  Consigneename = brackarray[0];
                              }
                          }
                          else {
                              Consigneename = brack;
                          }
                         SRH.Consignee1 = Consigneename;
                          SRH.CustomerName1 = result.CustomerName;
                          SRH.CreatedDate21 = result.CreatedDate.ToString();
                          SRH.Consignor1 = result.Consignor;
                          SRH.InvoiceNo1 = result.InvoiceNo;
                          SRH.CertificateId1 = result.CertificateId;
                          SRH.CertificatePath1 = result.CertificatePath;
                          SRH.RequestDate1 = Convert.ToDateTime(result.RequestDate);
                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }


          public List<CertificateRequestHeader> getReportDetails(string CustomerId, string Status, string Sort, string fromdate, string todate)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {


                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      if (Sort == "ASC")
                      {
                          System.Data.Linq.ISingleResult<DCISgetRepotDetailsASCResult> lst = datacontext.DCISgetRepotDetailsASC(CustomerId, Status, fromdate, todate);
                          foreach (DCISgetRepotDetailsASCResult result in lst)
                          {
                              CertificateRequestHeader SRH = new CertificateRequestHeader();
                              SRH.Consignee1 = result.Consignee;
                              SRH.CustomerName1 = result.CustomerName;
                              // SRH.CreatedDate1 = Convert.ToDateTime(result.CreatedDate);
                              SRH.TotalInvoiceValue1 = result.TotalInvoiceValue;
                              SRH.CertificateId1 = result.CertificateId;
                              SRH.NECMember1 = result.NCEMember;
                              SRH.InvoiceNo1 = result.InvoiceNo;
                              SRH.PortOfDischarge1 = result.PortOfDischarge;
                              SRH.PaidType1 = result.PaidType;
                              SRH.CreatedBy1 = result.CreatedBy;
                              SRH.RequestId1 = result.RequestId;
                              SRH.RequestDate1 =  Convert.ToDateTime(result.RequestDate);
                              SRH.CreatedDate1 = Convert.ToDateTime(result.CreatedDate);
                              DocList.Add(SRH);

                          }
                      }
                      if (Sort == "DESC")
                      {
                          System.Data.Linq.ISingleResult<DCISgetRepotDetailsDESCResult> lst = datacontext.DCISgetRepotDetailsDESC(CustomerId, Status, fromdate, todate);
                          foreach (DCISgetRepotDetailsDESCResult result in lst)
                          {
                              CertificateRequestHeader SRH = new CertificateRequestHeader();
                              SRH.Consignee1 = result.Consignee;
                              SRH.CustomerName1 = result.CustomerName;
                              // SRH.CreatedDate1 = Convert.ToDateTime(result.CreatedDate);
                              SRH.TotalInvoiceValue1 = result.TotalInvoiceValue;
                              SRH.CertificateId1 = result.CertificateId;
                              SRH.NECMember1 = result.NCEMember;
                              SRH.InvoiceNo1 = result.InvoiceNo;
                              SRH.PortOfDischarge1 = result.PortOfDischarge;
                              SRH.PaidType1 = result.PaidType;
                              SRH.CreatedBy1 = result.CreatedBy;
                              SRH.RequestId1 = result.RequestId;
                              SRH.RequestDate1 = Convert.ToDateTime(result.RequestDate);
                              SRH.CreatedDate1 = Convert.ToDateTime(result.CreatedDate);

                              DocList.Add(SRH);

                          }
                      }
                         
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }


          public List<CertificateRequestHeader> getSuuportingDocumentApproval(string CustomerId, string Status, string StartDate, string EndDate, string InvoiceRateId, string OtherRateId, string SupdocInvoiceRateId, string SupDocOtherRateId, string AttachSheetId)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {


                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetSuppotingDocumentPeriodicDetailResult> lst = datacontext.DCISgetSuppotingDocumentPeriodicDetail(CustomerId, Status, StartDate, EndDate, InvoiceRateId, OtherRateId, SupdocInvoiceRateId, SupDocOtherRateId, AttachSheetId);
                      foreach (DCISgetSuppotingDocumentPeriodicDetailResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                          SRH.RequestId1 = result.RequestID;
                          SRH.SuportingDocId1 = result.SupportingDocID;
                          SRH.SuportingDocName1 = result.UploadDocName;
                          SRH.RateId1 = result.RatesId;
                          SRH.Rate1 = result.Rates;
                         

                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }



          public bool setInvoiceRate(CertificateRequestHeader req)
          {
              
              try
              {
                 
                  using (DCISLCDataContext dbContext = new DCISLCDataContext())
                  {
                      dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                      dbContext.DCISsetInvoiceRate(req.CustomerId1, req.InvoiceNo1, req.SuportingDocName1, req.RateId1, req.Rate1, req.CreatedBy1);



                          return true;  
                      
                  }
                  //return false;
              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);

                  return false;
              }

          }


          public List<CertificateRequestHeader> getRateHistory(string InvoiceNo)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {


                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetInvoiceRateHistoryResult> lst = datacontext.DCISgetInvoiceRateHistory(InvoiceNo);
                      foreach (DCISgetInvoiceRateHistoryResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                          SRH.SuportingDocId1 = result.SupportingDocName;
                          SRH.SuportingDocName1 = result.DownloadDocName;
                          SRH.RateId1 = result.RateId;
                          SRH.SrequestDate1 = result.RequestDate.ToString();
                          SRH.Rate1 = result.RateValue;
                          SRH.CreatedDate21 = result.ApprovedDate.ToString();
                          SRH.RateValue1 = Math.Round(result.RateValue, 2).ToString();

                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }

          public CertificateRequestHeader getCertificateDetails(string CertifiateNo)
          {
              try
              {

                  CertificateRequestHeader req = new CertificateRequestHeader();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {
                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetCertificateDetailsResult> lst = datacontext.DCISgetCertificateDetails(CertifiateNo);

                      foreach (DCISgetCertificateDetailsResult result in lst)
                      {
                          req = new CertificateRequestHeader();
                          // B.CustomerName,A.InvoiceDate,A.Consignee,A.InvoiceNo,A.TotalInvoiceValue
                          req.CustomerName1 = result.CustomerName;
                          req.CustomerId1 = result.CustomerId;
                          req.SCreatedDate1 =result.CreatedDate.ToString() ;
                          req.SExpiryDate1 = result.ExpiryDate.ToString();
                          req.CertificateId1 = result.CertificateId;
                          if(result.IsDownloaded=="y" || result.IsDownloaded=="Y"){
                              req.Download1="Download";
                          }
                          else{
                          req.Download1="Not Download";
                          }
                          req.CreatedBy1=result.CreatedBy;
                          req.CertificateName1=result.CertificateName;
                          req.RequestId1=result.RequestId;
                          req.CertificatePath1 = result.CertificatePath;
                          

                      }
                  }

                  return req;
              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  return null;
              }


          }

          public List<CertificateRequestHeader> getAllCertificateCanceldetails(string CustomerId, string Status, string fromdate, string todate, string InvoiceSupDocId,string refNo)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {


                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetAllCertificateCanceldetailsResult> lst = datacontext.DCISgetAllCertificateCanceldetails(CustomerId, Status, fromdate, todate, InvoiceSupDocId,refNo);
                      foreach (DCISgetAllCertificateCanceldetailsResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                         
                          SRH.CustomerName1 = result.CustomerName;
                          SRH.CreatedDate21 = result.CreatedDate.ToString();
                          SRH.DocType1 = result.docTypes;
                          SRH.CertificateId1 = result.CertificateId;
                          SRH.Status1 = result.Invoice;
                          

                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }

          public bool setCertificateCancelation(CertificateRequestHeader req)
          {

              try
              {
                  string remark = null;
                  using (DCISLCDataContext dbContext = new DCISLCDataContext())
                  {
                      dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

                      remark = "Canceled With"+req.CertificateId1;
                      dbContext.DCISsetDocumentCancelation(req.CertificateId1,req.CustomerId1,req.Remark1,req.CreatedBy1,req.DocType1);
                      if (req.DocType1=="C")
                      {
                          System.Data.Linq.ISingleResult<DCISgetSupportingDocUsingCertificateIdResult> lst = dbContext.DCISgetSupportingDocUsingCertificateId(req.CertificateId1, req.InvoiceSupDocId1);
                          foreach (DCISgetSupportingDocUsingCertificateIdResult result in lst)
                          {
                              CertificateRequestHeader SRH = new CertificateRequestHeader();
                              SRH.RequestId1 = result.RequestID;
                              SRH.DocType1 = result.DocType;
                              dbContext.DCISsetDocumentCancelation(SRH.RequestId1, req.CustomerId1, remark, req.CreatedBy1, SRH.DocType1);
                               
                          }
                      }


                      return true;

                  }
                 
              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);

                  return false;
              }

          }

          public List<CertificateRequestHeader> getCancelCertificate(string start, string end, string cutomerId,string refNo)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {

                      string doc = null;
                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetCanceleCertificateResult> lst = datacontext.DCISgetCanceleCertificate(start, end, cutomerId, refNo);
                      foreach (DCISgetCanceleCertificateResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                          SRH.CertificateId1 = result.DocumentId;
                          SRH.CustomerName1 = result.CustomerName;
                          SRH.Remark1 = result.Remark;
                          SRH.CreatedBy1 = result.CancelBy;
                          doc = result.DocumentType;
                          if (doc == "C")
                          {
                              SRH.DocType1 = "Certificate";
                          }
                          if (doc == "O")
                          {
                              SRH.DocType1 = "Other Document";
                          }
                          if (doc == "I")
                          {
                              SRH.DocType1 = "Invoice";
                          }
                          SRH.CreatedDate21 = result.CancelDate.ToShortDateString();


                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }


          public List<CertificateRequestHeader> getSuportingDocumentForCertificate(string status,string CertificateRequestNo)
          {
              try
              {
                  List<CertificateRequestHeader> DocList = new List<CertificateRequestHeader>();
                  using (DCISLCDataContext datacontext = new DCISLCDataContext())
                  {

                      string doc = null;
                      datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                      System.Data.Linq.ISingleResult<DCISgetCertificateSuportingDocumentResult> lst = datacontext.DCISgetCertificateSuportingDocument(status, CertificateRequestNo);
                      foreach (DCISgetCertificateSuportingDocumentResult result in lst)
                      {
                          CertificateRequestHeader SRH = new CertificateRequestHeader();
                          SRH.SuportingDocName1 = result.SupportingDocumentName;
                         
                          DocList.Add(SRH);

                      }
                  }
                  return DocList;

              }
              catch (Exception ex)
              {
                  ErrorLog.LogError(ex);
                  Console.WriteLine(ex.Message.ToString());
                  return null;
              }

          }

    }
}
