using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;
using System.Windows;

namespace IndustrialTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #region ============================================== top =============================================================
        public DelegateCommand<object> MinimizeCommand { get; set; }
        public DelegateCommand<object> MaximizeCommand { get; set; }
        public DelegateCommand<object> CloseCommand { get; set; }
        public DelegateCommand HelpCommand { get; set; }
        private void CloseWindow(object obj)
        {
            SystemCommands.CloseWindow(obj as Window);
        }
        private void MaxiMizeWindow(object obj)
        {
            Window? mainWindow = obj as Window;
            if (mainWindow != null)
            {
                switch (mainWindow.WindowState)
                {
                    case WindowState.Normal:
                        SystemCommands.MaximizeWindow(mainWindow);
                        break;
                    case WindowState.Minimized:
                        break;
                    case WindowState.Maximized:
                        SystemCommands.RestoreWindow(mainWindow);
                        break;
                    default:
                        break;
                }
            }
        }
        private void MiniMizeWindow(object obj)
        {
            SystemCommands.MinimizeWindow(obj as Window);
        }
        private void ShowSetingsDialog()
        {
           
        }
        private void ShowHelpDialog()
        {
           
        }


        private void LoginFrom()
        {
            
        }


        public DelegateCommand _SetingsCommand;
        public DelegateCommand SetingsCommand
        {
            get => _SetingsCommand;
            set => SetProperty(ref _SetingsCommand, value);
        }


        public DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand
        {
            get => _LoginCommand;
            set => SetProperty(ref _LoginCommand, value);
        }

        #endregion


        public MainWindowViewModel()
        {
            MinimizeCommand = new DelegateCommand<object>(MiniMizeWindow);
            MaximizeCommand = new DelegateCommand<object>(MaxiMizeWindow);
            CloseCommand = new DelegateCommand<object>(CloseWindow);
        }
    }
}
