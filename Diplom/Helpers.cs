using Prism.Regions;

namespace Diplom
{
    static class RegionManagerHelper
    {
        public static void RequestNavigateInRootRegion(this IRegionManager regionManager, string view, NavigationParameters parameters = null)
        {
            regionManager.RequestNavigate(RegionNames.RootRegion, view, parameters);
        }
    }
}
