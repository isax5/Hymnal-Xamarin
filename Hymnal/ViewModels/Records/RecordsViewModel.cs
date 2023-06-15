using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;
using Hymnal.Models.DataBase;

namespace Hymnal.ViewModels
{
    public sealed partial class RecordsViewModel : BaseViewModel
    {
        private readonly HymnsService hymnsService;
        private readonly DatabaseService databaseService;


        public ObservableRangeCollection<Tuple<Hymn, RecordHymn>> Hymns { get; } = new();


        public RecordsViewModel(HymnsService hymnsService, DatabaseService databaseService)
        {
            this.hymnsService = hymnsService;
            this.databaseService = databaseService;
        }

        public override void Initialize()
        {
            base.Initialize();

            LoadData();
        }

        private void LoadData()
        {
            databaseService.GetTable<RecordHymn>()
                .ToListAsync()
                .ToObservable()
                .SubscribeOn(new NewThreadScheduler())
                .Subscribe(async hymnReferences =>
                {
                    try
                    {
                        var values = GetHymnsAsync(hymnReferences.OrderBy(r => r.SavedAt));

                        MainThread.BeginInvokeOnMainThread(() => Hymns.Clear());
                        await foreach (var value in values)
                            MainThread.BeginInvokeOnMainThread(() => Hymns.Add(value));

                        async IAsyncEnumerable<Tuple<Hymn, RecordHymn>> GetHymnsAsync(IEnumerable<RecordHymn> hymnReferences)
                        {
                            foreach (var hymnReference in hymnReferences)
                                yield return new Tuple<Hymn, RecordHymn>(await hymnsService.GetHymnAsync(hymnReference), hymnReference);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Report();
                    }
                }, error => error.Report());
        }

        [RelayCommand]
        private async void OpenHymnAsync(Tuple<Hymn, RecordHymn> hymn)
        {
            if (hymn is null)
                return;

            await Shell.Current.GoToAsync(nameof(HymnPage),
                new HymnIdParameter()
                {
                    Number = hymn.Item1.Number,
                    SaveInRecords = false,
                    HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item1.HymnalLanguageId),
                }.AsParameter());
        }
    }
}
