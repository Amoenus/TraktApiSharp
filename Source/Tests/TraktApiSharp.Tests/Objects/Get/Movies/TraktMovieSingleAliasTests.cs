﻿namespace TraktApiSharp.Tests.Objects.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using TraktApiSharp.Objects.Get.Movies;
    using Utils;

    [TestClass]
    public class TraktMovieSingleAliasTests
    {
        [TestMethod]
        public void TestTraktMovieSingleAliasDefaultConstructor()
        {
            var alias = new TraktMovieAlias();

            alias.Title.Should().BeNullOrEmpty();
            alias.CountryCode.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void TestTraktMovieSingleAliasReadFromJson()
        {
            var jsonFile = TestUtility.ReadFileContents(@"Objects\Get\Movies\MovieSingleAlias.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var alias = JsonConvert.DeserializeObject<TraktMovieAlias>(jsonFile);

            alias.Should().NotBeNull();
            alias.Title.Should().Be("La guerra de las galaxias. Episodio 7. El despertar de la Fuerza.");
            alias.CountryCode.Should().Be("es");
        }
    }
}
