using Prism.Mvvm;


namespace IndustrialTools.Modules.Content.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ContentViewModel()
        {
            Message = "View A from your Prism Module";
        }
    }
}
