﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Objects.Get.Ratings;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserRatingsRequest : ATraktUsersListGetRequest<TraktRatingsItem>
    {
        internal TraktUserRatingsRequest(TraktClient client) : base(client) {}

        internal string Username { get; set; }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Optional;

        public override string UriTemplate => "users/{username}/ratings{/type}{/rating}{?extended}";
    }
}
