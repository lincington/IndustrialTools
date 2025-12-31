using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;
using System;
using Microsoft.IdentityModel.Logging;
using System.Windows;
using IndustrialTools.Common.Models;
using System.Collections.ObjectModel;
using IndustrialTools.Common;
using Prism.Events;
using System.Collections.Generic;


namespace IndustrialTools.Modules.Content.ViewModels 
{
    public class ConnectionDialogViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand<object> _ComboboxSelectionChangedCommand;
        public DelegateCommand<object> ComboboxSelectionChangedCommand =>
            _ComboboxSelectionChangedCommand ?? (_ComboboxSelectionChangedCommand = new DelegateCommand<object>(ComboboxSelectionChangedDialog ));


        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));


        private string _message= "DBConnection";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DBConnection _DBConnectmessage = new  DBConnection();
        public DBConnection DBConnectMessage
        {
            get { return _DBConnectmessage; }
            set { SetProperty(ref _DBConnectmessage, value); }
        }

        private ModbusTCPConnection _modbusTCPConnection = new ModbusTCPConnection();
        public ModbusTCPConnection ModbusTCPConnection
        {
            get { return _modbusTCPConnection; }
            set { SetProperty(ref _modbusTCPConnection, value); }
        }
        private TreeDataModels combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public TreeDataModels CombboxItem
        {
            get { return combboxItem; }
            set { SetProperty(ref combboxItem, value); }
        }

        private ObservableCollection<TreeDataModels> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public ObservableCollection<TreeDataModels> DBSQLComList
        {
            get { return combboxList; }
            set { SetProperty(ref combboxList, value); }
         }

        private ObservableCollection<TreeDataModels> NocombboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public ObservableCollection<TreeDataModels> NoSQLComList
        {
            get { return NocombboxList; }
            set { SetProperty(ref NocombboxList, value); }
        }

        private string _Typemessage = "DBConnection";
        public string TypeMessage
        {
            get { return _Typemessage; }
            set { SetProperty(ref _Typemessage, value); }
        }

        private string _title = "Connection";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _Password = "";
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        public DialogCloseListener RequestClose { get; }

        enum ConnectionResult 
        {
            DB,
            Modbus,
            PLC,
            COM,
            MQ,
            RPC,
            TCP,
        }
        IDataBaseFactory _dataBaseFactory = new DataBaseFactory();
        DataBaseType dbType;
        IEventAggregator _aggregator;
     
        protected virtual void ComboboxSelectionChangedDialog(object parameter)
        {
            TreeDataModels dd = parameter  as TreeDataModels;
            dbType = dd.Key;
            switch (dbType)
            {
                case DataBaseType.MySql:
                    DBConnectMessage.Port = 3306;
                    DBConnectMessage.UserName = "root";
                    break;
                case DataBaseType.SqlServer:
                    DBConnectMessage.Port = 1433;
                    DBConnectMessage.UserName = "sa";
                    break;
                case DataBaseType.PostgreSQL:
                    DBConnectMessage.Port = 5432;
                    DBConnectMessage.UserName = "postgres";
                    break;
                case DataBaseType.Oracle:
                    DBConnectMessage.Port = 1521;
                     break;
                default:
                    break;
            }
        }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            string ConnectionString="";

          //string ConnectionString = "Data Source=192.168.1.70;User Id=sa;Password=201015;Initial Catalog=Stock;TrustServerCertificate=true;Pooling=true;Min Pool Size=1;";
          //string ConnString = "server=192.168.1.70;userid=root;pwd=201015;port=3306;database=stock;SslMode=none;";
          //string PostgreSQLConnection = "PORT=5432;DATABASE=stock;HOST=192.168.1.70;PASSWORD=201015;USER ID=postgres;";
          //string Connection = "Data Source = 192.168.1.70; Port=3306;User ID = root; Password=201015;Initial Catalog =stock; Charset=utf8;SslMode=none;Max pool size=10;";

            string MyConnection = $"Data Source ={DBConnectMessage.Host}; Port={DBConnectMessage.Port};User ID ={DBConnectMessage.UserName}; Password={DBConnectMessage.Password};AllowPublicKeyRetrieval=True;Charset=utf8;SslMode=None;Max pool size=10;";
            string MsConnection = $"server={DBConnectMessage.Host},{DBConnectMessage.Port};uid={DBConnectMessage.UserName};pwd={DBConnectMessage.Password};Encrypt=True;TrustServerCertificate=True;";
            string PgConnection = $"HOST={DBConnectMessage.Host};PORT={DBConnectMessage.Port};USER ID={DBConnectMessage.UserName};PASSWORD={DBConnectMessage.Password};";
            switch (dbType)
            {
                case DataBaseType.MySql:
                    ConnectionString = MyConnection;
                    break;
                case DataBaseType.SqlServer:
                    ConnectionString = MsConnection;
                    break;
                case DataBaseType.PostgreSQL:
                    ConnectionString = PgConnection;
                    break;
                case DataBaseType.Oracle:
                    break;
                default:
                    break;
            }
            if (parameter?.ToLower() == "test")
            {
                if (_dataBaseFactory.Test(ConnectionString, dbType))
                {
                    MessageBox.Show("Connection  Success");
                }
                else
                {
                    MessageBox.Show("Connection  Fail");
                }
            }
            else if (parameter?.ToLower() == "true")
            {
                if (_dataBaseFactory.Test(ConnectionString, dbType))
                {
                 TypeMessage = DBConnectMessage.ConnectionName;
                 result = ButtonResult.OK;
                 List<TreeNode> treeNodes = _dataBaseFactory.MakeSure(ConnectionString, dbType);
                 var parameters = new DialogParameters
                 {
                      { "ConnectionResult", Message },
                      { "ConnectionType", TypeMessage },
                      { "ConnectionTreeNodes", treeNodes }
                 };
                    RaiseRequestClose(new DialogResult(result) { Result = result, Parameters = parameters });
                }
                else
                {
                    MessageBox.Show("Connection  Fail");
                }
            } 
            else if (parameter?.ToLower() == "false")
            {
                 result = ButtonResult.Cancel;
                RaiseRequestClose(new DialogResult(result) { Result = result });
            }
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
             RequestClose.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("message");
            int result = 0;
            try
            {

                DBSQLComList = new ObservableCollection<TreeDataModels>() {
                  new TreeDataModels(){ Key=DataBaseType.MySql,Text= DataBaseType.MySql.ToString() },
                  new TreeDataModels(){ Key = DataBaseType.SqlServer, Text=DataBaseType.SqlServer.ToString() },
                  //new ComplexInfoModel(){ Key=DbType.Sqlite,Text=DbType.Sqlite.ToString() },
                  //new ComplexInfoModel(){ Key=DbType.Oracle,Text=DbType.Oracle.ToString() },
                  new TreeDataModels(){ Key=DataBaseType.PostgreSQL,Text=DataBaseType.PostgreSQL.ToString() }
                };
                combboxItem = DBSQLComList[0];  // 默认选中第一个
             
            }
            catch (Exception ex )
            {
                LogHelper.LogExceptionMessage(ex);
            }
            TypeMessage = result.ToString();
        }
    }
}
