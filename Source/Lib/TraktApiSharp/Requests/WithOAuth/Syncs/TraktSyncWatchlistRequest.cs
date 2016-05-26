﻿namespace TraktApiSharp.Requests.WithOAuth.Syncs
{
    using Base.Get;
    using Enums;
    using Objects.Basic;
    using Objects.Get.Syncs.Watchlist;
    using System.Collections.Generic;

    internal class TraktSyncWatchlistRequest : TraktGetRequest<TraktListResult<TraktSyncWatchlistItem>, TraktSyncWatchlistItem>
    {
        internal TraktSyncWatchlistRequest(TraktClient client) : base(client) { }

        protected override TraktAuthenticationRequirement AuthenticationRequirement => TraktAuthenticationRequirement.Required;

        internal TraktSyncWatchlistItemType? Type { get; set; }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            if (Type.HasValue && Type != TraktSyncWatchlistItemType.Unspecified)
                uriParams.Add("type", Type.Value.AsString());

            return uriParams;
        }

        protected override string UriTemplate => "sync/watchlist{/type}";

        protected override bool IsListResult => true;
    }
}
