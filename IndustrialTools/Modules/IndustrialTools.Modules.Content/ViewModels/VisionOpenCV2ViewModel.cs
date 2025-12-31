using IndustrialTools.Lib;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows.Media.Imaging;


namespace IndustrialTools.Modules.Content.ViewModels
{
    public class VisionOpenCV2ViewModel : BindableBase
    {

        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private BitmapImage _CaseCoverImage;
        public BitmapImage CaseCoverImage
        {
            get { return _CaseCoverImage; }
            set { SetProperty(ref _CaseCoverImage, value); }
        }
        private string _message= "VisionOpenCV2ViewModel";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        IEventAggregator _aggregator;
        public VisionOpenCV2ViewModel(IEventAggregator aggregator) {
  
            _aggregator = aggregator;
            logger.Info("VisionOpenCV2ViewModel Init");
            //PaddleOCRSharpHelper  paddleOCRSharpHelper = new PaddleOCRSharpHelper();
            //Message= paddleOCRSharpHelper.GetPaddleOCREngine();
            TesseractHelper tesseractHelper = new TesseractHelper();
            Message = tesseractHelper.ImageToText();
        }
    }
}
