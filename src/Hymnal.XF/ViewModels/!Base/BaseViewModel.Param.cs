using System.Threading.Tasks;
using Prism.Navigation;
using NavigationParameter = Hymnal.XF.Models.Parameters.NavigationParameter;

namespace Hymnal.XF.ViewModels
{
    public abstract class BaseViewModelParameter<TParameter> : BaseViewModel where TParameter : NavigationParameter
    {
        private TParameter parameter;
        public TParameter Parameter
        {
            get => parameter;
            private set => SetProperty(ref parameter, value);
        }

        protected BaseViewModelParameter(INavigationService navigationService) : base(navigationService)
        { }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (resolveParam(parameters, out TParameter value))
            {
                Initialize(parameters, value);
            }
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);

            if (resolveParam(parameters, out TParameter value))
            {
                await InitializeAsync(parameters, value);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (resolveParam(parameters, out TParameter value))
            {
                OnNavigatedTo(parameters, value);
            }
        }

        public virtual void Initialize(INavigationParameters parameters, TParameter parameter)
        { }

        public virtual Task InitializeAsync(INavigationParameters parameters, TParameter parameter)
        { return Task.CompletedTask; }

        public virtual void OnNavigatedTo(INavigationParameters parameters, TParameter parameter)
        { }

        private bool resolveParam(INavigationParameters parameters, out TParameter value)
        {
            if (Parameter is not null)
            {
                value = Parameter;
                return true;
            }

            var returnValue = parameters.TryGetValue(NavigationParameter.Key, out value)
                || parameters.TryGetValue(KnownNavigationParameters.XamlParam, out value);

            Parameter = value;

            return returnValue;
        }
    }
}
