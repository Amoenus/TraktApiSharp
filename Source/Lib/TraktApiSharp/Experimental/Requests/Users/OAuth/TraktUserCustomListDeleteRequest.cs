﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using TraktApiSharp.Requests;

    internal sealed class TraktUserCustomListDeleteRequest : ATraktUsersDeleteByIdRequest
    {
        internal TraktUserCustomListDeleteRequest(TraktClient client) : base(client) { }

        internal string Username { get; set; }

        public override TraktRequestObjectType RequestObjectType => TraktRequestObjectType.Lists;

        public override string UriTemplate => "users/{username}/lists/{id}";
    }
}
