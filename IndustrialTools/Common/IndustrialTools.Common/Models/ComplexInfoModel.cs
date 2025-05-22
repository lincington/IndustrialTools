using SqlSugar;
using System.Collections.ObjectModel;

namespace IndustrialTools.Common.Models
{
    public class ComplexInfoModel : BindableBase
    {
        private DbType key = DbType.MySql;
        /// <summary>
        /// Key值
        /// </summary>
        public DbType Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private string text="";
        /// <summary>
        /// Text值
        /// </summary>
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }

    public class MenuItemModel
    {
        public string Header { get; set; }= "";

        public CompositeCommand  Command { get; set; } = new CompositeCommand();

        public object CommandParameter { get; set; } =  new object ();    
        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();
    }


}
