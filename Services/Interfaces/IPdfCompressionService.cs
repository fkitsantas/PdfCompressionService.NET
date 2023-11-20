using System.IO;
using System.Threading.Tasks;

namespace PdfCompressionService.Services.Interfaces
{
    public interface IPdfCompressionService
    {
        /// <summary>
        /// Compresses a PDF file.
        /// </summary>
        /// <param name="pdfStream">The stream containing the PDF to be compressed.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains the compressed PDF as a MemoryStream.</returns>
        Task<MemoryStream> CompressPdfAsync(Stream pdfStream);
    }
}
