﻿namespace TraktApiSharp.Experimental.Requests.Base.Post
{
    using Interfaces.Base;
    using Interfaces.Base.Post;
    using System.Net.Http;
    using TraktApiSharp.Requests;

    internal abstract class ATraktListPostRequest<TItem, TRequestBody> : ATraktListRequest<TItem>, ITraktListPostRequest<TItem, TRequestBody>
    {
        internal ATraktListPostRequest(TraktClient client) : base(client)
        {
            RequestBody = new TraktRequestBody<TRequestBody>();
        }

        public override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Required;

        public override HttpMethod Method => HttpMethod.Post;

        public ITraktPostable<TRequestBody> RequestBody { get; set; }

        public TRequestBody RequestBodyContent
        {
            get { return RequestBody.RequestBody; }
            set { RequestBody.RequestBody = value; }
        }
    }
}
