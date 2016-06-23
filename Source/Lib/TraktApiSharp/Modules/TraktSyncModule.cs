﻿namespace TraktApiSharp.Modules
{
    using Enums;
    using Extensions;
    using Objects.Basic;
    using Objects.Get.Syncs.Activities;
    using Objects.Get.Syncs.Collection;
    using Objects.Get.Syncs.History;
    using Objects.Get.Syncs.Playback;
    using Objects.Get.Syncs.Ratings;
    using Objects.Get.Syncs.Watched;
    using Objects.Get.Syncs.Watchlist;
    using Objects.Post.Syncs.Collection;
    using Objects.Post.Syncs.Collection.Responses;
    using Objects.Post.Syncs.History;
    using Objects.Post.Syncs.History.Responses;
    using Objects.Post.Syncs.Ratings;
    using Objects.Post.Syncs.Ratings.Responses;
    using Objects.Post.Syncs.Watchlist;
    using Objects.Post.Syncs.Watchlist.Responses;
    using Requests;
    using Requests.WithOAuth.Syncs;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TraktSyncModule : TraktBaseModule
    {
        public TraktSyncModule(TraktClient client) : base(client) { }

        public async Task<TraktSyncLastActivities> GetLastActivitiesAsync()
        {
            return await QueryAsync(new TraktSyncLastActivitiesRequest(Client));
        }

        public async Task<TraktListResult<TraktSyncPlaybackProgressItem>> GetPlaybackProgressAsync(TraktSyncType? type = null,
                                                                                                   TraktExtendedOption extended = null,
                                                                                                   int? limit = null)
        {
            return await QueryAsync(new TraktSyncPlaybackProgressRequest(Client)
            {
                Type = type,
                ExtendedOption = extended ?? new TraktExtendedOption(),
                PaginationOptions = new TraktPaginationOptions(null, limit)
            });
        }

        public async Task RemovePlaybackItemAsync(string playbackId)
        {
            if (string.IsNullOrEmpty(playbackId) || playbackId.ContainsSpace())
                throw new ArgumentException("playback id not valid", "playbackId");

            await QueryAsync(new TraktSyncPlaybackDeleteRequest(Client) { Id = playbackId });
        }

        public async Task<TraktListResult<TraktSyncCollectionMovieItem>> GetCollectionMoviesAsync(TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncCollectionMoviesRequest(Client)
            {
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktListResult<TraktSyncCollectionShowItem>> GetCollectionShowsAsync(TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncCollectionShowsRequest(Client)
            {
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktSyncCollectionPostResponse> AddCollectionItemsAsync(TraktSyncCollectionPost collectionPost)
        {
            if (collectionPost == null)
                throw new ArgumentNullException("collection items post must not be null", "collectionPost");

            var movies = collectionPost.Movies;
            var shows = collectionPost.Shows;
            var episodes = collectionPost.Episodes;

            var bHasNoMovies = movies == null || !movies.Any();
            var bHasNoShows = shows == null || !shows.Any();
            var bHasNoEpisodes = episodes == null || !episodes.Any();

            if (bHasNoMovies && bHasNoShows && bHasNoEpisodes)
                throw new ArgumentException("no items set");

            return await QueryAsync(new TraktSyncCollectionAddRequest(Client) { RequestBody = collectionPost });
        }

        public async Task<TraktSyncCollectionRemovePostResponse> RemoveCollectionItemsAsync(TraktSyncCollectionRemovePost collectionRemovePost)
        {
            return await QueryAsync(new TraktSyncCollectionRemoveRequest(Client) { RequestBody = collectionRemovePost });
        }

        public async Task<TraktListResult<TraktSyncWatchedMovieItem>> GetWatchedMoviesAsync(TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncWatchedMoviesRequest(Client)
            {
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktListResult<TraktSyncWatchedShowItem>> GetWatchedShowsAsync(TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncWatchedShowsRequest(Client)
            {
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktPaginationListResult<TraktSyncHistoryItem>> GetWatchedHistoryAsync(TraktSyncHistoryItemType? type = null, string id = null,
                                                                                                  TraktExtendedOption extended = null,
                                                                                                  int? page = null, int? limit = null)
        {
            return await QueryAsync(new TraktSyncWatchedHistoryRequest(Client)
            {
                Type = type,
                ItemId = id,
                ExtendedOption = extended ?? new TraktExtendedOption(),
                PaginationOptions = new TraktPaginationOptions(page, limit)
            });
        }

        public async Task<TraktSyncHistoryPostResponse> AddWatchedHistoryItemsAsync(TraktSyncHistoryPost historyPost)
        {
            return await QueryAsync(new TraktSyncWatchedHistoryAddRequest(Client) { RequestBody = historyPost });
        }

        public async Task<TraktSyncHistoryRemovePostResponse> RemoveWatchedHistoryItemsAsync(TraktSyncHistoryRemovePost historyRemovePost)
        {
            return await QueryAsync(new TraktSyncWatchedHistoryRemoveRequest(Client) { RequestBody = historyRemovePost });
        }

        public async Task<TraktListResult<TraktSyncRatingsItem>> GetRatingsAsync(TraktSyncRatingsItemType? type = null,
                                                                                 int[] rating = null,
                                                                                 TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncRatingsRequest(Client)
            {
                Type = type,
                Rating = rating,
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktSyncRatingsPostResponse> AddRatingsAsync(TraktSyncRatingsPost ratingsPost)
        {
            return await QueryAsync(new TraktSyncRatingsAddRequest(Client) { RequestBody = ratingsPost });
        }

        public async Task<TraktSyncRatingsRemovePostResponse> RemoveRatingsAsync(TraktSyncRatingsRemovePost ratingsRemovePost)
        {
            return await QueryAsync(new TraktSyncRatingsRemoveRequest(Client) { RequestBody = ratingsRemovePost });
        }

        public async Task<TraktListResult<TraktSyncWatchlistItem>> GetWatchlistAsync(TraktSyncWatchlistItemType? type = null,
                                                                                     TraktExtendedOption extended = null)
        {
            return await QueryAsync(new TraktSyncWatchlistRequest(Client)
            {
                Type = type,
                ExtendedOption = extended ?? new TraktExtendedOption()
            });
        }

        public async Task<TraktSyncWatchlistPostResponse> AddWatchlistItemsAsync(TraktSyncWatchlistPost watchlistPost)
        {
            return await QueryAsync(new TraktSyncWatchlistAddRequest(Client) { RequestBody = watchlistPost });
        }

        public async Task<TraktSyncWatchlistRemovePostResponse> RemoveWatchlistItemsAsync(TraktSyncWatchlistRemovePost watchlistRemovePost)
        {
            return await QueryAsync(new TraktSyncWatchlistRemoveRequest(Client) { RequestBody = watchlistRemovePost });
        }
    }
}
