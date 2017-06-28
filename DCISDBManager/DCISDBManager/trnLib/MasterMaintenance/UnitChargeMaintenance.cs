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
    public class UnitChargeMaintenance
    {
        //string invoiceno = null;
      

        public List<CertificateUnitCharge> getUnitCharge(string TemplateId)
        {
            try
            {

                List<CertificateUnitCharge> lstcharges= new List<CertificateUnitCharge>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUnitChageResult> lst = datacontext.DCISgetUnitChage(TemplateId);

                    CertificateUnitCharge cu;

                    foreach (DCISgetUnitChageResult result in lst)
                    {
                        cu = new CertificateUnitCharge();
                        cu.Template_Id = result.TemplateId;
                        cu.UnitCharge_Value = result.UnitChargeValue;
                        cu.Modified_By = result.ModifiedBy;
                        cu.Created_By = result.CreatedBy;
                        cu.Created_Date = result.CreatedDate;
                        cu.Modified_Date = result.ModifiedDate;


                        lstcharges.Add(cu);

                    }
                }

                return lstcharges;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public List<CertificateUnitCharge> getUnitChargen(string TemplateId)
        {
            try
            {

                List<CertificateUnitCharge> lstcharges = new List<CertificateUnitCharge>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTemplateUnitChargeResult> lst = datacontext.DCISgetTemplateUnitCharge(TemplateId);

                    CertificateUnitCharge cu;

                    foreach (DCISgetTemplateUnitChargeResult result in lst)
                    {
                        cu = new CertificateUnitCharge();
                        cu.Template_Name = result.TemplateName;
                        cu.Template_Id = result.TemplateId;
                        cu.UnitCharge_Value = result.UnitChargeValue;
                        cu.Modified_By = result.ModifiedBy;
                        cu.Created_By = result.CreatedBy;
                        cu.Created_Date = result.CreatedDate;
                        cu.Modified_Date = result.ModifiedDate;
                      //  cu.Is_Active = result.IsActive;


                        lstcharges.Add(cu);

                    }
                }

                return lstcharges;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public List<CertificateUnitCharge> getTemplateID(string TemplateId)
        {
            try
            {

                List<CertificateUnitCharge> lstcharges = new List<CertificateUnitCharge>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTempalteIDResult> lst = datacontext.DCISgetTempalteID(TemplateId);

                    CertificateUnitCharge cu;

                    foreach (DCISgetTempalteIDResult result in lst)
                    {
                        cu = new CertificateUnitCharge();
                        cu.Template_Id = result.TemplateId;
                        cu.Template_Name = result.TemplateName;


                        lstcharges.Add(cu);

                    }
                }

                return lstcharges;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyUnitCharge(DCISDBManager.objLib.MasterMaintenance.CertificateUnitCharge cuc)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyCertificateUnitCharge(cuc.Template_Id, cuc.Modified_By,  cuc.UnitCharge_Value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        string InvoiceNo = string.Empty;
        public bool CreateUnitCharge(DCISDBManager.objLib.MasterMaintenance.CertificateUnitCharge cuc)
        {

            try
            {
                
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               // SequenceManager seqmanager = new SequenceManager();
               // Int64 RequestNo = seqmanager.getNextSequence("TemplateID", datacontext);
               // InvoiceNo = "temp" + RequestNo.ToString();
                //cuc.Template_Id


                datacontext.DCISsetCertificateUnitCharge(cuc.Template_Id, cuc.Modified_By, cuc.UnitCharge_Value, cuc.Created_By,cuc.Is_Active);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool CheckTempID(string TempID)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetUnitChageResult> Uid = datacontext.DCISgetUnitChage(TempID);

                foreach (DCISgetUnitChageResult result in Uid)
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
