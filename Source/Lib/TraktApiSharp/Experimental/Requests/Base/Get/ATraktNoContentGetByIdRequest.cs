﻿namespace TraktApiSharp.Experimental.Requests.Base.Get
{
    using Interfaces;

    internal abstract class ATraktNoContentGetByIdRequest : ATraktNoContentGetRequest, ITraktHasId
    {
        internal ATraktNoContentGetByIdRequest(TraktClient client) : base(client)
        {
            RequestId = new TraktRequestId();
        }

        public string Id
        {
            get { return RequestId.Id; }
            set { RequestId.Id = value; }
        }

        public TraktRequestId RequestId { get; set; }
    }
}
