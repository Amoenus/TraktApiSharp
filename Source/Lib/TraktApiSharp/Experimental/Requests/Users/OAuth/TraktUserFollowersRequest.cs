﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Objects.Get.Users;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserFollowersRequest : ATraktUsersListGetRequest<TraktUserFollower>
    {
        internal TraktUserFollowersRequest(TraktClient client) : base(client) {}

        internal string Username { get; set; }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Optional;

        public override string UriTemplate => "users/{username}/followers{?extended}";
    }
}
