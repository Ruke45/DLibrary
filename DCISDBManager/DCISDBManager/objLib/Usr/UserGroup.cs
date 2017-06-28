using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Usr
{
    public class UserGroup
    {
        string GroupId;

        public string GroupId1
        {
            get { return GroupId; }
            set { GroupId = value; }
        }
        string GroupName;

        public string GroupName1
        {
            get { return GroupName; }
            set { GroupName = value; }
        }
        string IsActive;

        public string IsActive1
        {
            get { return IsActive; }
            set { IsActive = value; }
        }
        string CreatedBy;

        public string CreatedBy1
        {
            get { return CreatedBy; }
            set { CreatedBy = value; }
        }
        string ModifiedBy;

        public string ModifiedBy1
        {
            get { return ModifiedBy; }
            set { ModifiedBy = value; }
        }


    }
}
