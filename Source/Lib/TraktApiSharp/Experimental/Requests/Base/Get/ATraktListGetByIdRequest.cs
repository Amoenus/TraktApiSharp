﻿namespace TraktApiSharp.Experimental.Requests.Base.Get
{
    using Interfaces;
    using TraktApiSharp.Requests;

    internal abstract class ATraktListGetByIdRequest<TItem> : ATraktListGetRequest<TItem>, ITraktHasId, ITraktObjectRequest
    {
        internal ATraktListGetByIdRequest(TraktClient client) : base(client)
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
