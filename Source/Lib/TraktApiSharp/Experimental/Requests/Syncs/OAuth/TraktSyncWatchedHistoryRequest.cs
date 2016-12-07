﻿namespace TraktApiSharp.Experimental.Requests.Syncs.OAuth
{
    using Objects.Get.History;
    using System;
    using TraktApiSharp.Requests;

    internal sealed class TraktSyncWatchedHistoryRequest : ATraktSyncPaginationRequest<TraktHistoryItem>
    {
        internal TraktSyncWatchedHistoryRequest(TraktClient client) : base(client) { }

        public override TraktAuthorizationRequirement AuthorizationRequirement
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string UriTemplate
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
