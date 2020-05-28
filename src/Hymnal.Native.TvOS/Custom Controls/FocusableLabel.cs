using System;
using System.ComponentModel;
using Foundation;
using UIKit;

namespace Hymnal.Native.TvOS.CustomControls
{
    [Register("FocusableLabel"), DesignTimeVisible(true)]
    public class FocusableLabel: UILabel
    {
        public FocusableLabel(IntPtr p) : base(p)
        {
            UserInteractionEnabled = true;
        }

        public override bool CanBecomeFocused => true;

        public override void DidUpdateFocus(UIFocusUpdateContext context, UIFocusAnimationCoordinator coordinator)
        {
            base.DidUpdateFocus(context, coordinator);

            if (context.NextFocusedView == this)
            {
                BackgroundColor = UIColor.LightGray;
            }
            else if (context.PreviouslyFocusedView == this)
            {
                BackgroundColor = UIColor.Clear;
            }
        }
    }
}
