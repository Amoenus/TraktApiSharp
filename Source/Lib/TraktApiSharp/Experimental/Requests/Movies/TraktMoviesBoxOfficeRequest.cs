﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Base.Get;
    using Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;

    internal sealed class TraktMoviesBoxOfficeRequest : ATraktListGetRequest<TraktBoxOfficeMovie>
    {
        internal TraktMoviesBoxOfficeRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate => "movies/boxoffice{?extended}";
    }
}
