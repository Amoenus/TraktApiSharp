﻿namespace TraktApiSharp.Tests.Objects.Get.Episodes
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using Traits;
    using TraktApiSharp.Objects.Get.Episodes;
    using Xunit;

    [Category("Objects.Get.Episodes")]
    public class TraktEpisodeWatchedProgress_Tests
    {
        [Fact]
        public void Test_TraktEpisodeWatchedProgress_Default_Constructor()
        {
            var episodeWatchedProgress = new TraktEpisodeWatchedProgress();

            episodeWatchedProgress.Number.Should().NotHaveValue();
            episodeWatchedProgress.Completed.Should().NotHaveValue();
            episodeWatchedProgress.LastWatchedAt.Should().NotHaveValue();
        }

        [Fact]
        public void Test_TraktEpisodeWatchedProgress_From_Json()
        {
            var episodeWatchedProgress = JsonConvert.DeserializeObject<TraktEpisodeWatchedProgress>(JSON);

            episodeWatchedProgress.Should().NotBeNull();
            episodeWatchedProgress.Number.Should().Be(2);
            episodeWatchedProgress.Completed.Should().BeFalse();
            episodeWatchedProgress.LastWatchedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        private const string JSON =
            @"{
                ""number"": 2,
                ""completed"": false,
                ""last_watched_at"": ""2011-04-18T01:00:00.000Z""
              }";
    }
}
