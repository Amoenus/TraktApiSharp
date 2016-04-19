﻿namespace TraktApiSharp.Objects.Checkins
{
    using Get.Shows;
    using Get.Shows.Episodes;
    using Newtonsoft.Json;

    public class TraktCheckinEpisode : TraktCheckin
    {
        [JsonProperty(PropertyName = "episode")]
        public TraktEpisode Episode { get; set; }

        [JsonProperty(PropertyName = "show")]
        public TraktShow Show { get; set; }
    }
}
