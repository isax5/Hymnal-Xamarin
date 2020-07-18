using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Settings")]
    public partial class SettingsViewController : MvxViewController<SettingsViewModel>
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}