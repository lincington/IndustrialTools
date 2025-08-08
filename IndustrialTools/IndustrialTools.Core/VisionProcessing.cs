using HalconDotNet;

namespace IndustrialTools.Core
{
    public class VisionProcessing
    {
        public void ProcessImage()
        {
                HObject image;            
                HOperatorSet.ReadImage(out image, "path_to_your_image.jpg");
        }
}
}
