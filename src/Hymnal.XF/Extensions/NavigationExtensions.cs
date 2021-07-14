using System.Threading.Tasks;
using Hymnal.XF.Models.Parameters;
using Prism.Navigation;

namespace Hymnal.XF.Extensions
{
    public static class NavigationExtensions
    {
        //public static Task<INavigationResult> NavigateAsync<TView>(
        //    this INavigationService navigationService)
        //    where TView : Page
        //{
        //    return navigationService.NavigateAsync(typeof(TView).Name);
        //}

        //public static Task<INavigationResult> NavigateAsync<TView, TViewModel, TParameter>(
        //    this INavigationService navigationService,
        //    TParameter parameter)
        //    where TView : BaseContentPage<TViewModel>
        //    where TViewModel : BaseViewModelParameter<TParameter>
        //    where TParameter : NavigationParameter
        //{
        //    return navigationService.NavigateAsync(typeof(TView).Name, parameter.AsParameter());
        //}

        //public static Task<INavigationResult> NavigateAsync<TView, TViewModel, TResult>(
        //    this INavigationService navigationService)
        //    where TView : BaseContentPage<TViewModel>
        //    where TViewModel : BaseViewModelResult<TResult>
        //    where TResult : NavigationResult
        //{
        //    navigationService.GoBackAsync()
        //    return navigationService.NavigateAsync(typeof(TView).Name).;
        //}

        //public static Task<INavigationResult> NavigateAsync<TView, TViewModel, TParameter>(
        //    this INavigationService navigationService,
        //    TParameter parameter)
        //    where TView : BaseContentPage<TViewModel>
        //    where TViewModel : class
        //    where TParameter : INavigationObject
        //{
        //    return navigationService.NavigateAsync(typeof(TView).Name, parameter.AsParameter());
        //}

        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, string name, INavigationObject parameter)
        {
            return navigationService.NavigateAsync(name, parameter.AsParameter());
        }
        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, string name, INavigationObject parameter, bool? useModalNavigation, bool animated)
        {
            return navigationService.NavigateAsync(name, parameter.AsParameter(), useModalNavigation, animated);
        }

        public static NavigationParameters AsParameter(this INavigationObject navigationParameter)
        {
            var parameters = new NavigationParameters()
            {
                { navigationParameter.GetKey(), navigationParameter }
            };
            return parameters;
        }
    }
}
