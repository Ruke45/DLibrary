using DCISDBManager.objLib.Template;
using DCISDBManager.trnLib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DCISDBManager.trnLib.MasterDataManagement
{
    public class TemplateDownloadManager
    {
        public List<CertificateTemplate> getDownloadTemplates()
        {
            try
            {

                List<CertificateTemplate> TemplateList = new List<CertificateTemplate>();
                DCISLCDataContext datacontext = new DCISLCDataContext();

                datacontext.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DocMgmtDBConnectionString"].ToString();
                System.Data.Linq.ISingleResult<DCISgetAllTemplateDownloadResult> lst = datacontext.DCISgetAllTemplateDownload();
                foreach (DCISgetAllTemplateDownloadResult Result in lst)
                {
                    CertificateTemplate Temp = new CertificateTemplate();
                    Temp.Downlaoded_Time = Convert.ToInt64(Result.DownlaodedTime);
                    Temp.Download_Path = Result.DownloadPath;
                    Temp.Index_No = Result.IndexNo;
                    Temp.Template_Display_Name = Result.TemplateDName;
                    Temp.Template_ID = Result.TemplateID;
                    Temp.Template_IMG_Path = Result.TemplateIMGPath;

                    TemplateList.Add(Temp);

                };
                return TemplateList;

            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return null;
            }

        }
    }
}
