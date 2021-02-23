using Prism.DependentViews.Core;

using System.Windows.Controls.Ribbon;

namespace Prism.DependentViews.Module.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewBTab.xaml
    /// </summary>
    public partial class ViewBTab : RibbonTab, ISupportDataContext
    {
        public ViewBTab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(RibbonTab));
        }
    }
}