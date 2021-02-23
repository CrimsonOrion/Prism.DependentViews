using Prism.DependentViews.Core;
using Prism.DependentViews.Module.RibbonTabs;

using System.Windows.Controls;

namespace Prism.DependentViews.Module.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    [DependentView(typeof(ViewATab), "RibbonTabRegion")]
    public partial class ViewA : UserControl, ISupportDataContext
    {
        public ViewA() => InitializeComponent();
    }
}