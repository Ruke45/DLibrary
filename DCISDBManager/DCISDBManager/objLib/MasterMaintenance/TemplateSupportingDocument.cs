using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
    public class TemplateSupportingDocument
    {

        String TemplateSupportingDocuments;

        public string TemplateSupporting_Document
        {
            get { return TemplateSupportingDocuments; }
            set { TemplateSupportingDocuments = value; }
        }


        String SupportingDocumentId;

        public string SupportingDocument_Id
        {
            get { return SupportingDocumentId; }
            set { SupportingDocumentId = value; }
        }

        String TemplateId;

        public string Template_Id
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }
       

        String SupportingDocumentName;

        public string SupportingDocument_Name
        {
            get { return SupportingDocumentName; }
            set { SupportingDocumentName = value; }
        }

        String TemplateName;

        public string Template_Name
        {
            get { return TemplateName; }
            set { TemplateName = value; }
        }


        String IsActive;
            public string Is_Active
        {
            get { return IsActive; }
            set { IsActive = value; }
        }

        String ModifiedBy;
        public string Modified_By
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }

        String CreatedBy;
        public string Created_By
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }

        String IsMandatory;
        public string Is_Mandatory
        {
            get { return IsMandatory; }
            set { IsMandatory = value; }
        }






    }
}
