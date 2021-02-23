using Prism.DependentViews.Core;

using System.Windows.Controls.Ribbon;

namespace Prism.DependentViews.Module.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewATab.xaml
    /// </summary>
    public partial class ViewATab : RibbonTab, ISupportDataContext
    {
        public ViewATab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(RibbonTab));
        }
    }
}