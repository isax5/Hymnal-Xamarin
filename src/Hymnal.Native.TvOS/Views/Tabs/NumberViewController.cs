using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxPagePresentation(WrapInNavigationController = true)]
    public partial class NumberViewController : MvxViewController<NumberViewModel>
    {
        public NumberViewController (IntPtr handle) : base (handle)
        { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Bindings
            MvxFluentBindingDescriptionSet<NumberViewController, NumberViewModel> set = this.CreateBindingSet<NumberViewController, NumberViewModel>();
            set.Bind(hymnNumberTextField).To(vm => vm.HymnNumber);
            // TODO: Correction MVX
            //set.Bind(openHymnButton).To(vm => vm.OpenHymnCommand);
            set.Apply();
        }

        // TODO: Correction MVX
        partial void openHymnActionButton(UIButton sender)
        {
            openHymnExecute();
        }

        // TODO: Correction MVX
        private async void openHymnExecute()
        {
            var num = hymnNumberTextField.Text ?? ViewModel.HymnNumber;

            HymnalLanguage language = ViewModel.PreferencesService.ConfiguratedHymnalLanguage;
            IEnumerable<Hymn> hymns = await ViewModel.HymnService.GetHymnListAsync(language);

            if (int.TryParse(num, out var number))
            {
                if (number < 0 || number > hymns.Count())
                    return;

                var parameter = new HymnIdParameter
                {
                    Number = number,
                    HymnalLanguage = language
                };


                var request = new MvxViewModelRequest(typeof(HymnViewModel));
                //IMvxViewModel viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
                IMvxViewModel viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, parameter, null);
                var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

                var controller = new UINavigationController();
                controller.PushViewController(screen, true);

                //NavigationController.PushViewController(controller, true);
                PresentViewController(controller, true, null);
            }
        }
    }
}