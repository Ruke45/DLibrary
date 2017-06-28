using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.TaxDetails;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.TaxManagement
{
   public class CustometTaxDetailManager
    {
        public bool setTaxData(setCustomerTaxDetails req)
        {
            try
            {
                string check = null;
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCIScheckTaxCodeResult> lst = datacontext.DCIScheckTaxCode(req.TaxCode1, req.CustomerId1);

                foreach (DCIScheckTaxCodeResult result in lst)
                {
                    check = result.CustomerId;    

                }
                if (check == null)
                {
                    datacontext.DCISsetCustomerApplicableTax(req.CustomerId1, req.TaxCode1, req.TaxRegistrationNo1, req.CreatedBy1, req.IsActive1, req.RequestId1);

                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

        public List<setCustomerTaxDetails> getTaxDetail(string CustomerId,string IsActive)
        {
            try
            {

                List<setCustomerTaxDetails> Requests = new List<setCustomerTaxDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerTaxResult> lst = datacontext.DCISgetCustomerTax(CustomerId,IsActive);

                    setCustomerTaxDetails req;

                    foreach (DCISgetCustomerTaxResult result in lst)
                    {
                        req = new setCustomerTaxDetails();
                        req.CustomerId1 = result.CustomerId;
                        req.TaxCode1 = result.TaxCode;
                        req.TaxName1 = result.TaxName;
                        req.TaxRegistrationNo1 = result.TaxRegistrationNo;
                        req.TaxPersentage1 = result.TaxPercentage;
                       
                       
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

        public bool ModifyTaxData(setCustomerTaxDetails req)
        {
            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyCustomerTax(req.CustomerId1, req.TaxCode1, req.TaxRegistrationNo1,req.IsActive1);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }

    }
}
