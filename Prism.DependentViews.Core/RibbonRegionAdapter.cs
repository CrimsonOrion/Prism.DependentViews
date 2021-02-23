using Prism.Regions;

using System;
using System.Collections.Specialized;
using System.Windows.Controls.Ribbon;

namespace Prism.DependentViews.Core
{
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            if (regionTarget == null)
                throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                        AddViewToRegion(view, regionTarget);
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                        RemoveViewFromRegion(view, regionTarget);
                }
            };
        }

        protected override IRegion CreateRegion() => new SingleActiveRegion();

        private static void AddViewToRegion(object view, Ribbon ribbon)
        {
            var ribbonTab = view as RibbonTab;
            if (ribbonTab is not null)
                ribbon.Items.Add(ribbonTab);
        }

        private static void RemoveViewFromRegion(object view, Ribbon ribbon)
        {
            var ribbonTab = view as RibbonTab;
            if (ribbonTab is not null)
                ribbon.Items.Remove(ribbonTab);
        }
    }
}