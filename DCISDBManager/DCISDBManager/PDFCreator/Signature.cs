using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DCISDBManager.trnLib.Utility;
using DCISDBManager.objLib.Email;
using DCISDBManager.objLib.Master;
using iTextSharp.text;

namespace DCISDBManager.PDFCreator
{
    public class Signature
    {

        string reason = System.Configuration.ConfigurationManager.AppSettings["SigningReason"];
        string Seal_AbsolutePosition_X = System.Configuration.ConfigurationManager.AppSettings["Seal_AbsolutePosition_X"];
        string Seal_AbsolutePosition_Y = System.Configuration.ConfigurationManager.AppSettings["Seal_AbsolutePosition_Y"];
        string Seal_ScaleAbsolute_W = System.Configuration.ConfigurationManager.AppSettings["Seal_ScaleAbsolute_W"];
        string Seal_ScaleAbsolute_H = System.Configuration.ConfigurationManager.AppSettings["Seal_ScaleAbsolute_H"];

        public bool signCertificate(string DocumentPath, string CertificateSavePath, Stream privateKeyStream, string keyPassword,string SignatureIMGPath)
        {
            try
            {
                Pkcs12Store pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());

                privateKeyStream.Dispose();

                //then Iterate throught certificate entries to find the private key entry
                string alias = null;
                foreach (string tAlias in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }
                var pk = pk12.GetKey(alias).Key;

                // reader and stamper
                PdfReader reader = new PdfReader(DocumentPath);
                int PageCount = reader.NumberOfPages;

                using (FileStream fout = new FileStream(CertificateSavePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0',null,true))
                    {
                        // appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Image = new iTextSharp.text.pdf.PdfImage();
                        //appearance.Reason = reason;
                        //   appearance.Location = location;
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(163, 72, 297, 24), PageCount, "Icsi-Vendor");//.IsInvisible();//s
                        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(SignatureIMGPath);
                        appearance.Image = watermark;
                        appearance.Image.ScaleToFit(70, 70);
                        //appearance.Image.Alignment=100;
                        appearance.Image.SetAbsolutePosition(100, 100);
                        appearance.GetAppearance().AddImage(watermark);

                        //digital signature
                        IExternalSignature es = new PrivateKeySignature(pk, "SHA-256");
                        MakeSignature.SignDetached(appearance, es, new Org.BouncyCastle.X509.X509Certificate[] { pk12.GetCertificate(alias).Certificate }, null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {               
                ErrorLog.LogError(Ex);
                return false;
            }

        }

        public bool signCertificate(string DocumentPath, string CertificateSavePath, Stream privateKeyStream, string keyPassword)
        {
            try
            {
                Pkcs12Store pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());

                privateKeyStream.Dispose();

                //then Iterate throught certificate entries to find the private key entry
                string alias = null;
                foreach (string tAlias in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }
                var pk = pk12.GetKey(alias).Key;

                // reader and stamper
                PdfReader reader = new PdfReader(DocumentPath);
                int PageCount = reader.NumberOfPages;

                using (FileStream fout = new FileStream(CertificateSavePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0', null, true))
                    {
                        // appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Image = new iTextSharp.text.pdf.PdfImage();
                        //appearance.Reason = reason;
                        //   appearance.Location = location;
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(220, 165, 420, 250), PageCount, "Icsi-Vendor");//.IsInvisible();//s
                        //220,165, 435, 310
                        //lly - gose up  llx - is width 210,245,495,620  500, 300, 297, 200
                        //iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(SignatureIMGPath);
                        //appearance.Image = watermark;
                        //appearance.Image.ScaleToFit(70, 70);
                        ////appearance.Image.Alignment=100;
                        //appearance.Image.SetAbsolutePosition(100, 100);
                        //appearance.GetAppearance().AddImage(watermark);

                        //digital signature
                        IExternalSignature es = new PrivateKeySignature(pk, "SHA-256");
                        MakeSignature.SignDetached(appearance, es, new Org.BouncyCastle.X509.X509Certificate[] { pk12.GetCertificate(alias).Certificate }, null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }

        }

        public bool signCertificate(string DocumentPath, string CertificateSavePath, Stream privateKeyStream, string keyPassword, string SignatureIMGPath,EmailCertificateConfig EConfig)
        {
            try
            {
                Pkcs12Store pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());

                privateKeyStream.Dispose();

                //then Iterate throught certificate entries to find the private key entry
                string alias = null;
                foreach (string tAlias in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }
                var pk = pk12.GetKey(alias).Key;

                // reader and stamper
                PdfReader reader = new PdfReader(DocumentPath);
                int PageCount = reader.NumberOfPages;

                using (FileStream fout = new FileStream(CertificateSavePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0', null, true))
                    {
                        // appearance
                                PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                                //appearance.Image = new iTextSharp.text.pdf.PdfImage();
                               // appearance.Reason = reason;
                                //   appearance.Location = location;
                                float llx = float.Parse(EConfig.LLX_Cordinates.ToString());
                                float lly = float.Parse(EConfig.LLY_Cordinates.ToString());
                                float urx = float.Parse(EConfig.URX_cordinates.ToString());
                                float ury = float.Parse(EConfig.URY_cordinates.ToString());

                                appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(llx, lly, urx, ury), PageCount, "Icsi-Vendor");//.IsInvisible();//s
                                iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(SignatureIMGPath);
                                appearance.Image = watermark;
                                appearance.Image.ScaleToFit(70, 70);
                                //appearance.Image.Alignment=100;
                                appearance.Image.SetAbsolutePosition(100, 100);
                                appearance.GetAppearance().AddImage(watermark);

                                //digital signature
                                IExternalSignature es = new PrivateKeySignature(pk, "SHA-256");
                                MakeSignature.SignDetached(appearance, es, new Org.BouncyCastle.X509.X509Certificate[] { pk12.GetCertificate(alias).Certificate }, null, null, null, 0, CryptoStandard.CMS);

                                stamper.Close();
                            
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }

        }

        public bool signCertificate(string DocumentPath, string CertificateSavePath, Stream privateKeyStream, string keyPassword, string SignatureIMGPath, SDSignatureConfig SDConfig, string RequestID)
        {
            try
            {
                Pkcs12Store pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());

                privateKeyStream.Dispose();

                //then Iterate throught certificate entries to find the private key entry
                string alias = null;
                foreach (string tAlias in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }
                var pk = pk12.GetKey(alias).Key;

                // reader and stamper
                PdfReader reader = new PdfReader(DocumentPath);
                int PageCount = reader.NumberOfPages;

                using (FileStream fout = new FileStream(CertificateSavePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0', null, true))
                    {
                        // appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Image = new iTextSharp.text.pdf.PdfImage();
                        appearance.Reason = "Request-(ID =" + RequestID + ") " + reason;
                        //   appearance.Location = location;
                        float llx = float.Parse(SDConfig.LLX_Cordinates.ToString());
                        float lly = float.Parse(SDConfig.LLY_Cordinates.ToString());
                        float urx = float.Parse(SDConfig.URX_cordinates.ToString());
                        float ury = float.Parse(SDConfig.URY_cordinates.ToString());

                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(llx, lly, urx, ury), PageCount, "Icsi-Vendor");//.IsInvisible();//s
                        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(SignatureIMGPath);
                        appearance.Image = watermark;
                        appearance.Image.ScaleToFit(70, 70);
                        //appearance.Image.Alignment=100;
                        appearance.Image.SetAbsolutePosition(100, 100);
                        appearance.GetAppearance().AddImage(watermark);

                        //digital signature
                        IExternalSignature es = new PrivateKeySignature(pk, "SHA-256");
                        MakeSignature.SignDetached(appearance, es, new Org.BouncyCastle.X509.X509Certificate[] { pk12.GetCertificate(alias).Certificate }, null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();

                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }

        }

        public bool signSupportingDoc(string RefID, string DocumentPath, string CertificateSavePath, Stream privateKeyStream, string keyPassword)
        {
            try
            {
                Pkcs12Store pk12 = new Pkcs12Store(privateKeyStream, keyPassword.ToCharArray());

                privateKeyStream.Dispose();

                //then Iterate throught certificate entries to find the private key entry
                string alias = null;
                foreach (string tAlias in pk12.Aliases)
                {
                    if (pk12.IsKeyEntry(tAlias))
                    {
                        alias = tAlias;
                        break;
                    }
                }
                var pk = pk12.GetKey(alias).Key;

                // reader and stamper
                PdfReader reader = new PdfReader(DocumentPath);
                int PageCount = reader.NumberOfPages;

                using (FileStream fout = new FileStream(CertificateSavePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0', null, true))
                    {
                        // appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                        //appearance.Image = new iTextSharp.text.pdf.PdfImage();
                        appearance.Reason = "Sign Request (Ref : " + RefID + ")";
                        //   appearance.Location = location; (0, 0, 320, 72) Lower left conner
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(600, 0, 400, 72), PageCount, "Icsi-Vendor");
                        //digital signature
                        IExternalSignature es = new PrivateKeySignature(pk, "SHA-256");
                        MakeSignature.SignDetached(appearance, es, new Org.BouncyCastle.X509.X509Certificate[] { pk12.GetCertificate(alias).Certificate }, null, null, null, 0, CryptoStandard.CMS);

                        stamper.Close();
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }

        }

        public bool AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, System.Drawing.Point point, string SEALIMG,bool SealRequred,string Signatory)
        {
            try
            {
                //variables
                string pathin = inputPdfPath;
                string pathout = outputPdfPath;

                //create PdfReader object to read from the existing document
                using (PdfReader reader = new PdfReader(pathin))
                //create PdfStamper object to write to get the pages from reader 
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
                {
                    //select two pages from the original document
                    reader.SelectPages("1-2");

                    //gettins the page size in order to substract from the iTextSharp coordinates
                    var pageSize = reader.GetPageSize(1);

                    // PdfContentByte from stamper to add content to the pages over the original content
                    PdfContentByte pbover = stamper.GetOverContent(1);

                    //add content to the page using ColumnText
                    Font font = new Font();
                    font.Size = 13;

                    Font font2 = new Font();
                    font2.Size = 7;

                    Font font3 = new Font();
                    font3.Size = 12;

                    Font font4 = new Font();
                    font4.Size = 8;

                    //setting up the X and Y coordinates of the document
                    int x = point.X;
                    int y = point.Y;

                    x += 113;
                    y = (int)(pageSize.Height - y);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(textToAdd, font), x, y, 0);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(reason, font2), 300, 13, 0);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(DateTime.Now.ToString("yyyy/MM/dd"), font4), 70, 35, 0);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(Signatory, font4), 70, 70, 0);
                    if (SealRequred)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(SEALIMG);
                        img.SetAbsolutePosition(60, 140); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                        img.ScaleAbsolute(140f, 153.25f);
                        PdfContentByte waterMark;
                        for (int page = 1; page <= reader.NumberOfPages; page++)
                        {
                            waterMark = stamper.GetOverContent(page);
                            waterMark.AddImage(img);
                        }
                    }

                    stamper.FormFlattening = true;
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }
        }


        public bool AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, System.Drawing.Point point, string SEALIMG)
        {
            try
            {
                //variables
                string pathin = inputPdfPath;
                string pathout = outputPdfPath;

                //create PdfReader object to read from the existing document
                using (PdfReader reader = new PdfReader(pathin))
                //create PdfStamper object to write to get the pages from reader 
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
                {
                    //select two pages from the original document
                    reader.SelectPages("1-2");

                    //gettins the page size in order to substract from the iTextSharp coordinates
                    var pageSize = reader.GetPageSize(1);

                    // PdfContentByte from stamper to add content to the pages over the original content
                    PdfContentByte pbover = stamper.GetOverContent(1);

                    //add content to the page using ColumnText
                    Font font = new Font();
                    font.Size = 13;

                    Font font2 = new Font();
                    font2.Size = 7;

                    //setting up the X and Y coordinates of the document
                    int x = point.X;
                    int y = point.Y;

                    x += 113;
                    y = (int)(pageSize.Height - y);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(textToAdd, font), x, y, 0);
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(reason, font2), 300, 13, 0);

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(SEALIMG);
                    img.SetAbsolutePosition(60, 140); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                    img.ScaleAbsolute(140f, 153.25f);
                    PdfContentByte waterMark;
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        waterMark = stamper.GetOverContent(page);
                        waterMark.AddImage(img);
                    }
                    

                    stamper.FormFlattening = true;
                }
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }
        }
        public bool AddSeal(string DocumentPath, string SealIMG)
        {
            try
            {
                string WatermarkLocation = SealIMG;
                // string FileLocation = Server.MapPath(Signed);

                Document document = new Document();
                PdfReader pdfReader = new PdfReader(DocumentPath);
                PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(DocumentPath.Replace(".pdf", "_S.pdf"), FileMode.Create));

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(WatermarkLocation);
                img.SetAbsolutePosition(60, 125); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                img.ScaleAbsolute(140f, 153.25f);

                PdfContentByte waterMark;
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    waterMark = stamp.GetOverContent(page);
                    waterMark.AddImage(img);
                }
                stamp.FormFlattening = true;

                stamp.Close();
                document.Close();
                stamp.Close();
                return true;
            }
            catch (Exception Ex)
            {
                ErrorLog.LogError(Ex);
                return false;
            }
        }

        public bool AddSealSD(string DocumentPath, string SavePath, string SealIMG)
        {
            try
            {
                string WatermarkLocation = SealIMG;
                // string FileLocation = Server.MapPath(Signed);

                Document document = new Document();
                PdfReader pdfReader = new PdfReader(DocumentPath);
                int PageCount = pdfReader.NumberOfPages;
                PdfStamper stamp = new PdfStamper(pdfReader, new FileStream(SavePath, FileMode.Create));

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(WatermarkLocation);
                img.SetAbsolutePosition(float.Parse(Seal_AbsolutePosition_X), float.Parse(Seal_AbsolutePosition_Y)); // set the position in the document where you want the watermark to appear (0,0 = bottom left corner of the page)
                img.ScaleAbsolute(float.Parse(Seal_ScaleAbsolute_W), float.Parse(Seal_ScaleAbsolute_H));
                PdfContentByte waterMark;
                for (int page = PageCount; page <= pdfReader.NumberOfPages; page++)
                {
                    waterMark = stamp.GetOverContent(page);
                    waterMark.AddImage(img);
                }
                stamp.FormFlattening = true;

                stamp.Close();
                document.Close();
                stamp.Close();
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