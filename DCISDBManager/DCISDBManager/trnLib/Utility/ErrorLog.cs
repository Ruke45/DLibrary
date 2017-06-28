using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.Utility
{
   public class ErrorLog
    {
            public static void LogError(Exception ex)
            {
                

                string Message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                Message += Environment.NewLine;
                Message += "-----------------------------------------------------------";
                Message += Environment.NewLine;
                Message += string.Format("Message: {0}", ex.Message);
                Message += Environment.NewLine;
                Message += string.Format("StackTrace: {0}", ex.StackTrace);
                Message += Environment.NewLine;
                Message += string.Format("Source: {0}", ex.Source);
                Message += Environment.NewLine;
                Message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                Message += Environment.NewLine;
                Message += "-----------------------------------------------------------";
                Message += Environment.NewLine;

                string settedPath = System.Configuration.ConfigurationManager.AppSettings["ErroLogPath"];
                if (!Directory.Exists(settedPath))
                {
                    Directory.CreateDirectory(settedPath);
                }
                string DirectoryPath = settedPath + "Error_Log_" + DateTime.Now.ToString("yyyy_MM_dd")+".txt";
                using (StreamWriter writer = new StreamWriter(DirectoryPath, true))
                {
                    writer.WriteLine(Message);
                    writer.Close();
                }
            }

            public static void LogError(string Info, Exception ex)
            {


                string Message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                Message += Environment.NewLine;
                Message += "-----------------------------------------------------------";
                Message += Environment.NewLine;
                Message += string.Format("Message: {0}", ex.Message);
                Message += Environment.NewLine;
                Message += string.Format("StackTrace: {0}", ex.StackTrace);
                Message += Environment.NewLine;
                Message += string.Format("Source: {0}", ex.Source);
                Message += Environment.NewLine;
                Message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                Message += Environment.NewLine;
                Message += string.Format("Message Info : {0}", Info);
                Message += Environment.NewLine;
                Message += "-----------------------------------------------------------";
                Message += Environment.NewLine;

                string settedPath = System.Configuration.ConfigurationManager.AppSettings["ErroLogPath"];
                if (!Directory.Exists(settedPath))
                {
                    Directory.CreateDirectory(settedPath);
                }
                string DirectoryPath = settedPath + "Error_Log_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                using (StreamWriter writer = new StreamWriter(DirectoryPath, true))
                {
                    writer.WriteLine(Message);
                    writer.Close();
                }
            }


        }
}
