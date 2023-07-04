using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Hymnal.Controls
{
    public sealed class SearchHymnHandler : SearchHandler
    {
        public static readonly BindableProperty HymnsProperty =
            BindableProperty.Create(nameof(Hymns), typeof(IEnumerable<Hymn>), typeof(SearchHymnHandler), null);

        public IEnumerable<Hymn> Hymns
        {
            get => (IEnumerable<Hymn>)GetValue(HymnsProperty);
            set => SetValue(HymnsProperty, value);
        }

        private readonly Subject<string> observableTextSearchBar = new();


        public SearchHymnHandler()
        {
            observableTextSearchBar
                .Throttle(TimeSpan.FromSeconds(0.3))
                .DistinctUntilChanged()
                .Subscribe(text => TextSearchExecute(text));
        }

        ~SearchHymnHandler()
        {
            observableTextSearchBar.Dispose();
        }


        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            observableTextSearchBar.OnNext(newValue);
        }

        private void TextSearchExecute(string text)
        {
            if (Hymns is null)
            {
                MainThread.InvokeOnMainThreadAsync(() => ItemsSource = null);
                return;
            }

            var result = string.IsNullOrWhiteSpace(text) ? null : Hymns.SearchQuery(text).Take(20);

            MainThread.InvokeOnMainThreadAsync(() => ItemsSource = result);
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            if (item is not Hymn hymn)
                return;

            // Let the animation complete https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/search#create-a-searchhandler
            await Task.Delay(500);

            Console.WriteLine($"Selected: {hymn}");

            await Shell.Current.GoToAsync(nameof(HymnPage),
                new HymnIdParameter()
                {
                    Number = hymn.Number,
                    SaveInRecords = true,
                    HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.HymnalLanguageId),
                }.AsParameter());
        }
    }
}
