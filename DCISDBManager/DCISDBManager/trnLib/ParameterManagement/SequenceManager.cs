using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCISDBManager.trnLib.Utility;
using System.Configuration;

namespace DCISDBManager.trnLib.ParameterManagement
{
    public class SequenceManager
    {
        public Int64 getNextSequence(string SequenceName, DCISLCDataContext dbContext)
        {
            try
            {
                Int64 SeqNo = 0;
                foreach (var p in dbContext.DCISgetSequence(SequenceName).ToList())
                {
                    SeqNo = Int64.Parse(p.SequesnceValue.ToString());
                }

                return SeqNo;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message.ToString());
                return 0;
            }


        }


        public Int64 getNextSequence(string SequenceName)
        {
            try
            {
                DCISLCDataContext dbContext = new DCISLCDataContext();
                dbContext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                Int64 SeqNo = 0;
                foreach (var p in dbContext.DCISgetSequence(SequenceName).ToList())
                {
                    SeqNo = Int64.Parse(p.SequesnceValue.ToString());
                }

                return SeqNo;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return 0;
            }


        }

    }
}
