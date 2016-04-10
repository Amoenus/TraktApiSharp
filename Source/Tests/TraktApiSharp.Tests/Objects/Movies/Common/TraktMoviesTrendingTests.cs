﻿namespace TraktApiSharp.Tests.Objects.Movies.Common
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using TraktApiSharp.Objects.Movies.Common;
    using Utils;

    [TestClass]
    public class TraktMoviesTrendingTests
    {
        [TestMethod]
        public void TestTraktMoviesTrendingDefaultConstructor()
        {
            var trendingShow = new TraktMoviesTrendingItem();

            trendingShow.Watchers.Should().NotHaveValue();
            trendingShow.Movie.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktMoviesTrendingReadFromJson()
        {
            var jsonFile = TestUtility.ReadFileContents(@"Movies\Common\MoviesTrending.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var trendingMovies = JsonConvert.DeserializeObject<IEnumerable<TraktMoviesTrendingItem>>(jsonFile);

            trendingMovies.Should().NotBeNull().And.HaveCount(2);

            var movies = trendingMovies.ToArray();

            movies[0].Watchers.Should().Be(35);
            movies[0].Movie.Should().NotBeNull();

            movies[1].Watchers.Should().Be(33);
            movies[1].Movie.Should().NotBeNull();
        }
    }
}