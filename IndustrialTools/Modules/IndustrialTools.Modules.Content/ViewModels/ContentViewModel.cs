using IndustrialTools.Core;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

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

        IApplicationCommands _applicationCommands;

        public DelegateCommand UpdateCommand { get; private set; }


        public ContentViewModel(IApplicationCommands applicationCommands)
        {
            Message = "View A from your Prism Module";
            _applicationCommands = applicationCommands;
            UpdateCommand = new DelegateCommand(Update);
            _applicationCommands.Connection.RegisterCommand(UpdateCommand);


        }

        private void Update()
        {
            MessageBox.Show("Update Command Executed");
        }
    }
}
