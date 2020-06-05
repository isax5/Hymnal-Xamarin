using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Settings", TabIconName = "settings tab")]
    public partial class SettingsViewController : BaseViewController<SettingsViewModel>
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}