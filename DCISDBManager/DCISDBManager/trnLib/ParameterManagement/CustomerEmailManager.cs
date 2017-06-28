using DCISDBManager.objLib.Email;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.ParameterManagement
{
    public class CustomerEmailManager
    {
        public List<EmailRequest> getCustomerEmailList()
        {
            try
            {
                List<EmailRequest> EmailList = new List<EmailRequest>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {


                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerEmailsResult> lst = datacontext.DCISgetCustomerEmails("%");
                    foreach (DCISgetCustomerEmailsResult result in lst)
                    {
                        EmailRequest Email = new EmailRequest(result.Email,result.CustomerId);
                        EmailList.Add(Email);

                    }
                }
                return EmailList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
