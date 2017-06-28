using DCISDBManager.objLib.TaxDetails;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace DCISDBManager.trnLib.TaxManagement
{
    public class TaxManager
    {
        public List<getTaxDetails> getTaxDetails(string IsActive, string IsVat)
        {
            try
            {

                List<getTaxDetails> taxdetail = new List<getTaxDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetTaxDetailsResult> lst = datacontext.DCISgetTaxDetails(IsActive,IsVat);

                    getTaxDetails tax;

                    foreach (DCISgetTaxDetailsResult result in lst)
                    {
                        
                            tax = new getTaxDetails();
                            tax.TaxName1 = result.TaxName;
                            tax.TaxPercentage1 = result.TaxPercentage;
                            tax.TaxCode1 = result.TaxCode;

                            taxdetail.Add(tax);
                        


                    }
                }

                return taxdetail;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }



        public List<getTaxDetails> getUserTaxDetails(String CustomerId,string IsActive)
        {
            try
            {

                List<getTaxDetails> taxdetail = new List<getTaxDetails>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetUserTaxDetailsResult> lst = datacontext.DCISgetUserTaxDetails(CustomerId,IsActive);

                    getTaxDetails tax;

                    foreach (DCISgetUserTaxDetailsResult result in lst)
                    {
                        tax = new getTaxDetails();
                        tax.TaxName1 = result.TaxName;
                        tax.TaxPercentage1 = Decimal.Parse(result.TaxPercentage.ToString());
                        tax.TaxRegistrationNo1 = result.TaxRegistrationNo;
                        tax.TaxCode1 = result.TaxCode;

                        taxdetail.Add(tax);



                    }
                }

                return taxdetail;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }


        public getTaxDetails getCustometSVAT(string CustomerId)
        {
            try
            {

                getTaxDetails req = new getTaxDetails();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerSVATResult> lst = datacontext.DCISgetCustomerSVAT(CustomerId);

                    foreach (DCISgetCustomerSVATResult result in lst)
                    {
                        req = new getTaxDetails();
                        req.SVAt1 = result.IsSVat;
                        
                    }
                   
                }

                return req;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifySVATData(string vat,string VATId,string CustomerId)
        {
            try
            {
                string id = null;
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCIScheckVATCustomerResult> lst = datacontext.DCIScheckVATCustomer(CustomerId, VATId);
                foreach (DCIScheckVATCustomerResult result in lst)
                    {
                      id = result.TaxCode;
                    }
                if (id == VATId && vat=="1")
                {
                    return false;
                }
                else {
                    
                    datacontext.DCISmodifyCustomerSVAT(CustomerId, vat);
                    return true;
                }


                
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }
    }
}
