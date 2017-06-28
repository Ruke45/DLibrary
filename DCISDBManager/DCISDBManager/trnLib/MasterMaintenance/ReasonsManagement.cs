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
    public class ReasonsManagement
    {
        public List<ReasonsMaintenance> getReasons(string RejectCode)
        {
            try
            {

                List<ReasonsMaintenance> lstreason = new List<ReasonsMaintenance>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRejectReasonsResult> lst = datacontext.DCISgetRejectReasons(RejectCode);

                    ReasonsMaintenance rr;

                    foreach (DCISgetRejectReasonsResult result in lst)
                    {
                        rr = new ReasonsMaintenance();
                        rr.Reject_Code = result.RejectCode;
                        rr.Reason_Name = result.ReasonName;
                        rr.Category_ = result.Category;
                        lstreason.Add(rr);

                    }
                }

                return lstreason;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
        public List<ReasonsMaintenance> getReasonsCategory(string RejectCategory)
        {
            try
            {

                List<ReasonsMaintenance> lstreason = new List<ReasonsMaintenance>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRejectReasonsCategoryResult> lst = datacontext.DCISgetRejectReasonsCategory(RejectCategory);

                    ReasonsMaintenance rr;

                    foreach (DCISgetRejectReasonsCategoryResult result in lst)
                    {
                        rr = new ReasonsMaintenance();
                        rr.Category_ = result.RejectReasonsCategory;

                        lstreason.Add(rr);

                    }
                }

                return lstreason;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyReasons(DCISDBManager.objLib.MasterMaintenance.ReasonsMaintenance rr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyRejectReasons(rr.Reject_Code, rr.Category_, rr.Reason_Name, rr.Modified_By);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreateReasons(DCISDBManager.objLib.MasterMaintenance.ReasonsMaintenance rr)
        {
            string RejectCode = string.Empty;
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();

                SequenceManager seqmanager = new SequenceManager();
                Int64 ptype = seqmanager.getNextSequence("RejectCode", datacontext);
                RejectCode = "RC" + ptype.ToString();
                datacontext.DCISsetRejectReasons(RejectCode, rr.Reason_Name, rr.Category_, rr.Is_Active, rr.Created_By);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public bool ModifyReasonsStatus(DCISDBManager.objLib.MasterMaintenance.ReasonsMaintenance rr)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyRejectReasonsStatus(rr.Reject_Code, rr.Is_Active, rr.Modified_By);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public List<ReasonsMaintenance> getReasonsn(string RejectCode, string IsActive)
        {
            try
            {

                List<ReasonsMaintenance> lstreason = new List<ReasonsMaintenance>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetRejectReasonsnResult> lst = datacontext.DCISgetRejectReasonsn(RejectCode, IsActive);

                    ReasonsMaintenance rr;

                    foreach (DCISgetRejectReasonsnResult result in lst)
                    {
                        rr = new ReasonsMaintenance();
                        rr.Reject_Code = result.RejectCode;
                        rr.Reason_Name = result.ReasonName;
                        rr.Category_ = result.Category;
                        rr.Is_Active = result.IsActive;
                        rr.Created_By = result.CreatedBy;
                        rr.Modified_By = result.ModifiedBy;
                        lstreason.Add(rr);

                    }
                }

                return lstreason;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public List<ReasonsMaintenance> getRejectReason(string RejectCategory)
        {
            try
            {

                List<ReasonsMaintenance> lstreason = new List<ReasonsMaintenance>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAdminRejectReasonResult> lst = datacontext.DCISgetAdminRejectReason(RejectCategory);

                    ReasonsMaintenance rr;

                    foreach (DCISgetAdminRejectReasonResult result in lst)
                    {
                        rr = new ReasonsMaintenance();
                        rr.Reason_Name = result.ReasonName;
                        rr.Reject_Code = result.RejectCode;

                        lstreason.Add(rr);

                    }
                }

                return lstreason;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

    }
}
