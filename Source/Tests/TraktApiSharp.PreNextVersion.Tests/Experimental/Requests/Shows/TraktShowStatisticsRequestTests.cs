﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktShowStatisticsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowStatisticsRequestIsNotAbstract()
        {
            typeof(TraktShowStatisticsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowStatisticsRequestIsSealed()
        {
            typeof(TraktShowStatisticsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowStatisticsRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            //typeof(TraktShowStatisticsRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktStatistics>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowStatisticsRequestHasAuthorizationNotRequired()
        {
            var request = new TraktShowStatisticsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowStatisticsRequestHasValidUriTemplate()
        {
            var request = new TraktShowStatisticsRequest(null);
            request.UriTemplate.Should().Be("shows/{id}/stats");
        }
    }
}
