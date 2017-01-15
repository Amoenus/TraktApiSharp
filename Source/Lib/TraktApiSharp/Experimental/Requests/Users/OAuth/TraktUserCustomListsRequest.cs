﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Base.Get;
    using Objects.Get.Users.Lists;
    using System.Collections.Generic;
    using TraktApiSharp.Requests;

    internal sealed class TraktUserCustomListsRequest : ATraktListGetRequest<TraktList>
    {
        internal TraktUserCustomListsRequest(TraktClient client) : base(client) {}

        internal string Username { get; set; }

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();
            uriParams.Add("username", Username);
            return uriParams;
        }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Optional;

        public override string UriTemplate => "users/{username}/lists";
    }
}
