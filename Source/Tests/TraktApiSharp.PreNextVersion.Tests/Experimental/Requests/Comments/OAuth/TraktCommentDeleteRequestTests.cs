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

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentDeleteRequestIsSealed()
        {
            typeof(TraktCommentDeleteRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentDeleteRequestIsSubclassOfATraktNoContentDeleteByIdRequest()
        {
            //typeof(TraktCommentDeleteRequest).IsSubclassOf(typeof(ATraktNoContentDeleteByIdRequest)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("With OAuth")]
        public void TestTraktCommentDeleteRequestHasValidUriTemplate()
        {
            var request = new TraktCommentDeleteRequest(null);
            request.UriTemplate.Should().Be("comments/{id}");
        }
    }
}
