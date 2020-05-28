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
    [Register ("NumberViewController")]
    partial class NumberViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OpenButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField Text { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (OpenButton != null) {
                OpenButton.Dispose ();
                OpenButton = null;
            }

            if (Text != null) {
                Text.Dispose ();
                Text = null;
            }
        }
    }
}