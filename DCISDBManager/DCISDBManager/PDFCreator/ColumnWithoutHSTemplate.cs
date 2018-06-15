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
PROGRAM-ID.       ColumnWithoutHSTemplate.cs
AUTHOR.           Tharaka Madusanka
COMPANY.          VOTRE IT (Pvt.) Ltd.
DATE-WRITTEN.     2016-10-08
Version.          1.1.0
*******************************************************************************/
namespace DCISDBManager.PDFCreator
{
    public class ColumnWithoutHSTemplate : Base_Certificate
    {

        public ColumnWithoutHSTemplate(CertificateRequestHeader CRH, List<CertificateRequestDetail> CRDlist, string LOGOpath, string DocPath, string AuthOffer,string Date)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertReqDetails = CRDlist;
            this.CertificateSavePath = DocPath;
            this.CertificateId = CRH.CertificateId1;
            this.AuthoirzedOfficer = AuthOffer;
            this.SignedDate = Date;
        }

        public ColumnWithoutHSTemplate(string CertificateID, CertificateRequestHeader CRH, List<CertificateRequestDetail> CRDlist, string LOGOpath, string DocPath, string AuthOffer, string Telephone)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertReqDetails = CRDlist;
            this.CertificateSavePath = DocPath;
            this.AuthoirzedOfficer = AuthOffer;
            this.AuthTelephone = Telephone;
            this.CertificateId = CertificateID;

        }

        protected override void drawBottom(string Comment)
        {
            PdfPTable table = new PdfPTable(4);

            table.HorizontalAlignment = 1;
            table.WidthPercentage = 100;
            table.SpacingBefore = 0;
            table.SpacingAfter = 0;
            table.DefaultCell.Border = Rectangle.BOX;
            table.SetWidths(new int[] { 1, 1, 1, 1 });


            PdfPCell bottom = new PdfPCell(new Phrase("For Office Use Only \n\n" + Comment, fontBoxHeader));
            bottom.Colspan = 3;
            bottom.FixedHeight = 180f;
            table.AddCell(bottom);

            PdfPTable nested = new PdfPTable(1);

            PdfPCell totInvoL = new PdfPCell(new Phrase("15. Total Invoice Value", fontBoxHeader));
            totInvoL.FixedHeight = 20f;
            totInvoL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totInvoL.Border = Rectangle.NO_BORDER;
            nested.AddCell(totInvoL);

            PdfPCell totInvoD = new PdfPCell(new Phrase(this.CertificateHead.TotalInvoiceValue1.ToString(), fontBoxDetail));//---------this.CertificateHead.TotalInvoiceValue1.ToString()

            totInvoD.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totInvoD.Border = Rectangle.NO_BORDER;
            totInvoD.FixedHeight = 20f;
            nested.AddCell(totInvoD);

            PdfPCell totQuantL = new PdfPCell(new Phrase("16. Total Quantity", fontBoxHeader));
            totQuantL.FixedHeight = 20f;
            totQuantL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totQuantL.Border = Rectangle.NO_BORDER;
            nested.AddCell(totQuantL);

            PdfPCell totQuntyD = new PdfPCell(new Phrase(this.CertificateHead.TotalQuantity1, fontBoxDetail)); //this.CertificateHead.TotalQuantity1

            totQuntyD.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totQuntyD.Border = Rectangle.NO_BORDER;
            totQuntyD.FixedHeight = 20f;
            nested.AddCell(totQuntyD);

            string s1 = "I declare that the goods are of Sri Lanka origin,";
            string s2 = "all particulars above are correctly stated,";
            string s3 = "and that the minimum value addition of goods exported is not less than 25% of the FOB price";

            string StatementTxt = s1 + s2 + s3;

            PdfPCell Statement = new PdfPCell(new Phrase(StatementTxt, fontSmallBold));

            Statement.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            Statement.Border = Rectangle.NO_BORDER;
            nested.AddCell(Statement);


            PdfPCell nesthousing = new PdfPCell(nested);
            nesthousing.Padding = 0f;
            table.AddCell(nesthousing);



            //----------------------------------------------------------

            PdfPTable FooterTable = new PdfPTable(4);


            FooterTable.HorizontalAlignment = 1;
            FooterTable.WidthPercentage = 100;
            FooterTable.SpacingBefore = 0;
            FooterTable.SpacingAfter = 0;
            FooterTable.DefaultCell.Border = Rectangle.BOX;
            FooterTable.SetWidths(new int[] { 1, 1, 1, 1 });

            PdfPCell Cell1 = new PdfPCell(new Phrase("Competent Authority - National Chamber of Exporters of Sri Lanka", fontSmallBold));
            Cell1.Colspan = 2;
            Cell1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            PdfPCell Cell2 = new PdfPCell(new Phrase("Submitted by", fontSmallBold));
            Cell2.Colspan = 2;
            Cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            FooterTable.AddCell(Cell1);
            FooterTable.AddCell(Cell2);


            PdfPCell NameAO = new PdfPCell(new Phrase("Name of NCE Authorized officer", fontFooterDetails));
            NameAO.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell Contact = new PdfPCell(new Phrase("Contact No +94 114651765", fontFooterDetails));
            Contact.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell SubName = new PdfPCell(new Phrase("Name & Designation", fontFooterDetails));
            SubName.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell SubContact = new PdfPCell(new Phrase("Contact No ", fontFooterDetails));
            SubContact.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;



            FooterTable.AddCell(NameAO);
            FooterTable.AddCell(Contact);
            FooterTable.AddCell(SubName);
            FooterTable.AddCell(SubContact);


            PdfPCell NameAOData = new PdfPCell(new Phrase(AuthoirzedOfficer, fontFooterDetails));
            NameAOData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            NameAOData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            NameAOData.FixedHeight = 20f;

            PdfPCell ContactData = new PdfPCell(new Phrase(AuthTelephone, fontFooterDetails));
            ContactData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            ContactData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            ContactData.FixedHeight = 20f;

            PdfPCell SubNameData = new PdfPCell(new Phrase(CertificateHead.Requester_Name + "-" + CertificateHead.Requester_Designation, fontFooterDetails)); //CertificateHead.CustomerName1
            SubNameData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubNameData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubNameData.FixedHeight = 20f;

            PdfPCell SubContactData = new PdfPCell(new Phrase(CertificateHead.Customer_Telephone, fontFooterDetails));//CertificateHead.Customer_Telephone
            SubContactData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubContactData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubContactData.FixedHeight = 20f;



            FooterTable.AddCell(NameAOData);
            FooterTable.AddCell(ContactData);
            FooterTable.AddCell(SubNameData);
            FooterTable.AddCell(SubContactData);



            PdfPCell NCEDate = new PdfPCell(new Phrase("Date", fontFooterDetails));
            NCEDate.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell NCESignaA = new PdfPCell(new Phrase("Signature of Authorized Officer", fontFooterDetails));
            NCESignaA.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell SubDate = new PdfPCell(new Phrase("Date", fontFooterDetails));
            SubDate.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell CompGen = new PdfPCell(new Phrase("This is a computer generated document", fontFooterDetails));
            CompGen.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;
            CompGen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;



            FooterTable.AddCell(NCEDate);
            FooterTable.AddCell(NCESignaA);
            FooterTable.AddCell(SubDate);
            FooterTable.AddCell(CompGen);


            PdfPCell NCEDateData = new PdfPCell(new Phrase(SignedDate, fontFooterDetails));//Date
            NCEDateData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            NCEDateData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            NCEDateData.FixedHeight = 15f;

            PdfPCell NCESignaAData = new PdfPCell(new Phrase("", fontFooterDetails));
            NCESignaAData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            NCESignaAData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            NCESignaAData.FixedHeight = 15f;

            PdfPCell SubDateData = new PdfPCell(new Phrase(CertificateHead.RequestDate1.ToString("dd/MM/yyyy"), fontFooterDetails));//CertificateHead.RequestDate1.ToString("dd/MM/yyyy")
            SubDateData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubDateData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubDateData.FixedHeight = 15f;

            PdfPCell CompGenData = new PdfPCell(new Phrase("No signature required", fontSmallBold));
            CompGenData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            CompGenData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            CompGenData.FixedHeight = 15f;



            FooterTable.AddCell(NCEDateData);
            FooterTable.AddCell(NCESignaAData);
            FooterTable.AddCell(SubDateData);
            FooterTable.AddCell(CompGenData);



            //------------------------------------------------------------

            document.Add(table);
            document.Add(FooterTable);
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
            SummaryHead.Colspan = 3;
            SummaryHead.FixedHeight = 20f;
            SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            CertItemTbl.AddCell(SummaryHead);

            //PdfPCell HSHead = new PdfPCell(new Phrase("14. HS Code", fontTableHeader));
            //HSHead.MinimumHeight = 20f;
            //HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //CertItemTbl.AddCell(HSHead);

            PdfPCell QttHead = new PdfPCell(new Phrase("14. Qty & Units", fontTableHeader));
            QttHead.FixedHeight = 20f;
            QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            CertItemTbl.AddCell(QttHead);


            document.Add(CertItemTbl);
            //-----------------------------------------------------------------------------

        }

        protected override void printlistRow()
        {
            //PdfPTable table = new PdfPTable(3);

            PdfPTable CertificateItems = new PdfPTable(7);
            CertificateItems.HorizontalAlignment = 1;
            CertificateItems.WidthPercentage = 100;
            //CertificateItems.SpacingBefore = 4;
            CertificateItems.SpacingAfter = 2;
            CertificateItems.DefaultCell.Border = Rectangle.BOX;
            CertificateItems.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

            for (int i = 0; i < CertReqDetails.Count; i++)//CertReqDetails.Count
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
                SummaryHead.Colspan = 3;
                SummaryHead.FixedHeight = 220f;
                SummaryHead.Border = Rectangle.LEFT_BORDER;
                SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                CertificateItems.AddCell(SummaryHead);

                //PdfPCell HSHead = new PdfPCell(new Phrase("", fontFooterDetails)); //CertReqDetails[i].HSCode1
                //HSHead.MinimumHeight = 20f;
                //HSHead.Border = Rectangle.LEFT_BORDER;
                //HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //CertificateItems.AddCell(HSHead);

                PdfPCell QttHead = new PdfPCell(new Phrase(CertReqDetails[i].Quantity1, fontFooterDetails));//CertReqDetails[i].Quantity1
                QttHead.FixedHeight = 220f;
                QttHead.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                QttHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
                CertificateItems.AddCell(QttHead);
            }

            //if (CertReqDetails.Count < 11)
            //{
            //    for (int i = 0; i < 11 - CertReqDetails.Count; i++)
            //    {
            //        PdfPCell GoodItemHead = new PdfPCell(new Phrase("", fontFooterDetails));//CertReqDetails[i].GoodItem1
            //        GoodItemHead.FixedHeight = 20f;
            //        GoodItemHead.Border = Rectangle.LEFT_BORDER;
            //        GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        CertificateItems.AddCell(GoodItemHead);

            //        PdfPCell ShipMarkHead = new PdfPCell(new Phrase("", fontFooterDetails));//CertReqDetails[i].ShippingMark1
            //        ShipMarkHead.FixedHeight = 20f;
            //        ShipMarkHead.Border = Rectangle.LEFT_BORDER;
            //        ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        CertificateItems.AddCell(ShipMarkHead);

            //        PdfPCell PackageHead = new PdfPCell(new Phrase("", fontFooterDetails));///CertReqDetails[i].PackageType1
            //        PackageHead.FixedHeight = 20f;
            //        PackageHead.Border = Rectangle.LEFT_BORDER;
            //        PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        CertificateItems.AddCell(PackageHead);

            //        PdfPCell SummaryHead = new PdfPCell(new Phrase("", fontFooterDetails));//CertReqDetails[i].SummaryDesc1
            //        SummaryHead.Colspan = 3;
            //        SummaryHead.FixedHeight = 20f;
            //        SummaryHead.Border = Rectangle.LEFT_BORDER;
            //        SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        CertificateItems.AddCell(SummaryHead);


            //        //PdfPCell HSHead = new PdfPCell(new Phrase("", fontFooterDetails)); //CertReqDetails[i].HSCode1
            //        //HSHead.MinimumHeight = 20f;
            //        //HSHead.Border = Rectangle.LEFT_BORDER;
            //        //HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        //CertificateItems.AddCell(HSHead);

            //        PdfPCell QttHead = new PdfPCell(new Phrase("", fontFooterDetails));//CertReqDetails[i].Quantity1
            //        QttHead.FixedHeight = 20f;
            //        QttHead.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //        QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //        CertificateItems.AddCell(QttHead);
            //    }
            //}
            document.Add(CertificateItems);
            //doc.Add(table);
        }

    }
}