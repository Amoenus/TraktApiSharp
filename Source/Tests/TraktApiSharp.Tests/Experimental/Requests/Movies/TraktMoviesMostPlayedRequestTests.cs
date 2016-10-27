﻿namespace TraktApiSharp.Tests.Experimental.Requests.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Movies;
    using TraktApiSharp.Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktMoviesMostPlayedRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesMostPlayedRequestIsNotAbstract()
        {
            typeof(TraktMoviesMostPlayedRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesMostPlayedRequestIsSealed()
        {
            typeof(TraktMoviesMostPlayedRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesMostPlayedRequestIsSubclassOfATraktMoviesMostPWCRequest()
        {
            typeof(TraktMoviesMostPlayedRequest).IsSubclassOf(typeof(ATraktMoviesMostPWCRequest<TraktMostPlayedMovie>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesMostPlayedRequestHasAuthorizationNotRequired()
        {
            var request = new TraktMoviesMostPlayedRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesMostPlayedRequestHasValidUriTemplate()
        {
            var request = new TraktMoviesMostPlayedRequest(null);
            request.UriTemplate.Should().Be("movies/played{/period}{?extended,page,limit,query,years,genres,languages,countries,runtimes,ratings,certifications}");
        }
    }
}
