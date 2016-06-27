﻿namespace TraktApiSharp.Requests.WithoutOAuth.Genres
{
    using Base.Get;
    using Objects.Basic;

    internal class TraktGenresMoviesRequest : TraktGetRequest<TraktListResult<TraktGenre>, TraktGenre>
    {
        internal TraktGenresMoviesRequest(TraktClient client) : base(client) { }

        protected override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        protected override bool IsListResult => true;

        protected override string UriTemplate => "genres/movies";
    }
}
