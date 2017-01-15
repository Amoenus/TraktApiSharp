﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Base.Get;
    using Objects.Get.Users;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserLikesRequest : ATraktPaginationGetRequest<TraktUserLikeItem>
    {
        internal TraktUserLikesRequest(TraktClient client) : base(client) {}

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Required;

        public override string UriTemplate => "users/likes{/type}{?page,limit}";
    }
}
