﻿namespace TraktApiSharp.Experimental.Requests.Base.Post.Bodyless
{
    using Interfaces.Base.Post.Bodyless;
    using System;
    using TraktApiSharp.Requests;

    internal abstract class ATraktSingleItemBodylessPostByIdRequest<TItem> : ATraktSingleItemBodylessPostRequest<TItem>, ITraktSingleItemBodylessPostByIdRequest<TItem>
    {
        internal ATraktSingleItemBodylessPostByIdRequest(TraktClient client) : base(client) { }

        public string Id { get; set; }

        public abstract TraktRequestObjectType RequestObjectType { get; }

        public virtual void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
