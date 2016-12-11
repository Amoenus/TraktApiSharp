﻿namespace TraktApiSharp.Tests.Experimental.Requests.Syncs.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Syncs.OAuth;

    [TestClass]
    public class TraktSyncWatchedHistoryAddRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncWatchedHistoryAddRequestIsNotAbstract()
        {
            typeof(TraktSyncWatchedHistoryAddRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestTraktSyncWatchedHistoryAddRequestIsSealed()
        {
            typeof(TraktSyncWatchedHistoryAddRequest).IsSealed.Should().BeTrue();
        }
    }
}
