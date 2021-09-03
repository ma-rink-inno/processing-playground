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

            int rasterSize = 10;
            var slope = GetSlope(255, -50, 0, rasterSize);

            string file = "innolizer-signet.dxf";
            DxfDocument doc = new DxfDocument();


            doc.AddEntity(new Polyline(new Vector3[] {
                new Vector3(0, 0, 0),
                new Vector3(img.Width, 0, 0),
                new Vector3(img.Width, img.Height, 0),
                new Vector3(0, img.Height, 0)
            }, true));

            for (int x = rasterSize / 2; x < img.Width; x += rasterSize)
            {
                for (int y = rasterSize / 2; y < img.Height; y += rasterSize)
                {
                    // int pixelvalue = logo.pixels[x + y * logo.width];
                    var pixel = img.GetPixel(x, y);
                    var pixelbrightness = pixel.GetBrightness() * 255.0f;
                    var diameter = Map(pixelbrightness, slope, 255, 0);
                    // float value = map(pixelbrightness, 255, -50, 0, rasterSize);
                    // ellipse(x, y, value, value);
                    if (diameter > 0)
                    {
                        var circle = new Circle { Center = new Vector3(x, y, 0), Radius = diameter / 2 };
                        doc.AddEntity(circle);
                    }
                }
            }

            doc.Save(file);

            // for (int i = 0; i < img.Width; i++)
            // {
            //     for (int j = 0; j < img.Height; j++)
            //     {

            //     }
            // }
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
