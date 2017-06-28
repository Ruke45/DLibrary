using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Usr;
using DCISDBManager.objLib.Certificate;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.CertificateManagement
{
    public class CertificateSignManagment
    {

        public bool setUserSignatureDetails(UserSignature usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUserSignatureDetails(usr.User_ID, usr.PFX_path, usr.SignatureIMG_Path, usr.Created_By);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool updateUserSignatureDetails(UserSignature usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISupdateUserSignatureDetails(usr.User_ID, usr.PFX_path, usr.SignatureIMG_Path, usr.Created_By);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public bool ApproveCertificate(CertificateApproval usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.Connection.Open();
                    try
                    {
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        datacontext.DCISsetCertificateApproval(usr.Certificate_Id, usr.Request_Id, usr.Expiry_Date, usr.Created_By, usr.Is_Downloaded, usr.Certificate_Path, usr.Certificate_Name, usr.Is_Valid);
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.LogError(ex);
                        datacontext.Transaction.Rollback();
                        return false;
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
                return false;
            }

        }

        public bool ApproveECertificate(CertificateApproval usr)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetECertificateApproval(usr.Certificate_Id,usr.Request_Id, usr.Expiry_Date, usr.Created_By, usr.Is_Downloaded, usr.Certificate_Path, usr.Certificate_Name, usr.Is_Valid);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool RejectCertificate(string RequestID, string RejectedBy,string ReasonCode)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetCertificateReject(RequestID,RejectedBy,ReasonCode);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool RejectUBCertificate(string RequestID, string RejectedBy, string ReasonCode)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetRejectUBCertificate(RejectedBy,RequestID,ReasonCode);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool RejectECertificate(string RequestID, string RejectedBy, string ReasonCode)
        {

            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetECertificateReject(RequestID, RejectedBy, ReasonCode);
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
