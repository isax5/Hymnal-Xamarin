// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hymnal.Native.iOS.Views
{
	[Register ("HymnViewController")]
	partial class HymnViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem closeBarButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel hymnContentLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel hymnTitleLabel { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem openSheetBarButton { get; set; }
		
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

			if (closeBarButton != null) {
				closeBarButton.Dispose ();
				closeBarButton = null;
			}

			if (openSheetBarButton != null) {
				openSheetBarButton.Dispose ();
				openSheetBarButton = null;
			}
		}
	}
}
