﻿namespace TraktApiSharp.Experimental.Requests.Base.Get
{
    using System.Net.Http;
    using TraktApiSharp.Requests;

    internal abstract class ATraktSingleItemGetRequest<TItem> : ATraktSingleItemRequest<TItem>
    {
        internal ATraktSingleItemGetRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement { get; }

        public override HttpMethod Method => HttpMethod.Get;
    }
}
