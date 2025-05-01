using IndustrialTools.Core;
using Prism.Commands;
using Prism.Dialogs;
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


        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }



        IApplicationCommands _applicationCommands;

        private IDialogService _dialogService;


        public DelegateCommand UpdateCommand { get; private set; }


        public ContentViewModel(IApplicationCommands applicationCommands,IDialogService dialogService)
        {
            Message = "View A from your Prism Module";

            _applicationCommands = applicationCommands;
            _dialogService = dialogService;

            UpdateCommand = new DelegateCommand(Update);
            _applicationCommands.Connection.RegisterCommand(UpdateCommand);


        }

        private void Update()
        {
            //MessageBox.Show("Update Command Executed");

            _dialogService.ShowDialog("ConnectionDialog", new DialogParameters($"message={Message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Title = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
    }
}
