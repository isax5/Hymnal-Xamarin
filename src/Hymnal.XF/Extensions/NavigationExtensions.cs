using System.Threading.Tasks;
using Hymnal.XF.Models.Parameters;
using Prism.Navigation;

namespace Hymnal.XF.Extensions
{
    public static class NavigationExtensions
    {
        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, string name, INavigationObject parameter)
        {
            return navigationService.NavigateAsync(name, parameter.AsParameter());
        }

        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, string name, INavigationObject parameter, bool? useModalNavigation, bool animated)
        {
            return navigationService.NavigateAsync(name, parameter.AsParameter(), useModalNavigation, animated);
        }

        public static Task<INavigationResult> NavigateAsync(this INavigationService navigationService, string name, bool? useModalNavigation, bool animated)
        {
            return navigationService.NavigateAsync(name, null, useModalNavigation, animated);
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
