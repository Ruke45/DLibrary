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

namespace DCISDBManager.PDFCreator
{
    public class CertificateCreate
    {

        Font fontBoxHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 1, BaseColor.BLACK);//Calibri
        Font fontBoxDetail = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.BLACK);
        Font fontBracketText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 6, 0, BaseColor.BLACK);
        Font fontItemDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.BLACK);
        Font fontDocumentHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, 1, BaseColor.BLACK);
        Font fontAddress = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 1, BaseColor.BLACK);
        Font fontSignatureInfo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 0, BaseColor.BLACK);
        Font fontFreeItemDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.WHITE);

        Font fontTableHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, 1, BaseColor.BLACK);
        Font fontSmallBold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
        Font fontFooterDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, 0, BaseColor.BLACK);

        Document document = null;
        PdfWriter pdfwriter = null;
        string LOGOimgPath = string.Empty;
        string CertificateSavePath = string.Empty;
        string AuthoirzedOfficer = string.Empty;
        string AuthTelephone = string.Empty;
        string CertificateId = string.Empty;
       // string Comment = string.Empty;

        CertificateRequestHeader CertificateHead = null;
        List<CertificateRequestDetail> CertReqDetails = null;


        //public CertificateCreate(string CertificateID,CertificateRequestHeader CRH,List<CertificateRequestDetail> CRDlist,string LOGOpath,string DocPath,string AuthOffer,string comment,string Telephone)
        //{
        //    this.LOGOimgPath = LOGOpath;
        //    this.CertificateHead = CRH;
        //    this.CertReqDetails = CRDlist;
        //    this.CertificateSavePath = DocPath;
        //    this.AuthoirzedOfficer = AuthOffer;
        //    this.AuthTelephone = Telephone;
        //    this.CertificateId = CertificateID;
        //    this.Comment = comment;

        //}

        public CertificateCreate(string CertificateID, CertificateRequestHeader CRH, List<CertificateRequestDetail> CRDlist, string LOGOpath, string DocPath, string AuthOffer, string Telephone)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertReqDetails = CRDlist;
            this.CertificateSavePath = DocPath;
            this.AuthoirzedOfficer = AuthOffer;
            this.AuthTelephone = Telephone;
            this.CertificateId = CertificateID;

        }
        private bool drawCertificateOfOrginHeader()
        {
            try
            {
                //Start of Top Table

                PdfPTable topTable = new PdfPTable(2);
                topTable.HorizontalAlignment = 1;
                topTable.WidthPercentage = 100;
                topTable.SpacingBefore = 0;
                topTable.SpacingAfter = 0;
                topTable.DefaultCell.Border = Rectangle.BOX;
                topTable.SetWidths(new int[] { 1, 1 });

                // Top Table Left  Columns //
                PdfPTable topLeftUpTable = new PdfPTable(1);

                // Consignor Name Start 
                PdfPCell ConsignorHederCell = new PdfPCell(new Phrase("1. Consignor / Exporter", fontBoxHeader));
                ConsignorHederCell.Border = Rectangle.NO_BORDER;
                ConsignorHederCell.FixedHeight = 15f;
                topLeftUpTable.AddCell(ConsignorHederCell);

                Paragraph consignorParagraph = new Paragraph();
                Phrase consignorNamePhrase = new Phrase(this.CertificateHead.Consignor1 + "\n", fontBoxDetail); //-------
                consignorParagraph.Add(consignorNamePhrase);

                PdfPCell ConsignorDetailCell = new PdfPCell(consignorParagraph);
                ConsignorDetailCell.Border = Rectangle.NO_BORDER;
                ConsignorDetailCell.FixedHeight = 85f;//65f
                topLeftUpTable.AddCell(ConsignorDetailCell);

                // Consignor Name Ends 

                // Consignee Name Start 
                PdfPCell ConsigneeHeaderCell = new PdfPCell(new Phrase("3. Consignee", fontBoxHeader));
                ConsigneeHeaderCell.Border = Rectangle.NO_BORDER;
                ConsigneeHeaderCell.Border = Rectangle.TOP_BORDER;
                ConsigneeHeaderCell.FixedHeight = 15f;
                topLeftUpTable.AddCell(ConsigneeHeaderCell);


                Paragraph consigneeParagraph = new Paragraph();
                Phrase consigneeNamePhrase = new Phrase(this.CertificateHead.Consignee1 + "\n", fontBoxDetail);//--------
                consigneeParagraph.Add(consigneeNamePhrase);

                PdfPCell ConsigneeDetailCell = new PdfPCell(consigneeParagraph);
                ConsigneeDetailCell.Border = Rectangle.NO_BORDER;
                ConsigneeDetailCell.FixedHeight = 100f;
                topLeftUpTable.AddCell(ConsigneeDetailCell);

                // COnsignee Name Ends 


                // Consignee Name Start 
                PdfPCell InoviceNoNDateCell = new PdfPCell(new Phrase("4. Invoice No & Date", fontBoxHeader));
                InoviceNoNDateCell.Border = Rectangle.NO_BORDER;
                InoviceNoNDateCell.Border = Rectangle.TOP_BORDER;
                InoviceNoNDateCell.FixedHeight = 15f;
                topLeftUpTable.AddCell(InoviceNoNDateCell);


                Paragraph InoviceNDateParagraph = new Paragraph();
                Phrase InvoicNDatePhrase = new Phrase(this.CertificateHead.InvoiceNo1 + "     " + this.CertificateHead.InvoiceDate1.ToString("dd/MM/yyyy") + "\n", fontBoxDetail);//-------
                InoviceNDateParagraph.Add(InvoicNDatePhrase);

                PdfPCell InoviceNDateDetailCell = new PdfPCell(InoviceNDateParagraph);
                InoviceNDateDetailCell.Border = Rectangle.NO_BORDER;
                InoviceNDateDetailCell.FixedHeight = 25f;
                topLeftUpTable.AddCell(InoviceNDateDetailCell);

                // COnsignee Name Ends 

                PdfPCell topLeftTableBaseCell = new PdfPCell(new PdfPTable(topLeftUpTable));
                topTable.AddCell(topLeftTableBaseCell);

                PdfPTable topRightUpTable = new PdfPTable(1);
                PdfPCell refNoTopLeftCell = new PdfPCell(new Phrase("2. Ref.No : " + this.CertificateId, fontBoxHeader)); // Haha
                refNoTopLeftCell.Border = Rectangle.NO_BORDER;
                refNoTopLeftCell.FixedHeight = 15f;
                topRightUpTable.AddCell(refNoTopLeftCell);



                //PdfPCell CEOLableTopRightCell = new PdfPCell(new Phrase("Certificate of Origin", fontDocumentHeader));
                //CEOLableTopRightCell.FixedHeight = 20f;
                //CEOLableTopRightCell.HorizontalAlignment = 1;
                //CEOLableTopRightCell.VerticalAlignment = 1;
                //CEOLableTopRightCell.Border = Rectangle.NO_BORDER;
                //topRightUpTable.AddCell(CEOLableTopRightCell);

                iTextSharp.text.Image logoImg = iTextSharp.text.Image.GetInstance(LOGOimgPath);

                PdfPCell logoCell = new PdfPCell(logoImg, false);
                logoCell.HorizontalAlignment = 1;
                logoCell.VerticalAlignment = 1;
                logoCell.Border = Rectangle.NO_BORDER;
                logoCell.FixedHeight = 200f;//40f
                topRightUpTable.AddCell(logoCell);

                //StringBuilder chamberAddressStrbuilder = new StringBuilder();
                //chamberAddressStrbuilder.Append(Environment.NewLine);
                ////chamberAddressStrbuilder.Append(Environment.NewLine);
                //chamberAddressStrbuilder.Append("50, Navam Mawatha, Colombo 02, Sri Lanka");
                ////chamberAddressStrbuilder.Append(Environment.NewLine);
                //chamberAddressStrbuilder.Append(Environment.NewLine);
                //chamberAddressStrbuilder.Append("Tel : (0094) 11 - 2421745-6, 2433148, 5588800");
                ////chamberAddressStrbuilder.Append(Environment.NewLine); 
                //chamberAddressStrbuilder.Append(Environment.NewLine);
                //chamberAddressStrbuilder.Append("Fax : (0094) 11 - 2437477, 2449352, 2381012 Email : eco@chamber.lk");
                //chamberAddressStrbuilder.Append(Environment.NewLine);
                //chamberAddressStrbuilder.Append(Environment.NewLine);

                //Paragraph chamberAddressPara = new Paragraph(chamberAddressStrbuilder.ToString(), fontAddress);

                //PdfPCell CEOLableAddressTopLeftCell = new PdfPCell(new Paragraph(chamberAddressPara));
                //CEOLableAddressTopLeftCell.HorizontalAlignment = 1;
                //CEOLableAddressTopLeftCell.VerticalAlignment = 2;
                //CEOLableAddressTopLeftCell.Border = Rectangle.BOTTOM_BORDER;
                //CEOLableAddressTopLeftCell.FixedHeight = 55f;//35f
                //topRightUpTable.AddCell(CEOLableAddressTopLeftCell);


                PdfPCell countryOfOriginHeaderCell = new PdfPCell(new Phrase("7. Country of Origin", fontBoxHeader));
                countryOfOriginHeaderCell.Border = Rectangle.NO_BORDER;
                countryOfOriginHeaderCell.FixedHeight = 15f;
                topRightUpTable.AddCell(countryOfOriginHeaderCell);

                PdfPTable cntryOfOriginDetailTable = new PdfPTable(3);
                cntryOfOriginDetailTable.SetWidths(new int[] { 1, 6, 1 });

                PdfPCell cntryOfOriginLeftFreeCell = new PdfPCell(new Phrase("  "));
                cntryOfOriginLeftFreeCell.FixedHeight = 15f;
                cntryOfOriginLeftFreeCell.Border = Rectangle.NO_BORDER;
                cntryOfOriginDetailTable.AddCell(cntryOfOriginLeftFreeCell);

                PdfPCell cntryOfOriginDetailCell = new PdfPCell(new Phrase(this.CertificateHead.CountryName1, fontBoxDetail));//---------
                cntryOfOriginDetailCell.FixedHeight = 15f;
                //cntryOfOriginDetailCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cntryOfOriginDetailCell.Border = Rectangle.NO_BORDER;
                cntryOfOriginDetailTable.AddCell(cntryOfOriginDetailCell);

                PdfPCell cntryOfOriginRightFreeCell = new PdfPCell(new Phrase(" "));
                cntryOfOriginRightFreeCell.FixedHeight = 15f;
                cntryOfOriginRightFreeCell.Border = Rectangle.NO_BORDER;

                cntryOfOriginDetailTable.AddCell(cntryOfOriginRightFreeCell);

                PdfPCell cntryOfOriginDetailbaseCell = new PdfPCell(cntryOfOriginDetailTable);
                cntryOfOriginDetailbaseCell.Border = Rectangle.NO_BORDER;
                topRightUpTable.AddCell(cntryOfOriginDetailbaseCell);

                PdfPCell topLeftFreeCell = new PdfPCell(new Phrase(" ", fontBoxHeader));
                topLeftFreeCell.Border = Rectangle.NO_BORDER;
                topLeftFreeCell.FixedHeight = 1f;
                topRightUpTable.AddCell(topLeftFreeCell);

                topTable.AddCell(topRightUpTable);


                //End of Top Right Up Table   

                PdfPTable PortingDetailTable1 = new PdfPTable(4);//-----------

                PortingDetailTable1.HorizontalAlignment = 0;
                PortingDetailTable1.WidthPercentage = 100;
                //PortingDetailTable1.SpacingBefore = 2f;
                //PortingDetailTable1.SpacingAfter = 1f;
                PortingDetailTable1.DefaultCell.Border = Rectangle.NO_BORDER;

                string PortFloading = "5. Port Of Loading";
                string Vessel = "6. Vessel ";

                PdfPCell PortOFLoadCellLbl = new PdfPCell(new Phrase(PortFloading, fontBoxHeader));
                PortOFLoadCellLbl.MinimumHeight = 20f;
                PortOFLoadCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortOFLoadCellLbl);

                PdfPCell PortOFLoadCellData = new PdfPCell(new Phrase(this.CertificateHead.LoadingPort1, fontBoxDetail));//----------
                PortOFLoadCellLbl.MinimumHeight = 20f;
                PortOFLoadCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortOFLoadCellData);

                PdfPCell VessleCellLbl = new PdfPCell(new Phrase(Vessel, fontBoxHeader));
                VessleCellLbl.MinimumHeight = 20f;
                VessleCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(VessleCellLbl);

                PdfPCell VessleCellData = new PdfPCell(new Phrase(this.CertificateHead.Vessel1, fontBoxDetail));//------
                VessleCellData.MinimumHeight = 20f;
                VessleCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(VessleCellData);

                PdfPTable PortingDetailTable = new PdfPTable(4);//-----------

                PortingDetailTable.HorizontalAlignment = 1;
                PortingDetailTable.WidthPercentage = 100;
                PortingDetailTable.DefaultCell.Border = Rectangle.NO_BORDER;


                string PortOfDischarg = "8. Port Of Discharge";
                string PlaceFDelivery = "9. Place OF Delivery";

                PdfPCell PortDischgCellLbl = new PdfPCell(new Phrase(PortOfDischarg, fontBoxHeader));
                PortDischgCellLbl.MinimumHeight = 20f;
                PortDischgCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable.AddCell(PortDischgCellLbl);

                PdfPCell PortDischgCellData = new PdfPCell(new Phrase(this.CertificateHead.PortOfDischarge1, fontBoxDetail));
                PortDischgCellData.MinimumHeight = 20f;
                PortDischgCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable.AddCell(PortDischgCellData);

                PdfPCell PlaceDeliverCell = new PdfPCell(new Phrase(PlaceFDelivery, fontBoxHeader));
                PlaceDeliverCell.MinimumHeight = 20f;
                PlaceDeliverCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable.AddCell(PlaceDeliverCell);


                PdfPCell PlaceDeliverCellData = new PdfPCell(new Phrase(this.CertificateHead.PlaceOfDelivery1, fontBoxDetail));
                PlaceDeliverCellData.MinimumHeight = 20f;
                PlaceDeliverCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable.AddCell(PlaceDeliverCellData);

                document.Add(topTable);
                document.Add(PortingDetailTable1);
                document.Add(PortingDetailTable);


                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        private void drawBottom(bool HSCOD, string Comment)
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
            bottom.FixedHeight = 110f;
            table.AddCell(bottom);

            PdfPTable nested = new PdfPTable(1);

            if (HSCOD)
            {
                PdfPCell totInvoL = new PdfPCell(new Phrase("16. Total Invoice Value", fontBoxHeader));
                totInvoL.FixedHeight = 20f;
                totInvoL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                totInvoL.Border = Rectangle.NO_BORDER;
                nested.AddCell(totInvoL);
            }
            else
            {
                PdfPCell totInvoL = new PdfPCell(new Phrase("15. Total Invoice Value", fontBoxHeader));
                totInvoL.FixedHeight = 20f;
                totInvoL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                totInvoL.Border = Rectangle.NO_BORDER;
                nested.AddCell(totInvoL);
            }

            PdfPCell totInvoD = new PdfPCell(new Phrase(this.CertificateHead.TotalInvoiceValue1.ToString(), fontBoxDetail));//---------

            totInvoD.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totInvoD.Border = Rectangle.NO_BORDER;
            totInvoD.FixedHeight = 20f;
            nested.AddCell(totInvoD);

            if (HSCOD)
            {
                PdfPCell totQuantL = new PdfPCell(new Phrase("17. Total Quantity", fontBoxHeader));
                totQuantL.FixedHeight = 20f;
                totQuantL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                totQuantL.Border = Rectangle.NO_BORDER;
                nested.AddCell(totQuantL);
            }
            else
            {
                PdfPCell totQuantL = new PdfPCell(new Phrase("16. Total Quantity", fontBoxHeader));
                totQuantL.FixedHeight = 20f;
                totQuantL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                totQuantL.Border = Rectangle.NO_BORDER;
                nested.AddCell(totQuantL);
            }

            PdfPCell totQuntyD = new PdfPCell(new Phrase(this.CertificateHead.TotalQuantity1, fontBoxDetail));

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


            PdfPCell NameAO = new PdfPCell(new Phrase("Name of Authorized officer", fontFooterDetails));
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

            PdfPCell SubNameData = new PdfPCell(new Phrase(CertificateHead.CustomerName1, fontFooterDetails));
            SubNameData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubNameData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubNameData.FixedHeight = 20f;

            PdfPCell SubContactData = new PdfPCell(new Phrase(CertificateHead.Customer_Telephone, fontFooterDetails));
            SubContactData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubContactData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubContactData.FixedHeight = 20f;



            FooterTable.AddCell(NameAOData);
            FooterTable.AddCell(ContactData);
            FooterTable.AddCell(SubNameData);
            FooterTable.AddCell(SubContactData);



            PdfPCell NCEDate = new PdfPCell(new Phrase("Date", fontFooterDetails));
            NCEDate.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;

            PdfPCell NCESignaA = new PdfPCell(new Phrase("Signature (Authorized Officer)", fontFooterDetails));
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


            PdfPCell NCEDateData = new PdfPCell(new Phrase(DateTime.Now.ToString(), fontFooterDetails));
            NCEDateData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            NCEDateData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            NCEDateData.FixedHeight = 40f;

            PdfPCell NCESignaAData = new PdfPCell(new Phrase("-", fontFooterDetails));
            NCESignaAData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            NCESignaAData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            NCESignaAData.FixedHeight = 40f;

            PdfPCell SubDateData = new PdfPCell(new Phrase(CertificateHead.RequestDate1.ToString("dd/MM/yyyy"), fontFooterDetails));
            SubDateData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            SubDateData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            SubDateData.FixedHeight = 40f;

            PdfPCell CompGenData = new PdfPCell(new Phrase("No signature required", fontSmallBold));
            CompGenData.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            CompGenData.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            CompGenData.FixedHeight = 40f;



            FooterTable.AddCell(NCEDateData);
            FooterTable.AddCell(NCESignaAData);
            FooterTable.AddCell(SubDateData);
            FooterTable.AddCell(CompGenData);



            //------------------------------------------------------------

            document.Add(table);
            document.Add(FooterTable);
        }

        private void drwRectangle()
        {
            PdfPTable RectangleT = new PdfPTable(7);
            RectangleT.HorizontalAlignment = 1;
            RectangleT.WidthPercentage = 100;
            RectangleT.SpacingBefore = 1;
            //CertItemTbl.SpacingAfter = 230;
            RectangleT.DefaultCell.Border = Rectangle.BOX;
            RectangleT.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

            PdfPCell GoodItemHead = new PdfPCell(new Phrase("", fontBoxDetail));//Rectangle
            GoodItemHead.Colspan = 7;
            GoodItemHead.MinimumHeight = 60f;
            GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_JUSTIFIED;
            RectangleT.AddCell(GoodItemHead);

            document.Add(RectangleT);
        }

        private void printListHead(bool HSCOD)
        {
            //----------------------------------------------------------------------------

            PdfPTable CertItemTbl = new PdfPTable(7);
            CertItemTbl.HorizontalAlignment = 1;
            CertItemTbl.WidthPercentage = 100;
            CertItemTbl.SpacingBefore = 4;
            //CertItemTbl.SpacingAfter = 230;
            CertItemTbl.DefaultCell.Border = Rectangle.BOX;
            CertItemTbl.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

            if (HSCOD)
            {
                PdfPCell GoodItemHead = new PdfPCell(new Phrase("10. Good/Item", fontTableHeader));
                GoodItemHead.MinimumHeight = 20f;
                GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(GoodItemHead);

                PdfPCell ShipMarkHead = new PdfPCell(new Phrase("11. Shipping Mark", fontTableHeader));
                ShipMarkHead.MinimumHeight = 20f;
                ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(ShipMarkHead);

                PdfPCell PackageHead = new PdfPCell(new Phrase("12. Package Typ/Qty", fontTableHeader));
                PackageHead.MinimumHeight = 20f;
                PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(PackageHead);

                PdfPCell SummaryHead = new PdfPCell(new Phrase("13. Summary Description", fontTableHeader));
                SummaryHead.Colspan = 2;
                SummaryHead.MinimumHeight = 20f;
                SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(SummaryHead);

                PdfPCell HSHead = new PdfPCell(new Phrase("14. HS Code", fontTableHeader));
                HSHead.MinimumHeight = 20f;
                HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(HSHead);

                PdfPCell QttHead = new PdfPCell(new Phrase("15. Quty & Units", fontTableHeader));
                QttHead.MinimumHeight = 20f;
                QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(QttHead);
            }
            else
            {
                PdfPCell GoodItemHead = new PdfPCell(new Phrase("10. Good/Item", fontTableHeader));
                GoodItemHead.MinimumHeight = 20f;
                GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(GoodItemHead);

                PdfPCell ShipMarkHead = new PdfPCell(new Phrase("11. Shipping Mark", fontTableHeader));
                ShipMarkHead.MinimumHeight = 20f;
                ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(ShipMarkHead);

                PdfPCell PackageHead = new PdfPCell(new Phrase("12. Package Typ/Qty", fontTableHeader));
                PackageHead.MinimumHeight = 20f;
                PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(PackageHead);

                PdfPCell SummaryHead = new PdfPCell(new Phrase("13. Summary Description", fontTableHeader));
                SummaryHead.Colspan = 3;
                SummaryHead.MinimumHeight = 20f;
                SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(SummaryHead);

                PdfPCell QttHead = new PdfPCell(new Phrase("14. Quty & Units", fontTableHeader));
                QttHead.MinimumHeight = 20f;
                QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                CertItemTbl.AddCell(QttHead);
            }


            document.Add(CertItemTbl);
            //-----------------------------------------------------------------------------

        }

        private void printlistRow(bool HSCOD , bool RectangleBox)
        {
            //PdfPTable table = new PdfPTable(3);

            PdfPTable CertificateItems = new PdfPTable(7);
            CertificateItems.HorizontalAlignment = 1;
            CertificateItems.WidthPercentage = 100;
            //CertificateItems.SpacingBefore = 4;
            CertificateItems.SpacingAfter = 2;
            CertificateItems.DefaultCell.Border = Rectangle.BOX;
            CertificateItems.SetWidths(new int[] { 1, 1, 1, 1, 1, 1, 1 });

            if (HSCOD)
            {
                for (int i = 0; i < CertReqDetails.Count; i++)
                {
                    PdfPCell GoodItemHead = new PdfPCell(new Phrase(CertReqDetails[i].GoodItem1, fontFooterDetails));
                    GoodItemHead.MinimumHeight = 20f;
                    GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(GoodItemHead);


                    PdfPCell ShipMarkHead = new PdfPCell(new Phrase(CertReqDetails[i].ShippingMark1, fontFooterDetails));
                    ShipMarkHead.MinimumHeight = 20f;
                    ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(ShipMarkHead);

                    PdfPCell PackageHead = new PdfPCell(new Phrase(CertReqDetails[i].PackageType1, fontFooterDetails));///Package
                    PackageHead.MinimumHeight = 20f;
                    PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(PackageHead);

                    PdfPCell SummaryHead = new PdfPCell(new Phrase(CertReqDetails[i].SummaryDesc1, fontFooterDetails));
                    SummaryHead.Colspan = 2;
                    SummaryHead.MinimumHeight = 20f;
                    SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(SummaryHead);

                    PdfPCell HSHead = new PdfPCell(new Phrase(CertReqDetails[i].HSCode1, fontFooterDetails));
                    HSHead.MinimumHeight = 20f;
                    HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(HSHead);

                    PdfPCell QttHead = new PdfPCell(new Phrase(CertReqDetails[i].Quantity1, fontFooterDetails));
                    QttHead.MinimumHeight = 20f;
                    QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(QttHead);
                }
            }
            else
            {
                for (int i = 0; i < CertReqDetails.Count; i++)
                {
                    PdfPCell GoodItemHead = new PdfPCell(new Phrase(CertReqDetails[i].GoodItem1, fontFooterDetails));
                    GoodItemHead.MinimumHeight = 20f;
                    GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(GoodItemHead);


                    PdfPCell ShipMarkHead = new PdfPCell(new Phrase(CertReqDetails[i].ShippingMark1, fontFooterDetails));
                    ShipMarkHead.MinimumHeight = 20f;
                    ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(ShipMarkHead);

                    PdfPCell PackageHead = new PdfPCell(new Phrase(CertReqDetails[i].PackageType1, fontFooterDetails));
                    PackageHead.MinimumHeight = 20f;
                    PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(PackageHead);

                    PdfPCell SummaryHead = new PdfPCell(new Phrase(CertReqDetails[i].SummaryDesc1, fontFooterDetails));
                    SummaryHead.Colspan = 3;
                    SummaryHead.MinimumHeight = 20f;
                    SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(SummaryHead);

                    PdfPCell QttHead = new PdfPCell(new Phrase(CertReqDetails[i].Quantity1, fontFooterDetails));
                    QttHead.MinimumHeight = 20f;
                    QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    CertificateItems.AddCell(QttHead);
                }

            }

            if (RectangleBox && HSCOD)
            {
                if (CertReqDetails.Count < 9)
                {
                    for (int i = 0; i < 9 - CertReqDetails.Count; i++)
                    {
                        PdfPCell GoodItemHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        GoodItemHead.MinimumHeight = 20f;
                        GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(GoodItemHead);


                        PdfPCell ShipMarkHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        ShipMarkHead.MinimumHeight = 20f;
                        ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(ShipMarkHead);

                        PdfPCell PackageHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        PackageHead.MinimumHeight = 20f;
                        PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(PackageHead);

                        PdfPCell SummaryHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        SummaryHead.Colspan = 2;
                        SummaryHead.MinimumHeight = 20f;
                        SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(SummaryHead);

                        PdfPCell HSHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        HSHead.MinimumHeight = 20f;
                        HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(HSHead);

                        PdfPCell QttHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                        QttHead.MinimumHeight = 20f;
                        QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        CertificateItems.AddCell(QttHead);
                    }
                }
            }
            else
            {
                if (CertReqDetails.Count < 12)
                {
                    if (HSCOD)
                    {
                        for (int i = 0; i < 12 - CertReqDetails.Count; i++)
                        {
                            PdfPCell GoodItemHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            GoodItemHead.MinimumHeight = 20f;
                            GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(GoodItemHead);


                            PdfPCell ShipMarkHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            ShipMarkHead.MinimumHeight = 20f;
                            ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(ShipMarkHead);

                            PdfPCell PackageHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            PackageHead.MinimumHeight = 20f;
                            PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(PackageHead);

                            PdfPCell SummaryHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            SummaryHead.Colspan = 2;
                            SummaryHead.MinimumHeight = 20f;
                            SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(SummaryHead);

                            PdfPCell HSHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            HSHead.MinimumHeight = 20f;
                            HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(HSHead);

                            PdfPCell QttHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            QttHead.MinimumHeight = 20f;
                            QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(QttHead);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 12 - CertReqDetails.Count; i++)
                        {
                            PdfPCell GoodItemHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            GoodItemHead.MinimumHeight = 20f;
                            GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(GoodItemHead);


                            PdfPCell ShipMarkHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            ShipMarkHead.MinimumHeight = 20f;
                            ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(ShipMarkHead);

                            PdfPCell PackageHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            PackageHead.MinimumHeight = 20f;
                            PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(PackageHead);

                            PdfPCell SummaryHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            SummaryHead.Colspan = 3;
                            SummaryHead.MinimumHeight = 20f;
                            SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(SummaryHead);

                            PdfPCell QttHead = new PdfPCell(new Phrase("--", fontFooterDetails));
                            QttHead.MinimumHeight = 20f;
                            QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            CertificateItems.AddCell(QttHead);
                        }
                    }
                }
            }
            document.Add(CertificateItems);
            //doc.Add(table);
        }

        public bool CreateCertificate(bool HSCOD,bool RectangelT,string Comment)
        {
            try
            {
                document = new Document(PageSize.A4, 25f, 25f, 25f, 15f);//Lest,right,top,bottom

                pdfwriter = PdfWriter.GetInstance(document, new FileStream(CertificateSavePath, FileMode.Create));

                document.Open();
                drawCertificateOfOrginHeader();
                printListHead(HSCOD);
                printlistRow(HSCOD, RectangelT);
                if (RectangelT)
                {
                    drwRectangle();
                }
                drawBottom(HSCOD,Comment);
                document.Close();
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }
        }
    }
}