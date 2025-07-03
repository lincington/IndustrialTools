using IndustrialTools.Common.Models;
using IndustrialTools.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace IndustrialTools.Modules.Content.ViewModels
{
    public class DBConnectionViewModel : BindableBase
    {
        private ObservableCollection<TreeNode> _Nodes = new ObservableCollection<TreeNode>();
        public ObservableCollection<TreeNode> Nodes
        {
            get { return _Nodes; }
            set { SetProperty(ref _Nodes, value); }
        }
        IEventAggregator _aggregator;

        private DelegateCommand<object> _ShowContentCommand;
        public DelegateCommand<object> ShowContentCommand =>
            _ShowContentCommand ?? (_ShowContentCommand = new DelegateCommand<object>(ShowContentChangedDialog));


        public void ShowContentChangedDialog(object ob)
        {

        }

        public DBConnectionViewModel(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
           
            _aggregator.GetEvent<TreeNodeMessageEvent>().Subscribe(ShowMethod);
        }

        public void ShowMethod(List<TreeNode> message)
        {
            Nodes.Clear();
            message.ForEach(node =>
            {
                if (!Nodes.Contains(node))
                {
                    Nodes.Add(node);
                }
            });
        }

    }
}
