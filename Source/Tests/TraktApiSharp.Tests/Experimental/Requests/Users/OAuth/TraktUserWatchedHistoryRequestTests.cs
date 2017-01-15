﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.History;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserWatchedHistoryRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedHistoryRequestIsNotAbstract()
        {
            typeof(TraktUserWatchedHistoryRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedHistoryRequestIsSealed()
        {
            typeof(TraktUserWatchedHistoryRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedHistoryRequestIsSubclassOfATraktUsersPaginationGetRequest()
        {
            typeof(TraktUserWatchedHistoryRequest).IsSubclassOf(typeof(ATraktUsersPaginationGetRequest<TraktHistoryItem>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedHistoryRequestHasAuthorizationOptional()
        {
            var request = new TraktUserWatchedHistoryRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Optional);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchedHistoryRequestHasValidUriTemplate()
        {
            var request = new TraktUserWatchedHistoryRequest(null);
            request.UriTemplate.Should().Be("users/{username}/history{/type}{/item_id}{?start_at,end_at,extended,page,limit}");
        }
    }
}
