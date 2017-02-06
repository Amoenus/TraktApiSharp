﻿namespace TraktApiSharp.Experimental.Requests.Users.OAuth
{
    using Objects.Get.Users;
    using System.Collections.Generic;

    internal sealed class TraktUserFriendsRequest //: ATraktUsersListGetRequest<TraktUserFriend>
    {
        internal TraktUserFriendsRequest(TraktClient client)  {}

        internal string Username { get; set; }

        public IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = new Dictionary<string, object>();
            uriParams.Add("username", Username);
            return uriParams;
        }
        
        public string UriTemplate => "users/{username}/friends{?extended}";
    }
}
