﻿namespace TraktApiSharp.Experimental.Requests.Base.Post.Bodyless
{
    using Interfaces;
    using Interfaces.Base;
    using TraktApiSharp.Requests;

    internal abstract class ATraktPaginationBodylessPostByIdRequest<TItem> : ATraktPaginationBodylessPostRequest<TItem>, ITraktHasId, ITraktObjectRequest
    {
        internal ATraktPaginationBodylessPostByIdRequest(TraktClient client) : base(client)
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
