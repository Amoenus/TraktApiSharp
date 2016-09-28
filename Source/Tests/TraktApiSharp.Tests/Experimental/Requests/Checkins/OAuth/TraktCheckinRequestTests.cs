﻿namespace TraktApiSharp.Tests.Experimental.Requests.Checkins.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Checkins.OAuth;

    [TestClass]
    public class TraktCheckinRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Checkins"), TestCategory("With OAuth")]
        public void TestTraktCheckinRequestIsNotAbstract()
        {
            typeof(TraktCheckinRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Checkins"), TestCategory("With OAuth")]
        public void TestTraktCheckinRequestIsSealed()
        {
            typeof(TraktCheckinRequest).IsSealed.Should().BeTrue();
        }
    }
}
