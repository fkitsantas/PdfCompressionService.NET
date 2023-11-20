using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace PdfCompressionService.Helpers
{
    public class ImageCompressor
    {
        public static async Task<MemoryStream> CompressImageAsync(Stream imageStream, int maxDimension = 1000)
        {
            imageStream.Position = 0;
            using var image = await Image.LoadAsync(imageStream);
            var scaleFactor = CalculateScaleFactor(image.Width, image.Height, maxDimension);

            image.Mutate(x => x.Resize((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor)));

            var compressedImageStream = new MemoryStream();
            await image.SaveAsync(compressedImageStream, GetEncoder(image));
            compressedImageStream.Position = 0;

            return compressedImageStream;
        }

        private static double CalculateScaleFactor(int width, int height, int maxDimension)
        {
            var scaleFactorWidth = (double)maxDimension / width;
            var scaleFactorHeight = (double)maxDimension / height;
            return (scaleFactorWidth < scaleFactorHeight) ? scaleFactorWidth : scaleFactorHeight;
        }

        private static IImageEncoder GetEncoder(Image image)
        {
            // Use the stored format in the metadata
            IImageFormat format = image.Metadata.DecodedImageFormat;
            if (format is null)
            {
                throw new InvalidOperationException("Image format could not be identified.");
            }

            return format switch
            {
                JpegFormat => new JpegEncoder(),
                PngFormat => new PngEncoder(),
                BmpFormat => new BmpEncoder(),
                GifFormat => new GifEncoder(),
                TiffFormat => new TiffEncoder(),
                _ => throw new NotSupportedException("Unsupported image format")
            };
        }
    }
}
