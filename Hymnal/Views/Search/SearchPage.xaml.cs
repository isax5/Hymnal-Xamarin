namespace Hymnal.Views;

public sealed partial class SearchPage : BaseContentPage<SearchViewModel>
{
	public SearchPage(SearchViewModel vm) : base(vm)
    {
		InitializeComponent();
	}
}
