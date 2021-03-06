﻿namespace TraktApiSharp.Objects.Post.Scrobbles.Responses
{
    using Get.Episodes;
    using Get.Shows;
    using Newtonsoft.Json;

    /// <summary>Represents an episode scrobble response.</summary>
    public class TraktEpisodeScrobblePostResponse : TraktScrobblePostResponse
    {
        /// <summary>
        /// Gets or sets the Trakt episode, which was scrobbled.
        /// See also <seealso cref="TraktEpisode" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }

        /// <summary>
        /// Gets or sets the Trakt show for the episode, which was scrobbled.
        /// See also <seealso cref="TraktShow" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "show")]
        public TraktShow Show { get; set; }
    }
}
