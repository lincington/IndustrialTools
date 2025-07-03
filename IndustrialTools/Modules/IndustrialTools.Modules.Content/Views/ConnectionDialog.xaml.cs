using System.Reflection;
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

        private void PbPwd_OnPasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var passwordtext = (PasswordBox)sender;
            SetPasswordBoxSelection(passwordtext, passwordtext.Password.Length + 1, passwordtext.Password.Length + 1);
        }

        private static void SetPasswordBoxSelection(PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select",
                BindingFlags.Instance | BindingFlags.NonPublic);

            select.Invoke(passwordBox, new object[] { start, length });
        }
    }
}
