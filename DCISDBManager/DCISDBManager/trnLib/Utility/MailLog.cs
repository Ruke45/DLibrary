using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.Utility
{
    public class MailLog
    {
        public static void LogMail(string Msg)
        {
            string Message = Msg;
            string settedPath = System.Configuration.ConfigurationManager.AppSettings["ErroLogPath"];
            if (!Directory.Exists(settedPath))
            {
                Directory.CreateDirectory(settedPath);
            }
            string DirectoryPath = settedPath + "Mail_Sync_Log_" + DateTime.Now.ToString("yyyy_MM_dd_HH") + ".txt";
            using (StreamWriter writer = new StreamWriter(DirectoryPath, true))
            {
                writer.WriteLine(Message);
                writer.Close();
            }
        }
    }
}
