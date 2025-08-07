using IndustrialTools.Common.Models;
using IndustrialTools.Core;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace IndustrialTools.Modules.Menu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        public ObservableCollection<MenuItemModel> MenuItems { get; set; }

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
            // Initialize the MenuItems collection with some sample data
            string[] Connection = { 
            "DB Connection",
            "WEB Connection",
            "Modbus Connection",
            "PLC Connection",
            "COM Connection",
            "MQTT Connection",
            "RabbitMQ Connection",
            "gRPC Connection"};

            MenuItems = new ObservableCollection<MenuItemModel>();

            foreach (var command in Connection) {
                MenuItems.Add(new MenuItemModel
                {
                    Header = command,
                    Command = ApplicationCommands.Connection,
                    CommandParameter = command
                });
            }
        }
    }
}
