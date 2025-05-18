using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;

using SqlSugar;
using System;
using Microsoft.IdentityModel.Logging;
using System.Windows;
using IndustrialTools.Common.Models;
using System.Collections.ObjectModel;


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



        private ComplexInfoModel combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { SetProperty(ref combboxItem, value); }
         }

        private ObservableCollection<ComplexInfoModel> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public ObservableCollection<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { SetProperty(ref combboxList, value); }
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


        protected virtual void ComboboxSelectionChangedDialog(object parameter)
        {
            ComplexInfoModel dd = parameter  as ComplexInfoModel;
            DbType dbType = dd.Key;
            switch (dbType)
            {
                case DbType.MySql:
                    DBConnectMessage.Port = 3306;
                    DBConnectMessage.UserName = "root";

                    break;
                case DbType.SqlServer:
                    DBConnectMessage.Port = 1433;
                    DBConnectMessage.UserName = "sa";

                    break;
                case DbType.PostgreSQL:
                    DBConnectMessage.Port = 5432;
                    DBConnectMessage.UserName = "postgres";

                    break;
                case DbType.Oracle:
                    DBConnectMessage.Port = 1521;
                     break;
                default:
                    break;
            }
        }

        protected virtual void CloseDialog(string parameter)
        {
             ButtonResult result = ButtonResult.None;
            string MyConnection = $"Data Source ={DBConnectMessage.Host}; Port={DBConnectMessage.Port};User ID ={DBConnectMessage.UserName}; Password={DBConnectMessage.Password};AllowPublicKeyRetrieval=True;Charset=utf8;SslMode=None;Max pool size=10";
            string MsConnection = $"server={DBConnectMessage.Host},{DBConnectMessage.Port};uid={DBConnectMessage.UserName};pwd={DBConnectMessage.Password};Encrypt=True;TrustServerCertificate=True;";
            string PgConnection = $"HOST={DBConnectMessage.Host};PORT={DBConnectMessage.Port};USER ID={DBConnectMessage.UserName};PASSWORD={DBConnectMessage.Password}";

            if (parameter?.ToLower() == "test")
            {
                MessageBox.Show("Connection  Fail");
                
            }
            else if (parameter?.ToLower() == "true")
            {
                TypeMessage = DBConnectMessage.ConnectionName ;
                result = ButtonResult.OK;
                 var parameters = new DialogParameters
                 {
                       { "ConnectionResult", Message },
                       { "ConnectionType", TypeMessage }
                 };
                 RaiseRequestClose(new DialogResult(result) {  Result= result  , Parameters = parameters });
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
 
            int result = 0;
            try
            {

                CombboxList = new ObservableCollection<ComplexInfoModel>() {
                  new ComplexInfoModel(){ Key=DbType.MySql,Text= DbType.MySql.ToString() },
                  new ComplexInfoModel(){ Key = DbType.SqlServer, Text=DbType.SqlServer.ToString() },
                  //new ComplexInfoModel(){ Key=DbType.Sqlite,Text=DbType.Sqlite.ToString() },
                  //new ComplexInfoModel(){ Key=DbType.Oracle,Text=DbType.Oracle.ToString() },
                  new ComplexInfoModel(){ Key=DbType.PostgreSQL,Text=DbType.PostgreSQL.ToString() }

                };
                combboxItem = CombboxList[0];  // 默认选中第一个
                //result = SugarDbFactory.New(MyConnection, DbType.MySql).Ado.ExecuteCommand("SELECT 1");
                //result += SugarDbFactory.New(MsConnection, DbType.SqlServer).Ado.ExecuteCommand("SELECT 2");
                //result += SugarDbFactory.New(PgConnection, DbType.PostgreSQL).Ado.ExecuteCommand("SELECT 3");
                ////result += SugarDbFactory.New(OrConnection, DbType.OceanBase).Ado.ExecuteCommand("SELECT 4");
            }
            catch (Exception ex )
            {
                LogHelper.LogExceptionMessage(ex);
            }
            TypeMessage = result.ToString();
        }
    }
}
