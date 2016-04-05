﻿namespace TraktApiSharp.Tests.Enums
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Enums;

    [TestClass]
    public class TraktPeriodTests
    {
        [TestMethod]
        public void TestHasMembers()
        {
            typeof(TraktPeriod).GetEnumNames().Should().HaveCount(4)
                                              .And.Contain("Weekly", "Monthly", "Yearly", "All");
        }

        [TestMethod]
        public void TestGetAsString()
        {
            TraktPeriod.Weekly.AsString().Should().Be("weekly");
            TraktPeriod.Monthly.AsString().Should().Be("monthly");
            TraktPeriod.Yearly.AsString().Should().Be("yearly");
            TraktPeriod.All.AsString().Should().Be("all");
        }
    }
}
