using Prism.Services;
using System;

namespace Hymnal.XF.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Report(this Exception exception)
        {
#if DEBUG
            var dialogService = App.Current.Container.Resolve(typeof(IPageDialogService)) as IPageDialogService;
            dialogService.DisplayAlertAsync("#Error", exception.Message, "Ok");
#elif RELEASE
#endif
        }
    }
}
