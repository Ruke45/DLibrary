using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.objLib.Certificate;
using DCISDBManager.objLib.Master;

namespace DCISDBManager.trnLib.MasterMaintenance
{
    public class ManualCertificateManagement
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

                       // SequenceManager seqmanager = new SequenceManager();
                       // Int64 RequestNo = seqmanager.getNextSequence("CertificateRequestNo", dbContext);
                       // certificatereqno = "CRN" + RequestNo.ToString();
                        dbContext.DCISsetCertifcateRequestHeader(hdr.RequestId1,
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

                            dbContext.DCISsetCertificateRequestDetails(hdr.RequestId1,
                                certificaterequestdetail.GoodItem1,
                                certificaterequestdetail.ShippingMark1,
                                certificaterequestdetail.PackageType1,
                                certificaterequestdetail.SummaryDesc1,
                                certificaterequestdetail.Quantity1,
                                certificaterequestdetail.HSCode1,
                                certificaterequestdetail.CreatedBy1);

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
        public objResultSet modifyCertificateRequest(CertificateRequestHeader hdr, List<CertificateRequestDetail> lstRequestDetail)
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

                        // SequenceManager seqmanager = new SequenceManager();
                        // Int64 RequestNo = seqmanager.getNextSequence("CertificateRequestNo", dbContext);
                        // certificatereqno = "CRN" + RequestNo.ToString();
                        dbContext.DCISModifCertifcateRequestHeader(
                                                                
                                                                
                                                                hdr.CreatedBy1,
                                                                hdr.RequestId1,
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
                                                                hdr.TotalQuantity1);


                        foreach (CertificateRequestDetail certificaterequestdetail in lstRequestDetail)
                            if (certificaterequestdetail.CreatedBy1 == "")
                            {

                                dbContext.DCISsetCertificateRequestDetails(hdr.RequestId1,
                                    certificaterequestdetail.GoodItem1,
                                    certificaterequestdetail.ShippingMark1,
                                    certificaterequestdetail.PackageType1,
                                    certificaterequestdetail.SummaryDesc1,
                                    certificaterequestdetail.Quantity1,
                                    certificaterequestdetail.HSCode1,
                                    hdr.CreatedBy1);

                            }
                            else {
                                dbContext.DCISModifyCertificateRequestDetailsM(hdr.RequestId1, certificaterequestdetail.GoodItem1,
                                     certificaterequestdetail.ShippingMark1,
                                        certificaterequestdetail.PackageType1,
                                        certificaterequestdetail.SummaryDesc1,
                                        certificaterequestdetail.Quantity1,
                                        certificaterequestdetail.HSCode1,
                                        certificaterequestdetail.CreatedBy1);
                            
                            
                            
                            
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


        public List<ManualCertificate> getUpoladList(string ReqID, string Status)
        {
            try
            {

                List<ManualCertificate> lstmct = new List<ManualCertificate>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUploadBasedCertReqResult> lst = datacontext.DCISgetUploadBasedCertReq(ReqID, Status);

                    ManualCertificate mt;

                    foreach (DCISgetUploadBasedCertReqResult result in lst)
                    {
                        mt = new ManualCertificate();
                        mt.Request_Id = result.RequestId;
                        mt.Customer_Id = result.CustomerId;
                        mt.Certificate_Name = result.CertificateName;
                        mt.Certificate_Path = result.CertificatePath;
                        mt.Status_ = result.Status;
                        

                        lstmct.Add(mt);

                    }
                }

                return lstmct;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
        public CertificateRequestHeader getRequestHeader(string ReqID)
        {
            try
            {

                CertificateRequestHeader mt = new CertificateRequestHeader();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllCertificateheaderMResult> lst = datacontext.DCISgetAllCertificateheaderM(ReqID);

                   // CertificateRequestHeader mt;

                    foreach (DCISgetAllCertificateheaderMResult result in lst)
                    {
                        mt = new CertificateRequestHeader();
                        mt.CertificateId1 = result.RequestId;
                        mt.Consignee1= result.Consignee;
                        mt.Consignor1 = result.Consignor;
                        mt.CountryCode1 = result.CountryCode;
                        mt.CreatedBy1 = result.CreatedBy;
                        mt.CreatedDate1 = result.CreatedDate;
                        mt.CustomerId1 = result.CustomerId;
                        mt.InvoiceDate1 =Convert.ToDateTime(result.InvoiceDate.ToString());
                        mt.InvoiceNo1 = result.InvoiceNo;
                        mt.LoadingPort1 = result.LoadingPort;
                        mt.ModifiedBy1 = result.ModifiedBy;
                  
                        mt.PlaceOfDelivery1 = result.PlaceOfDelivery;
                        mt.PortOfDischarge1 = result.PortOfDischarge;
                        mt.RequestId1 = result.RequestId;
                        mt.Status1 = result.Status;
                        mt.TemplateId1 = result.TemplateId;
                        mt.TotalInvoiceValue1 = result.TotalInvoiceValue.ToString();
                        mt.TotalQuantity1 = result.TotalQuantity;
                        mt.Vessel1 = result.Vessel;




                          

                    }

                }

                return mt;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public List<CertificateRequestDetail> getRequestDetails(string ReqID)
        {
            try
            {

                List<CertificateRequestDetail> lstmct = new List<CertificateRequestDetail>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCertificateDetailsMResult> lst = datacontext.DCISgetCertificateDetailsM(ReqID);

                    CertificateRequestDetail mt;

                    foreach (DCISgetCertificateDetailsMResult result in lst)
                    {
                        mt = new CertificateRequestDetail();
                        mt.HSCode1 = result.HSCode;
                        mt.GoodItem1 = result.GoodItem;
                        mt.PackageType1 = result.PackageType;
                        mt.Quantity1 = result.Quantity;
                        mt.ShippingMark1 = result.ShippingMark;
                        mt.SummaryDesc1 = result.SummaryDesc;
                        mt.CreatedBy1 = result.SeqNo.ToString();

                        lstmct.Add(mt);

                    }
                }

                return lstmct;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

       





        public List<ManualCertificate> getUpoladListE(string ReqID, string Status)
        {
            try
            {

                List<ManualCertificate> lstmct = new List<ManualCertificate>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUploadBasedCertReqEResult> lst = datacontext.DCISgetUploadBasedCertReqE(ReqID, Status);

                    ManualCertificate mt;

                    foreach (DCISgetUploadBasedCertReqEResult result in lst)
                    {
                        mt = new ManualCertificate();
                        mt.Request_Id = result.RequestId;
                        mt.Customer_Id = result.CustomerId;
                        mt.Certificate_Name = result.CertificateName;
                        mt.Certificate_Path = result.CertificatePath;
                        mt.Status_ = result.Status;


                        lstmct.Add(mt);

                    }
                }

                return lstmct;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool SetManualEntry(CertificateRequestDetail C)
        {


            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                datacontext.DCISsetCertificateRequestDetails(C.RequestId1, C.GoodItem1, C.ShippingMark1, C.PackageType1, C.SummaryDesc1, C.Quantity1, C.HSCode1, C.CreatedBy1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool SetManualHeaderEntry(CertificateRequestHeader hdr)
        {


            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                datacontext.DCISsetCertifcateRequestHeader(hdr.RequestId1,
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


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public String getTemplateID(string CustID)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetCusTemplateIDResult> Upd = datacontext.DCISgetCusTemplateID(CustID);

                foreach (DCISgetCusTemplateIDResult result in Upd)
                {

                    return result.TemplateId;
                    
                }
                return "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return "";
            }
        }
        public bool ModifyUploadbaseRemarks(ManualCertificate mc)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyUploadBasedCertRemarks(mc.Request_Id, mc.Remarks_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool ModifyEmailbaseRemarks(ManualCertificate mc)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyEmailbasedBasedCertRemarks(mc.Request_Id, mc.Remarks_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }


        public bool DeleteCertificateDetails(CertificateRequestDetail cd)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISDeleteCertificateDetail(cd.CreatedBy1);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

    }
}
