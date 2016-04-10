﻿namespace TraktApiSharp.Requests.Movies.Common
{
    using Base.Get;
    using Objects;
    using Objects.Movies.Common;

    internal class TraktMoviesPopularRequest : TraktGetRequest<TraktPaginationListResult<TraktMoviesPopularItem>, TraktMoviesPopularItem>
    {
        internal TraktMoviesPopularRequest(TraktClient client) : base(client) { }

        protected override string UriTemplate => "movies/popular";

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.NotRequired;

        protected override bool SupportsPagination => true;

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Movies;

        protected override bool IsListResult => true;
    }
}
