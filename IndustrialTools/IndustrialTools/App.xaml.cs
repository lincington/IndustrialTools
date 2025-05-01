using Prism.Ioc;
using IndustrialTools.Views;
using System.Windows;
using Prism.Modularity;
using IndustrialTools.Services.Interfaces;
using IndustrialTools.Services;
using IndustrialTools.Modules.Menu;
using IndustrialTools.Modules.Content;
using IndustrialTools.Core;
using IndustrialTools.Modules.Content.Views;
using IndustrialTools.Modules.Content.ViewModels;

namespace IndustrialTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

            // Register other services and types here

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            containerRegistry.RegisterDialog<ConnectionDialog, ConnectionDialogViewModel>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MenuModule>();
            moduleCatalog.AddModule<ContentModule>();

        }
    }
}
