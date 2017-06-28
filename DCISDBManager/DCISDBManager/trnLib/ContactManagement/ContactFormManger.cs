using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.ContactDetail;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.ContactManager
{
    public class ContactFormManger
    {
        public bool setContactFormDetails(ContactDetail details)
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

                        

                       
                        dbContext.DCISsetContactFromDetail(details.Name1,details.Email1,details.Phone1,details.Detail1);
                    
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


        public List<ContactDetail> getAllMassage(string fromdate, string todate, string viewStatus)
        {
            try
            {
                List<ContactDetail> DocList = new List<ContactDetail>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllMassageResult> lst = datacontext.DCISgetAllMassage(fromdate, todate, viewStatus);
                    foreach (DCISgetAllMassageResult result in lst)
                    {
                        ContactDetail CDetail = new ContactDetail();
                        CDetail.SeqId1 = result.seqNo;
                        CDetail.Name1 = result.Name;
                        CDetail.Email1 = result.Email;
                        CDetail.Phone1 = result.Phone;
                        CDetail.Detail1 = result.Detail;
                        CDetail.CreatedDate1 = result.CreatedDate.ToShortDateString();
                        if (result.ViewStatus == "Y")
                        {
                            CDetail.Status1 = "Checked";
                        }
                        else {
                            CDetail.Status1 = "New";
                        }
                        
                        CDetail.CreatedDate1 = result.CreatedDate.ToShortDateString();

                        DocList.Add(CDetail);

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


        public bool ModifyMassageViewStatus(string seq,string status)
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



                        long seqId = Convert.ToInt64(seq);
                        dbContext.DCISModifyMassageViewStatus(seqId,status);

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

        public List<ContactDetail> getAllNewMassage(string viewStatus)
        {
            try
            {
                List<ContactDetail> DocList = new List<ContactDetail>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllNewMassageResult> lst = datacontext.DCISgetAllNewMassage(viewStatus);
                    foreach (DCISgetAllNewMassageResult result in lst)
                    {
                        ContactDetail CDetail = new ContactDetail();
                        CDetail.SeqId1 = result.seqNo;
                        CDetail.Name1 = result.Name;
                        CDetail.Email1 = result.Email;
                        CDetail.Phone1 = result.Phone;
                        CDetail.Detail1 = result.Detail;
                        CDetail.CreatedDate1 = result.CreatedDate.ToShortDateString();
                        if (result.ViewStatus == "Y")
                        {
                            CDetail.Status1 = "Checked";
                        }
                        else
                        {
                            CDetail.Status1 = "New";
                        }

                        CDetail.CreatedDate1 = result.CreatedDate.ToShortDateString();

                        DocList.Add(CDetail);

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
