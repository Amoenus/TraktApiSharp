﻿namespace TraktApiSharp.Objects.Get.Syncs.Watchlist
{
    using Enums;
    using Movies;
    using Newtonsoft.Json;
    using Shows;
    using Shows.Episodes;
    using Shows.Seasons;
    using System;

    public class TraktSyncWatchlistItem
    {
        [JsonProperty(PropertyName = "listed_at")]
        public DateTime ListedAt { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(TraktSyncWatchlistItemTypeConverter))]
        public TraktSyncWatchlistItemType Type { get; set; }

        [JsonProperty(PropertyName = "movie")]
        public TraktMovie Movie { get; set; }

        [JsonProperty(PropertyName = "show")]
        public TraktShow Show { get; set; }

        [JsonProperty(PropertyName = "season")]
        public TraktSeason Season { get; set; }

        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }
    }
}
