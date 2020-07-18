using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : MvxViewController<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public BaseViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // NavBar configuration
            NavigationController.NavigationBar.PrefersLargeTitles = true;
            //NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackTranslucent;
            NavigationController.NavigationBar.TintColor = UIColor.FromName("bar title color");
            //NavigationController.NavigationBar.LargeTitleTextAttributes = new UIStringAttributes
            //{
            //    ForegroundColor = UIColor.White
            //};
            //NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes
            //{
            //    ForegroundColor = UIColor.White
            //};
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            View.EndEditing(true);
        }
    }
}
