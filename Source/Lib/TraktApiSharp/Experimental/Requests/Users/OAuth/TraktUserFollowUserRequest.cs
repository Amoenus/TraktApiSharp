﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Objects.Post.Users.Responses;
    using System.Collections.Generic;

    internal sealed class TraktUserFollowUserRequest
    {
        internal TraktUserFollowUserRequest(TraktClient client) {}

        internal string Username { get; set; }

        public IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = new Dictionary<string, object>();
            uriParams.Add("username", Username);
            return uriParams;
        }

        public string UriTemplate => "users/{username}/follow";
    }
}
