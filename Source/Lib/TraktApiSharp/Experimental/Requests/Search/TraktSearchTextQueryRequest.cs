﻿namespace TraktApiSharp.Experimental.Requests.Search
{
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktSearchTextQueryRequest : ATraktSearchRequest
    {
        public TraktSearchTextQueryRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
