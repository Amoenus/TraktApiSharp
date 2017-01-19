﻿namespace TraktApiSharp.Experimental.Requests.Base
{
    using System.Net.Http;
    using TraktApiSharp.Experimental.Requests.Interfaces.Base;
    using TraktApiSharp.Requests;

    internal abstract class TraktBodylessPostRequest<TContentType> : TraktRequest<TContentType>, ITraktBodylessPostRequest<TContentType>
    {
        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Required;

        public sealed override HttpMethod Method => HttpMethod.Post;
    }
}
