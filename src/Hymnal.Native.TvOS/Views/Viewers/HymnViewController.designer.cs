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
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel numberLabel { get; set; }

		[Outlet]
		UIKit.UIButton playButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (listCollectionView != null) {
				listCollectionView.Dispose ();
				listCollectionView = null;
			}

			if (numberLabel != null) {
				numberLabel.Dispose ();
				numberLabel = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (playButton != null) {
				playButton.Dispose ();
				playButton = null;
			}
		}
	}
}
