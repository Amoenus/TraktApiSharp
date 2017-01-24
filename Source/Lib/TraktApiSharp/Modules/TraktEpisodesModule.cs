﻿namespace TraktApiSharp.Modules
{
    using Attributes;
    using Enums;
    using Exceptions;
    using Extensions;
    using Objects.Basic;
    using Objects.Get.Shows;
    using Objects.Get.Shows.Episodes;
    using Objects.Get.Users;
    using Objects.Get.Users.Lists;
    using Requests;
    using Requests.Params;
    using Requests.WithoutOAuth.Shows.Episodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides access to data retrieving methods specific to episodes.
    /// <para>
    /// This module contains all methods of the <a href ="http://docs.trakt.apiary.io/#reference/episodes">"Trakt API Doc - Episodes"</a> section.
    /// </para>
    /// </summary>
    public class TraktEpisodesModule : TraktBaseModule
    {
        internal TraktEpisodesModule(TraktClient client) : base(client) { }

        /// <summary>
        /// Gets a <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/summary/get-a-single-episode-for-a-show">"Trakt API Doc - Episodes: Summary"</a> for more information.
        /// </para>
        /// <para>See also <seealso cref="GetMultipleEpisodesAsync(TraktMultipleEpisodesQueryParams)" />.</para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, which should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, which should be queried.</param>
        /// <param name="extendedInfo">
        /// The extended info, which determines how much data about the episode should be queried.
        /// See also <seealso cref="TraktExtendedInfo" />.
        /// </param>
        /// <returns>An <see cref="TraktEpisode" /> instance with the queried episode's data.</returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<TraktEpisode> GetEpisodeAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber,
                                                        TraktExtendedInfo extendedInfo = null)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeSummaryRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber,
                ExtendedInfo = extendedInfo
            });
        }

        /// <summary>
        /// Gets multiple different <see cref="TraktEpisode" />s at once in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/summary/get-a-single-episode-for-a-show">"Trakt API Doc - Episodes: Summary"</a> for more information.
        /// </para>
        /// <para>See also <seealso cref="GetEpisodeAsync(string, int, int, TraktExtendedInfo)" />.</para>
        /// </summary>
        /// <param name="episodesQueryParams">A list of show ids, season numbers, episode numbers and optional extended infos. See also <seealso cref="TraktMultipleEpisodesQueryParams" />.</param>
        /// <returns>A list of <see cref="TraktEpisode" /> instances with the data of each queried episode.</returns>
        /// <exception cref="TraktException">Thrown, if one request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if one of the given show ids is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<IEnumerable<TraktEpisode>> GetMultipleEpisodesAsync(TraktMultipleEpisodesQueryParams episodesQueryParams)
        {
            if (episodesQueryParams == null || episodesQueryParams.Count <= 0)
                return new List<TraktEpisode>();

            var tasks = new List<Task<TraktEpisode>>();

            foreach (var queryParam in episodesQueryParams)
            {
                Task<TraktEpisode> task = GetEpisodeAsync(queryParam.ShowId, queryParam.Season, queryParam.Episode,
                                                          queryParam.ExtendedInfo);
                tasks.Add(task);
            }

            var episodes = await Task.WhenAll(tasks);
            return episodes.ToList();
        }

        /// <summary>
        /// Gets top level comments for a <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/comments/get-all-episode-comments">"Trakt API Doc - Episodes: Comments"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the comments should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the comments should be queried.</param>
        /// <param name="commentSortOrder">The comments sort order. See also <seealso cref="TraktCommentSortOrder" />.</param>
        /// <param name="page">The page of the comments list, that should be queried. Defaults to the first page.</param>
        /// <param name="limitPerPage">The maximum count of comments for each page, that should be queried.</param>
        /// <returns>
        /// An <see cref="TraktPaginationListResult{TraktComment}"/> instance containing the queried episode comments and which also
        /// contains the queried page number, the page's item count, maximum page count and maximum item count.
        /// <para>
        /// See also <seealso cref="TraktPaginationListResult{ListItem}" /> and <seealso cref="TraktComment" />.
        /// </para>
        /// </returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<TraktPaginationListResult<TraktComment>> GetEpisodeCommentsAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber,
                                                                                           TraktCommentSortOrder commentSortOrder = null,
                                                                                           int? page = null, int? limitPerPage = null)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeCommentsRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber,
                Sorting = commentSortOrder,
                PaginationOptions = new TraktPaginationOptions(page, limitPerPage)
            });
        }

        /// <summary>
        /// Gets all <see cref="TraktList" />s containing the given <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/lists/get-lists-containing-this-episode">"Trakt API Doc - Episodes: Lists"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the lists should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the lists should be queried.</param>
        /// <param name="listType">The type of lists, that should be queried. Defaults to personal lists.</param>
        /// <param name="listSortOrder">The list sort order. See also <seealso cref="TraktListSortOrder" />. Defaults to sorted by popularity.</param>
        /// <param name="page">The page of the <see cref="TraktList" /> list, that should be queried. Defaults to the first page.</param>
        /// <param name="limitPerPage">The maximum count of <see cref="TraktList" />s for each page, that should be queried.</param>
        /// <returns>
        /// An <see cref="TraktPaginationListResult{TraktList}"/> instance containing the queried episode lists and which also
        /// contains the queried page number, the page's item count, maximum page count and maximum item count.
        /// <para>
        /// See also <seealso cref="TraktPaginationListResult{ListItem}" /> and <seealso cref="TraktList" />.
        /// </para>
        /// </returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<TraktPaginationListResult<TraktList>> GetEpisodeListsAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber,
                                                                                     TraktListType listType = null, TraktListSortOrder listSortOrder = null,
                                                                                     int? page = null, int? limitPerPage = null)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeListsRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber,
                Type = listType,
                Sorting = listSortOrder,
                PaginationOptions = new TraktPaginationOptions(page, limitPerPage)
            });
        }

        /// <summary>
        /// Gets the ratings for a <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/ratings/get-episode-ratings">"Trakt API Doc - Episodes: Ratings"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the ratings should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the ratings should be queried.</param>
        /// <returns>An <see cref="TraktRating" /> instance, containing the ratings for a episode with the given showIdOrSlug.</returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<TraktRating> GetEpisodeRatingsAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeRatingsRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber
            });
        }

        /// <summary>
        /// Gets the statistics for a <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/ratings/get-episode-stats">"Trakt API Doc - Episodes: Stats"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the statistics should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the statistics should be queried.</param>
        /// <returns>An <see cref="TraktStatistics" /> instance, containing the statistics for a episode with the given showIdOrSlug.</returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<TraktStatistics> GetEpisodeStatisticsAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeStatisticsRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber
            });
        }

        /// <summary>
        /// Gets all translations for a <see cref="TraktEpisode" /> with the given Trakt-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/translations/get-all-episode-translations">"Trakt API Doc - Episodes: Translations"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the translations should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the translations should be queried.</param>
        /// <param name="languageCode">An optional two letter language code to query a specific translation language.</param>
        /// <returns>A list of <see cref="TraktEpisodeTranslation" /> instances, each containing a title, overview and language code.</returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// Thrown, if the given languageCode is shorter or longer than two characters.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<IEnumerable<TraktEpisodeTranslation>> GetEpisodeTranslationsAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber,
                                                                                            string languageCode = null)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            if (languageCode != null && languageCode.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(languageCode), "language code has wrong length");

            return await QueryAsync(new TraktEpisodeTranslationsRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber,
                LanguageCode = languageCode
            });
        }

        /// <summary>
        /// Gets all watching users of a <see cref="TraktEpisode" /> in a show with the given Trakt-Show-Id or -Slug.
        /// <para>OAuth authorization not required.</para>
        /// <para>
        /// See <a href="http://docs.trakt.apiary.io/#reference/episodes/watching/get-users-watching-right-now">"Trakt API Doc - Episodes: Watching"</a> for more information.
        /// </para>
        /// </summary>
        /// <param name="showIdOrSlug">The show's Trakt-Id or -Slug. See also <seealso cref="TraktShowIds" />.</param>
        /// <param name="seasonNumber">The number of the season containing the episode, for which the watching users should be queried.</param>
        /// <param name="episodeNumber">The number of the episode, for which the watching users should be queried.</param>
        /// <param name="extendedInfo">
        /// The extended info, which determines how much data about the users should be queried.
        /// See also <seealso cref="TraktExtendedInfo" />.
        /// </param>
        /// <returns>A list of <see cref="TraktUser" /> instances.</returns>
        /// <exception cref="TraktException">Thrown, if the request fails.</exception>
        /// <exception cref="ArgumentException">Thrown, if the given showIdOrSlug is null, empty or contains spaces.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown, if the given season-number is below zero.
        /// Thrown, if the given episode-number is below one.
        /// </exception>
        [OAuthAuthorizationRequired(false)]
        public async Task<IEnumerable<TraktUser>> GetEpisodeWatchingUsersAsync([NotNull] string showIdOrSlug, int seasonNumber, int episodeNumber,
                                                                               TraktExtendedInfo extendedInfo = null)
        {
            Validate(showIdOrSlug, seasonNumber, episodeNumber);

            return await QueryAsync(new TraktEpisodeWatchingUsersRequest(Client)
            {
                Id = showIdOrSlug,
                Season = seasonNumber,
                Episode = episodeNumber,
                ExtendedInfo = extendedInfo
            });
        }

        private void Validate(string showIdOrSlug, int seasonNumber, int episodeNumber)
        {
            if (string.IsNullOrEmpty(showIdOrSlug) || showIdOrSlug.ContainsSpace())
                throw new ArgumentException("show id or slug not valid", nameof(showIdOrSlug));

            if (seasonNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(seasonNumber), "season number not valid");

            if (episodeNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(episodeNumber), "episode number not valid");
        }
    }
}
