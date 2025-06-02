using IndustrialTools.Common.Models;
using IndustrialTools.Core;
using IndustrialTools.Core.Events;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System.Collections.Generic;
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

        public DelegateCommand<object> ConnectionCommand { get; private set; }
        public DelegateCommand<object> HelpCommand  { get; private set; }
     
        public ObservableCollection<TreeNode> Nodes { get; set; }

        IEventAggregator _aggregator;
        public ContentViewModel(IApplicationCommands applicationCommands,IEventAggregator aggregator, IRegionManager regionManager, IDialogService dialogService)
        {
            _applicationCommands = applicationCommands;
            _dialogService = dialogService;
            _regionManager = regionManager;
            _aggregator = aggregator;
            ConnectionCommand = new DelegateCommand<object>(Connection);
            HelpCommand = new DelegateCommand<object>(Help);
            _applicationCommands.Connection.RegisterCommand(ConnectionCommand);
            _applicationCommands.Help.RegisterCommand(HelpCommand);       
            Nodes = new ObservableCollection<TreeNode>();
        }
   

        public  void Help(object ob)
        {
            string d = ob.ToString();

            if (d== "Welcome")
            {
                Welcome();
            }
            else
            {
                About();
            }

        }
        public void Welcome()
        {
            _dialogService.ShowDialog("HelpDialog", new DialogParameters($"message=Welcome"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                {
                    Title = "Result is OK";
                    Title = r.Parameters.GetValue<string>("ConnectionResult");
                    string d = r.Parameters.GetValue<string>("ConnectionType");
                    Nodes.Add(new TreeNode(d));
                    _regionManager.RequestNavigate("MidContentRegion", Title);
                }
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
        public void About()
        {
            _dialogService.ShowDialog("HelpDialog", new DialogParameters($"message=About"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                {
                    Title = "Result is OK";
                    Title = r.Parameters.GetValue<string>("ConnectionResult");
                    string d = r.Parameters.GetValue<string>("ConnectionType");
                    Nodes.Add(new TreeNode(d));
                    _regionManager.RequestNavigate("MidContentRegion", Title);
                }
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
        private void Connection(object ob )
        {
            Message = ob.ToString();
            _dialogService.ShowDialog("ConnectionDialog", new DialogParameters($"message={Message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                {
                    Title = "Result is OK";
                    Title = r.Parameters.GetValue<string>("ConnectionResult");
                    string d  = r.Parameters.GetValue<string>("ConnectionType");
                    List<TreeNode> treeNodes = r.Parameters.GetValue<List<TreeNode>>("ConnectionTreeNodes");
                    _regionManager.RequestNavigate("MidContentRegion", Title);
                    Nodes.Add(new TreeNode(d));
                    _aggregator.GetEvent<TreeNodeMessageEvent>().Publish(treeNodes);
                }
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
            });
        }
    }
}
