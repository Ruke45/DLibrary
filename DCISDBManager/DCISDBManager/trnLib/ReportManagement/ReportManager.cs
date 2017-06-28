using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.ReportManagement
{
    public class ReportManager
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ConnectionString);

        public DataTable getCORegistryReport(DateTime FDate, DateTime TDate, string CertRateID, string InvoSupDID, string InvoRID, string OthrDRID, string CID, string NCEM, string PayType)
        {
            try
            {
                connection.Open();

                DataTable dt = new DataTable();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = connection;

                cmd.CommandText = "DCISgetCORegistry";

                SqlParameter paraFrmDate = new SqlParameter("@FromDate", SqlDbType.VarChar);
                SqlParameter paraToDate = new SqlParameter("@ToDate", SqlDbType.VarChar);
                SqlParameter paraCertificateRateID = new SqlParameter("@CertificateRateId", SqlDbType.VarChar);
                SqlParameter paraInvoicSDID = new SqlParameter("@InvoiceSupportingDocId", SqlDbType.VarChar);
                SqlParameter paraInvoRateID = new SqlParameter("@InvoicerateId", SqlDbType.VarChar);
                SqlParameter paraOtheDRID = new SqlParameter("@OtherDocumentRateId", SqlDbType.VarChar);
                SqlParameter paraCustomerID = new SqlParameter("@CustomerId", SqlDbType.VarChar);
                SqlParameter paraNCEMember = new SqlParameter("@NCEMember", SqlDbType.VarChar);
                SqlParameter paraPayType = new SqlParameter("@Paytype", SqlDbType.VarChar);

                paraFrmDate.Value = FDate.ToString("yyyyMMdd");
                paraToDate.Value = TDate.ToString("yyyyMMdd");
                paraCertificateRateID.Value = CertRateID;
                paraInvoicSDID.Value = InvoSupDID;
                paraInvoRateID.Value = InvoRID;
                paraOtheDRID.Value = OthrDRID;
                paraCustomerID.Value = CID;
                paraNCEMember.Value = NCEM;
                paraPayType.Value = PayType;

                cmd.Parameters.Add(paraFrmDate);
                cmd.Parameters.Add(paraToDate);
                cmd.Parameters.Add(paraCertificateRateID);
                cmd.Parameters.Add(paraInvoicSDID);
                cmd.Parameters.Add(paraInvoRateID);
                cmd.Parameters.Add(paraOtheDRID);
                cmd.Parameters.Add(paraCustomerID);
                cmd.Parameters.Add(paraNCEMember);
                cmd.Parameters.Add(paraPayType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                return dt;


            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }
            finally
            {

                connection.Close();

            }

        }
    }
}
