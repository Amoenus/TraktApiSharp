﻿namespace TraktApiSharp.Tests.Experimental.Requests.Syncs.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Syncs.OAuth;

    [TestClass]
    public class ATraktSyncListRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Syncs")]
        public void TestATraktSyncListRequestIsAbstract()
        {
            typeof(ATraktSyncListRequest).IsAbstract.Should().BeTrue();
        }
    }
}
