using System.Collections.ObjectModel;

namespace IndustrialTools.Common.Models
{

    public enum DataBaseType
    {
        MySql,
        SqlServer,
        Oracle,
        PostgreSQL,
        SQLite,
        Access,
        MongoDB,
        Redis
    }
    /// <summary>
    /// 
    /// </summary>
    public class TreeNode
    {
        public string FullName  { get; set; }
        public string Name { get; set; }
        public ObservableCollection<TreeNode> Children { get; set; }

        public TreeNode(string name)
        {
            Name = name;
            FullName = name;
            Children = new ObservableCollection<TreeNode>();   
        }

        public TreeNode(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            Children = new ObservableCollection<TreeNode>();
        }
    }
    /// <summary>
    /// 复杂信息模型  
    /// </summary>
    public class TreeDataModels  : BindableBase
    {
        private DataBaseType key = DataBaseType.MySql;
        /// <summary>
        /// Key值
        /// </summary>
        public DataBaseType Key
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


    /// <summary>
    /// 菜单项模型
    /// </summary>
    public class MenuItemModel
    {
        public string Header { get; set; }= "";
        public CompositeCommand  Command { get; set; } = new CompositeCommand();
        public object CommandParameter { get; set; } =  new object ();    
        public ObservableCollection<MenuItemModel> Children { get; set; } = new ObservableCollection<MenuItemModel>();
    }

}
