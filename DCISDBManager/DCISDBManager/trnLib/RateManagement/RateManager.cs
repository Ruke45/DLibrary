using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.objLib.Rates;
using System.Configuration;
using DCISDBManager.trnLib.Utility;

namespace DCISDBManager.trnLib.RateManagement
{
    public class RateManager
    {
        public List<Rates> getCustomerRates(string CustometId,string Status)
        {
            try
            {

                List<Rates> Requests = new List<Rates>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetCustomerRateDetailsResult> lst = datacontext.DCISgetCustomerRateDetails(CustometId,Status);

                    Rates req;

                    foreach (DCISgetCustomerRateDetailsResult result in lst)
                    {
                        req = new Rates();
                        req.RateId1 = result.RateId;
                        req.RateName1 = result.RateName;
                        req.Rates1 = result.Rates;
                        req.PaidType1 = result.PaidType;

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



        public List<Rates> getAllRates(string status)
        {
            try
            {

                List<Rates> Requests = new List<Rates>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetAllRateDetailsResult> lst = datacontext.DCISgetAllRateDetails(status);

                    Rates req;

                    foreach (DCISgetAllRateDetailsResult result in lst)
                    {
                        req = new Rates();
                        req.RateId1 = result.RateId;
                        req.RateName1 = result.RateName;
                       


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



        public bool setCustomerRate(Rates req)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.Connection.Open();
                    try
                    {
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        datacontext.DCISsetCustomerRate(req.CustomerId1, req.RatesId1, req.Rates1);
                        datacontext.DCISsetPaidType(req.CustomerId1, req.PaidType1);
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex) {
                        datacontext.Transaction.Rollback();
                        ErrorLog.LogError(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public bool ModifyRateData(Rates req)
        {
            try
            {
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {

                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    datacontext.Connection.Open();
                    try
                    {
                        datacontext.Transaction = datacontext.Connection.BeginTransaction();
                        datacontext.DCISModifyCustomerRate(req.CustomerId1, req.RatesId1, req.Rates1);
                        
                        datacontext.SubmitChanges();
                        datacontext.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex) {
                        datacontext.Transaction.Rollback();
                        ErrorLog.LogError(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }
        }


        public List<Rates> getMemberRates(string Member,string Status)
        {
            try
            {

                List<Rates> Requests = new List<Rates>();
                using (DCISLCDataContext datacontext = new DCISLCDataContext())
                {
                    datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                    System.Data.Linq.ISingleResult<DCISgetMemberRateResult> lst = datacontext.DCISgetMemberRate(Member,Status);

                    Rates req;

                    foreach (DCISgetMemberRateResult result in lst)
                    {
                        req = new Rates();
                        req.RateId1 = result.RateID;
                        req.RateName1 = result.RateName;
                        req.StringRateValue1 =Math.Round(result.RateValue,2).ToString();


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





    }
}
