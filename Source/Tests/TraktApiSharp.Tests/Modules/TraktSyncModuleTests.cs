﻿namespace TraktApiSharp.Tests.Modules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using TraktApiSharp.Enums;
    using TraktApiSharp.Exceptions;
    using TraktApiSharp.Modules;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Get.Syncs.Activities;
    using TraktApiSharp.Objects.Get.Syncs.Collection;
    using TraktApiSharp.Objects.Get.Syncs.Playback;
    using TraktApiSharp.Requests;
    using Utils;

    [TestClass]
    public class TraktSyncModuleTests
    {
        [TestMethod]
        public void TestTraktSyncModuleIsModule()
        {
            typeof(TraktBaseModule).IsAssignableFrom(typeof(TraktSyncModule)).Should().BeTrue();
        }

        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            TestUtility.SetupMockHttpClient();
        }

        [ClassCleanup]
        public static void CleanupTests()
        {
            TestUtility.ResetMockHttpClient();
        }

        [TestCleanup]
        public void CleanupSingleTest()
        {
            TestUtility.ClearMockHttpClient();
        }

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetLastActivities

        [TestMethod]
        public void TestTraktSyncModuleGetLastActivities()
        {
            var lastActivities = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Activities\SyncLastActivities.json");
            lastActivities.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("sync/last_activities", lastActivities);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetLastActivitiesAsync().Result;

            response.Should().NotBeNull();

            response.All.Should().Be(DateTime.Parse("2014-11-20T07:01:32.378Z").ToUniversalTime());
            response.Movies.Should().NotBeNull();
            response.Movies.WatchedAt.Should().Be(DateTime.Parse("2014-11-19T21:42:41.823Z").ToUniversalTime());
            response.Movies.CollectedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.243Z").ToUniversalTime());
            response.Movies.RatedAt.Should().Be(DateTime.Parse("2014-11-19T18:32:29.459Z").ToUniversalTime());
            response.Movies.WatchlistedAt.Should().Be(DateTime.Parse("2014-11-19T21:42:41.844Z").ToUniversalTime());
            response.Movies.CommentedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.250Z").ToUniversalTime());
            response.Movies.PausedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.250Z").ToUniversalTime());

            response.Episodes.Should().NotBeNull();
            response.Episodes.WatchedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.305Z").ToUniversalTime());
            response.Episodes.CollectedAt.Should().Be(DateTime.Parse("2014-11-19T22:02:41.308Z").ToUniversalTime());
            response.Episodes.RatedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.310Z").ToUniversalTime());
            response.Episodes.WatchlistedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.321Z").ToUniversalTime());
            response.Episodes.CommentedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.325Z").ToUniversalTime());
            response.Episodes.PausedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.250Z").ToUniversalTime());

            response.Shows.Should().NotBeNull();
            response.Shows.RatedAt.Should().Be(DateTime.Parse("2014-11-19T19:50:58.557Z").ToUniversalTime());
            response.Shows.WatchlistedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.262Z").ToUniversalTime());
            response.Shows.CommentedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.281Z").ToUniversalTime());

            response.Seasons.Should().NotBeNull();
            response.Seasons.RatedAt.Should().Be(DateTime.Parse("2014-11-19T19:54:24.537Z").ToUniversalTime());
            response.Seasons.WatchlistedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.297Z").ToUniversalTime());
            response.Seasons.CommentedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.301Z").ToUniversalTime());

            response.Comments.Should().NotBeNull();
            response.Comments.LikedAt.Should().Be(DateTime.Parse("2014-11-20T03:38:09.122Z").ToUniversalTime());

            response.Lists.Should().NotBeNull();
            response.Lists.LikedAt.Should().Be(DateTime.Parse("2014-11-20T00:36:48.506Z").ToUniversalTime());
            response.Lists.UpdatedAt.Should().Be(DateTime.Parse("2014-11-20T06:52:18.837Z").ToUniversalTime());
            response.Lists.CommentedAt.Should().Be(DateTime.Parse("2014-11-20T06:51:30.250Z").ToUniversalTime());
        }

        [TestMethod]
        public void TestTraktSyncModuleGetLastActivitiesExceptions()
        {
            var uri = "sync/last_activities";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktSyncLastActivities>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Sync.GetLastActivitiesAsync();
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetPlaybackProgress

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgress()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("sync/playback", playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressWithExtendedOption()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithOAuth($"sync/playback?extended={extendedOption.ToString()}", playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(null, extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressWithExtendedOptionAndLimit()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithOAuth($"sync/playback?extended={extendedOption.ToString()}&limit={limit}",
                                                   playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(null, extendedOption, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressWithType()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var type = TraktSyncType.Episode;

            TestUtility.SetupMockResponseWithOAuth($"sync/playback/{type.AsStringUriParameter()}", playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(type).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressWithTypeAndLimit()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var type = TraktSyncType.Episode;
            var limit = 4;

            TestUtility.SetupMockResponseWithOAuth($"sync/playback/{type.AsStringUriParameter()}?limit={limit}",
                                                   playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(type, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressWithLimit()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var limit = 4;

            TestUtility.SetupMockResponseWithOAuth($"sync/playback?limit={limit}", playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(null, null, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressComplete()
        {
            var playbackProgress = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Playback\SyncPlaybackProgress.json");
            playbackProgress.Should().NotBeNullOrEmpty();

            var type = TraktSyncType.Episode;
            var limit = 4;

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true
            };

            TestUtility.SetupMockResponseWithOAuth(
                $"sync/playback/{type.AsStringUriParameter()}?extended={extendedOption.ToString()}&limit={limit}",
                playbackProgress);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync(type, extendedOption, limit).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetPlaybackProgressExceptions()
        {
            var uri = "sync/playback";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktListResult<TraktSyncPlaybackProgressItem>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Sync.GetPlaybackProgressAsync();
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region RemovePlaybackItem

        [TestMethod]
        public void TestTraktSyncModuleGetRemovePlaybackItem()
        {
            var playbackId = "13";

            TestUtility.SetupMockResponseWithOAuth($"sync/playback/{playbackId}", HttpStatusCode.NoContent);

            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Sync.RemovePlaybackItemAsync(playbackId);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetRemovePlaybackItemExceptions()
        {
            var playbackId = "13";

            var uri = $"sync/playback/{playbackId}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Sync.RemovePlaybackItemAsync(playbackId);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktObjectNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Conflict);
            act.ShouldThrow<TraktConflictException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetRemovePlaybackItemArgumentExceptions()
        {
            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Sync.RemovePlaybackItemAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Sync.RemovePlaybackItemAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Sync.RemovePlaybackItemAsync("user name");
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetCollectionMovies

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionMovies()
        {
            var collectionMovies = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Collection\SyncCollectionMoviesMetadata.json");
            collectionMovies.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("sync/collection/movies", collectionMovies);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetCollectionMoviesAsync().Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionMoviesComplete()
        {
            var collectionMovies = TestUtility.ReadFileContents(@"Objects\Get\Syncs\Collection\SyncCollectionMoviesMetadata.json");
            collectionMovies.Should().NotBeNullOrEmpty();

            var extendedOption = new TraktExtendedOption
            {
                Full = true,
                Images = true,
                Metadata = true
            };

            TestUtility.SetupMockResponseWithOAuth($"sync/collection/movies?extended={extendedOption.ToString()}",
                                                   collectionMovies);

            var response = TestUtility.MOCK_TEST_CLIENT.Sync.GetCollectionMoviesAsync(extendedOption).Result;

            response.Should().NotBeNull();
            response.Items.Should().NotBeNull().And.HaveCount(2);
        }

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionMoviesExceptions()
        {
            var uri = "sync/collection/movies";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktListResult<TraktSyncCollectionMovieItem>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Sync.GetCollectionMoviesAsync();
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetCollectionShows

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionShows()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionShowsComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetCollectionShowsExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region AddCollectionItems

        [TestMethod]
        public void TestTraktSyncModuleAddCollectionItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddCollectionItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddCollectionItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region RemoveCollectionItems

        [TestMethod]
        public void TestTraktSyncModuleRemoveCollectionItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveCollectionItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveCollectionItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetWatchedMovies

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedMovies()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedMoviesComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedMoviesExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetWatchedShows

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedShows()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedShowsComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedShowsExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetWatchedHistory

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistory()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithType()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndId()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndStartDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndStartDateAndEndDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndStartDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndStartDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndStartDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndEndDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndEndDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndIdAndEndDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndStartDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndStartDateAndEndDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndStartDateAndEndDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndStartDateAndEndDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndStartDateAndEndDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndEndDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndEndDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndEndDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndEndDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithTypeAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndEndDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndEndDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndEndDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndEndDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithStartDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithEndDate()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithEndDateAndPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithEndDateAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithEndDateAndPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryWithPageAndLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchedHistoryArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region AddWatchedHistoryItems

        [TestMethod]
        public void TestTraktSyncModuleAddWatchedHistoryItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddWatchedHistoryItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddWatchedHistoryItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region RemoveWatchedHistoryItems

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchedHistoryItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchedHistoryItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchedHistoryItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetRatings

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatings()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithType()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7_8()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7_8_9()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7_8_9_10()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7_8_9_10_11()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_0_1_2_3_4_5_6_7_8_9_10()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_1_2_3_4_5_6_7_8_9_11()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndRatingsFilter_0_1_2_3_4_5_6_7_8_9()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithRatingsFilter()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithTypeAndExtendedOption()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsWithExtendedOption()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetUserRatingsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region AddRatings

        [TestMethod]
        public void TestTraktSyncModuleAddRatings()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddRatingsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddRatingsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region RemoveRatings

        [TestMethod]
        public void TestTraktSyncModuleRemoveRatings()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveRatingsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveRatingsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetWatchlist

        [TestMethod]
        public void TestTraktSyncModuleGetWatchlist()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchlistWithType()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchlistWithExtendedOption()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchlistComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleGetWatchlistExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region AddWatchlistItems

        [TestMethod]
        public void TestTraktSyncModuleAddWatchlistItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddWatchlistItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleAddWatchlistItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region RemoveWatchlistItems

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchlistItems()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchlistItemsExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktSyncModuleRemoveWatchlistItemsArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion
    }
}
