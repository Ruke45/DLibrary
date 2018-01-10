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
AUTHOR.           Tharaka Madusanka
COMPANY.          VOTRE IT (Pvt.) Ltd.
DATE-WRITTEN.     2016-10-08
Version.          1.1.0
*******************************************************************************/
namespace DCISDBManager.PDFCreator
{
    public class RowWithHSTemplate
    {
        Font fontBoxHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 1, BaseColor.BLACK);//Calibri
        Font fontBoxDetail = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.BLACK);

        Font fontBoxHeaderFontSmal = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 1, BaseColor.BLACK);//Calibri
        Font fontBoxDetailFontSmal = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 0, BaseColor.BLACK);


        Font fontBracketText = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 6, 0, BaseColor.BLACK);
        Font fontItemDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.BLACK);
        Font fontDocumentHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, 1, BaseColor.BLACK);
        Font fontAddress = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 1, BaseColor.BLACK);
        Font fontSignatureInfo = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 0, BaseColor.BLACK);
        Font fontFreeItemDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, 0, BaseColor.WHITE);

        Font fontTableHeader = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, 1, BaseColor.BLACK);
        Font fontSmallBold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK);
        Font fontFooterDetails = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 8, 0, BaseColor.BLACK);

        Document document = null;
        PdfWriter pdfwriter = null;
        string LOGOimgPath = string.Empty;
        string CertificateSavePath = string.Empty;
        string AuthoirzedOfficer = "";
        string AuthTelephone = "";
        string CertificateId = "";
        string SignedDate = "";
       // string Comment = string.Empty;

        CertificateRequestHeader CertificateHead = null;
        //List<CertificateRequestDetail> CertReqDetails = null;
        CertificateRequestDetail CRD = null;


        public RowWithHSTemplate(CertificateRequestHeader CRH, CertificateRequestDetail CRDl, string LOGOpath, string DocPath, string AuthOffer,string SignDate)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CRD = CRDl;
            this.CertificateSavePath = DocPath;
            this.CertificateId = CRH.CertificateId1;
            this.AuthoirzedOfficer = AuthOffer;
            this.SignedDate = SignDate;
        }

        public RowWithHSTemplate(CertificateRequestHeader CRH, CertificateRequestDetail CRDlist, string LOGOpath, string DocPath)
        {
            this.LOGOimgPath = LOGOpath;
            this.CertificateHead = CRH;
            this.CertificateSavePath = DocPath;
            this.CertificateId = CRH.CertificateId1;
            this.CRD = CRDlist;
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
                Phrase consignorNamePhrase = new Phrase(this.CertificateHead.Consignor1.Replace("<br />", "\r\n") + "\n", fontBoxDetail); //-------this.CertificateHead.Consignor1
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
                Phrase consigneeNamePhrase = new Phrase(this.CertificateHead.Consignee1.Replace("<br />", "\r\n") + "\n", fontBoxDetail);//--------this.CertificateHead.Consignee1
                consigneeParagraph.Add(consigneeNamePhrase);

                PdfPCell ConsigneeDetailCell = new PdfPCell(consigneeParagraph);
                ConsigneeDetailCell.Border = Rectangle.NO_BORDER;
                ConsigneeDetailCell.FixedHeight = 100f;
                topLeftUpTable.AddCell(ConsigneeDetailCell);

                // COnsignee Name Ends 


                // Consignee Name Start 
                PdfPCell InoviceNoNDateCell = new PdfPCell(new Phrase("4. Invoice No : " + this.CertificateHead.InvoiceNo1, fontItemDetails));
                InoviceNoNDateCell.Border = Rectangle.TOP_BORDER;
                InoviceNoNDateCell.FixedHeight = 15f;
                topLeftUpTable.AddCell(InoviceNoNDateCell);

                PdfPCell InoviceNoNDateCell1 = new PdfPCell(new Phrase("& Invoice Date : " + this.CertificateHead.InvoiceDate1.ToString("dd/MM/yyyy"), fontItemDetails));
                InoviceNoNDateCell1.Border = Rectangle.NO_BORDER;
                InoviceNoNDateCell1.FixedHeight = 15f;
                topLeftUpTable.AddCell(InoviceNoNDateCell1);


                //Paragraph InoviceNDateParagraph = new Paragraph();
                //Phrase InvoicNDatePhrase = new Phrase("  " + "\n", fontBoxDetail);//-------this.CertificateHead.InvoiceNo1 this.CertificateHead.InvoiceDate1.ToString("dd/MM/yyyy")
                //InoviceNDateParagraph.Add(InvoicNDatePhrase);

                //PdfPCell InoviceNDateDetailCell = new PdfPCell(InoviceNDateParagraph);
                //InoviceNDateDetailCell.Border = Rectangle.NO_BORDER;
                //InoviceNDateDetailCell.FixedHeight = 25f;
                //topLeftUpTable.AddCell(InoviceNDateDetailCell);

                // COnsignee Name Ends 

                PdfPCell topLeftTableBaseCell = new PdfPCell(new PdfPTable(topLeftUpTable));
                topTable.AddCell(topLeftTableBaseCell);

                PdfPTable topRightUpTable = new PdfPTable(1);
                PdfPCell refNoTopLeftCell = new PdfPCell(new Phrase("2. Ref.No : " + this.CertificateId, fontBoxHeader)); // this.CertificateId
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

                PdfPCell cntryOfOriginDetailCell = new PdfPCell(new Phrase(this.CertificateHead.CountryName1, fontBoxDetail));//---------this.CertificateHead.CountryName1
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

                PdfPTable PortingDetailTable1 = new PdfPTable(8);//-----------

                PortingDetailTable1.HorizontalAlignment = 0;
                PortingDetailTable1.WidthPercentage = 100;
                //PortingDetailTable1.SpacingBefore = 2f;
                //PortingDetailTable1.SpacingAfter = 1f;
                PortingDetailTable1.DefaultCell.Border = Rectangle.NO_BORDER;
                PortingDetailTable1.SetWidths(new int[] { 1, 2, 1, 2, 1, 2, 1, 2 });

                string PortFloading = "5. Port Of Loading";
                string Vessel = "6. Vessel ";

                PdfPCell PortOFLoadCellLbl = new PdfPCell(new Phrase(PortFloading, fontBoxHeaderFontSmal));
                PortOFLoadCellLbl.FixedHeight = 20f;
                PortOFLoadCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortOFLoadCellLbl);

                PdfPCell PortOFLoadCellData = new PdfPCell(new Phrase(this.CertificateHead.LoadingPort1, fontBoxDetailFontSmal));//----------this.CertificateHead.LoadingPort1
                PortOFLoadCellLbl.FixedHeight = 20f;
                PortOFLoadCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortOFLoadCellData);

                PdfPCell VessleCellLbl = new PdfPCell(new Phrase(Vessel, fontBoxHeaderFontSmal));
                VessleCellLbl.FixedHeight = 20f;
                VessleCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(VessleCellLbl);

                PdfPCell VessleCellData = new PdfPCell(new Phrase(this.CertificateHead.Vessel1, fontBoxDetailFontSmal));//------this.CertificateHead.Vessel1
                VessleCellData.FixedHeight = 20f;
                VessleCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(VessleCellData);

                string PortOfDischarg = "8. Port of Discharge";
                string PlaceFDelivery = "9. Place of Delivery";

                PdfPCell PortDischgCellLbl = new PdfPCell(new Phrase(PortOfDischarg, fontBoxHeaderFontSmal));
                PortDischgCellLbl.FixedHeight = 20f;
                PortDischgCellLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortDischgCellLbl);

                PdfPCell PortDischgCellData = new PdfPCell(new Phrase(this.CertificateHead.PortOfDischarge1, fontBoxDetailFontSmal)); //this.CertificateHead.PortOfDischarge1
                PortDischgCellData.FixedHeight = 20f;
                PortDischgCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PortDischgCellData);

                PdfPCell PlaceofDeliveryLbl = new PdfPCell(new Phrase(PlaceFDelivery, fontBoxHeaderFontSmal));
                PlaceofDeliveryLbl.FixedHeight = 20f;
                PlaceofDeliveryLbl.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PlaceofDeliveryLbl);

                PdfPCell PlaceofDeliveryCellData = new PdfPCell(new Phrase(this.CertificateHead.PlaceOfDelivery1, fontBoxDetailFontSmal)); //this.CertificateHead.PortOfDischarge1
                PlaceofDeliveryCellData.FixedHeight = 20f;
                PlaceofDeliveryCellData.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                PortingDetailTable1.AddCell(PlaceofDeliveryCellData);

                document.Add(topTable);
                document.Add(PortingDetailTable1);



                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return false;
            }

        }

        private void drawBottom(string Comment)
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

            PdfPCell totInvoL = new PdfPCell(new Phrase("16. Total Invoice Value", fontBoxHeader));
            totInvoL.FixedHeight = 20f;
            totInvoL.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totInvoL.Border = Rectangle.NO_BORDER;
            nested.AddCell(totInvoL);

            PdfPCell totInvoD = new PdfPCell(new Phrase(this.CertificateHead.TotalInvoiceValue1.ToString(), fontBoxDetail));//---------this.CertificateHead.TotalInvoiceValue1.ToString()

            totInvoD.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            totInvoD.Border = Rectangle.NO_BORDER;
            totInvoD.FixedHeight = 20f;
            nested.AddCell(totInvoD);

            PdfPCell totQuantL = new PdfPCell(new Phrase("17. Total Quantity", fontBoxHeader));
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

        private void printListHead()
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

        private void printlistRow()
        {
            //PdfPTable table = new PdfPTable(3);

            PdfPTable CertificateItems = new PdfPTable(3);
            CertificateItems.HorizontalAlignment = 1;
            CertificateItems.WidthPercentage = 100;
            //CertificateItems.SpacingBefore = 4;
            CertificateItems.SpacingAfter = 2;
            CertificateItems.DefaultCell.Border = Rectangle.BOX;
            CertificateItems.SetWidths(new int[] { 5, 1, 1 });

            PdfPCell GoodItemHead = new PdfPCell(new Phrase(CRD.Good_Details.Replace("<br />", "\r\n"), fontItemDetails));
            GoodItemHead.FixedHeight = 230f;
            GoodItemHead.Border = Rectangle.LEFT_BORDER;
            GoodItemHead.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            CertificateItems.AddCell(GoodItemHead);

            PdfPCell SummaryHead = new PdfPCell(new Phrase(CRD.HSCode_Details.Replace("<br />", "\r\n"), fontItemDetails));
            SummaryHead.FixedHeight = 230f;
            SummaryHead.Border = Rectangle.LEFT_BORDER;
            SummaryHead.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            CertificateItems.AddCell(SummaryHead);

            PdfPCell ShipMarkHead = new PdfPCell(new Phrase(CRD.Quantity_Details.Replace("<br />", "\r\n"), fontItemDetails));
            ShipMarkHead.FixedHeight = 230f;
            ShipMarkHead.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            ShipMarkHead.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            CertificateItems.AddCell(ShipMarkHead);

            //PdfPCell PackageHead = new PdfPCell(new Phrase(" ", fontFooterDetails));
            //PackageHead.FixedHeight = 20f;
            //PackageHead.Border = Rectangle.LEFT_BORDER;
            //PackageHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //CertificateItems.AddCell(PackageHead);

            //PdfPCell HSHead = new PdfPCell(new Phrase(" ", fontFooterDetails));
            //HSHead.FixedHeight = 20f;
            //HSHead.Border = Rectangle.LEFT_BORDER;
            //HSHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //CertificateItems.AddCell(HSHead);

            //PdfPCell QttHead = new PdfPCell(new Phrase(" ", fontFooterDetails));
            //QttHead.FixedHeight = 20f;
            //QttHead.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //QttHead.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            //CertificateItems.AddCell(QttHead);

            //doc.Add(table);

            //PdfPTable CertificateItems = new PdfPTable(3);
            //CertificateItems.HorizontalAlignment = 1;
            //CertificateItems.WidthPercentage = 300;
            ////CertificateItems.SpacingBefore = 4;
            //CertificateItems.SpacingAfter = 2;
            //CertificateItems.DefaultCell.Border = Rectangle.BOX;
            //CertificateItems.SetWidths(new int[] { 5, 1, 1 });


            //PdfPCell GoodItemDetails = new PdfPCell(new Phrase("adfdsfafasf", fontFooterDetails));//CertReqDetails[i].GoodItem1
            //GoodItemDetails.MinimumHeight = 240f;
            //GoodItemDetails.Border = Rectangle.LEFT_BORDER;
            //CertificateItems.AddCell(GoodItemDetails);

            //PdfPCell HSDetails = new PdfPCell(new Phrase("fdfd", fontFooterDetails));//CertReqDetails[i].GoodItem1
            //HSDetails.MinimumHeight = 240f;
            //HSDetails.Border = Rectangle.LEFT_BORDER;
            //CertificateItems.AddCell(HSDetails);

            //PdfPCell QuantityDetails = new PdfPCell(new Phrase("dfdf", fontFooterDetails));//CertReqDetails[i].GoodItem1
            //QuantityDetails.MinimumHeight = 240f;
            //QuantityDetails.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //CertificateItems.AddCell(QuantityDetails);

            document.Add(CertificateItems);
        }


        private void printStatement()
        {
            PdfPTable StatemeTBL = new PdfPTable(4);
            StatemeTBL.HorizontalAlignment = 1;
            StatemeTBL.WidthPercentage = 100;
            //CertificateItems.SpacingBefore = 4;
            StatemeTBL.SpacingAfter = 2;
            StatemeTBL.DefaultCell.Border = Rectangle.BOX;
            StatemeTBL.SetWidths(new int[] { 1, 1, 1, 1 });


            string reason = System.Configuration.ConfigurationManager.AppSettings["SigningReason"];
            if (AuthoirzedOfficer == "")
            {
                reason = string.Empty;
            }
            PdfPCell Statementtxt = new PdfPCell(new Phrase(reason, fontFooterDetails));
            Statementtxt.Colspan = 4;
            Statementtxt.FixedHeight = 20f;
            Statementtxt.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            StatemeTBL.AddCell(Statementtxt);
            document.Add(StatemeTBL);
        }


        public bool CreateCertificate(string Comment)
        {
            try
            {
                document = new Document(PageSize.A4, 25f, 25f, 25f, 15f);//Lest,right,top,bottom

                pdfwriter = PdfWriter.GetInstance(document, new FileStream(CertificateSavePath, FileMode.Create));

                document.Open();
                drawCertificateOfOrginHeader();
                printListHead();
                printlistRow();
                drawBottom(Comment);
                printStatement();
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