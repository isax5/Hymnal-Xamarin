// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Hymnal.Native.TvOS
{
    [Register ("HymnViewController")]
    partial class HymnViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel contentLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel numberLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView testTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Hymnal.Native.TvOS.CustomControls.FocusableLabel textFocusableLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel titleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (contentLabel != null) {
                contentLabel.Dispose ();
                contentLabel = null;
            }

            if (numberLabel != null) {
                numberLabel.Dispose ();
                numberLabel = null;
            }

            if (testTextView != null) {
                testTextView.Dispose ();
                testTextView = null;
            }

            if (textFocusableLabel != null) {
                textFocusableLabel.Dispose ();
                textFocusableLabel = null;
            }

            if (titleLabel != null) {
                titleLabel.Dispose ();
                titleLabel = null;
            }
        }
    }
}