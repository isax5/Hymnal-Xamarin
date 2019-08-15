using System;
using Hymnal.Core.Models.Parameter;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : MvxViewModel<HymnId>
    {
        private HymnId hymnId;
        public HymnId HymnId
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }

        private readonly IMvxNavigationService navigationService;

        public HymnViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Prepare(HymnId parameter)
        {
            HymnId = parameter;
        }



        #region Commands
        public MvxCommand OpenSheetCommand => new MvxCommand(OpenSheet);
        private void OpenSheet()
        {
            navigationService.Navigate<MusicSheetViewModel, HymnId>(HymnId);
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion
    }
}
