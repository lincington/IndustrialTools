using Prism.Ioc;
using IndustrialTools.Views;
using System.Windows;
using Prism.Modularity;
using IndustrialTools.Modules.ModuleName;
using IndustrialTools.Services.Interfaces;
using IndustrialTools.Services;

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
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
