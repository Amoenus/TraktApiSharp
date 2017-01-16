﻿namespace TraktApiSharp.Experimental.Requests.Base.Post
{
    using Interfaces;
    using Interfaces.Base;
    using TraktApiSharp.Requests;

    internal abstract class ATraktSingleItemPostByIdRequest<TItem, TRequestBody> : ATraktSingleItemPostRequest<TItem, TRequestBody>, ITraktHasId, ITraktObjectRequest
    {
        internal ATraktSingleItemPostByIdRequest(TraktClient client) : base(client)
        {
            RequestId = new TraktRequestId();
        }

        public string Id
        {
            get { return RequestId.Id; }
            set { RequestId.Id = value; }
        }

        public TraktRequestId RequestId { get; set; }

        public abstract TraktRequestObjectType RequestObjectType { get; }
    }
}
