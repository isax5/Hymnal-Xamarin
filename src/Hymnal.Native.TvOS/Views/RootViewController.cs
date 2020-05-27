using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using MvvmCross.ViewModels;
using System;
using UIKit;

namespace Hymnal.Native.TvOS
{

    //[MvxFromStoryboard("Main")]
    //[MvxRootPresentation]
    //[MvxTabPresentation]
    //[MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true, WrapInNavigationPage = true)]
    public partial class RootViewController : MvxTabBarViewController<RootViewModel>
    {
        private bool _constructed;

        public RootViewController()
        {
            _constructed = true;

            // need this additional call to ViewDidLoad because UIkit creates the view before the C# hierarchy has been constructed
            ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            if (!_constructed)
                return;

            base.ViewDidLoad();

            var viewControllers = new UIViewController[]
            {
            CreateTabFor(0, typeof(NumberViewModel)),
            CreateTabFor(1, typeof(SimpleViewModel)),
            //CreateTabFor(0, "First", "FirstImage", typeof(SimpleViewController)),
            //CreateTabFor(1, "Second", "SecondImage", typeof(SecondViewModel)),
            //CreateTabFor(2, "Third", "ThirdImage", typeof(ThirdViewModel))
            };

            //var vm1 = Mvx.IoCProvider

            ViewControllers = viewControllers;
            //CustomizableViewControllers = new UIViewController[] { };

            //Sometimes I need to start with a specific tab selected
            //SelectedViewController = ViewControllers[ViewModel.CurrentPage];
        }

        private UIViewController CreateTabFor(int index, Type viewModelType, string title = null, string imageName = null)
        {
            var controller = new UINavigationController();
            var request = new MvxViewModelRequest(viewModelType);

            //var viewModel = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);

            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

            // use the title of the page created
            if (!string.IsNullOrEmpty(title))
            {
                screen.Title = title;
            }


            // use preferences of the TabBarItem used in the StoryBoard
            //screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle(imageName), index);
            //screen.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, index);
            //screen.TabBarItem = new UITabBarItem();

            controller.PushViewController(screen, true);
            return controller;
        }
    }
}