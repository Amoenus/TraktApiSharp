﻿namespace TraktApiSharp.Tests.Experimental.Requests.Comments.OAuth
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Comments.OAuth;

    [TestClass]
    public class TraktEpisodeCommentPostRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktEpisodeCommentPostRequestIsNotAbstract()
        {
            typeof(TraktEpisodeCommentPostRequest).IsAbstract.Should().BeFalse();
        }
    }
}
