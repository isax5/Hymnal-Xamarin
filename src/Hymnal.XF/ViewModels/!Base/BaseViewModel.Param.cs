using Prism.Navigation;
using NavigationParameter = Hymnal.XF.Models.Parameters.NavigationParameter;

namespace Hymnal.XF.ViewModels
{
    public abstract class BaseViewModelParameter<TParameter> : BaseViewModel where TParameter : NavigationParameter
    {
        protected BaseViewModelParameter(INavigationService navigationService) : base(navigationService)
        { }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(NavigationParameter.Key, out TParameter value)
                || parameters.TryGetValue(KnownNavigationParameters.XamlParam, out value))
            {
                OnNavigatedTo(parameters, value);
            }
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters, TParameter parameter)
        { }
    }
}
