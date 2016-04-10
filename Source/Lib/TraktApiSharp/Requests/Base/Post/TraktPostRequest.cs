﻿namespace TraktApiSharp.Requests.Base.Post
{
    using System;
    using System.Net.Http;

    internal abstract class TraktPostRequest<TResult, TItem> : TraktRequest<TResult, TItem>
    {
        protected TraktPostRequest(TraktClient client) : base(client) { }

        protected override HttpMethod Method { get { return HttpMethod.Post; } }

        protected override TraktAuthenticationRequirement AuthenticationRequirement { get { return TraktAuthenticationRequirement.Required; } }

        protected override void Validate()
        {
            base.Validate();

            if (RequestBody == null)
                throw new ArgumentException("request body not valid");
        }
    }
}
