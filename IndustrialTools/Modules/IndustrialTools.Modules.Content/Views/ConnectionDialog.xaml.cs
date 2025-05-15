using System.Windows.Controls;

namespace IndustrialTools.Modules.Content.Views
{
    /// <summary>
    /// Interaction logic for NotificationDialog
    /// </summary>
    public partial class ConnectionDialog : UserControl
    {
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox; 

            tb.CaretIndex = tb.Text.Length;
        }
    }
}
