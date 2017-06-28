using DCISDBManager.objLib.Master;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.MasterDataManagement
{
    public class RejectResonManagment
    {
        public List<RejectResons> getCertificaterRejectResons()
        {
            try
            {


                List<RejectResons> resontList = new List<RejectResons>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCertificateRejectResonsResult> lst = datacontext.DCISgetCertificateRejectResons();

                    foreach (DCISgetCertificateRejectResonsResult Result in lst)
                    {
                        RejectResons reson = new RejectResons();
                        reson.Reason_ = Result.ReasonName;
                        reson.Reason_Code = Result.RejectCode;
                        reson.Reason_Type_ID = Result.Category;
                        resontList.Add(reson);
                    }



                }
                return resontList;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }

        public List<RejectResons> getSDRejectResons()
        {
            try
            {


                List<RejectResons> resontList = new List<RejectResons>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetSupportingDocRejectResonsResult> lst = datacontext.DCISgetSupportingDocRejectResons();

                    foreach (DCISgetSupportingDocRejectResonsResult Result in lst)
                    {
                        RejectResons reson = new RejectResons();
                        reson.Reason_ = Result.ReasonName;
                        reson.Reason_Code = Result.RejectCode;
                        reson.Reason_Type_ID = Result.Category;
                        resontList.Add(reson);
                    }



                }
                return resontList;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
        }
    }
}
