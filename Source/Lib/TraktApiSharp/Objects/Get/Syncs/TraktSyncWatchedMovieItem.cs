﻿namespace TraktApiSharp.Objects.Get.Syncs
{
    using Movies;
    using Newtonsoft.Json;
    using System;

    public class TraktSyncWatchedMovieItem
    {
        [JsonProperty(PropertyName = "plays")]
        public int Plays { get; set; }

        [JsonProperty(PropertyName = "last_watched_at")]
        public DateTime LastWatchedAt { get; set; }

        [JsonProperty(PropertyName = "movie")]
        public TraktMovie Movie { get; set; }
    }
}
