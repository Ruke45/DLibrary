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
   public class TaxDataMaintenance
    {
       public List<Tax> getTaxData(string TaxCode,String IsActive)
       {
           try
           {

               List<Tax> lstUser = new List<Tax>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetTaxResult> lst = datacontext.DCISgetTax(TaxCode, IsActive);

                   Tax tx;

                   foreach (DCISgetTaxResult result in lst)
                   {
                       tx = new Tax();
                      // tx.Tax_Id = result.TaxId;
                       tx.Tax_Name = result.TaxName;
                       tx.Tax_Code = result.TaxCode;
                      // tx.Tax_Date = result.TaxDate;
                       tx.Tax_Priority = int.Parse(result.TaxPriority.ToString());
                       tx.Tax_Percentage = result.TaxPercentage;
                       tx.Is_Active = result.IsActive;
                       lstUser.Add(tx);

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

       public bool ModifyTaxData(DCISDBManager.objLib.MasterMaintenance.Tax tm)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifyTax( tm.Tax_Code, tm.Tax_Name,tm.Tax_Percentage,tm.Tax_Priority);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }

       public bool CreateTaxData(DCISDBManager.objLib.MasterMaintenance.Tax tm)
       {
           string TaxID = string.Empty;
           try
           {

               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               SequenceManager seqmanager = new SequenceManager();
               Int64 taxno = seqmanager.getNextSequence("TaxID", datacontext);
               TaxID = "tax" + taxno.ToString();

               datacontext.DCISsetTax(tm.Is_Active, tm.Tax_Code, tm.Tax_Name, tm.Tax_Percentage, tm.Tax_Priority);


               return true;
           }
           catch (Exception ex)
           {
               ErrorLog.LogError(ex);
               return false;
           }

       }

       public List<TaxPriorityList> getTaxPriorityList(string  PriorityNo)
       {
           try
           {

               List<TaxPriorityList> lstUser = new List<TaxPriorityList>();
               using (DCISLCDataContext datacontext = new DCISLCDataContext())
               {
                   datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                   System.Data.Linq.ISingleResult<DCISgetTaxPriorityListResult> lst = datacontext.DCISgetTaxPriorityList(PriorityNo.ToString());

                   TaxPriorityList tx;

                   foreach (DCISgetTaxPriorityListResult result in lst)
                   {
                       tx = new TaxPriorityList();
                       tx.Priority_No = result.PriorityNo;
                       tx.Priority_Description = result.PriorityDescription;                   
                       lstUser.Add(tx);

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

       public bool ModifyTaxDataStatus(DCISDBManager.objLib.MasterMaintenance.Tax tm)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();

               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               datacontext.DCISModifyTaxStatus(tm.Tax_Code, tm.Is_Active, tm.Modified_By);
               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message.ToString());
               return false;
           }
       }

       public bool CheckTaxCode(string TaxCode)
       {
           try
           {
               DCISLCDataContext datacontext = new DCISLCDataContext();
               datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
               System.Data.Linq.ISingleResult<DCISgetTaxCodeResult> Uid = datacontext.DCISgetTaxCode(TaxCode);

               foreach (DCISgetTaxCodeResult result in Uid)
               {
                   if (result.TaxCode.Equals(TaxCode))
                   {
                       return true;
                   }
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
