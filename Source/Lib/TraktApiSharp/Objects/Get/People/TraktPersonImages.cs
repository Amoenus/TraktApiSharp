﻿namespace TraktApiSharp.Objects.Get.People
{
    using Basic;
    using Newtonsoft.Json;

    /// <summary>A collection of images and image sets for a Trakt person.</summary>
    public class TraktPersonImages
    {
        /// <summary>Gets or sets the headshot image set. See also <seealso cref="TraktImageSet" />.<para>Nullable</para></summary>
        [JsonProperty(PropertyName = "headshot")]
        public TraktImageSet Headshot { get; set; }

        /// <summary>Gets or sets the fan art image set. See also <seealso cref="TraktImageSet" />.<para>Nullable</para></summary>
        [JsonProperty(PropertyName = "fanart")]
        public TraktImageSet FanArt { get; set; }
    }
}
