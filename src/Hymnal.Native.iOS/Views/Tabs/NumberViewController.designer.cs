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
    [Register ("NumberViewController")]
    partial class NumberViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField hymnNumberTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton openHymnButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem recordsBarButtton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem searchBarButtton { get; set; }

        [Action ("OpenHymnButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OpenHymnButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (hymnNumberTextField != null) {
                hymnNumberTextField.Dispose ();
                hymnNumberTextField = null;
            }

            if (openHymnButton != null) {
                openHymnButton.Dispose ();
                openHymnButton = null;
            }

            if (recordsBarButtton != null) {
                recordsBarButtton.Dispose ();
                recordsBarButtton = null;
            }

            if (searchBarButtton != null) {
                searchBarButtton.Dispose ();
                searchBarButtton = null;
            }
        }
    }
}