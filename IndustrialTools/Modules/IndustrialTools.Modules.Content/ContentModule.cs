using IndustrialTools.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation.Regions;

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
        }
    }  
}