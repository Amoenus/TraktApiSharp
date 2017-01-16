﻿namespace TraktApiSharp.Experimental.Requests.Base.Get
{
    using Interfaces.Base;
    using System;
    using TraktApiSharp.Requests;

    internal abstract class ATraktNoContentGetByIdRequest : ATraktNoContentGetRequest, ITraktHasId
    {
        internal ATraktNoContentGetByIdRequest(TraktClient client) : base(client) { }

        public string Id { get; set; }

        public abstract TraktRequestObjectType RequestObjectType { get; }

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
