﻿namespace TraktApiSharp.Tests.Objects.Get.Seasons
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using Traits;
    using TraktApiSharp.Objects.Get.Seasons;
    using Xunit;

    [Category("Objects.Get.Seasons")]
    public class TraktSeasonIds_Tests
    {
        [Fact]
        public void Test_TraktSeasonIds_Default_Constructor()
        {
            var seasonIds = new TraktSeasonIds();

            seasonIds.Trakt.Should().Be(0);
            seasonIds.Tvdb.Should().BeNull();
            seasonIds.Tmdb.Should().BeNull();
            seasonIds.TvRage.Should().BeNull();
            seasonIds.HasAnyId.Should().BeFalse();
        }

        [Fact]
        public void Test_TraktSeasonIds_HasAnyId()
        {
            var seasonIds = new TraktSeasonIds { Trakt = 1 };
            seasonIds.HasAnyId.Should().BeTrue();

            seasonIds = new TraktSeasonIds { Tvdb = 1 };
            seasonIds.HasAnyId.Should().BeTrue();

            seasonIds = new TraktSeasonIds { Tmdb = 1 };
            seasonIds.HasAnyId.Should().BeTrue();

            seasonIds = new TraktSeasonIds { TvRage = 1 };
            seasonIds.HasAnyId.Should().BeTrue();
        }

        [Fact]
        public void Test_TraktSeasonIds_GetBestId()
        {
            var seasonIds = new TraktSeasonIds();

            var bestId = seasonIds.GetBestId();
            bestId.Should().NotBeNull().And.BeEmpty();

            seasonIds = new TraktSeasonIds { Trakt = 1 };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds { Tvdb = 1 };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds { Tmdb = 1 };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds { TvRage = 1 };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds
            {
                Trakt = 1,
                Tvdb = 1,
                Tmdb = 1,
                TvRage = 1
            };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds
            {
                Tvdb = 1,
                Tmdb = 1,
                TvRage = 1
            };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");

            seasonIds = new TraktSeasonIds
            {
                Tmdb = 1,
                TvRage = 1
            };

            bestId = seasonIds.GetBestId();
            bestId.Should().Be("1");
        }

        [Fact]
        public void Test_TraktSeasonIds_From_Json()
        {
            var seasonIds = JsonConvert.DeserializeObject<TraktSeasonIds>(JSON);

            seasonIds.Should().NotBeNull();
            seasonIds.Trakt.Should().Be(61430);
            seasonIds.Tvdb.Should().Be(279121U);
            seasonIds.Tmdb.Should().Be(60523U);
            seasonIds.TvRage.Should().Be(36939U);
            seasonIds.HasAnyId.Should().BeTrue();
        }

        private const string JSON =
            @"{
                ""trakt"": 61430,
                ""tvdb"": 279121,
                ""tmdb"": 60523,
                ""tvrage"": 36939
              }";
    }
}
