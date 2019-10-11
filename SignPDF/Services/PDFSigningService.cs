using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using SignPDF.Services.Abstract;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SignPDF.Services
{
    public class PDFSigningService : IPDFSigningService
    {
        public void Sign(string inputPath, string outPath, string imagePath, bool visible = true)
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);

            X509Certificate2 cert = sel[0];

            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {
            cp.ReadCertificate(cert.RawData)};

            IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");

            PdfReader pdfReader = new PdfReader(inputPath);

            var signedPdf = new FileStream(outPath, FileMode.Create);

            var pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0');
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;

            if (imagePath != null && imagePath != string.Empty)
            {
                signatureAppearance.SignatureGraphic = Image.GetInstance(imagePath);
            }

            signatureAppearance.SetVisibleSignature(new Rectangle(100, 100, 250, 150), pdfReader.NumberOfPages, "Signature");
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;

            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);
            Process.Start(outPath);
        }
    }
}
