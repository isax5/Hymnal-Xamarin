using Hymnal.XF.ViewModels;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    /// <summary>
    /// Continues in <see cref="ThematicSubGroupPage"/>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class ThematicIndexPage : BaseContentPage<ThematicIndexViewModel>
    {
        public ThematicIndexPage()
        {
            InitializeComponent();
        }
    }
}
