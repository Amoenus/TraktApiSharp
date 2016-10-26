﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;

    internal sealed class TraktMoviesTrendingRequest : ATraktMoviesRequest<TraktTrendingMovie>
    {
        internal TraktMoviesTrendingRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate => "movies/trending{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications}";
    }
}
