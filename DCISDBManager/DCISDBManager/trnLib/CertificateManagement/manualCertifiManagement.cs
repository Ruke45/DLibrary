using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Certificate;

using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.CertificateManagement
{
    public class manualCertifiManagement
    {
        public List<objmanualcertifi> getManualData(string RequestId, string cusID,string refNo)
        {
            try
            {

                List<objmanualcertifi> lstpackage = new List<objmanualcertifi>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetManualCertifiLIstResult> lst = datacontext.DCISgetManualCertifiLIst(RequestId,cusID,refNo);

                    objmanualcertifi omc;

                    foreach (DCISgetManualCertifiLIstResult result in lst)
                    {
                        omc = new objmanualcertifi();
                        omc.Refference_No = result.ReferenceNo;
                        omc.Customer_ID = result.CustomerName;
                        omc.Cust_ID = result.CustomerId;
                        omc.Req_No = result.RequestNo;

                        DateTime a = Convert.ToDateTime(result.IssuedDate);
                        omc.Issued_Date = a.ToString("yyyy/MM/dd");
                        string item = result.ItemDescription;

                        if(item=="C"){

                            omc.Item_Description = "CO";
                        
                        
                        }
                        if (item == "I")
                        {

                            omc.Item_Description = "Invoice";


                        }
                        if (item == "O")
                        {

                            omc.Item_Description = "Document";


                        }



                        omc.ExporterInvoice_No = result.ExporterInvoiceNo;
                      


                        lstpackage.Add(omc);

                    }
                }

                return lstpackage;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }

        public bool ModifyManualData(objmanualcertifi om)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                datacontext.DCISModifyManualData(om.Refference_No, om.Status_, om.Issued_Date, om.Item_Description, om.ExporterInvoice_No,om.Created_By,om.Customer_ID);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public bool CreateManualData(objmanualcertifi mc)
        {
          

            try
            {

                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();



                datacontext.DCISsetManualData(mc.Refference_No, mc.Issued_Date, mc.ExporterInvoice_No, mc.Item_Description,mc.Status_,mc.Created_By,mc.Customer_ID);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }


        public bool CheckPackageDescription(string PackageDescription)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISCheckPackageDescriptionResult> Upd = datacontext.DCISCheckPackageDescription(PackageDescription);

                foreach (DCISCheckPackageDescriptionResult result in Upd)
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



        public String getInvoicedTrue(string RequestId)
        {
            try
            {
                DCISLCDataContext datacontext = new DCISLCDataContext();
                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetInvNoMResult> Upd = datacontext.DCISgetInvNoM(RequestId);

                foreach (DCISgetInvNoMResult result in Upd)
                {
                   
                        return result.RequestNo;
                    
                }
                return "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return "";
            }
        }



    }
}
