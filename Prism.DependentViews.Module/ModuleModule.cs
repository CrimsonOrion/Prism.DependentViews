using Prism.DependentViews.Module.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Prism.DependentViews.Module
{
    public class ModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
        }
    }
}