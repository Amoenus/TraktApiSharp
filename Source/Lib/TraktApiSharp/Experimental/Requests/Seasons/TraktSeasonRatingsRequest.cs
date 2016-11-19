﻿namespace TraktApiSharp.Experimental.Requests.Seasons
{
    using Base.Get;
    using Interfaces;
    using Objects.Basic;
    using TraktApiSharp.Requests;

    internal sealed class TraktSeasonRatingsRequest : ATraktSingleItemGetByIdRequest<TraktRating>, ITraktObjectRequest
    {
        internal TraktSeasonRatingsRequest(TraktClient client) : base(client) { }

        internal uint SeasonNumber { get; set; }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Seasons;

        public override string UriTemplate => "shows/{id}/seasons/{season}/ratings";
    }
}
