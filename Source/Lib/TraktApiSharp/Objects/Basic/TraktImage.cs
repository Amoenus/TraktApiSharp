﻿namespace TraktApiSharp.Objects.Basic
{
    using Newtonsoft.Json;

    /// <summary>
    /// An image for an item available on only one size.
    /// </summary>
    public class TraktImage
    {
        /// <summary>
        /// The address to the full size image.
        /// </summary>
        [JsonProperty(PropertyName = "full")]
        public string Full { get; set; }
    }
}
