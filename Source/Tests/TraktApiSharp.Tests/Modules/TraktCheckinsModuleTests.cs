﻿namespace TraktApiSharp.Tests.Modules
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using TraktApiSharp.Exceptions;
    using TraktApiSharp.Modules;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Objects.Get.Shows;
    using TraktApiSharp.Objects.Get.Shows.Episodes;
    using TraktApiSharp.Objects.Post.Checkins;
    using TraktApiSharp.Objects.Post.Checkins.Responses;
    using Utils;

    [TestClass]
    public class TraktCheckinsModuleTests
    {
        [TestMethod]
        public void TestTraktCheckinsModuleIsModule()
        {
            typeof(TraktBaseModule).IsAssignableFrom(typeof(TraktCheckinsModule)).Should().BeTrue();
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

        #region CheckinMovie

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovie()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieWithMessage()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate, message).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieWithSharing()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate, null, sharing).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieWithFoursquareVenueId()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueId = "venue id";

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueId = foursquareVenueId
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate, null, null,
                                                                                   foursquareVenueId).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieWithFoursquareVenueName()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueName = "venue name";

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate, null, null,
                                                                                   null, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieComplete()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var foursquareVenueId = "venue id";
            var foursquareVenueName = "venue name";

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message,
                Sharing = sharing,
                FoursquareVenueId = foursquareVenueId,
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate, message, sharing,
                                                                                   foursquareVenueId, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T01:11:37.953Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Movie.Should().NotBeNull();
            response.Movie.Title.Should().Be("Guardians of the Galaxy");
            response.Movie.Year.Should().Be(2014);
            response.Movie.Ids.Should().NotBeNull();
            response.Movie.Ids.Trakt.Should().Be(28);
            response.Movie.Ids.Slug.Should().Be("guardians-of-the-galaxy-2014");
            response.Movie.Ids.Imdb.Should().Be("tt2015381");
            response.Movie.Ids.Tmdb.Should().Be(118340);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieExceptions()
        {
            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            TestUtility.SetupMockErrorResponseWithoutOAuth("checkin", HttpStatusCode.Unauthorized);

            Func<Task<TraktMovieCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.BadRequest);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Forbidden);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Conflict);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktCheckinException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)412);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)429);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.InternalServerError);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)503);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)504);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)520);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)521);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)522);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region CheckinEpisode

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisode()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeWithMessage()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate,
                                                                                     message).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeWithSharing()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate,
                                                                                     null, sharing).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeWithFoursquareVenueId()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueId = "venue id";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueId = foursquareVenueId
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate,
                                                                                     null, null, foursquareVenueId).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeWithFoursquareVenueName()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueName = "venue name";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate, null, null,
                                                                                     null, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeComplete()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var foursquareVenueId = "venue id";
            var foursquareVenueName = "venue name";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message,
                Sharing = sharing,
                FoursquareVenueId = foursquareVenueId,
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate, message, sharing,
                                                                                     foursquareVenueId, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeExceptions()
        {
            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            TestUtility.SetupMockErrorResponseWithoutOAuth("checkin", HttpStatusCode.Unauthorized);

            Func<Task<TraktEpisodeCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.BadRequest);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Forbidden);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Conflict);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktCheckinException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)412);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)429);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.InternalServerError);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)503);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)504);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)520);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)521);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)522);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region CheckinEpisodeShow

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShow()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowWithMessage()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate,
                                                                                     message).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowWithSharing()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate,
                                                                                     null, sharing).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowWithFoursquareVenueId()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueId = "venue id";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueId = foursquareVenueId
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate,
                                                                                     null, null, foursquareVenueId).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowWithFoursquareVenueName()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var foursquareVenueName = "venue name";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate, null, null,
                                                                                     null, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowComplete()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;
            var message = "checkin message";

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var foursquareVenueId = "venue id";
            var foursquareVenueName = "venue name";

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                Show = show,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd"),
                Message = message,
                Sharing = sharing,
                FoursquareVenueId = foursquareVenueId,
                FoursquareVenueName = foursquareVenueName
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate, message, sharing,
                                                                                     foursquareVenueId, foursquareVenueName).Result;

            response.Should().NotBeNull();

            response.WatchedAt.Should().Be(DateTime.Parse("2014-08-06T06:54:36.859Z").ToUniversalTime());
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Episode.Should().NotBeNull();
            response.Episode.SeasonNumber.Should().Be(1);
            response.Episode.Number.Should().Be(1);
            response.Episode.Title.Should().Be("Pilot");
            response.Episode.Ids.Should().NotBeNull();
            response.Episode.Ids.Trakt.Should().Be(16);
            response.Episode.Ids.Tvdb.Should().Be(349232);
            response.Episode.Ids.Imdb.Should().Be("tt0959621");
            response.Episode.Ids.Tmdb.Should().Be(62085);
            response.Episode.Ids.TvRage.Should().Be(637041);
            response.Show.Should().NotBeNull();
            response.Show.Title.Should().Be("Breaking Bad");
            response.Show.Year.Should().Be(2008);
            response.Show.Ids.Should().NotBeNull();
            response.Show.Ids.Trakt.Should().Be(1);
            response.Show.Ids.Slug.Should().Be("breaking-bad");
            response.Show.Ids.Tvdb.Should().Be(81189);
            response.Show.Ids.Imdb.Should().Be("tt0903747");
            response.Show.Ids.Tmdb.Should().Be(1396);
            response.Show.Ids.TvRage.Should().Be(18164);
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowExceptions()
        {
            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            TestUtility.SetupMockErrorResponseWithoutOAuth("checkin", HttpStatusCode.Unauthorized);

            Func<Task<TraktEpisodeCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.BadRequest);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Forbidden);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.Conflict);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktCheckinException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)412);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)429);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", HttpStatusCode.InternalServerError);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)503);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)504);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)520);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)521);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockErrorResponseWithOAuth("checkin", (HttpStatusCode)522);

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        #endregion

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinMovieArgumentExceptions()
        {
            var checkinMovieResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\MovieCheckinPostResponse.json");
            checkinMovieResponse.Should().NotBeNullOrEmpty();

            var movie = new TraktMovie
            {
                Title = "Guardians of the Galaxy",
                Year = 2014,
                Ids = new TraktMovieIds
                {
                    Trakt = 28,
                    Slug = "guardians-of-the-galaxy-2014",
                    Imdb = "tt2015381",
                    Tmdb = 118340
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var movieCheckinPost = new TraktMovieCheckinPost
            {
                Movie = movie,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(movieCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinMovieResponse);

            Func<Task<TraktMovieCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(null, appVersion, appBuildDate);

            act.ShouldThrow<ArgumentNullException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, null, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, string.Empty, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            movie.Year = 0;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            movie.Year = 2014;
            movie.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentNullException>();

            movie.Ids = new TraktMovieIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinMovieAsync(movie, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeArgumentExceptions()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            Func<Task<TraktEpisodeCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(null, appVersion, appBuildDate);

            act.ShouldThrow<ArgumentNullException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, null, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, string.Empty, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.Number = -1;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.Number = 1;
            episode.SeasonNumber = -1;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.SeasonNumber = 1;
            episode.Ids = new TraktEpisodeIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void TestTraktCheckinsModuleCheckinEpisodeShowArgumentExceptions()
        {
            var checkinEpisodeResponse = TestUtility.ReadFileContents(@"Objects\Post\Checkins\Responses\EpisodeCheckinPostResponse.json");
            checkinEpisodeResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Number = 1,
                SeasonNumber = 1,
                Ids = new TraktEpisodeIds
                {
                    Trakt = 16,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var show = new TraktShow { Title = "Breaking Bad" };

            var appVersion = "app_version";
            var appBuildDate = DateTime.UtcNow;

            var episodeCheckinPost = new TraktEpisodeCheckinPost
            {
                Episode = episode,
                AppVersion = appVersion,
                AppDate = appBuildDate.ToString("yyyy-MM-dd")
            };

            var postJson = TestUtility.SerializeObject(episodeCheckinPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("checkin", postJson, checkinEpisodeResponse);

            Func<Task<TraktEpisodeCheckinPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(null, show, appVersion, appBuildDate);

            act.ShouldThrow<ArgumentNullException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, null, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentNullException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, null, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, string.Empty, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.Number = -1;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.Number = 1;
            episode.SeasonNumber = -1;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.SeasonNumber = 1;
            episode.Ids = new TraktEpisodeIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();

            episode.Ids = new TraktEpisodeIds
            {
                Trakt = 16,
                Tvdb = 349232,
                Imdb = "tt0959621",
                Tmdb = 62085,
                TvRage = 637041
            };

            show.Title = string.Empty;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Checkins.CheckinEpisodeAsync(episode, show, appVersion, appBuildDate);
            act.ShouldThrow<ArgumentException>();
        }
    }
}
