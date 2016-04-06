﻿namespace TraktApiSharp.Tests.Enums
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Enums;

    [TestClass]
    public class TraktAuthenticationModeTests
    {
        [TestMethod]
        public void TestTraktAuthenticationModeHasMembers()
        {
            typeof(TraktAuthenticationMode).GetEnumNames().Should().HaveCount(2)
                                                          .And.Contain("Device", "OAuth");
        }

        [TestMethod]
        public void TestTraktAuthenticationModeGetAsString()
        {
            TraktAuthenticationMode.Device.AsString().Should().Be("Device");
            TraktAuthenticationMode.OAuth.AsString().Should().Be("OAuth");
        }
    }
}
