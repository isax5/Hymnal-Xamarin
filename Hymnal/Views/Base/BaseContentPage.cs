namespace Hymnal.Views;

public class BaseContentPage<TViewModel> : BaseContentPage where TViewModel : class
{
    public TViewModel ViewModel
    {
        get => BindingContext as TViewModel;
        set => BindingContext = value;
    }


    public BaseContentPage(TViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    private bool pageLoaded;
    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (!pageLoaded && ViewModel is BaseViewModel viewModel)
        {
            pageLoaded = true;

            viewModel.Initialize();
            viewModel.InitializeAsync().ConfigureAwait(true);
        }
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        if (ViewModel is BaseViewModel viewModel)
            viewModel.OnNavigatedFrom(args);

        base.OnNavigatedFrom(args);
    }

    //protected override void OnChildAdded(Element child)
    //{
    //    base.OnChildAdded(child);

    //    if (!pageLoaded && ViewModel is BaseViewModel viewModel)
    //    {
    //        pageLoaded = true;

    //        viewModel.Initialize();
    //        viewModel.InitializeAsync().ConfigureAwait(true);
    //    }
    //}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (ViewModel is BaseViewModel viewModel)
            viewModel.OnNavigatedTo(args);

        base.OnNavigatedTo(args);
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        if (ViewModel is BaseViewModel viewModel)
            viewModel.OnNavigatingFrom(args);

        base.OnNavigatingFrom(args);
    }

    protected override void OnAppearing()
    {
        if (ViewModel is BaseViewModel viewModel)
            viewModel.OnAppearing();

        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        if (ViewModel is BaseViewModel viewModel)
            viewModel.OnDisappearing();

        base.OnDisappearing();
    }
}
