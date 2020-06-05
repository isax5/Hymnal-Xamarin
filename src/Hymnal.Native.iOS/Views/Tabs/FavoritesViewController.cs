using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Favorites", TabIconName = "tab favorites")]
    public partial class FavoritesViewController : BaseViewController<FavoritesViewModel>
    {
        public FavoritesViewController (IntPtr handle) : base (handle)
        {
        }
    }
}