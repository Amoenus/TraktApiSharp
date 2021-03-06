﻿namespace TraktApiSharp.Requests.Base
{
    using Interfaces.Base;
    using System.Net.Http;

    internal abstract class ATraktGetRequest<TContentType> : ATraktRequest<TContentType>, ITraktGetRequest<TContentType>
    {
        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.NotRequired;

        public sealed override HttpMethod Method => HttpMethod.Get;
    }
}
