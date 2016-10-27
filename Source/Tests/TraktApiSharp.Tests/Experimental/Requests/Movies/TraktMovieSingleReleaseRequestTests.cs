﻿namespace TraktApiSharp.Tests.Experimental.Requests.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Movies;
    using TraktApiSharp.Objects.Get.Movies;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktMovieSingleReleaseRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Movies")]
        public void TestTraktMovieSingleReleaseRequestIsNotAbstract()
        {
            typeof(TraktMovieSingleReleaseRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies")]
        public void TestTraktMovieSingleReleaseRequestIsSealed()
        {
            typeof(TraktMovieSingleReleaseRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies")]
        public void TestTraktMovieSingleReleaseRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            typeof(TraktMovieSingleReleaseRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktMovieRelease>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies")]
        public void TestTraktMovieSingleReleaseRequestHasAuthorizationNotRequired()
        {
            var request = new TraktMovieSingleReleaseRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies")]
        public void TestTraktMovieSingleReleaseRequestHasValidUriTemplate()
        {
            var request = new TraktMovieSingleReleaseRequest(null);
            request.UriTemplate.Should().Be("movies/{id}/releases/{language}");
        }
    }
}
