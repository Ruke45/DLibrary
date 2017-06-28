using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Template
{
    public class CustomerTemplate
    {
        
        string CustomerId;

        public string CustomerId1
        {
            get { return CustomerId; }
            set { CustomerId = value; }
        }
        string TemplateId;

        public string TemplateId1
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }

        string TemplateName;

        public string TemplateName1
        {
            get { return TemplateName; }
            set { TemplateName = value; }
        }
        string ImgUrl;

        public string ImgUrl1
        {
            get { return ImgUrl; }
            set { ImgUrl = value; }
        }
        string Discription;

        public string Discription1
        {
            get { return Discription; }
            set { Discription = value; }
        }
    }
}
