using System;
using Prism.Services;
using Xamarin.Essentials;

namespace Hymnal.XF.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Report(this Exception exception)
        {
#if DEBUG
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var dialogService = App.Current.Container.Resolve(typeof(IPageDialogService)) as IPageDialogService;
                dialogService.DisplayAlertAsync("#Error", exception.Message, "Ok");
            });
#elif RELEASE
#endif
        }
    }
}
