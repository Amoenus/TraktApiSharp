﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Objects.Get.Watched;
    using System.Collections.Generic;

    internal sealed class TraktUserWatchedShowsRequest : ATraktUsersListGetRequest<TraktWatchedShow>
    {
        internal TraktUserWatchedShowsRequest(TraktClient client) : base(client) {}

        internal string Username { get; set; }

        public IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = new Dictionary<string, object>();
            uriParams.Add("username", Username);
            return uriParams;
        }

        public string UriTemplate => "users/{username}/watched/shows{?extended}";
    }
}
