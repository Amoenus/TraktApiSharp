﻿namespace TraktApiSharp.Requests.WithOAuth.Users
{
    using Base.Get;
    using Objects.Basic;
    using Objects.Get.Users.Collections;
    using System.Collections.Generic;

    internal class TraktUserCollectionShowsRequest : TraktGetRequest<TraktListResult<TraktUserCollectionShowItem>, TraktUserCollectionShowItem>
    {
        internal TraktUserCollectionShowsRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.Optional;

        internal string Username { get; set; }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();
            uriParams.Add("username", Username);
            return uriParams;
        }

        protected override string UriTemplate => "users/{username}/collection/shows";

        protected override bool IsListResult => true;
    }
}
