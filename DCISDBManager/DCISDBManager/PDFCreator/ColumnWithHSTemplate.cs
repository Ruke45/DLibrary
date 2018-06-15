using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.objLib.Certificate;

/*
PROGRAM-ID.       ColumnWithHSTemplate.cs
AUTHOR.           Tharaka Madusanka
COMPANY.          VOTRE IT (Pvt.) Ltd.
DATE-WRITTEN.     2016-10-08
Version.          1.1.0
*******************************************************************************/

namespace DCISDBManager.PDFCreator
{
    public class ColumnWithHSTemplate : Base_Certificate
    {    
        public ColumnWithHSTemplate(string CertificateID, CertificateRequestHeader CRH, List<CertificateRequestDetail> CRDlist, string LOGOpath, string DocPath, string AuthOffer, string Telephone)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertReqDetails = CRDlist;
            this.CertificateSavePath = DocPath;
            this.AuthoirzedOfficer = AuthOffer;
            this.AuthTelephone = Telephone;
            this.CertificateId = CertificateID;

        }

        public ColumnWithHSTemplate(CertificateRequestHeader CRH, List<CertificateRequestDetail> CRDlist, string LOGOpath, string DocPath, string AuthOffer,string SignDate)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertReqDetails = CRDlist;
            this.CertificateSavePath = DocPath;
            this.CertificateId = CRH.CertificateId1;
            this.AuthoirzedOfficer = AuthOffer;
            this.SignedDate = SignDate;
        }

        protected override void printListHead()
        {
            //----------------------------------------------------------------------------

            PdfPTable CertItemTbl = new PdfPTable(7);
            CertItemTbl.HorizontalAlignment = 1;
            CertItemTbl.WidthPercentage = 100;
            CertItemTbl.SpacingBefore = 4;
            //CertItemTbl.SpacingAfter = 230;
            CertItemTbl.DefaultCell.Border = Rectangle.BOX;
            CertItemTbl.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

                PdfPCell GoodItemHead = new PdfPCell(new Phrase("10. Goods/Item", fontTableHeader));
                GoodItemHead.FixedHeight = 20f;
                GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(GoodItemHead);

                PdfPCell ShipMarkHead = new PdfPCell(new Phrase("11. Shipping Mark", fontTableHeader));
                ShipMarkHead.FixedHeight = 20f;
                ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(ShipMarkHead);

                PdfPCell PackageHead = new PdfPCell(new Phrase("12. Package Typ/Qty", fontTableHeader));
                PackageHead.FixedHeight = 20f;
                PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(PackageHead);

                PdfPCell SummaryHead = new PdfPCell(new Phrase("13. Summary Description", fontTableHeader));
                SummaryHead.Colspan = 2;
                SummaryHead.FixedHeight = 20f;
                SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(SummaryHead);

                PdfPCell HSHead = new PdfPCell(new Phrase("14. HS Code", fontTableHeader));
                HSHead.FixedHeight = 20f;
                HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(HSHead);

                PdfPCell QttHead = new PdfPCell(new Phrase("15. Qty & Units", fontTableHeader));
                QttHead.FixedHeight = 20f;
                QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(QttHead);


            document.Add(CertItemTbl);
            //-----------------------------------------------------------------------------

        }

        protected override void printlistRow()
        {
            PdfPTable CertificateItems = new PdfPTable(7);
            CertificateItems.HorizontalAlignment = 1;
            CertificateItems.WidthPercentage = 100;
            CertificateItems.SpacingAfter = 2;
            CertificateItems.DefaultCell.Border = Rectangle.BOX;
            CertificateItems.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

            for (int i = 0; i < CertReqDetails.Count; i++)

                {
                    PdfPCell GoodItemHead = new PdfPCell(new Phrase(CertReqDetails[i].GoodItem1, fontFooterDetails));//CertReqDetails[i].GoodItem1
                    GoodItemHead.FixedHeight = 220f;
                    GoodItemHead.Border = Rectangle.LEFT_BORDER;
                    GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(GoodItemHead);

                    PdfPCell ShipMarkHead = new PdfPCell(new Phrase(CertReqDetails[i].ShippingMark1, fontFooterDetails));//CertReqDetails[i].ShippingMark1
                    ShipMarkHead.FixedHeight = 220f;
                    ShipMarkHead.Border = Rectangle.LEFT_BORDER;
                    ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(ShipMarkHead);

                    PdfPCell PackageHead = new PdfPCell(new Phrase(CertReqDetails[i].PackageDescription1, fontFooterDetails));///CertReqDetails[i].PackageType1
                    PackageHead.FixedHeight = 220f;
                    PackageHead.Border = Rectangle.LEFT_BORDER;
                    PackageHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(PackageHead);

                    PdfPCell SummaryHead = new PdfPCell(new Phrase(CertReqDetails[i].SummaryDesc1, fontFooterDetails));//CertReqDetails[i].SummaryDesc1
                    SummaryHead.Colspan = 2;
                    SummaryHead.FixedHeight = 220f;
                    SummaryHead.Border = Rectangle.LEFT_BORDER;
                    SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(SummaryHead);


                    PdfPCell HSHead = new PdfPCell(new Phrase(CertReqDetails[i].HSCode1, fontFooterDetails)); //CertReqDetails[i].HSCode1
                    HSHead.FixedHeight = 220f;
                    HSHead.Border = Rectangle.LEFT_BORDER;
                    HSHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(HSHead);

                    PdfPCell QttHead = new PdfPCell(new Phrase(CertReqDetails[i].Quantity1, fontFooterDetails));//CertReqDetails[i].Quantity1
                    QttHead.FixedHeight = 220f;
                    QttHead.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    QttHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                    CertificateItems.AddCell(QttHead);
                }
                document.Add(CertificateItems);
        }
    }
}