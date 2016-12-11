﻿namespace TraktApiSharp.Tests.Experimental.Requests.Base.Post.Bodyless
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base;
    using TraktApiSharp.Experimental.Requests.Base.Post.Bodyless;

    [TestClass]
    public class ATraktNoContentBodylessPostRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post"), TestCategory("Bodyless")]
        public void TestATraktNoContentBodylessPostRequestIsAbstract()
        {
            typeof(ATraktNoContentBodylessPostRequest).IsAbstract.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Base"), TestCategory("Post"), TestCategory("Bodyless")]
        public void TestATraktNoContentBodylessPostRequestIsSubclassOfATraktNoContentRequest()
        {
            typeof(ATraktNoContentBodylessPostRequest).IsSubclassOf(typeof(ATraktNoContentRequest)).Should().BeTrue();
        }
    }
}
