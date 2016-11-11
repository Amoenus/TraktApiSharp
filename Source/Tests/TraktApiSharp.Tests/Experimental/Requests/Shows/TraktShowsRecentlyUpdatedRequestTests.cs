﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Reflection;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Objects.Get.Shows.Common;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktShowsRecentlyUpdatedRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestIsNotAbstract()
        {
            typeof(TraktShowsRecentlyUpdatedRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestIsSealed()
        {
            typeof(TraktShowsRecentlyUpdatedRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestIsSubclassOfATraktPaginationGetRequest()
        {
            typeof(TraktShowsRecentlyUpdatedRequest).IsSubclassOf(typeof(ATraktPaginationGetRequest<TraktRecentlyUpdatedShow>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestHasAuthorizationNotRequired()
        {
            var request = new TraktShowsRecentlyUpdatedRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestHasValidUriTemplate()
        {
            var request = new TraktShowsRecentlyUpdatedRequest(null);
            request.UriTemplate.Should().Be("shows/updates{/start_date}{?extended,page,limit}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestImplementsITraktExtendedInfoInterface()
        {
            typeof(TraktShowsRecentlyUpdatedRequest).GetInterfaces().Should().Contain(typeof(ITraktExtendedInfo));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows"), TestCategory("Lists")]
        public void TestTraktShowsRecentlyUpdatedRequestHasStartDateProperty()
        {
            var startDatePropertyInfo = typeof(TraktShowsRecentlyUpdatedRequest)
                    .GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name == "StartDate")
                    .FirstOrDefault();

            startDatePropertyInfo.CanRead.Should().BeTrue();
            startDatePropertyInfo.CanWrite.Should().BeTrue();
            startDatePropertyInfo.PropertyType.Should().Be(typeof(DateTime?));
        }
    }
}
