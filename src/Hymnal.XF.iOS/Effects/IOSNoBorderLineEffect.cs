using Hymnal.XF.Constants;
using Hymnal.XF.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName(EffectConstants.Group)]
[assembly: ExportEffect(typeof(IOSNoBorderLineEffect), EffectConstants.NoBorderLineEffect)]
namespace Hymnal.XF.iOS.Effects
{
    public class IOSNoBorderLineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is UITextField textField)
                textField.BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        { }
    }
}
