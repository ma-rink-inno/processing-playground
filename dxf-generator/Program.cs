using System;
using System.Drawing;

namespace dxf_genarator
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            Bitmap img = new Bitmap(".\\innolizer-signet.png");


            int rasterSize = 10;
            var slope = GetSlope(255, -50, 1, rasterSize);
            
            for (int x = rasterSize/2; x < img.Width; x+=rasterSize) {
                for (int y = rasterSize/2; y < img.Height; y+=rasterSize) {
                    // int pixelvalue = logo.pixels[x + y * logo.width];
                    var pixel = img.GetPixel(x,y);
                    var pixelbrightness = pixel.GetBrightness() * 255.0f;
                    var diameter = Map(pixelbrightness, slope, 255, 1);
                    // float value = map(pixelbrightness, 255, -50, 0, rasterSize);
                    // ellipse(x, y, value, value);
                    
                }
                Console.WriteLine();
            }

            // for (int i = 0; i < img.Width; i++)
            // {
            //     for (int j = 0; j < img.Height; j++)
            //     {
                    

                    
            //     }
            // }
        }

        private static float GetSlope(float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            return (targetMax - targetMin) / (sourceMax - sourceMin);
        }

        private static float Map(float value, float slope, float sourceMin, float targetMin)
        {
            return targetMin + slope * (value - sourceMin);
        }
    }
}
