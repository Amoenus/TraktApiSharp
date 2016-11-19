﻿namespace TraktApiSharp.Tests.Experimental.Requests.Shows
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TraktApiSharp.Experimental.Requests.Base.Get;
    using TraktApiSharp.Experimental.Requests.Interfaces;
    using TraktApiSharp.Experimental.Requests.Shows;
    using TraktApiSharp.Objects.Get.Shows;
    using TraktApiSharp.Requests;

    [TestClass]
    public class TraktShowSummaryRequestTests
    {
        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestIsNotAbstract()
        {
            typeof(TraktShowSummaryRequest).IsAbstract.Should().BeFalse();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestIsSealed()
        {
            typeof(TraktShowSummaryRequest).IsSealed.Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestIsSubclassOfATraktSingleItemGetByIdRequest()
        {
            typeof(TraktShowSummaryRequest).IsSubclassOf(typeof(ATraktSingleItemGetByIdRequest<TraktShow>)).Should().BeTrue();
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestHasAuthorizationNotRequired()
        {
            var request = new TraktShowSummaryRequest(null);
            request.AuthorizationRequirement.Should().Be(TraktAuthorizationRequirement.NotRequired);
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestHasValidUriTemplate()
        {
            var request = new TraktShowSummaryRequest(null);
            request.UriTemplate.Should().Be("shows/{id}{?extended}");
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestImplementsITraktObjectRequestInterface()
        {
            typeof(TraktShowSummaryRequest).GetInterfaces().Should().Contain(typeof(ITraktObjectRequest));
        }

        [TestMethod, TestCategory("Requests"), TestCategory("Shows")]
        public void TestTraktShowSummaryRequestImplementsITraktExtendedInfoInterface()
        {
            typeof(TraktShowSummaryRequest).GetInterfaces().Should().Contain(typeof(ITraktExtendedInfo));
        }
    }
}
