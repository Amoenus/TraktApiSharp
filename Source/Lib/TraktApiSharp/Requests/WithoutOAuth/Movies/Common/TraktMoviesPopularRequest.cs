﻿namespace TraktApiSharp.Requests.WithoutOAuth.Movies.Common
{
    using Base;
    using Base.Get;
    using Objects.Basic;
    using Objects.Get.Movies;

    internal class TraktMoviesPopularRequest : TraktGetRequest<TraktPaginationListResult<TraktMovie>, TraktMovie>
    {
        internal TraktMoviesPopularRequest(TraktClient client) : base(client) { }

        protected override string UriTemplate => "movies/popular{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications}";

        protected override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        internet TraktMovieFilter Filter { get; set; }

        protected override bool SupportsPagination => true;

        protected override bool IsListResult => true;
    }
}
