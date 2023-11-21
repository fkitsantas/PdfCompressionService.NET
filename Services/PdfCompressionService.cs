using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfCompressionService.Helpers;
using System.IO;
using System.Threading.Tasks;
using PdfCompressionService.Services.Interfaces;
using PdfSharpCore.Pdf.Advanced;

namespace PdfCompressionService.Services
{
    public class PdfCompressionService : IPdfCompressionService
    {
        public async Task<MemoryStream> CompressPdfAsync(Stream pdfStream)
        {
            pdfStream.Position = 0;
            var compressedPdfStream = new MemoryStream();
            var pdfDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import);
            var outputDocument = new PdfDocument();

            foreach (var page in pdfDocument.Pages)
            {
                var resources = page.Elements.GetDictionary("/Resources");
                if (resources != null)
                {
                    var xObjects = resources.Elements.GetDictionary("/XObject");
                    if (xObjects != null)
                    {
                        foreach (var item in xObjects.Elements.Values)
                        {
                            if (item is PdfReference reference)
                            {
                                var xObject = reference.Value as PdfDictionary;
                                if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                                {
                                    var imageStream = new MemoryStream(xObject.Stream.Value);
                                    var compressedImageStream = await ImageCompressor.CompressImageAsync(imageStream);

                                    // Modify the existing stream instead of creating a new one
                                    xObject.Stream.Value = compressedImageStream.ToArray();
                                }
                            }
                        }
                    }
                }
                outputDocument.AddPage(page);
            }

            outputDocument.Save(compressedPdfStream, false);
            compressedPdfStream.Position = 0;
            return compressedPdfStream;
        }
    }
}