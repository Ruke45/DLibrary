using DCISDBManager.objLib.Email;
using DCISDBManager.objLib.Master;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace DCISDBManager.trnLib.EmailManager
{
    public class MailSendManager
    {
        public int SendEmail(string To, string Subject, string Body, string Attachment_Path)
        {
            try
            {
                string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
                List<objParameters> Eb = getEmailSendParamters();

                string EBCR_EMAIL = Eb[0].Parameter_Value;

                string DecryptID = DECKey.Substring(10);
                string EBCRE_PASS = EncDec.Decrypt(Eb[1].Parameter_Value, DecryptID);

                string EBCRM_SMTP = Eb[2].Parameter_Value;
                int EBCRM_SMTP_PORT = Convert.ToInt32(Eb[3].Parameter_Value);

                if (EBCR_EMAIL.Equals("") || EBCRE_PASS.Equals("") || EBCRM_SMTP.Equals("") || EBCRM_SMTP_PORT.Equals(0))
                {
                    return 2;
                }
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(EBCRM_SMTP);//"smtp.live.com"
                mail.From = new System.Net.Mail.MailAddress(EBCR_EMAIL);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = Body;

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(Attachment_Path);
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = EBCRM_SMTP_PORT;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EBCR_EMAIL, EBCRE_PASS);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return 1;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError("Mail Send Faild (MailSendManager->SendEmail())", Ex);
                return 0;
            }

        }

        public List<EmailRequest> getSendPendingEBCertificates()
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                List<EmailRequest> objEmailRequest = new List<EmailRequest>();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetNotSendEmailCertificatesResult> lst = datacontext.DCISgetNotSendEmailCertificates();

                foreach (DCISgetNotSendEmailCertificatesResult result in lst)
                {
                    EmailRequest ER = new EmailRequest();
                    ER.Request_ID = result.RequestId;
                    ER.Email_ = result.Email;
                    ER.Customer_ID = result.CustomerId;
                    ER.Status_ = result.Status;
                    ER.Certificate_Id = result.CertificateId;
                    ER.Certificate_Path = result.CertificatePath;
                    ER.Is_Send = result.IsSend;

                    objEmailRequest.Add(ER);

                }

                return objEmailRequest;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

        public List<EmailRequest> getSendPendingMailSendingCertificates(string CustomerID,string RequestID)
        {
            DCISLCDataContext datacontext = new DCISLCDataContext();
            try
            {

                List<EmailRequest> objEmailRequest = new List<EmailRequest>();


                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISSENDAPPROVALEMAILResult> lst = datacontext.DCISSENDAPPROVALEMAIL(CustomerID,RequestID);

                foreach (DCISSENDAPPROVALEMAILResult result in lst)
                {
                    EmailRequest ER = new EmailRequest();
                    ER.Request_ID = result.RequestId;
                    ER.Email_ = result.ContactPersonEmail;
                    ER.Customer_ID = result.CustomerId;
                    ER.Certificate_Id = result.CertificateId;

                    objEmailRequest.Add(ER);

                }

                return objEmailRequest;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

        public bool UpdateEBCSend(string ERequestID, string IsSend)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateEBCSend(ERequestID,IsSend);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public bool UpdateApprovalMailsend(string CertificateID)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    int i = datacontext.DCISUPDATECERT_APPROVAL(CertificateID);
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public List<objParameters> getEmailSendParamters()
        {
            try
            {
                List<objParameters> ParaList = new List<objParameters>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEBCSendingMailParametersResult> lst = datacontext.DCISgetEBCSendingMailParameters();
                    foreach (DCISgetEBCSendingMailParametersResult result in lst)
                    {
                        objParameters Para = new objParameters(result.ParameterCode, result.ParameterValue);
                        ParaList.Add(Para);
                    }
                }
                return ParaList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

        public List<objParameters> getALLEBC_EmailParamters()
        {
            try
            {
                List<objParameters> ParaList = new List<objParameters>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEBCAllMailParametersResult> lst = datacontext.DCISgetEBCAllMailParameters();
                    foreach (DCISgetEBCAllMailParametersResult result in lst)
                    {
                        objParameters Para = new objParameters(result.ParameterCode, result.ParameterValue);
                        ParaList.Add(Para);
                    }
                }
                return ParaList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }

        public bool setUpdateEBCconfig(string Code,string Value)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.DCISsetUpdateTblParameter(Code, Value);
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
