﻿namespace TraktApiSharp.Objects.Post.Comments
{
    using Get.Shows.Episodes;
    using Newtonsoft.Json;
    using System;

    public class TraktEpisodeCommentPost : TraktCommentPost, IValidatable
    {
        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }

        public void Validate()
        {
            if (Episode == null)
                throw new ArgumentNullException("episode not set");

            if (Episode.Ids == null || !Episode.Ids.HasAnyId)
                throw new ArgumentException("episode ids not set");
        }
    }
}
