namespace SignPDF.Services.Abstract
{
    public interface IPDFSigningService
    {
        void Sign(string inputPath, string outPath, string imagePath, bool visible = true);
    }
}