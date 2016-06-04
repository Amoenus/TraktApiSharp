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
    using TraktApiSharp.Objects.Get.Shows.Seasons;
    using TraktApiSharp.Objects.Get.Users.Lists;
    using TraktApiSharp.Objects.Post.Comments;
    using TraktApiSharp.Objects.Post.Comments.Responses;
    using Utils;

    [TestClass]
    public class TraktCommentsModuleTests
    {
        [TestMethod]
        public void TestTraktCommentsModuleIsModule()
        {
            typeof(TraktBaseModule).IsAssignableFrom(typeof(TraktCommentsModule)).Should().BeTrue();
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

        #region GetSingleComment

        [TestMethod]
        public void TestTraktCommentsModuleGetComment()
        {
            var comment = TestUtility.ReadFileContents(@"Objects\Basic\Comment.json");
            comment.Should().NotBeNullOrEmpty();

            var commentId = "76957";

            TestUtility.SetupMockResponseWithoutOAuth($"comments/{commentId}", comment);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentAsync(commentId).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(76957);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2016-04-01T12:44:40Z").ToUniversalTime());
            response.Comment.Should().Be("I hate they made The flash a kids show. Could else be much better. And with a better flash offcourse.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(1);
            response.Likes.Should().Be(2);
            response.UserRating.Should().Be(7.3f);
            response.User.Should().NotBeNull();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentExceptions()
        {
            var commentId = "76957";
            var uri = $"comments/{commentId}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.NotFound);

            Func<Task<TraktComment>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentAsync(commentId);
            act.ShouldThrow<TraktCommentNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)429);
            act.ShouldThrow<TraktRateLimitException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.InternalServerError);
            act.ShouldThrow<TraktServerException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)503);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)504);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)520);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)521);
            act.ShouldThrow<TraktServerUnavailableException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithoutOAuth(uri, (HttpStatusCode)522);
            act.ShouldThrow<TraktServerUnavailableException>();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentArgumentExceptions()
        {
            var comment = TestUtility.ReadFileContents(@"Objects\Basic\Comment.json");
            comment.Should().NotBeNullOrEmpty();

            var commentId = "76957";

            TestUtility.SetupMockResponseWithoutOAuth($"comments/{commentId}", comment);

            Func<Task<TraktComment>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentAsync(null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetMultipleComments

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentsArgumentExceptions()
        {
            Func<Task<TraktListResult<TraktComment>>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentsAsync(new string[] { null });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentsAsync(new string[] { string.Empty });
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentsAsync(new string[] { });
            act.ShouldNotThrow();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.GetCommentsAsync(null);
            act.ShouldNotThrow();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostMovieComment

        [TestMethod]
        public void TestTraktCommentsModulePostMovieComment()
        {
            var movieCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            movieCommentPostResponse.Should().NotBeNullOrEmpty();

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

            var comment = "one two three four five";

            var movieCommentPost = new TraktMovieCommentPost
            {
                Movie = movie,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(movieCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, movieCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostMovieCommentWithSpoiler()
        {
            var movieCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            movieCommentPostResponse.Should().NotBeNullOrEmpty();

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

            var comment = "one two three four five";
            var spoiler = false;

            var movieCommentPost = new TraktMovieCommentPost
            {
                Movie = movie,
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(movieCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, movieCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostMovieCommentWithSharing()
        {
            var movieCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            movieCommentPostResponse.Should().NotBeNullOrEmpty();

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

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";

            var movieCommentPost = new TraktMovieCommentPost
            {
                Movie = movie,
                Comment = comment,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(movieCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, movieCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment, null, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostMovieCommentComplete()
        {
            var movieCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            movieCommentPostResponse.Should().NotBeNullOrEmpty();

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

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";
            var spoiler = false;

            var movieCommentPost = new TraktMovieCommentPost
            {
                Movie = movie,
                Comment = comment,
                Spoiler = spoiler,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(movieCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, movieCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment, spoiler, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostMovieCommentExceptions()
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

            var comment = "one two three four five";

            var uri = "comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostMovieCommentArgumentExceptions()
        {
            var movieCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            movieCommentPostResponse.Should().NotBeNullOrEmpty();

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

            var comment = "one two three four five";

            var movieCommentPost = new TraktMovieCommentPost
            {
                Movie = movie,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(movieCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, movieCommentPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(null, comment);

            act.ShouldThrow<ArgumentNullException>();

            movie.Title = string.Empty;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<ArgumentException>();

            movie.Title = "Guardians of the Galaxy";
            movie.Year = 0;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<ArgumentException>();

            movie.Year = 2014;
            movie.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<ArgumentException>();

            movie.Ids = new TraktMovieIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<ArgumentException>();

            movie.Ids = new TraktMovieIds
            {
                Trakt = 28,
                Slug = "guardians-of-the-galaxy-2014",
                Imdb = "tt2015381",
                Tmdb = 118340
            };

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostMovieCommentAsync(movie, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostShowComment

        [TestMethod]
        public void TestTraktCommentsModulePostShowComment()
        {
            var showCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            showCommentPostResponse.Should().NotBeNullOrEmpty();

            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var comment = "one two three four five";

            var showCommentPost = new TraktShowCommentPost
            {
                Show = show,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(showCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, showCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostShowCommentWithSpoiler()
        {
            var showCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            showCommentPostResponse.Should().NotBeNullOrEmpty();

            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var comment = "one two three four five";
            var spoiler = false;

            var showCommentPost = new TraktShowCommentPost
            {
                Show = show,
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(showCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, showCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostShowCommentWithSharing()
        {
            var showCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            showCommentPostResponse.Should().NotBeNullOrEmpty();

            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";

            var showCommentPost = new TraktShowCommentPost
            {
                Show = show,
                Comment = comment,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(showCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, showCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment, null, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostShowCommentComplete()
        {
            var showCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            showCommentPostResponse.Should().NotBeNullOrEmpty();

            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";
            var spoiler = false;

            var showCommentPost = new TraktShowCommentPost
            {
                Show = show,
                Comment = comment,
                Spoiler = spoiler,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(showCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, showCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment, spoiler, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostShowCommentExceptions()
        {
            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var comment = "one two three four five";

            var uri = "comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostShowCommentArgumentExceptions()
        {
            var showCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            showCommentPostResponse.Should().NotBeNullOrEmpty();

            var show = new TraktShow
            {
                Title = "Breaking Bad",
                Ids = new TraktShowIds
                {
                    Trakt = 1388,
                    Slug = "breaking bad",
                    Tvdb = 81189,
                    Imdb = "tt0903747",
                    Tmdb = 1396,
                    TvRage = 18164
                }
            };

            var comment = "one two three four five";

            var showCommentPost = new TraktShowCommentPost
            {
                Show = show,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(showCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, showCommentPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(null, comment);

            act.ShouldThrow<ArgumentNullException>();

            show.Title = string.Empty;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment);
            act.ShouldThrow<ArgumentException>();

            show.Title = "Breaking Bad";
            show.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment);
            act.ShouldThrow<ArgumentException>();

            show.Ids = new TraktShowIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment);
            act.ShouldThrow<ArgumentException>();

            show.Ids = new TraktShowIds
            {
                Trakt = 1388,
                Slug = "breaking bad",
                Tvdb = 81189,
                Imdb = "tt0903747",
                Tmdb = 1396,
                TvRage = 18164
            };

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostShowCommentAsync(show, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostSeasonComment

        [TestMethod]
        public void TestTraktCommentsModulePostSeasonComment()
        {
            var seasonCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            seasonCommentPostResponse.Should().NotBeNullOrEmpty();

            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var comment = "one two three four five";

            var seasonCommentPost = new TraktSeasonCommentPost
            {
                Season = season,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(seasonCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, seasonCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostSeasonCommentWithSpoiler()
        {
            var seasonCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            seasonCommentPostResponse.Should().NotBeNullOrEmpty();

            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var comment = "one two three four five";
            var spoiler = false;

            var seasonCommentPost = new TraktSeasonCommentPost
            {
                Season = season,
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(seasonCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, seasonCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostSeasonCommentWithSharing()
        {
            var seasonCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            seasonCommentPostResponse.Should().NotBeNullOrEmpty();

            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";

            var seasonCommentPost = new TraktSeasonCommentPost
            {
                Season = season,
                Comment = comment,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(seasonCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, seasonCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment, null, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostSeasonCommentComplete()
        {
            var seasonCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            seasonCommentPostResponse.Should().NotBeNullOrEmpty();

            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";
            var spoiler = false;

            var seasonCommentPost = new TraktSeasonCommentPost
            {
                Season = season,
                Comment = comment,
                Spoiler = spoiler,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(seasonCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, seasonCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment, spoiler, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostSeasonCommentExceptions()
        {
            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var comment = "one two three four five";

            var uri = "comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostSeasonCommentArgumentExceptions()
        {
            var seasonCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            seasonCommentPostResponse.Should().NotBeNullOrEmpty();

            var season = new TraktSeason
            {
                Ids = new TraktSeasonIds
                {
                    Trakt = 3950,
                    Tvdb = 30272,
                    Tmdb = 3572
                }
            };

            var comment = "one two three four five";

            var seasonCommentPost = new TraktSeasonCommentPost
            {
                Season = season,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(seasonCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, seasonCommentPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(null, comment);

            act.ShouldThrow<ArgumentNullException>();

            season.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment);
            act.ShouldThrow<ArgumentException>();

            season.Ids = new TraktSeasonIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment);
            act.ShouldThrow<ArgumentException>();

            season.Ids = new TraktSeasonIds
            {
                Trakt = 3950,
                Tvdb = 30272,
                Tmdb = 3572
            };

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostSeasonCommentAsync(season, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostEpisodeComment

        [TestMethod]
        public void TestTraktCommentsModulePostEpisodeComment()
        {
            var episodeCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            episodeCommentPostResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var comment = "one two three four five";

            var episodeCommentPost = new TraktEpisodeCommentPost
            {
                Episode = episode,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(episodeCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, episodeCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostEpisodeCommentWithSpoiler()
        {
            var episodeCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            episodeCommentPostResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var comment = "one two three four five";
            var spoiler = false;

            var episodeCommentPost = new TraktEpisodeCommentPost
            {
                Episode = episode,
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(episodeCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, episodeCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostEpisodeCommentWithSharing()
        {
            var episodeCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            episodeCommentPostResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";

            var episodeCommentPost = new TraktEpisodeCommentPost
            {
                Episode = episode,
                Comment = comment,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(episodeCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, episodeCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment, null, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostEpisodeCommentComplete()
        {
            var episodeCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            episodeCommentPostResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";
            var spoiler = false;

            var episodeCommentPost = new TraktEpisodeCommentPost
            {
                Episode = episode,
                Comment = comment,
                Spoiler = spoiler,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(episodeCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, episodeCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment, spoiler, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostEpisodeCommentExceptions()
        {
            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var comment = "one two three four five";

            var uri = "comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostEpisodeCommentArgumentExceptions()
        {
            var episodeCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            episodeCommentPostResponse.Should().NotBeNullOrEmpty();

            var episode = new TraktEpisode
            {
                Ids = new TraktEpisodeIds
                {
                    Trakt = 73482,
                    Tvdb = 349232,
                    Imdb = "tt0959621",
                    Tmdb = 62085,
                    TvRage = 637041
                }
            };

            var comment = "one two three four five";

            var episodeCommentPost = new TraktEpisodeCommentPost
            {
                Episode = episode,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(episodeCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, episodeCommentPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(null, comment);

            act.ShouldThrow<ArgumentNullException>();

            episode.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment);
            act.ShouldThrow<ArgumentException>();

            episode.Ids = new TraktEpisodeIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment);
            act.ShouldThrow<ArgumentException>();

            episode.Ids = new TraktEpisodeIds
            {
                Trakt = 73482,
                Tvdb = 349232,
                Imdb = "tt0959621",
                Tmdb = 62085,
                TvRage = 637041
            };

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostEpisodeCommentAsync(episode, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostListComment

        [TestMethod]
        public void TestTraktCommentsModulePostListComment()
        {
            var listCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            listCommentPostResponse.Should().NotBeNullOrEmpty();

            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var comment = "one two three four five";

            var listCommentPost = new TraktListCommentPost
            {
                List = list,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(listCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, listCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostListCommentWithSpoiler()
        {
            var listCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            listCommentPostResponse.Should().NotBeNullOrEmpty();

            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var comment = "one two three four five";
            var spoiler = false;

            var listCommentPost = new TraktListCommentPost
            {
                List = list,
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(listCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, listCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostListCommentWithSharing()
        {
            var listCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            listCommentPostResponse.Should().NotBeNullOrEmpty();

            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";

            var listCommentPost = new TraktListCommentPost
            {
                List = list,
                Comment = comment,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(listCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, listCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment, null, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostListCommentComplete()
        {
            var listCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            listCommentPostResponse.Should().NotBeNullOrEmpty();

            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var sharing = new TraktSharing
            {
                Facebook = true,
                Google = false,
                Twitter = true
            };

            var comment = "one two three four five";
            var spoiler = false;

            var listCommentPost = new TraktListCommentPost
            {
                List = list,
                Comment = comment,
                Spoiler = spoiler,
                Sharing = sharing
            };

            var postJson = TestUtility.SerializeObject(listCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, listCommentPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment, spoiler, sharing).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostListCommentExceptions()
        {
            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var comment = "one two three four five";

            var uri = "comments";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostListCommentArgumentExceptions()
        {
            var listCommentPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            listCommentPostResponse.Should().NotBeNullOrEmpty();

            var list = new TraktList
            {
                Ids = new TraktListIds
                {
                    Trakt = 2228577,
                    Slug = "oscars-2016"
                }
            };

            var comment = "one two three four five";

            var listCommentPost = new TraktListCommentPost
            {
                List = list,
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(listCommentPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth("comments", postJson, listCommentPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(null, comment);

            act.ShouldThrow<ArgumentNullException>();

            list.Ids = null;

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment);
            act.ShouldThrow<ArgumentException>();

            list.Ids = new TraktListIds();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment);
            act.ShouldThrow<ArgumentException>();

            list.Ids = new TraktListIds
            {
                Trakt = 2228577,
                Slug = "oscars-2016"
            };

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostListCommentAsync(list, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region UpdateComment

        [TestMethod]
        public void TestTraktCommentsModuleUpdateComment()
        {
            var commentUpdatePostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentUpdatePostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five update";

            var commentUpdatePost = new TraktCommentUpdatePost
            {
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(commentUpdatePost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}", postJson, commentUpdatePostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModuleUpdateCommentWithSpoiler()
        {
            var commentUpdatePostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentUpdatePostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five update";
            var spoiler = false;

            var commentUpdatePost = new TraktCommentUpdatePost
            {
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(commentUpdatePost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}", postJson, commentUpdatePostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModuleUpdateCommentExceptions()
        {
            var commentId = "190";
            var comment = "one two three four five update";

            var commentUpdatePost = new TraktCommentUpdatePost
            {
                Comment = comment
            };

            var uri = $"comments/{commentId}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModuleUpdateCommentArgumentExceptions()
        {
            var commentUpdatePostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentUpdatePostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five update";

            var commentUpdatePost = new TraktCommentUpdatePost
            {
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(commentUpdatePost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}", postJson, commentUpdatePostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(null, comment);

            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(string.Empty, comment);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.UpdateCommentAsync(commentId, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region PostCommentReply

        [TestMethod]
        public void TestTraktCommentsModulePostCommentReply()
        {
            var commentReplyPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentReplyPostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five reply";

            var commentReplyPost = new TraktCommentReplyPost
            {
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(commentReplyPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}/replies", postJson, commentReplyPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, comment).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostCommentReplyWithSpoiler()
        {
            var commentReplyPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentReplyPostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five reply";
            var spoiler = false;

            var commentReplyPost = new TraktCommentReplyPost
            {
                Comment = comment,
                Spoiler = spoiler
            };

            var postJson = TestUtility.SerializeObject(commentReplyPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}/replies", postJson, commentReplyPostResponse);

            var response = TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, comment, spoiler).Result;

            response.Should().NotBeNull();
            response.Id.Should().Be(190);
            response.ParentId.Should().Be(0);
            response.CreatedAt.Should().Be(DateTime.Parse("2014-08-04T06:46:01.996Z").ToUniversalTime());
            response.Comment.Should().Be("Oh, I wasn't really listening.");
            response.Spoiler.Should().BeFalse();
            response.Review.Should().BeFalse();
            response.Replies.Should().Be(0);
            response.Likes.Should().Be(0);
            response.UserRating.Should().NotHaveValue();
            response.User.Should().NotBeNull();
            response.User.Username.Should().Be("sean");
            response.User.Private.Should().BeFalse();
            response.User.Name.Should().Be("Sean Rudford");
            response.User.VIP.Should().BeTrue();
            response.User.VIP_EP.Should().BeFalse();
            response.Sharing.Should().NotBeNull();
            response.Sharing.Facebook.Should().BeTrue();
            response.Sharing.Twitter.Should().BeTrue();
            response.Sharing.Tumblr.Should().BeFalse();
            response.Sharing.Medium.Should().BeTrue();
        }

        [TestMethod]
        public void TestTraktCommentsModulePostCommentReplyExceptions()
        {
            var commentId = "190";
            var comment = "one two three four five reply";

            var commentReplyPost = new TraktCommentReplyPost
            {
                Comment = comment
            };

            var uri = $"comments/{commentId}/replies";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, comment);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)412);
            act.ShouldThrow<TraktPreconditionFailedException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, (HttpStatusCode)422);
            act.ShouldThrow<TraktValidationException>();

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
        public void TestTraktCommentsModulePostCommentReplyArgumentExceptions()
        {
            var commentReplyPostResponse = TestUtility.ReadFileContents(@"Objects\Post\Comments\Responses\CommentPostResponse.json");
            commentReplyPostResponse.Should().NotBeNullOrEmpty();

            var commentId = "190";
            var comment = "one two three four five reply";

            var commentReplyPost = new TraktCommentReplyPost
            {
                Comment = comment
            };

            var postJson = TestUtility.SerializeObject(commentReplyPost);
            postJson.Should().NotBeNullOrEmpty();

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}/replies", postJson, commentReplyPostResponse);

            Func<Task<TraktCommentPostResponse>> act =
                async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(null, comment);

            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(string.Empty, comment);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, null);
            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, string.Empty);
            act.ShouldThrow<ArgumentException>();

            comment = "one two three four";

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.PostCommentReplyAsync(commentId, comment);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region DeleteComment

        [TestMethod]
        public void TestTraktCommentsModuleDeleteComment()
        {
            var commentId = "190";

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}", HttpStatusCode.NoContent);
            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.DeleteCommentAsync(commentId);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void TestTraktCommentsModuleDeleteCommentExceptions()
        {
            var commentId = "190";

            var uri = $"comments/{commentId}";

            TestUtility.SetupMockResponseWithoutOAuth(uri, HttpStatusCode.Unauthorized);

            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.DeleteCommentAsync(commentId);
            act.ShouldThrow<TraktAuthorizationException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.BadRequest);
            act.ShouldThrow<TraktBadRequestException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.Forbidden);
            act.ShouldThrow<TraktForbiddenException>();

            TestUtility.ClearMockHttpClient();
            TestUtility.SetupMockResponseWithOAuth(uri, HttpStatusCode.NotFound);
            act.ShouldThrow<TraktNotFoundException>();

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
        public void TestTraktCommentsModuleDeleteCommentArgumentExceptions()
        {
            var commentId = "190";

            TestUtility.SetupMockResponseWithOAuth($"comments/{commentId}", HttpStatusCode.NoContent);

            Func<Task> act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.DeleteCommentAsync(null);

            act.ShouldThrow<ArgumentException>();

            act = async () => await TestUtility.MOCK_TEST_CLIENT.Comments.DeleteCommentAsync(string.Empty);
            act.ShouldThrow<ArgumentException>();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region LikeComment

        [TestMethod]
        public void TestTraktCommentsModuleLikeComment()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleLikeCommentExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleLikeCommentArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region UnlikeComment

        [TestMethod]
        public void TestTraktCommentsModuleUnlikeComment()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleUnlikeCommentExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleUnlikeCommentArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetCommmentReply

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentReply()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentReplyExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentReplyArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion

        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        #region GetCommentReplies

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentReplies()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentRepliesWithPage()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentRepliesWithLimit()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentRepliesComplete()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentRepliesExceptions()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestTraktCommentsModuleGetCommentRepliesArgumentExceptions()
        {
            Assert.Fail();
        }

        #endregion
    }
}
