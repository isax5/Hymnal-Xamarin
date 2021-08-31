using Prism.Navigation;
using NavigationParameter = Hymnal.XF.Models.Parameters.NavigationParameter;
using NavigationResult = Hymnal.XF.Models.Parameters.NavigationResult;

namespace Hymnal.XF.ViewModels
{
    public abstract class BaseViewModelParameterAndResult<TParameter, TResult> : BaseViewModel where TParameter : NavigationParameter where TResult : NavigationResult
    {
        protected BaseViewModelParameterAndResult(INavigationService navigationService) : base(navigationService)
        { }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(NavigationParameter.Key, out TParameter parameter)
                || parameters.TryGetValue(KnownNavigationParameters.XamlParam, out parameter))
            {
                OnNavigatedTo(parameters, parameter);
            }

            if (parameters.TryGetValue(NavigationResult.Key, out TResult result)
                || parameters.TryGetValue(KnownNavigationParameters.XamlParam, out result))
            {
                OnNavigatedTo(parameters, result);
            }
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters, TParameter result)
        { }

        public virtual void OnNavigatedTo(INavigationParameters parameters, TResult result)
        { }
    }
}
