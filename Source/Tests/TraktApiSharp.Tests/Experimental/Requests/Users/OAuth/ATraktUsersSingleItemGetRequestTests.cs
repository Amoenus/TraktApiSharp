﻿namespace TraktApiSharp.Tests.Experimental.Requests.Users.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Users.OAuth;

    [TestClass]
    public class ATraktUsersSingleItemGetRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Users")]
        public void TestATraktUsersSingleItemGetRequestIsAbstract()
        {
            typeof(ATraktUsersSingleItemGetRequest).IsAbstract.Should().BeTrue();
        }
    }
}
