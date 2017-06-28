using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.MasterMaintenance;
using DCISDBManager.trnLib.Utility;
using System.Configuration;

namespace DCISDBManager.trnLib.MasterMaintenance
{
   public  class TemplateForSupportingDocumentManagement
    {
        public bool ModifyTemplateSupportingDocument(DCISDBManager.objLib.MasterMaintenance.TemplateSupportingDocument ts)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyTemplateSupportingDocument(ts.Template_Id, ts.SupportingDocument_Id, ts.Modified_By,ts.Is_Mandatory,Convert.ToInt64(ts.TemplateSupporting_Document));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

       


        public bool CreateTemplateSupportingDocument(DCISDBManager.objLib.MasterMaintenance.TemplateSupportingDocument ts)
        {
            

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                // PackageType


                datacontext.DCISsetTemplateSupportingDocument( ts.SupportingDocument_Id, ts.Template_Id, ts.Created_By, ts.Is_Active,ts.Is_Mandatory);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public List<TemplateSupportingDocument> getTemplateIDforSupportDoc(string SupportingDocumentId,String TempName)
        {
            try
            {

                List<TemplateSupportingDocument> lstGroup = new List<TemplateSupportingDocument>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTemplateIDforSupDocResult> lst = datacontext.DCISgetTemplateIDforSupDoc(SupportingDocumentId, TempName);

                    TemplateSupportingDocument grp;

                    foreach (DCISgetTemplateIDforSupDocResult result in lst)
                    {
                        grp = new TemplateSupportingDocument();
                        grp.Template_Id = result.TemplateID;
                        grp.Template_Name = result.TemplateName;

                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
        public List<TemplateSupportingDocument> getSupportDocNameforTemplate(string SupportingDocumentId)
        {
            try
            {

                List<TemplateSupportingDocument> lstGroup = new List<TemplateSupportingDocument>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSupDocNameforTemplateResult> lst = datacontext.DCISgetSupDocNameforTemplate(SupportingDocumentId);

                    TemplateSupportingDocument grp;

                    foreach (DCISgetSupDocNameforTemplateResult result in lst)
                    {
                        grp = new TemplateSupportingDocument();
                        grp.SupportingDocument_Name = result.SupportingDocumentName;
                        grp.SupportingDocument_Id = result.SupportingDocumentId;

                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public List<TemplateSupportingDocument> getTemplateNameforSupportDoc(string SupportingDocumentId)
        {
            try
            {

                List<TemplateSupportingDocument> lstGroup = new List<TemplateSupportingDocument>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTemplateNameforSupDocResult> lst = datacontext.DCISgetTemplateNameforSupDoc(SupportingDocumentId);

                    TemplateSupportingDocument grp;

                    foreach (DCISgetTemplateNameforSupDocResult result in lst)
                    {
                        grp = new TemplateSupportingDocument();
                        grp.Template_Name = result.TemplateName;
                        grp.Template_Id = result.TemplateId;

                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

      

        public List<TemplateSupportingDocument> getTemplateSupportingDocumentn(string IsActive)
        {
            try
            {

                List<TemplateSupportingDocument> lstGroup = new List<TemplateSupportingDocument>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSupportingDocumentNResult> lst = datacontext.DCISgetSupportingDocumentN(IsActive);

                    TemplateSupportingDocument grp;

                    foreach (DCISgetSupportingDocumentNResult result in lst)
                    {
                        grp = new TemplateSupportingDocument();
                        grp.TemplateSupporting_Document = result.TemplateSupportingDocument.ToString();
                        grp.Template_Id = result.TemplateId;
                        grp.SupportingDocument_Id = result.SupportingDocumentId;
                        grp.SupportingDocument_Name = result.SupportingDocumentName;
                        grp.Template_Name = result.TemplateName;
                        grp.Is_Mandatory = result.IsMandatory;
                        lstGroup.Add(grp);

                    }
                }

                return lstGroup;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyTemplateSupportingDocumentStatus(DCISDBManager.objLib.MasterMaintenance.TemplateSupportingDocument ts)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyTemplateSupportingDocumentStatus(ts.Template_Id, ts.Is_Active, ts.Modified_By,ts.SupportingDocument_Id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CheckTaTempIDSupID(string TempID, string SupID)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISCheckSDID_andTempIDResult> Uid = datacontext.DCISCheckSDID_andTempID(TempID, SupID);

                foreach (DCISCheckSDID_andTempIDResult result in Uid)
                {
                    
                        return true;
                    
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


    }
}
