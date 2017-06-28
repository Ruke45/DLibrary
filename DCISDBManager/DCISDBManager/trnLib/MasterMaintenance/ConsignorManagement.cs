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
    public class ConsignorManagement
    {
        public bool ModifyConsignor(DCISDBManager.objLib.MasterMaintenance.Consignor C)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyConsignor(C.Code_,C.Description_);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreateConsignor(DCISDBManager.objLib.MasterMaintenance.Consignor C)
        {
           

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();


                datacontext.DCISsetConsignor(C.Code_, C.Description_,C.Is_Active);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        public List<Consignor> getConsignor( string IsActive)
        {
            try
            {

                List<Consignor> lstGroup = new List<Consignor>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetConsignorResult> lst = datacontext.DCISgetConsignor( IsActive);

                    Consignor grp;

                    foreach (DCISgetConsignorResult result in lst)
                    {
                        grp = new Consignor();
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

        public bool ModifyConsignorStatus(DCISDBManager.objLib.MasterMaintenance.Consignor C)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyConsignorStatus(C.Code_, C.Is_Active);
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
