using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Tvos.Views;
using MvvmCross.ViewModels;
using System;
using UIKit;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    public partial class RootViewController : MvxTabBarViewController<RootViewModel>
    {
        public RootViewController(IntPtr handle) : base(handle)
        { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var viewControllers = new UIViewController[]
            {
                CreateTabFor(typeof(NumberViewModel)),
                CreateTabFor(typeof(SimpleViewModel))
            };

            ViewControllers = viewControllers;
        }

        private UIViewController CreateTabFor(Type viewModelType, bool useNavigationPage = false, string title = null, int index = 0, string imageName = null)
        {
            var request = new MvxViewModelRequest(viewModelType);

            var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);

            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

            // use the title of the page created
            if (!string.IsNullOrEmpty(title))
            {
                screen.Title = title;
            }

            if (!string.IsNullOrEmpty(imageName))
            {
                // use preferences of the TabBarItem used in the StoryBoard
                screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle(imageName), index);
                //screen.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, index);
                //screen.TabBarItem = new UITabBarItem();
            }

            if (useNavigationPage)
            {
                var controller = new UINavigationController();
                controller.PushViewController(screen, true);
                return controller;
            }
            else
            {
                return screen;
            }

        }
    }
}