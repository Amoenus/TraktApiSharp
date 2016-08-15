﻿namespace TraktApiSharp.Example.UWP.ViewModels.Shows
{
    using Models.Shows;
    using Requests.Params;
    using Services.TraktService;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Views;
    using Windows.UI.Xaml.Navigation;

    public class TrendingShowsPageViewModel : PaginationViewModel
    {
        private TraktShowsService Movies { get; } = TraktShowsService.Instance;

        private ObservableCollection<TrendingShow> _trendingShows = new ObservableCollection<TrendingShow>();

        public ObservableCollection<TrendingShow> TrendingShows
        {
            get { return _trendingShows; }

            set
            {
                _trendingShows = value;
                base.RaisePropertyChanged();
            }
        }

        private int _totalUsers = 0;

        public int TotalUsers
        {
            get { return _totalUsers; }

            set
            {
                _totalUsers = value;
                base.RaisePropertyChanged();
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (TrendingShows != null && TrendingShows.Count <= 0)
                await LoadPage(1, DEFAULT_LIMIT);
        }

        protected override async Task LoadPage(int? page = null, int? limit = null)
        {
            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            Busy.SetBusy(true, "Loading trending shows...");
            var traktTrendingShows = await Movies.GetTrendingShowsAsync(extendedOption, whichPage: page, limitPerPage: limit);

            if (traktTrendingShows.Items != null)
            {
                TrendingShows = traktTrendingShows.Items;
                TotalUsers = traktTrendingShows.TotalUserCount.GetValueOrDefault();
                CurrentPage = traktTrendingShows.CurrentPage.GetValueOrDefault();
                ItemsPerPage = traktTrendingShows.LimitPerPage.GetValueOrDefault();
                TotalItems = traktTrendingShows.TotalItemCount.GetValueOrDefault();
                TotalPages = traktTrendingShows.TotalPages.GetValueOrDefault();
                SelectedLimit = ItemsPerPage;
                SelectedPage = CurrentPage;
            }

            Busy.SetBusy(false);
        }
    }
}
