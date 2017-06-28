using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAGetMail;// -- EAGetMail40.dll 
using DCISDBManager.trnLib.Utility;
using DCISDBManager.trnLib.ParameterManagement;
using DCISDBManager.objLib.Email;
using System.IO;
using System.Configuration;
using DCISDBManager.objLib.Master;

namespace DCISDBManager.trnLib.EmailManager
{
    public class EmailRequestManager
    {
        CustomerEmailManager CEm = new CustomerEmailManager();
        List<EmailRequest> EmailList = new List<EmailRequest>();
        string DECKey = System.Configuration.ConfigurationManager.AppSettings["DecKey"];
        

        public int SynceEmails(string AttachmentSavePath)
        {
            string curpath = AttachmentSavePath;//Server.MapPath("~/inbox" + "/" + DateTime.Now.Year);
            string mailbox = curpath;//String.Format("{0}\\inbox", curpath);

            EmailList = CEm.getCustomerEmailList();

            List<objParameters> Eb = getEmailSyncParamters();

            string EBCR_EMAIL = Eb[0].Parameter_Value;

            string DecryptID = DECKey.Substring(10);
            string EBCRE_PASS = EncDec.Decrypt(Eb[1].Parameter_Value, DecryptID);

            string EBCRM_SERVER = Eb[2].Parameter_Value;
            int EBCRMS_PORT = Convert.ToInt32(Eb[3].Parameter_Value);
            string EBCRMS_PROTOCOL = Eb[4].Parameter_Value;

            if (EBCR_EMAIL.Equals("") || EBCRE_PASS.Equals("") || EBCRMS_PORT.Equals("") || EBCRM_SERVER.Equals("") || EBCRMS_PROTOCOL.Equals(""))
            {
                return 2;
            }
            /* Hotmail/MSN POP3 server is "pop3.live.com"
             * Hotmail/MSN IMAP4 server is "imap-mail.outlook.com"
             */
            MailServer oServer = null;
            if (EBCRMS_PROTOCOL.Equals("POP3"))
            {
                oServer = new MailServer(EBCRM_SERVER,
                EBCR_EMAIL, EBCRE_PASS, ServerProtocol.Pop3);//ServerProtocol.Imap4 , ServerProtocol.Pop3

            }
            if (EBCRMS_PROTOCOL.Equals("IMAP4"))
            {
                oServer = new MailServer(EBCRM_SERVER,
                EBCR_EMAIL, EBCRE_PASS, ServerProtocol.Imap4);//ServerProtocol.Imap4 , ServerProtocol.Pop3

            }

            MailClient oClient = new MailClient("TryIt");
            /* Set SSL connection*/
            oServer.SSLConnection = true;

            /* Set 995 pop3 SSL port
             * Set 993 IMAP4 SSL port
             */
            oServer.Port = EBCRMS_PORT;

            //MailServer oServer = new MailServer("pop3.live.com",
            //            "ruke47@outlook.com", "@HotMail.com", ServerProtocol.Pop3);//ServerProtocol.Imap4 , ServerProtocol.Pop3
            //MailClient oClient = new MailClient("TryIt");

            ///* Set SSL connection*/
            //oServer.SSLConnection = true;

            ///* Set 995 pop3 SSL port
            // * Set 993 IMAP4 SSL port
            // */
            //oServer.Port = 995;


            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];

                    MailLog.LogMail("--------------------------------------------------------");
                    MailLog.LogMail("Email-Index:" + info.Index.ToString());
                    MailLog.LogMail("Email-Size:" + info.Size.ToString());
                    MailLog.LogMail("Email-UIDL:" + info.UIDL.ToString());
                    
                    string MailID = info.UIDL.ToString();

                    // Download email from Hotmail/MSN POP3 server
                    Mail oMail = oClient.GetMail(info);

                    for (int x = 0; x < EmailList.Count; x++)
                    {
                        if (EmailList[x].Email_.Equals(oMail.From.Address))
                        {
                            EmailRequest EmlR = new EmailRequest();

                            EmlR.Cerated_By = "";
                            EmlR.Customer_ID = EmailList[x].Customer_ID;
                            EmlR.Email_ = EmailList[x].Email_;
                            EmlR.Mail_ID = MailID;
                            EmlR.Mail_Subject = oMail.Subject;
                            EmlR.Recived_Date = oMail.ReceivedDate;
                            
                            MailLog.LogMail("From:" + oMail.From.ToString());
                            MailLog.LogMail("Subject:" + oMail.Subject);
                            MailLog.LogMail("Email-Address:" + oMail.From.Address.ToString());
                            MailLog.LogMail("Received-Date:" + oMail.ReceivedDate);
                            MailLog.LogMail("--------------------- Attachements ----------------------");


                            int count = 0;

                            Attachment[] atts = oMail.Attachments;
                            count = atts.Length;
                            if (count > 0)
                            {
                                EmlR.No_Of_Attachmments = count;

                                //bool Avilable = false;
                                string RequestID = setEmailCertificateRequest(EmlR);
                                if (RequestID != null)
                                {

                                    mailbox = curpath + "/" + RequestID;
                                    if (!Directory.Exists(mailbox))
                                    {
                                        Directory.CreateDirectory(mailbox);
                                    }
                                    for (int a = 0; a < count; a++)
                                    {
                                        Attachment att = atts[a];

                                        string Cert = att.Name.Substring(0, 3);
                                        if (Cert.Equals("NCE"))
                                        {
                                            // Avilable = true;
                                            MailLog.LogMail("Request ID :" + RequestID);
                                            MailLog.LogMail("Attachment-Name (Certificate) :" + att.Name.ToString());

                                            string Certificate = mailbox + "/Certificate";

                                            Directory.CreateDirectory(Certificate);

                                            string attname = String.Format("{0}\\{1}", Certificate, att.Name.Replace(" ", "_"));
                                            att.SaveAs(attname, true);
                                        }
                                        else
                                        {
                                            MailLog.LogMail("Attachment-Name :" + att.Name.ToString());
                                            string attname = String.Format("{0}\\{1}", mailbox, att.Name.Replace(" ", "_"));
                                            att.SaveAs(attname, true);
                                        }
                                    }
                                }
                            
                            }
                            else
                            {
                                setIgnoredEmails(EmlR);
                                MailLog.LogMail("------------------- No Attachements --------------------");
                            }
                        }
                     //   oClient.Delete(info);
                        /*-- "oClient.Delete(info);" - This will Delete The Email From the Hotmail
                         * --But When Enabling the POP3 in Hotmail
                         * --You Can Choose Not to Delete the Mails
                         * --It Will Put the Mails in POP3 Folder In Delete Folder
                         * */
                    }

                }
                oClient.Quit();
                return 1;
            }
            catch (MailServerException ex)
            {
                //Message contains the information returned by mail server
                ErrorLog.LogError("Mail Server Erro (Method :(EmailRequestManager->)", ex);
                return 0;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                ErrorLog.LogError("System.Net.Sockets.SocketException (Method :(EmailRequestManager->)", ex);
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return 0;
            }
        }


        public string setEmailCertificateRequest(EmailRequest EmR)
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
                        Int64 RequestNo = seqmanager.getNextSequence("ECertificateRequestNo", dbContext);/*tblSequence - */
                        certificatereqno = "ECRN" + RequestNo.ToString();
                        dbContext.DCISsetEmailCertificateRequests(certificatereqno,
                                                            EmR.Mail_ID,
                                                            EmR.Email_,
                                                            EmR.Customer_ID,
                                                            EmR.Recived_Date,
                                                            EmR.No_Of_Attachmments,
                                                            "P",
                                                            EmR.Mail_Subject,
                                                            EmR.Cerated_By);
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


        public bool setIgnoredEmails(EmailRequest EmR)
        {

            try
            {;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {

                    dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    dbContext.Connection.Open();

                    try
                    {
                        dbContext.Transaction = dbContext.Connection.BeginTransaction();
                        dbContext.DCISsetIgnoredEmails(EmR.Mail_ID,
                                                        EmR.Email_,
                                                        EmR.Customer_ID,
                                                        EmR.Recived_Date,
                                                        EmR.Mail_Subject,
                                                        EmR.Cerated_By);
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


        public List<EmailRequest> getEmailBasedCertRequest(string Status)
        {
            try
            {
                List<EmailRequest> RequestList = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEmailBasedCertificateRequestResult> lst = datacontext.DCISgetEmailBasedCertificateRequest(Status);
                    foreach (DCISgetEmailBasedCertificateRequestResult result in lst)
                    {
                        EmailRequest EmlR = new EmailRequest(result.RecivedDate,result.RequestId,result.CustomerId,result.CustomerName,result.Email);

                        RequestList.Add(EmlR);

                    }
                }
                return RequestList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }


        public EmailCertificateConfig getEmailSignatureConfig(string CustomerID)
        {
            try
            {
                EmailCertificateConfig EConfig = new EmailCertificateConfig();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSignatureCordinatesResult> lst = datacontext.DCISgetSignatureCordinates(CustomerID);
                    foreach (DCISgetSignatureCordinatesResult result in lst)
                    {
                        EConfig.Customer_ID = result.CustomerId;
                        EConfig.LLX_Cordinates = Convert.ToDouble(result.LLXcordinates);
                        EConfig.LLY_Cordinates = Convert.ToDouble(result.LLYcordinates);
                        EConfig.URX_cordinates = Convert.ToDouble(result.URXcordinates);
                        EConfig.URY_cordinates = Convert.ToDouble(result.URYcordinates);

                        /*
                         * llx(lower left x coordinate) = margin from left.
                         * lly(lower left y coordinate) = margin from bottom(bottom of rectangle)
                         * urx(upper right x coordinate) = width of article
                         * ury(upper right y coordinate) = margin from bottom of upper boundary of article.
                         */
                        return EConfig;
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


        public List<objParameters> getEmailSyncParamters()
        {
            try
            {
                List<objParameters> ParaList = new List<objParameters>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetEBCMailParametersResult> lst = datacontext.DCISgetEBCMailParameters();
                    foreach (DCISgetEBCMailParametersResult result in lst)
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

    }
}
