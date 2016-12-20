﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Base.Get;
    using Objects.Basic;
    using TraktApiSharp.Requests;

    internal sealed class TraktShowRatingsRequest : ATraktSingleItemGetByIdRequest<TraktRating>
    {
        internal TraktShowRatingsRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Shows;

        public override string UriTemplate => "shows/{id}/ratings";
    }
}
