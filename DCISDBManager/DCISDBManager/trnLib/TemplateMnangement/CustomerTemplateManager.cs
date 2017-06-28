using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Template;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.TemplateMnangement
{
    public class CustomerTemplateManager
    {
        public bool setCustomerTemplate(CustomerTemplate req)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISsetApprovedTemplate(req.CustomerId1, req.TemplateId1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public List<CustomerTemplate> getTemplate(string Status)
        {
            try
            {

                List<CustomerTemplate> Requests = new List<CustomerTemplate>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTemplateDetailsResult> lst = datacontext.DCISgetTemplateDetails(Status);

                    CustomerTemplate req;

                    foreach (DCISgetTemplateDetailsResult result in lst)
                    {
                        req = new CustomerTemplate();
                        req.TemplateId1 = result.TemplateId;
                        req.TemplateName1 = result.TemplateName;
                        req.ImgUrl1 = result.ImgUrl;
                        req.Discription1 = result.Description;
                       
                        Requests.Add(req);



                    }
                }

                return Requests;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
    }
}
