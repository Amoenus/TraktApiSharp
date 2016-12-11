﻿namespace TraktApiSharp.Experimental.Requests.Syncs.OAuth
{
    using Objects.Post.Syncs.Collection;
    using Objects.Post.Syncs.Collection.Responses;
    using System;

    internal sealed class TraktSyncCollectionAddRequest : ATraktSyncSingleItemPostRequest<TraktSyncCollectionPostResponse, TraktSyncCollectionPost>
    {
        internal TraktSyncCollectionAddRequest(TraktClient client) : base(client) { }

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
