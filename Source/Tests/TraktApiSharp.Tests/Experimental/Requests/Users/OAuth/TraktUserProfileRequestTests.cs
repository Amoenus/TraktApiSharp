﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Users;

    [TestClass]
    public class TraktUserProfileRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserProfileRequestIsNotAbstract()
        {
            typeof(TraktUserProfileRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserProfileRequestIsSealed()
        {
            typeof(TraktUserProfileRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserProfileRequestIsSubclassOfATraktUsersSingleItemGetRequest()
        {
            typeof(TraktUserProfileRequest).IsSubclassOf(typeof(ATraktUsersSingleItemGetRequest<TraktUser>)).Should().BeTrue();
        }
    }
}
