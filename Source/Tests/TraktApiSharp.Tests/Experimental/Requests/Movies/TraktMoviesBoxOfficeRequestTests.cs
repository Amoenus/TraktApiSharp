﻿namespace TraktApiSharp.Tests.Experimental.Requests.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Movies;
    using TraktApiSharp.Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktMoviesBoxOfficeRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesBoxOfficeRequestIsNotAbstract()
        {
            typeof(TraktMoviesBoxOfficeRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesBoxOfficeRequestIsSealed()
        {
            typeof(TraktMoviesBoxOfficeRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesBoxOfficeRequestIsSubclassOfATraktListGetRequest()
        {
            typeof(TraktMoviesBoxOfficeRequest).IsSubclassOf(typeof(ATraktListGetRequest<TraktBoxOfficeMovie>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesBoxOfficeRequestHasAuthorizationNotRequired()
        {
            var request = new TraktMoviesBoxOfficeRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }
    }
}
