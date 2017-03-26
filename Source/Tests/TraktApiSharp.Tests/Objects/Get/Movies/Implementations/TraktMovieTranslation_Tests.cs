﻿namespace TraktApiSharp.Tests.Objects.Get.Movies.Implementations
{
    using FluentAssertions;
    using Traits;
    using TraktApiSharp.Objects.Basic.Implementations;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Objects.Get.Movies.Implementations;
    using TraktApiSharp.Objects.JsonReader.Get.Movies;
    using Xunit;

    [Category("Objects.Get.Movies.Implementations")]
    public class TraktMovieTranslation_Tests
    {
        [Fact]
        public void Test_TraktMovieTranslation_Inherits_TraktTranslation()
        {
            typeof(TraktMovieTranslation).GetInterfaces().Should().Contain(typeof(TraktTranslation));
        }

        [Fact]
        public void Test_TraktMovieTranslation_Implements_ITraktMovieTranslation_Interface()
        {
            typeof(TraktMovieTranslation).GetInterfaces().Should().Contain(typeof(ITraktMovieTranslation));
        }

        [Fact]
        public void Test_TraktMovieTranslation_Default_Constructor()
        {
            var movieTranslation = new TraktMovieTranslation();

            movieTranslation.Title.Should().BeNullOrEmpty();
            movieTranslation.Overview.Should().BeNullOrEmpty();
            movieTranslation.Tagline.Should().BeNullOrEmpty();
            movieTranslation.LanguageCode.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Test_TraktMovieTranslation_From_Json()
        {
            var jsonReader = new TraktMovieTranslationObjectJsonReader();
            var movieTranslation = jsonReader.ReadObject(JSON);

            movieTranslation.Should().NotBeNull();
            movieTranslation.Title.Should().Be("Star Wars: Episode VII - The Force Awakens");
            movieTranslation.Overview.Should().Be("A continuation of the saga created by George Lucas, set thirty years after Star Wars: Episode VI – Return of the Jedi.");
            movieTranslation.Tagline.Should().Be("The Force Lives On...");
            movieTranslation.LanguageCode.Should().Be("en");
        }

        private const string JSON =
            @"{
                ""title"": ""Star Wars: Episode VII - The Force Awakens"",
                ""overview"": ""A continuation of the saga created by George Lucas, set thirty years after Star Wars: Episode VI – Return of the Jedi."",
                ""tagline"": ""The Force Lives On..."",
                ""language"": ""en""
              }";
    }
}
