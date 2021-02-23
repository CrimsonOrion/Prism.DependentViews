using Prism.Regions;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Prism.DependentViews.Core
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        private readonly Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new();
        public const string BehaviorKey = "DependentViewRegionBehavior";
        protected override void OnAttach() => Region.ActiveViews.CollectionChanged += Views_CollectionChanged;

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {


                foreach (var view in e.NewItems)
                {
                    List<DependentViewInfo> viewList = new();

                    if (_dependentViewCache.ContainsKey(view))
                    {
                        viewList = _dependentViewCache[view];
                    }
                    else
                    {
                        foreach (var attr in GetCustomAttributes<DependentViewAttribute>(view.GetType()))
                        {
                            var info = CreateDependentView(attr);

                            if (info.View is ISupportDataContext ribbonTabContext && view is ISupportDataContext viewContext)
                                ribbonTabContext.DataContext = viewContext.DataContext;

                            viewList.Add(info);
                        }

                        _dependentViewCache.Add(view, viewList);
                    }

                    viewList.ForEach(_ => Region.RegionManager.Regions[_.TargetRegionName].Add(_.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        _dependentViewCache[oldView].ForEach(_ => Region.RegionManager.Regions[_.TargetRegionName].Remove(_.View));

                        if (!ShouldKeepAlive(oldView))
                            _dependentViewCache.Remove(oldView);
                    }

                }
            }
        }

        private static bool ShouldKeepAlive(object oldView)
        {
            var lifetime = GetItemOrContextLifetime(oldView);
            if (lifetime is not null)
                return lifetime.KeepAlive;

            var lifetimeAttribute = GetItemOrContextLifetimeAttribute(oldView);
            if (lifetimeAttribute is not null)
                return lifetimeAttribute.KeepAlive;

            return true;
        }

        private static RegionMemberLifetimeAttribute GetItemOrContextLifetimeAttribute(object oldView)
        {
            var lifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(oldView.GetType()).FirstOrDefault();
            if (lifetimeAttribute is not null)
                return lifetimeAttribute;

            var frameworkElement = oldView as FrameworkElement;
            if (frameworkElement is not null && frameworkElement.DataContext is not null)
            {
                var dataContext = frameworkElement.DataContext;
                var contextLifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(dataContext.GetType()).FirstOrDefault();
                return contextLifetimeAttribute;
            }

            return null;
        }

        private static IRegionMemberLifetime GetItemOrContextLifetime(object oldView)
        {
            var regionLifetime = oldView as IRegionMemberLifetime;
            if (regionLifetime is not null) return regionLifetime;

            var framework = oldView as FrameworkElement;
            if (framework is not null) return framework.DataContext as IRegionMemberLifetime;

            return null;
        }

        private static DependentViewInfo CreateDependentView(DependentViewAttribute attr)
        {
            DependentViewInfo info = new();

            info.TargetRegionName = attr.TargetRegionName;

            if (attr.Type is not null)
                info.View = Activator.CreateInstance(attr.Type);

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type) => type.GetCustomAttributes(typeof(T), true).OfType<T>();
    }

    internal class DependentViewInfo
    {
        public object View { get; set; }

        public string TargetRegionName { get; set; }
    }
}