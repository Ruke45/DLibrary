using DCISDBManager.objLib.Email;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.EmailManager
{
   public class EmailManager
    {
       public bool setEmail(Email msg)
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
                       datacontext.DCISsetEmail(msg.EmailBody1,msg.EmailAddress1);
                       datacontext.SubmitChanges();
                       datacontext.Transaction.Commit();
                       return true;
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message.ToString());
                       datacontext.Transaction.Rollback();
                       return false;
                   }

               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }


       public List<Email> getEmail()
       {
           try
           {

               List<Email> lstEmail = new List<Email>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetEmailResult> lst = datacontext.DCISgetEmail();

                   Email msg;

                   foreach (DCISgetEmailResult result in lst)
                   {
                       msg = new Email();
                       msg.EmailAddress1 = result.EmailAddress;
                       msg.EmailBody1 = result.EmailBody;
                       msg.EmailId1 = result.EmailId;

                       lstEmail.Add(msg);

                   }
               }

               return lstEmail;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return null;
           }
       }


       public bool deleteMail(Email msg)
       {
           try
           {
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                   try
                   {


                       datacontext.DCISdeleteEmail(msg.EmailId1);

                       return true;

                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message.ToString());

                       return false;
                   }
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }



       }

       public Email getEmailCount()
       {
           try
           {

               Email req = new Email();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetEmailResult> lst = datacontext.DCISgetEmail();
                   int i=0;
                   foreach (DCISgetEmailResult result in lst)
                   {
                       req = new Email();

                       i++;
                   }

                   req.Count1 = i;
               }

               return req;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return null;
           }


       }
    }
}
