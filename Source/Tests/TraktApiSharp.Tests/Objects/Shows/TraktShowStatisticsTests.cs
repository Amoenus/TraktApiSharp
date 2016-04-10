﻿namespace TraktApiSharp.Tests.Objects.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using TraktApiSharp.Objects.Shows;
    using Utils;

    [TestClass]
    public class TraktShowStatisticsTests
    {
        [TestMethod]
        public void TestTraktShowStatisticsDefaultConstructor()
        {
            var showStats = new TraktShowStatistics();

            showStats.Watchers.Should().NotHaveValue();
            showStats.Plays.Should().NotHaveValue();
            showStats.Collectors.Should().NotHaveValue();
            showStats.CollectedEpisodes.Should().NotHaveValue();
            showStats.Comments.Should().NotHaveValue();
            showStats.Lists.Should().NotHaveValue();
            showStats.Votes.Should().NotHaveValue();
        }

        [TestMethod]
        public void TestTraktShowStatisticsReadFromJson()
        {
            var jsonFile = TestUtility.ReadFileContents(@"Shows\ShowStatistics.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var showStats = JsonConvert.DeserializeObject<TraktShowStatistics>(jsonFile);

            showStats.Should().NotBeNull();
            showStats.Watchers.Should().Be(265955);
            showStats.Plays.Should().Be(12491168);
            showStats.Collectors.Should().Be(106028);
            showStats.CollectedEpisodes.Should().Be(4092901);
            showStats.Comments.Should().Be(233);
            showStats.Lists.Should().Be(103943);
            showStats.Votes.Should().Be(44590);
        }
    }
}
