using Prism.DependentViews.Core;
using Prism.DependentViews.Module.RibbonTabs;
using Prism.Regions;

using System.Windows.Controls;

namespace Prism.DependentViews.Module.Views
{
    /// <summary>
    /// Interaction logic for ViewB.xaml
    /// </summary>
    [DependentView(typeof(ViewBTab), "RibbonTabRegion")]
    [DependentView(typeof(ViewC), "SubRegion")]
    public partial class ViewB : UserControl, ISupportDataContext, IRegionMemberLifetime
    {
        public ViewB() => InitializeComponent();

        public bool KeepAlive { get; } = false;
    }
}