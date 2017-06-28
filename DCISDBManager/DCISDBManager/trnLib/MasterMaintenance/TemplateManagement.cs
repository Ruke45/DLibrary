using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;
using DCISDBManager.trnLib.ParameterManagement;

namespace DCISDBManager.trnLib.MasterMaintenance
{
    public class TemplateManagement
    {
        public bool CreateTemplate(DCISDBManager.objLib.MasterMaintenance.TemplateHeader th)
        {
            string TemplateID = string.Empty;
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                SequenceManager seqmanager = new SequenceManager();
                Int64 tempid = seqmanager.getNextSequence("TemplateID", datacontext);
                TemplateID = "template-" + tempid.ToString();

                datacontext.DCISsetTemplateHeader(th.Template_Name,TemplateID,th.Created_By,th.Is_Active,th.Modified_By,th.Img_Url,th.Description_);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public List<TemplateHeader> getTemplateData(string TempID, String IsActive)
        {
            try
            {

                List<TemplateHeader> lstUser = new List<TemplateHeader>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTemplateHeaderResult> lst = datacontext.DCISgetTemplateHeader(TempID, IsActive);

                    TemplateHeader th;

                    foreach (DCISgetTemplateHeaderResult result in lst)
                    {
                        th = new TemplateHeader();
                        th.Template_Id = result.TemplateId;
                        th.Template_Name = result.TemplateName;
                        th.Description_ = result.Description;
                        th.Is_Active = result.IsActive;

                        lstUser.Add(th);

                    }
                }

                return lstUser;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyTemplateHeaderStatus(DCISDBManager.objLib.MasterMaintenance.TemplateHeader th)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyTemplateHeaderStatus(th.Template_Name, th.Is_Active, th.Modified_By);
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
