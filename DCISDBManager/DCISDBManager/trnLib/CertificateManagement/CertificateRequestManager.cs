using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.objLib.Certificate;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.objLib.Master;

namespace DCISDBManager.trnLib.CertificateManagement
{
    public class CertificateRequestManager
    {
        public objResultSet setCertificateRequest(CertificateRequestHeader hdr, List<CertificateRequestDetail> lstRequestDetail)
        {

            try
            {
                objResultSet Result = new objResultSet();
                string certificatereqno = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        SequenceManager seqmanager = new SequenceManager();
                        Int64 RequestNo = seqmanager.getNextSequence("CertificateRequestNo", dbContext);
                        certificatereqno = "CRN" + RequestNo.ToString();
                        dbContext.DCISsetCertifcateRequestHeader(certificatereqno,
                                                                hdr.TemplateId1,
                                                                hdr.CustomerId1,
                                                                hdr.CreatedBy1,
                                                                hdr.Status1,
                                                                hdr.Consignor1,
                                                                hdr.Consignee1,
                                                                hdr.InvoiceNo1,
                                                                hdr.InvoiceDate1,
                                                                hdr.CountryCode1,
                                                                hdr.LoadingPort1,
                                                                hdr.PortOfDischarge1,
                                                                hdr.Vessel1,
                                                                hdr.PlaceOfDelivery1,
                                                                hdr.TotalInvoiceValue1,
                                                                hdr.TotalQuantity1,
                                                                hdr.OtherComments1,
                                                                hdr.OtherDetails1,
                                                                hdr.Seal_Required);


                        foreach (CertificateRequestDetail certificaterequestdetail in lstRequestDetail)
                        {

                            dbContext.DCISsetCertificateRequestDetails(certificatereqno,
                                certificaterequestdetail.GoodItem1,
                                certificaterequestdetail.ShippingMark1,
                                certificaterequestdetail.PackageType1,
                                certificaterequestdetail.SummaryDesc1,
                                certificaterequestdetail.Quantity1,
                                certificaterequestdetail.HSCode1,
                                certificaterequestdetail.CreatedBy1);

                        }

                        if (hdr.Addto_Refference)
                        {
                            dbContext.DCISsetReffrennceRequest(hdr.Consignee1.Replace("<br />", " - "), hdr.CustomerId1, certificatereqno);
                        }
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        Result.Boolen_Value = true;
                        Result.String_Value = certificatereqno;
                        return Result;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public SupportingDocList getSupportindDOCs(string UserID,string TemplateID)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                SupportingDocList supportingDOClist = new SupportingDocList();
                

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetSupportingDOCforRequestResult> lst = datacontext.DCISgetSupportingDOCforRequest(UserID, TemplateID);
                supportingDOClist.SupportingDOCset = lst;

                return supportingDOClist;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                // Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }


        public bool setSupportingDocumentFRequest(SupportingDocUpload usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetSupportingDocUpload(usr.Request_Ref_No, usr.Document_Id, usr._Remarks, usr.Uploaded_By, usr.Uploaded_Path,usr.Document_Name,usr.Signature_Required.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public bool setUpdateSupportingDocumentFRequest(SupportingDocUpload usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateSupportingDoc(usr.Uploaded_By,usr.Uploaded_Path,usr.Document_Name,usr.Seq_No);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool setUpdateSupportingDocumenSignature(Int64 Seq,bool IsRequired,string Remark)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetSupportingDocSignatureRequired(Seq,IsRequired.ToString(),Remark);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public CertificateRequestHeader DCISgetCertificateRequestsByUser(string UserId, string Status)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                CertificateRequestHeader CRH = new CertificateRequestHeader();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetUserCertificateRequestsByResult> resultlist = datacontext.DCISgetUserCertificateRequestsBy(UserId, Status);
              //  System.Data.Linq.ISingleResult<DCISgetUserCertificateRequestsByResult> lst1 = datacontext.DCISgetUserCertificateRequestsBy(UserId, Status);
                CRH.UserCertificateRequestsList1 = resultlist;

                return CRH;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }


        public CertificateRequestHeader getRequestedCertificates(string CustomerId,string Status)
        {
              DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                CertificateRequestHeader CRH = new CertificateRequestHeader();
                

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCertificateRequestsResult> lst = datacontext.DCISgetCertificateRequests(CustomerId,Status);
                    CRH.CertificateRequestsList = lst;

                    return CRH;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }



        public CertificateRequest GetAllPendingCertificates(string CustomerId)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                CertificateRequest CRH = new CertificateRequest();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISGETALLPENDINGCERTIFICATEResult> lst = datacontext.DCISGETALLPENDINGCERTIFICATE(CustomerId);
                CRH.AllPendingCertificate_List = lst;

                return CRH;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }

        public bool updateCertificateRequest(CertificateRequestHeader hdr, List<CertificateRequestDetail> lstRequestDetail)
        {

            try
            {
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();
                        dbContext.DCISupdateCertifiRqustHdr(hdr.RequestId1,
                                                                hdr.ModifiedBy1,
                                                                hdr.Consignor1,
                                                                hdr.Consignee1,
                                                                hdr.InvoiceNo1,
                                                                hdr.InvoiceDate1,
                                                                hdr.CountryCode1,
                                                                hdr.LoadingPort1,
                                                                hdr.PortOfDischarge1,
                                                                hdr.Vessel1,
                                                                hdr.PlaceOfDelivery1,
                                                                hdr.TotalInvoiceValue1,
                                                                hdr.TotalQuantity1,
                                                                hdr.OtherComments1,
                                                                hdr.OtherDetails1,
                                                                hdr.Seal_Required);


                        foreach (CertificateRequestDetail certificaterequestdetail in lstRequestDetail)
                        {

                            dbContext.DCISupdateCertiReqDetail(certificaterequestdetail.SeqNo1,
                                certificaterequestdetail.GoodItem1,
                                certificaterequestdetail.ShippingMark1,
                                certificaterequestdetail.PackageType1,
                                certificaterequestdetail.SummaryDesc1,
                                certificaterequestdetail.Quantity1,
                                certificaterequestdetail.HSCode1);

                        }


                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }


        }


        public CertificateRequestHeader getRequestByID(string ID)
        {
            
            try
            {
               using(DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                CertificateRequestHeader CRH = new CertificateRequestHeader();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetCRequestHeaderDetailsResult> lst = datacontext.DCISgetCRequestHeaderDetails(ID);
                foreach (DCISgetCRequestHeaderDetailsResult result in lst)
                {
                    CRH.Consignee1 = result.Consignee;
                    CRH.Consignor1 = result.Consignor;
                    CRH.InvoiceDate1 = Convert.ToDateTime(result.InvoiceDate);
                    CRH.InvoiceNo1 = result.InvoiceNo;
                    CRH.LoadingPort1 = result.LoadingPort;
                    CRH.PlaceOfDelivery1 = result.PlaceOfDelivery;
                    CRH.PortOfDischarge1 = result.PortOfDischarge;
                    CRH.RequestId1 = result.RequestId;
                    CRH.Status1 = result.Status;
                    CRH.TotalInvoiceValue1 = result.TotalInvoiceValue.ToString();
                    CRH.TotalQuantity1 = result.TotalQuantity;
                    CRH.Vessel1 = result.Vessel;
                    CRH.CountryName1 = result.CountryName;
                    CRH.CountryCode1 = result.CountryCode;
                    CRH.TemplateId1 = result.TemplateId;
                    CRH.TemplateName1 = result.TemplateName;
                    CRH.RequestDate1 = result.RequestDate;
                    CRH.CustomerName1 = result.CustomerName;
                    CRH.Customer_Telephone = result.CustomerTelephone;
                    CRH.Status1 = result.Status;
                    CRH.CustomerId1 = result.CustomerId;
                    CRH.OtherComments1 = result.OtherComments;
                    CRH.OtherDetails1 = result.OtherDetails;
                    CRH.Requester_Designation = result.Designation;
                    CRH.Requester_Name = result.PersonName;
                    CRH.Seal_Required = result.SealRequired;
                  }

                return CRH;
               }

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }


        public List<CertificateRequestDetail> getReqDetailByReqID(string ID,bool forDetails)
        {
            try
            {
                List<CertificateRequestDetail> DetailList = new List<CertificateRequestDetail>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {

                    if (forDetails)
                    {
                        datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                        System.Data.Linq.ISingleResult<DCISgetCRequestDetailsByIDResult> lst = datacontext.DCISgetCRequestDetailsByID(ID);
                        foreach (DCISgetCRequestDetailsByIDResult result in lst)
                        {
                            CertificateRequestDetail CRD = new CertificateRequestDetail();
                            CRD.SeqNo1 = result.SeqNo;
                            CRD.GoodItem1 = result.GoodItem;
                            CRD.HSCode1 = result.HSCode;
                            CRD.PackageType1 = result.PackageType;
                            CRD.Quantity1 = result.Quantity;
                            CRD.ShippingMark1 = result.ShippingMark;
                            CRD.SummaryDesc1 = result.SummaryDesc;
                            CRD.PackageDescription1 = result.PackageDescription;

                            DetailList.Add(CRD);

                        }
                    }
                    else
                    {
                        datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                        System.Data.Linq.ISingleResult<DCISgetCRequestDetailsByIDResult> lst = datacontext.DCISgetCRequestDetailsByID(ID);
                        foreach (DCISgetCRequestDetailsByIDResult result in lst)
                        {
                            CertificateRequestDetail CRD = new CertificateRequestDetail();
                            CRD.SeqNo1 = result.SeqNo;
                            CRD.GoodItem1 = result.GoodItem;
                            CRD.HSCode1 = result.HSCode;
                            CRD.PackageType1 = result.PackageType;
                            CRD.Quantity1 = result.Quantity;
                            CRD.ShippingMark1 = result.ShippingMark;
                            CRD.SummaryDesc1 = result.SummaryDesc;
                            CRD.PackageDescription1 = result.PackageDescription;

                            DetailList.Add(CRD);

                        }
                    }
                }
                return DetailList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }


        public bool checkCertificateEditRequest(string UserId, string RequestID)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

                    System.Data.Linq.ISingleResult<DCSIcheckUserNCertificateResult> loggedUser = datacontext.DCSIcheckUserNCertificate(RequestID, UserId);
                    foreach (DCSIcheckUserNCertificateResult result in loggedUser)
                    {
                        if (result.RequestId.Equals(RequestID))
                        {
                            return true;
                        }
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message.ToString());
                ErrorLog.LogError(ex);
                return false;
            }


        }


        public List<SupportingDocUpload> getSupportingDOCfRequest(string ID)
        {
            try
            {
                List<SupportingDocUpload> DocList = new List<SupportingDocUpload>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCRequestSupportingDOCResult> lst = datacontext.DCISgetCRequestSupportingDOC(ID);
                    foreach (DCISgetCRequestSupportingDOCResult result in lst)
                    {
                        SupportingDocUpload SUP = new SupportingDocUpload();
                        SUP.Document_Id = result.SupportingDocumentId;
                        SUP._Remarks = result.Remarks;
                        SUP.Request_Ref_No = result.RequestRefNo;
                        SUP.Uploaded_By = result.UploadedBy;
                        SUP.Uploaded_Path = result.UploadedPath;
                        SUP.Document_Name = result.DocumentName;
                        SUP.Seq_No = result.UploadSeqNo;
                        SUP.SupportingDoc_Name = result.SupportingDocumentName;
                        SUP.Request_Date = result.RequestDate.ToString();
                        SUP.Signature_Required = Convert.ToBoolean(result.SignatureRequired);

                        DocList.Add(SUP);

                    }
                }
                return DocList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }


        public string setUploadBasedCertificateRequest(CertificateRequest EmR)
        {

            try
            {

                string certificatereqno = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        SequenceManager seqmanager = new SequenceManager();
                        Int64 RequestNo = seqmanager.getNextSequence("CertificateRequestNo", dbContext);/*tblSequence - */
                        certificatereqno = "CRN" + RequestNo.ToString();
                        dbContext.DCISsetUploadBasedCRequests(certificatereqno,
                                                            EmR.Customer_ID,
                                                            "P",
                                                            EmR.Created_By,
                                                            EmR.Upload_Path,
                                                            EmR.Invoice_No,
                                                            EmR.Seal_Required);
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        return certificatereqno;



                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public CertificateRequest getPendingUpBaseCRequest(string CustomerId)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {
                CertificateRequest CR = new CertificateRequest();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetPendingUBasedCertRequstResult> resultlist = datacontext.DCISgetPendingUBasedCertRequst(CustomerId);
                CR.CertificateUpload_List = resultlist;

                return CR;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }


        public bool UpdateSupportingDocCertified(SupportingDocUpload SD)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateSupportingDocUpload(SD.Seq_No,SD.Uploaded_Path,SD.Document_Name);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public List<SupportingDocUpload> getSupportingDOCfUploadBRequest(string ID)
        {
            try
            {
                List<SupportingDocUpload> DocList = new List<SupportingDocUpload>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUploadBCRequestSupportingDOCResult> lst = datacontext.DCISgetUploadBCRequestSupportingDOC(ID);
                    foreach (DCISgetUploadBCRequestSupportingDOCResult result in lst)
                    {
                        SupportingDocUpload SUP = new SupportingDocUpload();
                        SUP.Document_Id = result.SupportingDocumentId;
                        SUP._Remarks = result.Remarks;
                        SUP.Request_Ref_No = result.RequestRefNo;
                        SUP.Uploaded_By = result.UploadedBy;
                        SUP.Uploaded_Path = result.UploadedPath;
                        SUP.Document_Name = result.DocumentName;
                        SUP.Seq_No = result.UploadSeqNo;
                        SUP.SupportingDoc_Name = result.SupportingDocumentName;
                        SUP.Request_Date = result.RequestDate.ToString();

                        DocList.Add(SUP);

                    }
                }
                return DocList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }


        public bool UpdateUploadBCertifcateRequest(CertificateApproval CA)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateUploadBCertifcateRequest(CA.Created_By,CA.Certificate_Id,CA.Request_Id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool setDELETESupportingDocUpload(Int64 UploadID)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetDeleteSupportingDocUpload(UploadID);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool setDELETECertificatRequestDetails(Int64 SeqNo)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetDeleteCERTREQDETAILS(SeqNo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool setWebBasedCertificateCreation(string RequestId, string CertificatePath, string CertificateName)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetWebBasedCertificateCreation(RequestId, CertificatePath,CertificateName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool UpdateReqeustHeadStatus(string RequestID,string Status)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateCertificateRequestHeadStatus(RequestID, Status);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public CertificateRequest getPendingWebBaseCRequest(string CustomerId)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {
                CertificateRequest CR = new CertificateRequest();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetPendingWebbasedCertificateDetailsResult> resultlist = datacontext.DCISgetPendingWebbasedCertificateDetails(CustomerId);
                CR.PendignWebBasedCertificate_List = resultlist;

                return CR;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }


        public objResultSet setCertificateRequest(CertificateRequestHeader hdr, CertificateRequestDetail RD)
        {

            try
            {
                objResultSet Result = new objResultSet();
                string certificatereqno = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        SequenceManager seqmanager = new SequenceManager();
                        Int64 RequestNo = seqmanager.getNextSequence("CertificateRequestNo", dbContext);
                        certificatereqno = "CRN" + RequestNo.ToString();
                        dbContext.DCISsetCertifcateRequestHeader(certificatereqno,
                                                                hdr.TemplateId1,
                                                                hdr.CustomerId1,
                                                                hdr.CreatedBy1,
                                                                hdr.Status1,
                                                                hdr.Consignor1,
                                                                hdr.Consignee1,
                                                                hdr.InvoiceNo1,
                                                                hdr.InvoiceDate1,
                                                                hdr.CountryCode1,
                                                                hdr.LoadingPort1,
                                                                hdr.PortOfDischarge1,
                                                                hdr.Vessel1,
                                                                hdr.PlaceOfDelivery1,
                                                                hdr.TotalInvoiceValue1,
                                                                hdr.TotalQuantity1,
                                                                hdr.OtherComments1,
                                                                hdr.OtherDetails1,
                                                                hdr.Seal_Required);


                        dbContext.DCISsetROWCertificateDetails(certificatereqno,
                                                               RD.Good_Details,
                                                               RD.Quantity_Details,
                                                               RD.HSCode_Details,
                                                               hdr.CreatedBy1);
                        if (hdr.Addto_Refference)
                        {
                            dbContext.DCISsetReffrennceRequest(hdr.Consignee1.Replace("<br />", " - "), hdr.CustomerId1, certificatereqno);
                        }
                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        Result.Boolen_Value = true;
                        Result.String_Value = certificatereqno;
                        return Result;



                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool updateCertificateRequest(CertificateRequestHeader hdr, CertificateRequestDetail RD)
        {

            try
            {
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();
                        dbContext.DCISupdateCertifiRqustHdr(hdr.RequestId1,
                                                                hdr.ModifiedBy1,
                                                                hdr.Consignor1,
                                                                hdr.Consignee1,
                                                                hdr.InvoiceNo1,
                                                                hdr.InvoiceDate1,
                                                                hdr.CountryCode1,
                                                                hdr.LoadingPort1,
                                                                hdr.PortOfDischarge1,
                                                                hdr.Vessel1,
                                                                hdr.PlaceOfDelivery1,
                                                                hdr.TotalInvoiceValue1,
                                                                hdr.TotalQuantity1,
                                                                hdr.OtherComments1,
                                                                hdr.OtherDetails1,
                                                                hdr.Seal_Required);

                        dbContext.DCISsetUpdateRowCertificateDetails(RD.SeqNo1,
                                                               hdr.RequestId1,
                                                               RD.Good_Details,
                                                               RD.Quantity_Details,
                                                               RD.HSCode_Details,
                                                               hdr.CreatedBy1);

                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }


        }

        public CertificateRequestDetail getROWbasedCertificateRequestDetails(string RequestID)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                CertificateRequestDetail CRD = new CertificateRequestDetail();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetRowCertificateDetailsResult> resultlist = datacontext.DCISgetRowCertificateDetails(RequestID);
                foreach (DCISgetRowCertificateDetailsResult result in resultlist)
                {
                    CRD.SeqNo1 = result.SeqNo;
                    CRD.Good_Details = result.GoodDetails;
                    CRD.HSCode_Details = result.HSCodeDetails;
                    CRD.Quantity_Details = result.QuantityDetails;
                    CRD.CreatedBy1 = result.CreatedBy;
                    CRD.CreatedDate1 = Convert.ToDateTime(result.CreatedDate);

                }
                return CRD;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }

        public bool setUpdateCertificateRequestDetails(CertificateRequestDetail certificaterequestdetail)
        {

            try
            {
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        dbContext.DCISupdateCertiReqDetail(certificaterequestdetail.SeqNo1,
                            certificaterequestdetail.GoodItem1,
                            certificaterequestdetail.ShippingMark1,
                            certificaterequestdetail.PackageType1,
                            certificaterequestdetail.SummaryDesc1,
                            certificaterequestdetail.Quantity1,
                            certificaterequestdetail.HSCode1);

                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        return true;



                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }


        }

        public bool setCertificateRequestDetails(CertificateRequestDetail RD)
        {

            try
            {
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();

                        dbContext.DCISsetCertificateRequestDetails(RD.RequestId1,
                                                RD.GoodItem1,
                                                RD.ShippingMark1,
                                                RD.PackageType1,
                                                RD.SummaryDesc1,
                                                RD.Quantity1,
                                                RD.HSCode1,
                                                RD.CreatedBy1);

                        dbContext.SubmitChanges();
                        dbContext.Transaction.Commit();
                        return true;



                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        dbContext.Transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        dbContext.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }


        }

        public SupportingDocUpload getNotUploadedSupportingDOCfRequest(string ID)
        {

            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                SupportingDocUpload supportingDOClist = new SupportingDocUpload();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISnotUploadedSupportingDocumentsResult> lst = datacontext.DCISnotUploadedSupportingDocuments(ID);
                supportingDOClist.Not_Uploaded_SD = lst;

                return supportingDOClist;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                // Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }

        public CertificateRequest getCustomerConsignees(string CustomerId)
        {
            try
            {
                CertificateRequest cntry = new CertificateRequest();
                DCISLCDataContext datacontext = new DCISLCDataContext();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetCustomerConsigneesResult> lst = datacontext.DCISgetCustomerConsignees(CustomerId);
                cntry.DCISgetCustomerConsignees_List = lst;
                return cntry;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }
        public bool DeleteCustomerRefferenc(string ReqId)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISdeleteCustomerReffrenect(ReqId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }
        public bool DeleteSavedCertificate(string Reqid,string ModifiedBy)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetDeleteSavedCertificates(ModifiedBy,Reqid);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }
    }


}
