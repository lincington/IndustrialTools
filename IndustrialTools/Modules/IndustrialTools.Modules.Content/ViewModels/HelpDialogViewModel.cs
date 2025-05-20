using Prism.Dialogs;
using Prism.Mvvm;
using System;
 

namespace IndustrialTools.Modules.Content.ViewModels
{
    public class HelpDialogViewModel : BindableBase, IDialogAware
    {

        private string _title = "Connection";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog()
        {
          return true;
        }

        public void OnDialogClosed()
        { 
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
          
        }
    }
}
