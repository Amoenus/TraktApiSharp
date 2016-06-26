﻿namespace TraktApiSharp.Objects.Post.Syncs.Collection
{
    using Basic;
    using Get.Shows;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class TraktSyncCollectionPostShow
    {
        [JsonProperty(PropertyName = "collected_at")]
        public DateTime? CollectedAt { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "ids")]
        public TraktShowIds Ids { get; set; }

        [JsonProperty(PropertyName = "seasons")]
        public IEnumerable<TraktSyncCollectionPostShowSeason> Seasons { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public TraktMetadata Metadata { get; set; }
    }
}
