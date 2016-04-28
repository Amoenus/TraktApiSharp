﻿namespace TraktApiSharp.Requests.WithOAuth.Users
{
    using Base.Get;
    using Objects.Basic;
    using Objects.Get.Users.Watched;
    using System.Collections.Generic;

    internal class TraktUserWatchedMoviesRequest : TraktGetRequest<TraktListResult<TraktUserWatchedMovieItem>, TraktUserWatchedMovieItem>
    {
        internal TraktUserWatchedMoviesRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.Optional;

        internal string Username { get; set; }

        protected override IEnumerable<KeyValuePair<string, string>> GetPathParameters()
        {
            return new Dictionary<string, string> { { "username", Username } };
        }

        protected override string UriTemplate => "users/{username}/watched/movies";

        protected override bool IsListResult => true;
    }
}
