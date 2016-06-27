﻿namespace TraktApiSharp.Requests.WithoutOAuth.Shows
{
    using Base.Get;
    using Objects.Basic;
    using Objects.Get.Users;

    internal class TraktShowWatchingUsersRequest : TraktGetByIdRequest<TraktListResult<TraktUser>, TraktUser>
    {
        internal TraktShowWatchingUsersRequest(TraktClient client) : base(client) { }

        protected override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        protected override string UriTemplate => "shows/{id}/watching{?extended}";

        protected override TraktRequestObjectType? RequestObjectType => TraktRequestObjectType.Shows;

        protected override bool IsListResult => true;
    }
}
