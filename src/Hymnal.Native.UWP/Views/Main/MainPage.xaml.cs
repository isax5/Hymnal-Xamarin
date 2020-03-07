using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Hymnal.Core.ViewModels.Main;

namespace Hymnal.UWP
{
    [MvxPagePresentation]
    [MvxViewFor(typeof(MainViewModel))]
    public sealed partial class MainPage : MvxWindowsPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
