using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Crashes;
using Prism.Services;
using Xamarin.Essentials;

namespace Hymnal.XF.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Report(this Exception exception, Dictionary<string, string> properties = null)
        {
#if DEBUG
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var dialogService = App.Current.Container.Resolve(typeof(IPageDialogService)) as IPageDialogService;
                dialogService.DisplayAlertAsync("#Error", exception.Message, "Ok");

                if (properties is not null)
                    Crashes.TrackError(exception, properties);
            });
#elif RELEASE
#endif
        }
    }
}
