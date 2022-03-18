using Prism.Navigation;
using NavigationResult = Hymnal.XF.Models.Parameters.NavigationResult;

namespace Hymnal.XF.ViewModels
{
    public abstract partial class BaseViewModelResult<TResult> : BaseViewModel where TResult : NavigationResult
    {
        protected BaseViewModelResult(INavigationService navigationService) : base(navigationService)
        { }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(NavigationResult.Key, out TResult value)
                || parameters.TryGetValue(KnownNavigationParameters.XamlParam, out value))
            {
                OnNavigatedTo(parameters, value);
            }
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters, TResult result)
        { }
    }
}
