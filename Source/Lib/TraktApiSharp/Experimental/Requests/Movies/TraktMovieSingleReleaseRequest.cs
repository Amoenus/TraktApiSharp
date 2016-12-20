﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Base.Get;
    using Objects.Get.Movies;
    using System.Collections.Generic;
    using TraktApiSharp.Requests;

    internal sealed class TraktMovieSingleReleaseRequest : ATraktSingleItemGetByIdRequest<TraktMovieRelease>
    {
        internal TraktMovieSingleReleaseRequest(TraktClient client) : base(client) { }

        internal string LanguageCode { get; set; }

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();
            uriParams.Add("language", LanguageCode);
            return uriParams;
        }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Movies;

        public override string UriTemplate => "movies/{id}/releases/{language}";
    }
}
