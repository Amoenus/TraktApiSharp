﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Objects.Get.Movies.Common;

    internal sealed class TraktMoviesMostWatchedRequest : ATraktMoviesMostPWCRequest<TraktMostWatchedMovie>
    {
        public override string UriTemplate => "movies/watched{/period}{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications}";

        public override void Validate() { }
    }
}
