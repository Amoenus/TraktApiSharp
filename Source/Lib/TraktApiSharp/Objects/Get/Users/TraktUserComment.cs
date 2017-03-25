﻿namespace TraktApiSharp.Objects.Get.Users
{
    using Basic;
    using Enums;
    using Episodes;
    using Lists;
    using Movies;
    using Newtonsoft.Json;
    using Seasons;
    using Shows;
    using TraktApiSharp.Objects.Basic.Implementations;

    /// <summary>A Trakt user comment.</summary>
    public class TraktUserComment
    {
        /// <summary>
        /// Gets or sets the object type, which this comment contains.
        /// See also <seealso cref="TraktObjectType" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(TraktEnumerationConverter<TraktObjectType>))]
        public TraktObjectType Type { get; set; }

        /// <summary>Gets or sets the comment's content.<para>Nullable</para></summary>
        [JsonProperty(PropertyName = "comment")]
        public TraktComment Comment { get; set; }

        /// <summary>
        /// Gets or sets the movie, if <see cref="Type" /> is <see cref="TraktObjectType.Movie" />.
        /// See also <seealso cref="TraktMovie" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "movie")]
        public TraktMovie Movie { get; set; }

        /// <summary>
        /// Gets or sets the show, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="TraktShow" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "show")]
        public TraktShow Show { get; set; }

        /// <summary>
        /// Gets or sets the season, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="TraktSeason" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "season")]
        public TraktSeason Season { get; set; }

        /// <summary>
        /// Gets or sets the episode, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="TraktEpisode" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }

        /// <summary>
        /// Gets or sets the list, if <see cref="Type" /> is <see cref="TraktObjectType.Episode" />.
        /// See also <seealso cref="TraktList" />.
        /// <para>Nullable</para>
        /// </summary>
        [JsonProperty(PropertyName = "list")]
        public TraktList List { get; set; }
    }
}
