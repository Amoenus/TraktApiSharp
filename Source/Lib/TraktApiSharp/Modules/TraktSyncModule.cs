﻿namespace TraktApiSharp.Modules
{
    using Enums;
    using Extensions;
    using Objects.Basic;
    using Objects.Get.Collection;
    using Objects.Get.History;
    using Objects.Get.Ratings;
    using Objects.Get.Syncs.Activities;
    using Objects.Get.Syncs.Playback;
    using Objects.Get.Watched;
    using Objects.Get.Watchlist;
    using Objects.Post.Syncs.Collection;
    using Objects.Post.Syncs.Collection.Responses;
    using Objects.Post.Syncs.History;
    using Objects.Post.Syncs.History.Responses;
    using Objects.Post.Syncs.Ratings;
    using Objects.Post.Syncs.Ratings.Responses;
    using Objects.Post.Syncs.Watchlist;
    using Objects.Post.Syncs.Watchlist.Responses;
    using Requests;
    using Requests.Params;
    using Requests.WithOAuth.Syncs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides access to data retrieving methods specific to sync.
    /// <para>
    /// This module contains all methods of the <a href ="http://docs.trakt.apiary.io/#reference/sync">"Trakt API Doc - Sync"</a> section.
    /// </para>
    /// </summary>
    public class TraktSyncModule : TraktBaseModule
    {
        internal TraktSyncModule(TraktClient client) : base(client) { }

        /// <summary>
        /// Gets the user's last activities.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/last-activities/get-last-activity">"Trakt API Doc - Sync: Last Activities"</a> for more information.
        /// </para>
        /// </summary>
        /// <returns>A <see cref="TraktSyncLastActivities" /> instance with the queried last activities.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<TraktSyncLastActivities> GetLastActivitiesAsync()
        {
            return await QueryAsync(new TraktSyncLastActivitiesRequest(Client));
        }

        /// <summary>
        /// Gets the user's saved playback progress of scrobbles that are paused.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/last-activities/get-playback-progress">"Trakt API Doc - Sync: Playback"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="objectType">Determines, which type of items should be queried. By default, all types will be returned. See also <seealso cref="TraktSyncType" />.</param>
        /// <param name="limit">Determines, how many progress items should be queried. By default, all items will be returned</param>
        /// <returns>A list of <see cref="TraktSyncPlaybackProgressItem" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktSyncPlaybackProgressItem>> GetPlaybackProgressAsync(TraktSyncType? objectType = null, int? limit = null)
        {
            return await QueryAsync(new TraktSyncPlaybackProgressRequest(Client)
            {
                Type = objectType,
                PaginationOptions = new TraktPaginationOptions(null, limit)
            });
        }

        /// <summary>
        /// Removes a playback progress item from the user's playback progress list.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/remove-playback/remove-a-playback-item">"Trakt API Doc - Sync: Remove Playback"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="playbackId">The id of the playback progress item, which should be removed.</param>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given playback progress id is null, empty or contains spaces.</exception>
        public async Task RemovePlaybackItemAsync(string playbackId)
        {
            if (string.IsNullOrEmpty(playbackId) || playbackId.ContainsSpace())
                throw new ArgumentException("playback id not valid", nameof(playbackId));

            await QueryAsync(new TraktSyncPlaybackDeleteRequest(Client) { Id = playbackId });
        }

        /// <summary>
        /// Gets all collected movies in the user's collection.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-collection/get-collection">"Trakt API Doc - Sync: Get Collection"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the movies should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktCollectionMovie" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktCollectionMovie>> GetCollectionMoviesAsync(TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncCollectionMoviesRequest(Client) { ExtendedOption = extendedOption });
        }

        /// <summary>
        /// Gets all collected shows in the user's collection.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-collection/get-collection">"Trakt API Doc - Sync: Get Collection"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the shows should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktCollectionShow" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktCollectionShow>> GetCollectionShowsAsync(TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncCollectionShowsRequest(Client) { ExtendedOption = extendedOption });
        }

        /// <summary>
        /// Adds items to the user's collection. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-collection/add-items-to-collection">"Trakt API Doc - Sync: Add to Collection"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncCollectionPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncCollectionPost" />.
        /// See also <seealso cref="TraktSyncCollectionPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="collectionPost">A <see cref="TraktSyncCollectionPost" /> instance containing all shows, seasons, episodes and movies, which should be added.</param>
        /// <returns>A <see cref="TraktSyncCollectionPostResponse" /> instance, which contains information about which items were added, updated, existing and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given collection post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given collection post is empty.</exception>
        public async Task<TraktSyncCollectionPostResponse> AddCollectionItemsAsync(TraktSyncCollectionPost collectionPost)
        {
            ValidateCollectionPost(collectionPost);

            return await QueryAsync(new TraktSyncCollectionAddRequest(Client) { RequestBody = collectionPost });
        }

        /// <summary>
        /// Removes items from the user's collection. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/remove-from-collection/remove-items-from-collection">"Trakt API Doc - Sync: Remove from Collection"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncCollectionPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncCollectionPost" />.
        /// See also <seealso cref="TraktSyncCollectionPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="collectionRemovePost">A <see cref="TraktSyncCollectionPost" /> instance containing all shows, seasons, episodes and movies, which should be removed.</param>
        /// <returns>A <see cref="TraktSyncCollectionRemovePostResponse" /> instance, which contains information about which items were deleted and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given collection remove post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given collection remove post is empty.</exception>
        public async Task<TraktSyncCollectionRemovePostResponse> RemoveCollectionItemsAsync(TraktSyncCollectionPost collectionRemovePost)
        {
            ValidateCollectionPost(collectionRemovePost);

            return await QueryAsync(new TraktSyncCollectionRemoveRequest(Client) { RequestBody = collectionRemovePost });
        }

        /// <summary>
        /// Gets all movies the user has watched, sorted by most plays.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/remove-from-collection/get-watched">"Trakt API Doc - Sync: Get Watched"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the movies should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktWatchedMovie" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktWatchedMovie>> GetWatchedMoviesAsync(TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncWatchedMoviesRequest(Client) { ExtendedOption = extendedOption });
        }

        /// <summary>
        /// Gets all shows the user has watched, sorted by most plays.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/remove-from-collection/get-watched">"Trakt API Doc - Sync: Get Watched"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the shows should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktWatchedShow" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktWatchedShow>> GetWatchedShowsAsync(TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncWatchedShowsRequest(Client) { ExtendedOption = extendedOption });
        }

        /// <summary>
        /// Gets all movies, shows, seasons and / or episodes the user has watched, sorted by most recent.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-watched/get-watched-history">"Trakt API Doc - Sync: Get History"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="historyItemType">Determines, which type of history items should be queried. See also <seealso cref="TraktSyncItemType" />.</param>
        /// <param name="historyItemId">An unique history item id to determine, which item should be specifically queried. Will be ignored, if <paramref name="historyItemType" /> is not set or unspecified.</param>
        /// <param name="startAt">The datetime, after which history items should be queried. Will be converted to the Trakt UTC-datetime and -format.</param>
        /// <param name="endAt">The datetime, until which history items should be queried. Will be converted to the Trakt UTC-datetime and -format.</param>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the history items should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <param name="page">The page of the history items list, that should be queried. Defaults to the first page. See also <see cref="TraktPaginationOptions" />.</param>
        /// <param name="limitPerPage">The maximum count of history items for each page, that should be queried. See also <see cref="TraktPaginationOptions" />.</param>
        /// <returns>
        /// A <see cref="TraktPaginationListResult{TraktHistoryItem}"/> instance containing the queried history items and which also
        /// contains the queried page number, the page's item count, maximum page count and maximum item count.
        /// <para>
        /// See also <seealso cref="TraktPaginationListResult{ListItem}" /> and <seealso cref="TraktHistoryItem" />.
        /// </para>
        /// </returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<TraktPaginationListResult<TraktHistoryItem>> GetWatchedHistoryAsync(TraktSyncItemType? historyItemType = null, string historyItemId = null,
                                                                                              DateTime? startAt = null, DateTime? endAt = null,
                                                                                              TraktExtendedOption extendedOption = null,
                                                                                              int? page = null, int? limitPerPage = null)
        {
            return await QueryAsync(new TraktSyncWatchedHistoryRequest(Client)
            {
                Type = historyItemType,
                ItemId = historyItemId,
                StartAt = startAt,
                EndAt = endAt,
                ExtendedOption = extendedOption,
                PaginationOptions = new TraktPaginationOptions(page, limitPerPage)
            });
        }

        /// <summary>
        /// Adds items to the user's watch history. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/add-to-history/add-items-to-watched-history">"Trakt API Doc - Sync: Add to History"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncHistoryPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncHistoryPost" />.
        /// See also <seealso cref="TraktSyncHistoryPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="historyPost">A <see cref="TraktSyncHistoryPost" /> instance containing all shows, seasons, episodes and movies, which should be added.</param>
        /// <returns>A <see cref="TraktSyncHistoryPostResponse" /> instance, which contains information about which items were added and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given history post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given history post is empty.</exception>
        public async Task<TraktSyncHistoryPostResponse> AddWatchedHistoryItemsAsync(TraktSyncHistoryPost historyPost)
        {
            ValidateHistoryPost(historyPost);

            return await QueryAsync(new TraktSyncWatchedHistoryAddRequest(Client) { RequestBody = historyPost });
        }

        /// <summary>
        /// Removes items from the user's watch history. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/add-to-history/remove-items-from-history">"Trakt API Doc - Sync: Remove from History"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncHistoryRemovePostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncHistoryRemovePost" />.
        /// See also <seealso cref="TraktSyncHistoryRemovePost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="historyRemovePost">A <see cref="TraktSyncHistoryRemovePost" /> instance containing all shows, seasons, episodes and movies, which should be removed.</param>
        /// <returns>A <see cref="TraktSyncHistoryRemovePostResponse" /> instance, which contains information about which items were deleted and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given history remove post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given history remove post is empty.</exception>
        public async Task<TraktSyncHistoryRemovePostResponse> RemoveWatchedHistoryItemsAsync(TraktSyncHistoryRemovePost historyRemovePost)
        {
            ValidateHistoryPost(historyRemovePost);

            return await QueryAsync(new TraktSyncWatchedHistoryRemoveRequest(Client) { RequestBody = historyRemovePost });
        }

        /// <summary>
        /// Gets the user's ratings for movies, shows, seasons and / or episodes.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-ratings/get-ratings">"Trakt API Doc - Sync: Get Ratings"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="ratingsItemType">Determines, which type of rating items should be queried. See also <seealso cref="TraktSyncRatingsItemType" />.</param>
        /// <param name="ratingsFilter">
        /// An array of numbers. Numbers should be between 1 and 10.
        /// Will be ignored, if the given array contains a number higher than 10 or below 1 or if it contains more than ten numbers.
        /// </param>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the rating items should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktRatingsItem" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        // TODO FIXME ratingsFilter will be ignored, when ratingsItemType is null
        public async Task<IEnumerable<TraktRatingsItem>> GetRatingsAsync(TraktSyncRatingsItemType? ratingsItemType = null,
                                                                         int[] ratingsFilter = null,
                                                                         TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncRatingsRequest(Client)
            {
                Type = ratingsItemType,
                Rating = ratingsFilter,
                ExtendedOption = extendedOption
            });
        }

        /// <summary>
        /// Adds items to the user's ratings. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/add-ratings/add-new-ratings">"Trakt API Doc - Sync: Add Ratings"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncRatingsPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncRatingsPost" />.
        /// See also <seealso cref="TraktSyncRatingsPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="ratingsPost">A <see cref="TraktSyncRatingsPost" /> instance containing all shows, seasons, episodes and movies, which should be added.</param>
        /// <returns>A <see cref="TraktSyncRatingsPostResponse" /> instance, which contains information about which items were added and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given ratings post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given ratings post is empty.</exception>
        public async Task<TraktSyncRatingsPostResponse> AddRatingsAsync(TraktSyncRatingsPost ratingsPost)
        {
            ValidateRatingsPost(ratingsPost);

            return await QueryAsync(new TraktSyncRatingsAddRequest(Client) { RequestBody = ratingsPost });
        }

        /// <summary>
        /// Removes items from the user's ratings. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/add-ratings/remove-ratings">"Trakt API Doc - Sync: Remove Ratings"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncRatingsPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncRatingsPost" />.
        /// See also <seealso cref="TraktSyncRatingsPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="ratingsRemovePost">A <see cref="TraktSyncRatingsPost" /> instance containing all shows, seasons, episodes and movies, which should be removed.</param>
        /// <returns>A <see cref="TraktSyncRatingsRemovePostResponse" /> instance, which contains information about which items were deleted and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given ratings remove post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given ratings remove post is empty.</exception>
        public async Task<TraktSyncRatingsRemovePostResponse> RemoveRatingsAsync(TraktSyncRatingsPost ratingsRemovePost)
        {
            ValidateRatingsPost(ratingsRemovePost);

            return await QueryAsync(new TraktSyncRatingsRemoveRequest(Client) { RequestBody = ratingsRemovePost });
        }

        /// <summary>
        /// Gets the user's watchlist containing movies, shows, seasons and / or episodes.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/get-watchlist/get-watchlist">"Trakt API Doc - Sync: Get Watchlist"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="watchlistItemType">Determines, which type of watchlist items should be queried. See also <seealso cref="TraktSyncItemType" />.</param>
        /// <param name="extendedOption">
        /// The extended option, which determines how much data about the watchlist items should be queried.
        /// See also <seealso cref="TraktExtendedOption" />.
        /// </param>
        /// <returns>A list of <see cref="TraktWatchlistItem" /> instances.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        public async Task<IEnumerable<TraktWatchlistItem>> GetWatchlistAsync(TraktSyncItemType? watchlistItemType = null,
                                                                             TraktExtendedOption extendedOption = null)
        {
            return await QueryAsync(new TraktSyncWatchlistRequest(Client)
            {
                Type = watchlistItemType,
                ExtendedOption = extendedOption
            });
        }

        /// <summary>
        /// Adds items to the user's watchlist. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/add-to-watchlist/add-items-to-watchlist">"Trakt API Doc - Sync: Add to Watchlist"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncWatchlistPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncWatchlistPost" />.
        /// See also <seealso cref="TraktSyncWatchlistPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="watchlistPost">A <see cref="TraktSyncWatchlistPost" /> instance containing all shows, seasons, episodes and movies, which should be added.</param>
        /// <returns>A <see cref="TraktSyncWatchlistPostResponse" /> instance, which contains information about which items were added, existing and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given watchlist post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given watchlist post is empty.</exception>
        public async Task<TraktSyncWatchlistPostResponse> AddWatchlistItemsAsync(TraktSyncWatchlistPost watchlistPost)
        {
            ValidateWatchlistPost(watchlistPost);

            return await QueryAsync(new TraktSyncWatchlistAddRequest(Client) { RequestBody = watchlistPost });
        }

        /// <summary>
        /// Removes items from the user's watchlist. Accepts shows, seasons, episodes and movies.
        /// <para>OAuth authorization required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/sync/remove-from-watchlist/remove-items-from-watchlists">"Trakt API Doc - Sync: Remove from Watchlist"</a> for more information.
        /// </para>
        /// <para>
        /// It is recommended to use the <see cref="TraktSyncWatchlistPostBuilder" /> to create an instance
        /// of the required <see cref="TraktSyncWatchlistPost" />.
        /// See also <seealso cref="TraktSyncWatchlistPost.Builder()" />.
        /// </para>
        /// </summary>
        /// <param name="watchlistRemovePost">A <see cref="TraktSyncWatchlistPost" /> instance containing all shows, seasons, episodes and movies, which should be removed.</param>
        /// <returns>A <see cref="TraktSyncWatchlistRemovePostResponse" /> instance, which contains information about which items were deleted and not found.</returns>
        /// <exception cref="Exceptions.TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given watchlist remove post is null.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given watchlist remove post is empty.</exception>
        public async Task<TraktSyncWatchlistRemovePostResponse> RemoveWatchlistItemsAsync(TraktSyncWatchlistPost watchlistRemovePost)
        {
            ValidateWatchlistPost(watchlistRemovePost);

            return await QueryAsync(new TraktSyncWatchlistRemoveRequest(Client) { RequestBody = watchlistRemovePost });
        }

        private void ValidateCollectionPost(TraktSyncCollectionPost collectionPost)
        {
            if (collectionPost == null)
                throw new ArgumentNullException(nameof(collectionPost), "collection post must not be null");

            var movies = collectionPost.Movies;
            var shows = collectionPost.Shows;
            var episodes = collectionPost.Episodes;

            var bHasNoMovies = movies == null || !movies.Any();
            var bHasNoShows = shows == null || !shows.Any();
            var bHasNoEpisodes = episodes == null || !episodes.Any();

            if (bHasNoMovies && bHasNoShows && bHasNoEpisodes)
                throw new ArgumentException("no collection items set");
        }

        private void ValidateHistoryPost(TraktSyncHistoryPost historyPost)
        {
            if (historyPost == null)
                throw new ArgumentNullException(nameof(historyPost), "history post must not be null");

            var movies = historyPost.Movies;
            var shows = historyPost.Shows;
            var episodes = historyPost.Episodes;

            var bHasNoMovies = movies == null || !movies.Any();
            var bHasNoShows = shows == null || !shows.Any();
            var bHasNoEpisodes = episodes == null || !episodes.Any();

            if (bHasNoMovies && bHasNoShows && bHasNoEpisodes)
                throw new ArgumentException("no watched history items set");
        }

        private void ValidateRatingsPost(TraktSyncRatingsPost ratingsPost)
        {
            if (ratingsPost == null)
                throw new ArgumentNullException(nameof(ratingsPost), "ratings post must not be null");

            var movies = ratingsPost.Movies;
            var shows = ratingsPost.Shows;
            var episodes = ratingsPost.Episodes;

            var bHasNoMovies = movies == null || !movies.Any();
            var bHasNoShows = shows == null || !shows.Any();
            var bHasNoEpisodes = episodes == null || !episodes.Any();

            if (bHasNoMovies && bHasNoShows && bHasNoEpisodes)
                throw new ArgumentException("no ratings items set");
        }

        private void ValidateWatchlistPost(TraktSyncWatchlistPost watchlistPost)
        {
            if (watchlistPost == null)
                throw new ArgumentNullException(nameof(watchlistPost), "watchlist post must not be null");

            var movies = watchlistPost.Movies;
            var shows = watchlistPost.Shows;
            var episodes = watchlistPost.Episodes;

            var bHasNoMovies = movies == null || !movies.Any();
            var bHasNoShows = shows == null || !shows.Any();
            var bHasNoEpisodes = episodes == null || !episodes.Any();

            if (bHasNoMovies && bHasNoShows && bHasNoEpisodes)
                throw new ArgumentException("no watchlist items set");
        }
    }
}
