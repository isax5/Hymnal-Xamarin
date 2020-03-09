using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Favorites")]
    public partial class FavoritesViewController : MvxViewController<FavoritesViewModel>
    {
        public FavoritesViewController (IntPtr handle) : base (handle)
        {
        }
    }
}