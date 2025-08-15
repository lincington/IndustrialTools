using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace IndustrialTools.Lib
{
    public class TesseractHelper
    {
        public TesseractHelper() { }

        public string ImageToText(string imgPath= "D:\\IndustrialTools\\IndustrialTools\\IndustrialTools\\Common\\IndustrialTools.Lib\\Images\\1.png")
        {
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
