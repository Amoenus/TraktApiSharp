﻿namespace TraktApiSharp.Tests.Objects.Get.Episodes
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using Traits;
    using TraktApiSharp.Objects.Get.Episodes;
    using Xunit;

    [Category("Objects.Get.Episodes")]
    public class TraktEpisodeCollectionProgress_Tests
    {
        [Fact]
        public void Test_TraktEpisodeCollectionProgress_Default_Constructor()
        {
            var episodeCollectionProgress = new TraktEpisodeCollectionProgress();

            episodeCollectionProgress.Number.Should().NotHaveValue();
            episodeCollectionProgress.Completed.Should().NotHaveValue();
            episodeCollectionProgress.CollectedAt.Should().NotHaveValue();
        }

        [Fact]
        public void Test_TraktEpisodeCollectionProgress_From_Json()
        {
            var episodeCollectionProgress = JsonConvert.DeserializeObject<TraktEpisodeCollectionProgress>(JSON);

            episodeCollectionProgress.Should().NotBeNull();
            episodeCollectionProgress.Number.Should().Be(2);
            episodeCollectionProgress.Completed.Should().BeTrue();
            episodeCollectionProgress.CollectedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());
        }

        private const string JSON =
            @"{
                ""number"": 2,
                ""completed"": true,
                ""collected_at"": ""2011-04-18T01:00:00.000Z""
              }";
    }
}
