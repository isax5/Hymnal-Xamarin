using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SearchViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;

        public MvxObservableCollection<Hymn> Hymns { get; set; } = new MvxObservableCollection<Hymn>();
        public Hymn SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedHymnExecute(value);
                RaisePropertyChanged(nameof(SelectedHymn));
            }
        }

        private string textSearchBar;
        public string TextSearchBar
        {
            get => textSearchBar;
            set
            {
                SetProperty(ref textSearchBar, value);
                TextSearchExecuteAsync(value);
            }
        }


        public SearchViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            Hymns.AddRange((await hymnsService.GetHymnListAsync()).OrderByNumber());
        }


        private async void TextSearchExecuteAsync(string text)
        {
            Hymns.Clear();
            IEnumerable<Hymn> hymns = (await hymnsService.GetHymnListAsync()).OrderByNumber();

            if (string.IsNullOrWhiteSpace(text))
            {
                Hymns.AddRange(hymns);
                return;
            }

            Hymns.AddRange(hymns.SearchQuery(text));
        }

        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = hymn.Number });
        }
    }
}
