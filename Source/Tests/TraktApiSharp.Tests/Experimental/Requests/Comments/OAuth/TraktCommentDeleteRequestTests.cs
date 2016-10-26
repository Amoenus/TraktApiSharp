﻿namespace TraktApiSharp.Tests.Experimental.Requests.Comments.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Comments.OAuth;

    [TestClass]
    public class TraktCommentDeleteRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentDeleteRequestIsNotAbstract()
        {
            typeof(TraktCommentDeleteRequest).IsAbstract.Should().BeFalse();
        }
    }
}
