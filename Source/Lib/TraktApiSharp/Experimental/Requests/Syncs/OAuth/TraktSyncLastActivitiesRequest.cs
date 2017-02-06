﻿namespace TraktApiSharp.Experimental.Requests.Syncs.OAuth
{
    using Objects.Get.Syncs.Activities;

    internal sealed class TraktSyncLastActivitiesRequest : ATraktSyncGetRequest<TraktSyncLastActivities>
    {
        public override string UriTemplate => "sync/last_activities";
    }
}
