using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Template
{
    public class CertificateTemplate
    {
        Int64 IndexNo;

        public Int64 Index_No
        {
            get { return IndexNo; }
            set { IndexNo = value; }
        }
        string TemplateID;

        public string Template_ID
        {
            get { return TemplateID; }
            set { TemplateID = value; }
        }
        string TemplateDisplayName;

        public string Template_Display_Name
        {
            get { return TemplateDisplayName; }
            set { TemplateDisplayName = value; }
        }
        string TemplateIMGPath;

        public string Template_IMG_Path
        {
            get { return TemplateIMGPath; }
            set { TemplateIMGPath = value; }
        }
        string DownloadPath;

        public string Download_Path
        {
            get { return DownloadPath; }
            set { DownloadPath = value; }
        }
        Int64 DownlaodedTime;

        public Int64 Downlaoded_Time
        {
            get { return DownlaodedTime; }
            set { DownlaodedTime = value; }
        }
    }
}
