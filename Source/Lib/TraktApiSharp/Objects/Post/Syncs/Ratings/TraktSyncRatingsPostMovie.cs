﻿namespace TraktApiSharp.Objects.Post.Syncs.Ratings
{
    using Attributes;
    using Get.Movies;
    using Newtonsoft.Json;
    using System;

    public class TraktSyncRatingsPostMovie
    {
        [JsonProperty(PropertyName = "rated_at")]
        public DateTime? RatedAt { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int? Rating { get; set; }

        [JsonProperty(PropertyName = "title")]
        [Nullable]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "ids")]
        public TraktMovieIds Ids { get; set; }
    }
}
