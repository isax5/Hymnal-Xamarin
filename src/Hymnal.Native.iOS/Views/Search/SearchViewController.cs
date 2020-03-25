using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS
{
    [MvxFromStoryboard("Main")]
    public partial class SearchViewController : MvxViewController<SearchViewModel>
    {
        public SearchViewController (IntPtr handle) : base (handle)
        {
        }
    }
}