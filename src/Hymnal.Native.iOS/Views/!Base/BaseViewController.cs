using Cirrious.FluentLayouts.Touch;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Hymnal.iOS.Styles;
using UIKit;
using System;
using Foundation;

namespace Hymnal.iOS.Views
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
            NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            NavigationController.NavigationBar.LargeTitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            View.EndEditing(true);
        }

        //public override void ViewDidLoad()
        //{
        //    base.ViewDidLoad();

        //    View.BackgroundColor = UIColor.White;

        //    NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
        //    NavigationController.NavigationBar.Translucent = false;
        //    NavigationController.NavigationBar.Hidden = false;
        //    NavigationController.NavigationBar.BarTintColor = ColorPalette.Primary;
        //    NavigationController.NavigationBar.TintColor = UIColor.White;

        //    NavigationController.SetNeedsStatusBarAppearanceUpdate();

        //    CreateView();

        //    LayoutView();

        //    BindView();
        //}

        //public override void ViewWillAppear(bool animated)
        //{
        //    base.ViewWillAppear(animated);

        //    View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        //}

        //protected virtual void CreateView()
        //{
        //}

        //protected virtual void LayoutView()
        //{
        //}

        //protected virtual void BindView()
        //{
        //}
    }
}
