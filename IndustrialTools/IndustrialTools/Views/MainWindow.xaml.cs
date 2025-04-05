using System.Windows;
using System.Windows.Input;

namespace IndustrialTools.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {          
            InitializeComponent();  

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {        
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            SystemCommands.MaximizeWindow(this);

        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 检查鼠标左键是否按下
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // 开始拖动窗口
                this.DragMove();
            }
        }
    }
}
