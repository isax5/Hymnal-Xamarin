using Microsoft.Maui.Controls.Platform;

namespace Hymnal.Effects;

internal class NoBorderLineEffect: RoutingEffect
{ }

#if IOS
internal class NoBorderLinePlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (Control is UIKit.UITextField textField)
            textField.BorderStyle = UIKit.UITextBorderStyle.None;
    }

    protected override void OnDetached() { }
}
#elif ANDROID
internal class NoBorderLinePlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (Control is Android.Views.View v)
            v.Background = null;

    }

    protected override void OnDetached() { }
}
#else
internal class NoBorderLinePlatformEffect : PlatformEffect
{
    protected override void OnAttached() { }
    protected override void OnDetached() { }
}
#endif
