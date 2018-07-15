using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TiledMaps
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            //PackSprites();

            using (var game = new MainGame())
                game.Run();
        }

        private static void PackSprites()
        {
            const string inputPath = @"D:\Github\MonoGame.Extended\Source\Demos\TiledMaps\RawContent\isometric-landscape\PNG";
            const string outputPath = @"D:\Github\MonoGame.Extended\Source\Demos\TiledMaps\RawContent\isometric-landscape\packed.png";

            var maxWidth = 0;
            var maxHeight = 0;
            var count = 0;

            foreach (var filePath in Directory.GetFiles(inputPath))
            {
                using (var image = Image.Load(filePath))
                {
                    if (image.Width > maxWidth)
                        maxWidth = image.Width;

                    if (image.Height > maxHeight)
                        maxHeight = image.Height;
                }

                count++;
            }

            var columns = (int) Math.Sqrt(count) + 1;
            var rows = count / columns + 1;
            var packedWidth = maxWidth * columns;
            var packedHeight = maxHeight * rows;
            var xOffset = 0;
            var yOffset = 0;
            
            using (var packedImage = new Image<Rgba32>(packedWidth, packedHeight))
            {
                foreach (var filePath in Directory.GetFiles(inputPath))
                {
                    using (var image = Image.Load(filePath))
                    {
                        const int xAlignmentOffset = 0;
                        var yAlignmentOffset = maxHeight - image.Height;

                        for (var x = 0; x < image.Width; x++)
                        {
                            for (var y = 0; y < image.Height; y++)
                            {
                                var px = xOffset + x + xAlignmentOffset;
                                var py = yOffset + y + yAlignmentOffset;

                                packedImage[px, py] = image[x, y];
                            }
                        }
                    }

                    xOffset += maxWidth;

                    if (xOffset > packedWidth - maxWidth)
                    {
                        xOffset = 0;
                        yOffset += maxHeight;
                    }
                }

                using (var fileStream = File.OpenWrite(outputPath))
                    packedImage.SaveAsPng(fileStream);
            }

            Console.WriteLine($"width: {maxWidth} height: {maxHeight}");
        }
    }
}
