using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialTools.Modules.Content.ViewModels
{
    public  class VisionEmguCVViewModel  : BindableBase
    {

        private string _message = "VisionEmguCVViewModel";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}
