﻿namespace TraktApiSharp.Modules
{
    using Objects.Get.Movies;
    using Objects.Get.Shows;
    using Objects.Get.Shows.Episodes;
    using Objects.Post;
    using Objects.Post.Scrobbles;
    using Objects.Post.Scrobbles.Responses;
    using Requests;
    using Requests.WithOAuth.Scrobbles;
    using System;
    using System.Threading.Tasks;

    public class TraktScrobbleModule : TraktBaseModule
    {
        public TraktScrobbleModule(TraktClient client) : base(client) { }

        public async Task<TraktMovieScrobblePostResponse> StartMovieAsync(TraktMovie movie, float progress,
                                                                          string appVersion = null, DateTime? appDate = null,
                                                                          TraktExtendedOption extended = null)
        {
            var requestBody = CreateMovieScrobblePost(movie, progress, appVersion, appDate);
            return await QueryAsync(CreateScrobbleStartRequest<TraktMovieScrobblePostResponse, TraktMovieScrobblePost>(requestBody, extended));
        }

        public async Task<TraktMovieScrobblePostResponse> PauseMovieAsync(TraktMovie movie, float progress,
                                                                          string appVersion = null, DateTime? appDate = null,
                                                                          TraktExtendedOption extended = null)
        {
            var requestBody = CreateMovieScrobblePost(movie, progress, appVersion, appDate);
            return await QueryAsync(CreateScrobblePauseRequest<TraktMovieScrobblePostResponse, TraktMovieScrobblePost>(requestBody, extended));
        }

        public async Task<TraktMovieScrobblePostResponse> StopMovieAsync(TraktMovie movie, float progress,
                                                                         string appVersion = null, DateTime? appDate = null,
                                                                         TraktExtendedOption extended = null)
        {
            var requestBody = CreateMovieScrobblePost(movie, progress, appVersion, appDate);
            return await QueryAsync(CreateScrobbleStopRequest<TraktMovieScrobblePostResponse, TraktMovieScrobblePost>(requestBody, extended));
        }

        public async Task<TraktEpisodeScrobblePostResponse> StartEpisodeAsync(TraktEpisode episode, float progress, TraktShow show = null,
                                                                             string appVersion = "", DateTime? appDate = null,
                                                                             TraktExtendedOption extended = null)
        {
            var requestBody = CreateEpisodeScrobblePost(episode, progress, show, appVersion, appDate);
            return await QueryAsync(CreateScrobbleStartRequest<TraktEpisodeScrobblePostResponse, TraktEpisodeScrobblePost>(requestBody, extended));
        }

        public async Task<TraktEpisodeScrobblePostResponse> PauseEpisodeAsync(TraktEpisode episode, float progress, TraktShow show = null,
                                                                              string appVersion = "", DateTime? appDate = null,
                                                                              TraktExtendedOption extended = null)
        {
            var requestBody = CreateEpisodeScrobblePost(episode, progress, show, appVersion, appDate);
            return await QueryAsync(CreateScrobblePauseRequest<TraktEpisodeScrobblePostResponse, TraktEpisodeScrobblePost>(requestBody, extended));
        }

        public async Task<TraktEpisodeScrobblePostResponse> StopEpisodeAsync(TraktEpisode episode, float progress, TraktShow show = null,
                                                                             string appVersion = "", DateTime? appDate = null,
                                                                             TraktExtendedOption extended = null)
        {
            var requestBody = CreateEpisodeScrobblePost(episode, progress, show, appVersion, appDate);
            return await QueryAsync(CreateScrobbleStopRequest<TraktEpisodeScrobblePostResponse, TraktEpisodeScrobblePost>(requestBody, extended));
        }

        private TraktScrobbleStartRequest<T, U> CreateScrobbleStartRequest<T, U>(U requestBody,
                                                                                 TraktExtendedOption extended = null) where U : IValidatable
        {
            return new TraktScrobbleStartRequest<T, U>(Client)
            {
                RequestBody = requestBody,
                ExtendedOption = extended ?? new TraktExtendedOption()
            };
        }

        private TraktScrobblePauseRequest<T, U> CreateScrobblePauseRequest<T, U>(U requestBody,
                                                                                 TraktExtendedOption extended = null) where U : IValidatable
        {
            return new TraktScrobblePauseRequest<T, U>(Client)
            {
                RequestBody = requestBody,
                ExtendedOption = extended ?? new TraktExtendedOption()
            };
        }

        private TraktScrobbleStopRequest<T, U> CreateScrobbleStopRequest<T, U>(U requestBody,
                                                                                 TraktExtendedOption extended = null) where U : IValidatable
        {
            return new TraktScrobbleStopRequest<T, U>(Client)
            {
                RequestBody = requestBody,
                ExtendedOption = extended ?? new TraktExtendedOption()
            };
        }

        private TraktMovieScrobblePost CreateMovieScrobblePost(TraktMovie movie, float progress,
                                                               string appVersion = null, DateTime? appDate = null)
        {
            var movieScrobblePost = new TraktMovieScrobblePost
            {
                Movie = movie,
                Progress = progress
            };

            if (!string.IsNullOrEmpty(appVersion))
                movieScrobblePost.AppVersion = appVersion;

            if (appDate.HasValue)
                movieScrobblePost.AppDate = appDate.Value.ToString("yyyy-MM-dd");

            return movieScrobblePost;
        }

        private TraktEpisodeScrobblePost CreateEpisodeScrobblePost(TraktEpisode episode, float progress, TraktShow show = null,
                                                                   string appVersion = "", DateTime? appDate = null)
        {
            return new TraktEpisodeScrobblePost
            {
                Episode = new TraktEpisode
                {
                    Ids = episode.Ids,
                    SeasonNumber = episode.SeasonNumber,
                    Number = episode.Number
                },
                Show = show != null ? new TraktShow
                {
                    Title = show.Title
                } : null,
                Progress = progress,
                AppVersion = appVersion,
                AppDate = appDate.HasValue ? appDate.Value.ToString("yyyy-MM-dd") : DateTime.UtcNow.ToString("yyyy-MM-dd")
            };
        }
    }
}
