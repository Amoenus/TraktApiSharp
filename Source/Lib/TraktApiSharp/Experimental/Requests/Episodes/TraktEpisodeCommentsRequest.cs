﻿namespace TraktApiSharp.Experimental.Requests.Episodes
{
    using Base.Get;
    using Interfaces;
    using Objects.Basic;
    using TraktApiSharp.Requests;

    internal sealed class TraktEpisodeCommentsRequest : ATraktPaginationGetByIdRequest<TraktComment>, ITraktObjectRequest
    {
        public TraktEpisodeCommentsRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Episodes;

        public override string UriTemplate => "shows/{id}/seasons/{season}/episodes/{episode}/comments{/sorting}{?page,limit}";
    }
}
