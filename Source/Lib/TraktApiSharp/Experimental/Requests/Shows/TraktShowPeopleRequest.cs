﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Base.Get;
    using Objects.Basic;
    using TraktApiSharp.Requests;

    internal sealed class TraktShowPeopleRequest : ATraktSingleItemGetByIdRequest<TraktCastAndCrew>
    {
        public TraktShowPeopleRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate => "shows/{id}/people{?extended}";
    }
}
