using IndustrialTools.Common.Models;
using IndustrialTools.Core;
using Npgsql.Replication.PgOutput.Messages;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using SqlSugar;
using System.Collections.ObjectModel;

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


        private string _title = "IndustrialTools";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;

        IApplicationCommands _applicationCommands;

        private IDialogService _dialogService;

        public DelegateCommand UpdateCommand { get; private set; }

        public ObservableCollection<TreeNode> Nodes { get; set; }

        public ContentViewModel(IApplicationCommands applicationCommands, IRegionManager regionManager, IDialogService dialogService)
        {
            _applicationCommands = applicationCommands;
            _dialogService = dialogService;
            _regionManager = regionManager;

            UpdateCommand = new DelegateCommand(Update);
            _applicationCommands.Connection.RegisterCommand(UpdateCommand);

            Nodes = new ObservableCollection<TreeNode>();
            //{
            //    new TreeNode("Root 1")
            //    {
            //        Children = new ObservableCollection<TreeNode>
            //        {
            //            new TreeNode("Child 1.1")
            //            {
            //        Children = new ObservableCollection<TreeNode>
            //        {
            //            new TreeNode("Child 2.1"),
            //            new TreeNode("Child 2.2")
            //        }
            //        }
            //        }
            //    },
            //    new TreeNode("Root 2")         
            //};
        }

        private void Update()
        {
            _dialogService.ShowDialog("ConnectionDialog", new DialogParameters($"message={Message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                {
                    Title = "Result is OK";
                    Title = r.Parameters.GetValue<string>("ConnectionResult");
                   string d  =   r.Parameters.GetValue<string>("ConnectionType");
                    Nodes.Add(new TreeNode(d));
                    _regionManager.RequestNavigate("MidContentRegion", Title);
                }
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
    }


  
}
