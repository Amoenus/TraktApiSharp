﻿namespace TraktApiSharp.Tests.Experimental.Requests.Calendars
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Calendars;

    [TestClass]
    public class TraktCalendarAllShowsRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Calendars"), TestCategory("Without OAuth"), TestCategory("Shows")]
        public void TestTraktCalendarAllShowsRequestIsNotAbstract()
        {
            typeof(TraktCalendarAllShowsRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Calendars"), TestCategory("Without OAuth"), TestCategory("Shows")]
        public void TestTraktCalendarAllShowsRequestIsSealed()
        {
            typeof(TraktCalendarAllShowsRequest).IsSealed.Should().BeTrue();
        }
    }
}
