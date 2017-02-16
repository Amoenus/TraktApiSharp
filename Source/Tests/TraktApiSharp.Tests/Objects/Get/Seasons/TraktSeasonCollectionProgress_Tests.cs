﻿namespace TraktApiSharp.Tests.Objects.Get.Seasons
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using Traits;
    using TraktApiSharp.Objects.Get.Seasons;
    using Xunit;

    [Category("Objects.Get.Seasons")]
    public class TraktSeasonCollectionProgress_Tests
    {
        [Fact]
        public void Test_TraktSeasonCollectionProgress_Default_Constructor()
        {
            var seasonCollectionProgress = new TraktSeasonCollectionProgress();

            seasonCollectionProgress.Number.Should().NotHaveValue();
            seasonCollectionProgress.Aired.Should().NotHaveValue();
            seasonCollectionProgress.Completed.Should().NotHaveValue();
            seasonCollectionProgress.Episodes.Should().BeNull();
        }

        [Fact]
        public void Test_TraktSeasonCollectionProgress_From_Json()
        {
            var seasonCollectionProgress = JsonConvert.DeserializeObject<TraktSeasonCollectionProgress>(JSON);

            seasonCollectionProgress.Should().NotBeNull();
            seasonCollectionProgress.Number.Should().Be(2);
            seasonCollectionProgress.Aired.Should().Be(3);
            seasonCollectionProgress.Completed.Should().Be(2);
            seasonCollectionProgress.Episodes.Should().NotBeNull().And.HaveCount(2);

            var episodesCollectionProgress = seasonCollectionProgress.Episodes.ToArray();

            episodesCollectionProgress[0].Should().NotBeNull();
            episodesCollectionProgress[0].Number.Should().Be(1);
            episodesCollectionProgress[0].Completed.Should().BeTrue();
            episodesCollectionProgress[0].CollectedAt.Should().Be(DateTime.Parse("2011-04-18T01:00:00.000Z").ToUniversalTime());

            episodesCollectionProgress[1].Should().NotBeNull();
            episodesCollectionProgress[1].Number.Should().Be(2);
            episodesCollectionProgress[1].Completed.Should().BeTrue();
            episodesCollectionProgress[1].CollectedAt.Should().Be(DateTime.Parse("2011-04-19T02:00:00.000Z").ToUniversalTime());
        }

        private const string JSON =
            @"{
                ""number"": 2,
                ""aired"": 3,
                ""completed"": 2,
                ""episodes"": [
                  {
                    ""number"": 1,
                    ""completed"": true,
                    ""collected_at"": ""2011-04-18T01:00:00.000Z""
                  },
                  {
                    ""number"": 2,
                    ""completed"": true,
                    ""collected_at"": ""2011-04-19T02:00:00.000Z""
                  }
                ]
              }";
    }
}
