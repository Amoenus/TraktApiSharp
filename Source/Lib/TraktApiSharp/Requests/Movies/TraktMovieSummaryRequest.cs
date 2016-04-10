﻿namespace TraktApiSharp.Requests.Movies
{
    using Base.Get;
    using Objects.Movies;

    internal class TraktMovieSummaryRequest : TraktGetByIdRequest<TraktMovie, TraktMovie>
    {
        internal TraktMovieSummaryRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.NotRequired;

        protected override string UriTemplate => "movies/{id}";

        protected override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Movies;
    }
}
