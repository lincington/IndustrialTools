using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndustrialTools.Modules.Content.Views
{
    /// <summary>
    /// DBConnection.xaml 的交互逻辑
    /// </summary>
    public partial class DBConnection : UserControl
    {
        public DBConnection()
        {
            InitializeComponent();
        }

        private void GridSplitter_MouseEnter(object sender, MouseEventArgs e)
        {
            GridSplitter.Width = 5;
            GridSplitter.Background = new SolidColorBrush(Colors.Blue);
        }

        private void GridSplitter_MouseLeave(object sender, MouseEventArgs e)
        {
            GridSplitter.Width = 1;
            GridSplitter.Background = new SolidColorBrush(Colors.Gray);
        }

    }
}
