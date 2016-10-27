﻿namespace TraktApiSharp.Experimental.Requests.Movies
{
    using Base.Get;
    using Objects.Basic;
    using TraktApiSharp.Requests;

    internal sealed class TraktMoviePeopleRequest : ATraktSingleItemGetByIdRequest<TraktCastAndCrew>
    {
        internal TraktMoviePeopleRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate => "movies/{id}/people{?extended}";
    }
}
