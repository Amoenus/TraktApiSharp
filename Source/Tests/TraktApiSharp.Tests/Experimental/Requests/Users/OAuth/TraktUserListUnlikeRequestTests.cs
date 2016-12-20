﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;

    [TestClass]
    public class TraktUserListUnlikeRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListUnlikeRequestIsNotAbstract()
        {
            typeof(TraktUserListUnlikeRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserListUnlikeRequestIsSealed()
        {
            typeof(TraktUserListUnlikeRequest).IsSealed.Should().BeTrue();
        }
    }
}
