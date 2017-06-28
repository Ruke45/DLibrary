using DCISDBManager.objLib.Certificate;
using DCISDBManager.objLib.Master;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.SupportDocumentSignManagement
{
    public class SDocSignRequsetManager
    {
        public string setSupportingDocSignRequest(SupportingDocUpload hdr)
        {            
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.Connection.Open();

                    try
                    {
                        SequenceManager seqmanager = new SequenceManager();
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        string Doc_No = "SDR" + seqmanager.getNextSequence("SupportingDocSignRq").ToString();
                        datacontext.DCISsetSupportingDocApprove(Doc_No,
                                                                    hdr.Document_Id,
                                                                    hdr.Customer_ID,
                                                                    hdr.Uploaded_By,
                                                                    hdr.Status_,
                                                                    hdr.Uploaded_Path +"/"+Doc_No+"/"+hdr.SupportingDoc_Name,
                                                                    hdr.SupportingDoc_Name);
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return Doc_No;
                    }
                    catch (Exception Ex)
                    {
                        ErrorLog.LogError(Ex);
                        datacontext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        datacontext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public SupportingDocUpload DCISgetPendingSDApprovals(string Status,string CustomerID)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {
                SupportingDocUpload SDU = new SupportingDocUpload();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetPendingSDocApprovalsResult> resultlist = datacontext.DCISgetPendingSDocApprovals(Status,CustomerID);
                SDU.SDPendingApproval_List = resultlist;

                return SDU;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

        public bool ApproveSupportingDoc(SupportingDocUpload SD)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateSDApproveReq(SD.Request_Ref_No,SD.Approved_By,SD.Certified_Doc_Path,SD.Certified_Doc_Name,Convert.ToDateTime(SD.Expire_Date));
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool RejectSupportingDoc(SupportingDocUpload SD,string RejectCode)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateSDRejectReq(SD.Request_Ref_No,SD.Approved_By,RejectCode);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public string setSupportingDocSignRequestINCertRequest(SupportingDocUpload hdr)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.Connection.Open();

                    try
                    {
                        SequenceManager seqmanager = new SequenceManager();
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        long No = Convert.ToInt64(seqmanager.getNextSequence("SupportingDocSignRq"));
                        if (No == 0)
                        {
                            return null;
                        }
                        string Doc_No = "SDR" + No.ToString();
                        datacontext.DCISsetSupportingDocApproveFrmCRquest(Doc_No,
                                                                    hdr.Document_Id,
                                                                    hdr.Customer_ID,
                                                                    hdr.Uploaded_By,
                                                                    hdr.Status_,
                                                                    hdr.Uploaded_Path,
                                                                    hdr.Document_Name,
                                                                    hdr.Approved_By,
                                                                    Convert.ToDateTime(hdr.Request_Date),
                                                                    hdr.Certified_Doc_Path,
                                                                    Convert.ToDateTime(hdr.Expire_Date),
                                                                    hdr.Request_Ref_No);
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return Doc_No;
                    }
                    catch (Exception Ex)
                    {
                        ErrorLog.LogError(Ex);
                        datacontext.Transaction.Rollback();
                        return null;
                    }
                    finally
                    {
                        datacontext.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public SDSignatureConfig getSDSignatureConfig(string SupDocID)
        {
            try
            {
                SDSignatureConfig SDConfig = new SDSignatureConfig();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSDSignatureCordinatesResult> lst = datacontext.DCISgetSDSignatureCordinates(SupDocID);
                    foreach (DCISgetSDSignatureCordinatesResult result in lst)
                    {
                        SDConfig.Supporting_Doc_ID = result.SupportingDocId;
                        SDConfig.LLX_Cordinates = Convert.ToDouble(result.LLXcordinates);
                        SDConfig.LLY_Cordinates = Convert.ToDouble(result.LLYcordinates);
                        SDConfig.URX_cordinates = Convert.ToDouble(result.URXcordinates);
                        SDConfig.URY_cordinates = Convert.ToDouble(result.URYcordinates);

                        /*
                         * llx(lower left x coordinate) = margin from left.
                         * lly(lower left y coordinate) = margin from bottom(bottom of rectangle)
                         * urx(upper right x coordinate) = width of article
                         * ury(upper right y coordinate) = margin from bottom of upper boundary of article.
                         */
                        return SDConfig;
                    }
                    return null;
                    
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

    }
}
