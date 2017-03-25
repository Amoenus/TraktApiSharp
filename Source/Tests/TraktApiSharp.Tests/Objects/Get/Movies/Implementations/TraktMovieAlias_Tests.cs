﻿namespace TraktApiSharp.Tests.Objects.Get.Movies.Implementations
{
    using FluentAssertions;
    using Traits;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Objects.Get.Movies.Implementations;
    using TraktApiSharp.Objects.JsonReader.Get.Movies;
    using Xunit;

    [Category("Objects.Get.Movies.Implementations")]
    public class TraktMovieAlias_Tests
    {
        [Fact]
        public void Test_TraktMovieAlias_Default_Constructor()
        {
            var movieAlias = new TraktMovieAlias();

            movieAlias.Title.Should().BeNullOrEmpty();
            movieAlias.CountryCode.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Test_TraktMovieAlias_From_Json()
        {
            var jsonReader = new TraktMovieAliasObjectJsonReader();
            var movieAlias = jsonReader.ReadObject(JSON);

            movieAlias.Should().NotBeNull();
            movieAlias.Title.Should().Be("Star Wars: The Force Awakens");
            movieAlias.CountryCode.Should().Be("us");
        }

        private const string JSON =
            @"{
                ""title"": ""Star Wars: The Force Awakens"",
                ""country"": ""us""
              }";
    }
}
