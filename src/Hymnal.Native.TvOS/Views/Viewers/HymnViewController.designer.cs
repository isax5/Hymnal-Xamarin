// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hymnal.Native.TvOS.Views
{
	[Register ("HymnViewController")]
	partial class HymnViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UICollectionView listCollectionView { get; set; }

		[Outlet]
		UIKit.UINavigationItem navigationItem { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem playButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (navigationItem != null) {
				navigationItem.Dispose ();
				navigationItem = null;
			}

			if (listCollectionView != null) {
				listCollectionView.Dispose ();
				listCollectionView = null;
			}

			if (playButton != null) {
				playButton.Dispose ();
				playButton = null;
			}
		}
	}
}
