using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System.IO;
using System.Threading.Tasks;
using PdfCompressionService.Services.Interfaces;

namespace PdfCompressionService.Services
{
    public class PdfCompressionService : IPdfCompressionService
    {
        public PdfCompressionService()
        {
        }

        public Task<MemoryStream> CompressPdfAsync(Stream pdfStream)
        {
            // Ensure the stream is at the beginning
            pdfStream.Position = 0;

            // Create a new MemoryStream to hold the compressed PDF
            var compressedPdfStream = new MemoryStream();

            try
            {
                // Load the PDF document from the stream
                var pdfDocument = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import);

                // Create a new PDF document
                var outputDocument = new PdfDocument();

                // Iterate through all pages
                foreach (var page in pdfDocument.Pages)
                {
                    // Import the pages into the new document
                    outputDocument.AddPage(page);

                    // TODO: Implement image compression within the page
                    // This might involve replacing images in the PDF with compressed versions
                    // The specific implementation will depend on your compression requirements
                }

                // Save the compressed PDF to the MemoryStream
                outputDocument.Save(compressedPdfStream, false);
            }
            catch
            {
                // Handle exceptions (e.g., invalid PDF file)
                return null;
            }

            // Reset the stream position for reading
            compressedPdfStream.Position = 0;

            if (compressedPdfStream == null || compressedPdfStream.Length == 0)
            {
                throw new InvalidOperationException("PDF compression failed.");
            }

            return Task.FromResult(compressedPdfStream);
        }
    }
}