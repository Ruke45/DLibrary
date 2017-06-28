using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.MasterMaintenance
{
   public  class SignatureLevels
    {
       String UserId;

       public string User_Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

       String LevelId;

       public string Level_Id
        {
            get { return LevelId; }
            set { LevelId = value; }
        }

       String TemplateId;

       public string Template_Id
        {
            get { return TemplateId; }
            set { TemplateId = value; }
        }

       String IsActive;
       public string Is_Active
       {
           get { return IsActive; }
           set { IsActive = value; }
       }

       String CreatedBy;
       public string Created_By
       {
           get { return CreatedBy; }
           set { CreatedBy = value; }
       }

       String ModifiedBy;
       public string Modified_By
       {
           get { return ModifiedBy; }
           set { ModifiedBy = value; }
       }



    }
}
