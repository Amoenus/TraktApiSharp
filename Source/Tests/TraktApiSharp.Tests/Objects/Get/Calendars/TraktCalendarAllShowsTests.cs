﻿namespace TraktApiSharp.Tests.Objects.Get.Calendars
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TraktApiSharp.Objects.Get.Calendars;
    using Utils;

    [TestClass]
    public class TraktCalendarAllShowsTests
    {
        [TestMethod]
        public void TestTraktCalendarAllShowsDefaultConstructor()
        {
            var allShowsItem = new TraktCalendarShowItem();

            allShowsItem.FirstAired.Should().Be(DateTime.MinValue);
            allShowsItem.Episode.Should().BeNull();
            allShowsItem.Show.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktCalendarAllShowsReadFromJson()
        {
            var jsonFile = TestUtility.ReadFileContents(@"Objects\Get\Calendars\CalendarAllShows.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var allShows = JsonConvert.DeserializeObject<IEnumerable<TraktCalendarShowItem>>(jsonFile);

            allShows.Should().NotBeNull().And.HaveCount(2);

            var calendarShows = allShows.ToArray();

            calendarShows[0].FirstAired.Should().Be(DateTime.Parse("2014-07-14T01:00:00.000Z").ToUniversalTime());
            calendarShows[0].Episode.Should().NotBeNull();
            calendarShows[0].Episode.SeasonNumber.Should().Be(7);
            calendarShows[0].Episode.Number.Should().Be(4);
            calendarShows[0].Episode.Title.Should().Be("Death is Not the End");
            calendarShows[0].Episode.Ids.Should().NotBeNull();
            calendarShows[0].Episode.Ids.Trakt.Should().Be(443);
            calendarShows[0].Episode.Ids.Tvdb.Should().Be(4851180);
            calendarShows[0].Episode.Ids.Imdb.Should().Be("tt3500614");
            calendarShows[0].Episode.Ids.Tmdb.Should().Be(988123);
            calendarShows[0].Episode.Ids.TvRage.Should().NotHaveValue();
            calendarShows[0].Show.Should().NotBeNull();
            calendarShows[0].Show.Title.Should().Be("True Blood");
            calendarShows[0].Show.Year.Should().Be(2008);
            calendarShows[0].Show.Ids.Should().NotBeNull();
            calendarShows[0].Show.Ids.Trakt.Should().Be(5);
            calendarShows[0].Show.Ids.Slug.Should().Be("true-blood");
            calendarShows[0].Show.Ids.Tvdb.Should().Be(82283);
            calendarShows[0].Show.Ids.Imdb.Should().Be("tt0844441");
            calendarShows[0].Show.Ids.Tmdb.Should().Be(10545);
            calendarShows[0].Show.Ids.TvRage.Should().Be(12662);

            calendarShows[1].FirstAired.Should().Be(DateTime.Parse("2014-07-14T02:00:00.000Z").ToUniversalTime());
            calendarShows[1].Episode.Should().NotBeNull();
            calendarShows[1].Episode.SeasonNumber.Should().Be(1);
            calendarShows[1].Episode.Number.Should().Be(3);
            calendarShows[1].Episode.Title.Should().Be("Two Boats and a Helicopter");
            calendarShows[1].Episode.Ids.Should().NotBeNull();
            calendarShows[1].Episode.Ids.Trakt.Should().Be(499);
            calendarShows[1].Episode.Ids.Tvdb.Should().Be(4854797);
            calendarShows[1].Episode.Ids.Imdb.Should().Be("tt3631218");
            calendarShows[1].Episode.Ids.Tmdb.Should().Be(988346);
            calendarShows[1].Episode.Ids.TvRage.Should().NotHaveValue();
            calendarShows[1].Show.Should().NotBeNull();
            calendarShows[1].Show.Title.Should().Be("The Leftovers");
            calendarShows[1].Show.Year.Should().Be(2014);
            calendarShows[1].Show.Ids.Should().NotBeNull();
            calendarShows[1].Show.Ids.Trakt.Should().Be(7);
            calendarShows[1].Show.Ids.Slug.Should().Be("the-leftovers");
            calendarShows[1].Show.Ids.Tvdb.Should().Be(269689);
            calendarShows[1].Show.Ids.Imdb.Should().Be("tt2699128");
            calendarShows[1].Show.Ids.Tmdb.Should().Be(54344);
            calendarShows[1].Show.Ids.TvRage.Should().NotHaveValue();
        }
    }
}
