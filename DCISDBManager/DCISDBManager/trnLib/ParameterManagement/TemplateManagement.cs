using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.ParameterManagement
{
    public class TemplateManagement
    {
        public string getCustomerTemplate(string CustomerID)
        {
            try
            {
                string Temp = string.Empty;
                using (DCISLCDataContext dbContext = new DCISLCDataContext())
                {
                    
                    foreach (var p in dbContext.DCISgetCustomerTemplate(CustomerID).ToList())
                    {
                        Temp = p.TemplateId.ToString();
                    }
                }

                return Temp;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }


        }
    }
}
