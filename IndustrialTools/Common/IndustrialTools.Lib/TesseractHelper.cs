
using Tesseract;

namespace IndustrialTools.Lib
{
    public class TesseractHelper
    {
        public TesseractHelper() { }

        string path= AppDomain.CurrentDomain.BaseDirectory + "\\Images\\1.png";

        public string ImageToText(string imgPath="")
        {
            if (!File.Exists(imgPath))
            {
                imgPath = path;
            }
            try
            {
                using (var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imgPath))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
