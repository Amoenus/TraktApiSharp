﻿namespace TraktApiSharp.Objects.Get.Movies.Common
{
    using Newtonsoft.Json;

    /// <summary>A trending Trakt movie.</summary>
    public class TraktTrendingMovie
    {
        /// <summary>Gets or sets the watcher count for the <see cref="Movie" />.</summary>
        [JsonProperty(PropertyName = "watchers")]
        public int? Watchers { get; set; }

        /// <summary>Gets or sets the Trakt movie.</summary>
        [JsonProperty(PropertyName = "movie")]
        public TraktMovie Movie { get; set; }
    }
}
