﻿namespace TraktApiSharp.Modules
{
    using Objects;
    using Objects.Shows.Episodes;
    using Objects.Shows.Seasons;
    using Requests;
    using Requests.WithoutOAuth.Shows.Seasons;
    using System.Threading.Tasks;

    public class TraktSeasonsModule : TraktBaseModule
    {
        public TraktSeasonsModule(TraktClient client) : base(client) { }

        public async Task<TraktListResult<TraktSeason>> GetSeasonsAllAsync(string showId, TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktSeasonsAllRequest(Client)
            {
                Id = showId,
                ExtendedOption = extended
            });
        }

        public async Task<TraktListResult<TraktEpisode>> GetSeasonSingleAsync(string showId, int season,
                                                                              TraktExtendedOption extended = TraktExtendedOption.Unspecified)
        {
            return await QueryAsync(new TraktSeasonSingleRequest(Client)
            {
                Id = showId,
                Season = season,
                ExtendedOption = extended
            });
        }

        public async Task<TraktPaginationListResult<TraktSeasonComment>> GetSeasonCommentsAsync(string showId, int season,
                                                                                                int? page = null, int? limit = null)
        {
            return await QueryAsync(new TraktSeasonCommentsRequest(Client)
            {
                Id = showId,
                Season = season,
                PaginationOptions = new TraktPaginationOptions(page, limit)
            });
        }

        public async Task<TraktSeasonRating> GetSeasonRatingsAsync(string showId, int season)
        {
            return await QueryAsync(new TraktSeasonRatingsRequest(Client)
            {
                Id = showId,
                Season = season
            });
        }

        public async Task<TraktSeasonStatistics> GetSeasonStatisticsAsync(string showId, int season)
        {
            return await QueryAsync(new TraktSeasonStatisticsRequest(Client)
            {
                Id = showId,
                Season = season
            });
        }

        public async Task<TraktListResult<TraktSeasonWatchingUser>> GetSeasonWatchingUsersAsync(string showId, int season)
        {
            return await QueryAsync(new TraktSeasonWatchingUsersRequest(Client)
            {
                Id = showId,
                Season = season
            });
        }
    }
}
