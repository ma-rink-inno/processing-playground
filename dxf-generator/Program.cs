using System;
using System.Drawing;
using netDxf;
using netDxf.Entities;
using netDxf.Objects;

namespace dxf_genarator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Bitmap img = new Bitmap(".\\innolizer-signet.png");

            int rasterSize = 15;
            var slope = GetSlope(255, -50, 0, rasterSize);

            string file = "innolizer-signet.dxf";
            DxfDocument doc = new DxfDocument();

            var targetWidth = 85;
            // var targetHeight = targetWidth * img.Height / img.Width;

            var resizeFactor = (float)targetWidth / img.Width;
            Console.WriteLine(resizeFactor);
            var targetHeight = img.Height * resizeFactor;
            Console.WriteLine($"{img.Width}x{img.Height} -> {targetWidth}x{targetHeight}");

            doc.AddEntity(new Polyline(new Vector3[] {
                new Vector3(0, 0, 0),
                new Vector3(targetWidth, 0, 0),
                new Vector3(targetWidth, targetHeight, 0),
                new Vector3(0, targetHeight, 0)
            }, true));
            int row = 0;
            for (int y = rasterSize / 2; y < img.Height; y += rasterSize)
            {
                int start = rasterSize / 2;
                int end = img.Width;
                if (row % 2 > 0)
                {
                    start = rasterSize;
                    end = img.Width - rasterSize/2;
                }
                for (int x = start; x < end; x += rasterSize)
                {
                    var pixel = img.GetPixel(x, y);
                    var pixelbrightness = pixel.GetBrightness() * 255.0f;
                    var diameter = Map(pixelbrightness, slope, 255, 0);
                    if (diameter > 0)
                    {
                        var circle = new Circle { Center = new Vector3(x * resizeFactor, targetHeight - y * resizeFactor, 0), Radius = diameter * resizeFactor / 2 };
                        doc.AddEntity(circle);
                    }
                }
                row++;
            }

            doc.Save(file);
        }

        private static float
        GetSlope(
            float sourceMin,
            float sourceMax,
            float targetMin,
            float targetMax
        )
        {
            return (targetMax - targetMin) / (sourceMax - sourceMin);
        }

        private static float
        Map(float value, float slope, float sourceMin, float targetMin)
        {
            return targetMin + slope * (value - sourceMin);
        }
    }
}
