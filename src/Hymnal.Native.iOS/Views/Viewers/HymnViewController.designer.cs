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

namespace Hymnal.iOS.Views
{
    [Register ("HymnViewController")]
    partial class HymnViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel hymnContentLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel hymnTitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (hymnContentLabel != null) {
                hymnContentLabel.Dispose ();
                hymnContentLabel = null;
            }

            if (hymnTitleLabel != null) {
                hymnTitleLabel.Dispose ();
                hymnTitleLabel = null;
            }
        }
    }
}