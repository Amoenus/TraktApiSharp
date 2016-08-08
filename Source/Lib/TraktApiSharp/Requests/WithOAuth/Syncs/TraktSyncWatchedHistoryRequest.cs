﻿namespace TraktApiSharp.Requests.WithOAuth.Syncs
{
    using Base.Get;
    using Enums;
    using Extensions;
    using Objects.Basic;
    using Objects.Get.History;
    using System;
    using System.Collections.Generic;

    internal class TraktSyncWatchedHistoryRequest : TraktGetRequest<TraktPaginationListResult<TraktHistoryItem>, TraktHistoryItem>
    {
        internal TraktSyncWatchedHistoryRequest(TraktClient client) : base(client) { }

        protected override TraktAuthorizationRequirement AuthorizationRequirement => TraktAuthorizationRequirement.Required;

        internal TraktSyncItemType Type { get; set; }

        internal string ItemId { get; set; }

        internal DateTime? StartAt { get; set; }

        internal DateTime? EndAt { get; set; }

        protected override IDictionary<string, object> GetUriPathParameters()
        {
            var uriParams = base.GetUriPathParameters();

            var isTypeSetAndValid = Type != null && Type != TraktSyncItemType.Unspecified;

            if (isTypeSetAndValid)
                uriParams.Add("type", Type.UriName);

            if (!string.IsNullOrEmpty(ItemId) && isTypeSetAndValid)
                uriParams.Add("item_id", ItemId);

            if (StartAt.HasValue)
            {
                var startAt = StartAt.Value.ToUniversalTime();
                uriParams.Add("start_at", startAt.ToTraktLongDateTimeString());
            }

            if (EndAt.HasValue)
            {
                var endAt = EndAt.Value.ToUniversalTime();
                uriParams.Add("end_at", endAt.ToTraktLongDateTimeString());
            }

            return uriParams;
        }

        protected override string UriTemplate => "sync/history{/type}{/item_id}{?start_at,end_at,extended,page,limit}";

        protected override bool SupportsPagination => true;

        protected override bool IsListResult => true;
    }
}
