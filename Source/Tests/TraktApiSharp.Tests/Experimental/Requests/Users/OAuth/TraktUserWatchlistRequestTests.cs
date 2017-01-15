﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;
    using TraktApiSharp.Objects.Get.Watchlist;

    [TestClass]
    public class TraktUserWatchlistRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchlistRequestIsNotAbstract()
        {
            typeof(TraktUserWatchlistRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchlistRequestIsSealed()
        {
            typeof(TraktUserWatchlistRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestTraktUserWatchlistRequestIsSubclassOfATraktUsersPaginationGetRequest()
        {
            typeof(TraktUserWatchlistRequest).IsSubclassOf(typeof(ATraktUsersPaginationGetRequest<TraktWatchlistItem>)).Should().BeTrue();
        }
    }
}
