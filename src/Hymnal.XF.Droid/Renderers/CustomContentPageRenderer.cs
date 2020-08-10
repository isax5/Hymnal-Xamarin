using Android.Content;
using Hymnal.XF.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace Hymnal.XF.Droid.Renderers
{
    /// <summary>
    /// Custom renderer for ContentPage that allows for action buttons to be on the left or the right hand side (ex: a modal with cancel and done buttons)
    /// </summary>
    public class CustomContentPageRenderer : PageRenderer
    {
        public CustomContentPageRenderer(Context context) : base(context)
        {
        }

    }
}
