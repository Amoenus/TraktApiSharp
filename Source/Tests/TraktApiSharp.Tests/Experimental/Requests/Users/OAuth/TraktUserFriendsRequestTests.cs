﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Users;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktUserFriendsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserFriendsRequestIsNotAbstract()
        {
            typeof(TraktUserFriendsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserFriendsRequestIsSealed()
        {
            typeof(TraktUserFriendsRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserFriendsRequestIsSubclassOfATraktUsersListGetRequest()
        {
            typeof(TraktUserFriendsRequest).IsSubclassOf(typeof(ATraktUsersListGetRequest<TraktUserFriend>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserFriendsRequestHasAuthorizationOptional()
        {
            var request = new TraktUserFriendsRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.Optional);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserFriendsRequestHasValidUriTemplate()
        {
            var request = new TraktUserFriendsRequest(null);
            request.UriTemplate.Should().Be("users/{username}/friends{?extended}");
        }
    }
}
