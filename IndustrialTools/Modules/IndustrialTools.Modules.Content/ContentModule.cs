using IndustrialTools.Core;
using IndustrialTools.Modules.Content.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;
using System.Windows.Controls;

namespace IndustrialTools.Modules.Content
{
    public class ContentModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ContentModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "Content");         
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Content>();

            containerRegistry.RegisterForNavigation<DBConnection>();
            containerRegistry.RegisterForNavigation<ModbusConnection>();
            containerRegistry.RegisterForNavigation<PLCConnection>();


            containerRegistry.RegisterForNavigation<VisionHalcon>();
            containerRegistry.RegisterForNavigation<VisionOpenCV2>();
            containerRegistry.RegisterForNavigation<VisionEmguCV>();

        }
    }  
}