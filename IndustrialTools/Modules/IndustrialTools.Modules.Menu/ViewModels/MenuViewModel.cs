using IndustrialTools.Core;
using Prism.Mvvm;


namespace IndustrialTools.Modules.Menu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        public MenuViewModel(IApplicationCommands applicationCommands)
        {
            ApplicationCommands = applicationCommands;
        }


    }
}
