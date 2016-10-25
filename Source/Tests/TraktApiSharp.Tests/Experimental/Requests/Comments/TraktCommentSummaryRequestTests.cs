﻿namespace TraktApiSharp.Tests.Experimental.Requests.Comments
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Comments;
    using TraktApiSharp.Objects.Basic;

    [TestClass]
    public class TraktCommentSummaryRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("Without OAuth")]
        public void TestTraktCommentSummaryRequestIsNotAbstract()
        {
            typeof(TraktCommentSummaryRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("Without OAuth")]
        public void TestTraktCommentSummaryRequestIsSealed()
        {
            typeof(TraktCommentSummaryRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Comments"), TestCategory("Without OAuth")]
        public void TestTraktCommentSummaryRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            typeof(TraktCommentSummaryRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktComment>)).Should().BeTrue();
        }
    }
}
