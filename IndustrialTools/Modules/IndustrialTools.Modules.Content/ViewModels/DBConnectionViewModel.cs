using Dapper;
using IndustrialTools.Common;
using IndustrialTools.Common.Models;
using IndustrialTools.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;


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


        private DataView _tableView;
        public DataView TableView
        {
            get { return _tableView; }
            set { SetProperty(ref _tableView, value); }
        }
        public void ShowContentChangedDialog(object ob)
        {
            DataBaseFactory  dataBaseFactory = new DataBaseFactory();

            if (ob is TreeNode node)
            {
                string  da = "Select * from " + node.FullName + "   LIMIT  1000";
                TableView= dataBaseFactory.ExecuteSql(da, DataBaseType.MySql).DefaultView;  
            }
            else if (ob is string str)
            {
                
            }
            else
            {
                return;
            }
         
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
