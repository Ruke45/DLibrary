using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCISDBManager.objLib.Master
{
    public class SDSignatureConfig
    {
        /*
* llx(lower left x coordinate) = margin from left.
* lly(lower left y coordinate) = margin from bottom(bottom of rectangle)
* urx(upper right x coordinate) = width of article
* ury(upper right y coordinate) = margin from bottom of upper boundary of article.
*/

        string SupportingDocID;

        public string Supporting_Doc_ID
        {
            get { return SupportingDocID; }
            set { SupportingDocID = value; }
        }
        double LLXcordinates;

        public double LLX_Cordinates
        {
            get { return LLXcordinates; }
            set { LLXcordinates = value; }
        }
        double LLYcordinates;

        public double LLY_Cordinates
        {
            get { return LLYcordinates; }
            set { LLYcordinates = value; }
        }
        double URXcordinates;

        public double URX_cordinates
        {
            get { return URXcordinates; }
            set { URXcordinates = value; }
        }
        double URYcordinates;

        public double URY_cordinates
        {
            get { return URYcordinates; }
            set { URYcordinates = value; }
        }
    }
}
