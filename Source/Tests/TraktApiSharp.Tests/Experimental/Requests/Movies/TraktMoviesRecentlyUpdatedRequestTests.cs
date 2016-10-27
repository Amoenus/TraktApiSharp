﻿namespace TraktApiSharp.Tests.Experimental.Requests.Movies
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Movies;
    using TraktApiSharp.Objects.Get.Movies.Common;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktMoviesRecentlyUpdatedRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestIsNotAbstract()
        {
            typeof(TraktMoviesRecentlyUpdatedRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestIsSealed()
        {
            typeof(TraktMoviesRecentlyUpdatedRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestIsSubclassOfATraktPaginationGetRequest()
        {
            typeof(TraktMoviesRecentlyUpdatedRequest).IsSubclassOf(typeof(ATraktPaginationGetRequest<TraktRecentlyUpdatedMovie>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestHasAuthorizationNotRequired()
        {
            var request = new TraktMoviesRecentlyUpdatedRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestHasValidUriTemplate()
        {
            var request = new TraktMoviesRecentlyUpdatedRequest(null);
            request.UriTemplate.Should().Be("movies/updates{/start_date}{?extended,page,limit}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestHasStartDateProperty()
        {
            var startDatePropertyInfo = typeof(TraktMoviesRecentlyUpdatedRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "StartDate")
                    .FirstOrDefault();

            startDatePropertyInfo.CanRead.Should().BeTrue();
            startDatePropertyInfo.CanWrite.Should().BeTrue();
            startDatePropertyInfo.PropertyType.Should().Be(typeof(DateTime?));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Movies"), TestCategory("Lists")]
        public void TestTraktMoviesRecentlyUpdatedRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktMoviesRecentlyUpdatedRequest).GetMethods()
                                                                      .Where(m => m.Name == "GetUriPathParameters")
                                                                      .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }
    }
}
