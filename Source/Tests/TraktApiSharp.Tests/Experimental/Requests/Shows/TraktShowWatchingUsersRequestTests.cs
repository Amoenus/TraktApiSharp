﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Objects.Get.Users;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktShowWatchingUsersRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowWatchingUsersRequestIsNotAbstract()
        {
            typeof(TraktShowWatchingUsersRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowWatchingUsersRequestIsSealed()
        {
            typeof(TraktShowWatchingUsersRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowWatchingUsersRequestIsSubclassOfATraktListGetByIdRequest()
        {
            typeof(TraktShowWatchingUsersRequest).IsSubclassOf(typeof(ATraktListGetByIdRequest<TraktUser>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowWatchingUsersRequestHasAuthorizationNotRequired()
        {
            var request = new TraktShowWatchingUsersRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowWatchingUsersRequestHasValidUriTemplate()
        {
            var request = new TraktShowWatchingUsersRequest(null);
            request.UriTemplate.Should().Be("shows/{id}/watching{?extended}");
        }
    }
}
