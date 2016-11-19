﻿namespace TraktApiSharp.Experimental.Requests.Shows
{
    using Base.Get;
    using Objects.Get.Users;
    using TraktApiSharp.Requests;

    internal sealed class TraktShowWatchingUsersRequest : ATraktListGetByIdRequest<TraktUser>
    {
        public TraktShowWatchingUsersRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate => "shows/{id}/watching{?extended}";
    }
}
