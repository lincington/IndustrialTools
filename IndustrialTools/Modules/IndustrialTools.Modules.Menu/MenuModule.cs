using IndustrialTools.Core;
using IndustrialTools.Modules.Menu.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

namespace IndustrialTools.Modules.Menu
{
    public class MenuModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MenuModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.MenuRegion, "Menu");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Menu>();
        }
    }
}