using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using PdfCompressionService.Services.Interfaces;

namespace PdfCompressionService.Controllers
{
    [ApiController]
    [Route("")]
    public class CompressionController : ControllerBase
    {
        private readonly IPdfCompressionService _pdfCompressionService;

        public CompressionController(IPdfCompressionService pdfCompressionService)
        {
            _pdfCompressionService = pdfCompressionService;
        }

        [HttpPost("compressPdf")]
        public async Task<IActionResult> CompressPdf(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("A PDF file is required.");
            }

            try
            {
                using var inputStream = new MemoryStream();
                await pdfFile.CopyToAsync(inputStream);
                var compressedStream = await _pdfCompressionService.CompressPdfAsync(inputStream);

                if (compressedStream == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "PDF compression failed.");
                }

                compressedStream.Position = 0;
                return File(compressedStream, "application/pdf", "compressed.pdf");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while compressing the PDF.");
            }
        }
    }
}