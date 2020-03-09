using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Settings")]
    public partial class SettingsViewController : MvxViewController<SettingsViewModel>
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}