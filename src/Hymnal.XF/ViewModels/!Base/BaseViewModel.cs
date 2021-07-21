using System.Threading.Tasks;
using Hymnal.XF.Views;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public abstract class BaseViewModel : BindableBase, IInitialize, IInitializeAsync, INavigationAware, IDestructible, IPageLifecycleAware
    {
        protected INavigationService NavigationService { get; private set; }

#if DEBUG
        public DelegateCommand SampleCommand { get; private set; }
#endif

        private bool busy = false;
        public bool Busy
        {
            get => busy;
            set
            {
                SetProperty(ref busy, value);
                RaisePropertyChanged(nameof(NotBusy));
            }
        }

        public bool NotBusy => !busy;

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string tabName;
        public string TabName
        {
            get
            {
                if (string.IsNullOrEmpty(tabName))
                    return GetType().Name;

                return tabName;
            }
            set => SetProperty(ref tabName, value);
        }

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

#if DEBUG
            SampleCommand = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync(nameof(SimplePage));
            }).ObservesCanExecute(() => NotBusy);
#endif
        }


        public virtual void Initialize(INavigationParameters parameters)
        { }

        public virtual async Task InitializeAsync(INavigationParameters parameters)
        { }

        /// <summary>
        /// When navigate from this VM
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        { }

        /// <summary>
        /// When navigate to this VM
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        { }

        /// <summary>
        /// Destroing
        /// </summary>
        public virtual void Destroy()
        { }

        /// <summary>
        /// On Appearing
        /// </summary>
        public virtual void OnAppearing()
        { }

        /// <summary>
        /// On Disappearing
        /// </summary>
        public virtual void OnDisappearing()
        { }
    }
}
