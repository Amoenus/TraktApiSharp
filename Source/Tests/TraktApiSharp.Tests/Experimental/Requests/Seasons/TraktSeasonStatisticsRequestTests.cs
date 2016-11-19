﻿namespace TraktApiSharp.Tests.Experimental.Requests.Seasons
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Seasons;
    using TraktApiSharp.Objects.Basic;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktSeasonStatisticsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestIsNotAbstract()
        {
            typeof(TraktSeasonStatisticsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestIsSealed()
        {
            typeof(TraktSeasonStatisticsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            typeof(TraktSeasonStatisticsRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktStatistics>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestHasAuthorizationNotRequired()
        {
            var request = new TraktSeasonStatisticsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestHasValidUriTemplate()
        {
            var request = new TraktSeasonStatisticsRequest(null);
            request.UriTemplate.Should().Be("shows/{id}/seasons/{season}/stats");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestImplementsITraktObjectRequestInterface()
        {
            typeof(TraktSeasonStatisticsRequest).GetInterfaces().Should().Contain(typeof(ITraktObjectRequest));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestHasSeasonNumberProperty()
        {
            var sortingPropertyInfo = typeof(TraktSeasonStatisticsRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "SeasonNumber")
                    .FirstOrDefault();

            sortingPropertyInfo.CanRead.Should().BeTrue();
            sortingPropertyInfo.CanWrite.Should().BeTrue();
            sortingPropertyInfo.PropertyType.Should().Be(typeof(uint));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Seasons")]
        public void TestTraktSeasonStatisticsRequestHasGetUriPathParametersMethod()
        {
            var methodInfo = typeof(TraktSeasonStatisticsRequest).GetMethods()
                                                                 .Where(m => m.Name == "GetUriPathParameters")
                                                                 .FirstOrDefault();

            methodInfo.ReturnType.Should().Be(typeof(IDictionary<string, object>));
            methodInfo.GetParameters().Should().BeEmpty();
        }
    }
}
