﻿namespace TraktApiSharp.Tests.Objects.Get.Users
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Objects.Get.Users;
    using Utils;

    [TestClass]
    public class TraktUserHistoryItemTests
    {
        [TestMethod]
        public void TestTraktUserHistoryItemDefaultConstructor()
        {
            var userHistoryItem = new TraktUserHistoryItem();

            userHistoryItem.Id.Should().Be(0);
            userHistoryItem.WatchedAt.Should().Be(DateTime.MinValue);
            userHistoryItem.Action.Should().Be(TraktSyncHistoryActionType.Unspecified);
            userHistoryItem.Type.Should().Be(TraktSyncHistoryItemType.Unspecified);
            userHistoryItem.Movie.Should().BeNull();
            userHistoryItem.Show.Should().BeNull();
            userHistoryItem.Season.Should().BeNull();
            userHistoryItem.Episode.Should().BeNull();
        }

        [TestMethod]
        public void TestTraktUserHistoryItemReadFromJson()
        {
            var jsonFile = TestUtility.ReadFileContents(@"Objects\Get\Users\UserHistory.json");

            jsonFile.Should().NotBeNullOrEmpty();

            var userHistoryItems = JsonConvert.DeserializeObject<IEnumerable<TraktUserHistoryItem>>(jsonFile);

            userHistoryItems.Should().NotBeNull();

            var items = userHistoryItems.ToArray();

            items[0].Id.Should().Be(1982346);
            items[0].WatchedAt.Should().Be(DateTime.Parse("2014-03-31T09:28:53.000Z").ToUniversalTime());
            items[0].Action.Should().Be(TraktSyncHistoryActionType.Scrobble);
            items[0].Type.Should().Be(TraktSyncHistoryItemType.Movie);
            items[0].Movie.Should().NotBeNull();
            items[0].Movie.Title.Should().Be("The Dark Knight");
            items[0].Movie.Year.Should().Be(2008);
            items[0].Movie.Ids.Should().NotBeNull();
            items[0].Movie.Ids.Trakt.Should().Be(4);
            items[0].Movie.Ids.Slug.Should().Be("the-dark-knight-2008");
            items[0].Movie.Ids.Imdb.Should().Be("tt0468569");
            items[0].Movie.Ids.Tmdb.Should().Be(155);
            items[0].Show.Should().BeNull();
            items[0].Season.Should().BeNull();
            items[0].Episode.Should().BeNull();

            items[1].Id.Should().Be(1982347);
            items[1].WatchedAt.Should().Be(DateTime.Parse("2014-02-27T09:28:53.000Z").ToUniversalTime());
            items[1].Action.Should().Be(TraktSyncHistoryActionType.Checkin);
            items[1].Type.Should().Be(TraktSyncHistoryItemType.Episode);
            items[1].Movie.Should().BeNull();
            items[1].Show.Should().NotBeNull();
            items[1].Show.Title.Should().Be("Parks and Recreation");
            items[1].Show.Year.Should().Be(2009);
            items[1].Show.Ids.Should().NotBeNull();
            items[1].Show.Ids.Trakt.Should().Be(4);
            items[1].Show.Ids.Slug.Should().Be("parks-and-recreation");
            items[1].Show.Ids.Tvdb.Should().Be(84912);
            items[1].Show.Ids.Imdb.Should().Be("tt1266020");
            items[1].Show.Ids.Tmdb.Should().Be(8592);
            items[1].Show.Ids.TvRage.Should().Be(21686);
            items[1].Season.Should().BeNull();
            items[1].Episode.Should().NotBeNull();
            items[1].Episode.SeasonNumber.Should().Be(2);
            items[1].Episode.Number.Should().Be(1);
            items[1].Episode.Title.Should().Be("Pawnee Zoo");
            items[1].Episode.Ids.Should().NotBeNull();
            items[1].Episode.Ids.Trakt.Should().Be(251);
            items[1].Episode.Ids.Tvdb.Should().Be(797571);
            items[1].Episode.Ids.Imdb.Should().BeNull();
            items[1].Episode.Ids.Tmdb.Should().Be(397629);
            items[1].Episode.Ids.TvRage.Should().NotHaveValue();

            items[2].Id.Should().Be(1982348);
            items[2].WatchedAt.Should().Be(DateTime.Parse("2013-06-15T05:54:27.000Z").ToUniversalTime());
            items[2].Action.Should().Be(TraktSyncHistoryActionType.Checkin);
            items[2].Type.Should().Be(TraktSyncHistoryItemType.Show);
            items[2].Movie.Should().BeNull();
            items[2].Show.Should().NotBeNull();
            items[2].Show.Title.Should().Be("Parks and Recreation");
            items[2].Show.Year.Should().Be(2009);
            items[2].Show.Ids.Should().NotBeNull();
            items[2].Show.Ids.Trakt.Should().Be(4);
            items[2].Show.Ids.Slug.Should().Be("parks-and-recreation");
            items[2].Show.Ids.Tvdb.Should().Be(84912);
            items[2].Show.Ids.Imdb.Should().Be("tt1266020");
            items[2].Show.Ids.Tmdb.Should().Be(8592);
            items[2].Show.Ids.TvRage.Should().Be(21686);
            items[2].Season.Should().BeNull();
            items[2].Episode.Should().BeNull();

            items[3].Id.Should().Be(1982344);
            items[3].WatchedAt.Should().Be(DateTime.Parse("2013-05-15T05:54:27.000Z").ToUniversalTime());
            items[3].Action.Should().Be(TraktSyncHistoryActionType.Watch);
            items[3].Type.Should().Be(TraktSyncHistoryItemType.Season);
            items[3].Movie.Should().BeNull();
            items[3].Show.Should().BeNull();
            items[3].Season.Should().NotBeNull();
            items[3].Season.Number.Should().Be(1);
            items[3].Season.Ids.Should().NotBeNull();
            items[3].Season.Ids.Trakt.Should().Be(1);
            items[3].Season.Ids.Tvdb.Should().Be(439371);
            items[3].Season.Ids.Tmdb.Should().Be(3577);
            items[3].Season.Ids.TvRage.Should().NotHaveValue();
            items[3].Episode.Should().BeNull();
        }
    }
}
