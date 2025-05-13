using Prism.Commands;
using Prism.Mvvm;
using Prism.Dialogs;
using IndustrialTools.Common;
using SqlSugar;
using System;
using Microsoft.IdentityModel.Logging;
using System.Windows;
using IndustrialTools.Common.Models;

namespace IndustrialTools.Modules.Content.ViewModels 
{
    public class ConnectionDialogViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        private string _message= "DBConnection";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private DBConnection _DBConnectmessage;
        public DBConnection DBConnectMessage
        {
            get { return _DBConnectmessage; }
            set { SetProperty(ref _DBConnectmessage, value); }
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


        protected virtual void CloseDialog(string parameter)
        {
             ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "test")
            {
                MessageBox.Show("dsf");
            }
            else if (parameter?.ToLower() == "true")
            {
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
            string MyConnection = "Data Source =192.168.1.70; Port=3306;User ID =root; Password=201015;Initial Catalog =stock;AllowPublicKeyRetrieval=True;Charset=utf8;SslMode=None;Max pool size=10";
            string MsConnection = "server=192.168.1.70,1433;uid=sa;pwd=Zhouenlai@305;database=stock;Encrypt=True;TrustServerCertificate=True;";
            string PgConnection = "HOST=192.168.1.70;PORT=5432;USER ID=postgres;PASSWORD=201015;DATABASE=stock";
            //string OrConnection = "Data Source=192.168.1.70/orcl;User ID=system;Password=haha";

            int result = 0;
            try
            {
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
