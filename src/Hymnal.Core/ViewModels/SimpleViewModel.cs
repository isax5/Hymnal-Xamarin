using System;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SimpleViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public SimpleViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public MvxCommand ClickmeCommand => new MvxCommand(ClickmeExecute);

        private void ClickmeExecute()
        {
            _navigationService.Navigate<SimpleViewModel>();
        }
    }
}
