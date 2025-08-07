using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
namespace IndustrialTools.Core
{
    // This class demonstrates basic image processing using HALCON

    public class VisionProcessing
    {
        public void ProcessImage()
        {
            // Initialize HALCON objects
            HImage image = new HImage();
            HWindow window = new HWindow(0, 0, 512, 512);

            try
            {
                // Read and display an image
                image.ReadImage("particle");
                window.DispObj(image);

                // Perform thresholding
                HRegion region = image.Threshold(128, 255);
                window.SetColor("red");
                window.DispObj(region);
            }
            catch (HalconException e)
            {
                // Handle HALCON-specific exceptions
                Console.WriteLine("HALCON error: " + e.Message);
            }
            finally
            {
                // Clean up
                image.Dispose();
                window.Dispose();
            }
        }
    }
}
