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
   public class ConsigneeManagement
    {
        public bool ModifyConsignee(DCISDBManager.objLib.MasterMaintenance.Consignee C)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyConsignee(C.Code_, C.Description_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreateConsignee(DCISDBManager.objLib.MasterMaintenance.Consignee C)
        {


            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                datacontext.DCISsetConsignee(C.Code_, C.Description_, C.Is_Active);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public List<Consignee> getConsignee(string IsActive)
        {
            try
            {

                List<Consignee> lstGroup = new List<Consignee>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetConsigneeResult> lst = datacontext.DCISgetConsignee(IsActive);

                    Consignee grp;

                    foreach (DCISgetConsigneeResult result in lst)
                    {
                        grp = new Consignee();
                        grp.Code_ = result.Code;
                        grp.Description_ = result.Description;
                        grp.Is_Active = result.Status;
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

        public bool ModifyConsigneeStatus(DCISDBManager.objLib.MasterMaintenance.Consignee C)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyConsigneeStatus(C.Code_, C.Is_Active);
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
