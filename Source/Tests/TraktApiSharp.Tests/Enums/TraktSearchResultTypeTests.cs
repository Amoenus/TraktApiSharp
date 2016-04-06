﻿namespace TraktApiSharp.Tests.Enums
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Enums;

    [TestClass]
    public class TraktSearchResultTypeTests
    {
        [TestMethod]
        public void TestTraktSearchResultTypeHasMembers()
        {
            typeof(TraktSearchResultType).GetEnumNames().Should().HaveCount(5)
                                                        .And.Contain("Movie", "Show", "Episode", "Person", "List");
        }

        [TestMethod]
        public void TestTraktSearchResultTypeGetAsString()
        {
            TraktSearchResultType.Movie.AsString().Should().Be("movie");
            TraktSearchResultType.Show.AsString().Should().Be("show");
            TraktSearchResultType.Episode.AsString().Should().Be("episode");
            TraktSearchResultType.Person.AsString().Should().Be("person");
            TraktSearchResultType.List.AsString().Should().Be("list");
        }
    }
}
