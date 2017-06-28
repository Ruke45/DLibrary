using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.ContactDetail
{
   public class ContactDetail
    {
        long SeqId;

        public long SeqId1
        {
            get { return SeqId; }
            set { SeqId = value; }
        }
        string Name;

        public string Name1
        {
            get { return Name; }
            set { Name = value; }
        }
        string Email;

        public string Email1
        {
            get { return Email; }
            set { Email = value; }
        }
        string Phone;

        public string Phone1
        {
            get { return Phone; }
            set { Phone = value; }
        }
        string Detail;

        public string Detail1
        {
            get { return Detail; }
            set { Detail = value; }
        }
        string Status;

        public string Status1
        {
            get { return Status; }
            set { Status = value; }
        }
        string CreatedDate;

        public string CreatedDate1
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
    }
}
