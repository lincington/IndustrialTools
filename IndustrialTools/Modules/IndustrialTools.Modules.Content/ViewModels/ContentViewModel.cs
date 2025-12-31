using AutoUpdaterDotNET;
using IndustrialTools.Common.Models;
using IndustrialTools.Core;
using IndustrialTools.Core.Events;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
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
        public DelegateCommand<object> VisionCommand  { get; private set; }


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
            VisionCommand = new DelegateCommand<object>(Vision);


            _applicationCommands.Connection.RegisterCommand(ConnectionCommand);
            _applicationCommands.Help.RegisterCommand(HelpCommand);
            _applicationCommands.Vision.RegisterCommand(VisionCommand);

            Nodes = new ObservableCollection<TreeNode>();
        }
   
        public void Vision(object ob)
        {
            _regionManager.RequestNavigate("MidContentRegion", ob.ToString());
        }

        public  void Help(object ob){
            string d = ob.ToString();
            if (d== "Welcome"){
                Welcome();
            }
            else if(d== "Update"){
                Update();
            }
            else{
                About();
            }
     }

        public void Update()
        {
            AutoUpdater.InstalledVersion = new Version("1.2");
            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 1 * 30 * 1000,
            };
            timer.Elapsed += delegate
            {
                AutoUpdater.Start("http://localhost:8086/Download/AutoUpdaterTest.xml");
            };
            timer.Start();
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
